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

namespace MouseDrag
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            label1.Cursor = Cursors.AppStarting;
        }

        private void Label1_Drop(object sender, DragEventArgs e)
        {
            //label1.Content =  e.Data.GetData(DataFormats.StringFormat);
            ((Label)sender).Content = e.Data.GetData(DataFormats.Text);
            //label1.Content = e.Data.GetData(DataFormats.StringFormat);
        }

        private void Label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataObject data = new DataObject(typeof(Label), this.label1);
            DragDrop.DoDragDrop((sender as Label), (sender as Label).Content, DragDropEffects.Link);
        }

        private void Label1_DragOver(object sender, DragEventArgs e)
        {
            
        }

        private void Tb1_DragLeave(object sender, DragEventArgs e)
        {

        }

        private void Label1_DragLeave(object sender, DragEventArgs e)
        {

        }
    }
}
