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

namespace 依赖属性
{
    /// <summary>
    /// MyTextbox.xaml 的交互逻辑
    /// </summary>
    public partial class MyTextbox : TextBox
    {
        public MyTextbox()
        {
            InitializeComponent();
            
        }


        public string MyTextProperty
        {
            get { return (string)GetValue(MyTextPropertyProperty); }
            set { SetValue(MyTextPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyTextProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyTextPropertyProperty =
            DependencyProperty.Register("MyTextProperty", typeof(string), typeof(MyTextbox), new PropertyMetadata("",callback));
       
        private static void callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var tb = d as TextBox;
            tb.Text = e.NewValue.ToString();
        }
    }
}
