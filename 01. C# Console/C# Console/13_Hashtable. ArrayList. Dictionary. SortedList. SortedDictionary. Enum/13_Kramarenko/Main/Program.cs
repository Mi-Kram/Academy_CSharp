using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Diagnostics.Contracts;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasks.Task1();
            Tasks.Task2();

            End();
        }

        static void End()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(46, 12);
            Console.Write("                         ");

            Console.SetCursorPosition(46, 13);
            Console.Write("   Программа завершена   ");

            Console.SetCursorPosition(46, 14);
            Console.Write("                         ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
        }
    }

    class Tasks
    {
        public static void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. Разработать программу-переводчик, которая имеет следующие функции: ");
            Console.WriteLine("        - Add a word                                                      ");
            Console.WriteLine("        - Show words                                                      ");
            Console.WriteLine("        - Remove word                                                     ");
            Console.WriteLine("        - Translate word                                                  ");
            Console.WriteLine("        - Translate a sentense                                            ");
            Console.WriteLine("        - Save dictionary                                                 ");
            Console.WriteLine("        - Load dictionary                                                 \n\n\n");
            Console.ResetColor();

            try
            {
                string fName = @"C:\Users\Михаил\source\repos\13_Kramarenko\Main\Task1\Dic.txt";
                Dic d = new Dic();

                d.Add("hello",  "привет");
                d.Add("ball",   "мяч");
                d.Add("play",   "играть");
                d.Add("world",  "мир");
                d.Add("fire",   "огонь");
                d.Show();
                Console.ReadKey(true);

                Console.WriteLine("\n\nTranslate word:");
                string word = "привет";
                Console.WriteLine($"{word}  -  {d.TranslateWord(word)}");

                word = "world";
                Console.WriteLine($"{word}  -  {d.TranslateWord(word)}");
                Console.ReadKey(true);

                Console.WriteLine("\n\nTranslate sentense:");
                string sentense = "привет мир";
                Console.WriteLine($"{sentense}  -  {d.TranslateSentense(sentense)}");

                sentense = "играть ball";
                Console.WriteLine($"{sentense}  -  {d.TranslateSentense(sentense)}");
                Console.ReadKey(true);

                d.Save(fName);
                Console.WriteLine("\n\nDictionary saved!");

                Dic test = new Dic();
                test.Load(fName);
                Console.WriteLine("new Dictionary Load!\n\n");

                test.Show();
                Console.ReadKey(true);

                test.Remove("ball");
                test.Remove("играть");

                Console.WriteLine("\n\n");
                test.Show();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        public static void Task2()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task2. Пользователь вводит имя файла со словами, записанными в столбик и путь к папке с текстами    ");
            Console.WriteLine("       Программа для каждого слова из файла слов вычисляет сколько раз слово встречается в заданных ");
            Console.WriteLine("       текстах и сохраняет слова, отсортированные по частоте                                        \n\n\n");
            Console.ResetColor();

            try
            {
                string config = @"C:\Users\Михаил\source\repos\13_Kramarenko\Main\Task2\config.txt";
                string dir = @"C:\Users\Михаил\source\repos\13_Kramarenko\Main\Task2\Dir";
                string result = @"C:\Users\Михаил\source\repos\13_Kramarenko\Main\Task2\result.txt";

                Words w = new Words(dir, config, result);
                w.WriteToFileCountOfWords();

                Console.WriteLine("Задание выполнено");
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    class Dic
    {
        Dictionary<string, string> EngRu;
        Dictionary<string, string> RuEng;

        public Dic()
        {
            EngRu = new Dictionary<string, string>();
            RuEng = new Dictionary<string, string>();
        }

        public void Add(string s1, string s2)
        {
            if (s1.Length == 0 || s2.Length == 0) throw new Exception("Error strings");

            s1 = s1.ToLower();
            s2 = s2.ToLower();

            if (IsEng(s1[0]))
            {
                EngRu[s1] = s2;
                RuEng[s2] = s1;
            }
            else
            {
                RuEng[s1] = s2;
                EngRu[s2] = s1;
            }
        }
        public void Remove(string s)
        {
            s = s.ToLower();

            if (EngRu.ContainsKey(s))
            {
                RuEng.Remove(EngRu[s]);
                EngRu.Remove(s);
            }
            else if (RuEng.ContainsKey(s))
            {
                EngRu.Remove(RuEng[s]);
                RuEng.Remove(s);
            }
            else
            {
                throw new Exception("Error string");
            }
        }

        public void Show()
        {
            int EngW = GetWEng();
            int RuW = GetWRu();

            Console.WriteLine("Dictionary:");
            foreach (var item in EngRu)
            {
                setw(EngW, item.Key, true);
                Console.WriteLine($"  -  {item.Value}");
            }
        }
        public string TranslateWord(string s)
        {
            s = s.ToLower();
            
            if (EngRu.ContainsKey(s))
            {
                return EngRu[s];
            }
            else if (RuEng.ContainsKey(s))
            {
                return RuEng[s];
            }
            else
            {
                throw new Exception($"Unknown word({s})");
            }
        }
        public string TranslateSentense(string s)
        {
            s = s.ToLower();
            
            string result = string.Empty;
            string[] words = s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (words.Length == 0) throw new Exception("Empty string");

            if (IsEng(words[0][0]))
            {
                foreach (var item in words)
                {
                    if (!EngRu.ContainsKey(item)) { result += item + ' '; break; }
                    result += TranslToRu(item);
                    result += ' ';
                }
            }
            else
            {
                foreach (var item in words)
                {
                    if (!RuEng.ContainsKey(item)) { result += item + ' '; break; }
                    result += TranslToEng(item);
                    result += ' ';
                }
            }

            return result.Remove(result.Length - 1);
        }

        private string TranslToRu(string s)
        {
            if (!EngRu.ContainsKey(s)) throw new Exception($"Unknown word({s})");
            return EngRu[s];
        }
        private string TranslToEng(string s)
        {
            if (!RuEng.ContainsKey(s)) throw new Exception($"Unknown word({s})");
            return RuEng[s];
        }

        public void Save(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            
            foreach (var item in EngRu)
            {
                sw.WriteLine($"{item.Key}&{item.Value}");
            }

            sw.Close();
        }
        public void Load(string path)
        {
            if (!File.Exists(path)) throw new Exception("Error file path");
            if (EngRu.Count != 0) EngRu.Clear();
            if (RuEng.Count != 0) RuEng.Clear();

            StreamReader sr = new StreamReader(path);
            string str = string.Empty;

            while (!sr.EndOfStream)
            {
                str = sr.ReadLine();
                var words = str.Split(new char[] { '&', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                Add(words[0], words[1]);
            }

            sr.Close();
        }


        private bool IsEng(char ch)
        {
            return ch >= 'A' && ch <= 'Z' || ch >= 'a' && ch <= 'z';
        }

        private int GetWEng()
        {
            int max = 0;

            foreach (var item in EngRu)
            {
                if (max < item.Key.Length) max = item.Key.Length;
            }

            return max;
        }
        private int GetWRu()
        {
            if (RuEng.Count == 0) return 0;
            int min = RuEng.First().Key.Length;

            foreach (var item in RuEng)
            {
                if (min > item.Key.Length) min = item.Key.Length;
            }

            return min;
        }
        private void setw(int w, object value, bool side)
        {
            int tmp = 1;
            for (int i = 0; i < w; i++)
            {
                tmp *= 10;
            }

            string str = value.ToString();
            int length = (int)w - str.Length;

            if (!side)
            {
                for (int i = 0; i < length; i++)
                {
                    Console.Write(' ');
                }
            }

            Console.Write(str);

            if (side)
            {
                for (int i = 0; i < length; i++)
                {
                    Console.Write(' ');
                }
            }
        }

    }

    class Words
    {
        string dir;
        string config;
        string result;

        Dictionary<string, int> wordsCount;

        public Words(string d, string c, string r)
        {
            dir = d;
            config = c;
            result = r;
            wordsCount = new Dictionary<string, int>();
        }

        public void WriteToFileCountOfWords()
        {
            GetWordsCount();

            Write();
        }

        private void GetWordsCount()
        {
            if (!File.Exists(config)) throw new Exception("Error path to config file");
            DirectoryInfo d = new DirectoryInfo(dir);
            if (!d.Exists) throw new Exception("Error path to dir");

            string[] words = File.ReadAllText(config).Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in words)
            {
                wordsCount[item] = 0;
            }

            var files = d.GetFiles("*.txt");
            foreach (var file in files)
            {
                words = File.ReadAllText(file.FullName).Split(" \r\n\t.,./;'[]-=*+!@#№$%^&()_{}\\|:\"<>?~`".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in words)
                {
                    if (wordsCount.ContainsKey(word)) wordsCount[word]++;
                }
            }
        }
        private void Write()
        {
            wordsCount = wordsCount.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);

            StreamWriter sw = new StreamWriter(result);

            foreach (var item in wordsCount)
            {
                sw.WriteLine($"{item.Value}: {item.Key}");
            }

            sw.Close();
        }
    }

}
