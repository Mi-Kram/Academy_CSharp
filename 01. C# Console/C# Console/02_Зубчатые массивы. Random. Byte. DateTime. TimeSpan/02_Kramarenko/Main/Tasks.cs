using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Tasks
    {
        static private void setw(uint w, int num)
        {
            int tmp = 1;
            for (int i = 0; i < w; i++)
            {
                tmp *= 10;
            }

            string str = num.ToString();
            int length = (int)w - str.Length;
            for (int i = 0; i < length; i++)
            {
                Console.Write(' ');
            }
            
            Console.Write(num);
        }
        static private void Print(int m, int n, int [,]matrix, uint w)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    setw(w, matrix[i, j]);
                }
                Console.WriteLine();
            }
        }
        static private void swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }

        static public void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. Пользователь вводит размеры двумерного массива и сам массив. Программа меняет местами       ");
            Console.WriteLine("       первую строку массива с последней, а потом меняет местами первый столбец массива с последним\n\n");
            Console.ResetColor();

            int m, n;

            try
            {
                Console.Write("Введите кол-во строк матрицы> ");
                if (!Int32.TryParse(Console.ReadLine(), out m)) throw new Exception("Введены некорректные данные");
                if(m < 1) throw new Exception("Введены некорректные данные");

                Console.Write("Введите кол-во столбцов матрицы> ");
                if (!Int32.TryParse(Console.ReadLine(), out n)) throw new Exception("Введены некорректные данные");
                if(n < 1) throw new Exception("Введены некорректные данные");

                int[,] matrix = new int[m, n];

                Console.WriteLine();

                int cnt = 0;
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        matrix[i, j] = ++cnt;

                        //Console.Write($"Matrix[{i},{j}] = ");
                        //if (!Int32.TryParse(Console.ReadLine(), out matrix[i, j])) throw new Exception("Введены некорректные данные");
                    }
                }
                //Console.WriteLine("\n\n");

                Print(m, n, matrix, 6);

                for (int i = 0; i < n; i++) {
                    swap(ref matrix[0, i], ref matrix[m - 1, i]);
                }
                for (int i = 0; i < m; i++) {
                    swap(ref matrix[i, 0], ref matrix[i, n - 1]);
                }

                Console.WriteLine("\n\n");
                Print(m, n, matrix, 6);

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        static public void Task2()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task2. Пользователь вводит строку, программа подсчитывает сумму всех цифер в строке\n\n");
            Console.ResetColor();
            
            string str;
            try
            {
                Console.Write("Введите строку> ");
                str = Console.ReadLine();
                if (str.Length == 0) throw new Exception("Пустая строка");
                int sum = 0;

                foreach(var item in str)
                {
                    if(Char.IsDigit(item)) sum += item - '0';
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\nsum = {sum}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        static public void Task3()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task3. Пользователь вводит строку, программа подсчитывает сумму всех чисел в строке\n\n");
            Console.ResetColor();
            
            string str;
            try
            {
                Console.Write("Введите строку> ");
                str = Console.ReadLine();
                if (str.Length == 0) throw new Exception("Пустая строка");

                string[] numbers = str.Split(' ');
                double sum = 0;
                double num = 0;
                for (int i = 0; i < numbers.Length; i++)
                {
                    if (!double.TryParse(numbers[i], out num)) throw new Exception("Введены некорректные данные");
                    sum += num;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\nsum = {sum}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task3 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        static public void Task4()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task4. Пользователь вводит строку вида: 123 [op] 345,                       ");
            Console.WriteLine("       где [op] - это +, -, *, /, %. Программа вычисляет результат выражения\n\n");
            Console.ResetColor();
            
            string str;
            double num1, num2, res;
            char op;
            try
            {
                Console.Write("Введите строку> ");
                str = Console.ReadLine();

                string []words = str.Split(' ');
                if (words.Length != 3) throw new Exception("Строка введена неверно");

                if(!double.TryParse(words[0], out num1)) throw new Exception("Строка не соответствует требованиям образца");
                if(!double.TryParse(words[2], out num2)) throw new Exception("Строка не соответствует требованиям образца");

                if (words[1].Length != 1) throw new Exception("Строка введена неверно");
                switch(words[1][0])
                {
                    case '+':
                        res = num1 + num2;
                        break;
                    case '-':
                        res = num1 - num2;
                        break;
                    case '*':
                        res = num1 * num2;
                        break;
                    case '/':
                        res = num1 / num2;
                        break;
                    case '%':
                        res = num1 % num2;
                        break;
                    default: throw new Exception("Строка не соответствует требованиям образца");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n\nresult = {res:0.##}");
                Console.ResetColor();

            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task4 не выполнен. {ex.Message}!");
                Console.ResetColor();
            }
            Console.WriteLine("\n\n\n");
        }
    }
}
