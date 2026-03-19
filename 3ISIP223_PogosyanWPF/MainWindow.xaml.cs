using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
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
        private const double VerticalSensitivity = 0; 

        private const double MinAngle = -50; 
        private const double MaxAngle = 50;
        private const double SmoothingFactor = 0.1;

        public DateTime AttackTimer;
        private bool isAttacking = false;


        public MainWindow()
        {
            mainMenu menu = new mainMenu();
            var result = menu.ShowDialog();
            if (result == false)
            {
                Close();
            }
            InitializeComponent();
            viewport = Viewport;
            camera = viewport.Camera as PerspectiveCamera;
            CompositionTarget.Rendering += (s, e) => UpdateCameraDirection();

            NewLevel();

        }



        public void GenerateBoss()
        {
            ModelUIElement3D mod;
            mod = CreateModelEnemy.CreateModel(Colors.Gray, 0.5, 0, 2);
            mod.MouseDown += ModelUIElement3D_MouseDown;
            WorkGame.Game.SelectBoss(mod);
            Console.WriteLine($"{mod}");
            //WorkGame.Game.Enemies.Add(Vrags.Enemies[0].CreateEnemy(mod));


            //ModelUIElement3D mod = WorkGame.Game.Enemies[0].model;
            Viewport.Children.Add(mod);
        }
        public void GenerateEnemies()
        {

            ModelUIElement3D mod;
            for (double i = -0.8; i < 1; i+=.8)
            {
                mod = CreateModelEnemy.CreateModel(Colors.Gray, i, 0, 2);
                mod.MouseDown += ModelUIElement3D_MouseDown;
                WorkGame.Game.AddEnemis(mod);
                Console.WriteLine($"{mod}");
                //WorkGame.Game.Enemies.Add(Vrags.Enemies[0].CreateEnemy(mod));


                //ModelUIElement3D mod = WorkGame.Game.Enemies[0].model;
                Viewport.Children.Add(mod);

            }
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
            //Console.WriteLine($"x: {_currentrotX}; y: {_currentrotY}");

            if (Math.Abs(_rotX - _currentrotX) < 0.01)
                _currentrotX = _rotX;
            if (Math.Abs(_rotY - _currentrotY) < 0.01)
                _currentrotY = _rotY;

            double radX = _currentrotX * Math.PI / 180.0;
            double radY = _currentrotY * Math.PI / 180.0;

            double lookX = Math.Sin(radX) * Math.Cos(radY);
            double lookY = 0;
            //double lookY = Math.Sin(radY);
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
                if (IsClicking) return;


                //isAttacking = true;
                //Console.WriteLine(isAttacking + "    ");
                AttackTimer = DateTime.Now;
                //var timeline = new ParallelTimeline();

                DoubleAnimation attackAnimation = new DoubleAnimation();
                attackAnimation.From = 20;  
                attackAnimation.To = -30;
                attackAnimation.Duration = TimeSpan.FromMilliseconds(250);
                attackAnimation.AutoReverse = true;
                //attackAnimation.Completed += (s, er) =>
                //{
                //    isAttacking = false;
                //    Console.Write(isAttacking + "    ");
                //};
                //Console.WriteLine("Удар");
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

        

        public void NewLevel()
        {
            if (WorkGame.Game.step == 1)
            {
                GenerateEnemies();
                WorkGame.Game.step++;
                return;
            }

            if (WorkGame.Game.step == 4)
            {
                GenerateBoss();
                WorkGame.Game.step++;
                return;
            }
            LevelUP.Visibility = Visibility.Visible;
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = 0;
            anim.To = 1;
            anim.Duration = TimeSpan.FromMilliseconds(350);
            anim.AutoReverse = true;

            anim.Completed += (e, s) =>
            {
                GenerateEnemies();
                LevelUP.Visibility = Visibility.Collapsed;
                WorkGame.Game.step++;
            };
            LevelUP.BeginAnimation(OpacityProperty, anim);



        }
        public Viewport2DVisual3D GetViewport2DText(TranslateTransform3D transform, double num, bool isBosse = false )
        {
            Viewport2DVisual3D viewport2D = new Viewport2DVisual3D();
            TranslateTransform3D translate = new TranslateTransform3D(transform.OffsetX + (isBosse ? 1.05 : 1.15), .5, transform.OffsetZ);
            MeshGeometry3D mesh = new MeshGeometry3D();

            double xP = ((int)num).ToString().Length == 1 ? 0.05 : 0.1;
            mesh.Positions = new Point3DCollection
            {
                new Point3D(-xP, 0.1, -4),
                new Point3D(xP, 0.1, -4),
                new Point3D(xP, -0.1, -4),
                new Point3D(-xP, -0.1, -4)
            };
            mesh.TriangleIndices = new Int32Collection { 0, 2, 1, 2, 0, 3 };
            mesh.TextureCoordinates = new PointCollection
            {
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 1),
                new Point(0, 1) 
            };
            viewport2D.Geometry = mesh;

            DiffuseMaterial material = new DiffuseMaterial();
            material.SetValue(Viewport2DVisual3D.IsVisualHostMaterialProperty, true);
            viewport2D.Material = material;

            TextBlock textBlock = new TextBlock();
            textBlock.Text = num.ToString();
            textBlock.Foreground = Brushes.Red;


            viewport2D.Transform = translate;

            viewport2D.Visual = textBlock;

            

            return viewport2D;
        }

        private bool IsClicking => (DateTime.Now - AttackTimer).TotalMilliseconds < 500;
        
        public void ModelUIElement3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isMouseCaptured) return;
            //if (isAttacking) return;
            if (IsClicking) return;
            //Console.WriteLine("Удар по enemy, ${0}", isAttacking);

            //MessageBox.Show("Куб нажат!");
            ModelUIElement3D mod = (ModelUIElement3D)sender;
            var transform = mod.Transform as TranslateTransform3D;
            //mod.Visibility = Visibility.Collapsed;
            //Console.WriteLine($"{WorkGame.Game.Enemies.FirstOrDefault(s => s.model == mod).Name}");
            double attack = WorkGame.Game.Attack(mod);

            Viewport2DVisual3D text = GetViewport2DText(transform, attack);
            Viewport.Children.Add(text);


            var begAnim = new DoubleAnimation();
            begAnim.From = 0;
            begAnim.To = 1;
            begAnim.Duration = TimeSpan.FromMilliseconds(200);


            begAnim.Completed += (s, eа) =>
            {
                Viewport.Children.Remove(text);
            };

            text.BeginAnimation(OpacityProperty, begAnim);
            if (attack == 0)
            {
                if (WorkGame.Game.isBoss)
                {
                    WorkGame.Game.DeleteBoss();
                }
                else
                {
                    WorkGame.Game.DeleteEnemis(mod);
                }
                Viewport.Children.Remove(mod);
                if (WorkGame.Game.CountEnemies == 0 && !WorkGame.Game.isBoss)
                {
                    NewLevel();
                }
            }
        }

    }
}