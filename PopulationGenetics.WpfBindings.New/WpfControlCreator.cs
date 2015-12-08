using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.WpfBindings
{
    public class WpfControlManager : IControlManager
    {

        public StackPanel CreateLocusSelector(IGeneBank registeredGenes, Grid parent, IWorld world)
        {
            var sp = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Name = "locusSelector"
            };
            sp.Margin = new Thickness(0, 5, 5, 0);
            var label = new Label
            {
                Name = "selectorLabel",
                Content = "Active Locus (for View)",
                HorizontalAlignment = HorizontalAlignment.Left
            };
            var cb = new LocusComboBox
            {
                Name = "selectorBox",
                ItemsSource = registeredGenes.Loci,
                MaxHeight = 23,
                Margin = new Thickness(0, 0, 0, 5)
            };
            sp.Children.Add(label);
            sp.Children.Add(cb);
            parent.Children.Add(sp);
            return sp;
        }

        private StackPanel CreateDataPairBase(string controlName, string labelContent)
        {
            var stackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Right,
                Orientation = Orientation.Horizontal,
                Name = controlName + "Box",
                MinHeight = 26,
                MaxHeight = 26
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
            stackPanel.Margin = new Thickness(0, 5, 5, 0);

            stackPanel.Children.Add(label);
            stackPanel.Children.Add(tb);
            return stackPanel;
        }
        
        public StackPanel CreateDataPair(string controlName, string labelContent, string bindingPath, Grid parent, object source)
        {
            var stackPanel = CreateDataPairBase(controlName, labelContent);
            var binding = new Binding
            {
                Source = source,
                Path = new PropertyPath(bindingPath),
                Mode = BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var tb = stackPanel.Children[1] as TextBox;
            tb?.SetBinding(TextBox.TextProperty, binding);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Right;
            parent.Children.Add(stackPanel);
            return stackPanel;
        }

        public StackPanel CreateDataPairLinq(string controlName, string labelContent, Func<IAllele, int> bindingSource, IValueConverter converter, IAllele allele)
        {
            var stackPanel = CreateDataPairBase(controlName, labelContent);
            var binding = new Binding
            {
                Source = bindingSource,
                Mode = BindingMode.OneWay,
                Converter = converter,
                ConverterParameter = allele
            };
            var tb = stackPanel.Children[1] as TextBox;
            tb?.SetBinding(TextBox.TextProperty, binding);
            return stackPanel;
        }

        public StackPanel CreateCoDominantPairLinq(string controlName, string labelContent,
            Func<string, int> bindingSource, IValueConverter converter)
        {
            var stackPanel = CreateDataPairBase(controlName, labelContent);
            var binding = new Binding
            {
                Source = bindingSource,
                Mode = BindingMode.OneWay,
                Converter = converter,
                ConverterParameter = controlName
            };
            var tb = stackPanel.Children[1] as TextBox;
            tb?.SetBinding(TextBox.TextProperty, binding);
            return stackPanel;
        }


    }
}
