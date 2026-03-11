using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace _3ISIP223_PogosyanWPF
{
    public partial class MainWindow : Window
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        private Viewport3D viewport;
        private PerspectiveCamera camera;

        private bool _isMouseCaptured = false;

        private double _rotX = 0; 
        private double _rotY = 0;
        private double _currentrotX = 0;
        private double _currentrotY = 0;

        private const double HorizontalSensitivity = 0.1;
        private const double VerticalSensitivity = 0.15; 

        private const double MinAngle = -50; 
        private const double MaxAngle = 50;
        private const double SmoothingFactor = 0.1;

        //public DispatcherTimer animationTimer;

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
            Point center = new Point((this.Width / 2) - 10, (this.Height / 2) - 6);

            double deltaX = currentPosition.X - center.X;
            double deltaY = currentPosition.Y - center.Y;

            if (Math.Abs(deltaX) < 1 && Math.Abs(deltaY) < 1)
            {
                CenterMouse();
                return;
            }

            _rotX += deltaX * HorizontalSensitivity;
            _rotY -= deltaY * VerticalSensitivity;


            _rotY = Math.Max(MinAngle, Math.Min(MaxAngle, _rotY));
            _rotX = Math.Max(MinAngle, Math.Min(MaxAngle, _rotX));

            CenterMouse();

        }

        private void UpdateCameraDirection()
        {

            _currentrotX += (_rotX - _currentrotX) * SmoothingFactor;
            _currentrotY += (_rotY - _currentrotY) * SmoothingFactor;

            if (Math.Abs(_rotX - _currentrotX) < 0.01)
                _currentrotX = _rotX;
            if (Math.Abs(_rotY - _currentrotY) < 0.01)
                _currentrotY = _rotY;

            double radX = _currentrotX * Math.PI / 180.0;
            double radY = _currentrotY * Math.PI / 180.0;

            double lookX = Math.Sin(radX) * Math.Cos(radY);
            double lookY = Math.Sin(radY);
            double lookZ = -Math.Cos(radX) * Math.Cos(radY);

            camera.LookDirection = new Vector3D(lookX, lookY, lookZ);

        }

        private void CenterMouse()
        {
            Point windowCenter = this.PointToScreen(new Point((this.Width / 2)-10, (this.Height / 2)-6));


            SetCursorPos((int)windowCenter.X, (int)windowCenter.Y);
        }

        private void Viewport3D_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isMouseCaptured)
            {
                _isMouseCaptured = true;
                //UserInterFrame.IsHitTestVisible = false;
                Mouse.OverrideCursor = Cursors.None;
                CenterMouse();

            }
            else
            {

                //var timeline = new ParallelTimeline();

                DoubleAnimation attackAnimation = new DoubleAnimation();
                attackAnimation.From = 20;  
                attackAnimation.To = -30;
                attackAnimation.Duration = TimeSpan.FromMilliseconds(150);
                attackAnimation.AutoReverse = true;
                Console.WriteLine("Удар");
                WeaponAngle.BeginAnimation(AxisAngleRotation3D.AngleProperty, attackAnimation);


            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape && _isMouseCaptured)
            {
                if (_isMouseCaptured)
                {
                    _isMouseCaptured = false;
                    Mouse.Capture(null);
                    Mouse.OverrideCursor = null;
                    //UserInterFrame.IsHitTestVisible = true;

                }
            }
        }

        //private void Window_Deactivated(object sender, EventArgs e)
        //{
        //    ReleaseMouseCaptures();
        ////}


        private void ModelUIElement3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Куб нажат!");
            enem.Visibility = Visibility.Collapsed;

        }
    }
}