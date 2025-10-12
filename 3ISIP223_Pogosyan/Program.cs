//using System;
//using System.Collections.Generic;
//using System.ComponentModel.Design;
//using System.Diagnostics;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;

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

    class Enemy
    {
        public Random random = new Random();

        public string Name { get; set; }

        public double HP { get; set; }
        public double MaxHP { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public bool HaveDefense => Defense > 0;
        public bool IsAlive => HP > 0;
        public string UniqSkill {  get; set; }
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

            int hpProc = Convert.ToInt32(HP / (MaxHP/10));
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

    class Goblin: Enemy
    {
        public double ProcentKritAttack { get; set; }
        public bool IsKritAttack { get; set; }
        public bool KritAttack => random.Next(0, 100) < ProcentKritAttack;

        public Goblin(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            ProcentKritAttack = 15;
            UniqSkill = "Шанс критического удара";
            IsKritAttack = false;
        }
        public override void AttackInfo(double atack)
        {
            if (IsKritAttack)
            {
                IsKritAttack = false;
                Console.WriteLine($"\n{Name} издает боевой клич и наносит подлый удар! КРИТИЧЕСКИЙ УРОН! {atack} урона!");
            }
            else
            {
                Console.WriteLine($"\n{Name} атакует вас! Вы получаете {atack} урона.");

            }
        }

        public override void LastWord()
        {
            Console.WriteLine("Гоблин издает предсмертный визг и падает замертво.");
        }

    }
    class Skeleton : Enemy
    {
        public Skeleton(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            UniqSkill = "Игнор брони";
        }

        public override void AttackInfo(double atack)
        {
            Console.WriteLine($"\n{Name} проходит сквозь вашу защиту! Ваши доспехи бесполезны! {atack} урона!");
        }

        public override void LastWord()
        {
            Console.WriteLine("Скелет рассыпается в кучу костей с оглушительным лязгом.");
        }
    }
    class Magician : Enemy
    {
        public double ProcentFrozen { get; set; }
        public bool IsFroz { get; set; }
        public bool FrozenProc => random.Next(0, 100) < ProcentFrozen;
        public bool Frozen() {
            //(!IsFroz && random.Next(0, 101) <= ProcentFrozen);
            if (!IsFroz)
            {
                return FrozenProc;
            }
            return true;
        }

        public Magician(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            ProcentFrozen = 20;
            IsFroz = false;
            UniqSkill = "Шанс заморозки на 1 ход";
        }
        public override void AttackInfo(double attack)
        {

            if (Frozen())
            {
                Console.WriteLine($"\n{Name} произносит древнее заклинание, он пытается заморозить вас...");
                Thread.Sleep(1000);
                Console.WriteLine("У него получилось! Вы пропускаете следующий ход!");
            }
            Console.WriteLine($"\n{Name} атакует вас! Вы получаете {attack} урона.");
        }

        public override void LastWord()
        {
            Console.WriteLine($"{Name} издает последнее заклинание, которое рассеивается вместе с его жизнью.");
        }
    }

    class VVG : Goblin
    {
        
        public VVG(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            HP *= 2.0;
            MaxHP *= 2.0;
            Attack *= 1.5;
            Defense *= 1.2;
            ProcentKritAttack += 10;
            UniqSkill = "Усиленный критический удар";
        }
        public override void Demo()
        {

            Console.WriteLine("Из тени появляется МАССИВНЫЙ гоблин в ржавых доспехах - это ВВГ!\n");
        }
    }
    class Covalski : Skeleton
    {
        public Covalski(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            HP *= 2.5;
            MaxHP *= 2.5;
            Attack *= 1.3;
            Defense *= 1.4;
            UniqSkill = "Игнор брони";
        }
        public override void Demo()
        {
            Console.WriteLine("Кости с грохотом слагаются в исполинскую фигуру - это КОВАЛЬСКИЙ!!\n");
        }
    }
    class Arhimag : Magician
    {
        public Arhimag(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            HP *= 1.8;
            MaxHP *= 1.8;
            Attack *= 1.6;
            Defense *= 1.1;
            ProcentFrozen += 10;
            UniqSkill = "Усиленная заморозка";
        }
        public override void Demo()
        {
            Console.WriteLine("Воздух замерзает. По стенам расползается иней. Это АРХИМАГ C++!\n");
        }
    }
    class Pestov : Magician
    {
        public Pestov(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            HP *= 1.3;
            MaxHP *= 1.3;
            Attack *= 1.8;
            Defense *= 0.6;
            ProcentFrozen += 15;
            UniqSkill = "Усиленная заморозка";
        }
        public override void Demo()
        {
            Console.WriteLine("Тень отделяется от стены - это ПЕСТОВ С--! Движется неестественно, как сломанная анимация.\n");
        }
    }
    class Game
    {
        public Random random = new Random();

        //public bool ChoiceChestOrEnemy => false;
        public bool ChoiceChestOrEnemy => Convert.ToBoolean(random.Next(0, 2));

        //public bool IsChest { get; set; }
        //public bool ChoiceGameStart => ChoiceChestOrEnemy;
        public Player player;
        public Enemy Vrag = null;
        public Enemy Boss = null;
        public bool HaveEnemy => Vrag != null;
        public int step {  get; set; }
        public int StepSpendWinOverBoss {  get; set; }
        public double HPLostWinOverBoss {  get; set; }
        public bool GameOver {  get; set; }
        //public bool BosseStumles => true;
        public bool BosseStumles => step % 10 == 0;


        public int ToolsOfChest => random.Next(0, 3);
        public Game() 
        {
            string name = "Player1";
            player = new Player(name);
            step = 1;
            GameOver = false;
            StepSpendWinOverBoss = 0;
            HPLostWinOverBoss = 0;
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
            if ( BosseStumles || Boss != null)
            {
                string line = new string('■', 50);
                Console.WriteLine(line.PadLeft(80));
                Console.WriteLine($"{"ХОД".PadLeft(48)} {step} - АКТИВАЦИЯ БОССА");
                //Console.WriteLine(new string('■', 50));
                Console.WriteLine(line.PadLeft(80));
            }
            else
            {
                Console.WriteLine($"\n\t\t\t\t============== Ход {step} ==============\n");
            }

        }

        public void InforEnemAndPlayer()
        {
            //InfoZagalovok();
            if (player.IsAlive)
            {
                Console.WriteLine(new string('─', 110));
                player.PlayerInfo();
                //if (HaveEnemy)
                //{
                if (Boss != null &&  Boss.IsAlive) 
                {
                    Console.WriteLine();
                    Boss.EnemyInfo();
                }
                else if (HaveEnemy && Vrag.IsAlive)
                {
                        //Console.WriteLine($"PlayerHp: {player.HP}, PlayerArmor: {player.Armor}  |  Vrag_name: {vrag.Name}, VragHp: {vrag.HP}, Zashita: {vrag.Defense}");
                        Console.WriteLine();
                        Vrag.EnemyInfo();
                }
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
                Console.Write("Выберите действие:\n[А] АТАКА     [З] ЗАЩИТА\n> ");

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
            char n_boss;
            int RandEnemy = 0;
            int RandBosse = 0;
            //bool HaveEnemy = false;

            InfoStartGame();
           
            while (player.IsAlive)
            {
                Console.Clear();
                InforEnemAndPlayer();
                StepInfo();
                if (!BosseStumles && (Boss == null))
                {
                    if (ChoiceChestOrEnemy) // Enemy
                    {
                        if (!HaveEnemy)
                        {
                            RandEnemy = 1;
                            //RandEnemy = random.Next(0, 3);
                            switch (RandEnemy)
                            {
                                case 0:
                                    {
                                        Vrag = new Skeleton("Скелет", 12, 2, 20);
                                        break;
                                    }
                                case 1:
                                    {
                                        Vrag = new Magician("Маг", 14, 1, 15);
                                        break;
                                    }
                                case 2:
                                    {
                                        Vrag = new Goblin("Гоблин", 10, 3, 20);
                                        break;
                                    }
                            }
                            //HaveEnemy = true;
                            //Console.Clear();
                            //InforEnemAndPlayer(vrag);
                            //Thread.Sleep(500);
                            //StepInfo();
                            StepSpendWinOverBoss = 0;
                            Thread.Sleep(500);
                            Console.WriteLine();
                            MeetingEnemy();
                        }
                        if (player.IsFrozen)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"\t\t\t\t============== ВЫ ЗАМОРОЖЕНЫ! ==============");
                            Console.WriteLine("\t\t\t\tВы не можете действовать из-за магического льда.");
                            ((Magician)Vrag).IsFroz = false;
                            player.IsFrozen = false; 
                        }
                        else
                        {
                            InputPointMenu(out n);
                            switch (n)
                            {
                                case 'а'://attack
                                    {
                                        PointMenuAttack(ref Vrag);
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

                        }


                        AttackEnem(Vrag);


                    }
                    else // Chest
                    {
                        
                        Console.WriteLine($"{"НАЙДЕН".PadLeft(49)} СУНДУК \n");
                        ChestChoice();
                    }
                }
                else // boss
                {
                    if (Boss == null)
                    {
                        RandBosse = random.Next(0, 4);
                        switch (RandBosse)
                        {
                            case 0:
                                {
                                    Boss = new VVG("ВВГ", 10, 3, 20);
                                    break;
                                }
                            case 1:
                                {
                                    Boss = new Covalski("Ковальский", 12, 2, 20);
                                    break;
                                }
                            case 2:
                                {
                                    Boss = new Arhimag("Архимаг C++", 14, 1, 15);
                                    break;
                                }
                            case 3:
                                {
                                    Boss = new Pestov("Пестов С--", 14, 1, 15);
                                    break;
                                }
                        }
                        //Console.Clear();

                        //Thread.Sleep(500);
                        Console.WriteLine();

                        MeetingBoss();
                    }

                    if (player.IsFrozen)
                    {
                        Console.WriteLine($"\n\t\t\t\t============== ВЫ ЗАМОРОЖЕНЫ! ==============");
                        Console.WriteLine("\t\t\t\tВы не можете действовать из-за магического льда.");
                        ((Magician)Boss).IsFroz = false;

                        player.IsFrozen = false;
                    }
                    else
                    {
                        InputPointMenu(out n_boss);
                        switch (n_boss)
                        {
                            case 'а'://attack
                                {
                                    PointMenuAttack(ref Boss);
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
                    }


                    AttackEnem(Boss);


                }


                step++;
                player.Progress++;
            }
            PlayerDied();

            

        }

        public void WinOverBoss()
        {

            Console.Clear();
            //InforEnemAndPlayer();
            string line = new string('▓', 50);
            string lineBottom = new string('─', 50);
            Console.WriteLine(line.PadLeft(80));
            Console.WriteLine($"{" ".PadLeft(40)}БОСС ПОВЕРЖЕН - {Boss.Name} УНИЧТОЖЕН");
            //Console.WriteLine(new string('■', 50));
            Console.WriteLine(line.PadLeft(80));
            Console.WriteLine($"\n{"Ходов".PadLeft(42)} затрачено: {StepSpendWinOverBoss}, Потеряно HP: {Math.Round(HPLostWinOverBoss, 2)}");
            Console.WriteLine(lineBottom.PadLeft(80));
        }

        public void PointMenuAttack(ref Enemy vrag)
        {
            Console.WriteLine("\nВы замахиваетесь для атаки...");
            Thread.Sleep(500);
            double attack = Math.Max(0, player.AttackWeapon - vrag.Defense);
            if (attack > vrag.HP) attack = vrag.HP;
            attack = Math.Round(attack, 2);
            vrag.HP -= attack;
            Console.WriteLine($"Вы наносите удар! {vrag.Name} получает {attack} урона!");
            if (Boss !=  null) { StepSpendWinOverBoss++;}
            if (!vrag.IsAlive)
            {
                vrag.HP = 0;
                vrag.LastWord();
                if (Boss != null)
                {
                    Thread.Sleep(1100);
                    player.CountAttackBosses++;
                    WinOverBoss();
                }
                else
                {
                    player.CountAttackEnemies++;
                }
                vrag = null;
                Console.WriteLine("\n\nНажмите Enter, чтобы продолжить...");
                Console.ReadLine();
            }
            else
            {
                vrag.InfoHp();
                Console.WriteLine();
            }
            //Console.ReadLine();
            //Console.Clear();
            //InforEnemAndPlayer(vrag);
        }

        public void PointMenuDefense()
        {
            Console.WriteLine("Вы выбрали ЗАЩИТУ...");
            bool Def40 = random.Next(1, 101) <= 40;
            if (Def40)
            {
                player.IsEvade = true;
            }
            else
            {
                player.IsBlock = true;
            }
        }

        public void MeetingBoss()
        {
            Console.WriteLine();
            Thread.Sleep(700);
            Boss.Demo();
            Thread.Sleep(600);
            Boss.EnemyInfo();
            Console.WriteLine();
        }
        public void MeetingEnemy()
        {
            Console.WriteLine($"Вы слышите зловещее рычание из темноты...");
            Thread.Sleep(500);
            Console.WriteLine($"Перед вами {Vrag.Name}!\n");
            Vrag.EnemyInfo();
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
            else if (player.LastEnemy == "Гоблин")
            {
                Console.WriteLine("Критический удар гоблина пробил вашу защиту.");
            }
            else if (player.LastEnemy == "Скелет")
            {
                Console.WriteLine("Скелет проигнорировал вашу защиту и нанес \nсмертельный удар в ближнем бою.");
            }
            else if (player.LastEnemy == "Маг")
            {
                Console.WriteLine("Маг нанес смертельный удар.");
            }
            else if (player.LastEnemy == "ВВГ")
            {
                Console.WriteLine("Мощный критический удар ВВГ сокрушил вашу защиту.");
            }
            else if (player.LastEnemy == "Ковальский")
            {
                Console.WriteLine("Ковальский полностью проигнорировал вашу защиту.");
            }
            else if (player.LastEnemy == "Пестов С--")
            {
                Console.WriteLine("Мощное заклинание Пестова прожгло вашу защиту.");
            }
            else if (player.LastEnemy == "Архимаг C++")
            {
                Console.WriteLine("Заклинание Архимага пробило все уровни защиты.");
            }
        }

        public void AttackEnem(Enemy vrag)
        {
            //Console.Clear();
            //InforEnemAndPlayer(vrag);
            double attack = 0;

            if (vrag != null && vrag.IsAlive) 
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

                    if (!player.IsFrozen && vrag.UniqSkill.Contains("замороз") && ((Magician)vrag).Frozen())
                    {
                        ((Magician)vrag).IsFroz = true;
                        player.IsFrozen = true;
                    }

                    if (vrag is Skeleton)
                    {
                        attack = vrag.Attack;
                    }
                    else if (player.IsBlock)
                    {
                        Console.WriteLine("Уклонение не удалось! Но вы подставляете свой щит...");
                        double randBlock = random.Next(70, 101) / 100.0;
                        attack = Math.Max(0, vrag.Attack - (randBlock * player.Armor)); 
                        player.IsBlock = false;
                    }
                    else if (vrag is Goblin goblin && goblin.KritAttack)
                    {
                        goblin.IsKritAttack = true;
                        attack = Math.Max(0, vrag.Attack*2 - player.Armor);
                    }
                    else
                    {
                        attack = Math.Max(0, vrag.Attack - player.Armor);
                    }

                    if (attack >= player.HP)
                    {
                        attack = player.HP;
                        player.LastEnemy = vrag.Name;
                    }
                    player.HP -= attack;


                    Thread.Sleep(500);
                    //Console.WriteLine($"{vrag.Name} яростно бросается на вас! Вы получаете {attack} урона.");
                    attack = Math.Round(attack, 2);
                    vrag.AttackInfo(attack);
                    if (Boss != null) { HPLostWinOverBoss += attack; }

                    player.InfoHp();
                }
                Console.WriteLine("\n\nНажмите Enter, чтобы продолжить...");
                Console.ReadLine();
            }

        }


        public void ChestChoice()
        {
            player.CountOpenChests++;
            Console.WriteLine("В луче света вы замечаете старый сундук в углу пещеры...");
            Console.WriteLine("Вы открываете сундук и находите...\n");
            string line = new string('═', 40);
            Thread.Sleep(900);
            string chestName = "";
            switch (ToolsOfChest) 
            {
                case 0: 
                    { 
                        Console.WriteLine(line.PadLeft(70));
                        Console.WriteLine($"{"".PadLeft(42)} ЦЕЛЕБНОЕ ЗЕЛЬЕ");
                        //Console.WriteLine(new string('■', 50));
                        Console.WriteLine(line.PadLeft(70));

                        chestName = "ЦЕЛЕБНОЕ ЗЕЛЬЕ";
                        Console.WriteLine("\n'Мгновенно восстанавливает все здоровье'\n");
                        if (player.HP < 100)
                        {
                            player.HP = 100;
                            Console.WriteLine("> Вы выпиваете зелье! \n> Здоровье полностью восстановлено!");
                        }
                        else
                        {
                            Console.WriteLine("> Ваше здоровье уже полное! Зелье не нужно.");
                        }
                        player.InfoHp();
                        break;
                    }
                case 1:
                    {
                        int RandAttackWeapon = random.Next(1, 25);
                        if (RandAttackWeapon <= 5) chestName = "РЖАВЫЙ МЕЧ";
                        else if (RandAttackWeapon <= 12) chestName = "СТАЛЬНОЙ МЕЧ";
                        else if (RandAttackWeapon <= 16) chestName = "БАНАНОВЫЙ ПИСТОЛЕТ";
                        else if (RandAttackWeapon <= 18) chestName = "МАМИНА ШЛЕПАНКА";
                        else if (RandAttackWeapon <= 22) chestName = "ОГНЕННЫЙ КЛИНОК";
                        else if (RandAttackWeapon <= 25) chestName = "МОЛОТ ТОРА";
                        else chestName = "ОТЦОВСКИЙ РЕМЕНЬ";
                        Console.WriteLine(line.PadLeft(70));
                        Console.WriteLine($"{"".PadLeft(36)} {chestName} (АТАКА: {RandAttackWeapon})");
                        //Console.WriteLine(new string('■', 50));
                        Console.WriteLine(line.PadLeft(70));

                        //Console.WriteLine($"\nАТАКА: {RandAttackWeapon}\n");
                        int sizeBox = 36;
                        Console.WriteLine("\n\n┌───────────── СРАВНЕНИЕ ─────────────┐");
                        Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
                        Console.WriteLine($"| {$"Текущее: {player.InfoWeapon()}".PadRight(sizeBox)}|");
                        Console.WriteLine($"| {$"Новое: {chestName} (АТК: {RandAttackWeapon})".PadRight(sizeBox)}|");
                        Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
                        Console.WriteLine("└─────────────────────────────────────┘");

                        char choice = 'о';
                        while (choice != 'в')
                        {
                            Console.Write("\n[В] Взять новый предмет\n[О] Оставить старый\n> ");
                            if (char.TryParse(Console.ReadLine().ToLower(), out choice) && (choice == 'о' || choice == 'в'))
                            {
                                break;
                            }
                        }
                        switch (choice)
                        {
                            case 'в':
                                {
                                    player.AttackWeapon = RandAttackWeapon;
                                    player.NameWeapon = chestName;
                                    Console.WriteLine($"\nВы экипировали: {chestName}");
                                    player.PlayerInfo();
                                    break;
                                }
                            case 'о':
                                {
                                    Console.WriteLine("\nВы с сожалением оставляете предмет в сундуке...");
                                    break;
                                }
                        }

                        break;
                    }
                case 2: 
                    { 
                        chestName = "";
                        int RandArmor = random.Next(1, 30);
                        if (RandArmor <= 7) chestName = "КРОССОВКИ 'АБИБАС'";
                        else if (RandArmor <= 10) chestName = "ПИЖАМА ШЕЛДОНА";
                        else if (RandArmor <= 14) chestName = "ПЛАЩ ГАРРИ ПОТТЕРА";
                        else if (RandArmor <= 18) chestName = "БРОНЯ ЖЕЛЕЗНОГО ЧЕЛОВЕКА";
                        else if (RandArmor <= 23) chestName = "МАГИЧЕСКАЯ КАРТА ТИНЬКОФФ";
                        else chestName = "КУРТКА БЭТМЕНА";

                        Console.WriteLine(line.PadLeft(70));
                        Console.WriteLine($"{"".PadLeft(37)} {chestName} (ЗАЩИТА: {RandArmor})");
                        Console.WriteLine(line.PadLeft(70));

                        int sizeBox = 46;
                        Console.WriteLine("\n\n┌────────────────── СРАВНЕНИЕ ──────────────────┐");
                        Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
                        Console.WriteLine($"| {$"Текущее: {player.InfoArmor()}".PadRight(sizeBox)}|");
                        Console.WriteLine($"| {$"Новое: {chestName} (ЗАЩ: {RandArmor})".PadRight(sizeBox)}|");
                        Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
                        Console.WriteLine("└───────────────────────────────────────────────┘");


                        char choice = 'о';

                        while (choice != 'в')
                        {
                            Console.Write("\n[В] Взять новый предмет\n[О] Оставить старый\n> ");
                            if (char.TryParse(Console.ReadLine().ToLower(), out choice) && (choice == 'о' || choice == 'в'))
                            {
                                break;
                            }
                        }
                        switch (choice)
                        {
                            case 'в':
                                {
                                    player.Armor = RandArmor;
                                    player.NameArmor = chestName;
                                    Console.WriteLine($"\nВы экипировали: {chestName}");
                                    player.PlayerInfo();
                                    break;
                                }
                            case 'о':
                                {
                                    Console.WriteLine("\nВы с сожалением оставляете предмет в сундуке...");
                                    break;
                                }
                        }
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
            //IsChest = false;
            Console.WriteLine("\n\nНажмите Enter, чтобы начать...");
            Console.ReadLine();
        }
    }
}