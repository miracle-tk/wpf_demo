using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace 装饰器
{
    public class CustomAdorner : Adorner
    {
        VisualCollection visCollec;
        private const double THUMB_SIZE = 12;
        private const double Min_SIZE = 30;
        Thumb tl, tr, bl, br,mov;
        public CustomAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visCollec = new VisualCollection(this);
            visCollec.Add(tl = GetResizeThumb(Cursors.SizeNWSE,HorizontalAlignment.Left,VerticalAlignment.Top));
            visCollec.Add(tr = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Right, VerticalAlignment.Top));
            visCollec.Add(bl = GetResizeThumb(Cursors.SizeNESW, HorizontalAlignment.Left, VerticalAlignment.Bottom));
            visCollec.Add(br = GetResizeThumb(Cursors.SizeNWSE, HorizontalAlignment.Right, VerticalAlignment.Bottom));
            visCollec.Add(mov = GetMoveThumb());
            //   visCollec.Add(tr=GetResizeThumb)
        }

        private Thumb GetMoveThumb()
        {
            var t = new Thumb();
            t.Background = Brushes.Blue;
            t.Cursor = Cursors.ScrollAll;
            t.DragDelta += (s, e) =>
            {
                var element = AdornedElement as FrameworkElement;

                if (element == null)

                    return;



                Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);

                Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
            };
            return t;
        }

        protected override System.Windows.Size ArrangeOverride(System.Windows.Size finalSize)
        {
            double offset = THUMB_SIZE / 2;

            Size sz = new Size(THUMB_SIZE, THUMB_SIZE);

            tl.Arrange(new Rect(new Point(-offset,-offset), sz));
            tr.Arrange(new Rect(new Point(AdornedElement.DesiredSize.Width-offset, -offset), sz));
            bl.Arrange(new Rect(new Point(-offset, AdornedElement.DesiredSize.Height- offset), sz));
            br.Arrange(new Rect(new Point(AdornedElement.DesiredSize.Width - offset, AdornedElement.DesiredSize.Height - offset), sz));
            mov.Arrange(new Rect(new Point(AdornedElement.DesiredSize.Width / 2 - offset, AdornedElement.DesiredSize.Height / 2 - offset), sz));
            return base.ArrangeOverride(finalSize);
        }
        
        private Thumb GetResizeThumb(Cursor cursor,HorizontalAlignment hor,VerticalAlignment ver)
        {
            var t = new Thumb();
            t.Cursor = cursor;
          
            t.Width = THUMB_SIZE;
            t.Height = THUMB_SIZE;
            t.HorizontalAlignment = hor;
            t.VerticalAlignment = ver;
            t.Background =new SolidColorBrush(Colors.Red);
            t.DragDelta += (s, e) =>
            {
                var element = AdornedElement as FrameworkElement;
                switch (t.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        if (element.Width - e.HorizontalChange > Min_SIZE)
                        {
                            element.Width -= e.HorizontalChange;
                            Canvas.SetLeft(element, Canvas.GetLeft(element) + e.HorizontalChange);
                        }
                        break;
                    case HorizontalAlignment.Right:
                        if (element.Width + e.HorizontalChange > Min_SIZE)
                        {
                            element.Width += e.HorizontalChange;
                            
                        }
                        break;
                }
                switch (t.VerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        if (element.Height - e.VerticalChange > Min_SIZE)
                        {
                            element.Height -= e.VerticalChange;
                            Canvas.SetTop(element, Canvas.GetTop(element) + e.VerticalChange);
                        }
                        break;
                    case VerticalAlignment.Bottom:
                        if (element.Height + e.VerticalChange > Min_SIZE)
                        {
                            element.Height += e.VerticalChange;

                        }
                        break;
                }
            };
            return t;
        }

        protected override Visual GetVisualChild(int index)

        {

            return visCollec[index];

        }

        protected override int VisualChildrenCount

        {

            get

            {

                return visCollec.Count;

            }

        }
    }
}
