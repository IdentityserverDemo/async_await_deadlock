using BenchmarkDotNet.Attributes;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<Test>();
            Test.M_Await().Wait();
            Console.ReadLine();
        }
        public class Test
        {
            [Benchmark]
            public static async Task M_Await()
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-a");
                await WaitAsync($"M-await");
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-c");
                //await WaitAsync($"again");
                watch.Stop();
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>时长：{watch.ElapsedMilliseconds}");
            }
            private static async Task WaitAsync(string str)
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>{str}");
                await Task.Delay(TimeSpan.FromSeconds(1));
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>吧啦吧啦");
            }

            #region M_ConfigureAwait

            public async Task M_ConfigureAwait()
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-a");
                Task task = ConfigWaitAsync($"{Thread.CurrentThread.ManagedThreadId}=>M-await");
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-c");
                task.Wait();
                watch.Stop();
                Console.WriteLine($"时长：{watch.ElapsedMilliseconds}");
            }
            private static async Task ConfigWaitAsync(string str)
            {
                Console.WriteLine($"{str}");
                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>吧啦吧啦");
            }

            #endregion
        }
    }
}
