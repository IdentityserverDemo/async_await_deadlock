using BenchmarkDotNet.Attributes;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void M(object sender, EventArgs e)
        {
            await Test.M_Await();
            //Test.M_ConfigureAwait().Wait();
        }
    }
    public class Test
    {
        public static async Task M_ConfigureAwait()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-a");
            Task task = ConfigWaitAsync($"{Thread.CurrentThread.ManagedThreadId}=>M-await");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-c");
            task.Wait();
            watch.Stop();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>时长：{watch.ElapsedMilliseconds}");
        }
        [Benchmark]
        public static async Task M_Await()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-a");
            await WaitAsync($"{Thread.CurrentThread.ManagedThreadId}=>M-await");
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>M-c");
            watch.Stop();
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>时长：{watch.ElapsedMilliseconds}");
        }
        private static async Task WaitAsync(string str)
        {
            Console.WriteLine($"{str}");
            await Task.Delay(TimeSpan.FromSeconds(1));
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>吧啦吧啦");
        }
        private static async Task ConfigWaitAsync(string str)
        {
            Console.WriteLine($"{str}");
            await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId}=>吧啦吧啦");
        }
    }
}
//task.Wait();//task.GetAwaiter().GetResult();//会阻塞当前线程