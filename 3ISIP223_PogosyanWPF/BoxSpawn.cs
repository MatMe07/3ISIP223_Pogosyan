using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    public class BoxSpawn
    {


        private ModelUIElement3D _box = null;
        public ModelUIElement3D Box
        {
            get { 
                if (_box == null)
                {
                    _box = CreateBoxModel();
                }
                return _box;
            }
        }

        public ModelUIElement3D CreateBoxModel()
        {
            var model = new ModelUIElement3D();
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(Point3D.Parse("-0.2, -0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, -0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("-0.2, 0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, 0.3, -0.2"));


            mesh.Positions.Add(Point3D.Parse("-0.2, -0.3, 0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, -0.3, 0.2"));
            mesh.Positions.Add(Point3D.Parse("-0.2, 0.3, 0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, 0.3, 0.2"));


            mesh.Positions.Add(Point3D.Parse("-0.2, -0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("-0.2, 0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("-0.2, -0.3, 0.2"));
            mesh.Positions.Add(Point3D.Parse("-0.2, 0.3, 0.2"));


            mesh.Positions.Add(Point3D.Parse("0.2, -0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, 0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, -0.3, 0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, 0.3, 0.2"));


            mesh.Positions.Add(Point3D.Parse("-0.2, -0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("-0.2, -0.3, 0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, -0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, -0.3, 0.2"));


            mesh.Positions.Add(Point3D.Parse("-0.2, 0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("-0.2, 0.3, 0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, 0.3, -0.2"));
            mesh.Positions.Add(Point3D.Parse("0.2, 0.3, 0.2"));





            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(1);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);

            mesh.TriangleIndices.Add(4);
            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(6);

            mesh.TriangleIndices.Add(5);
            mesh.TriangleIndices.Add(7);
            mesh.TriangleIndices.Add(6);

            mesh.TriangleIndices.Add(8);
            mesh.TriangleIndices.Add(10);
            mesh.TriangleIndices.Add(9);

            mesh.TriangleIndices.Add(9);
            mesh.TriangleIndices.Add(10);
            mesh.TriangleIndices.Add(11);

            mesh.TriangleIndices.Add(12);
            mesh.TriangleIndices.Add(13);
            mesh.TriangleIndices.Add(14);

            mesh.TriangleIndices.Add(13);
            mesh.TriangleIndices.Add(15);
            mesh.TriangleIndices.Add(14);

            mesh.TriangleIndices.Add(16);
            mesh.TriangleIndices.Add(17);
            mesh.TriangleIndices.Add(18);

            mesh.TriangleIndices.Add(17);
            mesh.TriangleIndices.Add(19);
            mesh.TriangleIndices.Add(18);

            mesh.TriangleIndices.Add(20);
            mesh.TriangleIndices.Add(21);
            mesh.TriangleIndices.Add(22);

            mesh.TriangleIndices.Add(21);
            mesh.TriangleIndices.Add(23);
            mesh.TriangleIndices.Add(22);

            //0,1  0,0  1,1  1,0
            //mesh.TextureCoordinates.Add(new System.Windows.Point(0, 1));
            //mesh.TextureCoordinates.Add(new System.Windows.Point(0, 0));
            //mesh.TextureCoordinates.Add(new System.Windows.Point(1, 1));
            //mesh.TextureCoordinates.Add(new System.Windows.Point(1, 0));

            //var geom = new GeometryModel3D(mesh, new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Icons/Armor.png")))));
            //model.Model = geom;
            model.Model = new GeometryModel3D(mesh, new DiffuseMaterial(Brushes.Brown));


            model.Transform = new TranslateTransform3D() { OffsetZ = -1, OffsetX = 1.5 };


            return model;
        }

        

    }
}
