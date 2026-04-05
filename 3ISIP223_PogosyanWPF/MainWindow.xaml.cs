using _3ISIP223_PogosyanWPF.Model;
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
    public partial class MainWindow
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
        public DateTime EnemyAttackTime;

        public BoxSpawn boxSpawn;
        //private bool isAttacking = false;
        public static StackPanel LogerPanel {  get; set; }
        public static Grid FrozenPanel {  get; set; }

        public MainWindow()
        {
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

            //Border bo = LogirText("gekk");
            //stackLogir.Children.Add(bo);
            LogerPanel = stackLogir;
            FrozenPanel = FrozGrid;
            NewLevel();

        }




        public void GenerateBoss()
        {
            ModelUIElement3D mod;
            mod = CreateModelEnemy.CreateModel(Colors.Gray, 0.5, 0, -1, isBoss:true);
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
            
            for (double i = -.5; i <= 3; i+=1.5)
            {

                mod = CreateModelEnemy.CreateModel(Colors.Gray, i, 0, 2);

                //DoubleAnimation attackAnim1 = new DoubleAnimation();
                //attackAnim1.To = 0;
                //attackAnim1.From = 0;


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
            //Console.WriteLine((DateTime.Now - EnemyAttackTime).TotalMilliseconds);
            //if (IsAttackingEnemy)
            //{
            //    EnemyAttack();
            //    EnemyAttackTime = DateTime.Now;
            //}

            WorkGame.Game.AttackEnemy();


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

            var item = boxSpawn.CreateObjectModel(resSelectItem == 0, boxSpawn.selectItem.pathImg);
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
            if (WorkGame.Game.step == 4)
            {
                GenerateEnemies();
                WorkGame.Game.step++;
                return;
            }

            if (!WorkGame.Game.IsGameOrChest || true)
            {
                GenerateBox();

                PointAnimCamer(new Point3D(2.5, camera.Position.Y, -7));
                //posAnim.EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };

                return;

            }

            string text = "LEVEL UP!";
            bool boss = false;

            if (WorkGame.Game.step == 2)
            {
                GenerateBoss();
                WorkGame.Game.step++;
                text = "BOSS";
                boss = true;
                //return;
            }

            if (!lastBoss)
            {
                LevelUP.Visibility = Visibility.Visible;
                txtLevel.Text = text;
                DoubleAnimation anim = new DoubleAnimation();
                anim.From = 0;
                anim.To = 1;
                anim.Duration = TimeSpan.FromMilliseconds(350);
                anim.AutoReverse = true;

                anim.Completed += (e, s) =>
                {

                    LevelUP.Visibility = Visibility.Collapsed;
                    if (!boss)
                    {
                        GenerateEnemies();
                        WorkGame.Game.step++;
                    }
                };  
                LevelUP.BeginAnimation(OpacityProperty, anim);
            }
            else
            {
                Console.WriteLine("BOSSSS закончен!!!!!!!");

                lastBoss = false;
                //WindowWinBoss();
            }

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
            string textLog = "";

            if (attack == 0)
            {
                //textLog = $"Player kill {enem.Name}";
                borderLogir = GeneratedClass.LogirText("Player", enem.Name, attack, true, Text:"kill");
            }
            else
            {
                //textLog = $"Player: {attack} -> Enemy: {enem.Name}";
                borderLogir = GeneratedClass.LogirText($"Player", enem.Name, attack, false);
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

        public void GetItemPlayer(int typeItem, ItemChest item)
        {
            switch (typeItem)
            {
                case 0:
                    {
                        WorkGame.Game.ChangPlayer(item);
                        DiffuseMaterial colors_material = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(item.pathImg))));
                        HandModel.Material = colors_material;
                        break;
                    }
                case 1:
                    {
                        WorkGame.Game.ChangPlayer(item);

                        break;
                    }
                case 2:
                    {
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

            PointAnimCamer(new Point3D(1, camera.Position.Y, -1));
            NewLevel();
        }
        public void IgnoreItem()
        {
            GetItemPlayer(boxSpawn.IntTypeItem, boxSpawn.selectItem);
            Viewport.Children.Remove(boxSpawn.ItemModel);
            boxSpawn.OpensBoxClos();
            ChestHelper.Visibility = Visibility.Collapsed;

            PointAnimCamer(new Point3D(1, camera.Position.Y, -1));
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

            if (e.Key == Key.LeftShift && boxSpawn.boxIsOpen)
            {
                IgnoreItem();
            }

        }
    }
}