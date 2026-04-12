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
        public bool BossMoment => step % 2 == 0;

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

        public List<Enemy> Enemies { get; set; }

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Enemy Boss = null;
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
        public bool isBoss { get; set; }

        public bool IsGameOrChest {
            get
            {
                int n = RandomCLS.Next(0, 101);
                Console.WriteLine("\nRandIsGameOrChest = {0}\n", n);
                return n > 55;
            }
        }

        public Action<string> GameOver {  get; set; }
        public Game()
        {
            string name = "Player1";
            Player = new Player(name);
            Vrags = new EnemyCreater();
            Enemies = new List<Enemy>();
            isBoss = false;
            step = 1;


        }

        public Enemy AddEnemis()
        {
            if (IsPause) return null;
            var enm = Vrags.Enemies[RandomCLS.Next(0, Vrags.Enemies.Count)].CreateEnemy();
            Enemies.Add(enm);
            return enm;
        }
        public void SelectBoss()
        {
            if (IsPause) return;

            Boss = Vrags.Bosses[RandomCLS.Next(0, Vrags.Bosses.Count)].CreateEnemy();
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
            DateTime now = DateTime.Now;
            double delta = (now - lastUpdate).TotalMilliseconds;

            if (isBoss)
            {
                if (IsPause)
                {
                    Boss.LastAtTime = now;
                    return;
                }
                double deltaSec = (now - Boss.LastAtTime).TotalSeconds;

                if (deltaSec >= Boss.AttackIntervalSec)
                {
                    Console.WriteLine($"LastAtTime = {Boss.LastAtTime} | delta = {deltaSec} | enem = {Boss.Name}");
                    Boss.LastAtTime = now;




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
                if (Enemies.Count > 0)
                {
                    for(int i = 0; i < Enemies.Count; i++)
                    {
                        var enem = Enemies[i];
                    //}
                    //foreach (var enem in Enemies)
                    //{
                        if (IsPause)
                        {
                            enem.LastAtTime = now;
                            break;
                        }
                        double deltaSec = (now - enem.LastAtTime).TotalSeconds;

                        if (deltaSec >= enem.AttackIntervalSec)
                        {
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

                        }
                    }


                }
            }
        }



        public void Restart()
        {
            foreach (var enem in Enemies)
            {
                MainWindow.viewport.Children.Remove(enem.model);
            }
            if (Boss != null)
            {
                MainWindow.viewport.Children.Remove(Boss.model);
                DeleteBoss();
            }
            Player = new Player("Player");
            step = 1;
            Score = 0;

            Console.WriteLine("Len Enemies = {0}", Enemies.Count);
            Enemies.Clear();
            Console.WriteLine("Len Enemies = {0}, clear", Enemies.Count);
        }


    }


    public static class WorkGame
    {
        private static Game game = new Game();

        public static Game Game { get { return game; }}
    }
}
