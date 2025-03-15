using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Professional
{
    internal class LearnProcessAndThread
    {
        private static object obj = new object();
        /*【21001：进程】*/
        public static void LearnProcess()
        {
            Process current = Process.GetCurrentProcess();

            Console.WriteLine("通过Process类获取当前程序进程信息：");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine(">>>进程信息<<<");

            var pInfo = $"进程名：  {current.ProcessName}\n" +
                $"PID：     {current.Id}\n" +
                $"会话ID：  {current.SessionId}\n" +
                $"模块名：  {current.MainModule?.ModuleName}\n" +
                $"内存：    {current.PrivateMemorySize64 / 1024 / 1024}MB\n" +
                $"优先级：  {current.PriorityClass}\n" +
                $"关联句柄：{current.Handle}\n" +
                $"线程统计：{current.Threads.Count}\n" +
                $"计算机名：{current.MachineName}\n";

            Console.WriteLine(pInfo);
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine("是否通过Process类结束当前进程：Y/N");
            string? input = Console.ReadLine();
            if (input?.ToLower() == "y")
                current.Kill();
        }

        /*【21002：线程】*/
        public static void LearnThread()
        {
            Console.WriteLine("【通过Thread类学习多线程基础】");

            //获取当前线程
            Thread current = Thread.CurrentThread;
            current.Name = "主线程";
            ShowThreadInfo(current);

            Console.WriteLine("》》》暂无其他线程执行，主线程正在执行《《《");
            PrintNumber();

            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("》》》创建3个其他前台线程《《《");
            //创建线程
            Thread thread1 = new Thread(PrintNumber);
            Thread thread2 = new Thread(PrintNumber);
            Thread thread3 = new Thread(PrintNumber);

            //设置线程名
            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";
            thread3.Name = $"线程{thread3.ManagedThreadId}";

            //设置线程优先级
            thread1.Priority = ThreadPriority.Highest;
            thread2.Priority = ThreadPriority.Normal;
            thread3.Priority = ThreadPriority.Lowest;

            //显示所有线程信息
            ShowThreadInfo(current, thread1, thread2, thread3);

            Console.WriteLine("》》》线程创建完成，开始同时执行子线程，主线程持续执行《《《");

            Thread.Sleep(2000);//暂停主线程

            //开始执行线程
            thread1.Start();
            thread2.Start();
            thread3.Start();

            //显示所有线程信息
            ShowThreadInfo(current, thread1, thread2, thread3);

            PrintNumber();//主线程持续执行

            //等待所有线程执行完成
            thread1.Join();
            thread2.Join();
            thread3.Join();

            //显示所有线程信息
            ShowThreadInfo(current, thread1, thread2, thread3);

            Thread.Sleep(2000);//暂停主线程

            Console.WriteLine("》》》从新创建并执行子线程，开始依次执行子线程，主线程持续执行《《《");

            thread1 = new Thread(PrintNumber);
            thread2 = new Thread(PrintNumber);
            thread3 = new Thread(PrintNumber);
            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";
            thread3.Name = $"线程{thread3.ManagedThreadId}";
            thread1.Priority = ThreadPriority.Highest;
            thread2.Priority = ThreadPriority.Normal;
            thread3.Priority = ThreadPriority.Lowest;
            ShowThreadInfo(thread1, thread2, thread3);

            Thread.Sleep(2000);//暂停主线程

            //开始执行线程
            thread1.Start();
            ShowThreadInfo(thread1);
            thread1.Join();

            thread2.Start();
            ShowThreadInfo(thread2);
            thread2.Join();

            thread3.Start();
            ShowThreadInfo(thread3);
            thread3.Join();

            //显示所有线程信息
            ShowThreadInfo(current, thread1, thread2, thread3);

            Thread.Sleep(2000);//暂停主线程

            Console.WriteLine("》》》所有线程执行完成，主线程继续执行《《《");
            ShowThreadInfo(current);
            PrintNumber();//主线程继续执行

            Console.WriteLine("-----------------------------------------------");

            Thread.Sleep(2000);//暂停主线程

            Console.WriteLine("》》》创建一个后台线程并执行《《《");

            //创建一个后台线程
            Thread thread4 = new Thread(PrintNumber);
            thread4.Name = $"后台线程{thread4.ManagedThreadId}";
            thread4.IsBackground = true;

            ShowThreadInfo(thread4);

            Thread.Sleep(2000);//暂停主线程

            thread4.Start();//开始执行线程
            ShowThreadInfo(thread4);

            thread4.Join();//等待线程执行完成
            ShowThreadInfo(thread4);

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void PrintNumber()
        {
            var name = Thread.CurrentThread.Name;
            for (int i = 1; i <= 10; i++)
            {
                Thread.Sleep(1000);//暂停线程，模拟耗时操作
                Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
                Console.WriteLine($"{name}输出数字{i:00}");

                //Thread.Sleep(1000);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //用于显示线程的信息
        private static void ShowThreadInfo(params Thread[] threads)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------------------------------------------");
            foreach (var thread in threads)
            {
                Console.WriteLine(">>>线程信息<<<");

                var info = $"线程名：      {thread.Name}\n" +
                    $"TID：         {thread.ManagedThreadId}\n" +
                    $"线程状态：    {thread.ThreadState}\n" +
                    $"执行状态：    {thread.IsAlive}\n" +
                    $"后台状态：    {(thread.ThreadState == System.Threading.ThreadState.Stopped ? "Has Stopped" : thread.IsBackground)}\n" +
                    $"属于线程池：  {thread.IsThreadPoolThread}\n" +
                    $"优先级：      {(thread.ThreadState == System.Threading.ThreadState.Stopped ? "Has Stopped" : thread.Priority)}\n";

                Console.WriteLine(info);
            }
            Console.WriteLine("----------------------------------------------");
        }

        public static void StartLearnProcessAndThread()
        {
            string title = "001 进程：Process类代码示例\n" +
                "002 线程：Thread类代码示例";

            do
            {
                Console.WriteLine("【进程与线程】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnProcess(); break;
                    case "002": LearnThread(); break;
                    default: Console.WriteLine("输入错误！"); break;
                }

                Console.WriteLine();
                Console.WriteLine("是否继续查询和运行【进程与线程】章节其他代码：直接按下Enter继续，否则即退出");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;
                Console.WriteLine("\n");
            }
            while (true);
        }
    }
}
