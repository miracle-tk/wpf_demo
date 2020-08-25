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

namespace App_resource
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ResourceDictionary dr = new ResourceDictionary();
            dr.Source = new Uri(@"Style/en-US.xaml",UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(dr);
        
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            
            ChangeLanguage(((TextBlock)(cb.SelectedItem)).Text);
        }

        private void ChangeLanguage(string lang)
        {
            string filename = "zh-CN.xaml";
            switch (lang)
            {
                case "中文":
                    filename = "zh-CN.xaml";
                    break;
                case "English":
                    filename = "en-US.xaml";
                    break;

            }
            Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary dr = new ResourceDictionary();
            dr.Source = new Uri(@"Style/"+filename, UriKind.Relative);
            Application.Current.Resources.MergedDictionaries.Add(dr);
            ResourceDictionary dr1 = new ResourceDictionary();
            dr1.Source = new Uri(@"Style/en-US.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(dr1);
        }
    }
}
