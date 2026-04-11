using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    public static class CreateModelEnemy
    {
        public static MeshGeometry3D CreateModel(Color color,double x = 0, double y = 0, double z = 0, string imagePath = "", bool isBoss = false)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();

            if (isBoss)
            {
                mesh.Positions.Add(Point3D.Parse("0.6, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("0.6, 0.55, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0.55, -4"));

                mesh.Positions.Add(Point3D.Parse("0.95, 0, -4.2"));
                mesh.Positions.Add(Point3D.Parse("0.95, 0.55, -4.2"));
                mesh.Positions.Add(Point3D.Parse("0.95, 0, -3.8"));
                mesh.Positions.Add(Point3D.Parse("0.95, 0.55, -3.8"));
            }
            else
            {
                mesh.Positions.Add(Point3D.Parse("0.8, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("0.8, 0.5, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0.5, -4"));

                mesh.Positions.Add(Point3D.Parse("1.05, 0, -4.2"));
                mesh.Positions.Add(Point3D.Parse("1.05, 0.5, -4.2"));
                mesh.Positions.Add(Point3D.Parse("1.05, 0, -3.8"));
                mesh.Positions.Add(Point3D.Parse("1.05, 0.5, -3.8"));
            }
            mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(0); mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(1); mesh.TriangleIndices.Add(2); mesh.TriangleIndices.Add(3);

            mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(4); mesh.TriangleIndices.Add(6);
            mesh.TriangleIndices.Add(5); mesh.TriangleIndices.Add(6); mesh.TriangleIndices.Add(7);

            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));

            mesh.TextureCoordinates.Add(new Point(0, 1));
            mesh.TextureCoordinates.Add(new Point(0, 0));
            mesh.TextureCoordinates.Add(new Point(1, 1));
            mesh.TextureCoordinates.Add(new Point(1, 0));
            return mesh;
        }
    }
}
