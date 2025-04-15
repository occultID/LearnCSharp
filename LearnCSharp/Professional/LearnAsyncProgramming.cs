using LearnCSharp.Professional.LeanrAsyncProgrammingSpace;
using System.Collections.Concurrent;

namespace LearnCSharp.Professional
{
    class LearnAsyncProgramming
    {
        //显示线程信息
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

        //【21101：Task】
        public static void LearnTask()
        {
            Console.WriteLine("\n------示例：Task任务------\n");

            int printMaxNumber = 20;
            int delayTime = 200;

            void PrintNumber()
            {
                Console.WriteLine("输出任务开始执行");
                string name = $"线程 {Thread.CurrentThread.ManagedThreadId:00}";
                for (int i = 0; i <= printMaxNumber; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【{name}】输出数字{i:000}");
                    Console.ResetColor();
                    Task.Delay(delayTime).Wait();
                }
                Console.WriteLine("输出任务结束执行");
            }

            async Task PrintNumberAsync()
            {
                Console.WriteLine("输出任务开始执行");

                for (int i = 0; i <= printMaxNumber; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出数字{i:000}");
                    Console.ResetColor();
                    await Task.Delay(delayTime);
                }
                Console.WriteLine("输出任务结束执行");
            }

            Thread current = Thread.CurrentThread;
            current.Name = $"主线程";
            Console.WriteLine("》》》主线程信息《《《");
            ShowThreadInfo(Thread.CurrentThread);

            Console.WriteLine("》》》创建一个任务用于输出数字（new Task方法）《《《");
            Console.WriteLine("-----------------------------------------------");

            Task newPrintTask = new Task(PrintNumber);

            Console.WriteLine($"任务{newPrintTask.Id:00}状态：{newPrintTask.Status}");
            newPrintTask.Start();
            Console.WriteLine($"任务{newPrintTask.Id:00}状态：{newPrintTask.Status}");
            Console.WriteLine("主线程持续执行");
            Console.WriteLine();

            newPrintTask.Wait(); // 等待任务完成

            Console.WriteLine($"任务{newPrintTask.Id:00}状态：{newPrintTask.Status}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("》》》创建一个任务用于输出数字（Task.Factory.StartNew方法）《《《");
            Console.WriteLine("-----------------------------------------------");

            Task tfPrintTask = Task.Factory.StartNew(PrintNumber);

            Console.WriteLine($"任务{tfPrintTask.Id:00}状态：{tfPrintTask.Status}");
            Console.WriteLine("主线程持续执行");
            Console.WriteLine();

            tfPrintTask.Wait(); // 等待任务完成

            Console.WriteLine($"任务{tfPrintTask.Id:00}状态：{tfPrintTask.Status}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("》》》创建一个任务用于输出数字（Task.Run方法）《《《");
            Console.WriteLine("-----------------------------------------------");

            Task runPrintTask = Task.Run(PrintNumber);

            Console.WriteLine($"任务{runPrintTask.Id:00}状态：{runPrintTask.Status}");
            Console.WriteLine("主线程持续执行");
            Console.WriteLine();

            runPrintTask.Wait(); // 等待任务完成

            Console.WriteLine($"任务{runPrintTask.Id:00}状态：{runPrintTask.Status}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("》》》创建一个任务用于输出数字（async-await方法）《《《");
            Console.WriteLine("-----------------------------------------------");

            PrintNumberAsync();//异步调用，由于未使用await关键字，所以不会等待任务完成

            Console.WriteLine("主线程持续执行");
            Console.WriteLine();

            Thread.Sleep(printMaxNumber * delayTime + 1000);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        //【21102：异步方法】
        public static void LearnAsyncMethod()
        {
            Console.WriteLine("\n------示例：异步方法------\n");
            int maxNumber = 20;
            int delayTime = 100;

            async Task<int> CalculateNumberAsync()
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务开始执行");
                int result = 0;
                for (int i = 1; i <= maxNumber; i++)
                {
                    result += i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前输出结果：{result}");
                    Console.ResetColor();
                    await Task.Delay(delayTime);
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务结束执行");
                return result;
            }

            async void PrintResultAsync()
            {
                Console.WriteLine($"》》》创建一个任务用于输出0到{maxNumber}的整数之和《《《");
                Console.WriteLine("-----------------------------------------------");

                Console.WriteLine($"【线程{Thread.CurrentThread.ManagedThreadId:00}】调用异步方法CalculateNumberAsync并等待结果");
                
                int result = await CalculateNumberAsync();

                Console.WriteLine($"【线程{Thread.CurrentThread.ManagedThreadId:00}】调用异步方法CalculateNumberAsync完成并输出结果");
                Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                Console.WriteLine($"【线程{Thread.CurrentThread.ManagedThreadId:00}】已获取0到{maxNumber}的和，输出结果：{result}");
                Console.ResetColor();
                Console.WriteLine();

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();
            }

            Thread current = Thread.CurrentThread;
            current.Name = $"主线程";
            Console.WriteLine("》》》主线程信息《《《");
            ShowThreadInfo(Thread.CurrentThread);

            Console.WriteLine($"主线程{current.ManagedThreadId}调用异步方法PrintResultAsync,未使用await进行等待");

            PrintResultAsync();

            Console.WriteLine("主线程持续运行");
            Thread.Sleep(maxNumber * delayTime + 1000);
        }

        //【21103：同步上下文】
        public static void LearnSynchronizationContext()
        {
            Console.WriteLine("\n------示例：同步上下文------\n");

            int maxNumber = 20;

            async Task<int> CalculateNumberAsync()
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务开始执行");
                int result = 0;
                for (int i = 1; i <= maxNumber; i++)
                {
                    result += i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前输出结果：{result}");
                    Console.ResetColor();
                    await Task.Delay(100);
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务结束执行");
                return result;
            }

            async Task PrintResultAsync()
            {
                //int result = 0;
                Console.WriteLine($"》》》创建一个任务用于输出0到{maxNumber}的整数之和《《《");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】调用异步方法CalculateNumberAsync并等待结果");
                int result = await CalculateNumberAsync();
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】调用异步方法CalculateNumberAsync完成并输出结果");
                Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】已获取0到{maxNumber}的和，输出结果：{result}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();
            }

            var context = new CustomSynchronizationContext();//创建自定义同步上下文
            SynchronizationContext.SetSynchronizationContext(context);//获取当前线程的同步上下文
            if (context == null)
            {
                Console.WriteLine("当前没有同步上下文");
                return;
            }
            Console.WriteLine($"当前同步上下文类型：{context.GetType()}");
            Console.WriteLine($"当前同步上下文ID：{context.GetHashCode()}");
            Console.WriteLine();

            Thread current = Thread.CurrentThread;
            current.Name = $"主线程";
            Console.WriteLine("》》》主线程信息《《《");
            ShowThreadInfo(Thread.CurrentThread);

            context.Post(async _ =>
            {
                try { await PrintResultAsync(); }//异步调用PrintResultAsync方法
                finally { context.Complete(); }//完成添加
            }, null);

            context.RunOnCurrentThread();//运行消息循环
        }

        //【21104：ConfigureAwait】
        public static void LearnConfigureAwait()
        {
            Console.WriteLine("\n------示例：ConfigureAwait------\n");

            int maxNumber = 20;

            async Task<int> CalculateNumberAsync()
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务开始执行");
                int result = 0;
                for (int i = 1; i <= maxNumber; i++)
                {
                    result += i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前输出结果：{result}");
                    Console.ResetColor();

                    await Task.Delay(100).ConfigureAwait(false);//使用ConfigureAwait(false)配置上下文,后续代码不会在原线程中执行
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务结束执行");
                return result;
            }

            async Task PrintResultAsync()
            {
                //int result = 0;
                Console.WriteLine($"》》》创建一个任务用于输出0到{maxNumber}的整数之和《《《");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】调用异步方法CalculateNumberAsync并等待结果");

                int result = await CalculateNumberAsync().ConfigureAwait(true);//使用ConfigureAwait(true)配置上下文,后续代码会在原线程中执行,这是默认值

                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】调用异步方法CalculateNumberAsync完成并输出结果");
                Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】已获取0到{maxNumber}的和，输出结果：{result}");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();
            }

            var context = new CustomSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(context);//获取当前线程的同步上下文
            if (context == null)
            {
                Console.WriteLine("当前没有同步上下文");
                return;
            }
            Console.WriteLine($"当前同步上下文类型：{context.GetType()}");
            Console.WriteLine($"当前同步上下文ID：{context.GetHashCode()}");
            Console.WriteLine();

            Thread current = Thread.CurrentThread;
            current.Name = $"主线程";
            Console.WriteLine("》》》主线程信息《《《");
            ShowThreadInfo(Thread.CurrentThread);

            context.Post(async _ =>
            {
                try { await PrintResultAsync(); }
                finally { context.Complete(); }//完成添加
            }, null);//异步调用PrintResultAsync方法

            context.RunOnCurrentThread();//运行消息循环线程
        }

        public static void StartLearnAsyncProgramming()
        {
            string title = "001 进程：Process类代码示例\n" +
                "002 线程：Thread类代码示例\n" +
                "003 线程：线程传参\n" +
                "004 线程：互斥锁\n" +
                "005 进程：互斥体\n" +
                "006 线程：信号量\n" +
                "007 线程：读写锁\n" +
                "008 等待句柄：AutoResetEvent\n" +
                "009 等待句柄：ManualResetEvent\n" +
                "010 CountdownEvent\n" +
                "011 Barrier\n" +
                "012 线程池\n" +
                "013 CancellationToken";


            do
            {
                Console.WriteLine("【进程与线程】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
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

namespace LearnCSharp.Professional.LeanrAsyncProgrammingSpace
{
    class CustomSynchronizationContext : SynchronizationContext
    {
        private readonly BlockingCollection<(SendOrPostCallback Callback, object State, ManualResetEvent Mre)> queue = new();//阻塞集合

        //将委托异步调用添加到队列中，Post方法用于异步调用
        public override void Post(SendOrPostCallback callback, object state)
        {
            queue.Add((callback, state, null));//添加到阻塞集合中
        }

        //将委托同步调用添加到队列中，Send方法用于同步调用
        public override void Send(SendOrPostCallback callback, object state)
        {
            var mre = new ManualResetEvent(false);//创建一个手动重置事件
            queue.Add((callback, state, mre));//添加到阻塞集合中
            mre.WaitOne();//等待事件被设置
        }

        public void RunOnCurrentThread()
        {
            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】消息循环线程开始执行");
            foreach (var item in queue.GetConsumingEnumerable())
            {
                item.Callback(item.State);//执行回调
                item.Mre?.Set();//设置事件为已完成状态
            }   
        }

        public void Complete()
        {
            queue.CompleteAdding();//完成添加
        }
    }
}
