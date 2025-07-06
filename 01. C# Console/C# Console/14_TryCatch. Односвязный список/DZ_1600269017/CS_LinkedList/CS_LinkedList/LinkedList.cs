using System;
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
    }
}
