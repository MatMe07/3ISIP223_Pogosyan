using _3ISIP223_PogosyanWPF.Model;
using _3ISIP223_PogosyanWPF.Model.Creater;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
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

        //private ObservableCollection<Enemy> _enemies;
        //public List<(Enemy enemy, ModelUIElement3D model)> EnemiesAA;
        
        public List<Enemy> Enemies { get; set; }

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
            Enemies = new List<Enemy>();
            //EnemiesAA = new List<(Enemy, ModelUIElement3D)>();

            step = 1;
            GameOver = false;
            StepSpendWinOverBoss = 0;
            HPLostWinOverBoss = 0;
            //vrags = new EnemyCreater();
        }
        public void AddEnemis(ModelUIElement3D model)
        {
            Enemies.Add(Vrags.Enemies[0].CreateEnemy(model));
        }




        public double Attack(ModelUIElement3D model)
        {
            Enemy vrag = Enemies.FirstOrDefault(s=>s.model == model);
            double attack = Math.Max(0, Player.AttackWeapon - 2 - vrag.Defense);
            if (attack > vrag.HP) attack = vrag.HP;
            attack = Math.Round(attack, 2);
            vrag.HP -= attack;
            Console.WriteLine($"Вы наносите удар! {vrag.Name} получает {attack} урона!");
            //if (Boss != null) { StepSpendWinOverBoss++; }
            if (!vrag.IsAlive)
            {
                vrag.HP = 0;
                vrag.LastWord();
                //if (Boss != null)
                //{
                //    Thread.Sleep(1100);
                //    player.CountAttackBosses++;
                //    WinOverBoss();
                //}
                //else
                //{
                //    player.CountAttackEnemies++;
                //}
                vrag = null;
            }
            return attack;
            //Console.ReadLine();
            //Console.Clear();
            //InforEnemAndPlayer(vrag);
        }


    }


    public static class WorkGame
    {
        private static Game game = new Game();

        public static Game Game { get { return game; } }
    }
}
