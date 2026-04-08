using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace _3ISIP223_PogosyanWPF.Model
{
    public class Enemy
    {
        //public Random random = new Random();

        public string Name { get; set; }
        public ModelUIElement3D model {  get; set; }
        //protected DispatcherTimer timeAttack {  get; set; }

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
        //public double _TimeLastAttack { get; set; }
        public DateTime LastAtTime { get; set; }
        public double AttackIntervalSec { get; set; }

        public Enemy(string name, double attack, double defense, double hp, ModelUIElement3D mod, string pathQuiet, string pathAttack)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            HP = hp;
            MaxHP = hp;
            model = mod;
            //TimeLastAttack = ;
            //_TimeLastAttack = TimeLastAttack;
            AttackIntervalSec = RandomCLS.Next(2, 6);
            LastAtTime = DateTime.Now;
            materialQuiet = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(pathQuiet))));
            materialAttack = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri(pathAttack))));
            //materialQuiet = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Icons/Armor1.png"))));

        }


        public virtual void AttackInfo(double atack)
        {
            Console.WriteLine("VragAtrackuet");
        }

        public void AnimLogirTextInfo(double attack, string TextAttack = "(_)", bool PlayerIsBlock = false, int krit = 0)
        {
            
            Border borderLogir = GeneratedClass.LogirText("Player", $"{Name}", attack, false, true, TextAttack, PlayerIsBlock, EnemKrit:krit);
            Console.WriteLine($"{Name}: {attack} -> Player");

            MainWindow.LogerPanel.Children.Add(borderLogir);

            var animLogirText = new DoubleAnimation();
            animLogirText.From = 0;
            animLogirText.To = 1;
            animLogirText.Duration = TimeSpan.FromMilliseconds(500);

            animLogirText.Completed += (s, ea) =>
            {
                //var EndanimLogirText = new DoubleAnimation();
                animLogirText.From = 1;
                animLogirText.To = 0;
                animLogirText.Duration = TimeSpan.FromMilliseconds(1000);
                animLogirText.Completed += (s_end, aa) =>
                {
                    MainWindow.LogerPanel.Children.Remove(borderLogir);

                };
                borderLogir.BeginAnimation(Window.OpacityProperty, animLogirText);

            };

            borderLogir.BeginAnimation(Window.OpacityProperty, animLogirText);

            //attackingEnemies[enem] = DateTime.Now;

            Console.WriteLine($"Attack enem), attackInterval = {AttackIntervalSec}, attack = {Attack}");
        }

        public virtual void AnimAttackEnemyAndBoss(double attack, bool PlayerIsblock = false, int Krit = 0)
        {
            GeometryModel3D geom = model.Model as GeometryModel3D;
            geom.Material = materialAttack;
            //geom.Material = new DiffuseMaterial(new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Icons/Armor1.png"))));
            //timerAttackEnemy

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += (s, e) =>
            {
                geom.Material = materialQuiet;
                timer.Stop();
            };
            timer.Start();


            //if(PlayerIsblock)
            AnimLogirTextInfo(attack, PlayerIsBlock:PlayerIsblock, krit:Krit);

        }

        public void EnemyInfo()
        {
            
            Console.WriteLine($" | АТК: {Math.Round(Attack, 2)} | ЗАЩ: {Math.Round(Defense, 2)} | Особость: {UniqSkill}");
        }
        public virtual string LastWord()
        {
            Console.WriteLine("Umer");
            return "Umer";
        }

        public virtual (double attack, int Krit) EnemAttack(double playerArmor, bool playerFrozen = false)
        {
            return (-1, 0);
        }

        public virtual void Demo()
        {
            Console.WriteLine(Name);
        }
    }
}
