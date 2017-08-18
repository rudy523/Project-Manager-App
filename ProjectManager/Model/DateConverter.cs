using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Text.RegularExpressions;

namespace ProjectManager.Model
{
    [ValueConversion(typeof(DateTime), typeof(string))]
    public class DateConverter : IValueConverter
    {
        #region Methods
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;
            return date.ToString("D", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string amount = value.ToString();
            try
            {
                return Parse(amount);
            }
            catch (Exception)
            {
                return value;
                throw;
            }                   
        }

        private static decimal Parse(string input)
        {
            string pattern = @"[\$\,]";
            return decimal.Parse(Regex.Replace(input, pattern, ""));
        }
        #endregion

    }
}
