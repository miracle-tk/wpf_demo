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
using System.Collections.ObjectModel;
using System.Windows;

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
        private static ObservableCollection<LineInformation> datalist=new ObservableCollection<LineInformation>();

        public static ObservableCollection<LineInformation> DataList
        {
            get { return datalist; }
            set {datalist=value;
                
            }
        }

        public  Window2VIewModel()
        {
            LoadedCommand = new DelegateCommand<object>(Load);
            UnloadedCommand = new DelegateCommand(Unloaded);
            for (int i = 0; i < 5; i++)
            {
                var line = new LineInformation { LineName = $"{ i}" };
                line.EQPList.Add(new EQPInformation { EQPName = "EGIS" });
                line.EQPList.Add(new EQPInformation { EQPName = "EGIS1" });
                DataList.Add(line);
            }
           
           Task.Run(async () => {
                var list = new List<LineInformation>();
                while (true)
                {
                   // Thread.Sleep(1000);
                   await Task.Delay(1000);
                     list = DataList.ToList();
                    LineInformation line = new LineInformation { LineName = $"0" };
                    line.EQPList.Add(new EQPInformation { EQPName = "EGIS" });
                    line.EQPList.Add(new EQPInformation { EQPName = "EGIS1" });
                   Application.Current.Dispatcher.Invoke(new Action(() =>
                   {
                       DataList.Add(line);
                 //  DataList= new ObservableCollection<LineInformation>(list);
  
                    }));
                }


            });
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
    public class EQPInformation: BindableBase
    {
        private string eqpname;

        public string EQPName
        {
            get { return eqpname; }
            set {
               
                SetProperty(ref eqpname, value);
                
            }
        }


    }
    public class LineInformation : BindableBase
    {
        private string linename;

        public string LineName
        {
            get { return linename; }
            set { 
                SetProperty(ref linename, value);
            }
        }
      
        private ObservableCollection<EQPInformation> eqplist=new ObservableCollection<EQPInformation>();

        public ObservableCollection<EQPInformation> EQPList
        {
            get { return eqplist; }
            set {  SetProperty(ref eqplist, value); }
        }


    }

}
