using _3ISIP223_PogosyanWPF.Model;
using _3ISIP223_PogosyanWPF.Model.Creater;
using _3ISIP223_PogosyanWPF.Model.Units;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
//using System.Windows;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    public class Game : INotifyPropertyChanged
    {
        public bool ChoiceChestOrEnemy => RandomCLS.Next(0, 100) >= 50;

        private Player _player;

        private int _score = 0;

        public int Score
        {
            get { return _score; }
            set {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public bool IsPause { get; set; } = false;

        public Player Player
        {
            get { return _player; }
            set {
                _player = value; 
                OnPropertyChanged(nameof(Player));
                }
        }
        public Player GetPlayer() => Player;

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

        public int CountEnemies => Enemies.Count;

        //private ObservableCollection<Enemy> _enemies;
        //public List<(Enemy enemy, ModelUIElement3D model)> EnemiesAA;

        public List<Enemy> Enemies { get; set; }

        //public 

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public Enemy Vrag = null;
        public Enemy Boss = null;
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
        //public bool GameOver { get; set; }
        public bool isBoss { get; set; }
        //public bool BosseStumles => true;
        public bool BosseStumles => step % 10 == 0;

        public Dictionary<Enemy, DateTime> attackingEnemies;

        private DispatcherTimer atEnemTimer;
        private DispatcherTimer timerAttackEnemy;


        public bool IsGameOrChest => RandomCLS.Next(0, 100) > 50;

        public Action<string> GameOver {  get; set; }
        public Game()
        {
            string name = "Player1";
            Player = new Player(name);
            Vrags = new EnemyCreater();
            Enemies = new List<Enemy>();
            //EnemiesAA = new List<(Enemy, ModelUIElement3D)>();
            isBoss = false;
            step = 1;
            //GameOver = false;
            StepSpendWinOverBoss = 0;
            HPLostWinOverBoss = 0;
            //vrags = new EnemyCreater();
            attackingEnemies = new Dictionary<Enemy, DateTime>();


            atEnemTimer = new DispatcherTimer();
            atEnemTimer.Interval = TimeSpan.FromMilliseconds(16);
            //atEnemTimer.Tick += AtEnemTimer_Tick;
            //atEnemTimer.Start();

            timerAttackEnemy = new DispatcherTimer();
            timerAttackEnemy.Interval = TimeSpan.FromMilliseconds(100);
            //timerAttackEnemy.Tick += (s, e) =>
            //{
            //    AttackEnemy();
            //};
            //timerAttackEnemy.Start();

        }

        public void AddEnemis(ModelUIElement3D model)
        {
            Enemies.Add(Vrags.Enemies[RandomCLS.Next(0, Vrags.Enemies.Count)].CreateEnemy(model));
        }
        public void SelectBoss(ModelUIElement3D model)
        {
            Boss = Vrags.Bosses[RandomCLS.Next(0, Vrags.Bosses.Count)].CreateEnemy(model);
            isBoss = true;
        }

        public void DeleteEnemis(ModelUIElement3D model)
        {
            Enemy en = Enemies.FirstOrDefault(a=>a.model == model);
            Enemies.Remove(en);
        }
        public void DeleteBoss()
        {
            Boss = null;
            isBoss = false;
        }



        public (Enemy enem, double attack) Attack(ModelUIElement3D model)
        {
            Enemy vrag;
            if (isBoss)
            {
                vrag = Boss;
                
            }
            else vrag = Enemies.FirstOrDefault(s=>s.model == model);
            //if (vrag == null) return;
            double attack = Math.Max(0, Player.Weapon.Attack - 2 - vrag.Defense);
            if (attack > vrag.HP) attack = vrag.HP;
            attack = Math.Round(attack, 2);
            vrag.HP -= attack;
            Console.WriteLine($"Вы наносите удар! {vrag.Name} получает {attack} урона!");
            //if (Boss != null) { StepSpendWinOverBoss++; }
            if (!vrag.IsAlive)
            {
                vrag.HP = 0;
                vrag.LastWord();
            }
            return (vrag, attack);
        }

        private DateTime lastUpdate = DateTime.Now;






        public (double attack, int Krit) AttackEnemOrBoss(Enemy enem)
        {
            if (Player.IsBlock == true) return (0, 0);
            (double attack, int Krits) = enem.EnemAttack(Player.Armor.ArmorHP, Player.IsFrozen);
            //attack = at;

            if (attack == -1)
            {
                Player.IsFrozen = true;
                
            }
            else
            {
                    if (attack >= Player.HP) attack = Player.HP;

                    Player.HP -= attack;

            }
            return (attack, Krits);
        }


        public void ChangPlayer(ItemChest item)
        {
            if(item is Weapon weapon)
            {
                Player.Weapon = weapon;
            }
            else if (item is Armor armor)
            {
                Player.Armor = armor;
            }
            else
            {
                Player.CountPotionHP++;
            }
        }

        public void UsePotionPlayerHP()
        {
            WorkGame.Game.Player.HP = 100;
            WorkGame.Game.Player.CountPotionHP--;
        }


        public void AttackEnemy()
        {
            if (IsPause ) return;
            //Player.HP -= enemy.Attack;
            DateTime now = DateTime.Now;
            //double deltaMs = (now - lastUpdate).TotalSeconds;
            double delta = (now - lastUpdate).TotalMilliseconds;
            //Console.WriteLine($"(now - lastUpdate).TotalSeconds = {delta}");
            //if (delta < 100 || delta > 300 ) return;
            //double deltaSeconds = 0.1;
            //if (deltaMs > 0.1) deltaMs = 0.1;

            //lastUpdate = DateTime.Now;

            if (isBoss)
            {
                double deltaSec = (now - Boss.LastAtTime).TotalSeconds;
                //if (deltaSec > .2) return;
                //Console.WriteLine(deltaSec);

                if (deltaSec >= Boss.AttackIntervalSec)
                {
                    //enem.TimeLastAttack = enem._TimeLastAttack;
                    //enem.AttackIntervalMs = RandomCLS.Next(4000, 10000);
                    Console.WriteLine($"LastAtTime = {Boss.LastAtTime} | delta = {deltaSec} | enem = {Boss.Name}");
                    Boss.LastAtTime = now;




                    //var geom = new GeometryModel3D(mesh, new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Icons/Armor.png")))));
                    (double attack, int Krit) = AttackEnemOrBoss(Boss);
                    if (!Player.IsAlive)
                    {
                        Boss.AnimAttackEnemyAndBoss(attack, kill: true, Player.IsBlock, Krit);
                        IsPause = true;
                        GameOver("again");

                    }
                    else
                    {
                        Boss.AnimAttackEnemyAndBoss(attack, kill: false, Player.IsBlock, Krit);

                    }


                }
            }
            else
            {
                foreach (var enem in Enemies)
                {
                    //enem.TimeLastAttack += delta;
                    double deltaSec = (now - enem.LastAtTime).TotalSeconds;
                    //if (deltaSec > .2) return;
                    //Console.WriteLine(deltaSec);

                    if (deltaSec >= enem.AttackIntervalSec)
                    {
                        //enem.TimeLastAttack = enem._TimeLastAttack;
                        //enem.AttackIntervalMs = RandomCLS.Next(4000, 10000);
                        Console.WriteLine($"LastAtTime = {enem.LastAtTime} | delta = {deltaSec} | enem = {enem.Name}");
                        enem.LastAtTime = now;


                        (double attack, int Krit) = AttackEnemOrBoss(enem);
                        if (!Player.IsAlive)
                        {
                            enem.AnimAttackEnemyAndBoss(attack, kill:true, Player.IsBlock, Krit);
                            IsPause = true;
                            GameOver("again");

                        }
                        else
                        {
                            enem.AnimAttackEnemyAndBoss(attack, kill: false, Player.IsBlock, Krit);

                        }

                        //var geom = new GeometryModel3D(mesh, new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Icons/Armor.png")))));
                    }
                }

            }
        }



        public void Restart()
        {
            //Enemies.Clear();
            foreach(var enem in Enemies)
            {
                MainWindow.viewport.Children.Remove(enem.model);

            }
            if (Boss != null)
                MainWindow.viewport.Children.Remove(Boss.model);
            Player = new Player("Player");
            step = 0;
            Score = 0;
        }


    }


    public static class WorkGame
    {
        private static Game game = new Game();

        public static Game Game { get { return game; }}
    }
}
