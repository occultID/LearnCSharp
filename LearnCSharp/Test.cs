extern alias Helper;//引入外部程序集，并且为其创建一个别名

using Microsoft.VisualBasic;
using System.Diagnostics;
using static Helper.HelperLibForLearnCSharp.SharedData;

class Test
{
    // 预定义颜色列表（确保每个线程有唯一颜色）
    private static readonly ConsoleColor[] Colors = new ConsoleColor[]
    {
        ConsoleColor.Red,
        ConsoleColor.Green,
        ConsoleColor.Blue,
        ConsoleColor.Yellow,
        ConsoleColor.Cyan,
        ConsoleColor.Magenta,
        ConsoleColor.White,
        ConsoleColor.Gray
    };

    // 线程本地存储：为每个线程分配独立颜色
    private static ThreadLocal<ConsoleColor> _threadColor = new ThreadLocal<ConsoleColor>(() =>
    {
        // 为线程分配颜色（基于线程ID哈希值）
        int index = Environment.CurrentManagedThreadId % Colors.Length;
        return Colors[index];
    });

    // 同步锁，防止多线程输出时文字交错
    private static readonly object _lock = new object();

    internal static void TestFunc()
    {
        // 创建5个线程，每个线程循环输出5次
        for (int i = 0; i < 5; i++)
        {
            new Thread(() =>
            {
                for (int j = 0; j < 5; j++)
                {
                    // 获取当前线程的专属颜色
                    ConsoleColor color = _threadColor.Value;

                    // 同步输出，避免文字混杂
                    lock (_lock)
                    {
                        Console.ForegroundColor = color;
                        Console.WriteLine($"线程 {Thread.CurrentThread.ManagedThreadId} 第 {j + 1} 次输出");
                        Console.ResetColor(); // 重置为默认颜色（可选）
                    }

                    Thread.Sleep(100); // 模拟耗时操作
                }
            }).Start();
        }

        Console.ReadLine(); // 等待所有线程完成
    }
}