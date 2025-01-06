using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.BCL
{
    internal class BCLs
    {
        public static void LearnBCL()
        {
            Console.Title = ".NET BCL 学习";
            Console.WriteLine("---------.NET BCL 学习---------");

            do
            {
                string title = "001 \n" +
                    "002 \n" +
                    "003 \n" +
                    "004 \n";

                Console.WriteLine(title);
                Console.Write("【.NET BCL 学习】请输入编号章节查看代码运行结果：");

                string? code = Console.ReadLine();
                Console.WriteLine();

                switch (code)
                {
                    case "001": break;
                    case "002": break;
                    case "003": break;
                    case "004": break;
                    default: Console.WriteLine("未查询到相应章节！"); break;
                }
                Console.WriteLine();

                Console.WriteLine("【.NET BCL 学习】是否继续查询和运行章节代码：直接按下Enter继续，否则即回到上级目录");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;

                Console.WriteLine("\n");
            }
            while (true);
        }
    }
}
