using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            //Tasks.Task1();
            Tasks.Task2();


            End();
        }

        static void End()
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

    class Tasks
    {
        public static void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. Пользователь вводит полный путь к папке. Программа подсчитывает ");
            Console.WriteLine("       частотный словарь английских букв в текстовых файлах            \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\14_Kramarenko\Main\Task1";

                DirectoryInfo d = new DirectoryInfo(path);
                if (!d.Exists) throw new Exception("Dir is not found");

                LetFile lf = new LetFile();
                var files = d.GetFiles("*.txt");

                foreach (var file in files)
                {
                    lf.Count(file.FullName);
                }

                lf.Show();

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
        public static void Task2()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task2. В класс LinkedList добавить следующие методы:         ");
            Console.WriteLine("       - энумератор для цикла foreach                        ");
            Console.WriteLine("       - индексатор this[]                                   ");
            Console.WriteLine("       - GetNumberCount() - количество целых чисел в списке  ");
            Console.WriteLine("       - Insert(int index, object value) - вставка           ");
            Console.WriteLine("         значения перед элементом с индексом                 ");
            Console.WriteLine("       - Delete(int index) - удаление по индексу             ");
            Console.WriteLine("       - Sort() - сортировка списка                          ");
            Console.WriteLine("       - Save, Load - сохранение, загрузка списка в файл     \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\14_Kramarenko\Main\Task2\List.txt";

                LinkedList lst = new LinkedList();

                lst.Add("one");
                lst.Add("two");
                lst.Add("four");
                lst.Add("five");
                lst.Insert(2, "three");
                lst.Print();
                Console.ReadLine();

                lst.Sort();
                lst.Print();
                Console.ReadLine();

                lst.Delete(0);
                lst.Delete(lst.Length-1);
                lst.Print();
                Console.ReadLine();
                Console.WriteLine("\n\n");

                lst.Save(path);
                LinkedList lst2 = new LinkedList();
                lst2.Load(path);
                lst2.Print();

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    class LetFile
    {
        int[] count;

        public LetFile()
        {
            count = new int[(int)('z' - 'a' + 1)];
        }

        public void Count(string path)
        {
            if (!File.Exists(path)) throw new Exception("File is not exit");

            string str = File.ReadAllText(path);
            str = str.ToLower();

            foreach (var chr in str)
            {
                if (IsEng(chr))
                {
                    count[(int)chr - (int)'a']++;
                }
            }

        }
        private bool IsEng(char ch)
        {
            return ch >= 'a' && ch <= 'z' || ch >= 'A' && ch <= 'Z';
        }

        public void Show()
        {
            for (int i = 0; i < count.Length; i++)
            {
                Console.WriteLine($"{(char)((int)'a' + i)}: {count[i]}");
            }
        }
    }


    class Element
    {
        public object Data { get; set; }
        public Element next { get; set; }

        public Element(object data, Element next)
        {
            Data = data;
            this.next = next;
        }
    }
    class LinkedList
    {
        Element first, last;
        int length = 0;

        public int Length { get => length; }

        public LinkedList()
        {
            first = last = null;
        }

        public void Add(object data)
        {
            Element elem = new Element(data, null);

            if (first == null)
            {
                last = first = elem;
            }
            else
            {
                last.next = elem;
                last = elem;
            }

            length++;
        }
        public void Insert(int index, object data)
        {
            Element elem = new Element(data, null);

            if (length != 0)
            {
                if (index < 0 || index > length) throw new Exception("Error in index");

                if(index == 0)
                {
                    elem.next = first;
                    first = elem;
                }
                else if(index == length)
                {
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

            if(index == 0)
            {
                first = first.next;
            }
            else 
            {
                Element cur = first;
                for (int i = 0; i < index-1; i++)
                {
                    cur = cur.next;
                }

                cur.next = cur.next.next;
                if (index == length - 1) last = cur.next;
            }

            length--;
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
            StreamReader sr = new StreamReader(path);


            if (length != 0)
            {
                first = last = null;
                length = 0;
            }

            while (!sr.EndOfStream)
            {
                Add(sr.ReadLine());
            }

            sr.Close();
        }
    }

}
