using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FileWatcher.converter
{
    public class ImagepathToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                var temp = value.ToString();
                var list = temp.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                temp = list.Last();
                temp = temp.Split('.').First().ToLower();
                switch (temp)
                {
                    case "folder":
                        return Brushes.Yellow;
                    case "pdf":
                        return stringToBrush("#EB5E5E");
                    case "ppt":
                        return stringToBrush("#FFCC66");
                    case "rar":
                        return stringToBrush("#F95F5D");
                    case "txt":
                        return Brushes.Black;
                    case "word":
                        return stringToBrush("#558FF2");
                    default:
                        return Brushes.Black;
                }
            }
            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
        private Brush stringToBrush(string s)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(s);
            return brush;
        }
    }
}
