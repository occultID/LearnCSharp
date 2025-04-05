extern alias Helper;//引入外部程序集，并且为其创建一个别名
using System.Diagnostics;
using System.Runtime.InteropServices;
using static Helper.HelperLibForLearnCSharp.SharedData;

namespace LearnCSharp.Professional
{
    internal class LearnProcessAndThread
    {
        //定义一些私有字段用于线程相关代码演示使用
        private static object obj = new object();
        private static int count = 0;

        /*【21001：进程】*/
        public static void LearnProcess()
        {
            Console.WriteLine("\n------示例：通过Process类学习进程基础------\n");

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
        //用于显示线程的信息
        private static void ShowThreadInfo(params Thread[] threads)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------------------------------------------");
            foreach (var thread in threads)
            {
                var info = $">>>线程信息<<<\n线程名：      {thread.Name}\n" +
                    $"TID：         {thread.ManagedThreadId}\n" +
                    $"线程状态：    {thread.ThreadState}\n" +
                    $"执行状态：    {thread.IsAlive}\n" +
                    $"后台状态：    {(thread.ThreadState == System.Threading.ThreadState.Stopped ? "Has Stopped" : thread.IsBackground)}\n" +
                    $"属于线程池：  {thread.IsThreadPoolThread}\n" +
                    $"优先级：      {(thread.ThreadState == System.Threading.ThreadState.Stopped ? "Has Stopped" : thread.Priority)}";

                Console.WriteLine(info);
            }
            Console.WriteLine("----------------------------------------------\n");
        }

