using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Pictures.FallingCards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AddRectangle(0.2, 0.4, 0, 0, 0.1, 0, new Border() { Child = new Label() { Content = "MENU" } } );
            AddRectangle(0.6, 0.2, 0, 0, -0.2, 0, new Border() { Child = new Label() { Content = "PICTURES" } });

            SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RescaleRectangles();
        }

        private List<RectangleData> _rectangles = new();

        private void RescaleRectangles()
        {
            double actualWidth = CanvasView.ActualWidth;
            double actualHeight = CanvasView.ActualHeight;
            //double scaleX = actualWidth / 800;
            //double scaleY = actualHeight / 600;
            //double scale = Math.Min(scaleX, scaleY);

            foreach (var data in _rectangles)
            {
                double x = (0.5 + data.RelativeX) * actualWidth;
                double y = (0.5 + data.RelativeY) * actualHeight;

                double rectWidth = data.RelativeWidth * actualWidth;
                double rectHeight = data.RelativeHeight * actualHeight;
                if (data.MaxRatio >= 1)
                {
                    if (rectWidth / rectHeight > data.MaxRatio) rectWidth = rectHeight * data.MaxRatio;
                    else if (rectWidth / rectHeight < data.MinRatio) rectHeight = rectWidth / data.MinRatio;
                }

                data.Rectangle.Width = rectWidth;
                data.Rectangle.Height = rectHeight;
                data.Rectangle.RenderTransform = new RotateTransform(data.Angle, rectWidth / 2, rectHeight / 2);
                Canvas.SetLeft(data.Rectangle, x - rectWidth / 2);
                Canvas.SetTop(data.Rectangle, y - rectHeight / 2);
            }
            CheckForCollisions();
        }

        public bool AddRectangle(double relativeWidth, double relativeHeight, double maxRatio, double relativeX, double relativeY, double angle, string imagePath) =>
            AddRectangle(relativeWidth, relativeHeight, maxRatio, relativeX, relativeY, angle,
                new Image() { Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute)), Stretch = Stretch.UniformToFill, VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center });
        
        private int _timeout = 0;
        public bool AddRectangle(double relativeWidth, double relativeHeight, double maxRatio, double relativeX, double relativeY, double angle, UIElement child)
        {
            double actualWidth = CanvasView.ActualWidth;
            double actualHeight = CanvasView.ActualHeight;

            double x = (0.5 + relativeX) * actualWidth;
            double y = (0.5 + relativeY) * actualHeight;

            double rectWidth = relativeWidth * actualWidth;
            double rectHeight = relativeHeight * actualHeight;
            double minRatio = maxRatio * 0.9;
            if (maxRatio >= 1)
            {
                if (rectWidth / rectHeight > maxRatio) rectWidth = rectHeight * maxRatio;
                else if (rectWidth / rectHeight < minRatio) rectHeight = rectWidth / minRatio;
            }

            Border rect = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Width = rectWidth,
                Height = rectHeight,
                Child = child,
                RenderTransform = new RotateTransform(angle, rectWidth / 2, rectHeight / 2)
            };

            Canvas.SetLeft(rect, x - rectWidth / 2);
            Canvas.SetTop(rect, y - rectHeight / 2);

            var rectangle = new RectangleData { RelativeWidth = relativeWidth, RelativeHeight = relativeHeight, MaxRatio = maxRatio, RelativeX = relativeX, RelativeY = relativeY, Angle = angle, Rectangle = rect };
            if (CheckForCollisions(rectangle)) return false;
            

            CanvasView.Children.Add(rect);
            _rectangles.Add(rectangle);
            return true;
        }

        private bool CheckForCollisions(RectangleData rectangle)
        {
            if (_rectangles.Count < 2) return false;
            if (_timeout++ < 1000) return _rectangles.Any(rectB => CheckRotatedCollision(rectangle, rectB));
            return _rectangles.Take(2).Any(rectB => CheckRotatedCollision(rectangle, rectB));
        }
        private void CheckForCollisions()
        {
            foreach (var rectA in _rectangles.Skip(2).Reverse().ToList())
            {
                bool hasCollision = _rectangles.Any(rectB => rectA != rectB && _rectangles.Contains(rectB) && CheckRotatedCollision(rectA, rectB));
                //rectA.Rectangle.BorderBrush = hasCollision ? Brushes.Red : Brushes.Black; // Zmiana koloru ramki na czerwony jeśli kolizja
                if (hasCollision)
                {
                    CanvasView.Children.Remove(rectA.Rectangle);
                    _rectangles.Remove(rectA);
                }
            }
        }

        private bool CheckRotatedCollision(RectangleData rectA, RectangleData rectB)
        {
            Point[] aCorners = GetRotatedCorners(rectA);
            Point[] bCorners = GetRotatedCorners(rectB);

            return CheckSeparatingAxis(aCorners, bCorners) == false && CheckSeparatingAxis(bCorners, aCorners) == false;
        }

        private Point[] GetRotatedCorners(RectangleData rectData)
        {
            double x = Canvas.GetLeft(rectData.Rectangle) + rectData.Rectangle.Width / 2;
            double y = Canvas.GetTop(rectData.Rectangle) + rectData.Rectangle.Height / 2;
            double angleRad = rectData.Angle * Math.PI / 180;
            double cosA = Math.Cos(angleRad);
            double sinA = Math.Sin(angleRad);
            double w = rectData.Rectangle.Width / 2;
            double h = rectData.Rectangle.Height / 2;

            return
            [
                new Point(x + (-w * cosA - -h * sinA), y + (-w * sinA + -h * cosA)),
                new Point(x + ( w * cosA - -h * sinA), y + ( w * sinA + -h * cosA)),
                new Point(x + ( w * cosA -  h * sinA), y + ( w * sinA +  h * cosA)),
                new Point(x + (-w * cosA -  h * sinA), y + (-w * sinA +  h * cosA))
            ];
        }

        private bool CheckSeparatingAxis(Point[] rectA, Point[] rectB)
        {
            for (int i = 0; i < rectA.Length; i++)
            {
                Point edge = new Point(rectA[(i + 1) % rectA.Length].X - rectA[i].X, rectA[(i + 1) % rectA.Length].Y - rectA[i].Y);
                Point axis = new Point(-edge.Y, edge.X);

                double minA = rectA.Select(p => (p.X * axis.X + p.Y * axis.Y)).Min();
                double maxA = rectA.Select(p => (p.X * axis.X + p.Y * axis.Y)).Max();
                double minB = rectB.Select(p => (p.X * axis.X + p.Y * axis.Y)).Min();
                double maxB = rectB.Select(p => (p.X * axis.X + p.Y * axis.Y)).Max();

                if (maxA < minB || maxB < minA) return true;
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double x, y, angle;
            do
            {
                Random rnd = new();
                (x, y, angle) = (rnd.NextDouble() - 0.5, rnd.NextDouble() - 0.5, rnd.NextDouble() * 60 - 30);
                Data.Content = $"X: {x}; Y: {y}; Angle: {angle}";
            }
            while (!AddRectangle(0.2, 0.2, 1.5, x, y, angle, @"https://altimadental.pl/wp-content/uploads/2015/01/default-placeholder.png")) ;
        }
    }

    public class RectangleData
    {
        public double MinRatio => MaxRatio * 0.8;
        public double MaxRatio { get; set; } = -1;
        public double RelativeWidth { get; set; }
        public double RelativeHeight { get; set; }
        public double RelativeX { get; set; }
        public double RelativeY { get; set; }
        public double Angle { get; set; }
        public Border Rectangle { get; set; }
    }
}