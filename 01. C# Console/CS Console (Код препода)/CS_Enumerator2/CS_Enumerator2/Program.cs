using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Enumerator2
{
    class Test1
    {
        int[] m = { 5, 4, 3, 2, 1 };

        int[] m2 = { 9, 7, 5, 3, 1 };

        public IEnumerator GetEnumerator()
        {
            Console.WriteLine("GetEnumerator");

            for (int i = 0, k = 4; i < m.Length; i++, k--)
            {
                yield return m[i] + m2[k];
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Test1 t1 = new Test1();
            foreach (int n in t1)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine();

            foreach (int n in t1)
            {
                Console.WriteLine(n);
            }
        }
    }
}
