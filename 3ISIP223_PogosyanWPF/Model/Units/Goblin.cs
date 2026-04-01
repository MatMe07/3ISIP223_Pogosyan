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

        public bool KritAttack => random.Next(0, 100) < ProcentKritAttack;

        public Goblin(string name, double attack, double defense, double hp, ModelUIElement3D mod, string imgQuiet, string imgAttack) : base(name, attack, defense, hp, mod, imgQuiet, imgAttack)
        {
            ProcentKritAttack = 15;
            UniqSkill = "Шанс критического удара";
            IsKritAttack = false;
            //timeAttack = new System.Windows.Threading.DispatcherTimer();
            //double time = RandomCLS.Next(4000, 10000);
            //timeAttack.Interval = TimeSpan.FromMilliseconds(time);
            //timeAttack.Tick += TimeAttack_Tick;
            //timeAttack.Start();

        }

        private void TimeAttack_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Attack Enem");
            //WorkGame.Game.AttackEnemy(this);
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

        public override string LastWord()
        {
            Console.WriteLine("Гоблин издает предсмертный визг и падает замертво.");
            return "Гоблин издает предсмертный визг и падает замертво.";
        }

    }
}
