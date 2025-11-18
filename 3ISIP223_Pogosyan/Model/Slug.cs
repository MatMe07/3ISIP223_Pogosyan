using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class Slug : Enemy
    {
        public double ProcentKritAttack { get; set; }
        public bool IsKritAttack { get; set; }
        public bool KritAttack => random.Next(0, 100) < ProcentKritAttack;

        public Slug(string name, double attack, double defense, double hp) : base(name, attack, defense, hp)
        {
            ProcentKritAttack = 15;
            UniqSkill = "Шанс критического удара";
            IsKritAttack = false;
        }
    }
}
