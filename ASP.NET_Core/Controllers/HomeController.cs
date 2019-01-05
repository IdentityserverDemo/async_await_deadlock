using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Test.M_Await().Wait();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class Test
    {
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
