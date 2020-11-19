using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WriteAble
{
    public class MainWindowViewModel:BindableBase
    {
        private RECT RealRc;
        private BitmapImage _image;
        private Process process=new Process();
        private Process mainProcess = new Process();
        IntPtr handle1 = IntPtr.Zero;
        IntPtr handle2 = IntPtr.Zero;
        public BitmapImage Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }
        public DelegateCommand LoadedCommand;
        public DelegateCommand Unloaded;
        public MainWindowViewModel()
        {
           // LoadedCommand = new DelegateCommand(LoadExe);
           Image = new BitmapImage();
            Image.BeginInit();
            Image.UriSource = new Uri(@"1.bmp", UriKind.Relative);
            Image.EndInit();
                //  LoadExe();
        }
        private void LoadExe()
        {

            //string configpath = Environment.CurrentDirectory;
            string exePath = @"C:\Program Files (x86)\MELSOFT\SGT2000\SGT2000Main.exe";
            if (!System.IO.File.Exists(exePath))
            {
                return;
            }




            Task.Run(() =>
            {
                var intPtr = new IntPtr(0);
                var list = Process.GetProcesses();
                string processName = @"GT SoftGOT2000  [工程标题未设置]  :  No." + 1;
                if (list.Where((s) => s.MainWindowTitle == processName).FirstOrDefault() == null)
                {
                    process.StartInfo = new System.Diagnostics.ProcessStartInfo(exePath);
                    //process.StartInfo.RedirectStandardInput = true;
                    //process.StartInfo.RedirectStandardOutput = true;
                    //process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = true;
                    StringBuilder sb = new StringBuilder();
                    //sb.Append(iPAddress);
                    sb.Append(" -SGT" + 1);
                    //sb.Append("-popupwindow");
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
                    //process.StartInfo.WindowStyle= ProcessWindowStyle.Hidden;
                    //process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.Arguments = sb.ToString();

                    process.Start();

                    //process=Process.Start(startInfo);

                    //Thread.Sleep(3000);
                    //var t1 = DateTime.Now;

                    //  var stopwatch = new Stopwatch();
                    // stopwatch.Start();

                    // intPtr = FindWindow(null, processName);
                    // while (process.MainWindowHandle == IntPtr.Zero || process.MainWindowHandle == null)
                    //while (FindWindow(null, processName) ==IntPtr.Zero)

                    while (list.Where((s) => s.MainWindowTitle == processName).FirstOrDefault() == null)

                    {
                        list = Process.GetProcesses();
                        //if (stopwatch.ElapsedMilliseconds >= 20000)
                        //{
                        //    break;
                        //}

                        System.Threading.Thread.Sleep(100);

                    }
                    // stopwatch.Stop();
                }

                intPtr = Win32API.FindWindow(null, processName);
                mainProcess = list.Where((s) => s.MainWindowTitle == processName).FirstOrDefault();
                //while (process.WaitForInputIdle())
                //{


                handle1 = Win32API.FindWindowEx(mainProcess.MainWindowHandle, IntPtr.Zero, "AfxFrameOrView140u", null);
                while (handle1 == IntPtr.Zero)
                {
                    handle1 = Win32API.FindWindowEx(mainProcess.MainWindowHandle, IntPtr.Zero, "AfxFrameOrView140u", null);
                    Thread.Sleep(500);
                }
                handle2 = Win32API.FindWindowEx(handle1, IntPtr.Zero, "GotChild70Window", null);
                //while (handle2 == IntPtr.Zero)
                //{
                //    handle2 = Win32API.FindWindowEx(handle1, IntPtr.Zero, "GotChild70Window", null);
                //    Thread.Sleep(500);
                //}

                // handle3 = Win32API.FindWindowEx(handle1, IntPtr.Zero, "GotChild62Window", null);
                Win32API.GetClientRect(handle2, out this.RealRc);
                Win32API.GetClientRect(mainProcess.MainWindowHandle, out RECT rc);
               
                Bitmap b;
                while (true)
                {
                    b = PrintWindow.PrtWindow(mainProcess.MainWindowHandle);
                  //  b = PrintWindow.PrtWindow(handle1);
                    //b.Save($"{DateTime.Now.Ticks}.png");
                    Image = BitmapToBitmapImage(b);
                    //  _bitmap = new WriteableBitmap(BitmapToBitmapImage(b) as BitmapSource);
                    //Int32Rect rect = new Int32Rect(0, 0, (int)mypic.Width, (int)mypic.Height);
                    //byte[] pixels = new byte[(int)mypic.Width * (int)mypic.Height * _bitmap.Format.BitsPerPixel / 8];



                    //  int stride = (_bitmap.PixelWidth * _bitmap.Format.BitsPerPixel) / 8;

                    //   _bitmap.WritePixels(rect, pixels, stride, 0);
                    // b = PrintWindow.PrtWindow(mainProcess.MainWindowHandle);
                    // if((Win32API.GetWindowLong(handle3,Win32API.GWL_STYLE)& Win32API.WS_VISIBLE) > 0)
                    //{
                    //    Win32API.GetWindowRect(handle3, out RECT rc);
                    //    var c = PrintWindow.PrtWindow(handle3);
                    //    Graphics g = Graphics.FromImage(b);
                    //    g.DrawImage(c, new System.Drawing.Rectangle((b.Width / 2 - c.Width / 2), (b.Height / 2 - c.Height / 2), c.Width, c.Height));

                    //}
                    //b.Save($"{DateTime.Now.Ticks}.png");
                    //mypic.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    //{

                    //    mypic.Source = BitmapToBitmapImage(b);
                    //}));


                    Thread.Sleep(17);
                    GC.Collect();
                }

                // b.Save(@"a.png");
                //   mypic.BeginInvoke(new Action(() =>
                //         {

                //    long oldStyle = NativeMethods.GetWindowLong(handle2, NativeMethods.GWL_STYLE);

                //    SetWindowLong(handle2);
                //    while (Win32API.SetParent(handle2, mypic.Handle) == null)
                //    {
                //        System.Windows.Forms.MessageBox.Show("甚至父窗口失败");
                //    }

                //    while (!Win32API.MoveWindow(handle2, 0, 0, mypic.Width, mypic.Height, true))
                //    {
                //        //System.Windows.Forms.MessageBox.Show("MoveWindow失败");
                //    }


                // }));

                //if (_panel != null)
                //{
                //    FocusManager.SetIsFocusScope(_panel, true);
                //}
                //process.Start();


                System.Diagnostics.Process process2 = System.Diagnostics.Process.GetCurrentProcess();
                int a = 1;



                //getProcess();

            });



        }
        private BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }
    }
}
