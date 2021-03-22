using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MouseDrag
{
    public class FormUtility
    {
        public static Cursor CreateCursor(UIElement element)
        {
           // element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
           // element.Arrange(new Rect(new Point(), element.DesiredSize));
            Rectangle r = new Rectangle();
           

            var rtb =
              new RenderTargetBitmap(
                (int)element.DesiredSize.Width,
                (int)element.DesiredSize.Height,
                96, 96, PixelFormats.Pbgra32);

            rtb.Render(element);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var ms = new MemoryStream())
            {
                encoder.Save(ms);
                using (var bmp = new System.Drawing.Bitmap(ms))
                {
                    return InternalCreateCursor(bmp);
                }
            }
        }


        private static Cursor InternalCreateCursor(System.Drawing.Bitmap bmp)
        {
            var iconInfo = new NativeMethods.IconInfo();
            NativeMethods.GetIconInfo(bmp.GetHicon(), ref iconInfo);

            iconInfo.xHotspot = 125;
            iconInfo.yHotspot = 65;
            iconInfo.fIcon = false;

            SafeIconHandle cursorHandle = NativeMethods.CreateIconIndirect(ref iconInfo);
            return CursorInteropHelper.Create(cursorHandle);
        }
    }

    [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
    internal class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeIconHandle()
            : base(true)
        {
        }

        override protected bool ReleaseHandle()
        {
            return NativeMethods.DestroyIcon(handle);
        }
    }
    internal static class NativeMethods
    {
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.dll")]
        public static extern SafeIconHandle CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);
    }
}
