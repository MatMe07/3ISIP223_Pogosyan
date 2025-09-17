using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //string txt = Console.ReadLine();
            int slov = 0;
            int n = 0;
            do
            {
                Console.Clear();
                Console.Write("Выберите действие:\n\n1. Ввод нового текста\n2. Вывод количества слов в тексте.\n3. Поиск самого короткого слова\n4. Подсчёт количества предложений\n5. Подсчёт количества гласных и согласных букв\n6. Поиск самого длинного слова\n7. Создание статистики по частоте встречаемости каждой буквы\n0. Выйти из программы\n\nВведите номер команды: ");
                n = Convert.ToInt32(Console.ReadLine());

                switch (n) 
                {
                    
                }


            }
            while (n != 0);

        }

        class Text
        {
            public string text;
            public int countWords;
            public string shortWord;
            public int countSentences;
            public int countGlasnie;
            public int countSoglasnie;
            public string longWord;
            public Dictionary<char, int > letterMap;


            public Text(string text)
            {
                this.text = text;
                Stat();
            }

            public void Stat()
            {
                countWords = 1;
                string znakKonza = ".?!";
                string word = "";
                string[] abzac = word.Split(new char[] { '\n' });

                foreach (string predl in abzac) { 
                    for (int i = 0; i < predl.Length; i++)
                    {
                        if (text[i] == ' ')
                        {
                            countWords++;
                            if (countWords == 2) shortWord = word;
                            else
                            {
                                if (word.Length < shortWord.Length)
                                {
                                    shortWord = word;
                                }
                                 word = "";
                            }
                        }
                        else
                        {
                            word += text[i];
                        }


                        foreach (char c in znakKonza)
                        {
                            if (c == predl[i])
                            {

                            }
                        }

                    }

                }
                
                }
                
        }

    }


}
