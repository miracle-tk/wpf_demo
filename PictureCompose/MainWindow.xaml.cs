using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PictureCompose
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private SolidColorBrush waterBrush=Brushes.Red;
        private string loadpath=string.Empty;
        public MainWindow()
        {
            InitializeComponent();
           
        }
    

        private void Image1Open_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            OpenFileDialog fd = new OpenFileDialog();

            fd.Filter = "图像文件(*.bmp, *.jpg,*.png,*.jpeg)|*.bmp;*.jpg;*.png;*.jpeg";
            fd.InitialDirectory =loadpath==string.Empty? Environment.GetFolderPath(Environment.SpecialFolder.Desktop):loadpath;
           
            Nullable<bool> result = fd.ShowDialog();
            

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
 
                loadpath = Directory.GetParent(fd.FileName).FullName;
            
                string filename = fd.FileName;
                BitmapImage bitmapImage = new BitmapImage();


                bitmapImage.BeginInit();  //给BitmapImage对象赋予数据的时候，需要用BeginInit()开始，用EndInit()结束
                bitmapImage.UriSource = new Uri(filename);
                bitmapImage.DecodePixelWidth = 320;   //对大图片，可以节省内存。尽可能不要同时设置DecodePixelWidth和DecodePixelHeight，否则宽高比可能改变
                bitmapImage.EndInit();
                switch (btn.Name)
                {
                    case "Image1Open":
                        image1.Source = bitmapImage;
                        break;
                    case "Image2Open":
                        image2.Source = bitmapImage;
                        break;
                }
               
            }
            

        }

        private void Btn_compose_Click(object sender, RoutedEventArgs e)
        {

            Refresh();
           
        }

        private void Refresh()
        {
            BitmapSource btm1 = image1.Source as BitmapImage;
            BitmapSource btm2 = image2.Source as BitmapImage;

            //FormattedText signatureTxt = new FormattedText(watermark.Text, System.Globalization.CultureInfo.CurrentCulture,
            //    FlowDirection.LeftToRight, new Typeface(SystemFonts.MessageFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
            //    50, waterBrush, VisualTreeHelper.GetDpi(this).PixelsPerDip);
            var width =Convert.ToInt32( Math.Max(btm1.Width, btm2.Width));
            var height =Convert.ToInt32( btm1.Height + btm2.Height);
            var dpix = Math.Min(btm1.DpiX, btm2.DpiX);
            var dpiy = Math.Min(btm1.DpiY, btm2.DpiY);
            RenderTargetBitmap composeImage = new RenderTargetBitmap(width, height,
                                                                        96,96, PixelFormats.Default);
            FormattedText signatureTxt= MakeText(width);
            signatureTxt.MaxTextWidth = width-50;
            DrawingVisual dv = new DrawingVisual();
            using (var context = dv.RenderOpen())
            {
                
                context.DrawImage(btm1, new Rect(0, 0, btm1.Width, btm1.Height));
                context.DrawImage(btm2, new Rect(0, btm1.Height + 1, btm2.Width, btm2.Height));
                context.DrawText(signatureTxt, new Point(50, composeImage.Height * Convert.ToDouble(waterHeightPercent.Text)/100));
            }
            composeImage.Render(dv);
            destImage.Source = composeImage;
        }

        private FormattedText MakeText(int wide)
        {
           FormattedText signatureTxt = new FormattedText(watermark.Text, System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight, new Typeface(SystemFonts.MessageFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
                50, waterBrush, VisualTreeHelper.GetDpi(this).PixelsPerDip);
           // InsertEnter(signatureTxt, wide);
            return signatureTxt;
        }

        private void InsertEnter(FormattedText signatureTxt,int wide)
        {
            if (signatureTxt.Width <= wide)
            {
                return;
            }
            signatureTxt = new FormattedText(signatureTxt.Text.Insert(signatureTxt.Text.Length,"/n"), System.Globalization.CultureInfo.CurrentCulture,
               FlowDirection.LeftToRight, new Typeface(SystemFonts.MessageFontFamily, FontStyles.Normal, FontWeights.Normal, FontStretches.Normal),
               50, waterBrush, VisualTreeHelper.GetDpi(this).PixelsPerDip);

        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "图像文件(*.jpg,*.bmp, *.png,*.jpeg)|*.jpg;*.bmp;*.png;*.jpeg";
            sfd.InitialDirectory =loadpath==string.Empty? System.Environment.CurrentDirectory:loadpath;
            sfd.FileName = watermark.Text;
            Nullable<bool> result = sfd.ShowDialog();
            if (result == true)
            {
                string filepath = sfd.FileName;
                BitmapSource BS = (BitmapSource)destImage.Source;
                JpegBitmapEncoder PBE = new JpegBitmapEncoder();
                PBE.Frames.Add(BitmapFrame.Create(BS));
                using (Stream stream = File.Create(filepath))
                {
                    PBE.Save(stream);
                }
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Refresh();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            switch (rb.Name)
            {
                case "Red":
                    waterBrush = Brushes.Red;
                    break;
                case "Yellow":
                    waterBrush = Brushes.Yellow;
                    break;
                case "DarkGray":
                    waterBrush = Brushes.DarkGray;
                    break;
                case "White":
                    waterBrush = Brushes.White;
                    break;
                default:
                    waterBrush = Brushes.Red;
                    break;
            }
            Refresh();
        }

        private void Image1_Drop(object sender, DragEventArgs e)
        {
           
        }

        private void Image1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effects = DragDropEffects.Link;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
        }

 
    }
}
