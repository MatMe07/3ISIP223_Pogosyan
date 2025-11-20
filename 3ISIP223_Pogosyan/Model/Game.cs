using _3ISIP223_Pogosyan.Model.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class Game
    {


        //public bool ChoiceChestOrEnemy => false;
        public bool ChoiceChestOrEnemy => RandomCLS.Next(0, 100) >= 50;

        public Player player;
        public Enemy Vrag = null;
        public Enemy Boss = null;
        public EnemyCreater vrags = null;
        public bool HaveEnemy => Vrag != null;
        public int step { get; set; }
        public int StepSpendWinOverBoss { get; set; }
        public double HPLostWinOverBoss { get; set; }
        public bool GameOver { get; set; }
        //public bool BosseStumles => true;
        public bool BosseStumles => step % 10 == 0;


        public Game()
        {
            string name = "Player1";
            player = new Player(name);
            step = 1;
            GameOver = false;
            StepSpendWinOverBoss = 0;
            HPLostWinOverBoss = 0;
            vrags = new EnemyCreater();
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
            if (BosseStumles || Boss != null)
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
                if (Boss != null && Boss.IsAlive)
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
                            RandEnemy = RandomCLS.Next(0, vrags.Enemies.Count);
                            Vrag = vrags.CreateEnemy(RandEnemy).CreateEnemy();

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
                        RandBosse = RandomCLS.Next(0, vrags.Bosses.Count);
                        Boss = vrags.CreateBosses(RandBosse).CreateEnemy();
                        //switch (RandBosse)
                        //{
                        //    case 0:
                        //        {
                        //            Boss = new VVG("ВВГ", 10, 3, 20);
                        //            break;
                        //        }
                        //    case 1:
                        //        {
                        //            Boss = new Covalski("Ковальский", 12, 2, 20);
                        //            break;
                        //        }
                        //    case 2:
                        //        {
                        //            Boss = new Arhimag("Архимаг C++", 14, 1, 15);
                        //            break;
                        //        }
                        //    case 3:
                        //        {
                        //            Boss = new Pestov("Пестов С--", 14, 1, 15);
                        //            break;
                        //        }
                        //}
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
            if (Boss != null) { StepSpendWinOverBoss++; }
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
            bool Def40 = RandomCLS.Next(1, 101) <= 40;
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
            Console.WriteLine("GAME OVER".PadLeft(box / 2));
            Console.WriteLine(new string('*', box));
            Console.WriteLine("\n\n");

            var sb = new StringBuilder();
            //sb.AppendLine($"┌ {"СТАТИСТИКА".Pa} ┐");
            sb.AppendLine("┌───────────── СТАТИСТИКА ─────────────┐");
            sb.AppendLine($"| {" ".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {$"Пройдено ходов: {player.Progress}".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {$"Повержено врагов: {player.CountAttackEnemies}".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {$"Открыто сундуков: {player.CountOpenChests}".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {$"Убито боссов: {player.CountAttackBosses}".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {" ".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {$"Последний враг: {player.LastEnemy}".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {$"Оружие: {player.NameWeapon}".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {$"Доспехи: {player.NameArmor}".PadRight(boxStat - 4)} |");
            sb.AppendLine($"| {" ".PadRight(boxStat - 4)} |");
            sb.AppendLine($"└{new string('─', boxStat - 2)}┘");
            Console.WriteLine(sb.ToString());
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
                    if (vrag is Slug)
                    {
                        attack = player.AttackWeapon - 2;
                    }
                    else if (player.IsBlock)
                    {
                        Console.WriteLine("Уклонение не удалось! Но вы подставляете свой щит...");
                        double randBlock = RandomCLS.Next(70, 101) / 100.0;
                        attack = Math.Max(0, vrag.Attack - (randBlock * player.Armor));
                        player.IsBlock = false;
                    }
                    else if (vrag is Goblin goblin && goblin.KritAttack)
                    {
                        goblin.IsKritAttack = true;
                        attack = Math.Max(0, vrag.Attack * 2 - player.Armor);
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

            Chest chest = new Chest();
            chest.Open().StartItem(player);

            //string chestName = "";
            //switch (ToolsOfChest)
            //{
            //    case 0:
            //        {
            //            Console.WriteLine(line.PadLeft(70));
            //            Console.WriteLine($"{"".PadLeft(42)} ЦЕЛЕБНОЕ ЗЕЛЬЕ");
            //            //Console.WriteLine(new string('■', 50));
            //            Console.WriteLine(line.PadLeft(70));

            //            chestName = "ЦЕЛЕБНОЕ ЗЕЛЬЕ";
            //            Console.WriteLine("\n'Мгновенно восстанавливает все здоровье'\n");
            //            if (player.HP < 100)
            //            {
            //                player.HP = 100;
            //                Console.WriteLine("> Вы выпиваете зелье! \n> Здоровье полностью восстановлено!");
            //            }
            //            else
            //            {
            //                Console.WriteLine("> Ваше здоровье уже полное! Зелье не нужно.");
            //            }
            //            player.InfoHp();
            //            break;
            //        }
            //    case 1:
            //        {
            //            int RandAttackWeapon = RandomCLS.Next(1, 25);
            //            if (RandAttackWeapon <= 5) chestName = "РЖАВЫЙ МЕЧ";
            //            else if (RandAttackWeapon <= 12) chestName = "СТАЛЬНОЙ МЕЧ";
            //            else if (RandAttackWeapon <= 16) chestName = "БАНАНОВЫЙ ПИСТОЛЕТ";
            //            else if (RandAttackWeapon <= 18) chestName = "МАМИНА ШЛЕПАНКА";
            //            else if (RandAttackWeapon <= 22) chestName = "ОГНЕННЫЙ КЛИНОК";
            //            else if (RandAttackWeapon <= 25) chestName = "МОЛОТ ТОРА";
            //            else chestName = "ОТЦОВСКИЙ РЕМЕНЬ";
            //            Console.WriteLine(line.PadLeft(70));
            //            Console.WriteLine($"{"".PadLeft(36)} {chestName} (АТАКА: {RandAttackWeapon})");
            //            //Console.WriteLine(new string('■', 50));
            //            Console.WriteLine(line.PadLeft(70));

            //            //Console.WriteLine($"\nАТАКА: {RandAttackWeapon}\n");
            //            int sizeBox = 36;
            //            Console.WriteLine("\n\n┌───────────── СРАВНЕНИЕ ─────────────┐");
            //            Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
            //            Console.WriteLine($"| {$"Текущее: {player.InfoWeapon()}".PadRight(sizeBox)}|");
            //            Console.WriteLine($"| {$"Новое: {chestName} (АТК: {RandAttackWeapon})".PadRight(sizeBox)}|");
            //            Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
            //            Console.WriteLine("└─────────────────────────────────────┘");

            //            char choice = 'о';
            //            while (choice != 'в')
            //            {
            //                Console.Write("\n[В] Взять новый предмет\n[О] Оставить старый\n> ");
            //                if (char.TryParse(Console.ReadLine().ToLower(), out choice) && (choice == 'о' || choice == 'в'))
            //                {
            //                    break;
            //                }
            //            }
            //            switch (choice)
            //            {
            //                case 'в':
            //                    {
            //                        player.AttackWeapon = RandAttackWeapon;
            //                        player.NameWeapon = chestName;
            //                        Console.WriteLine($"\nВы экипировали: {chestName}");
            //                        player.PlayerInfo();
            //                        break;
            //                    }
            //                case 'о':
            //                    {
            //                        Console.WriteLine("\nВы с сожалением оставляете предмет в сундуке...");
            //                        break;
            //                    }
            //            }

            //            break;
            //        }
            //    case 2:
            //        {
            //            chestName = "";
            //            int RandArmor = RandomCLS.Next(1, 30);
            //            if (RandArmor <= 7) chestName = "КРОССОВКИ 'АБИБАС'";
            //            else if (RandArmor <= 10) chestName = "ПИЖАМА ШЕЛДОНА";
            //            else if (RandArmor <= 14) chestName = "ПЛАЩ ГАРРИ ПОТТЕРА";
            //            else if (RandArmor <= 18) chestName = "БРОНЯ ЖЕЛЕЗНОГО ЧЕЛОВЕКА";
            //            else if (RandArmor <= 23) chestName = "МАГИЧЕСКАЯ КАРТА ТИНЬКОФФ";
            //            else chestName = "КУРТКА БЭТМЕНА";

            //            Console.WriteLine(line.PadLeft(70));
            //            Console.WriteLine($"{"".PadLeft(37)} {chestName} (ЗАЩИТА: {RandArmor})");
            //            Console.WriteLine(line.PadLeft(70));

            //            int sizeBox = 46;
            //            Console.WriteLine("\n\n┌────────────────── СРАВНЕНИЕ ──────────────────┐");
            //            Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
            //            Console.WriteLine($"| {$"Текущее: {player.InfoArmor()}".PadRight(sizeBox)}|");
            //            Console.WriteLine($"| {$"Новое: {chestName} (ЗАЩ: {RandArmor})".PadRight(sizeBox)}|");
            //            Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
            //            Console.WriteLine("└───────────────────────────────────────────────┘");


            //            char choice = 'о';

            //            while (choice != 'в')
            //            {
            //                Console.Write("\n[В] Взять новый предмет\n[О] Оставить старый\n> ");
            //                if (char.TryParse(Console.ReadLine().ToLower(), out choice) && (choice == 'о' || choice == 'в'))
            //                {
            //                    break;
            //                }
            //            }
            //            switch (choice)
            //            {
            //                case 'в':
            //                    {
            //                        player.Armor = RandArmor;
            //                        player.NameArmor = chestName;
            //                        Console.WriteLine($"\nВы экипировали: {chestName}");
            //                        player.PlayerInfo();
            //                        break;
            //                    }
            //                case 'о':
            //                    {
            //                        Console.WriteLine("\nВы с сожалением оставляете предмет в сундуке...");
            //                        break;
            //                    }
            //            }
            //            break;
            //        }

            //    default:
            //        {
            //            break;
            //        }
            //}
            ////IsChest = false;
            Console.WriteLine("\n\nНажмите Enter, чтобы начать...");
            Console.ReadLine();
        }
    }

}
