using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan.Model
{
    internal class EnemyCreater
    {
        public List<Factory> Enemies {  get; set; }
        public List<Factory> Bosses {  get; set; }

        public EnemyCreater()
        {
            Enemies = new List<Factory>();
            Enemies.Add(new CreaterGoblin());
            Enemies.Add(new CreaterMag());
            Enemies.Add(new CreaterSkeleton());
        }

        public void AddEnemy(Factory enemy)
        {
            Enemies.Add(enemy);
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
