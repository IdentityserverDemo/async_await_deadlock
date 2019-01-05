using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Test.M_Await().Wait();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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