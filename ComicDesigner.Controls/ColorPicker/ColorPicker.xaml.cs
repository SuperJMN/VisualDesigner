using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236
using PostSharp.Patterns.Recording;

namespace ComicDesigner.Controls.ColorPicker
{
    public sealed partial class ColorPicker : UserControl
    {
        //public event SelectedColorChangedHandler SelectedColorChanged;

        public ColorPicker()
        {
            this.InitializeComponent();
        }

        private float Hue;

        public static readonly DependencyProperty SelectedPrimaryProperty =
            DependencyProperty.Register("SelectedPrimary", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Transparent, OnSelectedPrimaryChanged));

        private static void OnSelectedPrimaryChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker cp = d as ColorPicker;

            if (cp != null)
            {
                cp.SelectedColor = cp.SelectedPrimary;

                cp.ColorSample.Background = new SolidColorBrush(cp.SelectedPrimary);

                cp.HexCode.Text = ColorSpace.GetHexCode(cp.SelectedColor);
            }
        }

        public Color SelectedPrimary
        {
            get { return (Color)GetValue(SelectedPrimaryProperty); }
            set { SetValue(SelectedPrimaryProperty, value); }
        }

        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker), new PropertyMetadata(Colors.Transparent, OnSelectedColorChanged));

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker cp = d as ColorPicker;

            if (cp != null)
            {
                if (cp.SelectedPrimary == Colors.Transparent && cp.SelectedColor != Colors.Transparent)
                {
                    cp.SelectedPrimary = cp.SelectedColor;
                    return;
                }

                cp.SelectedColorDisplay.Fill = new SolidColorBrush(cp.SelectedColor);

                cp.HexCode.Text = ColorSpace.GetHexCode(cp.SelectedColor);
            }
        }

        public Color SelectedColor
        {
            get { return (Color)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        bool bDetectColor = false;

        private void Rectangle_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            if ( this.recordingScope != null )
                throw new InvalidOperationException("There is already an active recording scope,");

            this.recordingScope = RecordingServices.AmbientRecorder.StartAtomicScope();
            bDetectColor = true;
        }


        private void Rectangle_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            GetHue(sender, e);
        }

        private void GetHue(object sender, PointerRoutedEventArgs e)
        {
            if (bDetectColor)
            {
                PointerPoint p = e.GetCurrentPoint(this.ColorBar);

                //this.Hue = (p.Position.X / this.ColorBar.ActualWidth) * 360;

                this.SelectedPrimary = GetColorAtPoint(sender as Border, p.Position);

                this.Hue = ColorSpace.ConvertRgbToHsv(this.SelectedColor).Hue;

                //this.SelectedColor = ColorSpace.ConvertHsvToRgb(Hue, 0, 1);
            }
        }

        private void Border_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (this.recordingScope == null)
                throw new InvalidOperationException("There is no active recording scope,");

            GetHue(sender, e);

            bDetectColor = false;

            this.recordingScope.Complete();
            this.recordingScope = null;


        }

        private void GetColorSample(object sender, PointerRoutedEventArgs e)
        {
            if (bPickSample)
            {
                PointerPoint p = e.GetCurrentPoint(this.ColorSample);

                float value = 1 - (float)(p.Position.Y / this.ColorSample.ActualHeight);
                float saturation = (float)(p.Position.X / this.ColorSample.ActualWidth);

                this.SelectedColor = ColorSpace.ConvertHsvToRgb(Hue, saturation, value);
            }
        }


        //Calculates the color of a point in a Border that is filled
        //with a LinearGradientBrush.
        private Color GetColorAtPoint(Border theRec, Point thePoint)
        {
            //Get properties
            LinearGradientBrush br = (LinearGradientBrush)theRec.Background;

            double y3 = thePoint.Y;
            double x3 = thePoint.X;

            double x1 = br.StartPoint.X * theRec.ActualWidth;
            double y1 = br.StartPoint.Y * theRec.ActualHeight;
            Point p1 = new Point(x1, y1); //Starting point

            double x2 = br.EndPoint.X * theRec.ActualWidth;
            double y2 = br.EndPoint.Y * theRec.ActualHeight;
            Point p2 = new Point(x2, y2);  //End point

            //Calculate intersecting points 
            Point p4 = new Point(); //with tangent

            if (y1 == y2) //Horizontal case
            {
                p4 = new Point(x3, y1);
            }

            else if (x1 == x2) //Vertical case
            {
                p4 = new Point(x1, y3);
            }

            else //Diagnonal case
            {
                double m = (y2 - y1) / (x2 - x1);
                double m2 = -1 / m;
                double b = y1 - m * x1;
                double c = y3 - m2 * x3;

                double x4 = (c - b) / (m - m2);
                double y4 = m * x4 + b;
                p4 = new Point(x4, y4);
            }

            //Calculate distances relative to the vector start
            double d4 = dist(p4, p1, p2);
            double d2 = dist(p2, p1, p2);

            double x = d4 / d2;

            //Clip the input if before or after the max/min offset values
            double max = br.GradientStops.Max(n => n.Offset);
            if (x > max)
            {
                x = max;
            }
            double min = br.GradientStops.Min(n => n.Offset);
            if (x < min)
            {
                x = min;
            }

            //Find gradient stops that surround the input value
            GradientStop gs0 = br.GradientStops.Where(n => n.Offset <= x).OrderBy(n => n.Offset).Last();
            GradientStop gs1 = br.GradientStops.Where(n => n.Offset >= x).OrderBy(n => n.Offset).First();

            float y = 0f;
            if (gs0.Offset != gs1.Offset)
            {
                y = (float)((x - gs0.Offset) / (gs1.Offset - gs0.Offset));
            }

            //Interpolate color channels
            Color cx = new Color();
            byte aVal = (byte)((gs1.Color.A - gs0.Color.A) * y + gs0.Color.A);
            byte rVal = (byte)((gs1.Color.R - gs0.Color.R) * y + gs0.Color.R);
            byte gVal = (byte)((gs1.Color.G - gs0.Color.G) * y + gs0.Color.G);
            byte bVal = (byte)((gs1.Color.B - gs0.Color.B) * y + gs0.Color.B);
            cx = Color.FromArgb(aVal, rVal, gVal, bVal);

            return cx;
        }

        //Helper method for GetColorAtPoint
        //Returns the signed magnitude of a point on a vector with origin po and pointing to pf
        private double dist(Point px, Point po, Point pf)
        {
            double d = Math.Sqrt((px.Y - po.Y) * (px.Y - po.Y) + (px.X - po.X) * (px.X - po.X));
            if (((px.Y < po.Y) && (pf.Y > po.Y)) ||
                ((px.Y > po.Y) && (pf.Y < po.Y)) ||
                ((px.Y == po.Y) && (px.X < po.X) && (pf.X > po.X)) ||
                ((px.Y == po.Y) && (px.X > po.X) && (pf.X < po.X)))
            {
                d = -d;
            }
            return d;
        }

        bool bPickSample = false;
        private RecordingScope recordingScope;

        private void ColorSample_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            bPickSample = true;
        }

        private void ColorSample_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            this.GetColorSample(sender, e);
        }

        private void ColorSample_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            this.GetColorSample(sender, e);

            bPickSample = false;
        }
    }
}
