using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan
{
    
    internal class Program
    {
        static void Main(string[] args)
        {

        }

    }




    class Player
    {
        public int HP { get; set; }
        public string Name { get; set; }
        public bool Weapon {  get; set; }    
        public bool Armor { get; set; }

        public Player(string name) 
        {
            HP = 100;
            Weapon = true;
            Armor = true;
            Name = name;
        }
    }

    class Enemy
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Enemy(string name, int attack, int defense) 
        {
            HP = 100;
            Name = name;
            Attack = attack;
            Defense = defense;
        }
    }

    class Goblin: Enemy
    {
        public Random random = new Random();
        public double ProcentKritAttack { get; set; }
        public bool KritAttack => random.Next(0, 100) <= ProcentKritAttack;

        public Goblin() : base("Гоблин", 100, 20)
        {
            ProcentKritAttack = 15;
        }
    }
    class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 100, 20)
        {

        }
    }
    class Magician : Enemy
    {
        public Magician() : base("Маг", 100, 20)
        {

        }
    }
}