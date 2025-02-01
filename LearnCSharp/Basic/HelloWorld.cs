using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Basic
{
    internal class HelloWorld
    {
      
        //【10101：HelloWorld 示例】
        public static void SayHello()
        {
            Console.WriteLine("\n------示例：Hello World------\n");

        InputName:
            Console.WriteLine("你好，欢迎来到C#.NET的世界！");
            Console.Write("请输入你的名字：");

            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
                goto InputName;

            Console.WriteLine($"Hello World! 你好{name}\n");
        }

    }
}
