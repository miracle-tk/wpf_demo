using System;
using System.Collections.Generic;
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

namespace 可视化对象
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Brush drawingBrush = Brushes.AliceBlue;
        private Brush selectedDrawingBrush = Brushes.Red;
        private Pen drawingPen = new Pen(Brushes.SteelBlue, 3);
        private Size squareSize = new Size(30, 30);
        private Brush selectionSquareBrush = Brushes.Transparent;
        private Pen selectionSquarePen = new Pen(Brushes.Black, 2);

        private DrawingVisual selectedVisual;
        private Vector offsetVector;
        private DrawingVisual selectionSquare;
        bool isDragging = false;
        bool isMultiSelecting = false;
        private Point selectionSquareTopLeft;
        public MainWindow()
        {
            InitializeComponent();
          //  this.Background = Brushes.Blue;
        }

        private void DrawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(drawingSurface);
            if (cmdAdd.IsChecked==true)
            {
                DrawingVisual visual = new DrawingVisual();
                DrawSquare(visual, p, false);
                drawingSurface.AddVisual(visual);
            }else if (cmdDelete.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(p);
                if (visual != null) drawingSurface.DeleteVisual(visual);
            }else if (cmdSelectMove.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(p);
                //Panel.SetZIndex(visual as UIElement, 99);
                if (visual != null)
                {
                    Point topLeftCorner = new Point(visual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2, visual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
                    DrawSquare(visual, topLeftCorner, true);
                    offsetVector = topLeftCorner - p;
                    isDragging = true;
                    if (selectedVisual != null && selectedVisual != visual)
                    {
                        ClearSelection();
                    }
                    selectedVisual = visual;
                }
            }else if (cmdSelectMultiple.IsChecked == true)
            {
                selectionSquare = new DrawingVisual();
                drawingSurface.AddVisual(selectionSquare);
                isMultiSelecting = true;
                selectionSquareTopLeft = p;
               // drawingSurface.CaptureMouse();
            }
        }

        private void ClearSelection()
        {
            Point topLeftCorner = new Point(selectedVisual.ContentBounds.TopLeft.X + drawingPen.Thickness / 2, selectedVisual.ContentBounds.TopLeft.Y + drawingPen.Thickness / 2);
            DrawSquare(selectedVisual, topLeftCorner, false);
        }

        private void DrawSquare1(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            using (DrawingContext dc = visual.RenderOpen())
            {
                Console.WriteLine(dc.GetHashCode());

                var brush = drawingBrush;
                if (isSelected)
                {
                    brush = selectedDrawingBrush;
                }
               dc.DrawRectangle(brush, drawingPen, new Rect(topLeftCorner, new Size(10, 10)));
                //dc.DrawRectangle(brush, drawingPen, new Rect(topLeftCorner, squareSize));


            }
        }

        private void DrawSquare(DrawingVisual visual, Point topLeftCorner, bool isSelected)
        {
            using(DrawingContext dc = visual.RenderOpen())
            {
                
               
                var brush = drawingBrush;
                if (isSelected)
                {
                    brush = selectedDrawingBrush;
                }
           //    dc.DrawRectangle(brush, drawingPen, new Rect(topLeftCorner, new Size(150, 150)));
                dc.DrawRectangle(brush, drawingPen, new Rect(topLeftCorner, squareSize));
                
               
            }
        }

        private void DrawingSurface_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = e.GetPosition(drawingSurface) + offsetVector;
                DrawSquare(selectedVisual, p, true);
            }
            if (isMultiSelecting)
            {
                Point p = e.GetPosition(drawingSurface);
                DrawSelectSquare(selectionSquareTopLeft, p);
            }
        }
        private void DrawSelectSquare(Point p1,Point p2)
        {
            selectionSquarePen.DashStyle = DashStyles.Dash;
            using(var dc = selectionSquare.RenderOpen())
            {
                dc.DrawRectangle(selectionSquareBrush, selectionSquarePen, new Rect(p1, p2));
            }
        }

        private void DrawingSurface_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            if (isMultiSelecting)
            {
                RectangleGeometry geometry = new RectangleGeometry(new Rect(selectionSquareTopLeft, e.GetPosition(drawingSurface)));
                var list = drawingSurface.GetVisuals(geometry);
                MessageBox.Show(list.Count().ToString());
                isMultiSelecting = false;
                drawingSurface.DeleteVisual(selectionSquare);
                drawingSurface.ReleaseMouseCapture();
            }
        }
    }
    public class MouseFollowGrid : UserControl
    {
        protected override void OnRender(DrawingContext drawingContext)
        {
            
            base.OnRender(drawingContext);
            var mP = Mouse.GetPosition(this);
            
            drawingContext.PushOpacity(0.2);
            drawingContext.DrawRectangle(Brushes.Blue, null, new Rect(0, 0, this.ActualWidth/2, this.ActualHeight/2));
            drawingContext.Pop();
            drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 2), new Point(0, mP.Y), new Point(this.ActualWidth, mP.Y));
            drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 2), new Point(mP.X, 0), new Point(mP.X, this.ActualHeight));
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            InvalidateVisual();
        }
    }
    public class DrawingCanvas : Canvas
    {
        private List<Visual> visuals = new List<Visual>();
        private List<DrawingVisual> hits = new List<DrawingVisual>();
        protected override int VisualChildrenCount {
            get { return visuals.Count; }
        }
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }
        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);
            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }
        public void DeleteVisual(Visual visual)
        {
            visuals.Remove(visual);
            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }
       
        public DrawingVisual GetVisual(Point p)
        {
            
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, p);
            return hitResult.VisualHit as DrawingVisual;
        }
        public List<DrawingVisual> GetVisuals(Geometry region)
        {

            hits.Clear();
           var param= new GeometryHitTestParameters(region);
            var callback = new HitTestResultCallback(HitTestCallback);
            VisualTreeHelper.HitTest(this, null, callback, param);
            return hits;
        }

        private HitTestResultBehavior HitTestCallback(HitTestResult result)
        {
            var re = (GeometryHitTestResult)result;
            DrawingVisual visual = result.VisualHit as DrawingVisual;
            if (visual != null && re.IntersectionDetail == IntersectionDetail.FullyInside)
            {
                hits.Add(visual);
            }
            return HitTestResultBehavior.Continue;
        }
    }
}
