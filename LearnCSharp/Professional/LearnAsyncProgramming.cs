/*【211：异步编程】
 * 异步编程概述
	• 同步编程
		○ 了解同步编程
			§ 程序将按照用户写好的固定的代码严格按顺序依次执行每一条语句
			§ 在执行顺序中的下一条语句总要等之前的语句依次执行完才能继续执行
			§ 同步编程的方法都是先完成其工作再返回给调用者
		○ 同步编程的优点
			§ 代码总是按照用户书写规定的顺序执行
			§ 代码的可读性较高，且逻辑上也易于理解
		○ 同步编程的缺点
			§ 只适合用于现实生活中逻辑上只能一步一步进行的行为的抽象
			§ 可能会程序运行过程中出现线程阻塞导致程序长时间无响应
				□ 比如当代码正在执行大量的文件读取扫描或下载大容量文件时，如果该步骤未完成，接下来的代码都无法得到执行
		○ 我们平常编写和调用的大多数方法都是同步方法
	• 异步编程
		○ 了解异步编程
			§ 异步编程即以异步的方式编写运行时间很长或者可能很长的函数
				□ 该函数会在新的线程或任务上被调用，从而实现所需的并发性
				□ 异步方法的不同点在于并发性是在长时间运行的方法内启动的，而不是从这个方法外启动的
					® 优点一：I/O密集的并发性的实现不需要绑定线程，因此可以提高可伸缩性和效率
					® 优点二：简化富客户端应用程序的线程安全性
			§ 使用异步编程可以避免性能瓶颈并增强应用程序的总体响应能力
			§ 程序代码不一定按照编写时的顺序严格执行
				□ 当代码执行到需要进行长时间操作的代码时，异步实现可能会进行以下实现
					® 程序可能会新建一个线程来执行耗时的操作代码
						◊ 例如在拥有多个物理核心的计算机中运行程序，程序可以在不同核心处理器上执行任务
					® 程序可能会改变代码的执行顺序来充分利用单个线程的能力
						◊ 例如当只有一个核心处理器时，操作系统通过“时间分片”的方式来模拟多个核心
			§ 异步表示代码可以分开执行，一段代码的执行不会导致另一端代码的执行被终止或阻塞
				□ 异步代码不一定是同时执行，也不一定是在多线程上进行执行
			§ 异步编程是并行编程或并发编程的基础
			§ 异步编程的方法大部分工作都是在返回给调用者之后才完成的
		○ 异步编程的场景
			§ 如果需要大量的I/O绑定或者CPU绑定，异步编程是一个很好地方案
				□ I/O绑定：关于数据的读写，例如网络请求数据、访问数据库等
				□ CPU绑定：对数据的计算，例如执行成本高昂的计算
		○ 异步编程的优点
			§ 可以很大程度的避免线程阻塞
			§ 可以很大程度的提升程序运行效率
			§ 可以更灵活的执行需要同时处理多个任务的代码
		○ 异步编程的缺点
			§ 代码的执行过程不可控
			§ 异步代码同时执行对同一资源的访问时可能造成资源争夺
			§ 异步代码的可读性不一定好，在逻辑上也不一定易于理解
            § 异步编程的传统技术可能比较复杂，且代码可能难以编写、调试和维护

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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Timers;

namespace LearnCSharp.Professional
{
    class LearnAsyncProgramming
    {
        private static readonly object objLock = new object();
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

        /*【21102：基于事件的异步编程】*/
        public static void LearnAsyncByEvent()
        {
            Console.WriteLine("\n------示例：基于事件的异步编程------\n");
            //这里的代码用于演示基于事件的异步编程（EAP）模式，关于该模式的使用不建议在新的开发中使用
            //EAP类的实现代码位于本页代码底部的LearnAsyncByEventSpace命名空间中

            int maxNumber = 20;
            var calculator = new CalculateInteger();//创建一个Calculator对象

            using ManualResetEvent mre = new ManualResetEvent(false);//创建一个手动重置事件，用于等待异步操作完成

            calculator.CalculateIntegerCompleted += (sender, e) =>
            {
                if (e.Cancelled)
                {
                    Console.WriteLine("操作已取消");
                }
                else if (e.Error != null)
                {
                    Console.WriteLine($"操作发生异常：{e.Error.Message}");
                }
                else
                {
                    Console.WriteLine($"异步操作完成，{maxNumber}的阶乘计算结果：{e.Result}");
                }
                mre.Set();//设置手动重置事件为已终止状态
            };

            calculator.CalculateIntegerCancelled += (sender, e) =>
            {
                Console.WriteLine("收到取消请求……");
            };

            calculator.CalculateIntegerProgressChanged += (sender, e) =>
            {
                Console.WriteLine($"异步操作进度：{e.CurrentProgress}% | 当前值：{e.CurrentValue}");
            };

            Thread current = Thread.CurrentThread;
            current.Name = $"主线程 {current.ManagedThreadId:00}";
            Console.WriteLine("》》》主线程信息《《《");
            ShowThreadInfo(current);

            Console.WriteLine($"》》》开始计算{maxNumber}的阶乘(同步方法)《《《");
            Console.WriteLine("----------------------------------------------");

            long result = calculator.CalculateFactorial(maxNumber);//同步调用计算阶乘方法

            Console.WriteLine($"计算完成，{maxNumber}的阶乘计算结果：{result}");
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine();

            Console.WriteLine($"》》》开始计算{maxNumber}的阶乘(EAP异步)《《《");
            Console.WriteLine("----------------------------------------------");

            calculator.CalculateFactorialAsync(maxNumber);//异步调用计算阶乘方法
            mre.WaitOne();//等待异步操作完成
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();

            mre.Reset();//重置手动重置事件
            Console.WriteLine($"》》》开始计算{maxNumber}的阶乘，中途强制取消(EAP异步)《《《");
            Console.WriteLine("----------------------------------------------");

            calculator.CalculateFactorialAsync(maxNumber);//异步调用计算阶乘方法
            Thread.Sleep(3000);//等待2秒钟
            calculator.CancelAsync();//取消异步操作
            mre.WaitOne();//等待异步操作完成
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
        }

        /*【21103：初识Task】*/
        public static void LearnTask()
        {
            Console.WriteLine("\n------示例：Task任务------\n");

            int printMaxNumber = 20;
            int delayTime = 200;

            void PrintNumber()
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出任务开始执行");
                for (int i = 0; i <= printMaxNumber; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出数字{i:000}");
                    Console.ResetColor();
                    Task.Delay(delayTime).Wait();
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出任务结束执行");
            }

            async Task PrintNumberAsync()
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出任务开始执行");

                for (int i = 0; i <= printMaxNumber; i++)
                {
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出数字{i:000}");
                    Console.ResetColor();
                    await Task.Delay(delayTime);
                }

                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出任务结束执行");
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

        /*【21104：异步方法】*/
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

        /*【21105：同步上下文】*/
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

        /*【21106：ConfigureAwait】*/
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

        /*【21107：一发即忘---async void】
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
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出任务开始执行");
                for (int i = 0; i <= maxNumber; i++)
                {
                    await Task.Delay(200).ConfigureAwait(false);
                    result = i;
                    Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                    Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出数字{result:000}");
                    Console.ResetColor();
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出任务结束执行");

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

        /*【21108：多任务】*/
        public static void LearnMultiTask()
        {
            Console.WriteLine("\n------示例：多任务的实现------\n");

            async Task<long> CalculateNumber(int taskId, int maxNumber, Func<int,long,long> func)
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务 {taskId:00} 开始执行");
                long result = 0;
                for (int i = 1; i <= maxNumber; i++)
                {
                    await Task.Delay(300);
                    //result += i;
                    lock (objLock)
                    {
                        result = func.Invoke(i, result);
                        Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务 {taskId:00} 执行第{i}次计算，当前输出结果：{result}");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】计算任务 {taskId:00} 结束执行");
                return result;
            }

            Func<int, long, long> funcAdd = (i, r) => r + i;//计算整数顺序累加求和的委托
            Func<int, long, long> funcMul = (i, r) => r == 0 ? 1 : r * i;//计算整数阶乘的委托

            Thread current = Thread.CurrentThread;
            current.Name = $"主线程";
            Console.WriteLine("》》》主线程信息《《《");
            ShowThreadInfo(Thread.CurrentThread);

            Console.WriteLine("》》》创建多个任务用于计算数字（方案一）《《《");
            Console.WriteLine("-----------------------------------------------");

            Task<long> task1 = CalculateNumber(1, 10, funcAdd);
            Task<long> task2 = CalculateNumber(2, 10, funcMul);
            Task<long> task3 = CalculateNumber(3, 20, funcAdd);
            Task<long> task4 = CalculateNumber(4, 20, funcMul);

            long[] results = Task.WhenAll(task1, task2, task3, task4).Result;

            Console.WriteLine($"任务1计算结果：{results[0]}");
            Console.WriteLine($"任务2计算结果：{results[1]}");
            Console.WriteLine($"任务3计算结果：{results[2]}");
            Console.WriteLine($"任务4计算结果：{results[3]}");

            Console.WriteLine("主线程持续执行");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            Console.WriteLine("》》》创建多个任务用于计算数字（方案二）《《《");
            Console.WriteLine("-----------------------------------------------");

            List<Task<long>> tasks = new List<Task<long>>()
            {
                CalculateNumber(1, 10, funcAdd),
                CalculateNumber(2, 10, funcMul),
                CalculateNumber(3, 20, funcAdd),
                CalculateNumber(4, 20, funcMul)
            };

            results = Task.WhenAll(tasks).Result;

            Console.WriteLine($"任务1计算结果：{results[0]}");
            Console.WriteLine($"任务2计算结果：{results[1]}");
            Console.WriteLine($"任务3计算结果：{results[2]}");
            Console.WriteLine($"任务4计算结果：{results[3]}");

            Console.WriteLine("主线程持续执行");
            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【21109：任务的取消】*/
        public static void LearnCancelTask()
        {
            Console.WriteLine("\n------示例：取消任务的实现------\n");

            int participantCount = 10;//参与者数量
            bool isGameEnding = false;//比赛是否结束

            using CancellationTokenSource cts = new CancellationTokenSource();//创建一个取消令牌源
            CancellationToken cancellationToken = cts.Token;//获取取消令牌

            //模拟选手比赛进度
            //这里采用了异步方法来模拟选手的比赛进度，异步方法内异常捕获是为了模拟选手中断失败的进度，且该异常可以正常传递到调用任务的主线程
            //不建议使用同步方法来模拟选手的比赛进度，因为将同步方法放入任务中可能会导致异常捕捉出问题而导致程序崩溃（因为任务可能会在不同的线程中执行）
            async Task RunAsync(int id, int lineNumber, CancellationToken cancellationToken)
            {  
                var name = $"选手 {id:00}";//任务名称，表示选手编号
                int sleepTime = Random.Shared.Next(100, 500);//随机睡眠时间

                for (int i = 0; i <= 50; i++)
                {
                    lock (objLock)
                    {
                        cancellationToken.ThrowIfCancellationRequested();//检查是否请求取消

                        Console.SetCursorPosition(0, lineNumber);
                        Console.ForegroundColor = (ConsoleColor)(lineNumber % 16);
                        Console.Write($"{name}: ");
                        Console.ResetColor();
                        Console.Write("[");
                        Console.Write(new string('=', i));
                        Console.SetCursorPosition(Console.GetCursorPosition().Left, lineNumber);
                        Console.Write("]");
                        Console.ForegroundColor = (ConsoleColor)(lineNumber % 16);
                        Console.Write($"{i * 2}%");

                        if (i == 50)
                        {
                            Console.Write($" 【{name}】获得胜利");
                            isGameEnding = true;//比赛结束
                        }

                        Console.ResetColor();
                    }
                    await Task.Delay(sleepTime);
                }
            }

            Console.WriteLine($"》》》创建{participantCount}个任务表示{participantCount}个选手的比赛过程《《《");
            Console.WriteLine("-----------------------------------------------");

            Console.Write("请输入参赛选手人数，如不输入默认为10：");

            string? input = Console.ReadLine();
            if (int.TryParse(input, out int count))
            {
                participantCount = count;
            }
            else
            {
                Console.WriteLine("输入错误，使用默认值10");
            }

            Console.WriteLine($"当前参赛选手信息：{participantCount}");

            List<Task> tasks = new List<Task>(participantCount);//创建任务列表

            Console.WriteLine($"共计{participantCount}名选手参加比赛，按任意键开始！");
            Console.ReadKey(true);//等待用户输入

            Console.Clear();
            Console.CursorVisible = false;//隐藏光标

            Console.WriteLine("》》》比赛开始《《《");
            Console.WriteLine("-----------------------------------------------");

            Thread.Sleep(1000);

            //创建一个任务用于出现胜利者时取消其他选手的比赛进度
            Task cancelTask = Task.Run(() =>
            {
                while (true)
                {
                    if (isGameEnding)
                    {
                        cts.Cancel();
                        break;
                    }
                }
            });

            //创建多个任务表示选手的比赛进度
            for (int i = 0; i < participantCount; i++)
            {
                int id = i + 1;
                tasks.Add(Task.Run(() => RunAsync(id, id + 1, cancellationToken)));//创建比赛任务并添加到列表中
            }

            try
            {
                Task.WaitAll(tasks);
            }
            catch
            {
                //这里捕获到异常是因为任务被取消了
                //这里不写异常处理代码的原因是我们使用RunAsync方法模拟比赛进程，内部异常是用于模拟中断失败选手的进度，故不用处理
            }
            finally
            {
                cancelTask.Wait();
            }

            Console.SetCursorPosition(0, participantCount + 2);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("比赛完成！");
            Console.CursorVisible = true;//显示光标

            //因为上述代码中cts.Cancel()方法会导致所有任务都被标记取消，所以这里的代码不会被执行
            Task.Run(() => Console.WriteLine("永远不会在运行后看到！"), cancellationToken);

            Console.WriteLine();
        }

        /*【21110：WhenAll、WhenAny、ContinueWith】*/
        public static void LearnWhenAllWhenAnyAndContinueWith()
        {
            Console.WriteLine("\n------示例：WhenAll、WhenAny、ContinueWith------\n");

            int participantCount = 10;//参与者数量

            using CancellationTokenSource cts = new CancellationTokenSource();//创建一个取消令牌源
            CancellationToken cancellationToken = cts.Token;//获取取消令牌

            //模拟选手比赛进度
            //这里采用了异步方法来模拟选手的比赛进度，异步方法内异常捕获是为了模拟选手中断失败的进度，且该异常可以正常传递到调用任务的主线程
            //不建议使用同步方法来模拟选手的比赛进度，因为将同步方法放入任务中可能会导致异常捕捉出问题而导致程序崩溃（因为任务可能会在不同的线程中执行，或者同步方法内部未处理异常）
            async Task RunAsync(int id, int lineNumber, CancellationToken cancellationToken)
            {
                var name = $"选手 {id:00}";//任务名称，表示选手编号
                int sleepTime = Random.Shared.Next(100, 500);//随机睡眠时间

                for (int i = 0; i <= 50; i++)
                {
                    lock (objLock)
                    {
                        cancellationToken.ThrowIfCancellationRequested();//检查是否请求取消

                        Console.SetCursorPosition(0, lineNumber);
                        Console.ForegroundColor = (ConsoleColor)(lineNumber % 16);
                        Console.Write($"{name}: ");
                        Console.ResetColor();
                        Console.Write("[");
                        Console.Write(new string('=', i));
                        Console.SetCursorPosition(Console.GetCursorPosition().Left, lineNumber);
                        Console.Write("]");
                        Console.ForegroundColor = (ConsoleColor)(lineNumber % 16);
                        Console.Write($"{i * 2}%");

                        if (i == 50)
                        {
                            Console.Write($" 【{name}】获得胜利");
                        }

                        Console.ResetColor();
                    }
                    await Task.Delay(sleepTime);
                }
            }

            Console.WriteLine($"》》》创建{participantCount}个任务表示{participantCount}个选手的比赛过程《《《");
            Console.WriteLine("-----------------------------------------------");

            Console.Write("请输入参赛选手人数，如不输入默认为10：");

            string? input = Console.ReadLine();
            if (int.TryParse(input, out int count))
            {
                participantCount = count;
            }
            else
            {
                Console.WriteLine("输入错误，使用默认值10");
            }

            Console.WriteLine($"当前参赛选手信息：{participantCount}");

            List<Task> tasks = new List<Task>(participantCount);//创建任务列表

            Console.WriteLine($"共计{participantCount}名选手参加比赛，按任意键开始！");
            Console.ReadKey(true);//等待用户输入

            Console.Clear();
            Console.CursorVisible = false;//隐藏光标

            Console.WriteLine("》》》比赛开始《《《");
            Console.WriteLine("-----------------------------------------------");

            Thread.Sleep(1000);

            //创建多个任务表示选手的比赛进度
            for (int i = 0; i < participantCount; i++)
            {
                int id = i + 1;
                tasks.Add(Task.Run(() => RunAsync(id, id + 1, cancellationToken)));//创建比赛任务并添加到列表中
            }

            try
            {
                //WhenAny方法会在第一个任务完成时返回该任务
                //ContinueWith方法会在第一个任务完成后继续执行延续任务
                Task.WhenAny(tasks).ContinueWith(_ => cts.Cancel()).Wait();
            }
            catch
            {
                //这里捕获到异常是因为任务被取消了
                //这里不写异常处理代码的原因是我们使用RunAsync方法模拟比赛进程，内部异常是用于模拟中断失败选手的进度，故不用处理
            }

            //WhenAll方法会在所有任务完成后返回
            //ContinueWith方法会在所有任务完成后继续执行延续任务
            Task.WhenAll(tasks).ContinueWith(_ =>
            {
                Console.SetCursorPosition(0, participantCount + 2);

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("比赛完成！");
            }).Wait();


            Console.CursorVisible = true;//显示光标

            Console.WriteLine();
        }

        /*【21111：计时器】*/
        public static void LearnTimer()
        {
            Console.WriteLine("\n------示例：计时器------\n");
            
            int breakLoopCount = 0;//循环次数
            DateTime time = DateTime.Now;

            Console.WriteLine($"》》》创建一个计时器(System.Timers.Timer)并注册事件处理程序，并激活该事件《《《");
            Console.WriteLine("-----------------------------------------------");

            var cursorPosition = Console.GetCursorPosition();
            Console.CursorVisible = false;//隐藏光标

            var line1 = Console.CursorTop;
            var line2 = Console.CursorTop + 1;

            using var sysTimer = new System.Timers.Timer(1000);//创建一个计时器
            sysTimer.AutoReset = true;//设置计时器是否自动重置

            //System.Timers.Timer的Elapsed事件会在计时器到达指定时间间隔时触发，且该事件会在计时器的线程池线程中执行
            sysTimer.Elapsed += async(s,e)=> 
            {
                await Task.Delay(0);

                lock(objLock)
                {
                    time = DateTime.Now;
                    Console.SetCursorPosition(0, line1);
                    Console.WriteLine($"计时器---【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取时间：{time}");
                }
            };//注册事件处理程序

            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出时间：{time}");

            sysTimer.Start();//开始计时器

            while (breakLoopCount<=100)
            {
                lock(objLock)
                {
                    Console.SetCursorPosition(0, line2);
                    Console.WriteLine($"主线程---【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出时间：{time}");
                }

                breakLoopCount++;
                Thread.Sleep(200);
            }

            sysTimer.Stop();//停止计时器
            breakLoopCount = 0;

            Console.SetCursorPosition(0, line2 + 1);
            Console.WriteLine("-----------------------------------------------");
            Console.CursorVisible = true;//显示光标
            Console.WriteLine();

            Console.WriteLine($"》》》创建一个计时器(System.Threading.Timer)并添加回调执行的计时任务，并开始计时工作《《《");
            Console.WriteLine("-----------------------------------------------");

            cursorPosition = Console.GetCursorPosition();
            Console.CursorVisible = false;//隐藏光标

            line1 = Console.CursorTop;
            line2 = Console.CursorTop + 1;


            //System.Threading.Timer的回调方法会在计时器到达指定时间间隔时触发，且该事件会在计时器的线程池线程中执行
            using var threadTimer = new System.Threading.Timer(async _ =>
                {
                    await Task.Delay(0);
                    lock (objLock)
                    {
                        time = DateTime.Now;
                        Console.SetCursorPosition(0, line1);
                        Console.WriteLine($"计时器---【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取时间：{time}");
                    }
                }, null, 0, 1000);//设置计时器的回调方法、状态对象、首次执行延迟时间和执行间隔时间

            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出时间：{time}");

            while (breakLoopCount<=100)
            {
                lock (objLock)
                {
                    Console.SetCursorPosition(0, line2);
                    Console.WriteLine($"主线程---【线程 {Thread.CurrentThread.ManagedThreadId:00}】输出时间：{time}");
                }
                
                breakLoopCount++;
                Thread.Sleep(200);
            }

            threadTimer.Change(Timeout.Infinite, Timeout.Infinite);//停止计时器
            breakLoopCount = 0;

            Console.SetCursorPosition(0, line2 + 1);
            Console.WriteLine("-----------------------------------------------");
            Console.CursorVisible = true;//显示光标
            Console.WriteLine();

            Console.WriteLine($"》》》创建一个计时器(Stopwatch)并开始监测代码运行耗时《《《");
            Console.WriteLine("-----------------------------------------------");

            //创建一个Stopwatch对象
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("开始耗时工作并计时：");

            stopwatch.Start();//开始计时

            while(breakLoopCount<=100)
            {
                Console.Write($"\r【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取时间：{DateTime.Now}");
                Thread.Sleep(200);
                breakLoopCount++;
            }

            Console.WriteLine();

            stopwatch.Stop();//停止计时

            Console.WriteLine($"耗时工作完毕，用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        public static void StartLearnAsyncProgramming()
        {
            string title = "001 APM异步编程代码示例\n" +
                "002 EAP异步编程代码示例\n" +
                "003 初识Task\n" +
                "004 异步方法\n" +
                "005 同步上下文\n" +
                "006 ConfigureAwait\n" +
                "007 异步void方法\n" +
                "008 多任务\n" +
                "009 取消任务\n" +
                "010 WhenAll、WhenAny、ContinueWith\n" +
                "011 计时器\n" +
                "012 \n" +
                "013 ";


            do
            {
                Console.WriteLine("【进程与线程】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnAsyncDelegate(); break;
                    case "002": LearnAsyncByEvent(); break;
                    case "003": LearnTask(); break;
                    case "004": LearnAsyncMethod(); break;
                    case "005": LearnSynchronizationContext(); break;
                    case "006": LearnConfigureAwait(); break;
                    case "007": LearnAsyncVoid(); break;
                    case "008": LearnMultiTask(); break;
                    case "009": LearnCancelTask(); break;
                    case "010": LearnWhenAllWhenAnyAndContinueWith(); break;
                    case "011": LearnTimer(); break;
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
    #region 【21102：基于事件的异步编程】定义一系列事件和类来实现基于事件的异步编程
    class CalculateIntegerCompelledEventArgs : AsyncCompletedEventArgs
    {
        public long Result { get; }

        public CalculateIntegerCompelledEventArgs(long result, Exception error, bool cancelled, object? userState) : base(error, cancelled, userState)
        {
            Result = result;
        }
    }

    class CalculateIntegerProgressReportEventArgs : ProgressChangedEventArgs
    {
        public int CurrentProgress { get; }
        public long CurrentValue { get; }
        public CalculateIntegerProgressReportEventArgs(int progressPercentage, long currentValue, object? userState) : base(progressPercentage, userState)
        {
            CurrentProgress = progressPercentage;
            CurrentValue = currentValue;
        }
    }

    delegate void CalculateIntegerCompelledEventHandler(object sender, CalculateIntegerCompelledEventArgs e);//定义事件委托
    delegate void CalculateIntegerProgressReportEventHandler(object sender, CalculateIntegerProgressReportEventArgs e);//定义进度事件委托

    class CalculateInteger
    {
        private readonly object objLock = new object();//锁对象
        private CancellationTokenSource cts;

        public event CalculateIntegerCompelledEventHandler? CalculateIntegerCompleted;//定义事件
        public event CalculateIntegerProgressReportEventHandler? CalculateIntegerProgressChanged;//定义进度事件
        public event EventHandler? CalculateIntegerCancelled;//定义进度事件

        public bool IsBusy
        {
            get
            {
                lock (objLock)
                {
                    return cts != null && !cts.IsCancellationRequested;
                }
            }
        }

        /// <summary>
        /// 用于异步计算阶乘的计算方法
        /// </summary>
        /// <param name="maxNumber"></param>
        /// <param name="ct"></param>
        /// <param name="userState"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private long CalculateFactorial(int maxNumber, CancellationToken ct, object userState)
        {
            if (maxNumber < 0)
                throw new ArgumentException(nameof(maxNumber), "参数不能小于0");

            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】开始执行{maxNumber}的阶乘计算");
            long result = 1;

            for (int i = 1; i <= maxNumber; i++)
            {
                ct.ThrowIfCancellationRequested();//检查是否取消请求

                result *= i;

                Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算");
                Console.ResetColor();

                int progressPercentage = (int)Math.Round((double)i / maxNumber * 100);
                OnCalculateIntegerProgressChanged(new CalculateIntegerProgressReportEventArgs(progressPercentage, result, userState));//触发进度事件
                Thread.Sleep(200);//模拟计算延迟
            }

            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】完成执行{maxNumber}的阶乘计算");
            
            return result;
        }

        /// <summary>
        /// 同步计算阶乘的方法
        /// </summary>
        /// <param name="maxNumber"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public long CalculateFactorial(int maxNumber)
        {
            if (maxNumber < 0)
                throw new ArgumentException(nameof(maxNumber), "参数不能小于0");

            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】开始执行{maxNumber}的阶乘计算");
            long result = 1;

            for (int i = 1; i <= maxNumber; i++)
            {
                result *= i;

                Console.ForegroundColor = (ConsoleColor)(Thread.CurrentThread.ManagedThreadId % 16);
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】执行第{i}次计算，当前结果：{result}");
                Console.ResetColor();

                Thread.Sleep(200);//模拟计算延迟
            }

            Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】完成执行{maxNumber}的阶乘计算");

            return result;
        }

        /// <summary>
        /// 异步计算阶乘的方法
        /// </summary>
        /// <param name="maxNumber"></param>
        /// <param name="userState"></param>
        public void CalculateFactorialAsync(int maxNumber, object? userState = null)
        {
            lock (objLock)
            {
                if (IsBusy)
                    throw new InvalidOperationException("当前正在执行计算任务，请稍后再试");
                cts = new CancellationTokenSource();
            }

            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    long result = CalculateFactorial(maxNumber, cts.Token, userState);//调用计算方法
                    OnCalculateIntegerCompleted(new CalculateIntegerCompelledEventArgs(result, null, false, userState));//触发完成事件
                }
                catch (OperationCanceledException)
                {
                    OnCalculateIntegerCancelled(EventArgs.Empty);//触发取消事件
                    OnCalculateIntegerCompleted(new CalculateIntegerCompelledEventArgs(0, null, true, userState));//触发完成事件
                }
                catch (Exception ex)
                {
                    OnCalculateIntegerCompleted(new CalculateIntegerCompelledEventArgs(0, ex, false, userState));//触发异常事件
                }
                finally
                {
                    lock (objLock)
                    {
                        cts.Dispose();
                        cts = null;
                    }
                }
            });
        }

        public void CancelAsync()
        {
            lock (objLock)
            {
                if (IsBusy)
                {
                    cts.Cancel();
                }
            }
        }

        protected void OnCalculateIntegerCompleted(CalculateIntegerCompelledEventArgs e)
        {
            var handler = CalculateIntegerCompleted;//复制事件引用以避免多线程竞争问题
            
            if(handler != null)
            {
                SyncContextInvoke(() => handler(this, e));
            }
        }

        protected void OnCalculateIntegerProgressChanged(CalculateIntegerProgressReportEventArgs e)
        {
            var handler = CalculateIntegerProgressChanged;//复制事件引用以避免多线程竞争问题
            if (handler != null)
            {
                SyncContextInvoke(() => handler(this, e));
            }
        }

        protected void OnCalculateIntegerCancelled(EventArgs e)
        {
            var handler = CalculateIntegerCancelled;//复制事件引用以避免多线程竞争问题
            if (handler != null)
            {
                SyncContextInvoke(() => handler(this, e));
            }
        }

        private void SyncContextInvoke(Action action)
        {
            if (SynchronizationContext.Current != null)
            {
                SynchronizationContext.Current.Post(_ => action(), null);//使用Post方法异步调用事件处理程序
            }
            else
            {
                action();//直接调用事件处理程序
            }
        }
    }
    #endregion

    /// <summary>
    /// 自定义同步上下文类，用于模拟实现同步上下文
    /// </summary>
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
