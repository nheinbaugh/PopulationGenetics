using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<IAlleleControl> _controls;

        public List<IAllele> Alleles { get { return _alleles; } }
        public List<IAlleleControl> Controls { get { return _controls; } }

        public AlleleManager(IControlManager controlManager, IWorld world)
        {
            _controls = new List<IAlleleControl>();
            _controlManager = controlManager;
            _alleles = new List<IAllele>();
            _world = world;
        }

        public void CreateAllele(IAllele allele)
        {
            _alleles.Add(allele);
            var control = CreateAlleleControls(allele);
            control.UpdateControlValue();
        }

        public void CreateMultipleAlleles(IEnumerable<IAllele> alleles)
        {
            _alleles.AddRange(alleles);
        }



        private AlleleControl CreateAlleleControls(IAllele allele)
        {
            var func = new Func<IAllele, int>(AllelePopulation);
            var sp = _controlManager.CreateDataPairLinq(allele.Representation, allele.Representation + " Populus",
                 func, new ValueConverter(), allele);
            sp.Margin = new Thickness(5, 5, 5, 5);
            sp.HorizontalAlignment = HorizontalAlignment.Right;
            var control = new AlleleControl(allele, sp, func);
            _controls.Add(control);
            return control;

        }

        private int AllelePopulation(IAllele allele)
        {
            var ro = from b in _world.Population.Populus
                     from g in b.Genes
                     where g.Representation == allele.Representation
                     select b;
            return ro.ToList().Count;
        }
    }
}
