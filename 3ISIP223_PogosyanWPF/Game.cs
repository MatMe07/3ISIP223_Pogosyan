using _3ISIP223_PogosyanWPF.Model;
using _3ISIP223_PogosyanWPF.Model.Creater;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF
{
    public class Game : INotifyPropertyChanged
    {
        public bool ChoiceChestOrEnemy => RandomCLS.Next(0, 100) >= 50;

        private Player _player;

        public Player Player
        {
            get { return _player; }
            set {
                _player = value; 
                OnPropertyChanged(nameof(Player));
                }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private EnemyCreater _vrags = null;
        public EnemyCreater Vrags
        {
            get { return _vrags; }
            set
            {
                _vrags = value;
                OnPropertyChanged(nameof(Vrags));
            }
        }

        private ObservableCollection<Enemy> _enemies;
        public List<(Enemy, ModelUIElement3D)> EnemiesAA;
        
        public ObservableCollection<Enemy> Enemies
        {
            get { return _enemies; }
            set
            {
                _enemies = value;
                OnPropertyChanged(nameof(Enemies));
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public Enemy Vrag = null;
        //public Enemy Boss = null;
        //public EnemyCreater vrags = null;
        //public bool HaveEnemy => Vrag != null;
        private int _step;
        public int step
        {
            get { return _step; }
            set
            {
                _step = value;
                OnPropertyChanged(nameof(step));
            }
        }
        public int StepSpendWinOverBoss { get; set; }
        public double HPLostWinOverBoss { get; set; }
        public bool GameOver { get; set; }
        //public bool BosseStumles => true;
        public bool BosseStumles => step % 10 == 0;

        public Game()
        {
            string name = "Player1";
            Player = new Player(name);
            Vrags = new EnemyCreater();
            Enemies = new ObservableCollection<Enemy>();
            EnemiesAA = new List<(Enemy, ModelUIElement3D)>();
            ModelUIElement3D mod = new ModelUIElement3D();
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(Point3D.Parse("1, 0, -4"));
            mesh.Positions.Add(Point3D.Parse("1, 0.4, -4"));
            mesh.Positions.Add(Point3D.Parse("1.3, 0, -4"));
            mesh.Positions.Add(Point3D.Parse("1.3, 0.4, -4"));

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(2);

            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);
            mesh.TriangleIndices.Add(3);
            mod.Model = new GeometryModel3D(mesh, new DiffuseMaterial(new SolidColorBrush(Colors.Red)));
            EnemiesAA.Add(
                (
                    Vrags.Enemies[0].CreateEnemy(),
                    mod
                )
                );
            step = 1;
            GameOver = false;
            StepSpendWinOverBoss = 0;
            HPLostWinOverBoss = 0;
            //vrags = new EnemyCreater();
        }



    }


    public static class WorkGame
    {
        private static Game game = new Game();

        public static Game Game { get { return game; } }
    }
}
