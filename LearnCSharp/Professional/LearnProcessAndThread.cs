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
                $"模块名：  {current.MainModule.ModuleName}\n" +
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
            string outputString = "";

            //局部方法：用于显示线程的信息
            void ShowThreadInfo(params Thread[] threads)
            {
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

            void PrintThreadWork(int c)
            {
                for (int i = 1; i <= 10; i++)
                {
                    outputString = $"线程{c}输出字符串{i:0000}";
                    Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
                    Console.WriteLine(outputString);
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("【通过Thread类获取当前线程信息】");

            //获取当前线程
            Thread current = Thread.CurrentThread;
            ShowThreadInfo(current);

            //新建两个线程
            Thread thread1 = new Thread(() => PrintThreadWork(1));
            Thread thread2 = new Thread(() => PrintThreadWork(2));

            //为线程命名
            thread1.Name = "线程一";
            thread2.Name = "线程二";

            Console.WriteLine("\n【通过Thread类新建两个线程】");
            ShowThreadInfo(thread1, thread2);

            Console.WriteLine("\n【执行新建线程并查看输出】");

            //开始线程
            thread1.Start();
            thread2.Start();
            ShowThreadInfo(thread1, thread2);

            //直到新建线程执行完毕主线程才会继续执行
            thread1.Join();
            thread2.Join();

            Console.ForegroundColor = ConsoleColor.Gray;
            ShowThreadInfo(thread1, thread2);
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
