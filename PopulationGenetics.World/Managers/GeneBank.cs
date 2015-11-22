using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;

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
            IAllele allele;
            _alleleMap.TryGetValue(alleleId, out allele);
            return allele;
        }

        // Could this be async?
        public void UpdateVisibleControls(Grid targetGrid)
        {
            foreach (var locus in _loci)
            {
                var controls = locus.AlleleManager.Controls;
                if (!locus.isVisibleLocus)
                {
                    HideControls(targetGrid, controls);
                }
                else
                {
                    ShowControls(targetGrid, controls);
                }

            }
        }

        private static void ShowControls(Grid targetGrid, List<IAlleleControl> controls)
        {
            var currentRow = 1;
            foreach (var control in controls)
            {
                control.UpdateControlValue();
                control.StackPanel.Visibility = Visibility.Visible;
                var totalRows = targetGrid.RowDefinitions.Count;
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

        private static void HideControls(Grid targetGrid, List<IAlleleControl> controls)
        {
            foreach (var control in controls)
            {
                control.StackPanel.Visibility = Visibility.Collapsed;
                targetGrid.Children.Add(control.StackPanel);
            }
        }
    }
}
