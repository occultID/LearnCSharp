using LearnCSharp.BCL;
using LearnCSharp.Example;
using LearnCSharp.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Basic
{
    internal class Basics
    {
        public static void LearnBasic()
        {
            Console.Title = "C# 基础知识学习";
            Console.WriteLine("---------C# 基础知识学习---------");

            do
            {
                string title = "001 Hello World\n" +
                    "002 C#学习--高级篇\n" +
                    "003 .NET基础类库\n" +
                    "004 代码示例\n";

                Console.WriteLine(title);
                Console.Write("【C#.NET基础学习】请输入编号章节查看代码运行结果：");

                string? code = Console.ReadLine();
                Console.WriteLine();

                switch (code)
                {
                    case "001": HelloWorld.SayHello(); break;
                    case "002": break;
                    case "003": break;
                    case "004": break;
                    default: Console.WriteLine("未查询到相应章节！"); break;
                }
                Console.WriteLine();

                Console.WriteLine("【C#.NET基础学习】是否继续查询和运行章节代码：直接按下Enter继续，否则即回到上级目录");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;

                Console.WriteLine("\n");
            }
            while (true);
        }

    }
}
