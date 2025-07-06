using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.ComponentModel;

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
            Console.WriteLine("Task1. Пользователь вводит путь к папке, программа просматривает эту папку и подпапки и формирует ");
            Console.WriteLine("       файл отчёта, в который входят следующие данные:                                            ");
            Console.WriteLine("       | FileName | Extension | FullPath | FileSize | CreationDate |                              \n\n\n");
            Console.ResetColor();

            try
            {
                string reportFile = @"C:\Users\Михаил\source\repos\06_Kramarenko\Main\Task1\report.txt";
                if (File.Exists(reportFile)) File.Delete(reportFile);

                string path = @"C:\Users\Михаил\source\repos\06_Kramarenko\Main\Task1\Data";
                //string path = @"C:\Users\Михаил\source\repos\06_Kramarenko";
                Console.WriteLine($"path> {path}");

                //Console.Write("path> ");
                //string path = Console.ReadLine();

                var lst = CreateReport(path);
                WriteReport(reportFile, lst);

                if(lst.Count > 0)
                    Console.WriteLine("\n\n\n\tЗадание выполнено");
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
            Console.WriteLine("Task2. Пользователь вводит путь к папке, программа просматривает эту папку и удаляет ");
            Console.WriteLine("       все файлы с расширениями, заданными в отдельном конфигурационном файле        \n\n\n");
            Console.ResetColor();

            try
            {
                string configFile = @"C:\Users\Михаил\source\repos\06_Kramarenko\Main\Task2\config File.txt";


                string path = @"C:\Users\Михаил\source\repos\06_Kramarenko\Main\Task2\DIR";
                Console.WriteLine($"path> {path}");

                //Console.Write("path> ");
                //string path = Console.ReadLine();

                var Ext = File.ReadAllLines(configFile);
                if (Ext.Length != 0) DeleteAllExtension(path, Ext);

                Console.WriteLine("\n\n\n\tЗадание выполнено");

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }


        #region For Task1

        static void AddToReport(ref List<FileInfo> lst, string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            var files = dInfo.GetFiles();
            foreach (var item in files)
            {
                lst.Add(item);
            }
        }
        static List<FileInfo> CreateReport(string path)
        {
            List<FileInfo> lst = new List<FileInfo>();
            AddToReport(ref lst, path);

            DirectoryInfo dInfo = new DirectoryInfo(path);
            var dirs = dInfo.GetDirectories();
            foreach (var item in dirs)
            {
                lst.AddRange(CreateReport(item.FullName));
            }

            return lst;
        }

        static void WriteReport(string reportFile, List<FileInfo> lst)
        {
            if (lst.Count == 0)
            {
                Console.WriteLine("\n\n\tФайлы не найдены");
                return;
            }

            StreamWriter sw = new StreamWriter(reportFile);
            sw.WriteLine("+-----------------+------------+----------------------------------------------------+------------+-----------------+");
            sw.WriteLine("| Name            | Extension  | FullName                                           | Size       | Creation date   |");
            sw.WriteLine("+-----------------+------------+----------------------------------------------------+------------+-----------------+");

            foreach (var item in lst)
            {
                int[] len = new int[]
                {
                    Path.GetFileNameWithoutExtension(item.Name).Length / 16 + 1,
                    item.Extension.Length / 11 + 1,
                    item.FullName.Length / 51 + 1,
                    item.Length.ToString().Length / 11 + 1,
                    item.CreationTime.Date.ToString("d").Length / 16 + 1
                };

                int n = len.Max();

                for (int i = 0; i < n; i++)
                {
                    sw.Write("| ");
                    if (15 * i < Path.GetFileNameWithoutExtension(item.Name).Length)
                        writeToFile(15, Path.GetFileNameWithoutExtension(item.Name).Remove(0, 15 * i), true, sw);
                    else writeToFile(15, "", true, sw);

                    sw.Write(" | ");
                    if (10 * i < item.Extension.Length)
                        writeToFile(10, item.Extension.Remove(0, 10 * i), true, sw);
                    else writeToFile(10, "", true, sw);

                    sw.Write(" | ");
                    if (50 * i < item.FullName.Length)
                        writeToFile(50, item.FullName.Remove(0, 50 * i), true, sw);
                    else writeToFile(50, "", true, sw);

                    sw.Write(" | ");
                    if (10 * i < item.Length.ToString().Length)
                        writeToFile(10, item.Length.ToString().Remove(0, 10 * i), true, sw);
                    else writeToFile(10, "", true, sw);

                    sw.Write(" | ");
                    if (15 * i < item.CreationTime.Date.ToString("d").Length)
                        writeToFile(15, item.CreationTime.Date.ToString("d").Remove(0, 15 * i), true, sw);
                    else writeToFile(15, "", true, sw);

                    sw.Write(" |\n");
                }

                sw.Write("+-----------------+------------+----------------------------------------------------+------------+-----------------+\n");
            }
            sw.Close();
        }

        static private void writeToFile(uint w, object obj, bool site, StreamWriter sw)
        // site == false - текст по правой стороне
        // site == true  - текст по левой  стороне
        {
            string str = obj.ToString();

            if (str.Length > w)
            {
                str = str.Remove((int)w, str.Length - (int)w);
            }

            if (site) sw.Write(str);

            int length = (int)w - str.Length;
            for (int i = 0; i < length; i++)
            {
                sw.Write(' ');
            }


            if (!site) sw.Write(str);
        }

        #endregion

        #region For Task2

        static void DeleteExtension(string path, string[] Ext)
        {
            DirectoryInfo dInf = new DirectoryInfo(path);

            foreach (var ex in Ext)
            {
                var lst = dInf.GetFiles("*" + ex);
                foreach (var item in lst)
                {
                    item.Delete();
                }
            }

        }
        static void DeleteAllExtension(string path, string[] Ext)
        {
            DeleteExtension(path, Ext);

            DirectoryInfo dInf = new DirectoryInfo(path);
            var lst = dInf.GetDirectories();
            foreach (var item in lst)
            {
                DeleteAllExtension(item.FullName, Ext);
            }
        }

        #endregion

    }
}
