using System;
using System.Windows.Controls;
public class WpfControlManager : IControlManager
{

    public StackPanel CreateDataPair(string controlName, string labelContent)
    {
        var bob = new StackPanel();
        bob.Orientation = Orientation.Horizontal;
        bob.Name = controlName + "Box";
        bob.MinWidth = 100;
        var label = new Label();
        label.Name = controlName + "Label";
        label.Content = labelContent;
        label.MinWidth = 100;
        var tb = new TextBox();
        tb.Name = controlName + "Value";
        tb.IsReadOnly = true;
        tb.Text = "7";
        bob.Children.Add(label);
        bob.Children.Add(tb);
        return bob;
    }
}
