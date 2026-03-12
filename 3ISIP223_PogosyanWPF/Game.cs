using _3ISIP223_PogosyanWPF.Model;
using _3ISIP223_PogosyanWPF.Model.Creater;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_PogosyanWPF
{
    public class Game : INotifyPropertyChanged
    {
        public bool ChoiceChestOrEnemy => RandomCLS.Next(0, 100) >= 50;

        private Player _player;

        public Player Player
        {
            get { return _player; }
            set {
                _player = value; 
                OnPropertyChanged(nameof(Player));
                }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EnemyCreater _vrags = null;
        public EnemyCreater Vrags
        {
            get { return _vrags; }
            set
            {
                _vrags = value;
                OnPropertyChanged(nameof(Vrags));
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //public Enemy Vrag = null;
        //public Enemy Boss = null;
        //public EnemyCreater vrags = null;
        //public bool HaveEnemy => Vrag != null;
        private int _step;
        public int step
        {
            get { return _step; }
            set
            {
                _step = value;
                OnPropertyChanged(nameof(step));
            }
        }
        public int StepSpendWinOverBoss { get; set; }
        public double HPLostWinOverBoss { get; set; }
        public bool GameOver { get; set; }
        //public bool BosseStumles => true;
        public bool BosseStumles => step % 10 == 0;

        public Game()
        {
            string name = "Player1";
            Player = new Player(name);
            Vrags = new EnemyCreater();
            step = 1;
            GameOver = false;
            StepSpendWinOverBoss = 0;
            HPLostWinOverBoss = 0;
            //vrags = new EnemyCreater();
        }



    }


    public static class WorkGame
    {
        private static Game game = new Game();

        public static Game Game { get { return game; } }
    }
}
