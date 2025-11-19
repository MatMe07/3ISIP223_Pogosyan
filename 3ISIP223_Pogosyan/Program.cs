
using _3ISIP223_Pogosyan.Model;
using System.Linq;
using System.Text;

namespace _3ISIP223_Pogosyan
{
    
    internal class Program
    {
        static void Main(string[] args)
        {
            char n = 'н';
            int gameStep = 0;
            while (n != 'в')
            {
                while (gameStep!= 0)
                {
                    Console.Write("\n[Н] Начать заново   [В] Выйти\n> ");
                    if (char.TryParse(Console.ReadLine().ToLower(), out n) && (n == 'в' || n == 'н'))
                    {
                        break;
                    }
                }
                switch (n)
                {
                    case 'н':
                        {
                            Game game = new Game();
                            game.GameStart();
                            gameStep++;
                            break;
                        }
                    case 'в':
                        {
                            break;
                        }
                }

            }

        }

    }


}