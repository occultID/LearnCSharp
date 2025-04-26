using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Professional
{
    internal class LearnParallelProgramming
    {
        private readonly static object objLock = new object();

        /*【21201：并行迭代】*/
        public static void LearnParallelForAndForEach()
        {
            Console.WriteLine("\n------示例：并行迭代------\n");

            List<string> poems = new List<string>()
            {
                "众里寻他千百度，蓦然回首，那人却在灯火阑珊处。",
                "无边落木萧萧下，不尽长江滚滚来。",
                "多情自古伤离别，更那堪冷落清秋节。",
                "泪眼问花花不语，乱红飞过秋千去。",
                "竹杖芒鞋轻胜马，谁怕？一蓑烟雨任平生。",
                "此情无计可消除，才下眉头，却上心头。",
                "昨夜西风凋碧树，独上高楼，望尽天涯路。",
                "最是人间留不住，朱颜辞镜花辞树。",
                "洛阳亲友如相问，一片冰心在玉壶。",
                "抽刀断水水更流，举杯消愁愁更愁。",
                "玲珑骰子安红豆，入骨相思知不知？",
                "两情若是久长时，又岂在朝朝暮暮。",
                "胭脂泪，相留醉，几时重？自是人生长恨水长东。",
                "曾经沧海难为水，除却巫山不是云。",
                "何当共剪西窗烛，却话巴山夜雨时。",
                "欲买桂花同载酒，终不似，少年游。",
                "梦里不知身是客，一晌贪欢。",
                "流水落花春去也，天上人间。",
                "林花谢了春红，太匆匆。无奈朝来寒雨晚来风。",
                "独自莫凭栏，无限江山。别时容易见时难。"
            };

            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine($"》》》使用常规For循环获取poems集合内诗句《《《");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("》》》For 循环");
            Console.WriteLine();
            
            stopwatch.Start();
            for (int i = 0; i < poems.Count; i++) 
            {
                Console.WriteLine(poems[i]);
                Thread.Sleep(200);
            }
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"》》》For 循环 用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();

            Console.ReadKey();

            Console.WriteLine($"》》》使用Parallel.For获取poems集合内诗句《《《");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("》》》Parallel.For");
            Console.WriteLine();

            stopwatch.Restart();
            Parallel.For(0, poems.Count, i =>
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取诗句：{poems[i]}");
                Thread.Sleep(200);
            });
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"》》》Parallel.For 用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();

            Console.ReadKey();

            Console.WriteLine($"》》》使用常规Foreach循环获取poems集合内诗句《《《");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("》》》Foreach 循环");
            Console.WriteLine();

            stopwatch.Restart();
            foreach (var item in poems)
            {
                Console.WriteLine(item);
                Thread.Sleep(200);
            }
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"》》》Foreach 循环 用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();

            Console.ReadKey();

            Console.WriteLine($"》》》使用Parallel.ForEach获取poems集合内诗句《《《");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("》》》Parallel.ForEach");
            Console.WriteLine();

            stopwatch.Restart();
            Parallel.ForEach(poems, item =>
            {
                Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取诗句：{item}");
                Thread.Sleep(200);
            });
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"》》》Parallel.ForEach 用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("----------------------------------------------");

            Console.WriteLine();
        }

        /*【21202：Parallel.Invoke】*/
        public static void LearnParallelInvoke()
        {
            Console.WriteLine("\n------示例：并行调用------\n");

            int count = 0;
            Stopwatch stopwatch = new Stopwatch();
            List<Action> actions = new List<Action>(10);

            for (int i = 0; i < 10; i++)
            {
                int index = i;
                actions.Add(() => 
                {
                    for (int j = index * 50; j < index * 50 + 50; j++)
                    {
                        lock (objLock)
                        {
                            count += j;
                        }
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】 执行 [委托 {index+1:00}] 进行运算");
                        Thread.Sleep(10);
                    }
                });
            }

            Console.WriteLine($"》》》使用常规委托调用激活actions集合内委托《《《");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("》》》常规调用");
            Console.WriteLine();

            stopwatch.Start();
            actions.ForEach(action => action.Invoke());
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"》》》常规调用---计算count最终结果：{count} | 用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();
            
            count = 0;
            Console.ReadKey();

            Console.WriteLine($"》》》使用Parallel.Invoke调用激活actions集合内委托《《《");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("》》》Parallel.Invoke调用");
            Console.WriteLine();

            stopwatch.Restart();
            Parallel.Invoke(actions.ToArray());
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"》》》Parallel.Invoke调用---计算count最终结果：{count} | 用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();

            count = 0;
            Console.ReadKey();

            Console.WriteLine($"》》》使用多线程委托调用激活actions集合内委托《《《");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("》》》多线程调用");
            Console.WriteLine();

            List<Thread> threads = new List<Thread>(10);
            for (int i = 0; i < 10; i++)
            {
                int index = i;
                threads.Add(new Thread(() => actions[index].Invoke()));
            }

            stopwatch.Restart();
            threads.ForEach(thread => thread.Start());
            threads.ForEach(thread => thread.Join());
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine($"》》》多线程调用---计算count最终结果：{count} | 用时：{stopwatch.Elapsed.TotalMilliseconds}毫秒");
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine();

            count = 0;
            threads.Clear();
            actions.Clear();
        }

        /*【21203：ParallelLoopState】*/
        public static void LearnParallelLoopState()
        {
        }

        /*【21204：ParallelLoopResult】*/
        public static void LearnParallelLoopResult()
        {
        }

        /*【21205：ParallelOptions】*/
        public static void LearnParallelOptions()
        {
        }

        public static void StartLearnParallelProgramming()
        {
            // 1. Parallel类
            // 2. Parallel.Invoke方法
            // 3. Parallel.For方法
            // 4. Parallel.ForEach方法
            // 5. ParallelLoopState类
            // 6. ParallelLoopResult类
            // 7. ParallelOptions类
            // 8. Task类
            // 9. Task<TResult>类
            // 10. TaskFactory类
            // 11. TaskCompletionSource<TResult>类
            // 12. TaskScheduler类
            // 13. TaskContinuationOptions枚举
            // 14. TaskCreationOptions枚举
            // 15. TaskStatus枚举
            // 16. TaskCanceledException类
            // 17. TaskCompletionSource<TResult>类
            // 18. TaskFactory类
            // 19. TaskScheduler类
            // 20. TaskContinuationOptions枚举
            // 21. TaskCreationOptions枚举
            // 22. TaskStatus枚举
            // 23. TaskCanceledException类
            // 24. TaskCompletionSource<TResult>类
            // 25. TaskFactory类
            // 26. TaskScheduler类
            // 27. TaskContinuationOptions枚举
            // 28. TaskCreationOptions枚举
            // 29. TaskStatus枚举
            // 30. TaskCanceledException类
            // 31. TaskCompletionSource<TResult>类
            // 32. TaskFactory类
            // 33. TaskScheduler类
            // 34. TaskContinuationOptions枚举
            // 35. TaskCreationOptions枚举
            // 36. TaskStatus枚举
            // 37. TaskCanceledException类
            // 38. TaskCompletionSource<TResult>类
            // 39. TaskFactory类
            // 40. TaskScheduler类
            // 41. TaskContinuationOptions枚举
            // 42. TaskCreationOptions枚举
            // 43. TaskStatus枚举
            // 44. TaskCanceledException类
            // 45. TaskCompletionSource<TResult>类
            // 46.
        }
    }
}