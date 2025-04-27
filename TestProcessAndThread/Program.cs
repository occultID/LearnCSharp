using HelperLibForLearnCSharp;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using static HelperLibForLearnCSharp.SharedData; 

namespace TestProcessAndThread
{
    internal class Program
    {
        private static readonly object objLock = new object();
        static void Main(string[] args)
        {
            if (args.Length < 2) 
            {
                throw new ArgumentException("本程序至少提供功能指定参数和进程命名参数才能启动");
            }

            Console.Title = args[1] + " " + args[0] switch
            {
                "1" => "竞争测试",
                _=> "参数错误"
            };

            Thread.Sleep(1500);

            Process current = Process.GetCurrentProcess();

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

            string[] newArgs = new string[args.Length - 1];
            for (int i = 0; i < args.Length - 1; i++)
            {
                newArgs[i] = args[i + 1];
            }

            switch (args[0])
            {
                case "1":
                    ProcessTest(newArgs);
                    break;
                default:
                    Console.WriteLine("参数错误，请检查参数是否正确");
                    break;
            }
        }

        private static void ProcessTest(params string[] args)
        {
            int counter = 0;

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
