using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Population : IPopulation, INotifyPropertyChanged
    {
        private readonly List<IPerson> _populus;
        private readonly IPersonFactory _personFactory;
        public event PropertyChangedEventHandler PropertyChanged;
        private int _males;
        private int _females;

        public int PopulationSize => _populus.Count;
        public int Males => _males;
        public int Females => _females;

        public List<IPerson> Populus
        {
            get { return _populus; }
        }

        public Population(IPersonFactory pf)
        {
            _populus = new List<IPerson>();
            _personFactory = pf;
        }

        public void CreatePopulation(int numberToCreate, ILocusBank loci)
        {
            for (int i = 0; i < numberToCreate; i++)
            {
                _populus.Add(_personFactory.CreateNewPerson(loci));
            }
            NotifyPropertyChanged("PopulationSize");
            NotifyPropertyChanged("Males");
            NotifyPropertyChanged("Females");
        }

        public void UpdatePopulus()
        {
           _males=  _populus.AsQueryable().Where(p => p.IsFemale == false).ToList().Count;
           _females=  _populus.AsQueryable().Where(p => p.IsFemale == true).ToList().Count;
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

    }
}
