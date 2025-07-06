using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("NumberOfOccurrences. Функция возвращает повторяющееся число в массиве");
            Console.Write("NumberOfOccurrences(0, new int[] { 0, 0, 5, 5, 0, 0 }) = ");
            Console.WriteLine(NumberOfOccurrences(0, new int[] { 0, 0, 5, 5, 0, 0 }));
            Pause();

            Console.WriteLine("GetUnique. Функция возвращает повторяющееся число в массиве");
            Console.Write("GetUnique(new int[] { 1, 2, 3, 5, 3, 2, 1 }) = ");
            Console.WriteLine(GetUnique(new int[] { 1, 2, 3, 5, 3, 2, 1 }));
            Pause();

            Console.WriteLine("RemoveDuplicateWords. Функция возвращает строку без повторяющихся слов:");
            Console.Write("RemoveDuplicateWords(\"Hello World World World Hello Hello World\") = ");
            Console.WriteLine(RemoveDuplicateWords("Hello World World World Hello Hello World"));
            Pause();

            Console.WriteLine("Solve. Функция возвращает число, которое состоит из наибольшего кол-ва цифр подряд:");
            Console.Write("Solve(\"12hello987big89world56781\") = ");
            Console.WriteLine(Solve("12hello987big89world56781"));
            Pause();

            Console.WriteLine("ReverseWords. Функция возвращает строку с перевёрнутыми словами:");
            Console.Write("ReverseWords(\"Hello Big World\") = ");
            Console.WriteLine(ReverseWords("Hello Big World"));
            Pause();

            Console.WriteLine("MakeString. Функция возвращает строку состоящая из первых букв исходной строки:");
            Console.Write("\"Hello Big World\" = ");
            Console.WriteLine(MakeString("Hello Big World"));
            Pause();

            Console.WriteLine("Capitals. Функция возвращает отсортированный массив индексов заглавных букв строки:");
            Console.Write("\"Hello Big World\" = ");
            int[] arr = Capitals("Hello Big World");
            foreach (var item in arr) Console.Write($"{item}  ");
            Pause();
        }
        static void Pause()
        {
            while (Console.ReadKey().KeyChar != '\r') { }
            Console.WriteLine('\n');
        }


        static int NumberOfOccurrences(int num, params int[] arr)
        {
            return arr.Count(n => n == num);
        }

        static int GetUnique(params int[] arr)
        {
            return arr.Where(num => Array.FindAll(arr, x => x == num).Length == 1).FirstOrDefault();
        }

        static string RemoveDuplicateWords(string str)
        {
            return string.Join(" ", str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct());
        }

        static int Solve(string str)
        {
            return int.Parse(str.ToLower().Split(" qwertyuiopasdfghjklzxcvbnm".ToCharArray(),
                StringSplitOptions.RemoveEmptyEntries).OrderBy(s => s.Length).Last());
        }

        static string ReverseWords(string str)
        {
            return string.Join(" ", str.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries).Select(s => new string(s.ToCharArray().Reverse().ToArray())));
        }

        static string MakeString(string str)
        {
            return new string(str.Where(ch => char.IsUpper(ch)).ToArray());
        }

        static int[] Capitals(string str)
        {
            return str.Select((a, i) => (a, i)).Where(x => char.IsUpper(x.a)).Select(x => x.i).ToArray();

            // если вариант выше не работает
            // return str.Select((ch, i) => i).Where((ch, i) => char.IsUpper(str, i)).ToArray();
        }

    }
}
