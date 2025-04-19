/*【211：异步编程】
 * 异步编程模式概述
	• .NET提供了执行异步操作的三种模式
		○ 基于任务的异步模式（TAP）
			§ 该模式使用单一方法表示异步操作的开始和完成
			§ TAP是在.NET Framework 4中引入的
			§ TAP是在.NET中进行异步编程的推荐方法
			§ C#中的async和await关键词为TAP添加了语言支持
		○ 基于事件的异步模式（EAP）
			§ 该模式是提供异步行为的基于事件的旧模型
			§ 该模式需要使用
				□ 后缀为Async的方法
				□ 一个或多个事件委托类型
				□ EventArgs派生类型
				□ 事件处理程序
			§ 该模式是在.NET Framework 2.0中引入
				□ 新开发中建议不再使用这种模式
		○ 异步编程模型（APM）模式
			§ 该模式也称IAsyncResult模式
				□ 这是使用IAsyncResult接口提供异步行为的旧模型
			§ 该模式下同步操作需要Begin和End方法
				□ 例如BeginWrite和EndWrite以实现异步写入操作
			§ 不建议新的开发使用此模式

 * 基于任务的异步模式（TAP）
	• TAP模型提供了异步代码的抽象化
		○ 用户只需如同写同步编程的代码一样编写一连串语句即可
		○ 如同每条语句在下一句之前完成一样，代码的可读性将得到很好地维持
		○ 编译器将执行许多转换，因为其中一些语句可能会开始运行并返回表示正在进行的工作的Task
	• TAP使得编写异步代码支持读起来像一连串语句的代码
		○ 这些代码会根据外部资源分配和任务完成时间以更复杂的顺序执行
			§ 类似于为包含异步任务的流程给予指令的方式
	• TAP模型概述
		○ TAP异步编程的核心是Task和Task<T>对象
			§ 这两个对象对异步操作建模
			§ 它们受关键字async和await的支持
				□ async和await关键词为TAP添加了语言级别的异步编程支持
		○ 异步操作建模
			§ 对于I/O绑定代码，等待一个在async方法中返回Task或Task<T>的操作
			§ 对于CPU绑定代码，等待一个使用Task.Run方法在后台线程启动的操作
		○ async关键字
			§ 用于声明一个方法或lambda表达式是异步方法
			§ 在异步方法内部才能使用await关键字
		○ await关键字
			§ 它将控制权交给执行await方法的调用方
			§ 它最终允许UI具有响应性或服务具有灵活性
		○ 内部原理
			§ 在C#方面，编译器将代码转换为状态机，它将跟踪类似以下内容
				□ 到达await时暂停执行以及后台作业完成时继续执行
			§ 从理论上讲，这是 异步的承诺模型 的实现
		○ 识别CPU绑定和I/O绑定工作
			§ 如果代码会“等待”某些内容，例如数据库中的数据
				□ 则代码是执行I/O绑定工作
			§ 如果代码要执行开销巨大的计算
				□ 则代码是执行CPU绑定工作
			§ 根据不同情况选择异步编程方式
				□ I/O绑定工作
					® 使用async和await关键字进行异步方法编写
					® 不要使用Task.Run
					® 不要使用任务并行库（TPL）
				□ CPU绑定工作
					® 使用async和await关键字进行异步方法编写，但在另一个线程上使用Task.Run生成工作
					® 如果该工作同时适用于并发和并行，还应考虑使用任务并行库（TPL）
				□ 其他说明
					® 应始终对代码的执行进行测量，以便选择正确的折衷方案来进行异步编程
						◊ 例如需要考虑多线程处理时，上下文切换的开销高于CPU绑定工作的开销
					® 关于TPL知识，参考 并行编程 部分
		○ TAP模型异步编程细节建议
			§ async方法需要在主体中有await关键字，否则它们将永不暂停
				□ 如果async方法中不存在await关键字，则该方法的代码会以类似同步方法的方式进行编译和运行
				□ 这种方式非常低效，因为由C#编译器为异步方法生成的状态机将不会完成任何任务
			§ 添加Async作为编写的每个异步方法的名称的后缀
				□ 这是.NET中的惯例，以便轻松的区分同步和异步方法
				□ 该规则对未由代码显示调用的某些方法（如事件处理器方法或Web控制器方法）并不一定使用
			§ async void应仅用于事件处理程序
				□ 这是允许异步事件处理程序工作的唯一方法，因为事件不具有返回类型
					® 因此无法利用Task和Task<T>
				□ 其他任何情况对async void的使用都不遵循TAP模型，且可能存在一定使用难度
					® async void方法中引发的异常无法在该方法外部被捕获
					® async void方法很难测试
					® async void方法可能会导致不良副作用
						◊ 如果调用方不希望方法是异步的话
			§ 在LINQ表达式中使用异步Lambda时请谨慎
				□ LINQ中的Lambda表达式使用延迟执行
					® 代码可能在用户并不希望结束的时候停止执行
					® 编写不正确的代码，在将阻塞任务引入其中时可能和容易导致死锁
				□ 异步代码嵌套可能会对推断代码的执行带来更多困难
				□ Async和LINQ的功能都十分强大，但在结合使用两者时应尽可能小心
			§ 采用非阻止方式编写等待任务的代码
				□ 通过阻止当前线程来等待Task完成的方法可能导致死锁和已阻止的上下文线程，且可能需要更复杂的错误处理方法
				□ 非阻止方式处理等待任务的指南
					® 若要执行此操作	    使用以下方式	            而不是使用
					  检索后台任务的结果	await	                Task.Wait 或 Task.Result
					  等待任何任务完成	    await Task.WhenAny	    Task.WaitAny
					  等待所有任务完成	    await Task.WhenAll	    Task.WaitAll
					  等待一段时间	        await Task.Delay	    Tread.Sleep
			§ 如果可能，考虑使用ValueTask
			§ 考虑使用ConfigureAwait(false)
			§ 编写状态欠缺的代码
		○ 注意事项
			§ 异步代码可用于I/O绑定和CPU绑定代码，但在每个方案中有所不同
			§ 异步代码使用Task<T>和Task，它们是对后台所完成的工作进行建模的结构
			§ async关键字将方法转换为异步方法，然后可在方法主体使用await关键字
			§ 应用await关键字后，它将挂起调用方法，并将控制权返还给调用方，直到等待的任务完成
			§ 仅允许在异步方法中使用await
 */

