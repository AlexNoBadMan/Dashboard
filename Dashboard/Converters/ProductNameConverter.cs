using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dashboard.Converters
{
    public class ProductNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //var newProductName = string.Empty;
            if (!(value is string productName))
                    return string.Empty;
            var firstSpace = productName.IndexOf(' ');
            if (firstSpace > 0)
                productName = productName.Remove(0, firstSpace);
            return productName.Replace(" (Консорциум Развитие)", string.Empty).Trim();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
