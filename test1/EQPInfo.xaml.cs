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

namespace test1
{
    /// <summary>
    /// EQPInfo.xaml 的交互逻辑
    /// </summary>
    public partial class EQPInfo : UserControl
    {
        public EQPInfo()
        {
            InitializeComponent();
           // DataContext = this;
            //SetCurrentValue(EQPNameValueProperty, "Touch01");
        }


        public string EQPNameValue
        {
            get { return (string)GetValue(EQPNameValueProperty); }
            set { SetValue(EQPNameValueProperty, value); }
        }


        public Brush StateBrush
        {
            get { return (Brush)GetValue(StateBrushProperty); }
            set { SetValue(StateBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StateBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateBrushProperty =
            DependencyProperty.Register("StateBrush", typeof(Brush), typeof(EQPInfo), new PropertyMetadata(Brushes.Blue));


        // Using a DependencyProperty as the backing store for EQPNameValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EQPNameValueProperty =
            DependencyProperty.Register("EQPNameValue", typeof(string), typeof(EQPInfo), new PropertyMetadata(default(string), NameChanged));

        private static void NameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EQPInfo).EQPName.Text = e.NewValue.ToString();
        }

        private void InfoToalarm_Click(object sender, RoutedEventArgs e)
        {
            if(infoToalarm.Content.ToString()== "\xe876")
            {
                infoToalarm.Content = "\xe606";
            }
            else
            {
                infoToalarm.Content = "\xe876";
            }
        }
    }
}
