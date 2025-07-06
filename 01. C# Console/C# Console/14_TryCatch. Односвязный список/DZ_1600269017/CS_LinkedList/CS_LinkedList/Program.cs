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
            Console.WriteLine($"Count: {lst.Count}");
        }
    }
}
