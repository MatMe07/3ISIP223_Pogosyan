using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _3ISIP223_Pogosyan
{
    internal class Program
    {
        public static List<Text> lstText = new List<Text>();
        static void Main(string[] args)
        {
            //string txt = Console.ReadLine();
            int slov = 0;
            int n = 0;
            string str = "";
            Text txt = new Text(str);

            do
            {
                Console.Clear();
                Console.Write("Выберите действие:\n\n1. Ввод нового текста. \n2. Вывод количества слов в тексте. \n3. Поиск самого короткого слова. \n4. Подсчёт количества предложений. \n5. Подсчёт количества гласных и согласных букв. \n6. Поиск самого длинного слова. \n7. Создание статистики по частоте встречаемости каждой буквы. \n8. Вывод статистики по прошлым текстам. \n0. Выйти из программы. \n\nВведите номер команды: ");
                n = Convert.ToInt32(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        {
                            Console.WriteLine("Введите новый текст: ");
                            str = Console.ReadLine();
                            txt = new Text(str);
                            lstText.Add(txt);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine($"Количество слов в тексте = {txt.countWords}");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine($"Самое короткое слово в тексте = {txt.shortWord}");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine($"Количество предложений в тексте = {txt.shortWord}");
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine($"Количество гласных в тексте = {txt.countGlasnie};");
                            Console.WriteLine($"Количество согласных в тексте = {txt.countSoglasnie};");
                            break;
                        }
                    case 6:
                        {
                            Console.WriteLine($"Самое длинное слово в тексте = {txt.longWord};");
                            break;
                        }
                }


            }
            while (n != 0);

        }

        public class Text
        {
            public string text;
            public int countWords;
            public string shortWord;
            public int countSentences;
            public int countGlasnie;
            public int countSoglasnie;
            public string longWord;
            public Dictionary<string, int> letterMap;

            public Text(string text)
            {
                this.text = text;
                countSentences = 0;
                countGlasnie = 0;
                countSoglasnie = 0;

                countWords = text.Length > 0 ? 1 : 0;
                letterMap = new Dictionary<string, int>();
                Stat();
            }

            public void Stat()
            {
                string word = "";
                string simvol;
                
                for (int i = 0; i < text.Length; i++)
                {
                    if (text[i] == ' ')
                    {
                        countWords++;
                        //Console.WriteLine(word);
                        if (countWords == 2) { shortWord = word; longWord = word; }
                        else
                        {
                            if (word.Length < shortWord.Length )
                            {
                                shortWord = word;
                            }
                            if (word.Length > longWord.Length )
                            {
                                longWord = word;
                            }
                        }
                        word = "";
                    }
                    else
                    {
                        simvol =  text[i].ToString();
                        if (!Regex.IsMatch(simvol, "[.,:;?!-]")) { 
                            word += text[i]; 
                            if (!letterMap.ContainsKey(simvol.ToLower()))
                            {
                                letterMap.Add(simvol.ToLower(), 1);
                            }
                            else
                            {
                                letterMap[simvol.ToLower()]++;
                            }
                            if (Regex.IsMatch(simvol.ToLower(), "[аоуэиыеёюя]")) countGlasnie++;
                            if (Regex.IsMatch(simvol.ToLower(), "[бвгджзйклмнпрстфхцчшщ]")) countSoglasnie++;
                        }
                        if (Regex.IsMatch(simvol, "[.?!]") && text[i - 1].ToString().ToUpper() != text[i - 1].ToString()) countSentences++;

                    }

                }
                Console.WriteLine(countSentences);
                Console.WriteLine(countGlasnie);
                Console.WriteLine(countSoglasnie);
                foreach(KeyValuePair<string, int> ent in letterMap)
                {
                    Console.WriteLine($"{ent.Key} = {ent.Value}");
                }

                
                
                }
                
        }

    }


}
