using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Tasks.Task1();
            Tasks.Task3();

            //while (true) { Tasks.Task2(); }

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
            Console.WriteLine("Task1. Пользователь вводит путь к папке, программа объединяет все текстовые ");
            Console.WriteLine("       файлы в один большой текстовый файл(копирует содержимое)             \n\n\n");
            Console.ResetColor();

            try
            {
                string resultFile = @"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task1\resultFile.txt";
                if (File.Exists(resultFile)) File.Delete(resultFile);

                string path = @"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task1\Files";
                Console.WriteLine($"path> {path}");

                //Console.Write("path> ");
                //string path = Console.ReadLine();

                uniteTxtFiles(path, resultFile);

                Console.WriteLine("\n\n\nЗадание выполнено");
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {e.Message}\b!");
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
            Console.WriteLine("Task2. Пользователь вводит путь к папке и искомую строку. Программа находит в указанной папке все файлы, ");
            Console.WriteLine("       содержащие указанную строку и сообщает позицию (номер байта) строки в каждом найденном файле      \n\n\n");
            Console.ResetColor();

            try
            {
                //string path = @"C:\Users\Михаил\Desktop\C#";
                string path = @"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task2";
                Console.WriteLine($"path> {path}");

                //Console.Write("path> ");
                //string path = Console.ReadLine();
                Console.Write("str> ");
                string str = Console.ReadLine();
                if (str.Length == 0) throw new Exception("Строка введена неправильно");

                Console.WriteLine("\n\n");

                var lst = FindStr(path, str);

                Print(lst);

                Console.WriteLine("\n\n\nПосмотреть содержимое файлов? (space)");
                if (Console.ReadKey().Key == ConsoleKey.Spacebar)
                {
                    Console.WriteLine();
                    ReedAllFiles(path, str);

                    Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {e.Message}\b!");
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
            Console.WriteLine("Task3. Пользователь вводит путь к папке, программа создаёт 3 результирующие подпапки: JPG, TXT, DOC           ");
            Console.WriteLine("       Потом программа находит файлы с соответствующим расширением (.jpg, .txt, .doc) в указанной             ");
            Console.WriteLine("       папке и в подпапках. Найденные по расширениям файлы копируются в соответствующие папки (JPG, TXT, DOC) \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\Files";
                Console.Write($"path> {path}");

                //Console.Write("path> ");
                //string path = Console.ReadLine();

                Clear();
                SortFiles(path);

                Console.WriteLine("\n\n\n\nЗадание выполнено");
            }
            catch (Exception e)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task3 не выполнен. {e.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }


        #region For Task1

        private static void uniteFiles(string path, string result)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);
            FileStream fs = new FileStream(result, FileMode.Append, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs, Encoding.Default);


            var files = dInfo.GetFiles();
            foreach (var item in files)
            {
                if (item.Extension == ".txt")
                {
                    FileStream fs2 = new FileStream(item.FullName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs2, Encoding.Default);

                    bw.Write(br.ReadBytes((int)fs2.Length));
                    fs2.Close();
                }
            }

            fs.Close();
        }
        private static void uniteTxtFiles(string path, string result)
        {
            uniteFiles(path, result);

            DirectoryInfo dInfo = new DirectoryInfo(path);
            var dirs = dInfo.GetDirectories();
            foreach (var item in dirs)
            {
                uniteTxtFiles(item.FullName, result);
            }
        }

        #endregion

        #region For Task2

        class strPositionInFile
        {
            public FileInfo file;
            public int Position;

            public strPositionInFile(FileInfo f, int pos)
            {
                file = f;
                Position = pos;
            }
        }

        private static void Find(string path, ref List<strPositionInFile> lst, string str)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            var files = dInfo.GetFiles();
            foreach (var item in files)
            {
                if (item.Extension == ".txt")
                {
                    string buf = File.ReadAllText(item.FullName);
                    if(buf.IndexOf(str) != -1)
                    {
                        lst.Add(new strPositionInFile(item, buf.IndexOf(str) + 1));
                    }
                }
            }
        }
        private static List<strPositionInFile> FindStr(string path, string str)
        {
            List<strPositionInFile> lst = new List<strPositionInFile>();

            Find(path, ref lst, str);

            DirectoryInfo dInfo = new DirectoryInfo(path);
            var dirs = dInfo.GetDirectories();
            foreach (var item in dirs)
            {
                lst.AddRange(FindStr(item.FullName, str));
               
            }

            return lst;
        }
        
        static private void write(uint w, object obj, bool site)
        // site == false - текст по правой стороне
        // site == true  - текст по левой  стороне
        {
            string str = obj.ToString();

            if(str.Length > w)
            {
                str = str.Remove((int)w, str.Length - (int)w);
            }

            if (site) Console.Write(str);

            int length = (int)w - str.Length;
            for (int i = 0; i < length; i++)
            {
                Console.Write(' ');
            }


            if (!site) Console.Write(str);
        }

        private static void Print(List<strPositionInFile> lst)
        {
            if (lst.Count == 0)
            {
                Console.WriteLine("\tФайлы не найдены");
                return;
            }

            Console.WriteLine("\t+--------------------------------------------------------------+-----------------+-------+");
            foreach (var item in lst)
            {
                int first = item.file.FullName.Length / 61 + 1;
                int second = item.file.Name.Length / 16 + 1;
                int third = item.Position.ToString().Length / 6 + 1;

                int n = 0;
                if (first > n) n = first;
                if (second > n) n = second;
                if (third > n) n = third;

                for (int i = 0; i < n; i++)
                {
                    Console.Write("\t| ");
                    if (60 * i < item.file.FullName.Length)
                        write(60, item.file.FullName.Remove(0, 60 * i), true);
                    else write(60, "", true);

                    Console.Write(" | ");
                    if(15 * i < item.file.Name.Length)
                        write(15, item.file.Name.Remove(0, 15*i), true);
                    else write(15, "", true);

                    Console.Write(" | ");
                    if(5 * i < item.Position.ToString().Length)
                        write(5, item.Position.ToString().Remove(0, 5*i), true);
                    else write(5, "", true);

                    Console.WriteLine(" |");
                }

                Console.WriteLine("\t+--------------------------------------------------------------+-----------------+-------+");
            }
        }

        private static void ReedFiles(string path, string subStr)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            var files = dInfo.GetFiles();
            foreach (var item in files)
            {
                if (item.Extension == ".txt")
                {
                    Console.WriteLine($"{item.Name}: ");
                    PrintSearchStr(File.ReadAllText(item.FullName), subStr);
                    Console.WriteLine('\n');
                }
            }
        }
        private static void ReedAllFiles(string path, string subStr)
        {
            ReedFiles(path, subStr);

            DirectoryInfo dInfo = new DirectoryInfo(path);
            var dirs = dInfo.GetDirectories();
            foreach (var item in dirs)
            {
                ReedAllFiles(item.FullName, subStr);
            }
        }
        private static void PrintSearchStr(string str, string subStr)
        {
            int n = str.IndexOf(subStr);

            while (n != -1)
            {
                Console.Write(str.Remove(n, str.Length - n));
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(subStr);
                Console.ResetColor();

                str = str.Remove(0, n + subStr.Length);
                n = str.IndexOf(subStr);
            }
            Console.Write(str);
        }

        #endregion

        #region For Task3

        private static void SortFile(string path)
        {
            DirectoryInfo dInfo = new DirectoryInfo(path);

            var files = dInfo.GetFiles();
            foreach (var item in files)
            {
                switch (item.Extension.ToLower())
                {
                    case ".jpg":
                        if (File.Exists(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\JPG\" + item.Name)) 
                            File.Delete(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\JPG\" + item.Name);
                        File.Copy(item.FullName, @"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\JPG\" + item.Name);
                        break;
                    case ".txt":
                        if (File.Exists(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\TXT\" + item.Name))
                            File.Delete(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\TXT\" + item.Name);
                        File.Copy(item.FullName, @"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\TXT\" + item.Name);
                        break;
                    case ".doc":
                        if (File.Exists(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\DOC\" + item.Name))
                            File.Delete(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\DOC\" + item.Name);
                        File.Copy(item.FullName, @"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\DOC\" + item.Name);
                        break;
                }
            }
        }
        private static void SortFiles(string path)
        {
            SortFile(path);

            DirectoryInfo dInfo = new DirectoryInfo(path);
            var dirs = dInfo.GetDirectories();
            foreach (var item in dirs)
            {
                SortFiles(item.FullName);
            }
        }
        private static void Clear()
        {
            if (Directory.Exists(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\JPG"))
                Directory.Delete(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\JPG", true);
            if (Directory.Exists(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\TXT"))
                Directory.Delete(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\TXT", true);
            if (Directory.Exists(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\DOC"))
                Directory.Delete(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\DOC", true);

            Directory.CreateDirectory(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\JPG");
            Directory.CreateDirectory(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\TXT");
            Directory.CreateDirectory(@"C:\Users\Михаил\source\repos\05_Kramarenko\Main\Task3\result\DOC");
        }

        #endregion
    }
}
