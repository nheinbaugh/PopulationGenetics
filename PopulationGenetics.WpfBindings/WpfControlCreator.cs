using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.WpfBindings
{
    public class WpfControlManager : IControlManager
    {

        public StackPanel CreateDataPair(string controlName, string labelContent, string bindingPath, object source)
        {
            var bob = new StackPanel();
            bob.Orientation = Orientation.Horizontal;
            bob.Name = controlName + "Box";
            bob.Height = 23;
            var label = new Label();
            label.Name = controlName + "Label";
            label.Content = labelContent;
            label.MinWidth = 100;
            var tb = new TextBox();
            var binding = new Binding();
            binding.Source = source;
            binding.Path = new PropertyPath(bindingPath);
            binding.Mode = BindingMode.OneWay;
            tb.SetBinding(TextBox.TextProperty, binding);

            tb.Name = controlName + "Value";
            tb.IsReadOnly = true;
            tb.MinWidth = 100;
            bob.Children.Add(label);
            bob.Children.Add(tb);
            return bob;
        }
    }
}
