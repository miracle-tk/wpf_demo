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

namespace ListControl
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string mystring { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //lstbinding.DataContext = this;
            Models = GetModes();
            mystring = "hello world!";
            //lstbinding.ItemsSource = Models;
        }

        private void Lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lst.SelectedItem == null) return;
            tb.Text = string.Format("you chose at position{0}.\n checked state is {1}", lst.SelectedIndex, ((CheckBox)lst.SelectedItem).IsChecked);
            lst.ContainerFromElement((DependencyObject)lst.SelectedItems[0]);
        }

        public class Model
        {
            public int Number { get; set; }
        }
        public List<Model> Models { get; set; }
        public List<Model> GetModes()
        {
            List<Model> listmode = new List<Model>();
            for (int i = 0; i < 99; i++)
            {
                listmode.Add(new Model
                {
                    Number = i
                });
            }
            return listmode;
        }
    }
}
