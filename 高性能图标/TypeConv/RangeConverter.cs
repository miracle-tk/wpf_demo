using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 高性能图标.Model;

namespace 高性能图标.TypeConv
{
    public class RangeConverter:TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            
            if(value is string)
            {
                var temp = value.ToString();
                var strArry = temp.Replace(" ", "").Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries);
                if (strArry.Count() == 2&& double.TryParse(strArry[0], out double d0) && double.TryParse(strArry[1], out double d1)) return new Range(d0, d1);



            }
            return base.ConvertFrom(context, culture, value);
        }

    }
}
