using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MouseDrag
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private FrameworkElement _dragScope;
        private DragAdorner _adorner;
        private AdornerLayer _layer;
        private Storyboard _sb = new Storyboard() { FillBehavior = FillBehavior.Stop,RepeatBehavior=RepeatBehavior.Forever };
        private Rectangle slecetedRectangle;
        public MainWindow()
        {
            InitializeComponent();
            _sb.AutoReverse = true;
            slecetedRectangle = sourcePanel.Children[5] as Rectangle;
           // this.Background = null;
        }
       
       
        public Cursor HoloCursor { get; private set; }
        public bool IsMoving { get; private set; } = false;

        private void SourcePanel_MouseMove(object sender, MouseEventArgs e)
        {

            if (!(e.Source is Rectangle)) return;
            var rectangle = e.Source as Rectangle;
            slecetedRectangle = rectangle;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
               // sourcePanel.CaptureMouse();
                IsMoving = true;
                HoloCursor = FormUtility.CreateCursor(rectangle);
                // viewElemen.RockStart();
                //rectangle.GiveFeedback += DragSource_GiveFeedback;
                // DragDrop.DoDragDrop(viewElemen, Model, DragDropEffects.Copy);
                //    viewElemen.MouseMove -= ViewElemenOnPreviewMouseMove;
                //  viewElemen.GiveFeedback -= DragSource_GiveFeedback;
               Mouse.SetCursor(Cursors.Arrow);
                this._dragScope = Application.Current.MainWindow.Content as FrameworkElement;
                this._dragScope.AllowDrop = true;
                DragEventHandler draghandler = new DragEventHandler(DragScope_PreviewDragOver);
                this._dragScope.PreviewDragOver += draghandler;
                this._adorner = new DragAdorner(this._dragScope , (UIElement)this.slecetedRectangle, 0.5);
                var layer = AdornerLayer.GetAdornerLayer(_dragScope);
                layer.Add(this._adorner);

               

                RotateTransform rtf = new RotateTransform();
                _sb.Children.Clear();
                rectangle.RenderTransform = rtf;
                rectangle.RenderTransformOrigin =new Point(0.5, 0.5);
                var dbAscending1 = new DoubleAnimation(0, 3, new Duration(TimeSpan.FromMilliseconds(100)));
                _sb.Children.Add(dbAscending1);
                  Storyboard.SetTarget(dbAscending1, rectangle);
                Storyboard.SetTargetProperty(dbAscending1,new PropertyPath("RenderTransform.Angle"));
                var dbAscending2 = new DoubleAnimation(3, -3, new Duration(TimeSpan.FromMilliseconds(200)));
                _sb.Children.Add(dbAscending2);
                  Storyboard.SetTarget(dbAscending2, rectangle);
                Storyboard.SetTargetProperty(dbAscending2, new PropertyPath("RenderTransform.Angle"));
                _sb.Begin();
               
                DragDrop.DoDragDrop(e.Source as UIElement, e.Source , DragDropEffects.Copy);
                e.Handled = true;
             //   rectangle.MouseMove -= SourcePanel_MouseMove;
             //   rectangle.GiveFeedback -= DragSource_GiveFeedback;
            }
        }

        private void DragScope_PreviewDragOver(object sender, DragEventArgs e)
        {
           
            if (this._adorner != null)
            {
                this._adorner.LeftOffset = e.GetPosition(this._dragScope).X;
                this._adorner.TopOffset = e.GetPosition(this._dragScope).Y;
            }
        }

        private void DragSource_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            var viewElement = sender as Rectangle;
            if (viewElement == null) return;
            e.UseDefaultCursors = true;
            // Mouse.SetCursor(HoloCursor);
            this.InvalidateVisual();
           
          
              e.Handled = true;
        }

        private void TargetPanel_Drop(object sender, DragEventArgs e)
        {
            var item = e.Data;
           // object data = item.GetData(item.GetFormats()[0]);
            object data = item.GetData(typeof(Rectangle));
            if (data is UIElement)
            {
                _sb.Stop();
                sourcePanel.Children.Remove(data as UIElement);
                targetPanel.Children.Add(data as UIElement);
            }
            IsMoving = false;
        }

        private void DragScope_PreviewDragOver(object sender, MouseEventArgs e)
        {
            if (this._adorner != null)
            {
                this._adorner.LeftOffset = e.GetPosition(this._dragScope).X;
                this._adorner.TopOffset = e.GetPosition(this._dragScope).Y;
            }
        }
    }
}
