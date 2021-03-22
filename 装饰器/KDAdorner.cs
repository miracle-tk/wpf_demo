using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace 装饰器
{
    public class KDAdorner : Adorner
    {
        public KDAdorner(UIElement adornedElement) : base(adornedElement)
        {
            AdornerLayer al = AdornerLayer.GetAdornerLayer(adornedElement);
            ControlTemplate ct;
            
        }
    }
}
