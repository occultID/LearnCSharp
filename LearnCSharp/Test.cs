extern alias Helper;//引入外部程序集，并且为其创建一个别名

using Microsoft.VisualBasic;
using System.Diagnostics;
using static Helper.HelperLibForLearnCSharp.SharedData;

class Test
{
    private static object obj = new object();//锁对象
    /*【00000：测试方法】*/
    public static void TestFunc()
    {
        Stopwatch stopwatch = new Stopwatch();
        using CountdownEvent countdownEvent = new CountdownEvent(16);//创建一个倒计时事件，初始计数为4

        List<string> strings = new List<string>(10000);
        int count = 0;

        void AddList()
        {
            var name = Thread.CurrentThread.Name;
            Console.WriteLine($"{name}正在向strings列表添加数据");

            while (true)
            {
                lock (obj)
                {
                    if (count >= 10000)
                        break;
                    string data = $"【数据】{name}：{count:000000}";
                    strings.Add(data);
                    count++;
                }
                Thread.Sleep(TimeSpan.FromMilliseconds(1));//模拟耗时操作
            }
            if(name != "主线程")
            {
                countdownEvent.Signal();//通知倒计时事件，当前线程已完成
            }
        }

        Thread thread = Thread.CurrentThread;
        thread.Name = "主线程";

        stopwatch.Start();
        //AddList();
        stopwatch.Stop();
        Console.WriteLine($"单线程耗时{stopwatch.ElapsedMilliseconds}");
        Console.WriteLine();
        Console.ReadKey();
        foreach (var item in strings)
        {
            Console.WriteLine(item);
        }

        stopwatch.Reset();
        count = 0;
        strings.Clear();

        Console.WriteLine();
        List<Thread> threads = new List<Thread>();
        for (int i = 0; i < 16; i++)
        {
            Thread thread1 = new Thread(AddList);
            thread1.Name = $"线程{thread1.ManagedThreadId}";
            threads.Add(thread1);
        }

        stopwatch.Start();

        for (int i = 0; i < threads.Count; i++)
        {
            threads[i].Start();
        }

        countdownEvent.Wait();//等待所有线程完成
        stopwatch.Stop();
        Console.WriteLine($"多线程耗时{stopwatch.ElapsedMilliseconds}");
        stopwatch.Reset();
        Console.WriteLine();
        Console.ReadKey();
        foreach (var item in strings)
        {
            Console.WriteLine(item);
        }
    }
}