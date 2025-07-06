using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Reflection
{
    delegate void TestDelegate(string str);

    class Test1
    {
        int a = 3, b = 5;

        public event TestDelegate MyEvent;

        event TestDelegate MyEvent2;

        public int A
        {
            get
            {
                return a;
            }
            set
            {
                a = value;
            }
        }

        public Test1()
        {
        }

        public Test1(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        public void print()
        {
            Console.WriteLine("a={0} b={1}", a, b);
        }
        private int GetSum()
        {
            return a + b;
        }
        public void set(int a, int b)
        {
            Console.WriteLine("Method set!");
            this.a = a;
            this.b = b;
        }
    }
}
