using HelperLibForLearnCSharp;
using System.Diagnostics;
using static HelperLibForLearnCSharp.SharedData; 

namespace TestProcessAndThread
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "进程竞争测试";
            Process current = Process.GetCurrentProcess();
            int counter = 0;

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
            Console.WriteLine();

            if (args.Length == 3 && args[2] == "Mutex")
            {
                for (int i = 0; i < 1000000; i++)
                {
                    if (SharedData.Mutex.WaitOne(1000))
                    {
                        try
                        {
                            counter = GetData();

                            if (args[1] == "++")
                                counter++;
                            else
                                counter--;

                            if ((i + 1) % 50000 == 0)
                                Console.WriteLine($"{args[0]}第{i + 1:0000000}次计算共享资源值结果：{counter}");
                            SetData(counter);
                        }
                        finally
                        {
                            SharedData.Mutex.ReleaseMutex();
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 1000000; i++)
                {
                    counter = GetData();
                    //Thread.Sleep(TimeSpan.FromMilliseconds(1));

                    if (args[1] == "++")
                        counter++;
                    else
                        counter--;

                    if ((i + 1) % 50000 == 0)
                        Console.WriteLine($"{args[0]}第{i + 1:0000000}次计算共享资源值结果：{counter}");
                    SetData(counter);
                }
            }

            Console.WriteLine($"{args[0]}结束后获取共享资源值：{GetData()}");
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
