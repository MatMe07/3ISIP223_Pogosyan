using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF.Model.Creater
{
    public class EnemyCreater
    {
        public List<Factory> Enemies { get; set; }
        public List<Factory> Bosses { get; set; }

        public EnemyCreater()
        {
            Enemies = new List<Factory>();
            AddEnemy(new CreaterGoblin());
            //AddEnemy(new CreaterMag());
            //AddEnemy(new CreaterSkeleton());

            Bosses = new List<Factory>();
            AddBoss(new CreaterArhimag());
            AddBoss(new CreaterCovalski());
            AddBoss(new CreaterPestov());
            AddBoss(new CreaterVVG());

        }

        public void AddEnemy(Factory enemy)
        {
            Enemies.Add(enemy);
        }
        public void AddBoss(Factory enemy)
        {
            Bosses.Add(enemy);
        }

        public Factory CreateEnemy(int n)
        {
            return Enemies[n];
        }
        public Factory CreateBosses(int n)
        {
            return Bosses[n];
        }
    }
}
