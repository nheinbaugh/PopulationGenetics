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
            var param = parameter as IAllele;
            if (param == null)
            {
                var coDom = value as Func<string, int>;
                if (coDom != null)
                    return coDom.Invoke(parameter.ToString());
            }
            var bob = value as Func<IAllele, int>;
            if(bob != null)
                return bob.Invoke((IAllele)parameter);


            return -1;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
