using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {

            Application app = new Application();
            app.Run();
        }
    }

    class Tasks
    {
        public static void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. Переделать класс LinkedList в двусвязный список и назвать его DoubleLinkedList ");
            Console.WriteLine("       - DeleteFirst(object val) - удаление первого вхождения значения в списке       ");
            Console.WriteLine("       - DeleteAll(object val) - удаление всех вхождений значения в списке            ");
            Console.WriteLine("       - AddList(DoubleLinkedList lst) - добавление другого списка в конец текущего   \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\15_Kramarenko\Main\TMP\List.txt";
                DoubleLinkedList lst = new DoubleLinkedList();

                lst.Add("*****");
                lst.Add("*****");
                lst.Add("one");
                lst.Add("two");
                lst.Add("*****");
                lst.Add("three");
                lst.Add("*****");
                lst.Add("*****");
                lst.Add("four");
                lst.Add("five");
                lst.Add("*****");
                lst.Add("*****");
                lst.Add("*****");

                lst.Print();
                while (Console.ReadKey(true).KeyChar != '\r') { }
                Console.WriteLine('\n');

                lst.DeleteAll("*****");
                lst.Print();
                while (Console.ReadKey(true).KeyChar != '\r') { }
                Console.WriteLine('\n');

                lst.Sort();
                lst.Print();
                while (Console.ReadKey(true).KeyChar != '\r') { }
                Console.WriteLine('\n');

                lst.Delete(0);
                lst.Delete(lst.Length - 1);
                lst.Print();
                while (Console.ReadKey(true).KeyChar != '\r') { }
                Console.WriteLine('\n');


                Console.WriteLine("Created and loaded new DoubleLinkedList:\n");
                lst.Save(path);
                DoubleLinkedList lst2 = new DoubleLinkedList();
                lst2.Load(path);
                lst2.Print();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }

    }

    class Element
    {
        public object Data { get; set; }
        public Element next { get; set; }
        public Element prev { get; set; }

        public Element(object data, Element prev, Element next)
        {
            Data = data;
            this.next = next;
            this.prev = prev;
        }
    }
    class DoubleLinkedList
    {
        Element first, last;
        int length;

        public int Length { get => length; }

        public DoubleLinkedList()
        {
            first = last = null;
            length = 0;
        }

        public void Add(object data)
        {
            Element elem = new Element(data, null, null);

            if (first == null)
            {
                last = first = elem;
            }
            else
            {
                elem.prev = last;
                last.next = elem;
                last = elem;
            }

            length++;
        }
        public void AddList(DoubleLinkedList lst)
        {
            Element cur = lst.first;
            while (cur != null)
            {
                Add(cur.Data);
                cur = cur.next;
            }
        }
        public void Insert(int index, object data)
        {
            Element elem = new Element(data, null, null);

            if (length != 0)
            {
                if (index < 0 || index > length) throw new Exception("Error in index");

                if (index == 0)
                {
                    elem.next = first;
                    first.prev = elem;
                    first = elem;
                }
                else if (index == length)
                {
                    elem.prev = last;
                    last.next = elem;
                    last = elem;
                }
                else
                {
                    Element cur = first;
                    for (int i = 0; i < index - 1; i++)
                    {
                        cur = cur.next;
                    }

                    elem.next = cur.next;
                    cur.next = elem;

                    elem.next.prev = elem;
                    elem.prev = cur;
                }
            }
            else
            {
                first = last = elem;
            }
            length++;
        }

        public void Delete(int index)
        {
            if (index < 0 || index >= length) throw new Exception("Error in index");

            if (index == 0)
            {
                first = first.next;
                first.prev = null;
            }
            else
            {
                Element cur = first;
                for (int i = 0; i < index - 1; i++)
                {
                    cur = cur.next;
                }

                cur.next = cur.next.next;
                if (index == length - 1) last = cur;
                else cur.next.prev = cur;

            }

            length--;
        }
        public void DeleteFirst(object value)
        {
            if(first.Data.ToString() == value.ToString())
            {
                first = first.next;
                first.prev = null;
                length--;
            }
            else
            {
                Element cur = first;

                while (cur != last)
                {
                    Element tmp = cur.next;

                    if(tmp.Data.ToString() == value.ToString())
                    {
                        cur.next = cur.next.next;
                        if (tmp == last) last = cur;
                        else cur.next.prev = cur;

                        length--;
                        return;
                    }

                    cur = cur.next;
                }
            }
        }
        public void DeleteAll(object value)
        {
            while (first.Data.ToString() == value.ToString())
            {
                first = first.next;
                first.prev = null;
                length--;
            }

            Element cur = first;

            while (cur.next != null)
            {
                Element tmp = cur.next;

                if (tmp.Data.ToString() == value.ToString())
                {
                    cur.next = cur.next.next;
                    if (tmp == last) last = cur;
                    else cur.next.prev = cur;

                    length--;
                }
                else cur = cur.next;
            }
        }


        public void Print()
        {
            Element current = first;

            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.next;
            }
        }
        public void PrintWithIndex()
        {
            Element current = first;
            int index = 0;

            while (current != null)
            {
                Console.WriteLine($"[{index++}] - {current.Data}");
                current = current.next;
            }
        }
        public int IntCount()
        {
            int cnt = 0;
            Element current = first;

            while (current != null)
            {
                if (Int32.TryParse(current.Data.ToString(), out Int32 q)) cnt++;
                current = current.next;
            }

            return cnt;
        }
        public void Sort()
        {
            if (length > 1)
            {
                object[] arr = new object[length];
                Element elem = first;
                for (int i = 0; i < length; i++)
                {
                    arr[i] = elem.Data;
                    elem = elem.next;
                }
                try
                {
                    Array.Sort(arr);

                    first = last = null;
                    length = 0;
                    foreach (var item in arr)
                    {
                        Add(item);
                    }
                }
                catch
                {
                    Console.WriteLine("Different types in LinkedList");
                }
            }
        }

        public IEnumerator GetEnumerator()
        {
            Element current = first;

            while (current != null)
            {
                yield return current.Data;
                current = current.next;
            }
        }
        public object this[int index]
        {
            get
            {
                if (index < 0 || index >= length) throw new Exception("Error in index");

                Element current = first;

                for (int i = 0; i < index; i++)
                {
                    current = current.next;
                }

                return current.Data;
            }
        }


        public void Save(string path)
        {
            StreamWriter sw = new StreamWriter(path);
            if (!File.Exists(path)) throw new Exception("File is not found");

            Element cur = first;
            while (cur != null)
            {
                sw.WriteLine(cur.Data);

                cur = cur.next;
            }

            sw.Close();
        }
        public void Load(string path)
        {
            if (!File.Exists(path)) throw new Exception("File is not found");

            if (length != 0)
            {
                first = last = null;
                length = 0;
            }

            string[] lines = File.ReadAllLines(path);
            foreach (var item in lines)
            {
                Add(item);
            }
        }
    }


    delegate void menuitem_handler();
    class MenuItem
    {
        char key;
        string title;
        menuitem_handler handler;

        public MenuItem(char key, string title, menuitem_handler handler)
        {
            this.key = key;
            this.title = title;
            this.handler = handler;
        }

        public char Key
        {
            get => key;
        }
        public string Title
        {
            get => title;
        }
        public menuitem_handler Handler
        {
            get => handler;
        }
    }

    class Application
    {
        List<MenuItem> menu = new List<MenuItem>();
        DoubleLinkedList lst = new DoubleLinkedList();

        public Application()
        {
            menu.Add(new MenuItem('1', "Show", Show_Pressed));
            menu.Add(new MenuItem('2', "Add", Add_Pressed));
            menu.Add(new MenuItem('3', "AddList", AddList_Pressed));
            menu.Add(new MenuItem('4', "Insert", Insert_Pressed));
            menu.Add(new MenuItem('5', "Delete", Delete_Pressed));
            menu.Add(new MenuItem('6', "DeleteFirst", DeleteFirst_Pressed));
            menu.Add(new MenuItem('7', "DeleteAll", DeleteAll_Pressed));
            menu.Add(new MenuItem('8', "Кол-во целых чисел", IntCount_Pressed));
            menu.Add(new MenuItem('9', "Sort", Sort_Pressed));
            menu.Add(new MenuItem('i', "Получить элемент по индексу", Index_Pressed));
            menu.Add(new MenuItem('s', "Сохранить список в файле", Save_Pressed));
            menu.Add(new MenuItem('l', "Получить список из файла", Load_Pressed));
            menu.Add(new MenuItem('x', "Exit", Exit));
        }

        private void AcceptedMessage(string msg)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        private void DenyedMessage(string msg)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        private void Print()
        {
            Console.WriteLine('\n');
            foreach (var item in menu)
            {
                Console.WriteLine($"\t{item.Key}. {item.Title}");
            }
        }
        public void Run()
        {
            Console.CursorVisible = false;
            while (true)
            {
                Console.Clear();

                Print();

                char letter = Console.ReadKey(true).KeyChar;

                foreach (var item in menu)
                {
                    if (item.Key == letter)
                    {
                        Console.Clear();
                        item.Handler();
                        while (Console.ReadKey(true).KeyChar != '\r') { }
                        break;
                    }
                }
            }
        }

        private void Add_Pressed()
        {
            Console.Write("Data> ");
            lst.Add(Console.ReadLine());

            AcceptedMessage("\n\nЭлемент добавлен");
        }
        private void AddList_Pressed()
        {
            DoubleLinkedList tmp = new DoubleLinkedList();
            int count = 0;

            Console.Write("Кол-во элементов> ");
            while (!int.TryParse(Console.ReadLine(), out count) || count < 1)
            {
                DenyedMessage("\nВведено некорректное число");
                while (Console.ReadKey(true).KeyChar != '\r') { }
                Console.Clear();

                Console.Write("Кол-во элементов> ");
            }
            Console.WriteLine();

            for (int i = 0; i < count; i++)
            {
                Console.Write($"{i + 1}. ");
                tmp.Add(Console.ReadLine());
            }

            lst.AddList(tmp);
            AcceptedMessage("\n\nЭлементы добавлены");
        }
        private void Insert_Pressed()
        {
            int index = 0;

            ShowWithIndex();
            Console.Write("\n\nindex> ");
            while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index > lst.Length)
            {
                DenyedMessage("\nВведен некорректный индекс");
                while (Console.ReadKey(true).KeyChar != '\r') { }
                Console.Clear();

                ShowWithIndex();
                Console.Write("\n\nindex> ");
            }

            Console.Write("Data> ");
            lst.Insert(index, Console.ReadLine());

            AcceptedMessage("\n\nЭлемент добавлен");
        }

        private void Show_Pressed()
        {
            if (lst.Length > 0)
            {
                AcceptedMessage("DoubleLinkedList:\n");
                lst.Print();
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }
        private void ShowWithIndex()
        {
            AcceptedMessage("DoubleLinkedList:\n");
            lst.PrintWithIndex();
        }
        private void IntCount_Pressed()
        {
            if(lst.Length > 0)
            {
                lst.Print();
                Console.WriteLine($"\n\nКол-во целых чисел> {lst.IntCount()}");
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }
        private void Sort_Pressed()
        {
            if (lst.Length > 0)
            {
                lst.Sort();
                lst.Print();
                AcceptedMessage("\n\nСписок отсортирован");
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }
        private void Index_Pressed()
        {
            if(lst.Length > 0)
            {
                int index = 0;

                Console.Write("index> ");
                while (!int.TryParse(Console.ReadLine(), out index) || index < 1 || index >= lst.Length)
                {
                    DenyedMessage("\nВведен некорректный индекс");
                    while (Console.ReadKey(true).KeyChar != '\r') { }
                    Console.Clear();

                    Console.Write("index> ");
                }

                Console.WriteLine($"value> {lst[index]}");
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }

        private void Load_Pressed()
        {
            try
            {
                Console.Write("path> ");
                lst.Load(Console.ReadLine());
                AcceptedMessage("\n\nСписок успешно получен");
            }
            catch (Exception ex)
            {
                DenyedMessage($"\n{ex.Message}");
            }
        }
        private void Save_Pressed()
        {
            if (lst.Length > 0) {
                try
                {
                    Console.Write("path> ");
                    lst.Save(Console.ReadLine());
                    AcceptedMessage("\n\nСписок успешно сохранён");
                }
                catch (Exception ex)
                {
                    DenyedMessage(ex.Message);
                }
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }

        private void Delete_Pressed()
        {
            if (lst.Length > 0)
            {
                ShowWithIndex();
                int index = 0;

                Console.Write("\n\nindex> ");
                while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= lst.Length)
                {
                    DenyedMessage("\nВведен некорректный индекс");
                    while (Console.ReadKey(true).KeyChar != '\r') { }
                    Console.Clear();

                    ShowWithIndex();
                    Console.Write("\n\nindex> ");
                }
                try
                {
                    lst.Delete(index);
                    AcceptedMessage("\n\nЭлемент удалён");
                }
                catch (Exception ex)
                {
                    DenyedMessage(ex.Message);
                }
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }
        private void DeleteFirst_Pressed()
        {
            if (lst.Length > 0)
            {
                try
                {
                    lst.Print();
                    Console.Write("\n\nvalue> ");
                    string value = Console.ReadLine();
                    lst.DeleteFirst(value);
                    AcceptedMessage("\n\nЗадание выполнено");
                }
                catch (Exception ex)
                {
                    DenyedMessage(ex.Message);
                }
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }
        private void DeleteAll_Pressed()
        {
            if (lst.Length > 0)
            {
                try
                {
                    lst.Print();
                    Console.Write("\n\nvalue> ");
                    lst.DeleteAll(Console.ReadLine());
                    AcceptedMessage("\n\nЗадание выполнено");
                }
                catch (Exception ex)
                {
                    DenyedMessage(ex.Message);
                }
            }
            else
            {
                DenyedMessage("Список пуст");
            }
        }

        private void Exit()
        {
            End();
            Environment.Exit(0);
        }
        private void End()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(46, 12);
            Console.Write("                         ");

            Console.SetCursorPosition(46, 13);
            Console.Write("   Программа завершена   ");

            Console.SetCursorPosition(46, 14);
            Console.Write("                         ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
        }

    }

}