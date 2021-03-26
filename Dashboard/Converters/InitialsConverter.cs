using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Dashboard.Converters
{
    public class InitialsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var newFullName = string.Empty;
            if (value is string fullName)
            {
                var split = fullName.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries); 
                var initials = string.Empty;
                foreach (var piece in split.Skip(1))
                {
                    if (!Char.IsLower(piece[0]))
                        initials = $"{initials}{piece[0]}.";
                }
                newFullName = $"{split[0]} {initials}";
            }
            return newFullName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
