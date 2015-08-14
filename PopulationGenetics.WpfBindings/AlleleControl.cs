using System;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.WpfBindings
{
    public class AlleleControl : IAlleleControl
    {
        private Func<IAllele, int> _query;
        private StackPanel _stackPanel;
        private IAllele _allele;

        public StackPanel StackPanel { get { return _stackPanel; } }

        public AlleleControl(IAllele allele, StackPanel sp, Func<IAllele, int> query)
        {
            this._allele = allele;
            this._stackPanel = sp;
            this._query = query;
        }

        public void UpdateControlValue()
        {
            var val = _query.Invoke(_allele);
            var tb = _stackPanel.Children[1] as TextBox;
            tb?.SetValue(TextBox.TextProperty, val.ToString());
        } 
    }
}