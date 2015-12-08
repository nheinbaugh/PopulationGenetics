using PopulationGenetics.Library.Interfaces;
using System.Windows;
using System.Windows.Controls;

public class LocusComboBox : ComboBox, ILocusComboBox
{
    public LocusComboBox()
    {
        SelectedIndex = 0;
    }
    protected override void OnSelectionChanged(SelectionChangedEventArgs e)
    {
        var tbb = FindName("geneGrid");
        UpdateVisibleLocus();
        if (tbb != null)
        {
            UpdateVisibleControls((Grid)tbb);
        }
        base.OnSelectionChanged(e);
    }

    private void UpdateVisibleLocus()
    {
        foreach (ILocus item in Items)
        {
            if (item != SelectedItem) item.isVisibleLocus = false;
        }
        var locus = (ILocus)SelectedItem;
        locus.isVisibleLocus = true;
    }

    private void UpdateVisibleControls(Grid targetGrid)
    {
        var window = Application.Current.MainWindow as IMainWindow;
        foreach (var locus in window.World.GeneBank.Loci)
        {
            var controls = locus.AlleleManager.Controls;
            if (!locus.isVisibleLocus)
            {
                foreach (var control in controls)
                {
                    control.StackPanel.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                var currentRow = 1;
                foreach (var control in controls)
                {
                    control.UpdateControlValue();

                    var totalRows = targetGrid.RowDefinitions.Count;
                    Grid.SetColumn(control.StackPanel, 0);
                    if (currentRow >= totalRows)
                    {
                        var rd = new RowDefinition { Height = GridLength.Auto };
                        targetGrid.RowDefinitions.Add(rd);
                    }
                    Grid.SetRow(control.StackPanel, currentRow);
                    currentRow++;
                    control.StackPanel.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
