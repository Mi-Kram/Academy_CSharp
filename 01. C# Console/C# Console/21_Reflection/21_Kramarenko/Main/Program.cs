using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadMethods(new Test());
        }

        static void LoadMethods(object obj)
        {
            Type t = obj.GetType();
            
            if (t.IsClass)
            {
                MethodInfo[] methods = t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | 
                                                    BindingFlags.Instance | BindingFlags.DeclaredOnly);

                Random rand = new Random();
                string[] strs = new string[]
                {
                    "First",
                    "Second",
                    "Third",
                    "Fourth",
                    "Fifth"
                };

                foreach (MethodInfo method in methods)
                {
                    ParameterInfo[] parameters = method.GetParameters();
                    object[] args = new object[parameters.Count()];

                    for (int i = 0; i < parameters.Count(); i++)
                    {
                        switch (parameters[i].ParameterType.Name)
                        {
                            case "Int32": args[i] = rand.Next(10, 100); break;
                            case "String": args[i] = strs[rand.Next(strs.Count())]; break;
                            case "Double": args[i] = rand.Next(10, 100) + rand.NextDouble(); break;
                            case "Boolean": args[i] = rand.Next(2) == 1 ? true : false; break;
                            default: break;
                        }
                    }

                    method.Invoke(obj, args);
                }
            }
        }
    }

    class Test
    {
        public void Test1()
        {
            Console.WriteLine("Test1() started");
            Console.WriteLine('\n');
        }
        private void Test2()
        {
            Console.WriteLine("Test2() started");
            Console.WriteLine('\n');
        }

        public void Test3(int n, string sourse, string result)
        {
            Console.WriteLine("Test3() started:");
            Console.WriteLine($"int n = {n}");
            Console.WriteLine($"string sourse = {sourse}");
            Console.WriteLine($"string result n = {result}");
            Console.WriteLine('\n');
        }
        private void Test4(bool flag, string message, double dbl)
        {
            Console.WriteLine("Test4() started:");
            Console.WriteLine($"bool flag = {flag}");
            Console.WriteLine($"string message = {message}");
            Console.WriteLine($"double dbl = {dbl}");
            Console.WriteLine('\n');
        }
    }
}
