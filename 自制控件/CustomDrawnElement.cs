using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace 自制控件
{
    public class CustomDrawnElement:FrameworkElement
    {
        public Color BackgroundColor
        {
            get { return (Color)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackgroundColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(Color), typeof(CustomDrawnElement), getMetadata());

        private static FrameworkPropertyMetadata getMetadata()
        {
            FrameworkPropertyMetadata fm = new FrameworkPropertyMetadata(Colors.Red);
            fm.AffectsRender = true;
            return fm;
            
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            InvalidateVisual();
        }
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            InvalidateVisual();
        }
        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            drawingContext.DrawRectangle(GetBrush(), null, new Rect(0, 0,this.ActualWidth, this.ActualHeight));

        }

        private Brush GetBrush()
        {
            if (!IsMouseOver)
            {
                return new SolidColorBrush(BackgroundColor);

            }
            else
            {
                RadialGradientBrush brush = new RadialGradientBrush(Colors.Transparent, BackgroundColor);
                Point p = Mouse.GetPosition(this);
                Point relativeP = new Point(p.X / this.ActualWidth, p.Y / this.ActualHeight);
                brush.RadiusX = 0.1;
                brush.RadiusY = 0.1;
                brush.Center = relativeP;
                brush.GradientOrigin = relativeP;

                return brush;
            }

        }
    }
}
