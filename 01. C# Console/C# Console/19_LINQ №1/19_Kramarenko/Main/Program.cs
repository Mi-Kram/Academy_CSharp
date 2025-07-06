using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseFile test = new ParseFile();
            test.Tasks1(@"C:\Users\Михаил\source\repos\19_Kramarenko\Main\Text.txt");
        }
    }

    class ParseFile
    {
        public void Tasks1(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    string file = File.ReadAllText(path);
                    file = Replace(file, ",.<>/?;:\'\"[]{}|/+*-=_()*&!@#$%^`~\r\n\t".ToCharArray(), ' ').ToLower();

                    string[] words = file.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    const int N = 3;
                    string[][] GenderWords = new string[N][]{
                        new string[]
                        {
                        "йы",
                        //"ого",
                        //"умо",
                        //"мы",
                        //"мо"
                        },

                        new string[]
                        {
                        "яа",
                        //"йо",
                        //"юу"
                        },

                        new string[]
                        {
                        "ео",
                        //"ого",
                        //"умо",
                        //"мы",
                        //"мо"
                        }
                    };

                    List<string>[] result = new List<string>[N];
                    for (int i = 0; i < N; i++)
                        result[i] = new List<string>();

                    foreach (var word in words)
                    {
                        for (int i = 0; i < N; i++)
                        {
                            foreach (var end in GenderWords[i])
                            {
                                if (result[i].Contains(word)) continue;

                                char[] tmp = word.ToCharArray();
                                Array.Reverse(tmp);
                                string reverse = new string(tmp);

                                if (reverse.IndexOf(end) == 0)
                                {
                                    result[i].Add(word);
                                    break;
                                }
                            }
                        }
                    }

                    string[] resFileNames = new string[N]
                    {
                        @"C:\Users\Михаил\source\repos\19_Kramarenko\Main\GenderNames\M.txt",
                        @"C:\Users\Михаил\source\repos\19_Kramarenko\Main\GenderNames\F.txt",
                        @"C:\Users\Михаил\source\repos\19_Kramarenko\Main\GenderNames\N.txt"
                    };

                    for (int i = 0; i < N; i++)
                    {
                        try
                        {
                            File.Create(resFileNames[i]).Close();
                            File.WriteAllLines(resFileNames[i], result[i].ToArray());
                        }
                        catch
                        {
                            Console.WriteLine($"{resFileNames[i]} do not work");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private string Replace(string str, char[] elems, char ch = ' ')
        {
            foreach (var item in elems)
            {
                str = str.Replace(item, ch);
            }

            return str;
        }
    }
}
