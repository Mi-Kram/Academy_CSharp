using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_Reflection
{
    class Person
    {
        string name = "Vasya Pupkin";
        string address = "Donetsk";
        public void print()
        {
            Console.WriteLine("{0} {1}", name, address);
        }
    }

    class Program
    {
        static ArrayList list = new ArrayList();

        static void Main(string[] args)
        {
            Test1 test1 = new Test1();
            test1.print();

            //Type t = typeof(Test1);

            // Информации о ссылке obj нет никакой
            object obj = test1;

            // получить информацию о типе
            Type t = obj.GetType();

            Console.WriteLine("Full name of type: {0}\n", t.FullName);

            if (t.IsClass == true) Console.WriteLine(t.FullName + " is a class\n");
            else Console.WriteLine(t.FullName + " is not a class\n");

            if (t.IsAbstract == true) Console.WriteLine(t.FullName + " is an abstract class\n");
            else Console.WriteLine(t.FullName + " is not an abstract class\n");

            if (t.IsEnum == true) Console.WriteLine(t.FullName + " is an enum\n");
            else Console.WriteLine(t.FullName + " is not an enum\n");

            //Получаем информацию о конструкторах.
            ConstructorInfo[] ci = t.GetConstructors();
            Console.WriteLine("Constructors:");

            foreach (ConstructorInfo с in ci)
            {
                //Отображаем тип возвращаемого значения и имя.
                Console.Write("" + t.Name + "(");
                //Отображаем параметры.
                ParameterInfo[] pi = с.GetParameters();
                for (int i = 0; i < pi.Length; i++)
                {
                    Console.Write(pi[i].ParameterType.Name + " " + pi[i].Name);
                    if (i + 1 < pi.Length) Console.Write(", ");
                }
                Console.WriteLine(")");
            }

            Console.WriteLine("\nDeclared fields:");

            // Информация о полях класса
            FieldInfo[] fi = t.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(FieldInfo fieldInfo in fi)
            {
                Console.WriteLine($"Field: {fieldInfo.FieldType.Name} {fieldInfo.Name}");
            }

            Console.WriteLine("\nDeclared events:");

            // Информация о событиях класса (все events)
            var events = t.GetRuntimeEvents();
            foreach (EventInfo eventInfo in events)
            {
                Console.WriteLine($"Event: {eventInfo.EventHandlerType.Name} {eventInfo.Name}");
            }

            Console.WriteLine("\nNon public methods:");
            MethodInfo[] mi = t.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo m in mi)
            {
                Console.Write("{0} {1}(", m.ReturnType.Name, m.Name);
                ParameterInfo[] pi = m.GetParameters();
                for (int i = 0; i < pi.Length; i++)
                {
                    ParameterInfo p = pi[i];
                    Console.Write(p.ParameterType.Name + " " + p.Name);
                    if (i < pi.Length - 1) Console.Write(", ");
                }
                Console.WriteLine(")");
            }

            Console.WriteLine("\npublic methods:");
            mi = t.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            foreach (MethodInfo m in mi)
            {
                // запуск метода на выполнение
                if (m.Name == "set")
                {
                    // массив параметров для запуска метода
                    object[] arg = new object[2];
                    arg[0] = 10;
                    arg[1] = 30;
                    m.Invoke(obj, arg); // test1.set(10, 20);

                    Console.WriteLine("");
                    test1.print();
                    Console.WriteLine("");
                }

                Console.Write("{0} {1}(", m.ReturnType.Name, m.Name);

                ParameterInfo[] pi = m.GetParameters();

                for (int i = 0; i < pi.Length; i++)
                {
                    ParameterInfo p = pi[i];
                    Console.Write(p.ParameterType.Name + " " + p.Name);
                    if (i < pi.Length - 1) Console.Write(", ");
                }
                Console.WriteLine(")");
            }

            /*
            Random r = new Random();
            for (int i = 0; i < 10;i++ )
            {
                int n = r.Next(1,3);
                //Console.WriteLine(n);
                if (n == 1) list.Add(new Person());
                else list.Add("hello");
            }
            foreach (object obj2 in list)
            {
                if (obj2.GetType().Name == "Person")
                //if (obj2 is Person)
                {
                    MethodInfo[] mi2 = obj2.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
                    foreach (MethodInfo m in mi2)
                    {

                        if (m.Name == "print")
                        {
                            m.Invoke(obj2, null);
                        }
                    }

                }
                else
                {
                    Console.WriteLine((string)obj2);
                }
            }*/
        }
    }
}
