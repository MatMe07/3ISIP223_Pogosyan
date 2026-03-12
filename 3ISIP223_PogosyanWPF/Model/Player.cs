using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public double AttackWeapon { get; set; }
        public string NameWeapon { get; set; }
        private double _armor;
        public double Armor
        {
            get { return _armor; }
            set
            {
                _armor = value;
                OnPropertyChanged(nameof(Armor));
            }
        }
        public string NameArmor { get; set; }
        public bool IsEvade { get; set; }
        public bool IsBlock { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsAlive => HP > 0;
        public int Progress { get; set; }
        public int CountAttackEnemies { get; set; }
        public int CountOpenChests { get; set; }
        public int CountAttackBosses { get; set; }
        public string LastEnemy { get; set; }



        public Player(string name)
        {
            HP = 100;
            AttackWeapon = 15;
            //AttackWeapon = 8;
            Armor = 5;
            Name = name;
            IsEvade = false;
            IsBlock = false;
            IsFrozen = false;
            NameWeapon = "Чапалах";
            NameArmor = "Доспехи Ланнистеров";
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

        public string InfoWeapon()
        {
            return $"{NameWeapon} (АТК: {AttackWeapon})";
        }
        public string InfoArmor()
        {
            return $"{NameArmor} (ЗАЩ: {Armor})";

        }

    }
}
