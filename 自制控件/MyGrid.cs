using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace 自制控件
{
    public class MyGrid:Grid
    {
        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }
        protected override Size ArrangeOverride(Size arrangeSize)
        {
            return base.ArrangeOverride(arrangeSize);
        }
    }
}
