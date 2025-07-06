using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_LinkedList
{
    class LinkedList
    {
        Element first, last;
        int count = 0;

        public int Count { get => count; }

        public LinkedList()
        {
            first = last = null;
        }

        public void Add(object data)
        {
            Element elem = new Element(data, null);

            // если список пуст
            if (first == null)
            {
                // создать первый элемент списка
                last = first = elem;
            }
            else
            {
                // связать последний элемент с новым
                last.next = elem;

                // объявить новый элемент последним
                last = elem;
            }

            count++;
        }

        public void Print()
        {
            // получить ссылку на первый элемент списка
            Element current = first;

            // перебрать все элементы списка
            while(current != null)
            {
                Console.WriteLine(current.Data);
                current = current.next;
            }
        }

        public int IntCount()
        {
            int cnt = 0;

            // получить ссылку на первый элемент списка
            Element current = first;

            // перебрать все элементы списка
            while (current != null)
            {
                if (Int32.TryParse(current.Data.ToString(), out Int32 q)) cnt++;
                current = current.next;
            }

            return cnt;
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
                if (index < 0 || index >= count) throw new Exception("Error in index");

                Element current = first;

                for (int i = 0; i < index; i++)
                {
                    current = current.next;
                }

                return current.Data;
            }
        }
    }
}
