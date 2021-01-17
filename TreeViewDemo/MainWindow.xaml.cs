using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TreeViewDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Student> subitems;
        public MainWindow()
        {
            InitializeComponent();
            List<Student> items = new List<Student>();
            subitems = new ObservableCollection<Student>() {
            new Student(){Name="1"},
            new Student(){Name="2"},
            new Student(){Name="3"} };
            items.Add(new Student() { Name = "A" });
            items.Add(new Student() { Name = "A" });
            items.Add(new Student()
            {
                Name = "B",
                Students = subitems
            });
            items.Add(new Student() { Name = "A" });
            DataContext = items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            subitems.Add(new Student() { Name = DateTime.Now.ToString(), IsSelected = true });
        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
         //   var element = sender as FrameworkElement;
          //  element.BringIntoView();
        }

        private void TreeView_Selected(object sender, RoutedEventArgs e)
        {

        }
    }

    public class Student
    {
        public string Name { get; set; }

        public bool IsSelected { get; set; }

        public ObservableCollection<Student> Students { get; set; }
    }
}
