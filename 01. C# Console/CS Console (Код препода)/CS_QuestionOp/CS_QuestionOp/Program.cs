using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CS_QuestionOp
{
    class Person
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public void Print()
        {
            Console.WriteLine($"Person: {FirstName} {LastName}, Age: {Age}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // классический тернарный оператор
            /*int n = 5, k = 7;
            int val = n > k ? n : k;
            Console.WriteLine($"val = {val}");*/

            /*int[] a = new int[] { 1, 2, 3, 4, 5 };
            int[] b = new int[] { 9, 8, 7, 6, 5 };

            // создание ссылки на элемент массива
            ref int r = ref a[3];

            // изменение ячейки массива по ссылке
            r = 33;

            foreach(int c in a)
                Console.Write($"{c} ");
            Console.WriteLine();

            // тернарный оператор для ссылочных переменных
            ref int val2 = ref (a.Sum() > b.Sum()) ? ref a[0] : ref b[3];

            // изменение значения по ссылке
            val2 = 100;

            // использование тернарного оператора слева в операторе присваивания по ссылке
            ((a.Sum() > b.Sum()) ? ref a[0] : ref b[3]) = 300;

            Console.WriteLine("\nArray a:");
            foreach (int c in a)
                Console.Write($"{c} ");
            Console.WriteLine();

            Console.WriteLine("\nArray b:");
            foreach (int c in b)
                Console.Write($"{c} ");
            Console.WriteLine();*/

            // объявление переменной простого типа с возможностью занесения null
            /*int? val = 345;
            Console.WriteLine(val.HasValue);
            Console.WriteLine(val);*/

            // метод Print будет вызван только, если person != null
            /*Person person = null;
            person?.Print();

            // если ссылка на массив = null, то значение выражения arr?[3] будет равно null
            int[] arr = null;
            int? t = arr?[3];

            Console.WriteLine(t.HasValue);*/

            List<int> numbers = null;
            int? a = null;

            // выражение (a ?? 10) возвратит a, если a не равно null, иначе 10
            Console.WriteLine(a ?? 10);

            // выражение (numbers ??= new List<int>()) возвратит numbers,
            // если numbers не равно null, иначе в numbers будет присвоено new List<int>()
            // и это будет возвращено наружу
            (numbers ??= new List<int>()).Add(5);
            Console.WriteLine(string.Join(" ", numbers));

            // в переменную a присваивается 7, потому что a==null, потом a добавляется к списку numbers
            numbers.Add(a ??= 7);
            Console.WriteLine(string.Join(" ", numbers));
            Console.WriteLine(a);
        }
    }
}
