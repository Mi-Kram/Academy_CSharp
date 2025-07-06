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
}

class Tasks
{
    static public void Task1()
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Task1. Пользователь вводит 2 имени файла. Программа копирует из первого файла во второй только   ");
        Console.WriteLine("       имена людей можно с повторениями, список которых нахдится в 3 файле, записанный в столбик \n\n\n");
        Console.ResetColor();

        try
        {
            string fName1, fName2;
            fName1 = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\2\C#pictures.png";
            fName2 = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\2\C#pictures.png";
            //fName1 = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\1\text1.txt";
            //fName2 = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\1\text2.txt";

            //Console.Write("Первый файл> ");
            //fName1 = Console.ReadLine();
            //Console.Write("Второй файл> ");
            //fName2 = Console.ReadLine();

            Console.WriteLine($"Первый файл> {fName1}");
            Console.WriteLine($"Второй файл> {fName2}");

            FileStream fs1 = new FileStream(fName1, FileMode.Open, FileAccess.Read);
            FileStream fs2 = new FileStream(fName2, FileMode.Open, FileAccess.Read);

            BinaryReader br1 = new BinaryReader(fs1, Encoding.Default);
            BinaryReader br2 = new BinaryReader(fs2, Encoding.Default);

            bool flag = false;
            var fSize = fs1.Length;

            if (fSize == fs2.Length)
            {
                flag = true;
                for (int i = 0; i < fSize; i++)
                {
                    if (br1.ReadByte() != br2.ReadByte())
                    {
                        flag = false;
                        break;
                    }
                }
            }

            fs1.Close();
            fs2.Close();

            Console.WriteLine("\n\n");
            if (flag) Console.WriteLine("Файлы одинаковые");
            else Console.WriteLine("Файлы не одинаковые");
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
    static public void Task2()
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Task2. Пользователь вводит имя файла и размер тома. Программа делит исходный файл на одинаковые тома. Размер ");
        Console.WriteLine("        последнего тома может быть меньше. Количество томов зависит от размера исходного файла и размера     \n\n\n");
        Console.ResetColor();

        try
        {
            string fName, tomPath;
            uint tomSize = 0;
            fName = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\2\C#pictures.png";
            tomPath = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\2\Tom" + @"\tom";

            //Console.Write("Файл> ");
            //fName1 = Console.ReadLine();
            //Console.Write("Второй файл> ");
            //fName2 = Console.ReadLine();

            Console.WriteLine($"Файл> {fName}");
            Console.Write("Размер тома> ");
            if (!uint.TryParse(Console.ReadLine(), out tomSize)) throw new Exception("Указано неверное кол-во частей");

            FileStream source = new FileStream(fName, FileMode.Open, FileAccess.Read);
            BinaryReader BinSource = new BinaryReader(source, Encoding.Default);
            var fSize = source.Length;

            if (fSize != 0)
            {
                int count = (int)fSize / (int)tomSize;

                string newTomPath;
                FileStream tom;
                BinaryWriter BinTom;

                for (int i = 0; i < count; i++)
                {
                    newTomPath = tomPath + (i + 1);
                    tom = new FileStream(newTomPath, FileMode.Create, FileAccess.Write);
                    BinTom = new BinaryWriter(tom, Encoding.Default);

                    BinTom.Write(BinSource.ReadBytes((int)tomSize));

                    tom.Close();
                }

                newTomPath = tomPath + (count + 1);
                tom = new FileStream(newTomPath, FileMode.Create, FileAccess.Write);
                BinTom = new BinaryWriter(tom, Encoding.Default);

                BinTom.Write(BinSource.ReadBytes((int)fSize - ((int)count * (int)tomSize)));

                tom.Close();
            }

            source.Close();
        }
        catch (Exception e)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"Task2 не выполнен. {e.Message}\b!");
            Console.ResetColor();
        }

        Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
        Console.ReadLine();
        Console.Clear();
    }
    static public void Task3()
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Task3. Пользователь вводит названия частей. Программа объединяет их в ");
        Console.WriteLine("       один результирующий файл, имя которого пользователь тоже вводит \n\n\n");
        Console.ResetColor();

        try
        {
            //uint count = 0;
            //Console.Write("Кол-во частей> ");
            //if (!uint.TryParse(Console.ReadLine(), out count)) throw new Exception("Указано неверное кол-во частей");
            //Console.WriteLine();

            //string[] parts = new string[count];
            //for (int i = 0; i < count; i++)
            //{
            //    Console.WriteLine($"part{i + 1}> ");
            //    parts[i] = Console.ReadLine();
            //}
            //Console.WriteLine();

            //Console.Write("Результирующий файл> ");
            //string resFile = Console.ReadLine();

            uint count = 0;
            Console.Write("Кол-во частей> ");
            if (!uint.TryParse(Console.ReadLine(), out count)) throw new Exception("Указано неверное кол-во частей");
            Console.WriteLine();

            string[] parts = new string[count];
            for (int i = 0; i < count; i++)
            {
                parts[i] = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\2\Tom\tom" + (i + 1);
                Console.WriteLine($"part{i + 1}> {parts[i]}");
            }
            Console.WriteLine();

            string resFile = @"C:\Users\Михаил\source\repos\04_Kramarenko\Main\2\result.png";
            Console.WriteLine($"Результируюзий файл> {resFile}");



            FileStream result = new FileStream(resFile, FileMode.Create, FileAccess.Write);
            BinaryWriter BinSource = new BinaryWriter(result, Encoding.Default);

            FileStream tom;
            BinaryReader BinTom;

            for (int i = 0; i < count; i++)
            {
                tom = new FileStream(parts[i], FileMode.Open, FileAccess.Read);
                BinTom = new BinaryReader(tom, Encoding.Default);

                BinSource.Write(BinTom.ReadBytes((int)tom.Length));

                tom.Close();
            }

            result.Close();
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
}


