using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private Viewport3D viewport;
        private PerspectiveCamera camera;

        private bool _isMouseCaptured = false;

        private double _rotationX = 0; 
        private double _rotationY = 0;
        private double _currentRotationX = 0;
        private double _currentRotationY = 0;

        private const double HorizontalSensitivity = 0.2;
        private const double VerticalSensitivity = 0.2; 

        //private const double MinVerticalAngle = -40;
        //private const double MaxVerticalAngle = 40;
        private const double MinAngle = -40; 
        private const double MaxAngle = 40; 

        public MainWindow()
        {
            InitializeComponent();
            viewport = Viewport;
            camera = viewport.Camera as PerspectiveCamera;
            CompositionTarget.Rendering += (s, e) => UpdateCameraDirection();

        }

        private void Viewport3D_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseCaptured) return;

            Point currentPosition = e.GetPosition(this);
            Point center = new Point((this.Width / 2) - 10, (this.Height / 2) + 5);

            double deltaX = currentPosition.X - center.X;
            double deltaY = currentPosition.Y - center.Y;

            if (Math.Abs(deltaX) < 1.5 && Math.Abs(deltaY) < 1.5)
            {
                CenterMouse();
                return;
            }

            _rotationX += deltaX * HorizontalSensitivity;
            _rotationY -= deltaY * VerticalSensitivity;


            _rotationY = Math.Max(MinAngle, Math.Min(MaxAngle, _rotationY));
            _rotationX = Math.Max(MinAngle, Math.Min(MaxAngle, _rotationX));

            UpdateCameraDirection();

            //Debug.WriteLine($"Y={_rotationY}, дельтаY={deltaY}");

            CenterMouse();
        }
        private const double SmoothingFactor = 0.2;
        private void UpdateCameraDirection()
        {

            _currentRotationX += (_rotationX - _currentRotationX) * SmoothingFactor;
            _currentRotationY += (_rotationY - _currentRotationY) * SmoothingFactor;

            if (Math.Abs(_rotationX - _currentRotationX) < 0.01)
                _currentRotationX = _rotationX;
            if (Math.Abs(_rotationY - _currentRotationY) < 0.01)
                _currentRotationY = _rotationY;

            double radX = _currentRotationX * Math.PI / 180.0;
            double radY = _currentRotationY * Math.PI / 180.0;

            double lookX = Math.Sin(radX) * Math.Cos(radY);
            double lookY = Math.Sin(radY);
            double lookZ = -Math.Cos(radX) * Math.Cos(radY);

            camera.LookDirection = new Vector3D(lookX, lookY, lookZ);
        }

        private void CenterMouse()
        {
            Point windowCenter = this.PointToScreen(new Point((this.Width / 2)-10, (this.Height / 2)+5));


            SetCursorPos((int)windowCenter.X, (int)windowCenter.Y);
        }

        private void Viewport3D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseCaptured = true;
            //Mouse.Capture(viewport, CaptureMode.Element);
            Mouse.OverrideCursor = Cursors.None;
            CenterMouse();
        }

        private void ReleaseMouseCaptures()
        {
            if (_isMouseCaptured)
            {
                _isMouseCaptured = false;
                Mouse.Capture(null);
                Mouse.OverrideCursor = null;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && _isMouseCaptured)
            {
                ReleaseMouseCaptures();
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            ReleaseMouseCaptures();
        }


        private void ModelUIElement3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Куб нажат!");
            enem.Visibility = Visibility.Collapsed;

        }
    }
}