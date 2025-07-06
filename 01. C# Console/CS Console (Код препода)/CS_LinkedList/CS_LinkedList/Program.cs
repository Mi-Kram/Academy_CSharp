using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_LinkedList
{
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList lst = new LinkedList();
            lst.Add("First");
            lst.Add(23);
            lst.Add("End");

            lst.Print();
            Console.WriteLine();

            Console.WriteLine($"Count: {lst.Count}");
            Console.WriteLine($"Int32Count: {lst.IntCount()}\n");

            foreach (var item in lst)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            for (int i = 0; i < lst.Count; i++)
            {
                Console.WriteLine($"lst[{i}] = {lst[i]}");
            }



            Console.WriteLine("\n\n\n");
        }
    }
}
