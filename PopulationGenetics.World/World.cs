using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.WpfBindings;

namespace PopulationGenetics.Library
{
    public class World : IWorld, INotifyPropertyChanged
    {
        private readonly IPopulation _population;
        private readonly ILocusBank _registeredGenes;
        private readonly IPersonFactory _personFactory;
        private int _age;
        private readonly IControlManager _controlManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public IPersonFactory PersonFactory => _personFactory;
        public IPopulation Population => _population;
        public ILocusBank RegisteredGenes => _registeredGenes;
        public int Age => _age;


        public World(IPopulation pop, ILocusBank genes, IPersonFactory personFactory, IControlManager controlManager)
        {
            if (pop?.Populus?.Count > 0) pop.Populus.Clear();
            _population = pop;
            _registeredGenes = genes;
            _personFactory = personFactory;
            _controlManager = controlManager;
            WorldSeeds.BaseGenes(_registeredGenes);
            SeedWorld(1000);
        }

        public World(int seedSize)
        {
            SeedWorld(seedSize);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SeedWorld(int seedSize)
        {
            _population.CreatePopulation(seedSize);
        }

        public void ProcessTurn()
        {
            _age++;
            foreach (var person in _population.Populus)
            {


            }
            NotifyPropertyChanged("Age");
        }

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
            var spList = new List<StackPanel>();
            CreateGeneControls(targetGrid);
            var pop = _controlManager.CreateDataPair("pop", "Populus", "Population.PopulationSize", this);
            var age = _controlManager.CreateDataPair("age", "World Age", "Age", this);
            Grid.SetColumn(pop, 1);
            Grid.SetColumn(age, 1);
            Grid.SetRow(age, 1);
            Grid.SetRow(pop, 0);
            spList.Add(pop);
            spList.Add(age);
            targetGrid.Children.Add(pop);
            targetGrid.Children.Add(age);
            targetGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = GridLength.Auto});
            return spList;
        }

        private int AllelePopulation(IAllele allele)
        {
            var ro = from b in _population.Populus
                from g in b.Genes
                where g.Representation == allele.Representation
                select b;
            return ro.ToList().Count;
        }

        public void CreateGeneControls(Grid targetGrid)
        {
            var spList = new List<StackPanel>();
            foreach (var locus in _registeredGenes.Loci)
            {
                foreach (var allele in locus.AlleleManager.Alleles)
                {
                    var func = new Func<IAllele, int>(AllelePopulation);
                    var sp = _controlManager.CreateDataPairLinq(allele.Representation, allele.Representation + " Populus",
                         func, new ValueConverter(), allele);
                    var tb = sp.Children[1] as TextBox;
                    tb.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    Grid.SetColumn(sp, 0);

                    spList.Add(sp);
                }
            }
            var rowCount = targetGrid.RowDefinitions.Count;
            for (int i = 0; i < spList.Count; i++)
            {
                var sp = spList[i];
                sp.Margin = new Thickness(5, 5, 5, 5);
                sp.HorizontalAlignment = HorizontalAlignment.Right;
                if (i >= rowCount)
                {
                    var rd = new RowDefinition { Height = GridLength.Auto };
                    targetGrid.RowDefinitions.Add(rd);
                }
                
                Grid.SetRow(sp, i);
                targetGrid.Children.Add(sp);
            }
        }
    }
}