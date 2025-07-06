using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_LINQToObjects
{
    // класс-компаратор для сравнения строк
    class MyComp : IComparer<string>
    {
        public int Compare(string s1, string s2)
        {
            if (s1.Length > s2.Length) return 1;
            if (s1.Length < s2.Length) return -1;
            else return 0;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "Alexander", "Anton", "Adolf", "Pavel", "Anrew", "Nikolay" };

            // классический способ фильтрации коллекции
            /*List<string> strRes = new List<string>();
            foreach (var item in names)
            {
                if (item.Contains("o"))
                    strRes.Add(item);
            }
            string name = strRes[1];*/

            // Запрос к коллекции строк: выбрать список строк, которые содержат букву "о" и взять второй по счёту объект
            //string name = names.Where(n => n.Contains("o")).ElementAt(1);

            string name = (from n in names
                           where n.Contains("o")
                           select n).ElementAt(1);

            Console.WriteLine(name);

            string[] numbers = { "One", "Two", "Three" };

            // Преобразовать массив в список
            List<string> lst = numbers.ToList();
            foreach (string str in lst)
            {
                Console.WriteLine(str);
            }

            lst = new List<string>(names);

            // Выбрать объекты, которые начинаются на "An" и взять последний объект
            name = lst.Where(n => n.StartsWith("An")).Last();

            /*name = (from t in lst
                    where t.StartsWith("An")
                    select t).Last();*/

            Console.WriteLine($"Name =  {name}");

            // Выбрать объекты, которые начинаются на "An"
            //IEnumerable<string> n2 = lst.Where(n => n.StartsWith("An"));

            var n2 = from t in lst
                     where t.StartsWith("An")
                     select t;

            foreach (string n in n2)
                Console.WriteLine(n);

            Console.WriteLine("\nTake:  ------------------------------");

            // Взять пять первых элементов коллекции
            n2 = lst.Take(5);
            foreach (string n in n2)
                Console.WriteLine(n);

            // Брать объекты из коллекции lst до тех пор, пока выполняется условие
            Console.WriteLine("\nTakeWhile:  ------------------------------");

            n2 = lst.TakeWhile(n => n.Length < 9);
            foreach (string k in n2)
                Console.WriteLine(k);

            // Взять все объекты из коллекции, но пропустить первые 5
            Console.WriteLine("\nSkip:  ------------------------------");
            n2 = lst.Skip(5);
            foreach (string n in n2)
                Console.WriteLine(n);

            // Пропускать объекты, пока выполняется условие
            Console.WriteLine("\nSkipWhile:  ------------------------------");
            n2 = lst.SkipWhile(n => n.Length > 8);
            foreach (string n in n2)
                Console.WriteLine(n);

            // Объединение 2 коллекций в одну
            Console.WriteLine("\nConcat:  ------------------------------");
            n2 = lst.Take(2).Concat(lst.Skip(4));
            foreach (string n in n2)
                Console.WriteLine(n);

            // Сортировка коллекции lst по длине в порядке возрастания
            Console.WriteLine("\nOrderBy:  ------------------------------");

            //n2 = lst.OrderBy(s => s.Length);

            n2 = from s in lst
                 orderby s.Length
                 select s;

            foreach (string n in n2)
                Console.WriteLine(n);

            // Сортировка lst в алфавитном порядке убывания
            Console.WriteLine("\nOrderByDescending:  ------------------------------");

            //n2 = lst.OrderByDescending(s => s);

            n2 = from s in lst
                 orderby s descending
                 select s;

            foreach (string n in n2)
                Console.WriteLine(n);

            // Сортировка по длине, в группе с одинаковой длиной - сортировка по алфавиту в порядке убывания
            Console.WriteLine("\nThenBy:  ------------------------------");
            n2 = lst.OrderBy(s => s, new MyComp()).ThenByDescending(s => s);

            // сортировка только БЕЗ использования компаратора
            /*n2 = from s in lst
                 orderby s.Length, s descending
                 select s;*/

            foreach (string n in n2)
                Console.WriteLine(n);

            // Пересечение двух коллекций (оставляет в результате только те элементы, которые есть и в первой и во второй коллекциях)
            Console.WriteLine("\nIntersect:  ------------------------------");
            n2 = lst.Intersect(new string[] { "Anrew", "Nikolay", "Anna" });
            foreach (string n in n2)
                Console.WriteLine(n);

            // создание анонимного объекта анонимного класса, содержащего определённые свойства
            //var obj = new { Prop1 = "Hello", Prop2 = 34};
            //Console.WriteLine(obj);

            // Формирование новой коллекции из других коллекций
            Console.WriteLine("\nSelect:  ------------------------------");
            //var l = lst.Select(n => n.Length);  // запросить коллекцию длин строк

            //var l = from n in lst 
            //        select n.Length;

            Random rand = new Random();
            //var l = lst.Select(n => new { Name = n, n.Length, Weight = rand.Next(100) });

            var l = from t in lst
                    select new { Name = t, t.Length, Weight = rand.Next(100) };

            foreach (var n in l)
                Console.WriteLine(n);


        }
    }
}
