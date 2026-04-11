using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    static class GeneratedClass
    {

        public static Viewport2DVisual3D GetViewport2DText(TranslateTransform3D transform, double num, bool isBosse = false)
        {
            Viewport2DVisual3D viewport2D = new Viewport2DVisual3D();
            TranslateTransform3D translate = new TranslateTransform3D(transform.OffsetX + (isBosse ? 1 : 1.05), .65, transform.OffsetZ);
            MeshGeometry3D mesh = new MeshGeometry3D();

            int lenNum = num.ToString().Length;

            double xP = lenNum * 0.05;
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

        private static Dictionary<string, BitmapImage> iconSim = new Dictionary<string, BitmapImage> {
            {"заморозил", new BitmapImage(new Uri("pack://application:,,,/Icons/AttackSymbols/zamorozil_mag.png")) },
            {"(_)", new BitmapImage(new Uri("pack://application:,,,/Icons/AttackSymbols/udar2.png")) },
            {"kill", new BitmapImage(new Uri("pack://application:,,,/Icons/AttackSymbols/Killicon_backstab.png")) },
            {"shield", new BitmapImage(new Uri("pack://application:,,,/Icons/AttackSymbols/shield.png")) },
            {"broken-shield", new BitmapImage(new Uri("pack://application:,,,/Icons/AttackSymbols/broken-shield.png")) },
            {"doubleUdar", new BitmapImage(new Uri("pack://application:,,,/Icons/AttackSymbols/doubleUdar.png")) },
        };

        public static Border LogirText(string player, string enem, double attack, bool killed, bool isEnem = false, string Text = "(_)", bool isBlock = false, int EnemKrit = 0)
        {
            if (isBlock) Text = "shield";

            switch (EnemKrit)
            {
                case -1:
                    {
                        Text = "заморозил";
                        break;
                    }
                case -2:
                    {
                        Text = "doubleUdar";
                        break;
                    }
                case -3:
                    {
                        Text = "broken-shield";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

                Border border = new Border();
            border.Background = (Brush)(new BrushConverter().ConvertFromString("#33000000"));
            border.Padding = new Thickness(10, 2, 10, 2);
            border.Margin = new Thickness(5);
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = (Brush)(new BrushConverter().ConvertFromString("#7FFF0000"));
            border.HorizontalAlignment = HorizontalAlignment.Right;

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            if (attack != -1)
            {
                if (isEnem) enem += $": {attack}";
                else
                {
                    player += $": {attack}";
                }
            }



            TextBlock textBlockPlayer = new TextBlock()
                {
                    Text = player,
                    FontSize = 10,
                    VerticalAlignment = VerticalAlignment.Center,
                    Foreground = (Brush)(new BrushConverter().ConvertFromString("#FF8C4D0E"))
                };

            Image imageCenterBlock = new Image()
            {
                Source = iconSim[Text],
                Width = 20,
                Margin = new Thickness(5, 0, 5, 0),

            };
            TextBlock textBlockCenter = new TextBlock()
            {
                Text = Text,
                FontSize = 20,
                Margin = new Thickness(5, 0, 5, 0),
                Foreground = (Brush)(new BrushConverter().ConvertFromString("#FFADA8A3"))
            };

            TextBlock textBlockEnem = new TextBlock()
            {
                Text = enem,
                FontSize = 10,
                VerticalAlignment = VerticalAlignment.Center,
                Foreground = (Brush)(new BrushConverter().ConvertFromString("#FF7A94BF"))
            };

            //if (killed && isEnem)
            //{
            //    stack.Children.Add(textBlockEnem);
            //}
            //else if (killed && !isEnem)
            //{
            //    stack.Children.Add(textBlockPlayer);
            //}
            if (isEnem)
            {
                stack.Children.Add(textBlockEnem);
                stack.Children.Add(imageCenterBlock);
                stack.Children.Add(textBlockPlayer);
            }
            else
            {
                stack.Children.Add(textBlockPlayer);
                stack.Children.Add(imageCenterBlock);
                stack.Children.Add(textBlockEnem);

            }

            border.Child = stack;



            return border;
        }

    }
}
