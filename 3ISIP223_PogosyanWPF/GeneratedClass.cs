using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    static class GeneratedClass
    {

        public static Viewport2DVisual3D GetViewport2DText(TranslateTransform3D transform, double num, bool isBosse = false)
        {
            Viewport2DVisual3D viewport2D = new Viewport2DVisual3D();
            TranslateTransform3D translate = new TranslateTransform3D(transform.OffsetX + (isBosse ? 1.05 : 1.15), .5, transform.OffsetZ);
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



        public static Border LogirText(string player, string enem, bool killed, bool isEnem = false)
        {
            
            Border border = new Border();
            border.Background = (Brush)(new BrushConverter().ConvertFromString("#33000000"));
            border.Padding = new Thickness(10, 2, 10, 2);
            border.Margin = new Thickness(5);
            border.BorderThickness = new Thickness(1);
            border.BorderBrush = (Brush)(new BrushConverter().ConvertFromString("#7FFF0000"));
            border.HorizontalAlignment = HorizontalAlignment.Right;

            StackPanel stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;


            TextBlock textBlockPlayer = new TextBlock()
            {
                Text = player,
                FontSize = 10,
                Foreground = (Brush)(new BrushConverter().ConvertFromString("#FF8C4D0E"))
            };

            TextBlock textBlockCenter = new TextBlock()
            {
                Text = "(_)",
                FontSize = 10,
                Margin = new Thickness(5, 0, 5, 0),
                Foreground = (Brush)(new BrushConverter().ConvertFromString("#FFADA8A3"))
            };

            TextBlock textBlockEnem = new TextBlock()
            {
                Text = enem,
                FontSize = 10,
                Foreground = (Brush)(new BrushConverter().ConvertFromString("#FF7A94BF"))
            };

            if (killed && isEnem)
            {
                stack.Children.Add(textBlockEnem);
            }
            else if (killed && !isEnem)
            {
                stack.Children.Add(textBlockPlayer);
            }
            else if (isEnem)
            {
                stack.Children.Add(textBlockEnem);
                stack.Children.Add(textBlockCenter);
                stack.Children.Add(textBlockPlayer);

            }
            else
            {
                stack.Children.Add(textBlockPlayer);
                stack.Children.Add(textBlockCenter);
                stack.Children.Add(textBlockEnem);

            }

            border.Child = stack;



            return border;
        }

    }
}
