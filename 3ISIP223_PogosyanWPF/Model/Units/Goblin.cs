using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace _3ISIP223_PogosyanWPF.Model.Units
{
    internal class Goblin : Enemy
    {
        public double ProcentKritAttack { get; set; }
        public bool IsKritAttack { get; set; }

        public bool KritAttack => RandomCLS.Next(0, 100) < ProcentKritAttack;

        public Goblin(string name, double attack, double defense, double hp, ModelUIElement3D mod, string imgQuiet, string imgAttack, string imgPoluch) : base(name, attack, defense, hp, mod, imgQuiet, imgAttack, imgPoluch)
        {
            ProcentKritAttack = 20;
            UniqSkill = "Шанс критического удара";
            IsKritAttack = false;

        }

        private void TimeAttack_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Attack Enem");
            //WorkGame.Game.AttackEnemy(this);
        }


        public bool TryKrit()
        {
            return KritAttack;
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

        public override (double attack, int Krit) EnemAttack(double playerArmor, bool playerFrozen = false)
        {
            if (TryKrit())
            {
                IsKritAttack = true;
                return ( Math.Max(0, Attack * 2 - playerArmor), -2);
            }
            return (Math.Max(0, Attack - playerArmor), 0);
        }

        public override string LastWord()
        {
            Console.WriteLine("Гоблин издает предсмертный визг и падает замертво.");
            return "Гоблин издает предсмертный визг и падает замертво.";
        }

    }
}
