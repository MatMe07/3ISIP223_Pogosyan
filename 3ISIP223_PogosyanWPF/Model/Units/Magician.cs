using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Units
{
    public class Magician : Enemy
    {
        public double ProcentFrozen { get; set; }
        public bool IsFroz { get; set; }
        public bool FrozenProc {
            get
            {
                int randZ = RandomCLS.Next(0, 100);
                Console.WriteLine("RandZ = {0}", randZ);
                return randZ < ProcentFrozen;
            }
            set { }
            }

        public bool Frozen()
        {
            //(!IsFroz && random.Next(0, 101) <= ProcentFrozen);
            if (!IsFroz)
            {
                return FrozenProc;
            }
            return false;
        }

        public Magician(string name, double attack, double defense, double hp, ModelUIElement3D mod, string imgQuiet, string imgAttack) : base(name, attack, defense, hp, mod, imgQuiet, imgAttack)
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

        public bool TryFreeze()
        {
            Console.WriteLine($"\n{Name} произносит древнее заклинание, он пытается заморозить вас...");

            if (Frozen())
            {
                Console.WriteLine("Маг заморозил вас!");
                return true;
            }
            Console.WriteLine("Магу не удалось заморозить");
            return false;
        }

        public override double EnemAttack(double playerArmor, bool playerFrozen = false)
        {
            if (!playerFrozen)
            {
                if (TryFreeze())
                {
                    //Player.IsFrozen = true;
                    return -1;
                }
                else
                    return Math.Max(0, Attack - playerArmor);

            }
            return Math.Max(0, Attack - playerArmor);
        }

        public override string LastWord()
        {
            Console.WriteLine($"{Name} издает последнее заклинание, которое рассеивается вместе с его жизнью.");
            return $"{Name} издает последнее заклинание, которое рассеивается вместе с его жизнью.";
        }
    }
}
