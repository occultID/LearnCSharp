extern alias Helper;//引入外部程序集，并且为其创建一个别名

using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using static Helper.HelperLibForLearnCSharp.SharedData;
using LearnCSharp.Professional.LeanrAsyncProgrammingSpace;

class Print
{
    public static int classNumber;
    public int instanceNumber;
    public void PrintInstanceInfo()
    {
        Console.WriteLine($"---{Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine("instance");
        instanceNumber = 10;
    }
    public static void PrintClassInfo()
    {
        Console.WriteLine($"---{Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine("class");
        classNumber = 20;
    }
}
class Test
{
    // 同步锁，防止多线程输出时文字交错
    private static readonly object objLock = new object();

    //static int count1 = 0;
    internal static void TestFunc()
    {
        try
        {
            Task.Run(async() =>
            {
                Console.WriteLine("捕获异常测试");
                await Task.Delay(1000);
                throw new Exception();
            }).Wait();
        }
        catch
        {
            Console.WriteLine("catch");
        }
        finally
        {
            Console.WriteLine("finally");
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
}
