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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 图案解锁
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private List<ContentControl> Points=new List<ContentControl>();
        private Line currentLine;
        private void UniformGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.Source is ContentControl anchore)
            {
                CreateLine(anchore);
               
            }
        }

        private void CreateLine(ContentControl anchore)
        {
            var p =anchore.TranslatePoint(new Point(35, 35), mainContainer);
            currentLine = new Line()
            {
                X1 = p.X,
                Y1 = p.Y,
                X2 = p.X,
                Y2 = p.Y,
                Stroke = new SolidColorBrush(Colors.Red),
                StrokeThickness = 4,
                StrokeEndLineCap = PenLineCap.Round,
                StrokeStartLineCap = PenLineCap.Round
            };
            lineContainer.Children.Add(currentLine);
            Points.Add(anchore);
        }

        private void UniformGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            currentLine = null;
            Points.Clear();
            lineContainer.Children.Clear();
        }

        private void UniformGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentLine == null) return;
            if(e.Source is ContentControl anchore && !Points.Contains(anchore))
            {
                var p = anchore.TranslatePoint(new Point(35, 35), mainContainer);
                currentLine.X2 = p.X;
                currentLine.Y2 = p.Y;
                CreateLine(anchore);
            }
            else
            {
                var p = e.GetPosition(mainContainer);
                Console.WriteLine(p);
                currentLine.X2 = p.X;
                currentLine.Y2 = p.Y;
            }
        }
    }
}
