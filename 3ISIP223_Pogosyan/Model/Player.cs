using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class Player
    {
        public double HP { get; set; }
        public string Name { get; set; }
        public double AttackWeapon { get; set; }
        public string NameWeapon { get; set; }
        public double Armor { get; set; }
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

        public string InfoWeapon()
        {
            return $"{NameWeapon} (АТК: {AttackWeapon})";
        }
        public string InfoArmor()
        {
            return $"{NameArmor} (ЗАЩ: {Armor})";

        }

        public void InfoHp()
        {
            //Console.Write("HP: ");
            Console.Write($"{$"[{Name}]".PadRight(10)} HP: █");

            int hpProc = Convert.ToInt32(HP / 10);
            for (int i = 1; i < hpProc; i++)
            {
                Console.Write("█");
            }
            for (int i = 0; i < 10 - hpProc; i++)
            {
                Console.Write(" ");
            }
            Console.Write($" {Math.Round(HP, 2)}/100");
        }

        public void PlayerInfo()
        {
            InfoHp();
            Console.WriteLine($" | Оружие: {NameWeapon} (АТК: {Math.Round(AttackWeapon, 2)}) | Доспехи: {NameArmor} (ЗАЩ: {Math.Round(Armor, 2)})");
        }
    }

}
