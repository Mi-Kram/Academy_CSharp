using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Tasks.Task1();
            Tasks.Task2();
            Tasks.Task3();
            
            End();
        }

        static void End()
        {
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(46, 12);
            Console.Write("                         ");

            Console.SetCursorPosition(46, 13);
            Console.Write("   Программа завершена   ");

            Console.SetCursorPosition(46, 14);
            Console.Write("                         ");

            Console.BackgroundColor = ConsoleColor.Black;
            Console.CursorVisible = false;
        }
    }

    class Tasks
    {
        public static void Task1()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task1. В класс Matrix добавить возможность просмотра в цикле foreach хранимой в классе матрицы. Матрица ");
            Console.WriteLine("       просматривается слева-направо и сверху-вниз. Использовать интерфейсы IEnumerable, IEnumerator    \n\n\n");
            Console.ResetColor();

            try
            {
                Matrix m = new Matrix(4, 6);

                // присваивание матрице значения
                int cnt = 1;
                for (int i = 0; i < m.Row; i++)
                {
                    for (int j = 0; j < m.Col; j++)
                    {
                        m[i, j] = cnt++;
                    }
                }
                // вывод матрицы
                foreach (var item in m)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task1 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        public static void Task2()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task2. В класс Matrix добавить возможность просмотра в цикле foreach хранимой в классе матрицы. Матрица ");
            Console.WriteLine("       просматривается слева-направо и сверху-вниз. Использовать интерфейсы IEnumerable, IEnumerator    \n\n\n");
            Console.ResetColor();

            try
            {
                // создание массива первых 10 простых чисел
                PrimeNumbers pr = new PrimeNumbers(10);
                Console.WriteLine("PrimeNumbers pr = new PrimeNumbers(10);");
                // вывод массива
                foreach (var item in pr)
                {
                    Console.WriteLine(item);
                }

                Console.WriteLine("\n\n");
                Console.ReadLine();

                // создание массива 10 простых чисел после числа 30
                pr = new PrimeNumbers(30, 10);
                Console.WriteLine("PrimeNumbers pr = new PrimeNumbers(30, 10);");
                // вывод массива
                foreach (var item in pr)
                {
                    Console.WriteLine(item);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task2 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
        public static void Task3()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Task3. Пользователь вводит имя файла, содержащего какой-то текст. Программа располагает каждое ");
            Console.WriteLine("       предложение на новой строке и сохраняет результат в другой файл                         \n\n\n");
            Console.ResetColor();

            try
            {
                string path = @"C:\Users\Михаил\source\repos\10_Kramarenko\Main\text.txt";
                string result = @"C:\Users\Михаил\source\repos\10_Kramarenko\Main\result.txt";
                string text = File.ReadAllText(path);

                // избавление от символов \n \r \t
                text = text.Replace('\n', ' ');
                text = text.Replace("\r", "");
                text = text.Replace("\t", "");

                // деление текста на предложения
                var sentence = text.Split(new char[] { '.', '!', '?', ';' }, StringSplitOptions.RemoveEmptyEntries);

                StreamWriter sw = new StreamWriter(result);
                foreach (var item in sentence)
                {
                    // узнать кол-во пробелов перед началом предложения
                    int space = 0;
                    while (space != item.Length)
                    {
                        if (item[space] == ' ') space++;
                        else break;
                    }

                    // запись предложение в файл без пробелов в начале
                    sw.WriteLine(item.Remove(0, space));
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"Task3 не выполнен. {ex.Message}\b!");
                Console.ResetColor();
            }

            Console.WriteLine("\n\n\nДля продолжения нажмите Enter...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    #region For Task1

    #region Matrix V1

    class Matrix : IEnumerable, IEnumerator
    {
        int cr = 0, cc = -1;

        int[,] matrix;

        /// <summary>
        /// кол-во строк
        /// </summary>
        public int Row
        {
            get => matrix.GetLength(0);
        }
        /// <summary>
        /// кол-во столбцов
        /// </summary>
        public int Col
        {
            get => matrix.GetLength(1);
        }

        /// <summary>
        /// конструктор с параметрами
        /// </summary>
        /// <param name="row">кол-во строк</param>
        /// <param name="col">кол-во стобцов</param>
        public Matrix(int row, int col)
        {
            matrix = new int[row, col];
        }

        /// <summary>
        /// обращение к матрице через [] по индексам
        /// </summary>
        public int this[int row, int col]
        {
            get => matrix[row, col];
            set { matrix[row, col] = value; }
        }

        // методы для интерфейсов IEnumerable и IEnumerator
        public object Current
        {
            get => matrix[cr, cc];
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (cr < matrix.GetLength(0))
            {
                if (cc < matrix.GetLength(1) - 1)
                {
                    cc++;
                }
                else
                {
                    cc = 0;
                    cr++;
                    if (cr >= matrix.GetLength(0))
                    {
                        cr = 0;
                        cc = -1;
                        return false;
                    }
                }
                return true;
            }
            else
            {
                cr = 0;
                cc = -1;
                return false;
            }
        }

        public void Reset()
        {
            cr = 0;
            cc = -1;
        }
    }

    #endregion

    #region Matrix V2

    //class Matrix : IEnumerable, IEnumerator
    //{
    //    int curpos = -1;

    //    int[,] matrix;

    //public int Row
    //{
    //    get => matrix.GetLength(0);
    //}
    //public int Col
    //{
    //    get => matrix.GetLength(1);
    //}

    //    public Matrix(int row, int col)
    //    {
    //        matrix = new int[row, col];

    //        int cnt = 1;
    //        for (int i = 0; i < row; i++)
    //        {
    //            for (int j = 0; j < col; j++)
    //            {
    //                matrix[i, j] = cnt++;
    //            }
    //        }
    //    }

    //    public int this[int row, int col]
    //    {
    //        get => matrix[row, col];
    //        set { matrix[row, col] = value; }
    //    }

    //    public object Current
    //    {
    //        get
    //        {
    //            return matrix[curpos / matrix.GetLength(1), curpos % matrix.GetLength(1)];
    //        }
    //    }

    //    public IEnumerator GetEnumerator()
    //    {
    //        return this;
    //    }

    //    public bool MoveNext()
    //    {
    //        if (curpos < matrix.GetLength(0) * matrix.GetLength(1) - 1)
    //        {
    //            curpos++;
    //            return true;
    //        }
    //        else
    //        {
    //            curpos = -1;
    //            return false;
    //        }
    //    }

    //    public void Reset()
    //    {
    //        curpos = -1;
    //    }
    //}

    #endregion

    #endregion

    #region For Task2

    /// <summary>
    /// массив, хранящий простые числа
    /// </summary>
    class PrimeNumbers : IEnumerable, IEnumerator
    {
        int curpos = -1;
        long[] arr;

        /// <summary>
        /// конструктор с параметрами (простые числа начинаются с первого простого числа)
        /// </summary>
        /// <param name="count">кол-во просты чисел</param>
        public PrimeNumbers(int count)
        {
            arr = new long[count];
            Fill(2, count);
        }
        /// <summary>
        /// конструктор с параметрами (простые числа начинаются со startValue)
        /// </summary>
        /// <param name="startValue">число, с которого начинается массив</param>
        /// <param name="count">кол-во просты чисел</param>
        public PrimeNumbers(int startValue, int count)
        {
            arr = new long[count];
            Fill(startValue, count);
        }

        /// <summary>
        /// заполнить массив простыми числами
        /// </summary>
        private void Fill(long startValue, long count)
        {
            for (long i = 2; i < startValue; i++)
            {
                if (startValue % i == 0)
                {
                    startValue++;
                    i = 1;
                }
            }

            List<long> primeLst = new List<long>();
            for (long i = 2; i < startValue; i++)
            {
                bool flag = true;
                for (long j = 2; j < i; j++)
                {
                    if (i % j == 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag) primeLst.Add(i);
            }

            long cnt = 0;
            for (long i = 0; i < count; i++)
            {
                bool flag = true;
                foreach (var item in primeLst)
                {
                    if (startValue % item == 0)
                    {
                        flag = false;
                        i--;
                        break;
                    }
                }
                if (flag)
                {
                    primeLst.Add(startValue);
                    arr[cnt++] = startValue;
                }
                startValue++;
            }
        }

        /// <summary>
        /// получить длину масива
        /// </summary>
        public int Length
        {
            get => arr.Length;
        }
        /// <summary>
        /// обращение к масиву через [] по индексу
        /// </summary>
        /// <returns></returns>
        public long this[int index]
        {
            get => arr[index];
        }

        // методы для интерфейсов IEnumerable и IEnumerator
        public object Current
        {
            get => arr[curpos];
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (curpos < arr.Length - 1)
            {
                curpos++;
                return true;
            }
            else
            {
                curpos = -1;
                return false;
            }
        }

        public void Reset()
        {
            curpos = -1;
        }
    }

    #endregion

}
