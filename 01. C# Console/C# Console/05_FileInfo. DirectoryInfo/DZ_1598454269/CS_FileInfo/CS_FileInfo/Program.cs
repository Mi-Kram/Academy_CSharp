using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_FileInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] str = { "Привет", "мир!!!" };
            File.WriteAllLines("test.txt", str, Encoding.Default);

            Console.Write("File attributes:\t\t");
            Console.WriteLine(File.GetAttributes("test.txt"));
            Console.Write("Creation time:\t\t");
            Console.WriteLine(File.GetCreationTime("test.txt"));
            Console.Write("Last Access Time:\t");
            Console.WriteLine(File.GetLastAccessTime("test.txt"));
            Console.Write("Existance check:\t");
            Console.WriteLine(File.Exists("test.txt"));

            File.Delete("test2.txt");
            File.Delete("test3.txt");

            // 3 параметр - перезаписать файл, если он существует
            File.Copy("test.txt", "test2.txt", true);
            File.Move("test.txt", "test3.txt");

            FileInfo info = new FileInfo(@"c:\temp\text.txt");
            Console.Write("File attributes:\t\t");
            Console.WriteLine(info.Attributes);
            Console.Write("Creation time:\t\t");
            Console.WriteLine(info.CreationTime);
            Console.Write("Last Access Time:\t");
            Console.WriteLine(info.LastAccessTime);

            // проверка существования файла
            Console.Write("Existance check:\t");
            Console.WriteLine(info.Exists);
        }
    }
}
