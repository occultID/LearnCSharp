extern alias Helper;//引入外部程序集，并且为其创建一个别名

using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using LearnCSharp.Professional.LeanrAsyncProgrammingSpace;
using LearnCSharp.Professional.LearnProcessAndThreadSpace;

class Print
{
}
class Test
{
    // 同步锁，防止多线程输出时文字交错
    private static readonly object objLock = new object();

    internal static void TestFunc()
    {
        Console.WriteLine("\n------示例：互斥体------\n");
        int processCount = 4;//子进程数量

        ProcessStartInfo GetPsi(string arg) => new ProcessStartInfo
        {
            FileName = "TestProcessAndThread.exe",
            Arguments = arg,
            UseShellExecute = true,
            RedirectStandardOutput = false,
            CreateNoWindow = true
        };

        Console.WriteLine($"》》》同时执行 {processCount} 个子进程创建单例对象（无互斥体），输出单例对象哈希值《《《");
        Console.WriteLine("-----------------------------------------------");

        Process[] processes = new Process[processCount];

        for (int i = 0; i < processCount; i++)
        {
            processes[i] = Process.Start(GetPsi($"2 子进程{i + 1}")!)!;
        }

        for (int i = 0; i < processCount; i++)
        {
            processes[i].WaitForExit();
            processes[i].Exited += (sender, e) => processes[i].Dispose();
        }

        Console.WriteLine("运行结束");
        Console.ReadKey();

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine();

        Thread.Sleep(2000);//等待子进程结束，确保共享资源值被重置

        Console.WriteLine($"》》》同时执行 {processCount} 个子进程创建单例对象（互斥体），输出单例对象哈希值《《《");
        Console.WriteLine("-----------------------------------------------");

        for (int i = 0; i < processCount; i++)
        {
            processes[i] = Process.Start(GetPsi($"2 子进程{i + 1} Mutex")!)!;
        }

        for (int i = 0; i < processCount; i++)
        {
            processes[i].WaitForExit();
            processes[i].Exited += (sender, e) => processes[i].Dispose();
        }

        Console.WriteLine("运行结束");
        Console.ReadKey();

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine();

        Helper.HelperLibForLearnCSharp.SharedData.Dispose();
    }

