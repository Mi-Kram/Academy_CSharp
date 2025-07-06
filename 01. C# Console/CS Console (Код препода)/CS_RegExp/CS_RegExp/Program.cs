using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Console_RexEx
{
    class Program
    {
        static void Main(string[] args)
        {
            // ввод строки с клавиатуры
            string str = Console.ReadLine();

            // Метод проверяет, входит ли в первую строку подстрока, соответствующая шаблону, заданному во второй строке
            // Возвращается коллекция вхождений

            // Поиск слова gray или grey
            //MatchCollection m = Regex.Matches(str, "gr[ae]y", RegexOptions.IgnoreCase);

            // Поиск слова gray или grey
            //MatchCollection m = Regex.Matches(str, "^gr[ae]y$", RegexOptions.IgnoreCase);

            // Конструция выбора (разные варианты разделяются вертикальной чертой)
            //MatchCollection m = Regex.Matches(str, "Papa|Mama|Son", RegexOptions.IgnoreCase);

            // Точка в шаблоне обозначает ОДИН ЛЮБОЙ символ
            //MatchCollection m = Regex.Matches(str, "...");

            // Нахождение слов, состоящих из одной или более цифер
            //MatchCollection m = Regex.Matches(str, "[a-f0-9]+", RegexOptions.IgnoreCase);

            // @ - отключает спецсимволы в литералах
            //Console.WriteLine(@"hello\tworld!");

            // (* - подстрока встречается 0 или более раз, + - подстрока встречается 1 или более раз)
            //MatchCollection m = Regex.Matches(str, @"^I love (Papa|Mama|Son)*$", RegexOptions.IgnoreCase);

            // ? - символ может встречаться 0 или 1 раз, но не более
            //MatchCollection m = Regex.Matches(str, @"^Hello(\.\.\.)?$");

            // {2} - указание количества повторений подстроки, {2,} - повторения 2 или более раз, {2, 4} - повторения от 2 до 4 раз
            //MatchCollection m = Regex.Matches(str, @"^(abc){2,4}$");

            // \. - экранирование специальных символов: )(}{.+/*][|-
            //MatchCollection m = Regex.Matches(str, @"\.\.\.");

            // Использование дополнительных метасимволов (/w - буква, /d - цифра, /s - пробел, /t - табуляция)
            //MatchCollection m = Regex.Matches(str, @"^\d+\s+\d+$");

            // ^ - требует нахождения подстроки в начале строки, $ - требует нахождения подстроки в конце строки
            //MatchCollection m = Regex.Matches(str, @"^\d+$");

            //12:33:57.998
            //MatchCollection m = Regex.Matches(str, @"^[01][0-9]|2[0-3]:[0-5][0-9]:[0-5][0-9]\.(\d){3}$");

            // 12  руб.  34  коп.
            // 37  коп.
            // 45   руб.
            MatchCollection m = Regex.Matches(str, @"^(\d+\s+rub\.)?(\s*([1-9]|[1-9][0-9])\s+kop\.)?$");

            if (m.Count > 0) Console.WriteLine("Yes. Count: {0}", m.Count);
            else Console.WriteLine("No");

            // Перебрать все найденные подстроки, удовлетворяющие шаблону
            foreach (Match match in m)
            {
                Console.WriteLine("Value: {0}, Start: {1}, Length: {2}", match.Value, match.Index, match.Length);
            }

            //Console.WriteLine(Regex.Replace("uygjy56:56 uyguyg 23:12hgjy xfx04:04szdsz", "([01][0-9]|2[0-3]):[0-5][0-9]", "!!!"));
            //Console.WriteLine(Regex.Replace("         uygjy56:56 uyguyg 23:12hgjy xfx04:04szdsz", @"^\s+", "!"));

            /*Console.WriteLine("\nSplit:");
            Console.WriteLine("Enter delimiter:");
            string d = Console.ReadLine();
            Console.WriteLine("Enter text:");
            string txt = Console.ReadLine();

            //string [] ttt = txt.Split(new char[] { ' ', '.' });

            string[] str1 = Regex.Split(txt, d);
            foreach (string s in str1)
            {
                Console.WriteLine(s);
            }*/

        }
    }
}
