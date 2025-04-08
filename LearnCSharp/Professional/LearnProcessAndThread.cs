extern alias Helper;//引入外部程序集，并且为其创建一个别名
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using static Helper.HelperLibForLearnCSharp.SharedData;

namespace LearnCSharp.Professional
{
    internal class LearnProcessAndThread
    {
        //定义一些私有字段用于线程相关代码演示使用
        private static object objLock = new object();

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
            Console.WriteLine(  );

            var currentThreads = current.Threads;

            Console.WriteLine(">>>进程线程信息<<<");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine($"线程数：{currentThreads.Count}");

            foreach (ProcessThread thread in currentThreads)
            {
                Console.WriteLine("----------------------------------------------");

                var tInfo = $"线程ID：  {thread.Id}\n" +
                    $"优先级：  {thread.PriorityLevel}\n" +
                    $"状态：    {thread.ThreadState}\n" +
                    $"创建时间：{thread.StartTime}\n" +
                    $"CPU时间： {thread.TotalProcessorTime}";

                Console.WriteLine(tInfo);
                Console.WriteLine("----------------------------------------------\n");
            }

            Console.WriteLine("是否通过Process类结束当前进程：Y/N");
            string? input = Console.ReadLine();
            if (input?.ToLower() == "y")
                current.Kill();
        }

        /*【21002：线程】*/
        //用于显示线程的信息
        private static void ShowThreadInfo(params Thread[] threads)
        {
            Console.ResetColor();
            foreach (var thread in threads)
            {

                Console.WriteLine("----------------------------------------------");
                var info = $">>>线程信息<<<\n线程名：      {thread.Name}\n" +
                    $"TID：         {thread.ManagedThreadId}\n" +
                    $"线程状态：    {thread.ThreadState}\n" +
                    $"执行状态：    {thread.IsAlive}\n" +
                    $"后台状态：    {(thread.ThreadState == System.Threading.ThreadState.Stopped ? "Has Stopped" : thread.IsBackground)}\n" +
                    $"属于线程池：  {thread.IsThreadPoolThread}\n" +
                    $"优先级：      {(thread.ThreadState == System.Threading.ThreadState.Stopped ? "Has Stopped" : thread.Priority)}";

                Console.WriteLine(info);
                Console.WriteLine("----------------------------------------------\n");
            }
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
                    lock(objLock)
                    {
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.WriteLine($"【{name}】输出数字{i:00}");
                        Console.ResetColor();
                    }
                    //Thread.Sleep(1000);
                }
                Console.ResetColor();
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
            void PrintNumber(object obj)
            {
                (string threadName, int maxPrintNumber) = ((string, int))obj;
                string name = $"{threadName}{Thread.CurrentThread.ManagedThreadId,5}";//线程名称+线程ID

                for (int i = 1; i <= maxPrintNumber; i++)
                {
                    Thread.Sleep(500);//暂停线程，模拟耗时操作
                    lock (objLock)
                    {
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.WriteLine($"【{name}】输出数字{i:00}");
                        Console.ResetColor();
                    }
                }
                Console.ResetColor();
            }

            //获取当前线程
            Thread current = Thread.CurrentThread;
            current.Name = "主线程";
            ShowThreadInfo(current);

            Console.WriteLine("》》》暂无其他线程执行，主线程正在执行《《《");
            PrintNumber(("主线程", 12));//主线程执行

            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("》》》创建2个其他前台线程《《《");

            //创建线程
            Thread thread1 = new Thread(PrintNumber!);//使用方法名创建线程，在启动时传参
            Thread thread2 = new Thread(() => PrintNumber(("线程", 10)));//使用Lambda表达式创建线程，并直接传参

            //设置线程名
            thread1.Name = $"线程{thread1.ManagedThreadId}";
            thread2.Name = $"线程{thread2.ManagedThreadId}";

            //显示所有线程信息
            ShowThreadInfo(thread1, thread2);

            Console.WriteLine("》》》线程创建完成，开始同时执行子线程，主线程持续执行《《《");

            Thread.Sleep(2000);//暂停主线程

