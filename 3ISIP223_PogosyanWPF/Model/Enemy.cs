using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.Model
{
    public class Enemy
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

        public void EnemyInfo()
        {
            
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
