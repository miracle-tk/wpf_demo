using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace 高性能图标.impl
{
    public abstract class AxisLabel:AixsComponentBase
    {
        public AxisLabel()
        {
            SetCurrentValue(FontFamilyProperty, new FontFamily("Consolas"));
            SetCurrentValue(FontSizeProperty, 20d);
            SetCurrentValue(ForegroundProperty, Brushes.Red);
        }
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == FontFamilyProperty || e.Property == FontSizeProperty || e.Property == ForegroundProperty)
            {
                Refresh();
            }
        }

    }
}
