using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Basic
{
    internal class HelloWorld
    {
        public static void SayHello()
        {
            InputName:
            Console.WriteLine("你好，欢迎来到C#.NET的世界！");
            Console.Write("请输入你的名字：");

            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
                goto InputName;

            Console.WriteLine($"Hello World! 你好{name}");
        }
    }
}
