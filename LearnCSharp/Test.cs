extern alias Helper;//引入外部程序集，并且为其创建一个别名

using Microsoft.VisualBasic;
using System.Diagnostics;
using static Helper.HelperLibForLearnCSharp.SharedData;

class Test
{
    private static object obj = new object();//锁对象
    /*【00000：测试方法】*/
    public static void TestFunc()
    {
        int maxNumber = 30;//最大输出数字
        int threadForLoopSleepTime = 1000;//线程循环输出数字的间隔时间
        int mainForLoopSleepTime = 500;//主线程循环执行业务的间隔时间
        int mainForLoopCount = 5;//主线程循环执行业务的次数
        int manualReserSleepTime = mainForLoopSleepTime * 10;//手动重置事件对象的间隔时间

        using AutoResetEvent are = new AutoResetEvent(false);

        void PrintNumber()
        {
            var name = Thread.CurrentThread.Name;
            Console.WriteLine($"{name}等待执行输出");
            are.WaitOne();//等待信号量
            Console.WriteLine($"{name}收到了信号，开始执行输出");
            for (int i = 1; i <= maxNumber; i++)
            {
                are.WaitOne();//等待信号量
                Thread.Sleep(threadForLoopSleepTime);//暂停线程，模拟耗时操作
                Console.WriteLine($"{name}输出数字{i:00}");
            }
        }

        Thread thread1 = new Thread(PrintNumber);
        Thread thread2 = new Thread(PrintNumber);
        Thread thread3 = new Thread(PrintNumber);

        thread1.Name = $"线程{thread1.ManagedThreadId}";
        thread2.Name = $"线程{thread2.ManagedThreadId}";
        thread3.Name = $"线程{thread3.ManagedThreadId}";


        thread1.Start();
        thread2.Start();
        thread3.Start();

        Thread.Sleep(500);

        while (true)
        {
            for (int i = 1; i <= mainForLoopCount; i++)
            {
                Thread.Sleep(mainForLoopSleepTime);
                Console.WriteLine($"主线程正在执行一些事务[{i}]");
            }

            Console.WriteLine();
            Console.WriteLine("主线程完成了工作，现在发出信号通知子线程可以开始执行输出任务");

            are.Set();//发出信号通知子线程可以开始执行输出任务

            if (!thread1.IsAlive && !thread2.IsAlive && !thread3.IsAlive)
                break;//如果所有子线程都已经执行完成，则退出循环

            Thread.Sleep(manualReserSleepTime);//暂停主线程
            //are.Reset();//重置事件对象，阻止子线程继续执行

            Thread.Sleep(threadForLoopSleepTime);
            Console.WriteLine();
            Console.WriteLine("主线程介入了工作，现在发出信号通知子线程等待执行输出任务");
        }

        Console.WriteLine("所有线程均已完成工作！");
        Console.WriteLine();
    }
}