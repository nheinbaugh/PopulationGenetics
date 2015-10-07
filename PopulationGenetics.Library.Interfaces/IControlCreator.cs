using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IControlManager
    {
        StackPanel CreateDataPair(string controlName, string label, string bindingPath, object source);
        StackPanel CreateDataPairLinq(string controlName, string labelContent, Func<IAllele, int> bindingSource, IValueConverter converter, IAllele allele);

        StackPanel CreateCoDominantPairLinq(string controlName, string labelContent,
            Func<string, int> bindingSource, IValueConverter converter);

        StackPanel CreateLocusSelector(ILocusBank registeredGenes, IWorld world);
    }
}