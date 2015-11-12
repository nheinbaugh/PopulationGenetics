using System;
using System.Windows.Controls;

public class AlleleSelectionChangedEventArgs : EventArgs, IAlleleSelectionChangedEventArgs
{
    public Grid TargetGrid { get { return _targetGrid; } }

    private Grid _targetGrid;

    public AlleleSelectionChangedEventArgs(Grid targetGrid)
    {
        _targetGrid = targetGrid;
    }

}
