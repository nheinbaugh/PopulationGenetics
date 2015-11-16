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
        private List<IAllele> _dominantAlleles;

        public List<IAllele> Alleles { get { return _alleles; } }
        public List<IAlleleControl> Controls { get { return _controls; } }

        public AlleleManager(IControlManager controlManager, IWorld world)
        {
            _controls = new List<IAlleleControl>();
            _controlManager = controlManager;
            _alleles = new List<IAllele>();
            _world = world;
            _dominantAlleles = new List<IAllele>();
        }

        public AlleleManager(List<IAllele> alleles)
        {
            _alleles = alleles;
        }

        public void CreateAllele(IAllele allele)
        {
            _alleles.Add(allele);
            CreateAlleleControls(allele);
            if (allele.IsDominant) _dominantAlleles.Add(allele);
        }

        public void CreateMultipleAlleles(IEnumerable<IAllele> alleles)
        {
            _alleles.AddRange(alleles);
        }

        public void UpdateControls()
        {
            foreach (var control in _controls)
            {
                control.UpdateControlValue();
            }
        }

        public void GetAlleleControlByName()
        {

        }


        private void CreateAlleleControls(IAllele allele)
        {
            if (allele.IsDominant) CreateCoDominantControls(allele);
            CreateControl(allele, allele.Representation);
        }

        private void CreateCoDominantControls(IAllele allele)
        {
            foreach (var all in _dominantAlleles)
            {
                var rep = GeneRepresentationBuilder.CreateName(all.Representation, allele.Representation);
                var func = new Func<object, int>(CoDominantPopulation);
                var cdAll = new Allele(rep, false);
                var sp = _controlManager.CreateCoDominantPairLinq(rep, rep + " Population" + " Populus",
                 func, new ValueConverter());
                var control = new AlleleControl(cdAll, sp, func, true);
                control.UpdateControlValue();
                _controls.Add(control);
            }
            
        }

        private void CreateControl(IAllele allele, string representation)
        {
            var func = new Func<object, int>(AllelePopulation);
            var sp = _controlManager.CreateDataPairLinq(representation, representation + " Population" + " Populus",
                 func, new ValueConverter(), allele);

            var control = new AlleleControl(allele, sp, func);
            _controls.Add(control);
        }

        /// <summary>
        /// Default query to determine the number of the population with a given allele representation
        /// </summary>
        /// <param name="all">Must be an IAllele</param>
        /// <returns></returns>
        private int AllelePopulation(object all)
        {
            var allele = all as IAllele;
               return _world.Population.Populus.SelectMany(b => b.Genes, (b, g) => new { b, g })
                    .Where(t => (t.g.Representation == allele.Representation) && (t.g.FirstAlleleId == allele.Id || t.g.SecondAlleleId == allele.Id))
                    .Select(t => t.b).ToList().Count;
        }

        /// <summary>
        /// Default query to determine the number of the population with a given CoDominant gene representation
        /// </summary>
        /// <param name="representation"></param>
        /// <returns></returns>
        private int CoDominantPopulation(object representation)
        {
            var ro = from b in _world.Population.Populus
                     from g in b.Genes
                     where g.Representation == representation.ToString()
                     select b;
            return ro.ToList().Count;
        }
    }
}
