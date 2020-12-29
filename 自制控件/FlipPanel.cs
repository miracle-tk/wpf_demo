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
    [TemplateVisualState(Name ="Normal",GroupName ="ViewStates")]
    [TemplateVisualState(Name ="Flipped",GroupName ="ViewStates")]
    [TemplatePart(Name ="FlipButton",Type =typeof(ToggleButton))]
    [TemplatePart(Name ="FlipButtonAlternate",Type =typeof(ToggleButton))]
    public class FlipPanel : Control
    {
        static FlipPanel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FlipPanel), new FrameworkPropertyMetadata(typeof(FlipPanel)));
            
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ToggleButton flipbutton = base.GetTemplateChild("FlipButton") as ToggleButton;
            if (flipbutton !=null)
            {
                flipbutton.Click += flipbutton_click;
            }
            ToggleButton flipButtonAlternate = base.GetTemplateChild("FlipButtonAlternate") as ToggleButton;
            if (flipButtonAlternate != null)
            {
                flipButtonAlternate.Click += flipButtonAlternate_click;
            }
            this.ChangeVisualState(false);
        }

        private void flipButtonAlternate_click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void flipbutton_click(object sender, RoutedEventArgs e)
        {
            this.IsFlipped = !IsFlipped;
            ChangeVisualState(true);
        }

        public FlipPanel()
        {
            
        }

        public object FrontContent
        {
            get { return (object)GetValue(FrontContentProperty); }
            set { SetValue(FrontContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FrontContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FrontContentProperty =
            DependencyProperty.Register("FrontContent", typeof(object), typeof(FlipPanel),null);


        public object BackContent
        {
            get { return (object)GetValue(BackContentProperty); }
            set { SetValue(BackContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BackContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackContentProperty =
            DependencyProperty.Register("BackContent", typeof(object), typeof(FlipPanel), null);



        public bool IsFlipped
        {
            get { return (bool)GetValue(IsFlippedProperty); }
            set {
                    SetValue(IsFlippedProperty, value);
                    ChangeVisualState(true);
                }
        }

        private void ChangeVisualState(bool useTransitions)
        {
            if (!IsFlipped)
            {
                VisualStateManager.GoToState(this, "Normal", useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, "Flipped", useTransitions);
            }
        }

        // Using a DependencyProperty as the backing store for IsFlipped.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsFlippedProperty =
            DependencyProperty.Register("IsFlipped", typeof(bool), typeof(FlipPanel),null);




        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(FlipPanel),null );
        

    }
}