            //开始执行线程
            thread1.Start(("线程", 15));//传递参数
            thread2.Start();

            //显示所有线程信息
            ShowThreadInfo(thread1, thread2);

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

            int count = 0;
            //定义一个局部方法用于互斥锁的示例
            void PrintNumber(object obj)
            {
                (string opt, string lockMethod) calCount = ((string, string))obj;

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
                            lock (objLock)
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
                                lockTaken = Monitor.TryEnter(objLock, TimeSpan.FromSeconds(1));//获取锁
                                action.Invoke();
                            }
                            finally
                            {
                                if (lockTaken)
                                {
                                    Monitor.Exit(objLock);//释放锁
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
            int processCount = 4;//子进程数量

            int initialize = SetAngGetDataByMutex(0);//在主线程提前初始化SharedData，确保内存映射文件持续存在
            
            ProcessStartInfo GetPsi(string arg) => new ProcessStartInfo
            {
                FileName = "TestProcessAndThread.exe",
                Arguments = arg,
                UseShellExecute = true,
                RedirectStandardOutput = false,
                CreateNoWindow = true
            };

            Console.WriteLine($"》》》依次执行 {processCount} 个子进程对同一共享资源进行修改（无互斥体），输出最终共享资源值《《《");
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < processCount; i++)
            {
                using (Process process = Process.Start(GetPsi($"子进程{i + 1} {(i % 2 == 0 ? "++" : "--")}")!)!) 
                {
                    //string output = process1.StandardOutput.ReadToEnd();
                    process.WaitForExit();
                    //Console.WriteLine(output);

                    Console.ReadKey();//阻断主线程强制等待子进程完全结束
                    Thread.Sleep(2000);
                    Console.WriteLine($"子进程 {i + 1} 结束后获取共享资源值：{GetData()}");
                }

                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine($"所有子进程结束后预期共享资源值：{expectation}");
            Console.WriteLine($"所有子进程结束后获取共享资源值：{GetData()}");
            SetData(0);//重置共享资源值

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Thread.Sleep(2000);//等待子进程结束，确保共享资源值被重置

            Process[] processes = new Process[processCount];

            Console.WriteLine($"》》》同时执行 {processCount} 个子进程对同一共享资源进行修改（无互斥体），输出最终共享资源值《《《");
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < processCount; i++)
            {
                processes[i] = Process.Start(GetPsi($"子进程{i + 1} {(i % 2 == 0 ? "++" : "--")}")!)!;
            }

            for(int i = 0; i < processCount; i++)
            {
                processes[i].WaitForExit();
                processes[i].Exited += (sender, e) => processes[i].Dispose();
            }

            Console.ReadKey();
            Thread.Sleep(2000);
            Console.WriteLine($"所有子进程结束后预期共享资源值：{expectation}");
            Console.WriteLine($"所有子进程结束后获取共享资源值：{GetData()}");
            SetData(0);//重置共享资源值

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Thread.Sleep(2000);//等待子进程结束，确保共享资源值被重置

            Console.WriteLine($"》》》同时执行 {processCount} 个子进程对同一共享资源进行修改（互斥体），输出最终共享资源值《《《");
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < processCount; i++)
            {
                processes[i] = Process.Start(GetPsi($"子进程{i + 1} {(i % 2 == 0 ? "++" : "--")} Mutex")!)!;
            }

            for (int i = 0; i < processCount; i++)
            {
                processes[i].WaitForExit();
                processes[i].Exited += (sender, e) => processes[i].Dispose();
            }