    internal static void TestActionInvoke()
    {
        int count = 0;
        Stopwatch sw = new Stopwatch();

        sw.Start();
        for (int i = 0; i < 500; i++) 
        {
            count += i;
            Thread.Sleep(10);
        }
        sw.Stop();
        Console.WriteLine($"count:{count} | time:{sw.Elapsed.TotalMilliseconds}");
        Console.WriteLine();
        count = 0;

        sw.Restart();
        Parallel.For(0, 500, i =>
        {
            lock (objLock)
            {
                count += i;
            }
            Thread.Sleep(10);
        });
        sw.Stop();
        Console.WriteLine($"count:{count} | time:{sw.Elapsed.TotalMilliseconds}");
        Console.WriteLine();
        count = 0;

        Thread[] threads = new Thread[10];
        for (int i = 0; i < 10; i++)
        {
            int temp = i;
            threads[i] =new Thread(() =>
            {
                for (int j = temp * 50; j < temp * 50 + 50; j++)
                {
                    lock(objLock)
                    {
                        count += j;
                    }
                    Thread.Sleep(10);
                }
            }
            );
        }

        sw.Restart();
        Array.ForEach(threads, thread => thread.Start());
        Array.ForEach(threads, thread => thread.Join());
        sw.Stop();
        Console.WriteLine($"count:{count} | time:{sw.Elapsed.TotalMilliseconds}");
        Console.WriteLine();
        count = 0;

        Task[] tasks = new Task[10];
        for (int i = 0; i < 10; i++)
        {
            int temp = i;
            tasks[i] = new Task(() =>
            {
                for (int j = temp * 50; j < temp * 50 + 50; j++)
                {
                    lock (objLock)
                    {
                        count += j;
                    }
                    Thread.Sleep(10);
                }
            }
            );
        }

        sw.Restart();
        Array.ForEach(tasks, task => task.Start());
        Task.WaitAll(tasks);
        sw.Stop();
        Console.WriteLine($"count:{count} | time:{sw.Elapsed.TotalMilliseconds}");
        Console.WriteLine();
        count = 0;

        sw.Restart();
        Task.Run(() =>
        {
            for (int i = 0; i < 500; i++)
            {
                count += i;
                Thread.Sleep(10);
            }
        }).Wait();
        sw.Stop();
        Console.WriteLine($"count:{count} | time:{sw.Elapsed.TotalMilliseconds}");
        Console.WriteLine();
        count = 0;

        using ManualResetEvent mre = new ManualResetEvent( false );
        sw.Restart();
        ThreadPool.QueueUserWorkItem(_ =>
        {
            for (int i = 0; i < 500; i++)
            {
                count += i;
                Thread.Sleep(10);
            }
            mre.Set();
        },null);
        mre.WaitOne();
        sw.Stop();
        Console.WriteLine($"count:{count} | time:{sw.Elapsed.TotalMilliseconds}");
        Console.WriteLine();
        count = 0;

        using CountdownEvent cde = new CountdownEvent(10);
        ThreadPool.SetMinThreads(10, 10);
        ThreadPool.SetMaxThreads(10, 10);
        sw.Restart();
        for (int i = 0; i < 10; i++)
        {
            int temp = i;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                for (int j = temp*50; j < temp*50+50; j++)
                {
                    lock (objLock)
                    {
                        count += j;
                    }
                    Thread.Sleep(10);
                }
                cde.Signal();
            }, null);
        }
        cde.Wait();
        sw.Stop();
        Console.WriteLine($"count:{count} | time:{sw.Elapsed.TotalMilliseconds}");
        Console.WriteLine();
        count = 0;
    }
    static void TestTaskThread()
    {
        async Task Calculate(int id)
        {
            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】开始执行 [任务 {id:00}]计算");
            int result = 0;
            for (int i = 1; i <= 100; i++)
            {
                await Task.Delay(100);

                lock (objLock)
                {
                    result += i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】正在执行 [任务 {id:00}] 第{i:00}次计算，当前result值：{result}");
                    Console.ResetColor();
                }
            }
            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】结束执行 [任务 {id:00}]计算");
        }

        Calculate(1).Wait();
        Console.WriteLine();
        Console.ReadKey();

        Task.Run(() => Calculate(2)).Wait();
        Console.WriteLine();
        Console.ReadKey();

        async Task Run()
        {
            Console.WriteLine($"---{Thread.CurrentThread.ManagedThreadId}");
            await Calculate(3);
            Console.WriteLine($"---{Thread.CurrentThread.ManagedThreadId}");
        }

        Run().Wait();
    }

    static void Testdd()
    {
        void Test(int id)
        {
            Console.WriteLine($"---开始{Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 10; i++)
            {
                lock (objLock)
                {
                    Console.WriteLine($"---输出{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Task {id}: {i}");
                }
                Task.Delay(200).Wait();
            }
            Console.WriteLine($"---结束{Thread.CurrentThread.ManagedThreadId}");
        }

        async Task TestTask(int id)
        {
            Console.WriteLine($"---开始{Thread.CurrentThread.ManagedThreadId}");
            for (int i = 0; i < 10; i++)
            {
                await Task.Delay(200);
                lock (objLock)
                {
                    Console.WriteLine($"---输出{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"Task {id}: {i}");
                }
            }
            Console.WriteLine($"---结束{Thread.CurrentThread.ManagedThreadId}");
        }

        //1.同步方法执行，主线程执行
        Test(0);

        Console.WriteLine();
        Console.ReadKey(true);

        //2.异步任务执行，方法内第一个await之前由主线程执行，之后可能由其他线程执行
        TestTask(1).Wait();

        Console.WriteLine();
        Console.ReadKey(true);

        //3.使用异步任务执行同步方法，方法所有代码由主线程或子线程单独执行
        Task task = new Task(() => Test(2));
        task.Start();
        task.Wait();

        Console.WriteLine();
        Console.ReadKey(true);

        //4.同2
        Task task1 = TestTask(3);
        task1.Wait();

        Console.WriteLine();
        Console.ReadKey(true);

        //Task.Run直接执行同步方法，方法所有代码由主线程或子线程单独执行
        Task.Run(() => Test(4)).Wait();

        Console.WriteLine();
        Console.ReadKey(true);

        //Task.Run直接执行异步方法，方法所有代码由主线程或子线程单独执行
        Task.Run(async () => await TestTask(5)).Wait();

        Console.WriteLine();
    }
}
