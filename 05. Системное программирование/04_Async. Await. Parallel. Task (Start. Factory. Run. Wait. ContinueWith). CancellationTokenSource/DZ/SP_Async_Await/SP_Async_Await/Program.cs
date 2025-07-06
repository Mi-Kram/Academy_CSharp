using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SP_Async_Await
{
    class Program
    {
        static int GetPrimeCount(int x)
        {
            int result = 0;
            for (int i = 2; i <= x; i++)
            {
                result++;
                for(int k = 2; k<i; k++)
                {
                    if (i % k == 0)
                    {
                        result--;
                        break;
                    }
                }
            }
            return result;
        }
        
        static async void PrimeNumbersAsync()
        {
            Console.WriteLine("Start GetPrimeCount");

            // параллельный вызов задачи 
            int res = await Task.Run(() => GetPrimeCount(100000));

            Console.WriteLine("Prime numbers = {0}", res);
            Console.WriteLine("End GetPrimeCount");
        }

        static void Main(string[] args)
        {
            // параллельный вызов метода
            PrimeNumbersAsync();

            for(int i=1; i<=5; i++)
            {
                Console.WriteLine("Main: {0}", i);
                Thread.Sleep(1000);
            }
        }
    }
}
