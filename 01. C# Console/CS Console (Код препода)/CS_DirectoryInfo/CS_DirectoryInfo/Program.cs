using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

class Program
{
    public void showAllFiles(string path)
    {
        DirectoryInfo dinfo = new DirectoryInfo(path);

        if (dinfo.Exists)
        {
            // Получить массив файлов в текущей папке
            try
            {
                FileInfo[] files = dinfo.GetFiles();
                foreach (FileInfo current in files)
                {
                    Console.WriteLine(current.FullName);
                }

                // Получить массив подпапок в текущей папке
                DirectoryInfo[] dirs = dinfo.GetDirectories();
                foreach (DirectoryInfo current in dirs)
                {
                    Console.WriteLine("<DIR>    " + path + "\\" + current.Name);
                    showAllFiles(path + @"\" + current.Name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Path is not exists");
        }
    }

    static void Main(string[] args)
    {
        // получить расширение указанного файла
        string extension = Path.GetExtension(@"c:\temp\text.my.txt");
        Console.WriteLine(extension);

        // получить имя указанного файла без расширения
        string shortName = Path.GetFileNameWithoutExtension(@"c:\temp\text.my.txt");
        Console.WriteLine(shortName);

        // сгенерировать случайное имя
        string randomName = Path.GetRandomFileName();
        Console.WriteLine(randomName);

        // сгенерировать пустой временный файл и возвратить его полный путь
        string tempFileName = Path.GetTempFileName();
        Console.WriteLine(tempFileName);

        //Program p = new Program();
        //p.showAllFiles(@"c:\Temp");

        /*DirectoryInfo dinfo = new DirectoryInfo(".");
        Console.Write("Текущая папка:\t\t");
        Console.WriteLine(dinfo.FullName);       // Получить полное имя папки
        Console.Write("Родительская папка:\t");
        Console.WriteLine(dinfo.Parent);         // Получить краткое имя родительской папки
        Console.Write("Корневая папка:\t\t");
        Console.WriteLine(dinfo.Root);           // Получить краткое имя корневой папки
        Console.Write("Текущая папка:\t\t");
        Console.WriteLine(dinfo.Name);           // Получить краткое имя текущей папки
        Console.Write("Атрибуты папки:\t\t");
        Console.WriteLine(dinfo.Attributes);     // Получить атрибуты текущей папки
        Console.Write("Время посл. доступа:\t");
        Console.WriteLine(dinfo.LastAccessTime); // Получить время последнего доступа
        Console.Write("Время создания:\t\t");
        Console.WriteLine(dinfo.CreationTime);   // Получить время создания

        Console.WriteLine("\nСоздание папок:\t\t");
        DirectoryInfo d2 = new DirectoryInfo(@".\test");
        if (d2.Exists == false)
            d2.Create();     // Создание папки

        Console.WriteLine(d2.FullName);
        DirectoryInfo d3 = new DirectoryInfo(".\\test2");
        if (d3.Exists == false) 
            d3.Create();     // Создание папки

        Console.WriteLine(d3.FullName);

        Console.WriteLine("\nПросмотр вложенных папок:\t");
        DirectoryInfo[] dirs = dinfo.GetDirectories();
        foreach (DirectoryInfo current in dirs)
        {
            Console.WriteLine(current.Name);
        }

        Console.Write("\nПеремещение папки:\t");
        DirectoryInfo d4 = new DirectoryInfo(d3.FullName + "\\" + d2.Name);
        if (d4.Exists == false)
        {
            d2.MoveTo(d3.FullName + "\\" + d2.Name);  //  Перемешение папки
            Console.WriteLine(d2.FullName);
        }

        //d2.Delete();                                   //  Удаление папки

        Console.WriteLine("\nПросмотр вложенных файлов:\t");
        FileInfo[] files = dinfo.GetFiles();
        foreach (FileInfo current in files)
        {
            Console.WriteLine(current.Name);
        }

        Console.WriteLine("\nПросмотр дисков компьютера:\t");
        foreach (string current in Directory.GetLogicalDrives())
        {
            Console.WriteLine(current);
        }*/
    }

}
