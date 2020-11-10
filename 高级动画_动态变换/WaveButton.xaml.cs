using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 高级动画_动态变换
{
    /// <summary>
    /// WaveButton.xaml 的交互逻辑
    /// </summary>
    public partial class WaveButton : Button
    {
        public WaveButton()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(this);
            var path = Template.FindName("myPath", this) as Path;
            var opAni = new DoubleAnimation(0.3, 0, new Duration(TimeSpan.FromSeconds(1)));
            var EGeometry = Template.FindName("myEllipes", this) as EllipseGeometry;
            EGeometry.Center = pos;
            var xAni = new DoubleAnimation(0,150, new Duration(TimeSpan.FromSeconds(1)));
            EGeometry.BeginAnimation(EllipseGeometry.RadiusXProperty, xAni);
            
            path.BeginAnimation(Path.OpacityProperty, opAni);
            
        }
    }
}
