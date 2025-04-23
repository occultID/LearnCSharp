extern alias Helper;//引入外部程序集，并且为其创建一个别名

using Microsoft.VisualBasic;
using System;
using System.Diagnostics;
using static Helper.HelperLibForLearnCSharp.SharedData;

class Test
{
    // 同步锁，防止多线程输出时文字交错
    private static readonly object objLock = new object();

    //static int count1 = 0;
    internal static void TestFunc()
    {
        string[] names = { "Alice", "Bob", "Charlie", "David", "Eve" };

        List<Action> actions = new List<Action>();

        for (int i = 0; i < names.Length; i++)
        {
            int index = i; // Capture the current value of i
            actions.Add(() =>
            {
                lock (objLock)
                {
                    Console.WriteLine($"Hello, {names[index]}!");
                    //count1++;
                    //Console.WriteLine(count1);
                }
            });
        }

        foreach (var item in actions)
        {
            item.Invoke();
        }
    }
    
}