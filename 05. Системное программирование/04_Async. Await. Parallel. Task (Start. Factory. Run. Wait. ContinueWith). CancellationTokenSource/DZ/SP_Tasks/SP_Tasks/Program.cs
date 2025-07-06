using System;
using System.Threading;
using System.Threading.Tasks;

namespace SP_Tasks
{
    class Program
    {
        static void testTask()
        {
            Console.WriteLine("test start...");
            Thread.Sleep(2000);
            Console.WriteLine("test end.");
        }

        static int Sum(int a, int b)
        {
            Thread.Sleep(2000);
            return a+b;
        }

        static void ContinueTask(Task t)
        {
            Console.WriteLine("\nContinueTask started...");
            Console.WriteLine("Current task Id : {0}", Task.CurrentId);
            Console.WriteLine("Prev task Id: {0}\n", t.Id);
            Thread.Sleep(3000);
            Console.WriteLine("\nContinueTask ended...");
        }

        static void Main(string[] args)
        {
            // создание задачи и запуск методом Start
            /*Task task1 = new Task(() => Console.WriteLine("Task 1 started."));
            task1.Start();

            // создание и запуск задачи
            Task task2 = Task.Factory.StartNew(() => Console.WriteLine("Task 2 started."));

            // создание и запуск задачи
            Task task3 = Task.Run(() =>
            {
                Console.WriteLine("Task 3 started...");
                Console.WriteLine("Task 3 ended.");
            });

            // создание задачи на основе метода
            Task task = new Task(testTask);
            task.Start();

            //new Task(testTask).Start();

            // ожидание окончания выполнения задачи task
            task.Wait();*/

            // запуск вложенной задачи из внешней задачи
            /*Console.WriteLine("\nInner-Outer tasks test........................");
            var outerTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Outer task start...");
                var innerTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Inner task start...");
                    Thread.Sleep(10000);
                    Console.WriteLine("Inner task end.");
                //});
                }, TaskCreationOptions.AttachedToParent);
                Console.WriteLine("Outer task end.");
            });

            // в методе Main ожидать окончания выполнения внешней задачи
            outerTask.Wait();*/

            /*
            //Console.WriteLine("\nWaitAll, WaitAny test........................");
            Task[] tasks1 = new Task[3]
            {
                new Task(() =>
                {
                    Console.WriteLine("First task start...");
                    Thread.Sleep(2000);
                    Console.WriteLine("First task end.");
                }),
                new Task(() => Console.WriteLine("Second task.")),
                new Task(() =>
                {
                    Console.WriteLine("Third task start...");
                    Thread.Sleep(3000);
                    Console.WriteLine("Third task end.");
                })
            };

            // задача ContinueTask будет запущена после задачи Third task
            tasks1[2].ContinueWith(ContinueTask);

           // задача будет запущена после задачи First task
           tasks1[0].ContinueWith((Task t) =>
           {
               Console.WriteLine("\nAnother continue task: {0}", Task.CurrentId);
           });

            // старт всех задач из массива задач
            foreach (var t in tasks1)
                t.Start();

            // ожидание окончания всех задач
            //Task.WaitAll(tasks1);

            // ожидание окончания любой задачи из массива
            //Task.WaitAny(tasks1);
            */

            // получение результата из задачи
            /*Task<int> sumTask = new Task<int>(() => Sum(5, 7));
            sumTask.Start();

            // подождать окончания задачи
            //sumTask.Wait();

            // получить результат задачи
            Console.WriteLine($"a+b equals {sumTask.Result}");*/

            // прерывание задачи при помощи токена отмены

            // создание генератора токена
            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

            // генерация токена
            CancellationToken token = cancelTokenSource.Token;

            // запуск задачи и передача токена
            Task task1 = new Task(() => Sum(10, token));
            task1.Start();

            for(int i=0; i<5; i++)
            {
                Console.WriteLine("Main");
                Thread.Sleep(1000);
            }

            // отмена задачи
            cancelTokenSource.Cancel();

            Console.WriteLine("Main end.");

            Console.ReadLine();
        }

        static void Sum(int x, CancellationToken token)
        {
            int result = 1;
            for (int i = 1; i <= x; i++)
            {
                // проверка на отмену задачи
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("Task cancelled by token!");
                    return;
                }

                result += i;
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Result = {0}", result);
        }
    }
}