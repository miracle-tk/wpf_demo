using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 高性能图标.TypeConv;

namespace 高性能图标.Model
{
    [TypeConverter(typeof(RangeConverter))]
    public class Range
    {
        public double From { get; set; }
        public double To { get; set; }
        public double Max => Math.Max(From, To);
        public double Min => Math.Min(From, To);

        public double Distance => Math.Abs(Max - Min);
        public Range() { }
        public Range(double from,double to)
        {
            From = from;
            To = to;
        }


    }
}
