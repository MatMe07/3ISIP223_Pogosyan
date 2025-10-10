using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace _3ISIP223_Pogosyan
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.GameStart();
        }

    }




    class Player
    {
        public int HP { get; set; }
        public string Name { get; set; }
        public int AttackWeapon {  get; set; }    
        public int Armor { get; set; }
        public bool IsEvade { get; set; }
        public bool IsBlock { get; set; }

        public int CounMurders {  get; set; } 

        public Player(string name) 
        {
            HP = 100;
            AttackWeapon = 20;
            Armor = 40;
            Name = name;
            CounMurders = 0;
            IsEvade = false;
            IsBlock = false;
        }

        public void InfoWeapon()
        {

        }
        public void InfoArmor()
        {

        }
    }

    class Enemy
    {
        public string Name { get; set; }
        public double HP { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public bool HaveDefense => Defense > 0;
        public bool IsAlive => HP > 0;
        public Enemy(string name, double attack, double defense) 
        {
            HP = 100.0;
            Name = name;
            Attack = attack;
            Defense = defense;
        }
    }

    class Goblin: Enemy
    {
        public Random random = new Random();
        public double ProcentKritAttack { get; set; }
        public bool KritAttack => random.Next(0, 101) <= ProcentKritAttack;

        public Goblin() : base("Гоблин", 40, 20)
        {
            ProcentKritAttack = 15;
        }
    }
    class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 35, 30)
        {

        }
    }
    class Magician : Enemy
    {
        public Random random = new Random();
        public double ProcentFrozen { get; set; }
        public bool Frozen => random.Next(0, 101) <= ProcentFrozen ;
        public Magician() : base("Маг", 40, 15)
        {
            ProcentFrozen = 25;
        }
    }

    class VVG : Goblin
    {
        
        public VVG()
        {
            Name = "ВВГ";
            HP *= 2.0;
            Attack *= 1.5;
            Defense *= 1.2;
            ProcentKritAttack += 10;
        }
    }
    class Covalski : Skeleton
    {
        public Covalski()
        {
            Name = "Ковальский";
            HP *= 2.5;
            Attack *= 1.3;
            Defense *= 1.4;
        }
    }
    class Arhimag : Magician
    {
        public Arhimag()
        {
            Name = "Архимаг C++";
            HP *= 1.8;
            Attack *= 1.6;
            Defense *= 1.1;
            ProcentFrozen += 10;
        }
    }
    class Pestov : Magician
    {
        public Pestov()
        {
            Name = "Пестов С--";
            HP *= 1.3;
            Attack *= 1.8;
            Defense *= 0.6;
            ProcentFrozen += 15;
        }
    }
    class Game
    {
        public Random random = new Random();

        public bool ChoiceChestOrEnemy => Convert.ToBoolean(random.Next(0, 2));
        public bool ChoiceGameStart => ChoiceChestOrEnemy;
        public Player player;
        

        public int ToolsOfChest => random.Next(0, 3);
        public Game() 
        {
            string name = "Name";
            player = new Player(name);
        }


        public void InforEnemAndPlayer(Enemy vrag)
        {
            if (vrag != null)
            {
                if (vrag.IsAlive)
                {
                    Console.WriteLine($"PlayerHp: {player.HP}, PlayerArmor: {player.Armor}  |  Vrag_name: {vrag.Name}, VragHp: {vrag.HP}, Zashita: {vrag.Defense}");
                }
                else
                {
                    Console.WriteLine($"PlayerHp: {player.HP}, PlayerArmor: {player.Armor}  |  Vrag_name: {vrag.Name} -- Umer");
                }
            }
            else
            {
                    Console.WriteLine($"PlayerHp: {player.HP}, PlayerArmor: {player.Armor}");

            }
        }

        public void GameStart()
        {
            char n;
            int RandEnemy = 0;
            bool HaveEnemy = false;
            Enemy vrag = null;
            int step = 1;
            while (true)
            {
                Console.Clear();
                InforEnemAndPlayer(vrag);
                if (ChoiceChestOrEnemy) // Enemy
                {
                    if (!HaveEnemy)
                    {
                        RandEnemy = random.Next(0, 3);
                        switch (RandEnemy)
                        {
                            case 0:
                                {
                                    vrag = new Skeleton();
                                    break;
                                }
                            case 1:
                                {
                                    vrag = new Magician();
                                    break;
                                }
                            case 2:
                                {
                                    vrag = new Goblin();
                                    break;
                                }
                        }
                        HaveEnemy = true;
                        Console.Clear();
                        InforEnemAndPlayer(vrag);
                    }
                    
                    Console.Write("viberi!: ");
                    n = Convert.ToChar(Console.ReadLine().ToLower());
                    switch (n)
                    {
                        case 'с':
                            {
                                break;
                            }
                        case 'а'://attack
                            {
                                if (vrag.HaveDefense)
                                {
                                    vrag.Defense -= player.AttackWeapon;
                                    vrag.Defense = vrag.HaveDefense ? vrag.Defense : 0;
                                }
                                else
                                {
                                    vrag.HP -= player.AttackWeapon;
                                    if (!vrag.IsAlive)
                                    {
                                        vrag.HP = 0;
                                        player.CounMurders++; 
                                    }
                                }
                                Console.Clear();
                                InforEnemAndPlayer(vrag);
                                break;
                            }
                        case 'з'://Defence
                            {
                                bool Def40 = random.Next(1, 101) <= 40;
                                if (Def40)
                                {
                                    Console.WriteLine("Uklanis");
                                    player.IsEvade = true;
                                }
                                else
                                {
                                    Console.WriteLine("Ne povezlo: на 70-100% от защиты");
                                    player.IsBlock = true; 
                                }
                                break;

                            }
                        default:
                            {
                                break;
                            }
                    }

                    AttackEnem(vrag);


                }

                else // Chest
                {
                    Console.WriteLine("hello CHest");
                    ChestChoice();
                }


                step++;
            }
        }

        public void AttackEnem(Enemy vrag)
        {
            Console.Clear();
            InforEnemAndPlayer(vrag);
            if (vrag.IsAlive) 
            {
                
            }
        }

        public void ChestChoice()
        {

            string chestName = "";
            switch (ToolsOfChest) 
            {
                case 0: 
                    { 
                        chestName = "Лечебное зелье";
                        player.HP = 100;
                        Console.WriteLine();   
                        break;
                    }
                case 1:
                    {
                        chestName = "Оружие";
                        int RandAttackWeapon = random.Next(15, 51);
                        Console.WriteLine();
                        player.InfoWeapon();
                        Console.WriteLine();
                        char choice;
                        while (true)
                        {
                            choice = Convert.ToChar(Console.ReadLine().ToLower());
                            if (choice == 'y' || choice == 'n')
                            {
                                break;
                            }
                        }
                        if (choice == 'y')
                        {
                            player.AttackWeapon = RandAttackWeapon;
                        }
                        break;
                    }
                case 2: 
                    { 
                        chestName = "Доспех";
                        int RandArmor = random.Next(30, 71);
                        Console.WriteLine();
                        player.InfoWeapon();
                        Console.WriteLine();
                        char choice;
                        while (true)
                        {
                            choice = Convert.ToChar(Console.ReadLine());
                            if (choice == 'y' || choice == 'Y' || choice == 'N' || choice == 'n')
                            {
                                break;
                            }
                        }
                        if (choice == 'y' || choice == 'Y')
                        {
                            player.Armor = RandArmor;
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
    }
}