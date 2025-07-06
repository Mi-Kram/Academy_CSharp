using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CS_Linq_Solutions
{
    class Program
    {
        // 1. Функция принимает массив чисел и возвращает исходный массив, но без нечётных чисел
        public static int[] NoOdds(int[] values)
        {
            return (
              from v in values
              where (v % 2 == 0)
              select v
            ).ToArray();
        }

        public static int[] NoOdds2(int[] values) => values.Where(v => v % 2 == 0).ToArray();

        // 2. Функция возвращает количество вхождений элемента в заданном массиве чисел.
        //    var arr = { 1, 0, 2, 2, 3 };
        //    NumberOfOccurrences(0, arr) == 1;
        public static int NumberOfOccurrences(int x, int[] xs) => xs.Count(Item => Item == x);

        public static int NumberOfOccurrences2(int x, int[] xs) => (from int i in xs
                                                                    where i == x
                                                                    select i).Count();

        // 3. Функция принимает массив, который содержит повторяющиеся числа. Только одно число в 
        //    массиве не повторяется. Функция возвращает это число
        //    findUniq([1, 1, 1, 2, 1, 1]) -> 2
        //    findUniq([0, 2, 0.1, 2, 0]) -> 0.1

        public static int GetUnique(IEnumerable<int> numbers)
        {
            return numbers.GroupBy(s => s)
            .Where(x => x.Count() == 1)
            .Select(x => x.Key)
            .FirstOrDefault();
        }

        public static int GetUnique2(IEnumerable<int> numbers)
        {
            return numbers.GroupBy(x => x).Single(x => x.Count() == 1).Key;
        }

        public static int GetUnique3(IEnumerable<int> numbers)
        {
            return numbers.OrderBy(x => numbers.Where(y => y == x).Count()).First();
        }

        // 4. Строка состоит из слов, которые разделены пробелами и могут повторяться. Функция принимает строку
        //    и удаляет в ней все повторяющиеся слова, оставляя их в одном экземпляре в том месте, где они первый раз встретились.
        public static string RemoveDuplicateWords(string s) => String.Join(" ", s.Split(' ').Distinct());

        public static string RemoveDuplicateWords2(string s) => string.Join(" ", s.Split(' ').GroupBy(x => x).Select(x => x.Key));

        // 5. Функция принимает строку, которая содержит буквы и цифры и возвращает число, которое состоит
        //    из максимального количества цифр, идущих подряд в строке

        public static int Solve(string s)
        {
            return Regex.Split(s, "[a-z]+")
                        .Where(e => e != string.Empty)
                        .Select(e => int.Parse(e))
                        .Max();
        }

        // 6. Функция возвращает количество гласных букв в переданной строке
        public static int GetVowelCount(string str)
        {
            return str.Count(i => "aeiouy".Contains(i));
        }

        public static int GetVowelCount2(string str)
        {
            return str.ToLower().Count(c => "aeiouy".IndexOf(c) != -1);
        }

        // 7. Функция принимает строку, содержащую слова, разделённые пробелами, производит реверс каждого
        // слова, объединяет их в результирующую строку и возвращает эту строку
        // ReverseWords("Hello Big World") -> "olleH giB dlroW"

        public static string ReverseWords(string str) => string.Join(" ", str.Split().Select(x => string.Concat(x.Reverse())));

        public static string ReverseWords2(string str)
        {
            return string.Join(" ", str.Split(' ').Select(i => new string(i.Reverse().ToArray())));
        }

        // 8. Функция принимает строку и возвращает строку, состоящую из первых букв каждого слова исходной строки
        // MakeString("Hello Big World") -> "HBW"
        public static string MakeString(string s)
        {
            return string.Join("", s.Split(' ').Select(x => x[0]));
        }

        // 9. Функция принимает строку и возвращает отсортированный массив индексов заглавных букв
        // Capitals("Hello World") -> { 0, 6 }

        public static int[] Capitals(string word)
        {
            return word.Select((x, n) => n).Where((x, i) => char.IsUpper(word, i)).ToArray();
        }

        // 10. Функция возвращает индекс минимального элемента входного массива
        public static int FindSmallest(int[] numbers)
        {
            return Array.IndexOf(numbers, numbers.Min());
        }

        static void Main(string[] args)
        {
            var res = Capitals("Hello World World World");
            foreach (var item in res)
            {
                Console.WriteLine(item);
            }
        }
    }
}
