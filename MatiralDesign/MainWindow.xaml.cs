using Microsoft.Win32;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using Point = OpenCvSharp.Point;
using Rect = OpenCvSharp.Rect;

namespace MatiralDesign
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        unsafe private void Bbb_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.RestoreDirectory = true;
            string imgName = "";
            if (openFileDialog.ShowDialog()==true)
            {
                imgName = openFileDialog.FileName;
            }
            Console.WriteLine("文件名为" + imgName);
            Mat srcMat = new Mat(imgName, ImreadModes.AnyColor);
            srcImage.Source = MatToBitmapImage(srcMat);
            // srcImage.Height = srcImage.Source.Height;
            /// srcImage.Width = srcImage.Source.Width;
            #region example1
            //读取并进行边缘检测
            //Mat srcImg = new Mat(imgName, ImreadModes.Color);
            //Cv2.ImShow("input", srcImg);

            //Mat dstImg = new Mat();
            //Cv2.Canny(srcImg, dstImg, 50, 200);
            //Cv2.ImShow("canny", dstImg);
            //Cv2.Flip(srcImg, dstImg, FlipMode.XY);
            //Cv2.ImShow("flip", dstImg);
            //Cv2.PutText(srcImg,                                      //目标图像
            //            "lenna",                        //字符串
            //            new Point(40, 80),                //位置，注意这是字符串左下角的位置
            //            HersheyFonts.HersheyComplex,    //字体类型
            //            2,                              //字体大小   
            //            Scalar.White,7);
            //Cv2.ImShow("write", srcImg);//颜色
            #endregion

            #region example2
            //Mat src = new Mat(imgName, ImreadModes.AnyColor);
            //Rect roi = new Rect(169, 22, 150, 146);
            //Mat ImgROI = new Mat(src, roi);
            //Cv2.ImShow("gungun", src);
            //Cv2.ImShow("ROI", ImgROI);
            //Rect rect = new Rect(0, 0, ImgROI.Width, ImgROI.Height);
            //Mat pos = new Mat(src, rect);
            ////Cv2.ImShow("pos", pos);
            //ImgROI.CopyTo(pos);
            //Cv2.ImShow("gungun", src);
            #endregion

            #region example3
            //Mat panda = new Mat(imgName, ImreadModes.AnyColor);
            //Cv2.ImShow("gray", panda);
            //来个复杂点的， 
            //Random random = new Random();
            //for (int i = 0; i < 30000; i++)//30000个点
            //{
            //    int row = random.Next(srcMat.Rows);//随机生成行
            //    int col = random.Next(srcMat.Cols);//随机生成列
            //    Vec3b white = new Vec3b(255, 255, 255);
            //    //char white = (char)255;
            //    srcMat.Set(row, col, white);

            //}
            //Cv2.ImShow("滚滚", srcMat);
            // //Cv2.ImWrite("1.jpg", srcMat);
            // double h = srcImage.Height;
            //var c= Cv2.HoughCircles(srcMat, HoughMethods.Gradient, 2, h / 4, 25, 100);
            // foreach (var item in c)
            // {
            //     Cv2.Circle(srcMat, new Point(item.Center.X,item.Center.Y), (int)(item.Radius), Scalar.Red, 1);
            //     srcMat.Set((int)item.Center.Y, (int)item.Center.X, 255);
            // }
            //Cv2.ImShow("22", srcMat);

            //Console.WriteLine($"height:{srcMat.Height}   width:{srcMat.Width}");

            //for(int j = 0; j < srcMat.Height; j++)
            //{
            //    IntPtr c = srcMat.Ptr(j); //Mat对象的 Ptr方法：返回指向指定矩阵行的指针。
            //    byte* c1 = (byte*)c; //像素值在C#中是byte类型 ，在C++中是 uchar类型
            //    Console.WriteLine($"{j+1}行：");
            //    for (int i = 0; i < srcMat.Width; i++)
            //    {
            //        Console.WriteLine($"第{i + 1}个像素值: {*(c1 + i)}");
            //    }
            //}

            InputArray kneral = InputArray.Create<int>(new int[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } });
            Mat dst = new Mat();
            Cv2.Filter2D(srcMat, dst, -1, kneral, new Point(-1, 1),50);
            Cv2.ImShow("filter2d", dst);

            #endregion
            
        }

        public Bitmap MatToBitmap(Mat image)
        {
            return OpenCvSharp.Extensions.BitmapConverter.ToBitmap(image);
        }
        public BitmapImage MatToBitmapImage(Mat image)
        {
            Bitmap bitmap = MatToBitmap(image);
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png); // 坑点：格式选Bmp时，不带透明度

                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                // According to MSDN, "The default OnDemand cache option retains access to the stream until the image is needed."
                // Force the bitmap to load right now so we can dispose the stream.
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }


    }
}
