using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace _3ISIP223_PogosyanWPF.Model
{
    public class Enemy
    {
        public Random random = new Random();

        public string Name { get; set; }
        public ModelUIElement3D model {  get; set; }
        protected DispatcherTimer timeAttack {  get; set; }

        public double HP { get; set; }
        public double MaxHP { get; set; }
        public double Attack { get; set; }
        public double Defense { get; set; }
        public bool HaveDefense => Defense > 0;
        public bool IsAlive => HP > 0;
        public string UniqSkill { get; set; }

        public DiffuseMaterial materialQuiet {  get; set; }
        public DiffuseMaterial materialAttack {  get; set; }
        public double TimeLastAttack { get; set; }
        public double AttackIntervalSec { get; set; }

        public Enemy(string name, double attack, double defense, double hp, ModelUIElement3D mod, string pathQuiet, string pathAttack)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            HP = hp;
            MaxHP = hp;
            model = mod;
            TimeLastAttack = 0;
            AttackIntervalSec = RandomCLS.Next(4, 10);
            materialQuiet = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(pathQuiet))));
            materialAttack = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(pathAttack))));
            //materialQuiet = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Icons/Armor1.png"))));

        }


        public virtual void AttackInfo(double atack)
        {
            Console.WriteLine("VragAtrackuet");
        }

        public void EnemyInfo()
        {
            
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
}
