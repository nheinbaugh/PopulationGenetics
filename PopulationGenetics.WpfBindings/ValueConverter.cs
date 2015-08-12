using System;
using System.Globalization;
using System.Windows.Data;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.WpfBindings
{
    public class ValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bob = value as Func<IAllele, int>;
            return bob.Invoke((IAllele)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