using LearnCSharp.Professional.LeanrAsyncProgrammingSpace;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace LearnCSharp.Professional
{
    class LearnAsyncProgramming
    {
        private static event EventHandler? PrintNumberEvent;
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

        /*【21101：基于委托的异步编程】
            异步编程模型（APM）模式
			§ 该模式也称IAsyncResult模式
				□ 这是使用IAsyncResult接口提供异步行为的旧模型
			§ 该模式下同步操作需要Begin和End方法
				□ 例如BeginWrite和EndWrite以实现异步写入操作
			§ 不建议新的开发使用此模式

            注意：本例代码仅用于演示异步编程模型（APM）模式，在.NET5中已不推荐使用，下列代码可通过编译，但在运行时会抛出异常：PlatformNotSupportedException
        */
        public static void LearnAsyncDelegate()
        {
            Console.WriteLine("\n------示例：基于委托的异步编程------\n");

            int maxNumber = 20;

            //创建一个委托方法
            int CalculateNumber(int maxNumber)
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务开始执行");
                int result = 0;
                for (int i = 1; i <= maxNumber; i++)
                {
                    Thread.Sleep(300);

                    result += i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前输出结果：{result}");
                    Console.ResetColor();
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务结束执行");
                return result;
            }

            Thread current = Thread.CurrentThread;
            current.Name = $"主线程 {current.ManagedThreadId:00}";
            Console.WriteLine("》》》主线程信息《《《");
            ShowThreadInfo(current);

            Func<int,int> func = CalculateNumber;//创建委托实例

            try 
            {
                IAsyncResult asyncResult = func.BeginInvoke(maxNumber, null, null);//异步调用委托方法，传入参数，回调方法和状态参数为null

                Console.WriteLine("主线程持续执行");

                //轮询确认异步调用是否完成
                while (!asyncResult.IsCompleted)
                {
                    Console.WriteLine($"异步任务持续执行");
                    Thread.Sleep(100);
                }

                int result = func.EndInvoke(asyncResult);//获取异步调用的结果

                Console.WriteLine($"异步任务执行完成，获取计算结果：{result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"捕获异常：{ex.Message}");
            }

            Console.WriteLine("异步任务完成执行！");
        }

        /*【21102：Task】*/
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

        /*【21103：异步方法】*/
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
                    await Task.Delay(delayTime);

                    result += i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前输出结果：{result}");
                    Console.ResetColor();
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务结束执行");
                return result;
            }

            async Task PrintResultAsync()
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

            PrintResultAsync();//异步调用PrintResultAsync方法，但未使用await关键字，所以不会等待任务完成

            Console.WriteLine("主线程持续运行");
            Thread.Sleep(maxNumber * delayTime + 1000);
        }

        /*【21104：同步上下文】*/
        public static void LearnSynchronizationContext()
        {
            Console.WriteLine("\n------示例：同步上下文------\n");

            int maxNumber = 20;

            async Task<int> CalculateNumberAsync()
            {
                int result = 0;
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务开始执行");
                //int result = 0;
                for (int i = 1; i <= maxNumber; i++)
                {
                    await Task.Delay(100);
                    result += i;

                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前输出结果：{result}");
                    Console.ResetColor();
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务结束执行");
                return result;
            }

            async Task PrintResultAsync()
            {
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
            SynchronizationContext.SetSynchronizationContext(null);//清除当前线程的同步上下文
        }

        /*【21105：ConfigureAwait】*/
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
                    await Task.Delay(100).ConfigureAwait(false);//使用ConfigureAwait(false)配置上下文,后续代码不会在原线程中执行
                    result += i;

                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前输出结果：{result}");
                    Console.ResetColor();
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
            SynchronizationContext.SetSynchronizationContext(null);//清除当前线程的同步上下文
        }

        /*【21106：一发即忘---async void】
         * 通常不建议使用async void方法，除非是事件处理程序
         * async void方法不能被await关键字等待
         * async void方法不能返回Task对象
         * async void方法不能被调用者捕获异常
         */
        public static void LearnAsyncVoid()
        {
            Console.WriteLine("\n------示例：async void------\n");

            int result = 0;
            int maxNumber = 30;

            async void PrintNumberAsync(object sender,EventArgs e)
            {
                string name = $"线程 {Thread.CurrentThread.ManagedThreadId:00}";
                Console.WriteLine($"【{name}】输出任务开始执行");
                for (int i = 0; i <= maxNumber; i++)
                {
                    await Task.Delay(200).ConfigureAwait(false);
                    result = i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【{name}】输出数字{result:000}");
                    Console.ResetColor();
                }
                Console.WriteLine($"【{name}】输出任务结束执行");

                //抛出异常测试，如果取消注释下面的代码，主线程无法捕获到异常
                //throw new Exception("异常测试");
            }

            Console.WriteLine($"》》》创建一个async void方法并将其注册到PrintNumberEvent事件，通过事件调用该异步方法《《《");
            Console.WriteLine("-----------------------------------------------");

            //尝试捕获异步void方法中的异常，实际上无法捕获
            try
            {
                PrintNumberEvent += PrintNumberAsync;//注册事件处理程序
                PrintNumberEvent.Invoke(null, EventArgs.Empty);//调用事件处理程序
            }
            catch (Exception ex)
            {
                Console.WriteLine($"捕获异常：{ex.Message}");
            }

            Console.WriteLine("主线程持续执行");
            while (true)
            {
                Console.WriteLine($"【主线程 {Thread.CurrentThread.ManagedThreadId:00}】获取数字{result:000}");
                Thread.Sleep(100);
                if (result == maxNumber)
                    break;
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine( );
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
            using var mre = new ManualResetEvent(false);//创建一个手动重置事件
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
