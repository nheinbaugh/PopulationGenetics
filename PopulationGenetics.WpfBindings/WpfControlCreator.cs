using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.WpfBindings
{
    public class WpfControlManager : IControlManager
    {

        private StackPanel CreateDataPairBase(string controlName, string labelContent)
        {
            var stackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Name = controlName + "Box",
                Height = 23
            };
            var label = new Label
            {
                Name = controlName + "Label",
                Content = labelContent,
                MinWidth = 100
            };
            var tb = new TextBox
            {
                Name = controlName + "Value",
                IsReadOnly = true,
                MinWidth = 100
            };
            stackPanel.Children.Add(label);
            stackPanel.Children.Add(tb);
            return stackPanel;
        }

        public StackPanel CreateDataPair(string controlName, string labelContent, string bindingPath, object source)
        {
            var stackPanel = CreateDataPairBase(controlName, labelContent);
            var binding = new Binding
            {
                Source = source,
                Path = new PropertyPath(bindingPath),
                Mode = BindingMode.OneWay
            };

            var tb = stackPanel.Children[1] as TextBox;
            tb.SetBinding(TextBox.TextProperty, binding);
            
            return stackPanel;
        }

        public StackPanel CreateDataPairLinq(string controlName, string labelContent)
        {
            var stackPanel = CreateDataPairBase(controlName, labelContent);
            return stackPanel;
        }
    }
}
