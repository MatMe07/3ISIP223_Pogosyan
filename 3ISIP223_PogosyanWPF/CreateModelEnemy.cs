using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    public static class CreateModelEnemy
    {
        public static ModelUIElement3D CreateModel(Color color,double x = 0, double y = 0, double z = 0, string imagePath = "", bool isBoss = false)
        {
            var model = new ModelUIElement3D();
            MeshGeometry3D mesh = new MeshGeometry3D();
            if (isBoss)
            {
                mesh.Positions.Add(Point3D.Parse("0.8, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("0.8, 0.4, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0.4, -4"));
            }
            else
            {
                mesh.Positions.Add(Point3D.Parse("1, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("1, 0.4, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0, -4"));
                mesh.Positions.Add(Point3D.Parse("1.3, 0.4, -4"));
            }


            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            model.Model = new GeometryModel3D(mesh, new DiffuseMaterial(new SolidColorBrush(color)));
            

            model.Transform = new TranslateTransform3D() { OffsetZ = z, OffsetX = x, OffsetY = y };


            return model;
        }
    }
}
