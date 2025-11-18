using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class Enemy
    {
        public Random random = new Random();

        public string Name { get; set; }

        public double HP { get; set; }
        public double MaxHP { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public bool HaveDefense => Defense > 0;
        public bool IsAlive => HP > 0;
        public string UniqSkill { get; set; }
        public Enemy(string name, double attack, double defense, double hp)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            HP = hp;
            MaxHP = hp;
        }

        public virtual void AttackInfo(double atack)
        {
            Console.WriteLine("VragAtrackuet");
        }
        public void InfoHp()
        {
            //Console.Write($"[{Name}] HP: █");
            Console.Write($"{$"[{Name}]".PadRight(10)} HP: █");

            int hpProc = Convert.ToInt32(HP / (MaxHP / 10));
            for (int i = 1; i < hpProc; i++)
            {
                Console.Write("█");
            }
            //Console.WriteLine($" MaxHp/10={MaxHP/10}| hpProc={hpProc}");
            for (int i = 0; i < 10 - hpProc; i++)
            {
                Console.Write(" ");
            }
            Console.Write($" {Math.Round(HP, 2)}/{MaxHP}");
        }
        public void EnemyInfo()
        {
            //Console.Write($"[{Name}] ");
            InfoHp();
            Console.WriteLine($" | АТК: {Math.Round(Attack, 2)} | ЗАЩ: {Math.Round(Defense, 2)} | Особость: {UniqSkill}");
        }
        public virtual void LastWord()
        {
            Console.WriteLine("Umer");
        }

        public virtual void Demo()
        {
            Console.WriteLine(Name);
        }
    }

}
