using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace 高性能图标.impl
{
    public class HorizontalAxisLabel : AxisLabel
    {
        #region 构造器
        public HorizontalAxisLabel()
        {
           
            
        }
        #endregion
        #region 方法
        protected override double Normalize(double v)
        {
            return (v - Range.Min) * kx;
        }   

        protected override void Refresh()
        {

            if (!CanRender()) return;
            Root.Children.Clear();
            kx = RenderSize.Width / Range.Distance;
            double width = RenderSize.Width / (Range.Distance / Step);
            var v = Range.Min;
            double x;
            while (v <= Range.Max)
            {
                x = Normalize(v);
                var label = new Label
                {
                    Content = v,
                    Width=width,
                    HorizontalContentAlignment=HorizontalAlignment.Center,
                    FontFamily=FontFamily,
                    FontSize=FontSize,
                    Foreground=Foreground,
                   
                };
                Root.Children.Add(label);
                Canvas.SetLeft(label, x-width/2.0);
                v += Step;
            }

        }
        protected override bool CanRender()
        {
            return base.CanRender() && (RenderSize.Width > 0);
        }

       
        #endregion
        #region 依赖属性

        #endregion
        #region 字段
        private double kx = 0;
        #endregion
        

        
   
    }
}
