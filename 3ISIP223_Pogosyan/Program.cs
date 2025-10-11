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
            char n = 'н';
            int gameStep = 0;
            while (n != 'в')
            {
                while (gameStep!= 0)
                {
                    Console.Write("\n[Н] Начать заново   [В] Выйти\n> ");
                    if (char.TryParse(Console.ReadLine().ToLower(), out n) && (n == 'в' || n == 'н'))
                    {
                        break;
                    }
                }
                switch (n)
                {
                    case 'н':
                        {
                            Game game = new Game();
                            game.GameStart();
                            gameStep++;
                            break;
                        }
                    case 'в':
                        {
                            break;
                        }
                }

            }

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
        public int Progress { get; set; }
        public int CountAttackEnemies { get; set; }
        public int CountOpenChests { get; set; }
        public int CountAttackBosses { get; set; }
        public string LastEnemy { get; set; }
        


        public Player(string name) 
        {
            HP = 5;
            AttackWeapon = 100;
            Armor = 20;
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
            Console.Write($"{$"[{Name}]".PadRight(10)} HP: █");

            int hpProc = Convert.ToInt32(HP / 10);
            for (int i = 1; i < hpProc; i++)
            {
                Console.Write("█");
            }
            for (int i = 1; i < 10 - hpProc; i++)
            {
                Console.Write(" ");
            }
            Console.Write($" {HP}/100");
        }

        public void PlayerInfo()
        {
            Console.WriteLine(new string('─', 110));
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
            //Console.Write($"[{Name}] HP: █");
            Console.Write($"{$"[{Name}]".PadRight(10)} HP: █");

            int hpProc = Convert.ToInt32(HP / 10);
            for (int i = 1; i < hpProc; i++)
            {
                Console.Write("█");
            }
            for (int i = 1; i < 10 - hpProc; i++)
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

        public override void LastWord()
        {
            Console.WriteLine("Гоблин издает предсмертный визг и падает замертво.");
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

        public override void LastWord()
        {
            Console.WriteLine("Скелет рассыпается в кучу костей с оглушительным лязгом.");
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

        public override void LastWord()
        {
            Console.WriteLine("Маг издает последнее заклинание, которое рассеивается вместе с его жизнью.");
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
        public Enemy vrag = null;
        public bool HaveEnemy => vrag != null;
        public int step {  get; set; }
        public bool GameOver {  get; set; }


        public int ToolsOfChest => random.Next(0, 3);
        public Game() 
        {
            string name = "Player1";
            player = new Player(name);
            step = 1;
            GameOver = false;
        }

        public void InfoStartGame()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t======================= ДОБРО ПОЖАЛОВАТЬ В ПОДЗЕМЕЛЬЕ РОКА! =======================\n");
            Console.WriteLine("Прежде чем начать, представься:\n");
            Console.Write("Введите ваше имя:\n> ");
            string NamePlayer = Console.ReadLine();
            if (NamePlayer == null || NamePlayer == "") NamePlayer = "Player1";
            player.Name = NamePlayer;
            Console.WriteLine("\n\nНажмите Enter, чтобы начать...");
            Console.ReadLine();
        }

        public void StepInfo()
        {
            Console.WriteLine($"\n\t\t\t\t======================== Ход {step} ========================\n");
        }

        public void InforEnemAndPlayer()
        {
            //InfoZagalovok();
            if (player.IsAlive)
            {
                player.PlayerInfo();
                //if (HaveEnemy)
                //{
                    if (HaveEnemy && vrag.IsAlive)
                    {
                        //Console.WriteLine($"PlayerHp: {player.HP}, PlayerArmor: {player.Armor}  |  Vrag_name: {vrag.Name}, VragHp: {vrag.HP}, Zashita: {vrag.Defense}");
                        Console.WriteLine();
                        vrag.EnemyInfo();
                    }
                    //else
                    //{
                    //    vrag.LastWord();
                    //}
                //}
                Console.WriteLine(new string('─', 110));

            }
            else
            {

            }

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
            //bool HaveEnemy = false;

            InfoStartGame();
            while (player.IsAlive)
            {
                Console.Clear();
                InforEnemAndPlayer();
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
                        //HaveEnemy = true;
                        //Console.Clear();
                        //InforEnemAndPlayer(vrag);
                        //Thread.Sleep(500);
                        //StepInfo();
                        Thread.Sleep(500);
                        Console.WriteLine();
                        MeetingEnemy();
                    }

                    InputPointMenu(out n);
                    switch (n)
                    {
                        case 'а'://attack
                            {
                                PointMenuAttack();
                                break;
                            }
                        case 'з'://Defence
                            {
                                PointMenuDefense();
                                break;

                            }
                        default:
                            {

                                break;
                            }
                    }

                    AttackEnem();


                }

                else // Chest
                {
                    Console.WriteLine("В луче света вы замечаете старый сундук в углу пещеры...");
                    ChestChoice();
                }


                step++;
                player.Progress++;
            }
            PlayerDied();



        }

        public void PointMenuAttack()
        {
            Console.WriteLine("\nВы замахиваетесь для атаки...");
            Thread.Sleep(500);
            double attack = player.AttackWeapon - vrag.Defense;
            vrag.HP -= attack;
            Console.WriteLine($"Вы наносите удар! {vrag.Name} получает {attack} урона!");
            if (!vrag.IsAlive)
            {
                vrag.HP = 0;
                vrag.LastWord();
                player.CountAttackEnemies++;
                vrag = null;
                Console.WriteLine("\n\nНажмите Enter, чтобы продолжить...");
                Console.ReadLine();
            }
            else
            {
                vrag.InfoHp();
                Console.WriteLine();
            }
            
            //Console.Clear();
            //InforEnemAndPlayer(vrag);
        }

        public void PointMenuDefense()
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
        }

        public void MeetingBoss()
        {
            Console.WriteLine($"!!! ХОД {step} - ТРЕВОГА !!!\r\nПочва под ногами содрогается. Воздух наполняется зловещей энергией...");
            vrag.Demo();
        }
        public void MeetingEnemy()
        {
            Console.WriteLine($"Вы слышите зловещее рычание из темноты...");
            Thread.Sleep(500);
            Console.WriteLine($"Перед вами {vrag.Name}!\n");
            vrag.EnemyInfo();
            Console.WriteLine();
        }

        public void PlayerDied()
        {
            int box = 60;
            int boxStat = 40;
            Console.Clear();
            Console.WriteLine(new string('*', box));
            Console.WriteLine("GAME OVER".PadLeft(box/2));
            Console.WriteLine(new string('*', box));
            Console.WriteLine("\n\n");

            var sb = new StringBuilder();
            //sb.AppendLine($"┌ {"СТАТИСТИКА".Pa} ┐");
            sb.AppendLine("┌───────────── СТАТИСТИКА ─────────────┐");
            sb.AppendLine($"| {" ".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {$"Пройдено ходов: {player.Progress}".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {$"Повержено врагов: {player.CountAttackEnemies}".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {$"Открыто сундуков: {player.CountOpenChests}".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {$"Убито боссов: {player.CountAttackBosses}".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {" ".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {$"Последний враг: {player.LastEnemy}".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {$"Оружие: {player.NameWeapon}".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {$"Доспехи: {player.NameArmor}".PadRight(boxStat-4)} |");
            sb.AppendLine($"| {" ".PadRight(boxStat-4)} |");
            sb.AppendLine($"└{new string('─', boxStat-2)}┘");
            Console.WriteLine(sb.ToString());
            Console.WriteLine("============ ПРИЧИНА ГИБЕЛИ ============");
            if (player.CountAttackEnemies == 1)
            {
                Console.WriteLine("Не хватило опыта и снаряжения для первого \nже серьезного противника.");
            }
            else if (vrag.Name == "Гоблин")
            {
                Console.WriteLine("Критический удар гоблина пробил вашу защиту.");
            }
            else if (vrag.Name == "Скелет")
            {
                Console.WriteLine("Скелет проигнорировал вашу защиту и нанес \nсмертельный удар в ближнем бою.");
            }
            else if (vrag.Name == "Маг")
            {
                Console.WriteLine("Маг нанес смертельный удар.");
            }
        }

        public void AttackEnem()
        {
            if (!player.IsAlive) {player.InfoAfterDeath(); return; }
            //Console.Clear();
            //InforEnemAndPlayer(vrag);
            double attack = 0;

            if (HaveEnemy && vrag.IsAlive) 
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
                        if (attack > player.HP) attack = player.HP;
                        player.HP -= attack;
                        //if (!player.IsAlive) player.HP = 0;
                    }
                    else
                    {
                        attack = vrag.Attack - (vrag.Name == "Скелет" ? 0 : player.Armor);
                        Console.WriteLine($"attack = {attack}");
                        if (attack > player.HP)
                            attack = player.HP;

                        player.HP -= attack;
                        //if (!player.IsAlive) player.HP = 0;
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