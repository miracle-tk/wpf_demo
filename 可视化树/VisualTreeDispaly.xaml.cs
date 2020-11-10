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
using System.Windows.Shapes;

namespace 可视化树
{
    /// <summary>
    /// VisualTreeDispaly.xaml 的交互逻辑
    /// </summary>
    public partial class VisualTreeDispaly : Window
    {
        public VisualTreeDispaly()
        {
            InitializeComponent();
        }

        public void ShowViusalTree(DependencyObject element)
        {
            treeElements.Items.Clear();
            ProcessElement(element, null);
        }

        private void ProcessElement(DependencyObject element, TreeViewItem p)
        {
            var item = new TreeViewItem();
            item.Header = element.GetType().Name;
            item.IsExpanded = false;

            //if (p == null)
            //{
            //    treeElements.Items.Add(item);
            //}
            //else
            //{
            //    p.Items.Add(item);
            //}           
           _=  p == null ? treeElements.Items.Add(item) : p.Items.Add(item);

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                ProcessElement(VisualTreeHelper.GetChild(element, i), item);
            }
        }
    }
}
