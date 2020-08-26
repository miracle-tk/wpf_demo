using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MircoSoftToDoDemo.Converters
{
    public class addtaskIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if (bool.TryParse(value.ToString(),out bool result))
                {
                    if (result)
                    {
                        return "⚪";
                    }
                    
                }
            }
            return "➕";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