            Console.ReadKey();
            Thread.Sleep(2000);
            Console.WriteLine($"所有子进程结束后预期共享资源值：{expectation}");
            Console.WriteLine($"所有子进程结束后获取共享资源值：{GetData()}");
            SetData(0);//重置共享资源值

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Dispose();
        }

        /*【21006：Semaphore信号量】*/
        public static void LearnSemaphore()
        {
            Console.WriteLine("\n------示例：信号量------\n");

            int initialCount = 2;//信号量初始计数器
            int maxCount = 4;//信号量最大计数器
            int threadCount = 10;//线程数量

            using Semaphore semaphore = new Semaphore(initialCount, maxCount);//创建信号量，初始计数器为2，最大计数器为4

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
                    lock (objLock)
                    {
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.WriteLine($"【{name}】输出数字{i:00}");
                        Console.ResetColor();
                    }
                }
                //Console.ResetColor();
                Console.WriteLine($"{name}完成输出");
                semaphore.Release();//释放信号量
            }

            Thread[] threads = new Thread[threadCount];//创建10个线程

            Console.WriteLine("》》》创建10其他前台线程《《《");

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(PrintNumber);
                threads[i].Name = $"线程{threads[i].ManagedThreadId}";
                ShowThreadInfo(threads[i]);
            }
            Console.WriteLine("》》》线程创建完成，开始同时执行子线程，主线程持续执行《《《");

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            
            Console.WriteLine();
        }

        /*【21007：ReaderWriterLock读写锁】*/
        public static void LearnReaderWriterLock()
        {
            Console.WriteLine("\n------示例：读写锁------\n");

            int count = 0;//计数器
            int capacity = 10000;//列表容量
            int threadCount = 7;//线程数量

            List<string> strings = new List<string>(capacity);

            //创建读写锁
            ReaderWriterLockSlim rwLock = new ReaderWriterLockSlim();

            //用于线程的方法
            void ReadOrWrite(object objBool)
            {
                bool isRead = (bool)objBool;
                var name = Thread.CurrentThread.Name;
                Console.WriteLine($"{name}正在{(isRead?"读取":"写入")}数据");

                if (isRead)
                {
                    while (count < capacity)
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
                            if (count >= capacity)
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

            Thread[] threads = new Thread[threadCount];//创建多个线程
            Console.WriteLine($"》》》创建{threadCount}其他前台线程《《《");

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(ReadOrWrite!);
                threads[i].Name = $"线程{threads[i].ManagedThreadId}";
                ShowThreadInfo(threads[i]);
            }
            Console.WriteLine("》》》线程创建完成，开始同时执行子线程，主线程持续执行《《《");

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start(i < threadCount / 2 ? true : false);
            }

            for (int i = 0; i < threads.Length; i++)
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
                    lock (objLock)
                    {
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.WriteLine($"【{name}】输出数字{i:00}");
                        Console.ResetColor();
                    }
                }
                Console.ResetColor();
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

            int maxNumber = 30;//最大输出数字
            int threadForLoopSleepTime = 1000;//线程循环输出数字的间隔时间
            int mainForLoopSleepTime = 500;//主线程循环执行业务的间隔时间
            int mainForLoopCount = 5;//主线程循环执行业务的次数
            int manualReserSleepTime = mainForLoopSleepTime * 10;//手动重置事件对象的间隔时间

            using ManualResetEvent mre = new ManualResetEvent(false);//创建一个手动重置事件对象

            void PrintNumber()
            {
                var name = Thread.CurrentThread.Name;
                Console.WriteLine($"{name}等待执行输出");
                mre.WaitOne();//等待信号量
                Console.WriteLine($"{name}收到了信号，开始执行输出");
                for (int i = 1; i <= maxNumber; i++)
                {
                    mre.WaitOne();//等待信号量
                    Thread.Sleep(threadForLoopSleepTime);//暂停线程，模拟耗时操作
                    lock (objLock)
                    {
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.WriteLine($"【{name}】输出数字{i:00}");
                        Console.ResetColor();
                    }
                }
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

            while(true)
            {
                for (int i = 1; i <= mainForLoopCount; i++)
                {
                    Thread.Sleep(mainForLoopSleepTime);
                    Console.WriteLine($"主线程正在执行一些事务[{i}]");
                }

                Console.WriteLine();
                Console.WriteLine("主线程完成了工作，现在发出信号通知子线程可以开始执行输出任务");

                mre.Set();//发出信号通知子线程可以开始执行输出任务

                if (!thread1.IsAlive && !thread2.IsAlive && !thread3.IsAlive)
                    break;//如果所有子线程都已经执行完成，则退出循环

                Thread.Sleep(manualReserSleepTime);//暂停主线程
                mre.Reset();//重置事件对象，阻止子线程继续执行

                Thread.Sleep(threadForLoopSleepTime);
                Console.WriteLine();
                Console.WriteLine("主线程介入了工作，现在发出信号通知子线程等待执行输出任务");
            }

            Console.WriteLine("所有线程均已完成工作！");
            Console.WriteLine();
        }

        /*【21010：CountdownEvent】*/
        public static void LearnCountdownEvent()
        {
            Console.WriteLine("\n------示例：CountdownEvent------\n");
            int initialCount = 5;//初始计数器值
            int count = 0;
            int capacity = 50;//strings列表的容量

            using CountdownEvent cde = new CountdownEvent(initialCount);//创建一个计数器

            List<string> strings = new List<string>(capacity);

            void UpdateStrings()
            {
                var name = Thread.CurrentThread.Name;
                Thread.Sleep(1000);
                Console.WriteLine($"{name}正在向strings列表添加数据");
                int sleepTime = Random.Shared.Next(200, 1500);

                while (true)
                {
                    lock(objLock)
                    {
                        if (count >= capacity)
                            break;
                        string data = $"编号：{strings.Count:00000} | 线程：{name,8} | 计数：{cde.CurrentCount, 5:00} | 随机数字：{Random.Shared.Next(100, 1000)}";
                        //Console.WriteLine(data);
                        strings.Add(data);
                        count++;
                    }
                    Thread.Sleep(sleepTime);//模拟耗时操作
                }

                cde.Signal();//发出信号，计数器减1
            }

            Console.WriteLine($"》》》创建{initialCount}个子线程执行对strings列表的写入操作《《《");
            Console.WriteLine("-----------------------------------------------");

            Thread[] threads = new Thread[initialCount];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(UpdateStrings);
                threads[i].Name = $"线程{threads[i].ManagedThreadId}";
                ShowThreadInfo(threads[i]);
            }

            Console.WriteLine("》》》创建完成《《《");
            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("》》》开始执行子线程《《《");
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
                ShowThreadInfo(threads[i]);
            }

            Thread.Sleep(500);

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
            Console.WriteLine("\n------示例：Barrier------\n");

            int participantCount = 10;//参与者数量
            int capacity = 50;//strings列表的容量
            List<string> strings = new List<string>(capacity);

            //创建一个Barrier对象，初始参与者数量为10
            using Barrier barrier = new Barrier(participantCount, (b) =>
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"所有线程到达屏障，当前屏障编号：{b.CurrentPhaseNumber}");
                Console.WriteLine($"所有线程到达屏障，当前屏障数量：{b.ParticipantCount}");
                Console.WriteLine("-----------------------------------------------");
            });

            void UpdateStrings()
            {
                var name = Thread.CurrentThread.Name;
                Thread.Sleep(2000);
                int phaseCount = capacity / participantCount;
                int sleepTime = Random.Shared.Next(200, 1500);
                for (int i = 1; i <= phaseCount; i++)
                {
                    lock (objLock)
                    {
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.WriteLine($"{name,-6}正在第{i}轮向strings列表写入数据");
                        strings.Add($"编号：{strings.Count:00000} | 线程：{name,8} | 轮次：{i,5:00} | 随机数字：{Random.Shared.Next(100,1000)}");
                        Thread.Sleep(sleepTime);//模拟耗时操作
                        Console.WriteLine($"{name,-6}完成第{i}轮向strings列表写入数据");
                        Console.ResetColor();
                    }

                    //不要将barrire.SignalAndWait()方法放入到处理共享数据的锁中，否则很可能造成死锁
                    barrier.SignalAndWait();//发出信号，等待其他线程到达屏障
                }
            }

            Console.WriteLine($"》》》创建{participantCount}个子线程执行对strings列表的写入操作《《《");
            Console.WriteLine("-----------------------------------------------");

            Thread[] threads = new Thread[participantCount];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(UpdateStrings);
                threads[i].Name = $"线程{threads[i].ManagedThreadId}";
                ShowThreadInfo(threads[i]);
            }

            Console.WriteLine("》》》创建完成《《《");
            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("》》》开始执行子线程《《《");
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Start();
                ShowThreadInfo(threads[i]);
            }

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine();
            Console.WriteLine("》》》子线程数据写入完毕，主线程继续执行输出strings列表数据《《《");
            Console.WriteLine("-----------------------------------------------");

            foreach (var str in strings)
            {
                Console.WriteLine(str);
            }

            Console.WriteLine();
        }

        /*【21012：ThreadPool线程池】*/
        public static void LearnThreadPool() 
        {
            Console.WriteLine("\n------示例：线程池------\n");
            int maxNumber = 5;//最大输出数字
            int taskCount = 10;//线程池任务数量
            int participantCount = 4;//参与者数量

            int mainForLoopSleepTime = 500;//主线程循环执行业务的间隔时间
            int mainForLoopCount = 5;//主线程循环执行业务的次数
            int threadForLoopSleepTime = 1000;//线程循环输出数字的间隔时间

            int minThreadCount = 4;//线程池最小线程数
            int maxThreadCount = 4;//线程池最大线程数

            using ManualResetEvent mre = new ManualResetEvent(false);//创建一个手动重置事件对象
            using CountdownEvent cde = new CountdownEvent(taskCount);//创建一个计数器

            void PrintNumber(object obj)
            {
                var name = $"池内线程{Thread.CurrentThread.ManagedThreadId}";
                int taskId = (int)obj;
                mre.WaitOne();//等待信号量
                Console.WriteLine($"{name}收到了信号，任务{taskId}开始执行输出");


                for (int i = 1; i <= maxNumber; i++)
                {
                    Thread.Sleep(threadForLoopSleepTime);
                    lock (objLock)
                    {
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.Write($"【{name}】 ");
                        Console.ResetColor();
                        Console.Write($"执行");
                        Console.ForegroundColor = (ConsoleColor)(taskId % 16);
                        Console.Write($" [任务{taskId}] ");
                        Console.ResetColor();
                        Console.WriteLine($"输出数字{i:00}");
                    }
                }

                cde.Signal();//发出信号，计数器减1
                Console.ResetColor();
            }


            Thread current = Thread.CurrentThread;
            current.Name = "主线程";
            ShowThreadInfo(Thread.CurrentThread);


            Console.WriteLine("》》》创建线程池并执行任务《《《");
            Console.WriteLine("-----------------------------------------------");

            ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);         //获取线程池最小线程数
            ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxCompletionPortThreads);   //获取线程池最大线程数

            Console.WriteLine($"默认线程池最小线程数：{workerThreads}");
            Console.WriteLine($"默认线程池最大线程数：{maxWorkerThreads}");

            ThreadPool.SetMinThreads(minThreadCount, completionPortThreads);        //设置线程池最小线程数
            ThreadPool.SetMaxThreads(maxThreadCount, maxCompletionPortThreads);     //设置线程池最大线程数

            ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);         //获取线程池最小线程数
            ThreadPool.GetMaxThreads(out maxWorkerThreads, out maxCompletionPortThreads);   //获取线程池最大线程数

            Console.WriteLine($"当前线程池最小线程数：{workerThreads}");
            Console.WriteLine($"当前线程池最大线程数：{maxWorkerThreads}");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            for (int i = 0; i < taskCount; i++)
            {
                ThreadPool.QueueUserWorkItem(PrintNumber, i + 1);
            }

            
            for (int i = 0; i < mainForLoopCount; i++)
            {
                Thread.Sleep(mainForLoopSleepTime);
                Console.WriteLine($"主线程正在执行一些事务[{i}]");
            }

            Console.WriteLine("主线程执行完成 开始执行线程池任务 主线程等待");

            mre.Set();//发出信号通知子线程可以开始执行输出任务

            cde.Wait();//等待计数器归零

            Console.WriteLine("所有子线程执行完成，主线程继续执行");

            Console.WriteLine();
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
