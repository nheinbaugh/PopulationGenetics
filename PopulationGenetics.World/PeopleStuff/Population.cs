using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Population : IPopulation, INotifyPropertyChanged
    {
        private readonly HashSet<IPerson> _populus;
        private readonly IPersonFactory _personFactory;
        public event PropertyChangedEventHandler PropertyChanged;
        private int _males;
        private int _females;

        public int PopulationSize => _populus.Count;
        public int Males => _males;
        public int Females => _females;

        public HashSet<IPerson> Populus
        {
            get { return _populus; }
        }

        public Population(IPersonFactory pf)
        {
            _populus = new HashSet<IPerson>();
            _personFactory = pf;
        }

        public void CreatePopulation(int numberToCreate, IGeneBank loci)
        {
            for (int i = 0; i < numberToCreate; i++)
            {
                _populus.Add(_personFactory.CreateNewPerson(loci));
            }
            NotifyPropertyChanged("PopulationSize");
        }

        /// <summary>
        /// Update the properties which contain counts of how many males and females are in the Population. This is calculated once per turn.
        /// </summary>
        public void UpdatePopulus()
        {
           _males=  _populus.AsQueryable().Where(p => p.IsFemale == false && p.EligibleForBreeding).ToList().Count;
           _females=  _populus.AsQueryable().Where(p => p.IsFemale && p.EligibleForBreeding).ToList().Count;
        }

        public void DestroyPopulation()
        {
            _populus.Clear();
            NotifyPropertyChanged("PopulationSize");
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddGeneration(ConcurrentBag<IPerson> children)
        {
            _populus.UnionWith(children);
            UpdatePopulus();
        }
    }
}
