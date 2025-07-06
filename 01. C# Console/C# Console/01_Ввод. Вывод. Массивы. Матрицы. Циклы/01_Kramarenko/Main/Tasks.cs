using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Tasks
    {
        static int Min(int[,] matrix, int m, int n) {
            int min = matrix[0, 0];

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (min > matrix[i, j]) min = matrix[i, j];
                }
            }

            return min;
        }
        static int Max(int[,] matrix, int m, int n) {
            int max = matrix[0, 0];

            for (int i = 0; i < m; i++) {
                for (int j = 0; j < n; j++) {
                    if (max < matrix[i, j]) max = matrix[i, j];
                }
            }

            return max;
        }
        static void swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }


        static public void Task1()
        {
            int m, n;
            try
            {
                Console.WriteLine("Task1. Среднее арифметическое минимального и максимального элементов матрицы\n");
                Console.Write("Кол-во строк матрицы> ");
                if (!Int32.TryParse(Console.ReadLine(), out m))
                    throw new ArgumentException("Введено некорректное число");

                Console.Write("Кол-во столбцов матрицы> ");
                if (!Int32.TryParse(Console.ReadLine(), out n))
                    throw new ArgumentException("Введено некорректное число");

                int[,] matrix = new int[m, n];

                Console.WriteLine("");
                for (int i = 0; i < m; i++) {
                    for (int j = 0; j < n; j++) {
                        Console.Write($"matrix[{i}, {j}] = ");

                        if (!Int32.TryParse(Console.ReadLine(), out matrix[i,j]))
                            throw new ArgumentException("Введено некорректное число");
                    }
                }

                int min = Min(matrix, m, n),
                    max = Max(matrix, m, n);
                double avg = (min + max) / 2.0;
                Console.WriteLine(@"{0}{0}{0}минимальное число> {1}{0}максимальное число> {2}{0}avg> {3:.##}", '\n', min, max, avg);
            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine($"Task1 не выполнен. {ex.Message}!");
            }

            Console.WriteLine("\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();

        } // Task1
        static public void Task2()
        {
            bool flag = false;
            int n;
            try {
                Console.WriteLine("Task2. Состоит ли массив только из простых чисел или нет\n");
                Console.Write("длина массива> ");
                if (!Int32.TryParse(Console.ReadLine(), out n))
                    throw new ArgumentException("Введено некорректное число");

                int[] arr = new int[n];
                Console.WriteLine();
                for (int i = 0; i < n; i++) {
                    Console.Write($"array[{i}] = ");
                    if (!Int32.TryParse(Console.ReadLine(), out arr[i]))
                        throw new ArgumentException("Введено некорректное число");
                }

                for (int i = 0; i < n; i++)
                {
                    if(arr[i] <= 1)
                    {
                        flag = true;
                        break;
                    }

                    for (int j = 4; j < arr[i]; j++)
                    {
                        if (arr[i] % j == 0)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag) break;
                }


                if (flag) Console.WriteLine("\n\nВ массиве не все числа простые");
                else Console.WriteLine("\n\nВ массиве все числа простые");

            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine($"Task2 не выполнен. {ex.Message}!");
            }

            Console.WriteLine("\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();

        } // Task2
        static public void Task3()
        {
            int n;
            try
            {
                Console.WriteLine("Task3. рограмма находит в массиве 2 максимальных числа за один проход массива\n");
                Console.Write("длина массива> ");
                if (!Int32.TryParse(Console.ReadLine(), out n))
                    throw new ArgumentException("Введено некорректное число");

                if(n < 2) throw new ArgumentException("Длина массива должна быть больше 1 числа");

                int[] arr = new int[n];
                Console.WriteLine();
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"array[{i}] = ");
                    if (!Int32.TryParse(Console.ReadLine(), out arr[i]))
                        throw new ArgumentException("Введено некорректное число");
                }

                int max1 = arr[0];
                int max2 = arr[1];
                if (max1 < max2) swap(ref max1, ref max2);

                for (int i = 2; i < n; i++) {
                    if(max2 < arr[i])
                    {
                        if(max1 < arr[i])
                        {
                            swap(ref max1, ref max2);
                            max1 = arr[i];
                        }
                        else
                        {
                            max2 = arr[i];
                        }
                    }
                }

                Console.WriteLine($"\n\n2 максимальных числа в массиве> {max2} и {max1}");

            }
            catch (ArgumentException ex)
            {
                Console.Clear();
                Console.WriteLine($"Task3 не выполнен. {ex.Message}!");
            }

            Console.WriteLine("\n\n");

        } // Task3


    } // Tasks
}
