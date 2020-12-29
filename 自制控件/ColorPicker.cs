using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 自制控件
{
  
    public class ColorPicker : Control
    {
        static ColorPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorPicker), new FrameworkPropertyMetadata(typeof(ColorPicker)));
            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var slider = GetTemplateChild("PART_RedSlider") as RangeBase;
            Binding bind = new Binding("Red");

            slider.SetBinding(RangeBase.ValueProperty, bind);
            
        }


        #region 依赖属性
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Red.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RedProperty =
            DependencyProperty.Register("Red", typeof(byte), typeof(ColorPicker), new PropertyMetadata(default(byte), OnRGBChanged));




        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Green.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GreenProperty =
            DependencyProperty.Register("Green", typeof(byte), typeof(ColorPicker), new PropertyMetadata(default(byte), OnRGBChanged));


        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Blue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlueProperty =
            DependencyProperty.Register("Blue", typeof(byte), typeof(ColorPicker), new PropertyMetadata(default(byte), OnRGBChanged));




        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }

        }

        // Using a DependencyProperty as the backing store for Color.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
            var cp = d as ColorPicker;
            // cp.Color = (Color)e.NewValue;
            cp.Red = ((Color)e.NewValue).R;
            cp.Green = ((Color)e.NewValue).G;
            cp.Blue = ((Color)e.NewValue).B;
            var args = new RoutedPropertyChangedEventArgs<Color>((Color)e.OldValue, (Color)e.NewValue);
            args.RoutedEvent = ColorPicker.ColorChangedEvent;
            cp.RaiseEvent(args);
        }
        private static void OnRGBChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var cp = d as ColorPicker;
            var color = cp.Color;
            if (e.Property == RedProperty)
            {
                color.R = (byte)e.NewValue;
            }
            if (e.Property == GreenProperty)
            {
                color.G = (byte)e.NewValue;
            }
            if (e.Property == BlueProperty)
            {
                color.B = (byte)e.NewValue;
            }
            cp.Color = color;
        }
        #endregion
        #region 路由事件
        public static readonly RoutedEvent ColorChangedEvent = EventManager.
            RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }
        #endregion
        #region Command

        #endregion
    }
}
