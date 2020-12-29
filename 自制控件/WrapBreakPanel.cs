using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace 自制控件
{
    public class WrapBreakPanel : Panel
    {

        public static bool GetLineBreakBefore(DependencyObject obj)
        {
            return (bool)obj.GetValue(LineBreakBeforeProperty);
        }
        
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            dc.DrawLine(new Pen(Brushes.Red, 3), new Point(0, 0), new Point(RenderSize.Width, RenderSize.Height));
        }
        public static void SetLineBreakBefore(DependencyObject obj, bool value)
        {
            obj.SetValue(LineBreakBeforeProperty, value);
        }

        // Using a DependencyProperty as the backing store for LineBreakBeforeProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineBreakBeforeProperty =
            DependencyProperty.RegisterAttached("LineBreakBefore", typeof(bool), typeof(WrapBreakPanel), ajkj());

        private static PropertyMetadata ajkj()
        {
            FrameworkPropertyMetadata metadata = new FrameworkPropertyMetadata();
            metadata.AffectsArrange = true;
            metadata.AffectsMeasure = true;
            
            return metadata;
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            Size currentLineSize = new Size();
            Size panelSize = new Size();
            foreach (UIElement element in base.InternalChildren)
            {
                element.Measure(availableSize);
                Size desiredSize = element.DesiredSize;
                if (GetLineBreakBefore(element) || currentLineSize.Width + desiredSize.Width > availableSize.Width)
                {
                    panelSize.Height += currentLineSize.Height;
                    panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
                    currentLineSize = desiredSize;
                    if (desiredSize.Width > availableSize.Width)
                    {
                        panelSize.Height += desiredSize.Height;
                        panelSize.Width = Math.Max(desiredSize.Width, panelSize.Width);//
                        currentLineSize = new Size();
                    }
                }
                else
                {
                    currentLineSize.Width += desiredSize.Width;
                    currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
                }
            }
            panelSize.Width = Math.Max(currentLineSize.Width, panelSize.Width);
            panelSize.Height += currentLineSize.Height;
            //return panelSize;
            return new Size();
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            int startIndex = 0;
            Size currentLineSize = new Size();
            var elements = base.InternalChildren;
            double lastLineYpos=0;
            for (int i = 0; i < elements.Count; i++)
            {
               
                Size desiredSize = elements[i].DesiredSize;
                if(GetLineBreakBefore(elements[i]) || desiredSize.Width + currentLineSize.Width > finalSize.Width)
                {
                    DoArrange(startIndex, i, currentLineSize.Height, lastLineYpos);
                    lastLineYpos += currentLineSize.Height;
                    currentLineSize = desiredSize;
                    if (desiredSize.Width > finalSize.Width)
                    {
                        DoArrange(i, ++i, desiredSize.Height, lastLineYpos);
                        lastLineYpos += desiredSize.Height;
                        currentLineSize = new Size();
                    }

                    startIndex = i;
                }
                else
                {
                    currentLineSize.Width += desiredSize.Width;
                    currentLineSize.Height = Math.Max(desiredSize.Height, currentLineSize.Height);
                }

            }
            if (startIndex < elements.Count)
            {
                DoArrange(startIndex, elements.Count, currentLineSize.Height, lastLineYpos);
            }



            return finalSize;
        }
        private void DoArrange(int start ,int end,double lineHeight,double y)
        {
            double x=0;
            var children = base.InternalChildren;
            for (int i = start; i < end; i++)
            {
                children[i].Arrange(new Rect(x, y, children[i].DesiredSize.Width, lineHeight));
                x += children[i].DesiredSize.Width;
            }
        }

    }
}
