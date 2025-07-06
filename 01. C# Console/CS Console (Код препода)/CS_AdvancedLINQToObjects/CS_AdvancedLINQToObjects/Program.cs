using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_AdvancedLINQToObjects
{
    class Author
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        private int au_id;

        public int Au_id
        {
            get { return au_id; }
            set { au_id = value; }
        }

        public Author(int au_id, string name, string adr, int age)
        {
            this.au_id = au_id;
            this.name = name;
            this.address = adr;
            this.age = age;
        }

        public void Print()
        {
            Console.WriteLine("{0} {1} {2} {3}", this.au_id, this.name, this.address, this.age);
        }
    }

    class Book
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private int pages;

        public int Pages
        {
            get { return pages; }
            set { pages = value; }
        }

        private int au_id;

        public int Au_id
        {
            get { return au_id; }
            set { au_id = value; }
        }

        private int book_id;

        public int Book_id
        {
            get { return book_id; }
            set { book_id = value; }
        }

        public Book(int book_id, int au_id, string title, int pages)
        {
            this.au_id = au_id;
            this.title = title;
            this.book_id = book_id;
            this.pages = pages;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Author[] authors =
            {
                new Author(1, "Pushkin", "Piter", 300),
                new Author(2, "Nekrasov", "Moskow", 200),
                new Author(3, "Tolstoy", "Piter", 340),
                new Author(4, "Konan-Doyle", "London", 300),
                new Author(4, "Kristy", "London", 250),
            };

            Book[] books = {
                new Book(1, 1, "Poetry", 300),
                new Book(2, 1, "Fairy-tales", 200),
                new Book(3, 3, "Aelita", 340)
            };

            List<Author> lst = new List<Author>(authors);

            // Сформировать из списка авторов коллекцию имён и взять последний объект
            //string name = lst.Select(n => n.Name.ToUpper()).Last();

            string name = (from t in lst
                           select t.Name.ToUpper()).Last();

            Console.WriteLine(name);

            // Сформировать из списка авторов новый список объектов и получить количество объектов в новом списке
            Console.WriteLine("Count: {0}", authors.Select(n => n).Count());

            // Сформировать из списка авторов новый список объектов и получить максимальный возраст
            Console.WriteLine("Max age: {0}", lst.Select(n => n).Max(n => n.Age));

            // Объединение списков авторов и книг в новый список объектов нового типа, задаваемого свойствами
            //var res = authors.Join(books, a => a.Au_id, b => b.Au_id, (a, b) =>
            //    new { a.Name, Title = string.Format("{0} ({1} pages)", b.Title, b.Pages) });

            var res = from b in books
                      join a in authors 
                      on b.Au_id equals a.Au_id
                      select new
                      {
                          a.Name,
                          Title = string.Format("{0} ({1} pages)", b.Title, b.Pages)
                      };

            foreach (var n in res)
                Console.WriteLine(n);

            // Группировка авторов по адресу
            /*var groups = authors.GroupBy(a => a.Address);

            // Для каждой группы вывести название группы, количество объектов в группе и минимальный возраст внутри группы
            foreach (var n in groups)
                Console.WriteLine("{0}, {1}, {2}", n.Key, n.Count(), n.Min(a => a.Age));

            */

            var groups = from t in authors
                         group t by t.Address into g
                         select new
                         {
                             Adr = g.Key,
                             Count = g.Count(),
                             Min = g.Min(n => n.Age)
                         };

            // Для каждой группы вывести название группы, количество объектов в группе и минимальный возраст внутри группы
            foreach (var n in groups)
                Console.WriteLine("{0}, {1}, {2}", n.Adr, n.Count, n.Min);

            // Получить из списка авторов автора с определённым именем, если такого автора нет - вернуть объект по умолчанию
            Author au = authors.Where(n => n.Name.Equals("Barto")).DefaultIfEmpty(new Author(5, "Terry", "New York", 23)).First();
            au.Print();

            // формирование диапазона значений
            IEnumerable<int> numbers = Enumerable.Range(5, 6);

            foreach (int n in numbers)
                Console.WriteLine(n);

            // повторить значение
            IEnumerable<string> str = Enumerable.Repeat("Hello", 5);

            foreach (string s in str)
                Console.WriteLine(s);


            /*
            1. Функция возвращает количество вхождений элемента в заданном массиве чисел.
               var arr = { 1, 0, 2, 2, 3 };
               NumberOfOccurrences(arr, 0) -> 1

            2. Функция принимает массив, который содержит повторяющиеся числа. Только одно число в 
               массиве не повторяется. Функция возвращает это число
               GetUnique([1, 1, 1, 2, 1, 1]) -> 2
               GetUnique([0, 2, 0.1, 2, 0]) -> 0.1

            3. Строка состоит из слов, которые разделены пробелами и могут повторяться. Функция принимает строку
               и удаляет в ней все повторяющиеся слова, оставляя их в одном экземпляре в том месте, где они первый раз встретились.
               RemoveDuplicateWords("Hello big world big Hello") -> "Hello big world"    

            4. Функция принимает строку, которая содержит буквы и цифры и возвращает число, которое состоит
               из максимального количества цифр, идущих подряд в строке
               Solve("12hello987big89world") -> 987

            5. Функция принимает строку, содержащую слова, разделённые пробелами, производит реверс каждого
               слова, объединяет их в результирующую строку и возвращает эту строку
               ReverseWords("Hello Big World") -> "olleH giB dlroW"

            6. Функция принимает строку и возвращает строку, состоящую из первых букв каждого слова исходной строки
               MakeString("Hello Big World") -> "HBW"

            7. Функция принимает строку и возвращает отсортированный массив индексов заглавных букв
               Capitals("Hello World") -> { 0, 6 }
            */

            var arr = NoOdds(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            foreach(var c in arr)
                Console.WriteLine(c);

            Console.WriteLine(@"Smallest Index: {0}", FindSmallestIndex(new int[] { 10, 2, 3, 4, 5, 6, 7, 8, 9 }));

            Console.WriteLine(@"Vowel Count: {0}", GetVowelCount("Hello big world"));
        }

        // функция принимает массив чисел и возвращает исходный массив, но без нечётных чисел
        public static int[] NoOdds(int[] values) => 
              (from v in values
              where (v % 2 == 0)
              select v).ToArray();

        // функция возвращает индекс минимального элемента входного массива
        public static int FindSmallestIndex(int[] numbers) => Array.IndexOf(numbers, numbers.Min());

        // функция возвращает количество гласных букв в переданной строке
        public static int GetVowelCount(string str) => str.Count(letter => "aeiouy".Contains(letter));

    }
}
