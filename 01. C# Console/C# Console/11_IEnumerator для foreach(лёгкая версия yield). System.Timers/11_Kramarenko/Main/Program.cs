using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasks.Task1();
            Tasks.Task2();
            Tasks.Task3();




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
            Console.WriteLine("Task1. Разработать класс MyDate, который принимает год и месяц и в цикле ");
            Console.WriteLine("       foreach возвращает дни этого месяца                               \n\n\n");
            Console.ResetColor();

            try
            {
                MyDate md = new MyDate(9, 2020);

                foreach (var item in md)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}\b!");
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
            Console.WriteLine("Task2. Разработать класс MyDate, который принимает год и месяц и в цикле ");
            Console.WriteLine("       foreach возвращает дни этого месяца                               \n\n\n");
            Console.ResetColor();

            try
            {
                string config = @"C:\Users\Михаил\source\repos\11_Kramarenko\Main\Task2\config.txt";
                string path = @"C:\Users\Михаил\source\repos\11_Kramarenko\Main\Task2\Dir";

                DelFiles df = new DelFiles(path, config, 1000);

                df.Start();
                Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
                Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                Console.ReadLine();
            }

            Console.Clear();
        }
        public static void Task3()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task3. Программа сканирует по таймеру 1 раз в сек. определённую папку и её подпапки и копирует все       ");
            Console.WriteLine("       картинки в папку BackUp. Файлы, которые были скопированы в прошлое срабатывание таймера больше не ");
            Console.WriteLine("       копируются. Если в папке BackUp имена повторяются, то имя нового файла должно быть изменено       \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\11_Kramarenko\Main\Task3\Dir";
                string backUp = @"C:\Users\Михаил\source\repos\11_Kramarenko\Main\Task3\BackUp";

                CopyFiles cf = new CopyFiles(path, backUp, 1000);
                cf.Start();
                Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                Console.ReadKey();

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task3 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
                Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                Console.ReadLine();
            }

            Console.Clear();
        }
    }

    class MyDate
    {
        int daysCount;

        public MyDate(int month, int year)
        {
            if (month < 1 || month > 12) throw new Exception("Error month");
            if (year < 1) throw new Exception("Error year");

            daysCount = DateTime.DaysInMonth(year, month);
        }
        public IEnumerator GetEnumerator()
        {
            for (int i = 1; i <= daysCount; i++)
            {
                yield return i;
            }
        }
    }
    class DelFiles
    {
        string path;
        string config;
        string[] extensions;
        static Timer t;

        public DelFiles(string p, string c, int interval)
        {
            if (!File.Exists(c)) throw new Exception("Config file is not exist");
            if (!Directory.Exists(p)) throw new Exception("Dir is not exist");
            
            path = p;
            config = c;

            extensions = File.ReadAllLines(config);
            if (extensions.Length == 0) throw new Exception("Config file is empty");

            t = new Timer(interval);
            t.Elapsed += T_Elapsed;
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            DelFileOfExtensions(path, extensions);
        }

        private int DelFileOfExtensions(string path, string[] exts)
        {
            if (DelFile(path, exts) == 1) return 1;

            var dirs = new DirectoryInfo(path).GetDirectories();
            foreach (var item in dirs)
            {
                if (DelFileOfExtensions(item.FullName, exts) == 1) return 1;
            }

            return -1;
        }
        private int DelFile(string path, string[] exts)
        {
            DirectoryInfo d = new DirectoryInfo(path);

            foreach (var ext in exts)
            {
                var files = d.GetFiles("*" + ext);
                if (files.Length > 0)
                {
                    File.Delete(files[0].FullName);
                    return 1;
                }
            }

            return -1;
        }

        public void Start()
        {
            t.Start();
        }
    }
    class CopyFiles
    {
        string path;
        string backUp;
        string[] Extension = new string[]
        {
            "*.png",
            "*.bmp",
            "*.jpg"
        };
        List<string> FullFileName = new List<string>();

        static Timer t;

        public CopyFiles(string p, string b, int interval)
        {
            if (!Directory.Exists(b)) throw new Exception("BackUp dir is not exist");
            if (!Directory.Exists(p)) throw new Exception("Dir is not exist");

            path = p;
            backUp = b;

            t = new Timer(interval);
            t.Elapsed += T_Elapsed;
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Copy(path, backUp, Extension);
        }

        private int Copy(string path, string backUp, string[] exts)
        {
            if (CopyFile(path, backUp ,exts) == 1) return 1;

            var dirs = new DirectoryInfo(path).GetDirectories();
            foreach (var item in dirs)
            {
                if (Copy(item.FullName, backUp, exts) == 1) return 1;
            }

            return -1;
        }
        private int CopyFile(string path, string backUp, string[] exts)
        {
            DirectoryInfo d = new DirectoryInfo(path);

            foreach (var ext in exts)
            {
                var files = d.GetFiles(ext);
                foreach (var file in files)
                {
                    bool flag = false;
                    foreach (var item in FullFileName)
                    {
                        if(item == file.FullName)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) continue;

                    string fileName = backUp + '\\' + file.Name;
                    if (File.Exists(fileName))
                    {
                        fileName = fileName.Remove(fileName.Length - file.Extension.Length) + "(1)" + file.Extension;
                        int cnt = 2;
                        while (File.Exists(fileName))
                        {
                            fileName = fileName.Remove(fileName.Length - file.Extension.Length);
                            int index = fileName.Length - 1;
                            int dig = 0;
                            while (fileName[index] != '(')
                            {
                                dig++;
                                index--;
                            }

                            fileName = fileName.Remove(fileName.Length - dig, dig) + cnt.ToString() + ')' + file.Extension;
                            cnt++;
                        }
                    }

                    File.Copy(file.FullName, fileName);
                    FullFileName.Add(file.FullName);
                    return 1;
                }
            }

            return -1;
        }

        public void Start()
        {
            t.Start();
        }
    }

}
