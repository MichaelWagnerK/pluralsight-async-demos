using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SandboxConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            //string title = GetTitleTplAsync("a").Result;

            //Console.WriteLine(title);

            //DoWorkAsync("a");

            //Thread.Sleep(500);

            //DoWorkAsync("b");


            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            //CancellationToken cancellationToken = cancellationTokenSource.Token;

            //var str = TestCancelAsync(cancellationToken);

            //do
            //{
            //    var c = Console.ReadLine();
            //    Console.WriteLine(c);
            //    if (c != "c") continue;
            //    cancellationTokenSource.Cancel();
            //} while (true);

            CancellationDemo1();

            Console.ReadLine();
        }

        private static void CancellationDemo1()
        {
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource())
            {
                var waitFor = new List<Task>();

                CancellationToken cancellationToken = cancellationTokenSource.Token;
                Action<Task<int>> continuation = t =>
                {
                    if (t.IsCanceled)
                    {
                        Console.WriteLine("Task was cancelled");
                    }
                    else
                    {
                        Console.WriteLine("Status " + t.Result + ": " + t.Status);
                    }
                };

                for (int i = 0; i < 100; i++)
                {
                    int current = i;
                    var c = Task.Factory
                        .StartNew(() =>
                            {
                                Console.WriteLine("Running " + current);
                                Thread.Sleep(100);
                                Console.WriteLine("Finishing " + current);
                                return current;
                            }, cancellationToken)
                            .ContinueWith(continuation);
                    Console.WriteLine("Launched " + current);
                    waitFor.Add(c);

                }
                Console.WriteLine("Waiting....");
                Thread.Sleep(500);

                Console.WriteLine("Cancelling....");
                cancellationTokenSource.Cancel();
                Console.WriteLine("Cancel Returned");
                Task.WaitAll(waitFor.ToArray());
            }
        }

        private static async Task TestCancelAsync(CancellationToken cancellationToken)
        {
            do
            {
                Func<string> funcWaiting = WaitingToCancel;
                var task = await Task.Run(funcWaiting, cancellationToken);

                Console.WriteLine(task);

            } while (!cancellationToken.IsCancellationRequested);

            Console.WriteLine("Cancellation requested: " + cancellationToken.IsCancellationRequested);
        }

        private static string WaitingToCancel()
        {
            Thread.Sleep(2000);
            return DateTime.Now.ToLongTimeString();
        }


        private static async Task<string> GetTitleCsAsync(string url)
        {
            var task = await Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "url: " + DateTime.Now.ToLongTimeString();
            });

            return task;
        }

        private static Task<string> GetTitleTplAsync(string url)
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(2000);
                return "url: " + DateTime.Now.ToLongTimeString();
            });

            return task;
        }


        private static void DoWork()
        {
            Task<string> task = GetString();
            task.ContinueWith(t =>
            {
                string result = t.Result;
                Console.WriteLine(result);
            });

            Console.WriteLine("Work Done");

        }

        private static async void DoWorkAsync(string id)
        {
            Console.WriteLine("Starting Work: " + id);
            Task<string> task = GetString();
            string result = await task;
            Console.WriteLine(result);
            Console.WriteLine("Work Done: " + id);
        }

        private static async Task<string> GetString()
        {
            var task = await Task.Run(() =>
            {
                Thread.Sleep(3000);

                return "String From Task";
            });

            return task;
        }
    }
}
