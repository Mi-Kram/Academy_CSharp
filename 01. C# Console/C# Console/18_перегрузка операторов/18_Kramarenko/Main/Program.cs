using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"C:\Users\Михаил\source\repos\18_Kramarenko\Main\List.txt";

                DoubleLinkedList lst = new DoubleLinkedList();

                lst.Add("one");
                lst.Add("two");
                lst.Add("four");
                lst.Add("five");
                lst.Insert(2, "three");
                lst.Print();
                Console.ReadLine();


                Writer("\nlst += \"wwwwwww\":");
                lst += "wwwwwww";
                lst.Print();
                Console.ReadLine();


                DoubleLinkedList lst2 = new DoubleLinkedList("11111","22222","33333","44444","55555");
                Writer("\nlst += lst2:");
                lst += lst2;
                lst.Print();
                Console.ReadLine();


                Writer("\nlst == lst:");
                Console.WriteLine(lst==lst?true:false);

                Writer("\nlst == lst2:");
                Console.WriteLine(lst==lst2?true:false);
                Console.ReadLine();


                Writer("\nConsole.WriteLine(lst) - кол-во элементов");
                Console.WriteLine(lst);

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");
        }

        public static void Writer(string str)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.WriteLine(str);
            Console.ResetColor();
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
        public DoubleLinkedList(params object[] arr)
        {
            first = last = null;
            length = 0;

            foreach (var item in arr)
            {
                Add(item);
            }
        }
        public DoubleLinkedList(DoubleLinkedList lst)
        {
            first = last = null;
            length = 0;

            Element current = lst.first;

            while (current != null)
            {
                Add(current.Data);
                current = current.next;
            }
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
            if (first.Data.ToString() == value.ToString())
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

                    if (tmp.Data.ToString() == value.ToString())
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


        public static DoubleLinkedList operator +(DoubleLinkedList p, object obj)
        {
            DoubleLinkedList res = new DoubleLinkedList(p);
            res.Add(obj);

            return res;
        }
        public static DoubleLinkedList operator +(DoubleLinkedList p, DoubleLinkedList lst)
        {
            DoubleLinkedList res = new DoubleLinkedList(p);

            Element current = lst.first;
            while (current != null)
            {
                res.Add(current.Data);
                current = current.next;
            }

            return res;
        }

        public static DoubleLinkedList operator -(DoubleLinkedList p, object obj)
        {
            DoubleLinkedList res = new DoubleLinkedList(p);
            res.DeleteFirst(obj);

            return res;
        }

        public static bool operator ==(DoubleLinkedList p, DoubleLinkedList p2)
        {
            if (p.length != p2.length) return false;

            Element cur1 = p.first;
            Element cur2 = p2.first;

            while (cur1 != null)
            {
                if (cur1.Data.ToString() != cur2.Data.ToString()) return false;

                cur1 = cur1.next;
                cur2 = cur2.next;
            }

            return true;
        }
        public static bool operator !=(DoubleLinkedList p, DoubleLinkedList p2)
        {
            if (p.length != p2.length) return true;

            Element cur1 = p.first;
            Element cur2 = p2.first;

            while (cur1 != null)
            {
                if (cur1.Data.ToString() != cur2.Data.ToString()) return true;

                cur1 = cur1.next;
                cur2 = cur2.next;
            }

            return false;
        }

        public override bool Equals(object obj)
        {
            try
            {
                if (this.length != (obj as DoubleLinkedList).length) return false;

                Element cur1 = this.first;
                Element cur2 = (obj as DoubleLinkedList).first;

                while (cur1 != null)
                {
                    if (cur1.Data.ToString() != cur2.Data.ToString()) return false;

                    cur1 = cur1.next;
                    cur2 = cur2.next;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static implicit operator int(DoubleLinkedList p)
        {
            return p.length;
        }
    }
}