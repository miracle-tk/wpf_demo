using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace 高性能图表
{
    public class DrawingCanvas : Canvas
    {
        private List<Visual> visuals = new List<Visual>();
        public enum MouseMode
        {
            ZOOM,
            VIEW
        }
        public MouseMode mMode = MouseMode.VIEW;
        public DrawingCanvas()
        {
            this.Background = Brushes.Transparent;
            this.PreviewMouseLeftButtonDown += DrawingCanvas_MouseLeftButtonDown;
            this.PreviewMouseLeftButtonUp += DrawingCanvas_MouseLeftButtonUp;
            this.MouseMove += DrawingCanvas_MouseMove;
            this.MouseLeave += DrawingCanvas_MouseLeave;
        }

        private void DrawingCanvas_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (mMode == MouseMode.VIEW && textVisual != null)
            {
                this.RemoveVisual(textVisual);
                this.InvalidateVisual();
            }
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isMouseDown = false;
            mMode = MouseMode.VIEW;
            endup = e.GetPosition(this).X;
            if (endup < YZero)
                endup = YZero;
            if (endup > canvasWidth)
                endup = canvasWidth;
            //重画 选中区域
            double step = (canvasWidth - YZero) / sData.Count;
            if (isMouseMoved && Math.Abs(endup - startDown) > step)
            {

                int startIndex = 0;
                int endIndex = 0;
                if (endup > startDown)
                {
                    startIndex = (int)((startDown - YZero) / step) + 1;
                    endIndex = (int)((endup - YZero) / step);
                }
                else
                {
                    startIndex = (int)((endup - YZero) / step) + 1;
                    endIndex = (int)((startDown - YZero) / step);
                }
                //暂存
                DoubleCollection tempM = new DoubleCollection(mData.Count);
                DoubleCollection tempS = new DoubleCollection(sData.Count);
                for (int i = 0; i < mData.Count; i++)
                {
                    tempM.Add(mData[i]);
                    tempS.Add(sData[i]);
                }
                mData.Clear();
                sData.Clear();
                for (int i = startIndex; i <= endIndex; i++)
                {
                    mData.Add(tempM[i]);
                    sData.Add(tempS[i]);
                }
                this.Polyline();
                this.RemoveVisual(rectVisual);
                this.InvalidateVisual();
            }
            isMouseMoved = false;
        }
        double preMoved = YZero;
        private void DrawingCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            double positionX = e.GetPosition(this).X;
            if (mMode == MouseMode.VIEW)
            {
                Console.WriteLine("Moving" + positionX);
                if (positionX > YZero && positionX < canvasWidth && Math.Abs(positionX - preMoved) >= (canvasWidth - YZero) / mData.Count)
                {

                    Console.WriteLine("" + positionX);
                    this.PolyText(positionX);

                    //this.InvalidateVisual();
                }
            }
            else if (isMouseDown && startDown > YZero && startDown < canvasWidth)
            {

                isMouseMoved = true;

                this.PolyRect(startDown, positionX);
                this.InvalidateVisual();

            }
        }

        private bool isMouseDown = false;
        private bool isMouseMoved = false;
        private double startDown;
        private double endup;
        private int clickTimes = 0;
        private void DrawingCanvas_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            isMouseDown = true;
            mMode = MouseMode.ZOOM;
            startDown = e.GetPosition(this).X;

            if (textVisual != null)
                this.RemoveVisual(textVisual);
            //双击
            clickTimes++;
            DispatcherTimer timer = new DispatcherTimer();

            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);

            timer.Tick += (s, e1) => { timer.IsEnabled = false; clickTimes = 0; };

            timer.IsEnabled = true;

            if (clickTimes % 2 == 0)

            {
                Console.WriteLine("double");
                timer.IsEnabled = false;

                clickTimes = 0;

                mData.Clear();
                sData.Clear();
                for (int i = 0; i < rtData.Count; i++)
                {
                    mData.Add(rtData[i]);
                    sData.Add(thData[i]);
                }
                this.Polyline();
                this.InvalidateVisual();
            }
        }


        //获取Visual的个数
        protected override int VisualChildrenCount
        {
            get { return visuals.Count; }
        }



        //获取Visual
        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        //添加Visual
        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);

            base.AddVisualChild(visual);
            base.AddLogicalChild(visual);
        }

        //删除Visual
        public void RemoveVisual(Visual visual)
        {
            visuals.Remove(visual);

            base.RemoveVisualChild(visual);
            base.RemoveLogicalChild(visual);
        }

        //命中测试
        public DrawingVisual GetVisual(Point point)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, point);
            return hitResult.VisualHit as DrawingVisual;
        }

        private const double YZero = 50;
        private const double YMargin = 10;
        private const double YLabelLen = 5;
        private double yHeight;
        private Brush line1Color = Brushes.Black;
        private Brush labelColor = Brushes.Black;
        private Brush axisColor = Brushes.Black;
        private Brush line2Color = Brushes.Black;
        private double thinkness = 1;
        private double yLabelMax = 100;
        private double yLabelMin = -100;
        private double canvasWidth = 0;
        private const int yLinesCount = 12;
        DoubleCollection rtData;
        DoubleCollection thData;
        DoubleCollection mData;
        DoubleCollection sData;
        //使用DrawVisual画Polyline
        DrawingVisual lineVisual;
        public DrawingVisual Polyline()
        {
            if (lineVisual != null)
                this.RemoveVisual(lineVisual);
            lineVisual = new DrawingVisual();
            DrawingContext dc = lineVisual.RenderOpen();
            Pen penAxis = new Pen(AxisColor, Thinkness);
            penAxis.Freeze();
            //画Y轴
            dc.DrawLine(penAxis, new Point(YZero, YMargin), new Point(YZero, YHeight - YMargin));
            dc.DrawLine(penAxis, new Point(YZero - YLabelLen, YMargin), new Point(YZero, YMargin));
            dc.DrawLine(penAxis, new Point(YZero - YLabelLen, YHeight / 2), new Point(YZero, YHeight / 2));
            dc.DrawLine(penAxis, new Point(YZero - YLabelLen, YHeight - YMargin), new Point(YZero, YHeight - YMargin));

            FormattedText ft1 = new FormattedText(yLabelMax.ToString(), new System.Globalization.CultureInfo("zh-CHS", false), FlowDirection.LeftToRight, new Typeface("Microsoft YaHei"), 10, Brushes.Black);
            dc.DrawText(ft1, new Point(YZero - YLabelLen - ft1.Width, YMargin - ft1.Height / 2));

            FormattedText ft2 = new FormattedText(((yLabelMax + yLabelMin) / 2).ToString(), new System.Globalization.CultureInfo("zh-CHS", false), FlowDirection.LeftToRight, new Typeface("Microsoft YaHei"), 10, Brushes.Black);
            dc.DrawText(ft2, new Point(YZero - YLabelLen - ft2.Width, YHeight / 2 - ft2.Height / 2));

            FormattedText ft3 = new FormattedText(yLabelMin.ToString(), new System.Globalization.CultureInfo("zh-CHS", false), FlowDirection.LeftToRight, new Typeface("Microsoft YaHei"), 10, Brushes.Black);
            dc.DrawText(ft3, new Point(YZero - YLabelLen - ft3.Width, YHeight - YMargin - ft3.Height / 2));
            //画线
            Pen linePen1 = new Pen(Line1Color, Thinkness);
            linePen1.Freeze();
            Pen linePen2 = new Pen(Line2Color, Thinkness);
            linePen2.Freeze();
            double ratio = (yLabelMax - yLabelMin) / (YHeight - 2 * YMargin);
            for (int i = 0; i < mData.Count - 1; i++)
            {
                //将数值转换成位置   
                //理论值 
                Point p3 = new Point(YZero + i * (canvasWidth - YZero) / sData.Count, YMargin + (yLabelMax - sData[i]) / ratio);
                Point p4 = new Point(YZero + (i + 1) * (canvasWidth - YZero) / sData.Count, YMargin + (yLabelMax - sData[i + 1]) / ratio);
                dc.DrawLine(linePen2, p3, p4);
                //实测值
                Point p1 = new Point(YZero + i * (canvasWidth - YZero) / mData.Count, YMargin + (yLabelMax - mData[i]) / ratio);
                Point p2 = new Point(YZero + (i + 1) * (canvasWidth - YZero) / mData.Count, YMargin + (yLabelMax - mData[i + 1]) / ratio);
                dc.DrawLine(linePen1, p1, p2);
            }
            //绘制纵向网格和x轴文本
            Pen pen3 = new Pen(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666")), 4);
            pen3.DashStyle = new DashStyle(new double[] { 2.5, 2.5 }, 0);
            pen3.Freeze();
            double stepX = (canvasWidth - YZero) / yLinesCount;
            int stepData = mData.Count / yLinesCount;
            if (stepData < 1)
                stepData = 1;
            for (int i = 1; i < yLinesCount; i++)
            {
                //纵向网格
                Point p1 = new Point(YZero + i * stepX, YHeight - YMargin);
                Point p2 = new Point(YZero + i * stepX, YMargin);
                dc.DrawLine(pen3, p1, p2);
                //x轴文本
                FormattedText ftX = new FormattedText((i * stepData).ToString(), new System.Globalization.CultureInfo("zh-CHS", false), FlowDirection.LeftToRight, new Typeface("Microsoft YaHei"), 10, Brushes.Black);
                Point p3 = new Point(YZero + i * stepX - ftX.Width / 2, yHeight - YMargin);
                dc.DrawText(ftX, p3);
            }

            dc.Close();

            this.AddVisual(lineVisual);
            return lineVisual;
        }
        //绘制鼠标选择放大的矩形
        DrawingVisual rectVisual;
        public DrawingVisual PolyRect(double startx, double endx)
        {
            if (rectVisual != null)
                this.RemoveVisual(rectVisual);
            rectVisual = new DrawingVisual();
            DrawingContext dc = rectVisual.RenderOpen();

            Pen pen = new Pen(new SolidColorBrush(Color.FromArgb(100, 255, 200, 200)), 1);
            dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(100, 255, 200, 200)), pen, new Rect(new Point(startx, YMargin), new Point(endx, YHeight - YMargin)));

            dc.Close();
            this.AddVisual(rectVisual);

            return rectVisual;
        }
        //绘制显示选中的数值
        DrawingVisual textVisual;
        public void PolyText(double xPosition)
        {
            if (textVisual != null)
            {
                this.RemoveVisual(textVisual);
                //textVisual = new DrawingVisual();
                //this.AddVisual(textVisual);
            }


            textVisual = new DrawingVisual();
            DrawingContext dc = textVisual.RenderOpen();

            double step = (canvasWidth - YZero) / mData.Count;
            int index = (int)((xPosition - YZero) / step);
            double ax = YZero + index * step;
            //竖线
            Pen pen = new Pen(Brushes.Transparent, 3);
            pen.Freeze();
            FormattedText ftX = new FormattedText("里程:" + index + " 实测:" + mData[index] + " 设计:" + sData[index], new System.Globalization.CultureInfo("zh-CHS", false), FlowDirection.LeftToRight, new Typeface("Microsoft YaHei"), 15, Brushes.White);
            //计算是否超出范围
            if (ax + ftX.Width < canvasWidth)
            {
                dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(200, 0, 150, 179)), pen, new Rect(new Point(ax, YMargin), new Point(ax + ftX.Width, YMargin + ftX.Height)));
                dc.DrawText(ftX, new Point(ax, YMargin));
            }
            else
            {
                dc.DrawRectangle(new SolidColorBrush(Color.FromArgb(200, 0, 150, 179)), pen, new Rect(new Point(ax - ftX.Width, YMargin), new Point(ax, YMargin + ftX.Height)));
                dc.DrawText(ftX, new Point(ax - ftX.Width, YMargin));
            }

            Pen penLine = new Pen(new SolidColorBrush(Color.FromArgb(200, 0, 150, 179)), 3);
            penLine.DashStyle = new DashStyle(new double[] { 2.5, 2.5 }, 0);
            penLine.Freeze();
            dc.DrawLine(penLine, new Point(ax, YMargin), new Point(ax, YHeight - YMargin));
            dc.Close();
            this.AddVisual(textVisual);
        }

        public double YHeight
        {
            get
            {
                return yHeight;
            }

            set
            {
                yHeight = value;
            }
        }



        public Brush LabelColor
        {
            get
            {
                return labelColor;
            }

            set
            {
                labelColor = value;
            }
        }

        public Brush AxisColor
        {
            get
            {
                return axisColor;
            }

            set
            {
                axisColor = value;
            }
        }

        public double Thinkness
        {
            get
            {
                return thinkness;
            }

            set
            {
                thinkness = value;
            }
        }

        public double YLabelMax
        {
            get
            {
                return yLabelMax;
            }

            set
            {
                yLabelMax = value;
            }
        }

        public double YLabelMin
        {
            get
            {
                return yLabelMin;
            }

            set
            {
                yLabelMin = value;
            }
        }

        public static double YZero1
        {
            get
            {
                return YZero;
            }
        }

        public double CanvasWidth
        {
            get
            {
                return canvasWidth;
            }

            set
            {
                canvasWidth = value;
            }
        }

        public Brush Line2Color
        {
            get
            {
                return line2Color;
            }

            set
            {
                line2Color = value;
            }
        }

        public Brush Line1Color
        {
            get
            {
                return line1Color;
            }

            set
            {
                line1Color = value;
            }
        }

        public DoubleCollection RtData
        {
            get
            {
                return rtData;
            }

            set
            {
                rtData = value;
                mData = new DoubleCollection(value.Count);
                foreach (double temp in value)
                {
                    mData.Add(temp);
                }
            }
        }

        public DoubleCollection ThData
        {
            get
            {
                return thData;
            }

            set
            {
                thData = value;
                sData = new DoubleCollection(value.Count);
                foreach (double temp in value)
                {
                    sData.Add(temp);
                }
            }
        }
    }
}