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
            string str = " ";
            Text txt = new Text(str);

            do
            {
                Console.Clear();
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
                            Console.WriteLine($"Количество предложений в тексте = {txt.countSentences}");
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
                    case 7:
                        {
                            Console.WriteLine($"Cтатистика по частоте встречаемости каждой буквы: ");
                            foreach (KeyValuePair<string, int> ent in txt.letterMap)
                            {
                                Console.WriteLine($"{ent.Key} = {ent.Value}");
                            }
                            break;
                        }
                    case 8:
                        {
                            Console.WriteLine("Прошлые тексты:");
                            for (int i = 0; i < lstText.Count; i++)
                            {
                                Console.WriteLine($"{i+1}. {lstText[i].FirstWord}...");
                            }
                            Console.Write($"Выберите текст: ");
                            int d = Convert.ToInt32( Console.ReadLine() ); d--;
                            if (d > lstText.Count)
                            {
                                Console.WriteLine("Некорректный ввод данных!");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine($"Количество слов в тексте = {lstText[d].countWords}");
                                Console.WriteLine($"Самое короткое слово в тексте = {lstText[d].shortWord}");
                                Console.WriteLine($"Количество предложений в тексте = {lstText[d].countSentences}");
                                Console.WriteLine($"Количество гласных в тексте = {lstText[d].countGlasnie};");
                                Console.WriteLine($"Количество согласных в тексте = {lstText[d].countSoglasnie};");
                                Console.WriteLine($"Самое длинное слово в тексте = {lstText[d].longWord};");
                                Console.WriteLine($"Cтатистика по частоте встречаемости каждой буквы: ");
                                foreach (KeyValuePair<string, int> ent in lstText[d].letterMap)
                                {
                                    Console.WriteLine($"{ent.Key} = {ent.Value}");
                                }

                            }
                            break;
                        }
                }
                Console.Write("Нажмите Enter!");
                Console.ReadLine();


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
            public string FirstWord;
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
                        if (countWords == 2) { shortWord = word; longWord = word; FirstWord = word; }
                        else
                        {
                            if (word.Length > 0 && word.Length < shortWord.Length )
                            {
                                shortWord = word;
                            }
                            if (word.Length > 0 && word.Length > longWord.Length )
                            {
                                longWord = word;
                            }
                        }
                        word = "";
                    }
                    else
                    {
                        simvol =  text[i].ToString();
                        if (!Regex.IsMatch(simvol, "[.,:;?!\\-\"]") && simvol != "\\") { 
                            word += text[i]; 
                            if (!letterMap.ContainsKey(simvol.ToLower()))
                            {
                                letterMap.Add(simvol.ToLower(), 1);
                            }
                            else
                            {
                                letterMap[simvol.ToLower()]++;
                            }
                            if (Regex.IsMatch(simvol.ToLower(), "[аоуэиыеёюiя]")) countGlasnie++;
                            if (Regex.IsMatch(simvol.ToLower(), "[a-zа-я]")) countSoglasnie++;
                        }
                        if (Regex.IsMatch(simvol, "[.?!]") && text[i - 1].ToString().ToUpper() != text[i - 1].ToString()) countSentences++;

                    }

                }

                countSoglasnie -= countGlasnie;
                }
                
        }

    }


}
