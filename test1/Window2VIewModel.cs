using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using Prism.Commands;

namespace test1
{
    public class Window2VIewModel:BindableBase
    {
        private CancellationTokenSource _cancelSource = new CancellationTokenSource();
        private BitmapImage imagePath=new BitmapImage();
        public DelegateCommand UnloadedCommand { set; get; }
        public DelegateCommand<object> LoadedCommand { set; get; }

        public BitmapImage ImagePath
        {
            get { return imagePath; }
            set {SetProperty(ref imagePath , value); }
        }
        public Window2VIewModel()
        {
            LoadedCommand = new DelegateCommand<object>(Load);
            UnloadedCommand = new DelegateCommand(Unloaded);
          
        }
        private void Unloaded()
        {
            try
            {

                // mainProcess.Kill();

            }
            catch
            {

            }
            try
            {

                _cancelSource.Cancel();
              //  mypic.Source = null;
               // GOTImage = null;
              //  mypic.UpdateLayout();
            }
            catch
            {

            }
        }

        private async void Load(object u)
        {
         await   Task.Run(() =>
            {
                while (true)
                {
                    if (_cancelSource.IsCancellationRequested)
                    {
                        return;
                    }
                    var b = new Bitmap("637417422122195843.bmp");




                    ImagePath = ToBitmapImage(b);


                    Thread.Sleep(200);
                }
              
            });
           
        }
        private BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            //using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            //{
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;

            using (Stream ms = new MemoryStream())
            {

                bitmap.Save(ms, bitmap.RawFormat);
                bitmap.Dispose();
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
    }
}
