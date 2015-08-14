using System;
using System.Collections.Generic;
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
    public class LocusBank: ILocusBank
    {
        private List<ILocus> _loci;
        private readonly IControlManager _controlManager;
        private IWorld _world;

        public List<ILocus> Loci => _loci;

        public LocusBank(IControlManager controlManager, IWorld world)
        {
            _world = world;
            _controlManager = controlManager;
            _loci = new List<ILocus>();
        }

        /// <summary>
        /// Use to add a predefined gene to the gene pool
        /// </summary>
        /// <param name="newLocus">The locus to be added to the pool of known gene positions</param>
        public void AddToBank(ILocus newLocus)
        {
            _loci.Add(newLocus);
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

        public void CreateGeneControls(Func<IAllele, int> allelePopulation, Grid targetGrid)
        {
            var spList = new List<StackPanel>();
            var currentRow = 0;
            foreach (var locus in _loci)
            {
                foreach (var allele in locus.AlleleManager.Alleles)
                {
                    var totalRows = targetGrid.RowDefinitions.Count;
                    var func = new Func<IAllele, int>(allelePopulation);
                    var sp = _controlManager.CreateDataPairLinq(allele.Representation, allele.Representation + " Populus",
                         func, new ValueConverter(), allele);
                    var tb = sp.Children[1] as TextBox;
                    sp.Margin = new Thickness(5, 5, 5, 5);
                    sp.HorizontalAlignment = HorizontalAlignment.Right;

                    tb.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                    Grid.SetColumn(sp, 0);
                    if (currentRow >= totalRows)
                    {
                        var rd = new RowDefinition { Height = GridLength.Auto };
                        targetGrid.RowDefinitions.Add(rd);
                    }
                    Grid.SetRow(sp, currentRow);
                    currentRow++;
                    spList.Add(sp);
                    targetGrid.Children.Add(sp);
                }
            }
        }
    }
}
