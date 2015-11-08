using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.WpfBindings;

namespace PopulationGenetics.Library.Managers
{
    /// <summary>
    /// The GeneBank class contains the representation of every locus that has been introduced into the population. 
    /// Each locus will have all alleles that are registered to it. 
    /// </summary>
    public class GeneBank: IGeneBank
    {
        private List<ILocus> _loci;
        private readonly IControlManager _controlManager;
        private Dictionary<Guid, IAllele> _alleleMap;
        private IWorld _world;

        public List<ILocus> Loci => _loci;

        public GeneBank(IControlManager controlManager, IWorld world)
        {
            _world = world;
            _controlManager = controlManager;
            _loci = new List<ILocus>();
            _alleleMap = new Dictionary<Guid, IAllele>();
        }

        /// <summary>
        /// Use to add a predefined gene to the gene pool
        /// </summary>
        /// <param name="newLocus">The locus to be added to the pool of known gene positions</param>
        public void AddToBank(ILocus newLocus)
        {
            _loci.Add(newLocus);
            foreach (var allele in newLocus.AlleleManager.Alleles)
            {
                _alleleMap.Add(allele.Id, allele);
            }
        }

        /// <summary>
        /// Generate a new gene to 'randomly' add to the gene pool
        /// </summary>
        public void AddToBank()
        {
            throw new NotImplementedException();
        }

        private void GetLocusByName()
        {
            //var bo = _loci.AsQueryable().Select
        }

        public IAllele GetAlleleById(Guid alleleId)
        {
            IAllele bob;
            _alleleMap.TryGetValue(alleleId, out bob);
            return bob;
        }

        public void CreateGeneControls(Grid targetGrid)
        {
            var currentRow = 1;
            foreach (var locus in _loci)
            {
                var controls = locus.AlleleManager.Controls;
                if (!locus.isVisibleLocus) continue;
                foreach (var control in controls)
                {
                    control.UpdateControlValue();

                    var totalRows = targetGrid.RowDefinitions.Count;
                    control.StackPanel.Margin = new Thickness(5, 5, 5, 5);
                    control.StackPanel.HorizontalAlignment = HorizontalAlignment.Right;

                    Grid.SetColumn(control.StackPanel, 0);
                    if (currentRow >= totalRows)
                    {
                        var rd = new RowDefinition { Height = GridLength.Auto };
                        targetGrid.RowDefinitions.Add(rd);
                    }
                    Grid.SetRow(control.StackPanel, currentRow);
                    currentRow++;
                    targetGrid.Children.Add(control.StackPanel);
                }
            }
        }
    }
}
