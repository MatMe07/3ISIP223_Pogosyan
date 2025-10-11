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
        public double HP { get; set; }
        public string Name { get; set; }
        public double AttackWeapon {  get; set; }    
        public string NameWeapon { get; set; }
        public double Armor { get; set; }
        public string NameArmor { get; set; }
        public bool IsEvade { get; set; }
        public bool IsBlock { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsAlive => HP > 0;


        public int CountMurders {  get; set; } 


        public Player(string name) 
        {
            HP = 100;
            AttackWeapon = 30;
            Armor = 20;
            Name = name;
            CountMurders = 0;
            IsEvade = false;
            IsBlock = false;
            IsFrozen = false;
            NameWeapon = "Чапалах";
            NameArmor = "Доспехи Ланнистеров";
        }

        public void InfoWeapon()
        {

        }
        public void InfoAfterDeath()
        {

        }
        public void InfoArmor()
        {

        }

        public void InfoHp()
        {
            //Console.Write("HP: ");
            Console.Write($"[{Name}] HP: ");

            int hpProc = Convert.ToInt32(HP / 10);
            for (int i = 0; i < hpProc; i++)
            {
                Console.Write("█");
            }
            for (int i = 0; i < 10 - hpProc; i++)
            {
                Console.Write(" ");
            }
            Console.Write($" {HP}/100");
        }

        public void PlayerInfo()
        {
            Console.WriteLine($"==================================================");
            InfoHp();
            Console.WriteLine($" | Оружие: {NameWeapon} (АТК: {AttackWeapon}) | Доспехи: {NameArmor} (ЗАЩ: {Armor})");
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
        public string UniqSkill {  get; set; }
        public Enemy(string name, double attack, double defense) 
        {
            HP = 100.0;
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public virtual void AttackInfo(double atack)
        {
            Console.WriteLine("VragAtrackuet");
        }
        public void InfoHp()
        {
            Console.Write($"[{Name}] HP: ");
            int hpProc = Convert.ToInt32(HP / 10);
            for (int i = 0; i < hpProc; i++)
            {
                Console.Write("█");
            }
            for (int i = 0; i < 10 - hpProc; i++)
            {
                Console.Write(" ");
            }
            Console.Write($" {HP}/100");
        }
        public void EnemyInfo()
        {
            //Console.Write($"[{Name}] ");
            InfoHp();
            Console.WriteLine($" | АТК: {Attack} | ЗАЩ: {Defense} | Особость: {UniqSkill}");
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

    class Goblin: Enemy
    {
        public Random random = new Random();
        public double ProcentKritAttack { get; set; }
        public bool KritAttack => random.Next(0, 101) <= ProcentKritAttack;

        public Goblin() : base("Гоблин", 40, 20)
        {
            ProcentKritAttack = 15;
        }
        public override void AttackInfo(double atack)
        {
            if (KritAttack)
            {
                Console.WriteLine($"\nГоблин издает боевой клич и наносит подлый удар! КРИТИЧЕСКИЙ УРОН! {atack} урона!");
            }
            else
            {
                Console.WriteLine($"\nГоблин атакует вас! Вы получаете {atack} урона.");

            }
        }

    }
    class Skeleton : Enemy
    {
        public Skeleton() : base("Скелет", 25, 15)
        {

        }

        public override void AttackInfo(double atack)
        {
            Console.WriteLine($"\nСкелет проходит сквозь вашу защиту! Ваши доспехи бесполезны! {atack} урона!");
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
        public override void AttackInfo(double attack)
        {

            Console.WriteLine("\nМаг произносит древнее заклинание, он пытается заморозить вас...");
            Thread.Sleep(1000);
            if (!Frozen) Console.WriteLine("У него не получилось! Вам удалось увернуться от ледяных оков!");
            else Console.WriteLine("У него получилось! Вы пропускаете следующий ход!");
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
        public int step {  get; set; }
        public bool GameOver {  get; set; }


        public int ToolsOfChest => random.Next(0, 3);
        public Game() 
        {
            string name = "Name";
            player = new Player(name);
            step = 1;
            GameOver = false;
        }

        public void InfoStartGame()
        {
            Console.WriteLine("\t\t\t======================= ДОБРО ПОЖАЛОВАТЬ В ПОДЗЕМЕЛЬЕ РОКА! =======================\n");
            Console.WriteLine("\nНажмите Enter, чтобы начать...");
            Console.ReadLine();
        }

        public void StepInfo()
        {
            Console.WriteLine($"\n\t\t\t\t======================== Ход {step} ========================\n");
        }

        public void InforEnemAndPlayer(Enemy vrag)
        {
            //InfoZagalovok();
            player.PlayerInfo();
            if (vrag != null)
            {
                if (vrag.IsAlive)
                {
                    //Console.WriteLine($"PlayerHp: {player.HP}, PlayerArmor: {player.Armor}  |  Vrag_name: {vrag.Name}, VragHp: {vrag.HP}, Zashita: {vrag.Defense}");
                    Console.WriteLine();
                    vrag.EnemyInfo();
                }
                else
                {
                    vrag.LastWord();
                }
            }
            Console.WriteLine($"==================================================");

        }

        public void InputPointMenu(out char n)
        {
            while (true)
            {
                Console.Write("Выберите действие:\n[А] АТАКА\n[З] ЗАЩИТА\n> ");

                if (char.TryParse(Console.ReadLine().ToLower(), out n) && (n == 'а' || n == 'з'))
                {
                    break;
                }

                Console.WriteLine("Неверное действие!\n");
            }
        }

        public void GameStart()
        {
            char n;
            int RandEnemy = 0;
            bool HaveEnemy = false;
            Enemy vrag = null;

            InfoStartGame(); 
            while (true)
            {
                Console.Clear();
                InforEnemAndPlayer(vrag);
                StepInfo();
                if (true) // Enemy
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
                        //Console.Clear();
                        //InforEnemAndPlayer(vrag);
                        //Thread.Sleep(500);
                        //StepInfo();
                        Thread.Sleep(500);
                        Console.WriteLine();
                        MeetingEnemy(vrag);
                    }

                    InputPointMenu(out n); 
                    switch (n)
                    {
                        case 'с':
                            {
                                break;
                            }
                        case 'а'://attack
                            {
                                PointMenuAttack(vrag);
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
                    Console.WriteLine("В луче света вы замечаете старый сундук в углу пещеры...");
                    ChestChoice();
                }


                step++;
            }
        }

        public void PointMenuAttack(Enemy vrag)
        {
            Console.WriteLine("\nВы замахиваетесь для атаки...");
            Thread.Sleep(500);
            double attack = player.AttackWeapon - vrag.Defense;
            vrag.HP -= attack;
            Console.WriteLine($"Вы наносите удар! {vrag.Name} получает {attack} урона!");
            if (!vrag.IsAlive)
            {
                vrag.HP = 0;
                player.CountMurders++;
                vrag.LastWord();
            }
            else
            {
                vrag.InfoHp();
                Console.WriteLine();
            }
            
            //Console.Clear();
            //InforEnemAndPlayer(vrag);
        }

        public void MeetingBoss(Enemy vrag)
        {
            Console.WriteLine($"!!! ХОД {step} - ТРЕВОГА !!!\r\nПочва под ногами содрогается. Воздух наполняется зловещей энергией...");
            vrag.Demo();
        }
        public void MeetingEnemy(Enemy vrag)
        {
            Console.WriteLine($"Вы слышите зловещее рычание из темноты...");
            Thread.Sleep(500);
            Console.WriteLine($"Перед вами {vrag.Name}!\n");
            vrag.EnemyInfo();
            Console.WriteLine();
        }

        public void AttackEnem(Enemy vrag)
        {
            if (!player.IsAlive) {player.InfoAfterDeath(); return; }
            //Console.Clear();
            //InforEnemAndPlayer(vrag);
            double attack = 0;

            if (vrag.IsAlive) 
            {
                //Console.Clear();
                //InforEnemAndPlayer(vrag);
                if (player.IsEvade)
                {
                    Console.WriteLine("\nВам удалось ловко уклониться от атаки врага!");
                    player.IsEvade = false;
                }
                else
                {
                    if(vrag.Name == "Маг") { }
                    else if (player.IsBlock)
                    {
                        player.IsBlock = false;
                        double randBlock = random.Next(70, 101);
                        attack = vrag.Attack - (randBlock * player.Armor) / 100.0;
                        player.HP -= attack;
                        if (!player.IsAlive) player.HP = 0;
                    }
                    else
                    {
                        attack = vrag.Attack - (vrag.Name == "Скелет" ? 0 : player.Armor);
                        player.HP -= attack;
                        if (!player.IsAlive) player.HP = 0;
                    }

                    Thread.Sleep(500);
                    //Console.WriteLine($"{vrag.Name} яростно бросается на вас! Вы получаете {attack} урона.");
                    vrag.AttackInfo(attack);
                    if (vrag.Name == "Маг" && ((Magician)vrag).Frozen) { player.IsFrozen = true; }

                    player.InfoHp();
                }
                Console.WriteLine("\n\nНажмите Enter, чтобы продолжить...");
                Console.ReadLine();
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
                        Console.WriteLine(chestName);   
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