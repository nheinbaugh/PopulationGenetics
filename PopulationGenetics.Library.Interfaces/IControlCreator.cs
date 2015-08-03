using System.Windows.Controls;

public interface IControlManager
{
    StackPanel CreateDataPair(string controlName, string label);
}