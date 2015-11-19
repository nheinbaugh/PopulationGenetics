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
        var tbb = this.FindName("geneGrid");
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
        var bob = FindName("MainWindow");
        var window = Application.Current.MainWindow as IMainWindow;
        var currentRow = 1;
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
                foreach (var control in controls)
                {
                    control.UpdateControlValue();

                    var totalRows = targetGrid.RowDefinitions.Count;
                    control.StackPanel.HorizontalAlignment = HorizontalAlignment.Right;

                    Grid.SetColumn(control.StackPanel, 0);
                    if (currentRow >= totalRows)
                    {
                        var rd = new RowDefinition { Height = GridLength.Auto };
                        targetGrid.RowDefinitions.Add(rd);
                    }
                    Grid.SetRow(control.StackPanel, currentRow);
                    currentRow++;
                    control.StackPanel.Visibility = Visibility.Visible;
                    //targetGrid.Children.Add(control.StackPanel);
                }
            }
        }
    }
}
