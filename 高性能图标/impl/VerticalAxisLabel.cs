using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
namespace 高性能图标.impl
{
    public class VerticalAxisLabel : AxisLabel
    {
        protected override double Normalize(double v)
        {
            
            if (ky <= 0) return 0;
            return RenderSize.Height - ky * (v - Range.Min);
        }

        protected override void Refresh()
        {
            if (!CanRender()) return;
            Root.Children.Clear();
            var v = Range.Min;
            ky = RenderSize.Height / Range.Distance;
            double height = RenderSize.Height /( Range.Distance/Step);
            while (v <= Range.Max)
            {
                double y = Normalize(v);
                var label = new Label
                {
                    Height=height,
                    Content = v,
                    FontSize = FontSize,
                    FontFamily = FontFamily,
                    Foreground = Foreground,
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                Root.Children.Add(label);
                Canvas.SetTop(label, y-height/2.0);
                v += Step;
            }
        }
        protected override bool CanRender()
        {
            return base.CanRender() && (RenderSize.Height > 0);
        }
        private double ky = 0;
    }
}
