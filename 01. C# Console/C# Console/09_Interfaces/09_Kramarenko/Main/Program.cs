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
            // объявление и определение начальных параметров
            string startParh = @"C:\Users\Михаил";
            string cmd = "";
            DirectoryInfo path = new DirectoryInfo(startParh);

            while (true)
            {
                // получение команды и деление её на отдельный слова
                Console.Write($"\n{path.FullName}>");
                cmd = Console.ReadLine();
                var words = cmd.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // если строка была не пустая
                if (words.Length > 0)
                {
                    // определение нужной команды по первому слову
                    switch (words[0].ToLower())
                    {
                        case "dir": CMD.DIR(words, path); break;
                        case "cd": CMD.CD(words, ref path); break;
                        case "delete": CMD.Delete(words); break;
                        case "copy": CMD.Copy(words); break;
                        case "type": CMD.Type(words); break;
                        case "exit": End(); return;
                        default: CMD.Message("Неизвестная команда"); break;
                    }
                }
            }
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

    class CMD
    {
        /// <summary>
        /// Вывести содержимое папки
        /// </summary>
        public static void DIR(string[] words, DirectoryInfo d)
        {
            // если команда состоит из одного слова   dir
            if (words.Length == 1) ShowDir(d);
            // если команда состоит из двух слов  dir *путь*
            else if (words.Length == 2)
            {
                // если второе слово - полный путь
                if (Directory.Exists(words[1])) ShowDir(new DirectoryInfo(words[1]));
                // если второе слово - часть пути
                else if (Directory.Exists(d.FullName + "\\" + words[1])) ShowDir(new DirectoryInfo(d.FullName + "\\" + words[1]));
                else Message("Неверный путь");
            }
            // если команда введена некорректно
            else Message("Неизвестная команда");
        }
        /// <summary>
        /// Вывести содержимое определённой папки
        /// </summary>
        private static void ShowDir(DirectoryInfo d)
        {
            if (d.Exists)
            {
                // если папка существует
                try
                {
                    // получение списков всех подпапок и файлов
                    var dirs = d.GetDirectories();
                    var files = d.GetFiles();

                    // вывод полученный подпапок и файлов
                    foreach (var item in dirs)
                    {
                        Console.WriteLine($"<DIR>      {item.Name}");
                    }
                    foreach (var item in files)
                    {
                        Console.WriteLine($"<FILE>     {item.Name}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            // если папка не существует
            else Message("Папка не найдена");
        }


        /// <summary>
        /// изменит текущий путь
        /// </summary>
        public static void CD(string[] words, ref DirectoryInfo d)
        {
            if (words.Length == 2)
            {
                // если введено   cd ..
                if (words[1] == "..")
                {
                    // если текущая папка имеет род. папку
                    if (d.Parent != null) d = d.Parent;
                }
                // если введено   cd *полный путь*
                else if (Directory.Exists(words[1])) d = new DirectoryInfo(words[1]);
                // если введено   cd *часть пути*
                else if (Directory.Exists(d.FullName + "\\" + words[1])) d = new DirectoryInfo(d.FullName + "\\" + words[1]);
                else Message("Неверный путь");
            }
            else Message("Неизвестная команда");
        }


        /// <summary>
        /// удаление файла
        /// </summary>
        public static void Delete(string[] words)
        {
            if (words.Length == 2)
            {
                // если файл существует - удалить
                if (File.Exists(words[1])) File.Delete(words[1]);
                else Message("Неверный путь");
            }
            else Message("Неизвестная команда");
        }


        /// <summary>
        /// копирование файла
        /// </summary>
        public static void Copy(string[] words)
        {
            if (words.Length == 3)
            {
                /// если исходный файл существует
                if (File.Exists(words[1]))
                {
                    FileInfo f = new FileInfo(words[2]);
                    // если нет папки второго файла - создать
                    if (!f.Directory.Exists) Directory.CreateDirectory(f.Directory.FullName);
                    // копирование файлов
                    File.Copy(words[1], words[2]);
                }
                else Message("Неверный путь");

            }
            else Message("Неизвестная команда");
        }


        /// <summary>
        /// вывести содержимое файла
        /// </summary>
        public static void Type(string[] words)
        {
            if (words.Length == 2)
            {
                // если файл существует - вывести
                if (File.Exists(words[1])) typeFile(words[1]);
                else Message("Неверный путь");
            }
            else Message("Неизвестная команда");
        }
        /// <summary>
        /// вывести содержимое определённого файла
        /// </summary>
        private static void typeFile(string path)
        {
            // если файл существует
            if (File.Exists(path))
            {
                Console.WriteLine();
                try
                {
                    // прочитать всё содержимое выйла и вывести
                    Console.WriteLine(File.ReadAllText(path));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else Message("Файл не найден");
        }


        /// <summary>
        /// вывод сообщения
        /// </summary>
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}

