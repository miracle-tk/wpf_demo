using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace AllTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Type> Controls = new ObservableCollection<Type>();
        public MainWindow()
        {
            Loaded += MainWindow_Loaded;
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          var assebmly=  Assembly.GetAssembly(typeof(Control));

            foreach (var type in assebmly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(Control)) && !type.IsAbstract && type.IsPublic)
                {
                   // type.GetConstructor(System.Type.EmptyTypes)
                    Controls.Add(type);

                }
            }
            controlList.ItemsSource = Controls;
        }

        private void ControlList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var l = e.AddedItems;
            var list = l.Cast<Type>();
            var type = list.FirstOrDefault();
            var constructor = type.GetConstructor(System.Type.EmptyTypes);
            if (constructor == null)
            {
                return;
            }
            var f = constructor.Invoke(null);

            var control = f as Control;
            control.Visibility = Visibility.Collapsed;
            main.Children.Add(control);
            var ct = control.Template;
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(sb, settings);
             XamlWriter.Save(ct, writer);
            tb.Text = sb.ToString();
            main.Children.Remove(control);
        }
    }
}
