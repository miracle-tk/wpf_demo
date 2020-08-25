using System.Windows;
using System.Windows.Data;

namespace _8_Binding
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BindingOperations.ClearBinding(tb, FontSizeProperty);
        }
        /// <summary>
        /// 不能更改当前Binding对象的Mode，只能新建Binding对象
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Binding bind = BindingOperations.GetBinding(tb, FontSizeProperty);
            Binding newBind = new Binding();
            newBind = bind;
            BindingOperations.ClearBinding(tb, FontSizeProperty);
            newBind.Mode = BindingMode.TwoWay;
            tb.SetBinding(FontSizeProperty, newBind);
            // bind.Mode = BindingMode.TwoWay;

        }
    }
}
