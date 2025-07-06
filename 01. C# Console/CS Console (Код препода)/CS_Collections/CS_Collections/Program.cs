using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            // Стек, хранящий числа
            Stack<int> stack = new Stack<int>();

            // Поместить в стек
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            stack.Push(4);
            stack.Push(5);

            // Пока стек не пуст
            while (stack.Count > 0)
            {
                // Вытащить из стека и занести в переменную (происходит удаление из стека)
                int num = stack.Pop();
                Console.WriteLine(num);
            }

            Console.WriteLine();

            // Очередь, хранящая числа
            Queue<int> queue = new Queue<int>();

            // Поместить в очередь
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Enqueue(5);

            // Пока очередь не пуста
            while (queue.Count > 0)
            {
                // Вытащить из очереди и поместить в переменную (из очереди происходит удаление)
                int num = queue.Dequeue();
                Console.WriteLine(num);
            }
            */

            // HashSet - множество на основе Hash-таблицы
            //HashSet<string> set = new HashSet<string>

            // HashSet - множество на основе бинарного дерева
            SortedSet<string> set = new SortedSet<string>
            {
                "First",
                "Start",
                "Second",
                "Third",
                "First",
                "Ok",
                "End"
            };

            // добавление во множество
            set.Add("Start");

            // удаление из множества
            set.Remove("Ok");

            // перебрать все элементы множества
            foreach (var item in set)
            {
                Console.WriteLine(@"{0}", item);
            }

            // Проверка вхождения во множества
            Console.WriteLine(set.Contains("First"));

            Console.WriteLine();

            // Перечение множеств
            //var result = set.Intersect(new HashSet<string>{"One", "Two", "First", "Second"});

            // Объединение множеств
            //var result = set.Union(new HashSet<string> { "One", "Two", "First", "Second", "Hello world" });

            // Вычитание (исключение)
            var result = set.Except(new HashSet<string> { "One", "Two", "First", "Second" });

            foreach (var item in result)
            {
                Console.WriteLine(@"{0}", item);
            }
        }
    }
}
