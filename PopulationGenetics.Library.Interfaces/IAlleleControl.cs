using System.Windows.Controls;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IAlleleControl
    {
        StackPanel StackPanel { get; }
        void UpdateControlValue();
    }
}