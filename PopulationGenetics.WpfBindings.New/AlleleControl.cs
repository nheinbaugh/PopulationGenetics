using System;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.WpfBindings
{
    public class AlleleControl : IAlleleControl
    {
        private Func<object, int> _query;
        private StackPanel _stackPanel;
        private IAllele _allele;
        private bool _isCoDominant;

        public StackPanel StackPanel => _stackPanel;

        public AlleleControl(IAllele allele, StackPanel sp, Func<object, int> query, bool coDominant = false)
        {
            _allele = allele;
            _stackPanel = sp;
            _query = query;
            _isCoDominant = coDominant;
        }

        public void UpdateControlValue()
        {
            var val = -1;
            val = _isCoDominant ? _query.Invoke(_allele.Representation) : _query.Invoke(_allele);
            var tb = _stackPanel.Children[1] as TextBox;
            tb?.SetValue(TextBox.TextProperty, val.ToString());
        } 
    }
}