﻿using System.Windows.Controls;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IControlManager
    {
        StackPanel CreateDataPair(string controlName, string label, object source);
    }
}