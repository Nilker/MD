using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TT
{
    class Program
    {
        private static int a = 1;
        static Program()
        {
            a = 2;
        }

        private int b = 3;

        public Program()
        {
            a = 4;
        }

        static void Main(string[] args)
        {
            var tt= new Program().GenerateFibonacci(5);

            tt.ToList().ForEach(s =>
            {
                Console.WriteLine(s);
            });

            Console.ReadKey();
        }


        public static IEnumerable<int> enFunc()
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield break;
            yield return 4;
        }


         IEnumerable<int> GenerateFibonacci(int n)
         {
             int current = 1;
             int next = 1;

            for (int i = 0; i < n; ++i)
            {
                yield return current;
                
                next = current + (current = next);
            }
        }


        private static async Task<string> GetUrlContent(string uri)
        {
            using (var client=new HttpClient())
            {
                var content =  await client.GetStringAsync(uri);
                return content;
            }
        }


        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }


        //static async Task Main(string[] args)
        //{
        //    Task<int> num = Task.Run(() =>
        //    {
        //        var tem = Enumerable.Range(2, 3000000).Count(s =>
        //            Enumerable.Range(2, (int)Math.Sqrt(s) - 1).All(i => s % i > 0));
        //        return tem;
        //    });

        //    Console.WriteLine($"------------>{num.Result}");
        //}

        //public static async Task Main(string[] args)
        //{
        //    Console.WriteLine("Syc proccess - start");

        //    Console.WriteLine("Syc proccess - enter Func1");
        //    await func1();
        //    Console.WriteLine("Syc proccess - out Func1");

        //    Console.WriteLine("Syc proccess - enter Func2");

        //    Console.WriteLine("Syc proccess - out Func2");

        //    Console.WriteLine("Syc proccess - enter Func3");
        //    func3();
        //    Console.WriteLine("Syc proccess - out Func3");

        //    Console.WriteLine("Syc proccess - done");

        //    Console.ReadKey();
        //}

        private static async Task func1()
        {
            Console.WriteLine("Func1 proccess - start");
            await Task.Run(() => Thread.Sleep(1000));
            Console.WriteLine("Func1 proccess - end");
        }

        private static async Task func2()
        {
            Console.WriteLine("Func2 proccess - start");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine("Func2 proccess - end");
        }

        private static async Task func3()
        {
            Console.WriteLine("Func3 proccess - start");
            await Task.Run(() => Thread.Sleep(5000));
            Console.WriteLine("Func3 proccess - end");
        }
    }
}
