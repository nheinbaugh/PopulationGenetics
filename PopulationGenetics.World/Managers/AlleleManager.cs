using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.WpfBindings;

namespace PopulationGenetics.Library.Managers
{


    public class AlleleManager : IAlleleManager
    {
        private List<IAllele> _alleles;
        private IControlManager _controlManager;
        private IWorld _world;
        private List<AlleleControl> _controls;

        public List<IAllele> Alleles { get { return _alleles; } }
        public List<AlleleControl> Controls { get { return _controls; } }

        public AlleleManager(IControlManager controlManager, IWorld world)
        {
            _controls = new List<AlleleControl>();
            _controlManager = controlManager;
            _alleles = new List<IAllele>();
            _world = world;
        }

        public void CreateAllele(IAllele allele)
        {
            _alleles.Add(allele);
        }

        public void CreateMultipleAlleles(IEnumerable<IAllele> alleles)
        {
            _alleles.AddRange(alleles);
        }

  

        private void CreateAlleleControls(Func<IAllele, int> allelePopulation)
        {
            foreach (var allele in _alleles)
            {
                var func = new Func<IAllele, int>(allelePopulation);
                var sp = _controlManager.CreateDataPairLinq(allele.Representation, allele.Representation + " Populus",
                     func, new ValueConverter(), allele);
                sp.Margin = new Thickness(5, 5, 5, 5);
                sp.HorizontalAlignment = HorizontalAlignment.Right;
                var control = new AlleleControl(allele, sp, func);
                _controls.Add(control);
            }
        }
    }
}
