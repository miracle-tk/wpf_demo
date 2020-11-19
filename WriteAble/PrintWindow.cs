using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WriteAble
{
    public class PrintWindow
    {
        public static Bitmap PrtWindow(IntPtr hWnd)

        {
            try
            {
                IntPtr hscrdc = Win32API.GetWindowDC(hWnd);

                RECT rect;

                Win32API.GetClientRect(hWnd, out rect);

                IntPtr hbitmap = Win32API.CreateCompatibleBitmap(hscrdc, (int)(rect.Right - rect.Left), (int)(rect.Bottom - rect.Top));

                IntPtr hmemdc = Win32API.CreateCompatibleDC(hscrdc);

                Win32API.SelectObject(hmemdc, hbitmap);

                Win32API.PrintWindow(hWnd, hmemdc, 0);

                Bitmap bmp = Bitmap.FromHbitmap(hbitmap);

                Win32API.DeleteDC(hscrdc);

                Win32API.DeleteDC(hmemdc);
                Win32API.DeleteObject(hbitmap);
                return bmp;

            }
            catch
            {
                return null;
            }


        }
    }
}