        /*【21002：线程】*/
        public static void LearnThread()
        {
            Console.WriteLine("\n------示例：通过Thread类学习多线程基础------\n");

            //用于线程的打印数字的方法
            void PrintNumber()
            {
                var name = Thread.CurrentThread.Name;
                for (int i = 1; i <= 10; i++)
                {
                    Thread.Sleep(500);//暂停线程，模拟耗时操作
                    Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
                    Console.WriteLine($"{name}输出数字{i:00}");

                    //Thread.Sleep(1000);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }

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

            //开始执行线程,根据执行结果可以看出线程的优先级对执行顺序的影响并不绝对
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

            //开始逐个执行线程
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
        }

        /*【21003：线程传参】*/
        public static void LearnThreadWithParams() 
        {
            Console.WriteLine("\n------示例：通过Thread类学习多线程传参------\n");

            //用于线程的打印数字的方法，需要使用参数传递
            void PrintNumber(object objParam)
            {
                (string threadName, int maxPrintNumber) = ((string, int))objParam;

                for (int i = 1; i <= maxPrintNumber; i++)
                {
                    Thread.Sleep(500);//暂停线程，模拟耗时操作
                    Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
                    Console.WriteLine($"{threadName}输出数字{i:00}");

                    //Thread.Sleep(1000);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            //获取当前线程
            Thread current = Thread.CurrentThread;
            current.Name = "主线程";
            ShowThreadInfo(current);

            Console.WriteLine("》》》暂无其他线程执行，主线程正在执行《《《");
            PrintNumber(("主线程", 12));//主线程执行

            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("》》》创建3个其他前台线程《《《");

            //创建线程
            Thread thread1 = new Thread(PrintNumber!);//使用方法名创建线程，在启动时传参
            Thread thread2 = new Thread(() => PrintNumber(("线程二", 10)));//使用Lambda表达式创建线程，并直接传参

            //设置线程名
            thread1.Name = "线程一";
            thread2.Name = "线程二";

            //显示所有线程信息
            ShowThreadInfo(current, thread1, thread2);

            Console.WriteLine("》》》线程创建完成，开始同时执行子线程，主线程持续执行《《《");

            Thread.Sleep(2000);//暂停主线程

            //开始执行线程
            thread1.Start(("线程一", 15));//传递参数
            thread2.Start();

            //显示所有线程信息
            ShowThreadInfo(current, thread1, thread2);

            PrintNumber(("主线程", 12));//主线程持续执行

            //等待所有线程执行完成
            thread1.Join();
            thread2.Join();

            //显示所有线程信息
            ShowThreadInfo(current, thread1, thread2);
        }

        /*【21004：互斥锁】*/
        public static void LearnLockAndMonitor()
        {
            Console.WriteLine("\n------示例：互斥锁------\n");

            //定义一个局部方法用于互斥锁的示例
            void PrintNumber(object objParam)
            {
                (string opt, string lockMethod) calCount = ((string, string))objParam;

                Thread.Sleep(500);

                var name = Thread.CurrentThread.Name;
                Console.WriteLine($"{name}正在对 count 进行{(calCount.opt == "++"?"累加":"累减")}操作");

                Action action = calCount.lockMethod == "Interlocked" 
                    ? (calCount.opt == "++" ? new Action(() => Interlocked.Increment(ref count)) : new Action(() => Interlocked.Decrement(ref count))) 
                    : (calCount.opt == "++" ? new Action(() => count++) : new Action(() => count--));

                switch (calCount.lockMethod)
                {
                    case "None":
                    case "Interlocked":
                        for (int i = 1; i <= 100000; i++)
                        {
                            action.Invoke();
                        }
                        break;
                    case "Lock":
                        for (int i = 1; i <= 100000; i++)
                        {
                            lock (obj)
                            {
                                action.Invoke();
                            }
                        }
                        break;
                    case "Monitor":
                        for (int i = 1; i <= 100000; i++)
                        {
                            bool lockTaken = false;//是否获取锁的标志
                            try
                            {
                                lockTaken = Monitor.TryEnter(obj, TimeSpan.FromSeconds(1));//获取锁
                                action.Invoke();
                            }
                            finally
                            {
                                if (lockTaken)
                                {
                                    Monitor.Exit(obj);//释放锁
                                }
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("无效的锁方法");
                        break;
                }
            }

            //获取当前线程
            Thread current = Thread.CurrentThread;
            current.Name = "主线程";
            ShowThreadInfo(current);

            Console.WriteLine("》》》主线程依次执行累加累减count变量的方法，输出count结果《《《");
            
            PrintNumber(("++", "None"));
            PrintNumber(("--", "None"));

            Console.WriteLine($"计算结果：{count}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            count = 0;//重置count变量
            Thread.Sleep(2000);//暂停主线程

            Console.WriteLine("》》》创建两个线程分别同时执行累加和累减count变量的方法（不加锁），完成后输出count结果《《《");

            Thread thread1 = new Thread(PrintNumber!);
            Thread thread2 = new Thread(PrintNumber!);

            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";
            
            ShowThreadInfo(thread1, thread2);

            thread1.Start(("++", "None"));
            thread2.Start(("--", "None"));

            ShowThreadInfo(thread1, thread2);

            thread1.Join();
            thread2.Join();

            ShowThreadInfo(thread1, thread2);

            Console.WriteLine($"计算结果：{count}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            count = 0;//重置count变量

            Thread.Sleep(2000);//暂停主线程
            Console.WriteLine("》》》创建两个线程分别同时执行累加和累减count变量的方法（原子锁），完成后输出count结果《《《");

            thread1 = new Thread(PrintNumber!);
            thread2 = new Thread(PrintNumber!);

            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";

            ShowThreadInfo(thread1, thread2);

            thread1.Start(("++", "Interlocked"));
            thread2.Start(("--", "Interlocked"));

            ShowThreadInfo(thread1, thread2);

            thread1.Join();
            thread2.Join();

            ShowThreadInfo(thread1, thread2);

            Console.WriteLine($"计算结果：{count}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            count = 0;//重置count变量

            Thread.Sleep(2000);//暂停主线程
            Console.WriteLine("》》》创建两个线程分别同时执行累加和累减count变量的方法（加互斥锁），完成后输出count结果《《《");

            thread1 = new Thread(PrintNumber!);
            thread2 = new Thread(PrintNumber!);

            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";

            ShowThreadInfo(thread1, thread2);

            thread1.Start(("++", "Lock"));
            thread2.Start(("--", "Lock"));

            ShowThreadInfo(thread1, thread2);

            thread1.Join();
            thread2.Join();

            ShowThreadInfo(thread1, thread2);

            Console.WriteLine($"计算结果：{count}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            count = 0;//重置count变量

            Thread.Sleep(2000);//暂停主线程
            Console.WriteLine("》》》创建两个线程分别同时执行累加和累减count变量的方法（手动加锁Monitor），完成后输出count结果《《《");

            thread1 = new Thread(PrintNumber!);
            thread2 = new Thread(PrintNumber!);

            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";

            ShowThreadInfo(thread1, thread2);

            thread1.Start(("++", "Monitor"));
            thread2.Start(("--", "Monitor"));

            ShowThreadInfo(thread1, thread2);

            thread1.Join();
            thread2.Join();

            ShowThreadInfo(thread1, thread2);

            Console.WriteLine($"计算结果：{count}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
            count = 0;//重置count变量
        }

        /*【21005：Mutex互斥体】*/
        public static void LearnMutex()
        {
            Console.WriteLine("\n------示例：互斥体------\n");

            Console.WriteLine("-----------------------------------------------");
            string description = "测试互斥体的子进程代码示例：\n" +
                "1. 主进程通过Process类创建两个子进程，执行TestProcessAndThread.exe\n" +
                "2. 通过命令行参数传递给子进程，执行不同的操作\n" +
                "   2.1 参数说明{参数一 参数二 参数三}\n" +
                "       2.1.1 参数一 指定一个易于观察的进程名称\n" +
                "       2.1.2 参数二 指定对共享内存中整数的操作是递增(++)还是递减(--)\n" +
                "       2.1.3 参数三 指定进程对共享内存的操作是否使用Mutex互斥体\n" +
                "   2.2 本示例两个子进程的具体操作\n" +
                "       2.2.1 子进程一 对共享内存中整数进行100万次递增(++)\n" +
                "       2.2.2 子进程二 对共享内存中整数进行100万次递减(--)\n" +
                "3. 子进程通过共享内存和互斥体进行数据共享和同步操作\n" +
                "4. 主线程等待子进程结束后获取共享资源值";

            Console.WriteLine(description);
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            int expectation = 0;//预期共享资源值
            int initialize = SetAngGetDataByMutex(0);//在主线程提前初始化SharedData，确保内存映射文件持续存在
            
            ProcessStartInfo GetPsi(string arg) => new ProcessStartInfo
            {
                FileName = "TestProcessAndThread.exe",
                Arguments = arg,
                UseShellExecute = true,
                RedirectStandardOutput = false,
                CreateNoWindow = true
            };

            Console.WriteLine("》》》依次执行两个子进程对同一共享资源进行修改（无互斥体），输出最终共享资源值《《《");
            Console.WriteLine("-----------------------------------------------");

            using (Process process1 = Process.Start(GetPsi("子进程一 ++")!)!)
            {
                //string output = process1.StandardOutput.ReadToEnd();
                process1.WaitForExit();
                //Console.WriteLine(output);

                Console.ReadKey();//阻断主线程强制等待子进程完全结束
                Thread.Sleep(2000);
                Console.WriteLine($"子进程一结束后获取共享资源值：{GetData()}");
            }

            Console.WriteLine();

            using (Process process2 = Process.Start(GetPsi("子进程二 --")!)!)
            {
                //string output = process2.StandardOutput.ReadToEnd();
                process2.WaitForExit();
                // Console.WriteLine(output);
                Console.ReadKey();

                Thread.Sleep(2000);
                Console.WriteLine($"子进程二结束后获取共享资源值：{GetData()}");
            }

            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine($"子进程一和二结束后预期共享资源值：{expectation}");
            Console.WriteLine($"子进程一和二结束后获取共享资源值：{GetData()}");
            SetData(0);//重置共享资源值

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Thread.Sleep(2000);//等待子进程结束，确保共享资源值被重置

            Console.WriteLine("》》》同时执行两个子进程对同一共享资源进行修改（无互斥体），输出最终共享资源值《《《");
            Console.WriteLine("-----------------------------------------------");

            using (Process process1 = Process.Start(GetPsi("子进程一 ++")!)!, process2 = Process.Start(GetPsi("子进程二 --")!)!)
            {
                // Read the output (or error) stream first and then wait.
                //string output1 = process1.StandardOutput.ReadToEnd();
                //string output2 = process2.StandardOutput.ReadToEnd();
                process1.WaitForExit();
                process2.WaitForExit();
                //Console.WriteLine(output1);
                //Console.WriteLine(output2);

                Console.ReadKey();
                Thread.Sleep(2000);
                Console.WriteLine($"子进程一和二结束后预期共享资源值：{expectation}");
                Console.WriteLine($"子进程一和二结束后获取共享资源值：{GetData()}");
                SetData(0);
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Thread.Sleep(2000);//等待子进程结束，确保共享资源值被重置

            Console.WriteLine("》》》同时执行两个子进程对同一共享资源进行修改（互斥体），输出最终共享资源值《《《");
            Console.WriteLine("-----------------------------------------------");

            using (Process process1 = Process.Start(GetPsi("子进程一 ++ Mutex")!)!, process2 = Process.Start(GetPsi("子进程二 -- Mutex")!)!)
            {
                // Read the output (or error) stream first and then wait.
                //string output1 = process1.StandardOutput.ReadToEnd();
                //string output2 = process2.StandardOutput.ReadToEnd();
                process1.WaitForExit();
                process2.WaitForExit();
                //Console.WriteLine(output1);
                //Console.WriteLine(output2);

                Console.ReadKey();
                Thread.Sleep(2000);
                Console.WriteLine($"子进程一和二结束后预期共享资源值：{expectation}");
                Console.WriteLine($"子进程一和二结束后获取共享资源值：{GetData()}");
                SetData(0);
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Dispose();
        }

        /*【21006：Semaphore信号量】*/
        public static void LearnSemaphore()
        {
            Console.WriteLine("\n------示例：信号量------\n");

            using Semaphore semaphore = new Semaphore(2, 4);//创建信号量，初始计数器为2，最大计数器为4

            //用于线程的打印数字的方法
            void PrintNumber()
            {
                var name = Thread.CurrentThread.Name;
                Console.WriteLine($"{name}等待执行输出");

                Thread.Sleep(2000);
                semaphore.WaitOne();//等待信号量
                ShowThreadInfo(Thread.CurrentThread);

                Thread.Sleep(1000);
                Console.WriteLine($"{name}正在执行输出");
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(2000);//暂停线程，模拟耗时操作
                    Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
                    Console.WriteLine($"{name}输出数字{i:00}");
                    //Thread.Sleep(1000);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"{name}完成输出");
                semaphore.Release();//释放信号量
            }

            Thread[] threads = new Thread[10];//创建10个线程

            Console.WriteLine("》》》创建10其他前台线程《《《");

            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(PrintNumber);
                threads[i].Name = $"线程{threads[i].ManagedThreadId}";
                ShowThreadInfo(threads[i]);
            }
            Console.WriteLine("》》》线程创建完成，开始同时执行子线程，主线程持续执行《《《");

            for (int i = 0; i < 10; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < 10; i++)
            {
                threads[i].Join();
            }
            
            Console.WriteLine();
            Console.ReadKey();
        }

        /*【21007：ReaderWriterLock读写锁】*/
        public static void LearnReaderWriterLock()
        {
            Console.WriteLine("\n------示例：读写锁------\n");
            //创建读写锁
            ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();
            List<string> strings = new List<string>(10000);
            int count = 0;

            //用于线程的打印数字的方法
            void ReadOrWrite(object objBool)
            {
                bool isRead = (bool)objBool;
                var name = Thread.CurrentThread.Name;
                Console.WriteLine($"{name}正在{(isRead?"读取":"写入")}数据");

                if (isRead)
                {
                    while (count < 10000)
                    {
                        try
                        {
                            rwLock.EnterReadLock();//获取读锁
                            if (strings.Count > 0) 
                                Console.WriteLine($"{name}读取数据-- {strings[^1]}");
                        }
                        finally
                        {
                            rwLock.ExitReadLock();
                        }

                        Thread.Sleep(TimeSpan.FromMilliseconds(0.01));
                    }
                }
                else
                {
                    while (true)
                    {
                        try
                        {
                            rwLock.EnterWriteLock();
                            if (count >= 10000)
                                break;

                            string data = $"【数据】{name}：{count:000000}";
                            strings.Add(data);
                            count++;
                            //Console.WriteLine($"{name}写入数据-- {data}");
                        }
                        finally
                        {
                            rwLock.ExitWriteLock();
                        }
                        Thread.Sleep(TimeSpan.FromMilliseconds(0.01));
                    }
                }
            }

            Thread[] threads = new Thread[7];//创建7个线程
            Console.WriteLine("》》》创建7其他前台线程《《《");

            for (int i = 0; i < 7; i++)
            {
                threads[i] = new Thread(ReadOrWrite!);
                threads[i].Name = $"线程{threads[i].ManagedThreadId}";
                ShowThreadInfo(threads[i]);
            }
            Console.WriteLine("》》》线程创建完成，开始同时执行子线程，主线程持续执行《《《");

            for (int i = 0; i < 7; i++)
            {
                threads[i].Start(i < 3 ? true : false);
            }

            for (int i = 0; i < 7; i++)
            {
                threads[i].Join();
            }

            Console.ReadKey();
            Console.WriteLine($"strings列表数据：【{strings.Count}】");
            foreach (var str in strings)
            {
                Console.WriteLine(str);
            }
            Console.WriteLine();
        }

        /*【21008：WaitHandle等待句柄：AutoResetEvent】*/
        public static void LearnAutoResetEvent()
        {
            Console.WriteLine("\n------示例：WaitHandle等待句柄--AutoResetEvent------\n");

            using AutoResetEvent are = new AutoResetEvent(false);

            void PrintNumber()
            {
                var name = Thread.CurrentThread.Name;
                Console.WriteLine($"{name}等待执行输出");
                are.WaitOne();//等待信号量
                Console.WriteLine($"{name}收到了信号，开始执行输出");
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(2000);//暂停线程，模拟耗时操作
                    Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
                    Console.WriteLine($"{name}输出数字{i:00}");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Thread thread = new Thread(PrintNumber);

            thread.Name = $"线程{thread.ManagedThreadId}";

            ShowThreadInfo(thread);

            thread.Start();

            ShowThreadInfo(thread);

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1500);
                Console.Write($"\r主线程正在执行一些事务[{i}]");
            }
            Console.WriteLine();

            Console.WriteLine("主线程完成了工作，现在发出信号通知子线程可以开始执行输出任务");
            are.Set();//发出信号通知子线程可以开始执行输出任务

            thread.Join();

            Console.WriteLine();
        }

        /*【21009：WaitHandle等待句柄：ManualResetEvent】*/
        public static void LearnManualResetEvent()
        {
            Console.WriteLine("\n------示例：WaitHandle等待句柄--ManualResetEvent------\n");

            using ManualResetEvent mre = new ManualResetEvent(false);

            void PrintNumber()
            {
                var name = Thread.CurrentThread.Name;
                Console.WriteLine($"{name}等待执行输出");
                mre.WaitOne();//等待信号量
                Console.WriteLine($"{name}收到了信号，开始执行输出");
                for (int i = 1; i <= 5; i++)
                {
                    Thread.Sleep(2000);//暂停线程，模拟耗时操作
                    Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
                    Console.WriteLine($"{name}输出数字{i:00}");
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            Thread thread1 = new Thread(PrintNumber);
            Thread thread2 = new Thread(PrintNumber);
            Thread thread3 = new Thread(PrintNumber);

            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";
            thread3.Name = $"线程{thread3.ManagedThreadId}";

            ShowThreadInfo(thread1, thread2, thread3);

            thread1.Start();
            thread2.Start();
            thread3.Start();

            Thread.Sleep(500);
            ShowThreadInfo(thread1, thread2, thread3);

            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1500);
                Console.Write($"\r主线程正在执行一些事务[{i}]");
            }
            Console.WriteLine();
            Console.WriteLine("主线程完成了工作，现在发出信号通知子线程可以开始执行输出任务");
            
            mre.Set();//发出信号通知子线程可以开始执行输出任务
            
            thread1.Join();
            thread2.Join();
            thread3.Join();

            Console.WriteLine();
        }

        /*【21010：CountdownEvent】*/
        public static void LearnCountdownEvent()
        {
            Console.WriteLine("\n------示例：WaitHandle等待句柄--ManualResetEvent------\n");

            using CountdownEvent cde = new CountdownEvent(3);//创建一个计数器，初始值为3

            int count = 0;
            List<string> strings = new List<string>(50);

            void UpdateStrings()
            {
                var name = Thread.CurrentThread.Name;
                Thread.Sleep(1000);
                Console.WriteLine($"{name}正在向strings列表添加数据");
                
                while(true)
                {
                    lock(obj)
                    {
                        if (count >= 50)
                            break;
                        string data = $"【数据】{name}：{count:000000}";
                        strings.Add(data);
                        count++;
                    }
                    Thread.Sleep(200);//模拟耗时操作
                }

                cde.Signal();//发出信号，计数器减1
            }

            Console.WriteLine("》》》创建三个子线程执行对strings列表的写入操作《《《");
            Console.WriteLine("-----------------------------------------------");

            Thread thread1 = new Thread(UpdateStrings);
            Thread thread2 = new Thread(UpdateStrings);
            Thread thread3 = new Thread(UpdateStrings);

            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";
            thread3.Name = $"线程{thread3.ManagedThreadId}";

            Console.WriteLine("》》》创建完成《《《");
            Console.WriteLine("-----------------------------------------------");
            ShowThreadInfo(thread1, thread2, thread3);

            Console.WriteLine("》》》开始执行子线程《《《");
            Console.WriteLine("-----------------------------------------------");
            thread1.Start();
            thread2.Start();
            thread3.Start();

            Thread.Sleep(100);
            ShowThreadInfo(thread1, thread2, thread3);

            cde.Wait();//等待计数器归零

            Console.WriteLine();
            Console.WriteLine("》》》子线程数据写入完毕，主线程继续执行输出strings列表数据《《《");
            Console.WriteLine("-----------------------------------------------");

            foreach (var str in strings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }

        /*【21011：Barrier】*/
        public static void LearnBarrier()
        {

        }

        public static void StartLearnProcessAndThread()
        {
            string title = "001 进程：Process类代码示例\n" +
                "002 线程：Thread类代码示例\n"+
                "003 线程：线程传参\n" +
                "004 线程：互斥锁\n" +
                "005 进程：互斥体\n" +
                "006 线程：信号量";

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
