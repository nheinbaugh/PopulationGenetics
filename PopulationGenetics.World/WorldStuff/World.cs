using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;

namespace PopulationGenetics.Library
{
    public class World : IWorld, INotifyPropertyChanged
    {
        private readonly IPopulation _population;
        private readonly IGeneBank _registeredGenes;
        private readonly IPersonFactory _personFactory;
        private int _age;
        private readonly IControlManager _controlManager;
        private IMortalityCurve _mortalityCurve;
        private IRandomGenerator _random;
        private List<TextBox> fieldsToUpdate = new List<TextBox>();

        public event PropertyChangedEventHandler PropertyChanged;

        public IPersonFactory PersonFactory => _personFactory;
        public IPopulation Population => _population;
        public IGeneBank GeneBank => _registeredGenes;
        public int Age => _age;
        public IMortalityCurve MortalityCurve => _mortalityCurve;


        public World(IPopulation pop, IPersonFactory pf, IControlManager controlManager, IMortalityCurve mortalityCurve, IRandomGenerator random)
        {
            if (pop?.Populus?.Count > 0) pop.Populus.Clear();
            _random = random;
            _population = pop;
            _registeredGenes = new GeneBank(controlManager, this);
            _personFactory = pf;
            _mortalityCurve = mortalityCurve;
            _controlManager = controlManager;
            WorldSeeds.BaseGenes(_registeredGenes, controlManager, this);
            SeedWorld(1000);
            _population?.UpdatePopulus();
        }


        public World(int seedSize)
        {
            SeedWorld(seedSize);
        }

        /// <summary>
        /// Seed a world with a populatin
        /// </summary>
        /// <param name="seedSize">Number of individuals to create for the new world</param>
        public void SeedWorld(int seedSize)
        {
            _population.CreatePopulation(seedSize, _registeredGenes);
        }

        public void ProcessTurn()
        {
            for (int i = 0; i < fieldsToUpdate.Count; i++)
            {
                var sender = fieldsToUpdate[i];
                sender.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
            _age++;
            CullPopulation();
            ProcessBreeding();
            NotifyPropertyChanged("Age");
            foreach (var locus in _registeredGenes.Loci)
            {
                if(locus.isVisibleLocus)
                    locus.AlleleManager.UpdateControls();
            }
        }

        /// <summary>
        /// Kill members of the population due to age
        /// </summary>
        private void CullPopulation()
        {
            var culledPopulation = new ConcurrentBag<IPerson>();
            Parallel.ForEach(_population.Populus, p =>
            {
                var survives = p.AgePerson(_mortalityCurve);
                if (!survives) culledPopulation.Add(p);
            });
            foreach (var culled in culledPopulation)
            {

                _population.Populus.Remove(culled);
            }
        }

        /// <summary>
        /// Run through the process of finding mates for the population and potentially creating offspring
        /// </summary>
        private void ProcessBreeding()
        {
            var children = new ConcurrentBag<IPerson>();
            var maleList = _population.Populus.Where(a => !a.IsFemale && a.EligibleForBreeding).ToList();
            var femaleList = _population.Populus.Where(a => a.IsFemale && a.EligibleForBreeding).ToList();
            Parallel.ForEach(_population.Populus, p =>
            {
                var child = p.IsFemale ? _personFactory.MakeBaby(p, maleList, _registeredGenes) :
                    _personFactory.MakeBaby(p, femaleList, _registeredGenes);
                if (child != null) children.Add(child);
            });
            _population.AddGeneration(children);
        }
        /// <summary>
        /// Clear the entire population and possibly clear the registered genes
        /// </summary>
        /// <param name="clearGenes">Whether to clear the gene bank or leave it populated with existing loci</param>
        public void CleanWorld(bool clearGenes)
        {
            _population.DestroyPopulation();
            _age = 0;
            if (clearGenes)
            {
                _registeredGenes.Loci.Clear();
            }
        }

        public List<StackPanel> CreateWorldControls(Grid targetGrid)
        {
            var rowCount = targetGrid.RowDefinitions.Count;
            var args = new AlleleSelectionChangedEventArgs(targetGrid);
            var lSelector = _controlManager.CreateLocusSelector(_registeredGenes, this);
            ((ComboBox)lSelector.Children[1]).SelectedIndex = 0;
            var parentSp = new StackPanel() { Orientation = Orientation.Vertical };
            var spList = new List<StackPanel>
            {
                lSelector,
                _controlManager.CreateDataPair("pop", "Populus", "Population.PopulationSize", this),
                _controlManager.CreateDataPair("male", "Eligible Males", "Population.Males", this),
                _controlManager.CreateDataPair("female", "Eligible Females", "Population.Females", this),
                _controlManager.CreateDataPair("age", "World Age", "Age", this)
            };
            for (int i = 0; i < spList.Count; i++)
            {
                var current = spList[i];
                if (i >= rowCount)
                {
                    targetGrid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetColumn(current, 1);
                Grid.SetRow(current, i);
                targetGrid.Children.Add(current);
                var bob= current.Children[1] as TextBox;
                if (bob != null) fieldsToUpdate.Add(bob);
            }

            targetGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
            return spList;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}