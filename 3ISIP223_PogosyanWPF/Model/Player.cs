using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace _3ISIP223_PogosyanWPF.Model
{
    public class Player : INotifyPropertyChanged
    {

        private double _hp;
        public double HP
        {
            get { return _hp; }
            set { 
                _hp = value; 
                OnPropertyChanged(nameof(HP));
            }
        }
        public string Name { get; set; }

        private Weapon _weapon;
        public Weapon Weapon
        {
            get { return _weapon; }
            set
            {
                _weapon = value;
                OnPropertyChanged(nameof(Weapon));
            }
        }



        private Armor _armor;
        public Armor Armor
        {
            get { return _armor; }
            set
            {
                _armor = value;
                OnPropertyChanged(nameof(Armor));
            }
        }
        public bool IsEvade { get; set; }
        public bool IsBlock { get; set; }
        private bool isFrozen = false;

        public bool IsFrozen
        {
            get => isFrozen;
            set
            {
                if (value == true)
                {
                    isFrozen = true;

                    MainWindow.FrozenPanel.Visibility = Visibility.Visible;
                    var anim = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = TimeSpan.FromMilliseconds(1200)
                    };
                    anim.Completed += (e, s) =>
                    {
                        Console.WriteLine("Начинает исчезать...");
                        anim = new DoubleAnimation();
                        anim.From = 1;
                        anim.To = 0;
                        anim.Duration = TimeSpan.FromSeconds(3);

                        anim.Completed += (es, ss) =>
                        {
                            Console.WriteLine("Исчез");
                            MainWindow.FrozenPanel.Visibility = Visibility.Collapsed;
                            IsFrozen = false;
                        };

                        MainWindow.FrozenPanel.BeginAnimation(Window.OpacityProperty, anim);
                    };
                    MainWindow.FrozenPanel.BeginAnimation(Window.OpacityProperty, anim);

                }
                else
                {
                    isFrozen = false;
                }
            }
        }


        private int _countPotionHP = 0;
        public int CountPotionHP
        {
            get
            {
                return _countPotionHP;
            }
            set
            {
                _countPotionHP = value;
                OnPropertyChanged(nameof(CountPotionHP));
                OnPropertyChanged(nameof(HP));
            }
        }

        public bool IsAlive => HP > 0;
        public int Progress { get; set; }
        public int CountAttackEnemies { get; set; }
        public int CountOpenChests { get; set; }
        public int CountAttackBosses { get; set; }
        public string LastEnemy { get; set; }



        public Player(string name)
        {
            HP = 10;
            Weapon = new Weapon("МЕЧ", 50, "pack://application:,,,/Images/weapons/swordUs.png");
            //AttackWeapon = 8;
            Armor = new Armor("Доспехи Ланнистеров", 5, "pack://application:,,,/Images/armors/Lanister_armor.png");
            Name = name;
            IsEvade = false;
            IsBlock = false;
            IsFrozen = false;
            Progress = 1;
            CountAttackEnemies = 0;
            CountOpenChests = 0;
            CountAttackBosses = 0;
            LastEnemy = "NONE";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
