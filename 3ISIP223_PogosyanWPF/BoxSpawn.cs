using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
                    //boxIsOpen = true;
                }
                return _box;
            }
            set { 
                _box = value;
                boxIsOpen = false;
            }
        }
        public bool boxIsOpen {  get; set; }
        public List<Weapon> weaponsLst {  get; set; }
        public List<Armor> armorsLst {  get; set; }
        public ItemChest ZelebZele {  get; set; }
        public BoxSpawn()
        {
            weaponsLst = new List<Weapon>()
            {
                new Weapon("Меч", 20, "pack://application:,,,/Images/weapons/swordUs.png")
            };
            armorsLst = new List<Armor>()
            {
                new Armor("Доспех", 10, "pack://application:,,,/Icons/Armor.png")
            };

            ZelebZele = new ItemChest("Целебное зелье", "pack://application:,,,/Icons/armorInvent.png");
        }

        public (ItemChest item, int SelectItem) RandomSelectItem()
        {
            boxIsOpen = true;

            ItemChest path = null;
            int select = RandomCLS.Next(0, 2);
            switch(select)
            {
                case 0:
                    {
                        path = weaponsLst[RandomCLS.Next(0, weaponsLst.Count)];

                        break;
                    }
                case 1:
                    {
                        path = armorsLst[RandomCLS.Next(0, armorsLst.Count)];
                        break;
                    }
                case 2:
                    {
                        path = ZelebZele;
                        break;
                    }
            }

            return (path, select);
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


            model.Transform = new TranslateTransform3D() { OffsetZ = -9, OffsetX = 3 };


            return model;
        }


        public ModelUIElement3D CreateObjectModel(bool isWeapon, string path = "pack://application:,,,/weapons/swordUs.png")
        {
            ModelUIElement3D model = new ModelUIElement3D();

            GeometryModel3D geometry = new GeometryModel3D();

            DiffuseMaterial material = new DiffuseMaterial();
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = new BitmapImage(new Uri(path));
            material.Brush = brush;
            geometry.Material = material;

            MeshGeometry3D mesh = new MeshGeometry3D();
            if (isWeapon)
            {
                mesh.Positions = new Point3DCollection
                {
                    new Point3D(-0.2, 0, 0),
                    new Point3D(-0.2, 0.3, 0),
                    new Point3D(-0.1, 0, 0),
                    new Point3D(-0.1, 0.3, 0)
                };

            }
            else
            {
                mesh.Positions = new Point3DCollection
                {
                    new Point3D(-0.3, 0, 0),
                    new Point3D(-0.3, 0.3, 0),
                    new Point3D(-0.1, 0, 0),
                    new Point3D(-0.1, 0.3, 0)
                };
            }

            mesh.TriangleIndices = new Int32Collection
            {
                1, 0, 2,
                1, 2, 3
            };

            mesh.TextureCoordinates = new PointCollection
            {
                new Point(0, 1),
                new Point(0, 0),
                new Point(1, 1),
                new Point(1, 0)
            };

            geometry.Geometry = mesh;
            model.Model = geometry;

            TranslateTransform3D transform = new TranslateTransform3D();
            transform.OffsetZ = -9;
            transform.OffsetX = 3.2;
            model.Transform = transform;

            return model;
        }


    }
}
