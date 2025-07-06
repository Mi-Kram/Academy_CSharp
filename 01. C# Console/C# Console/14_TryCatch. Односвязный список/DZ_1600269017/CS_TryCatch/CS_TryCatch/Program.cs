using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_TryCatch
{
    class Program
    {

        static void func()
        {
            try
            {
                int n = 0;
                Console.WriteLine("Try...");
                Console.WriteLine(23 / n);
                Console.WriteLine("Last line!");
            }
            finally
            {
                Console.WriteLine("Inner finally");
            }
        }

        static void Main(string[] args)
        {
            // стандартные поля класса Exception
            /*try
            {
                int x = 0;
                int y = 10 / x;
                Console.WriteLine($"Result: {y}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Source: {ex.Source}");
                Console.WriteLine($"Target Site (Method): {ex.TargetSite}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }*/

            // неявный вызов блока finally
            /*try
            {
                Console.WriteLine("Try...");
                goto l2;
            }
            catch
            {
                Console.WriteLine("Catch...");
            }
            finally
            {
                Console.WriteLine("Finally...");
            }

            l2:
            Console.WriteLine("End.");*/

            // вложенные вызовы try..catch..finally
            /*try
            {
                try
                {
                    try
                    {
                        func();
                    }
                    catch(InvalidCastException ex)
                    {
                        Console.WriteLine("InvalidCastException catch...");
                    }
                    finally
                    {
                        Console.WriteLine("InvalidCastException finally");
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("IndexOutOfRangeException catch...");
                }
                finally
                {
                    Console.WriteLine("IndexOutOfRangeException finally...");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception catch: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Exception finally...");
            }*/

            // дополнительные фильтры при обработке ошибок
            /*int[] arr = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1};
            int index = 0;

            try
            {
                Console.WriteLine("Enter index of array: ");
                Int32.TryParse(Console.ReadLine(), out index);

                Console.WriteLine($"arr[{index}] = {arr[index]}");
            }
            catch (IndexOutOfRangeException ex) when (index < 0)
            {
                Console.WriteLine("Index cannot be less than zero!");
            }
            catch (IndexOutOfRangeException ex) when (index >= arr.Length)
            {
                Console.WriteLine("Index is too big!");
            }*/

            try
            {
                double result = Calculate(0, 5);
                Console.WriteLine($"Result = {result}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static double Calculate(int a, int b)
        {
            if (a == 0 || b == 0)
            {
                // создание стандартного исключения
                /*Exception ex = new Exception("Incorrect number operation.");

                // словарь дополнительной информации об исключении
                ex.Data["a"] = a;
                ex.Data["b"] = b;

                // выброс исключения
                throw ex;*/

                throw new MyNumericException(a, b, $"Incorrect number operation: a = {a}, b = {b}");
            }

            return a / (double)b;
        }
    }

    // пользовательский класс исключений
    public class MyNumericException: Exception
    {
        public int A { get; set; }
        public int B { get; set; }

        public MyNumericException(int a, int b, string message): base(message)
        {
            this.A = a;
            this.B = b;
        }
    }
}
