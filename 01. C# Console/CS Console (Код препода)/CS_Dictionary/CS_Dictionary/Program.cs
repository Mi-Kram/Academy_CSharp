using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Hashtable ht = new Hashtable();

            //Добавление элементов в хеш-таблицу
            // первый параметр - ключ, второй параметр - значение
            ht.Add("здание", "house");
            ht.Add("машина", "machine");
            ht.Add("книга", "book");
            ht.Add("яблоко", "apple");*/

            Hashtable ht = new Hashtable
            {
                //Добавление элементов в хеш-таблицу
                { "здание", "house" },
                { "машина", "machine" },
                { "книга", "book" },
                { "яблоко", "apple" }
            };

            // вызывает исключение для повторяющихся ключей
            //ht.Add("здание", "something");

            // Добавление элемента с помощью индексатора (или перезапись уже существующего ключа)
            ht["здание"] = "building";

            // Удаление элемента по ключу
            ht.Remove("книга");

            //Получение коллекции ключей
            ArrayList с = new ArrayList(ht.Keys);

            //Используем ключи для получения значений
            foreach (string str in с)
                Console.WriteLine(str + " : " + ht[str]);

            Console.WriteLine("");

            // Получение кол-ва элементов
            Console.WriteLine(ht.Count);

            Console.WriteLine("");

            //Получение коллекции значений
            ArrayList v = new ArrayList(ht.Values);

            foreach (string str in v)
                Console.WriteLine(str);

            Console.WriteLine("");

            // создание копии хэш-таблицы
            Hashtable ht2 = (Hashtable)ht.Clone();

            // Определение содержания значения
            Console.WriteLine(ht2.ContainsKey("книга"));
            Console.WriteLine(ht2.ContainsKey("яблоко"));

            // быстрый поиск для Value не поддерживатся
            Console.WriteLine(ht2.ContainsValue("apple"));

            Dictionary<char, long> dict = new Dictionary<char, long>
            {
                { 'A', 45 },
                { 'B', 345 },
                { 'C', 4656 }
            };

            foreach (var item in dict)
            {
                Console.WriteLine(@"{0} : {1}", item.Key, item.Value);
            }

            if (dict.ContainsKey('z'))
            {
                long size = dict['z'];
                Console.WriteLine(size);
            }

            // Происходит сортировка ключа
            // Сортирующий словарь на основе отсортированного массива (дихотомический поиск)
            // Быстрее работает чтение и запись значений существующих элементов
            SortedList<string, int> list = new SortedList<string, int>();

            // Происходит сортировка ключа
            // Сортирующий словарь на основе бинарного дерева (дихотомический поиск)
            // Быстрее работает вставка нового элемента и удаление существующих элементов
            SortedDictionary<string, int> sorted_dict = new SortedDictionary<string, int>();
        }
    }
}
