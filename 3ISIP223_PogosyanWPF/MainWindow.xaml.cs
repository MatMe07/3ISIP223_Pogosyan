using _3ISIP223_PogosyanWPF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace _3ISIP223_PogosyanWPF
{
    public partial class MainWindow
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        public static Viewport3D viewport {get; set;}
        private PerspectiveCamera camera;

        private bool _isMouseCaptured { get; set; } = false;

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
        public DateTime EnemyAttackTime;

        public BoxSpawn boxSpawn;
        //private bool isAttacking = false;
        public static StackPanel LogerPanel {  get; set; }
        public static Grid FrozenPanel {  get; set; }

        //public static Action WindowWinOrAgain = WindWinOrAgain;
        public MainWindow()
        {
            DataContext = WorkGame.Game;
            mainMenu menu = new mainMenu();
            boxSpawn = new BoxSpawn();
            //EnemyAttackTime = DateTime.Now;
            var result = menu.ShowDialog();
            if (result == false)
            {
                Close();
            }
            InitializeComponent();
            viewport = Viewport;
            camera = viewport.Camera as PerspectiveCamera;
            CompositionTarget.Rendering += (s, e) => UpdateCameraDirection();
            WorkGame.Game.GameOver = WindWinOrAgain;
            //Border bo = LogirText("gekk");
            //stackLogir.Children.Add(bo);
            LogerPanel = stackLogir;
            FrozenPanel = FrozGrid;
            NewLevel();

        }

        public static void RestartGame()
        {
            WorkGame.Game.Restart();
        }

        public void WindWinOrAgain(string text)
        {
            //var CursOver = Mouse.OverrideCursor;
            WorkGame.Game.IsPause = true;
            _isMouseCaptured = false;
            Mouse.OverrideCursor = null;
            //CompositionTarget.Rendering -= (s, e) => UpdateCameraDirection();
            var windRes = new ResulLevelWindow(text);
            windRes.Owner = this;
            //this.IsEnabled = false;
            var result = windRes.ShowDialog();
            windRes.Closed += (s, e) => this.IsEnabled = true;
            if (result == false)
            {
                Close();
            }
            else
            {

                Mouse.OverrideCursor = Cursors.None;
                WorkGame.Game.IsPause = false;
                switch (text)
                {
                    case "win":
                        {
                            //WorkGame.Game.step++;
                            ////NewLevel();

                            break;
                        }
                    case "again":
                        {
                            RestartGame();
                            NewLevel();
                            break;
                        }
                }
                _isMouseCaptured = true;
                //CompositionTarget.Rendering -= (s, e) => UpdateCameraDirection();
            }
        }

        private void WindRes_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        public void GenerateBoss()
        {
            WorkGame.Game.SelectBoss();
            MeshGeometry3D mesh;

            mesh = CreateModelEnemy.CreateModel(Colors.Gray, isBoss:true);
            var model = new ModelUIElement3D();

            var imageBrush = new ImageBrush();
            var binding = new Binding("ImagePath");
            binding.Source = WorkGame.Game.Boss.EnemyModel;
            BindingOperations.SetBinding(imageBrush, ImageBrush.ImageSourceProperty, binding);

            var geom = new GeometryModel3D(mesh, new DiffuseMaterial(imageBrush));

            model.MouseLeftButtonDown += ModelUIElement3D_MouseDown;

            model.Model = geom;

            model.Transform = new TranslateTransform3D() { OffsetZ = -1, OffsetX = 1, OffsetY = 0 };


            Console.WriteLine($"{model}");
            WorkGame.Game.Boss.model = model;
            Viewport.Children.Add(model);
        }
        public void GenerateEnemies()
        {

            MeshGeometry3D mesh;
            
            for (double i = -.5; i <= 3; i+=1.5)
            {

                var enem = WorkGame.Game.AddEnemis();

                mesh = CreateModelEnemy.CreateModel(Colors.Gray);
                var model = new ModelUIElement3D();

                var imageBrush = new ImageBrush();
                var binding = new Binding("ImagePath");
                binding.Source = enem.EnemyModel;
                BindingOperations.SetBinding(imageBrush, ImageBrush.ImageSourceProperty, binding);

                var geom = new GeometryModel3D(mesh, new DiffuseMaterial(imageBrush));
                model.Model = geom;

                model.Transform = new TranslateTransform3D() { OffsetZ = -0, OffsetX = i, OffsetY = 0 };



                model.MouseLeftButtonDown += ModelUIElement3D_MouseDown;
                Console.WriteLine($"{model}");
                enem.model = model;

                Viewport.Children.Add(model);

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


        private void UpdateBillboard()
        {
            var transform = new Vector3D(SpritePosition.OffsetX, SpritePosition.OffsetY, SpritePosition.OffsetZ);
            Vector3D direction = new Vector3D(camera.Position.X, camera.Position.Y, camera.Position.Z) - transform;
            direction.Normalize();

            double angle = Math.Atan2(direction.X, direction.Z) * 180 / Math.PI;

            SpriteRotation.Angle = angle;
            karandzavRotatation.Angle = angle;
        }
        private void UpdateCameraDirection()
        {
            if (WorkGame.Game.IsPause) return;
            WorkGame.Game.AttackEnemy();
            UpdateBillboard();

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
            double lookY = -.01;
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


                AttackTimer = DateTime.Now;

                DoubleAnimation attackAnimation = new DoubleAnimation();
                attackAnimation.From = 0;  
                attackAnimation.To = 30;
                attackAnimation.Duration = TimeSpan.FromMilliseconds(250);
                attackAnimation.AutoReverse = true;

                WeaponAngle.BeginAnimation(AxisAngleRotation3D.AngleProperty, attackAnimation);


            }
        }




        private bool lastBoss = false;


        private Border CreateChestInfo(string name, double attack = -1, double armor = -1)
        {
            Border chestInfo = new Border();
            chestInfo.VerticalAlignment = VerticalAlignment.Center;
            chestInfo.HorizontalAlignment = HorizontalAlignment.Left;
            chestInfo.Background = new SolidColorBrush(Color.FromArgb(178, 0, 0, 0));
            chestInfo.CornerRadius = new CornerRadius(10);
            chestInfo.Padding = new Thickness(10, 5, 10, 5);
            chestInfo.Margin = new Thickness(20, 0, 0, 0);
            chestInfo.Visibility = Visibility.Collapsed;
            chestInfo.Width = 250;

            StackPanel stackPanel = new StackPanel();
            stackPanel.VerticalAlignment = VerticalAlignment.Center;

            TextBlock nameText = new TextBlock();
            nameText.Text = $"Название: {name}";
            nameText.Foreground = Brushes.White;
            nameText.FontSize = 6;
            nameText.VerticalAlignment = VerticalAlignment.Center;
            nameText.Margin = new Thickness(0, 5, 0, 5);
            nameText.TextWrapping = TextWrapping.Wrap;

            stackPanel.Children.Add(nameText);
            if (attack != -1 || armor != -1) { 
                TextBlock damageText = new TextBlock();
                if (attack == -1) { damageText.Text = $"Защита: {armor}"; }
                else
                    damageText.Text = $"Урон: {attack}";
                damageText.Foreground = Brushes.White;
                damageText.FontSize = 6;
                damageText.VerticalAlignment = VerticalAlignment.Center;
                damageText.Margin = new Thickness(0, 5, 0, 5);

                stackPanel.Children.Add(damageText);

            }

            chestInfo.Child = stackPanel;

            return chestInfo;
        }
            
        public void GenerateObjectBox()
        {
            var resSelectItem = boxSpawn.RandomSelectItem();

            var item = boxSpawn.CreateObjectModel(resSelectItem == 0, boxSpawn.selectItem.pathImg, resSelectItem==2);
            Viewport.Children.Add(item);



            Border ChestInfo;
            if (boxSpawn.selectItem is Weapon weapon)
            {
                ChestInfo = CreateChestInfo(weapon.Name, weapon.Attack);
            }
            else if (boxSpawn.selectItem is Armor armor)
            {
                ChestInfo = CreateChestInfo(armor.Name, armor:armor.ArmorHP);

            }
            else
            {
                ChestInfo = CreateChestInfo(boxSpawn.selectItem.Name);
                ChestHelperCtrl.Visibility = Visibility.Visible;
            }

            MainGrid.Children.Add(ChestInfo);

            item.MouseEnter += (o, e) =>
                {
                    ChestInfo.Visibility = Visibility.Visible;
                };
            item.MouseLeave += (o, e) => ChestInfo.Visibility = Visibility.Collapsed;


            TranslateTransform3D transform = item.Transform as TranslateTransform3D ;
            var anim = new DoubleAnimation();
            anim.From = transform.Value.OffsetY;
            anim.To = transform.Value.OffsetY+.1;
            anim.Duration = TimeSpan.FromMilliseconds(800);
            anim.AutoReverse = true;
            anim.RepeatBehavior = RepeatBehavior.Forever;

            transform.BeginAnimation(TranslateTransform3D.OffsetYProperty, anim);

        }

        public void GenerateBox()
        {
            var mod = boxSpawn.Box;
            mod.MouseDown += Model3DBox_MouseDown;


            Viewport.Children.Add(mod);

            //GenerateObjectBox();

        }

        public void NewLevel()
        {
            Console.WriteLine("\nStep = {0}\n", WorkGame.Game.step);
            if (WorkGame.Game.step == 1)
            {
                GenerateEnemies();
                return;
            }

            if (!WorkGame.Game.IsGameOrChest)
            {
                GenerateBox();

                PointAnimCamer(new Point3D(2.5, camera.Position.Y, -7));
                //posAnim.EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };

                return;

            }

            string text = "LEVEL UP!";
            bool boss = false;

            if (WorkGame.Game.BossMoment)
            {
                GenerateBoss();
                text = "BOSS";
                boss = true;
            }
                LevelUP.Visibility = Visibility.Visible;
                txtLevel.Text = text;
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 0;
                anim.To = 1;
                anim.Duration = TimeSpan.FromMilliseconds(500);
                anim.AutoReverse = true;

                anim.Completed += (e, s) =>
                {

                    LevelUP.Visibility = Visibility.Collapsed;
                    if (!boss)
                    {
                        GenerateEnemies();
                        //WorkGame.Game.step++;
                    }
                };  
                LevelUP.BeginAnimation(OpacityProperty, anim);

        }

        public void WindowWinBoss()
        {
            GenerateEnemies();

        }
            
        private bool IsClicking => (DateTime.Now - AttackTimer).TotalMilliseconds < 500;
        private bool IsAttackingEnemy => (DateTime.Now - EnemyAttackTime).TotalMilliseconds > 1000;
        public void Model3DBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isMouseCaptured) return;
            //if (isAttacking) return;
            if (IsClicking) return;
            Viewport.Children.Remove(boxSpawn.Box);
            GenerateObjectBox();

            ChestHelper.Visibility = Visibility.Visible;
            var animHelper = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500)
            };
            ChestHelper.BeginAnimation(OpacityProperty, animHelper);
            boxSpawn.Box = null;
        }

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
            (Enemy enem, double attack) lst = WorkGame.Game.Attack(mod);

            double attack = lst.attack;
            Enemy enem = lst.enem;

            Viewport2DVisual3D text = GeneratedClass.GetViewport2DText(transform, attack, WorkGame.Game.isBoss);
            Viewport.Children.Add(text);

            Border borderLogir;
            if (attack == 0)
            {
                //textLog = $"Player kill {enem.Name}";
                borderLogir = GeneratedClass.LogirText("Player", enem.Name, attack, true, Text:"kill");
            }
            else
            {
                //textLog = $"Player: {attack} -> Enemy: {enem.Name}";
                borderLogir = GeneratedClass.LogirText($"Player", enem.Name, attack, false);
                enem.AnimPoluchEnemyAndBoss();
            }

            Console.WriteLine($"Player: {attack} -> Enemy: {enem.Name}");
            stackLogir.Children.Add(borderLogir);

            var animLogirText = new DoubleAnimation();
            animLogirText.From = 0;
            animLogirText.To = 1;
            animLogirText.Duration = TimeSpan.FromMilliseconds(600);

            animLogirText.Completed += (s, ea) =>
            {
                //var EndanimLogirText = new DoubleAnimation();
                animLogirText.From = 1;
                animLogirText.To = 0;
                animLogirText.Duration = TimeSpan.FromMilliseconds(1000);
                animLogirText.Completed += (s_end, aa) =>
                {
                    stackLogir.Children.Remove(borderLogir);

                };
                borderLogir.BeginAnimation(OpacityProperty, animLogirText);

            };

            borderLogir.BeginAnimation(OpacityProperty, animLogirText);

 

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
                    WorkGame.Game.Score += 10;
                    WorkGame.Game.DeleteBoss();
                    //var CursOver = Mouse.OverrideCursor;
                    //Mouse.OverrideCursor = null;

                    //var windRes = new ResulLevelWindow("win");
                    //windRes.Owner = this;
                    //var result = windRes.ShowDialog();
                    //if (result == false)
                    //{
                    //    Mouse.OverrideCursor = CursOver;

                    //}
                    WindWinOrAgain("win");

                }
                else
                {
                    WorkGame.Game.Score += 3;

                    WorkGame.Game.DeleteEnemis(mod);
                }
                Viewport.Children.Remove(mod);
                if (WorkGame.Game.CountEnemies == 0 && !WorkGame.Game.isBoss)
                {
                    WorkGame.Game.step++;
                    NewLevel();
                }
            }
        }

        public void GetItemPlayer(int typeItem, ItemChest item)
        {
            switch (typeItem)
            {
                case 0:
                    {
                        WorkGame.Game.ChangPlayer(item);
                        //DiffuseMaterial colors_material = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(item.pathImg))));
                        //HandModel.Material = colors_material;
                        break;
                    }
                case 1:
                    {
                        WorkGame.Game.ChangPlayer(item);

                        break;
                    }
                case 2:
                    {
                        WorkGame.Game.ChangPlayer(item);
                        break;
                    }
            }
        }

        public void PointAnimCamer(Point3D point)
        {

            var posAnim = new Point3DAnimation();
            posAnim.From = camera.Position;
            posAnim.To = point;
            posAnim.Duration = TimeSpan.FromSeconds(1);

            camera.BeginAnimation(PerspectiveCamera.PositionProperty, posAnim);
        }

        public void TakeItem()
        {
            GetItemPlayer(boxSpawn.IntTypeItem, boxSpawn.selectItem);
            Viewport.Children.Remove(boxSpawn.ItemModel);
            boxSpawn.OpensBoxClos();
            ChestHelper.Visibility = Visibility.Collapsed;
            ChestHelperCtrl.Visibility = Visibility.Collapsed;

            PointAnimCamer(new Point3D(1.5, camera.Position.Y, -2));
            NewLevel();
        }
        public void IgnoreItem()
        { 
            Viewport.Children.Remove(boxSpawn.ItemModel);
            boxSpawn.OpensBoxClos();
            ChestHelper.Visibility = Visibility.Collapsed;
            ChestHelperCtrl.Visibility = Visibility.Collapsed;

            PointAnimCamer(new Point3D(1.5, camera.Position.Y, -2));
            NewLevel();
        }
        public void UseItem()
        {
            WorkGame.Game.Player.HP = 100;
            Viewport.Children.Remove(boxSpawn.ItemModel);
            boxSpawn.OpensBoxClos();
            ChestHelper.Visibility = Visibility.Collapsed;
            ChestHelperCtrl.Visibility = Visibility.Collapsed;

            PointAnimCamer(new Point3D(1.5, camera.Position.Y, -2));
            NewLevel();
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


            if (e.Key == Key.Enter && boxSpawn.boxIsOpen)
            {
                TakeItem();
            }

            if ((e.Key == Key.LeftShift || e.Key == Key.RightShift) && boxSpawn.boxIsOpen)
            {
                IgnoreItem();
            }
            if ((e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)  && boxSpawn.boxIsOpen && ChestHelperCtrl.Visibility == Visibility.Visible)
            {
                UseItem();
            }
            else if ((e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl) && WorkGame.Game.Player.CountPotionHP > 0)
            {
                //UseItem();
                WorkGame.Game.UsePotionPlayerHP();
            }

        }


        private void Viewport_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseCaptured)
            {

                var animX = new DoubleAnimation();
                animX.From = 1.5;
                animX.To = 0.5;
                animX.Duration = TimeSpan.FromMilliseconds(300);

                var animY = new DoubleAnimation();
                animY.From = -0.6;
                animY.To = -0.5;
                animY.Duration = TimeSpan.FromMilliseconds(300);

                animX.Completed += (es, sd) => WorkGame.Game.Player.IsBlock = true;
                ZashitaTrans.BeginAnimation(TranslateTransform3D.OffsetXProperty, animX);
                ZashitaTrans.BeginAnimation(TranslateTransform3D.OffsetYProperty, animY);
            }

        }

        private void Viewport_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!IsMouseCaptured)
            {

                var animX = new DoubleAnimation();
                animX.From = 0.5;
                animX.To = 1.5;
                animX.Duration = TimeSpan.FromMilliseconds(300);
                animX.Completed += (es, sd) => WorkGame.Game.Player.IsBlock = false;

                var animY = new DoubleAnimation();
                animY.From = -0.5;
                animY.To = -0.6;
                animY.Duration = TimeSpan.FromMilliseconds(300);

                ZashitaTrans.BeginAnimation(TranslateTransform3D.OffsetXProperty, animX);
                ZashitaTrans.BeginAnimation(TranslateTransform3D.OffsetYProperty, animY);

            }
        }
    }
}