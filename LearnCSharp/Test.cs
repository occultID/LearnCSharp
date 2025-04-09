extern alias Helper;//引入外部程序集，并且为其创建一个别名

using Microsoft.VisualBasic;
using System.Diagnostics;
using static Helper.HelperLibForLearnCSharp.SharedData;

class Test
{
    // 同步锁，防止多线程输出时文字交错
    private static readonly object objLock = new object();

    //static int count1 = 0;
    internal static void TestFunc()
    {

        void Run(object obj)
        {
            int lineNumber = (int)obj;
            var name = Thread.CurrentThread.Name;

            for (int i = 0; i <= 50; i++)
            {
                lock (objLock)
                {
                    Console.SetCursorPosition(0, lineNumber);
                    Console.ForegroundColor = (ConsoleColor)(lineNumber % 16);
                    Console.Write($"{name}: ");
                    Console.ResetColor();
                    Console.Write("[");
                    Console.Write(new string('=', i));
                    Console.SetCursorPosition(Console.GetCursorPosition().Left, lineNumber);
                    Console.Write($"]{i*2}%");
                }
                Thread.Sleep(100);
            }
            
        }

        Console.Clear();

        for (int i = 0; i < 3; i++)
        {
            Thread.CurrentThread.Name = $"Thread {i}";
            Run(i);
        }

        Console.WriteLine();
    }
}