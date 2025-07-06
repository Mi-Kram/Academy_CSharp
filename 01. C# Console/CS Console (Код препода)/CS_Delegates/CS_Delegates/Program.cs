using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Delegates
{
    // объявление типа данных, задающего определённую сигнатуру функций для запуска
    public delegate int CalcOperations(int a, int b);

    class Program
    {
        // нестатические методы для вызова через делегат operations
        int add(int a, int b)
        {
            Console.WriteLine("add");
            return a + b;
        }
        int mul(int a, int b)
        {
            Console.WriteLine("mul");
            return a * b;
        }

        // переменная типа делегат CalcOperations
        public CalcOperations operations;

        // объявление типа данных, задающего определённую сигнатуру функций для запуска
        public delegate void MyFunc(int a);

        // переменная типа делегат MyFunc
        public static MyFunc del1;

        // статиские методы для вызова через делегат del1
        static void test1(int a)
        {
            Console.WriteLine("Test1 {0}", a);
        }

        static void test2(int a)
        {
            Console.WriteLine("Test2 {0}", a * a);
        }

        static void Main(string[] args)
        {
            // присвоение методов
            del1 = test1;

            // обычный запуск метода
            //test1(345);

            // запуск метода test1 через делегат del1
            //del1(123);

            // присвоение методов
            MyFunc del2 = new MyFunc(test2), del3 = null;

            // использование операторов (комбинирование делегатов)
            del3 = del1 + del2 + del1;
            //del3 = del3 - del1;

            // комбинирование делегатов через методы
            /*del3 = (MyFunc)MyFunc.Combine(del2, del1);
            del3 = (MyFunc)MyFunc.Combine(del3, del1);
            del3 = (MyFunc)MyFunc.Remove(del3, del1);
            del3 = (MyFunc)MyFunc.Remove(del3, del1);*/

            // запуск методов
            del3(3);

            //del3.Invoke(3);

            // обработка нестатических методов
            Program pr = new Program();

            // добавление в переменную типа делегат CalcOperations методов
            pr.operations += new CalcOperations(pr.mul);
            pr.operations += pr.add;

            // запуск методов через делегат (возвращается результат последнего метода, на который ссылается переменная-делегат)
            Console.WriteLine(pr.operations(3, 4));

            // динамический вызов методов из переменной-делегата
            Console.WriteLine("DynamicInvoke: ");
            foreach(var current in pr.operations.GetInvocationList())
            {
                Console.WriteLine(current.DynamicInvoke(3, 4));
            }
            Console.WriteLine("-----\n");

            // объявление переменной делегата и присвоение в него анонимного метода
            CalcOperations op2 = delegate (int a, int b)
            {
                return a - b;
            };

            // добавление других методов в переменную-делегат
            op2 += pr.add;
            op2 += new CalcOperations(pr.mul);
            op2 += delegate (int a, int b)
            {
                return a % b;
            };

            // добавление метода-лямбда
            op2 += (a, b) => a % b;

            // запуск методов через делегат
            Console.WriteLine(op2(1, 2));

            // пример использования лямбда-выражений (передача лямбда в качестве параметра)
            Console.WriteLine("Sum of array = " + pr.ArrayCalc(0, (x, y) => x + y, 1, 2, 3, 4, 5));

            Console.WriteLine("Production of array = " + pr.ArrayCalc(1, (x, y) => x * y, 1, 2.3, 3.1, 4, 5, 6, 7));
        }

        // тип данных, задающий тип параметров
        public delegate double BinOp(double a, double b);

        // метод, принимающий код в качестве параметра и применяющий его к массиву
        public double ArrayCalc(double initVal, BinOp op, params double[] arr)
        {
            double result = initVal;
            foreach(double elem in arr)
            {
                // вызов лямбда-выражения, полученного в качестве параметра
                result = op(result, elem);
            }
            return result;
        }
    }
}
