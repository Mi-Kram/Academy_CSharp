using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

namespace StreamReader1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StreamWriter writer = new StreamWriter(@"c:\temp\test.txt", false, Encoding.Default);
                writer.WriteLine("My name is {0} ---- {1}", "Dima", 345);
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                StreamReader stream = new StreamReader(@"c:\temp\test.txt", Encoding.Default);
                string line;
                line = stream.ReadLine();

                // Пока строки в файле не закончились
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = stream.ReadLine();
                }
                stream.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //  сохранить текстовую строку в файл
            File.WriteAllText(@"c:\temp\test2.txt", "Hello\nworld\n");

            //  сохранить массив строк в файл
            string []lines = { "Hello", "big", "world", "!!!"};
            File.WriteAllLines(@"c:\temp\test3.txt", lines);

            // загрузить строку из файла
            string str2 = File.ReadAllText(@"c:\temp\test2.txt");
            Console.WriteLine(str2);

            string []lines2 = File.ReadAllLines(@"c:\temp\test3.txt");
            foreach (string line in lines2)
            {
                Console.WriteLine(line);
            }
        }
    }
}
