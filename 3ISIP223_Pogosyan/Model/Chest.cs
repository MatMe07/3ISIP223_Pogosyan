using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class Chest
    {
        private RandomCLS random;

        public Chest(RandomCLS randomCLS)
        {
            random = randomCLS;
        }

        public Item Open()
        {
            Console.WriteLine("В луче света вы замечаете старый сундук в углу пещеры...");
            Console.WriteLine("Вы открываете сундук и находите...\n");

            //string line = new string('═', 40);
            Thread.Sleep(900);

            int randChest = random.Next(0, 3);
            string chestName = "";
            switch (randChest)
            {
                case 0:
                    {
                        return new HealthItem("ЦЕЛЕБНОЕ ЗЕЛЬЕ");
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
                        return new WeaponItem(chestName, RandAttackWeapon);
                    }
                    case 2:
                    {
                        
                        int RandArmor = random.Next(1, 30);
                        if (RandArmor <= 7) chestName = "КРОССОВКИ 'АБИБАС'";
                        else if (RandArmor <= 10) chestName = "ПИЖАМА ШЕЛДОНА";
                        else if (RandArmor <= 14) chestName = "ПЛАЩ ГАРРИ ПОТТЕРА";
                        else if (RandArmor <= 18) chestName = "БРОНЯ ЖЕЛЕЗНОГО ЧЕЛОВЕКА";
                        else if (RandArmor <= 23) chestName = "МАГИЧЕСКАЯ КАРТА ТИНЬКОФФ";
                        else chestName = "КУРТКА БЭТМЕНА";
                        return new ArmorItem(chestName, RandArmor);
                    }
                default: {
                        return new HealthItem("ЦЕЛЕБНОЕ ЗЕЛЬЕ"); 
                    }
            }
        }
    }

    abstract class Item
    {
        public string Name;
        public string line = new string('═', 40);
        public Item(string name)
        {
            Name = name;
        }
        public abstract void StartItem(Player player);
    }

    class HealthItem : Item
    {
        public HealthItem(string name) : base(name)
        {
        }

        public override void StartItem(Player player)
        {
            Console.WriteLine(line.PadLeft(70));
            Console.WriteLine($"{"".PadLeft(42)} ЦЕЛЕБНОЕ ЗЕЛЬЕ");
            //Console.WriteLine(new string('■', 50));
            Console.WriteLine(line.PadLeft(70));

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
        }
    }

    class WeaponItem : Item
    {
        public double Attack;
        public WeaponItem(string name, double attack) : base(name) { Attack = attack; }
        public override void StartItem(Player player) {
            Console.WriteLine(line.PadLeft(70));
            Console.WriteLine($"{"".PadLeft(36)} {Name} (АТАКА: {Attack})");
            //Console.WriteLine(new string('■', 50));
            Console.WriteLine(line.PadLeft(70));

            //Console.WriteLine($"\nАТАКА: {RandAttackWeapon}\n");
            int sizeBox = 36;
            Console.WriteLine("\n\n┌───────────── СРАВНЕНИЕ ─────────────┐");
            Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
            Console.WriteLine($"| {$"Текущее: {player.InfoWeapon()}".PadRight(sizeBox)}|");
            Console.WriteLine($"| {$"Новое: {Name} (АТК: {Attack})".PadRight(sizeBox)}|");
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
                        player.AttackWeapon = Attack;
                        player.NameWeapon = Name;
                        Console.WriteLine($"\nВы экипировали: {Name}");
                        player.PlayerInfo();
                        break;
                    }
                case 'о':
                    {
                        Console.WriteLine("\nВы с сожалением оставляете предмет в сундуке...");
                        break;
                    }
            }
        }
    }


    class ArmorItem : Item
    {
        public double Armor;
        public ArmorItem(string name, double armor) : base(name)
        {
            Armor = armor;
        }
        public override void StartItem(Player player)
        {
            Console.WriteLine(line.PadLeft(70));
            Console.WriteLine($"{"".PadLeft(37)} {Name} (ЗАЩИТА: {Armor})");
            Console.WriteLine(line.PadLeft(70));

            int sizeBox = 46;
            Console.WriteLine("\n\n┌────────────────── СРАВНЕНИЕ ──────────────────┐");
            Console.WriteLine($"| {$" ".PadRight(sizeBox)}|");
            Console.WriteLine($"| {$"Текущее: {player.InfoArmor()}".PadRight(sizeBox)}|");
            Console.WriteLine($"| {$"Новое: {Name} (ЗАЩ: {Armor})".PadRight(sizeBox)}|");
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
                        player.Armor = Armor;
                        player.NameArmor = Name;
                        Console.WriteLine($"\nВы экипировали: {Name}");
                        player.PlayerInfo();
                        break;
                    }
                case 'о':
                    {
                        Console.WriteLine("\nВы с сожалением оставляете предмет в сундуке...");
                        break;
                    }
            }
        }
    }

}
