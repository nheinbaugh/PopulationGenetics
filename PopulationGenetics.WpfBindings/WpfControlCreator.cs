﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.WpfBindings
{
    public class WpfControlManager : IControlManager
    {
        public StackPanel CreateLocusSelector(ILocusBank registeredGenes, IWorld world)
        {
            var sp = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Name = "locusSelector",
                Height = 23
            };
            var label = new Label
            {
                Name = "selectorLabel",
                Content = "Active Locus (for View)"
            };
            var cb = new ComboBox
            {
                Name = "selectorBox",
                ItemsSource = registeredGenes.Loci
            };
            cb.SelectionChanged += GeneViewItemUpdate;
            sp.Children.Add(label);
            sp.Children.Add(cb);
            return sp;
        }

        private void GeneViewItemUpdate(object sender, SelectionChangedEventArgs e)
        {
            var bob = sender as ComboBox;
            var item = (ILocus)bob.SelectedItem;
            item.isVisibleLocus = true;
        }

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
            tb?.SetBinding(TextBox.TextProperty, binding);
            stackPanel.Margin = new Thickness(5, 5, 5, 5);
            stackPanel.HorizontalAlignment = HorizontalAlignment.Right;
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
