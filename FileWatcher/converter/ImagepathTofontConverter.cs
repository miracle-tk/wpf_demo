using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FileWatcher.converter
{
    public class ImagepathTofontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var temp = value.ToString();
                var list=temp.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                temp= list.Last();
                temp =temp.Split('.').First().ToLower();
                switch (temp)
                {
                    case "folder":
                        return "\xe636";
                    case "pdf":
                        return "\xe722";
                    case "ppt":
                        return "\xe6a4";
                    case "rar":
                        return "\xe629";
                    case "txt":
                        return "\xe7d0";
                    case "word":
                        return "\xe6a3";
                    default:
                        return "\xe7b9";
                }
            }
            return "\xe7b9";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
