using MyBOEMobile.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyBOEMobile.Converters
{
    public class ChatImageConverter : IMultiValueConverter
    {
       

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values != null)
            {
                if ((bool)values[0])
                {
                    return null;
                }
                else
                {
                    return ((ContactInfo)parameter).ImagePath;
                }
            }
            return null;
        }

        

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
