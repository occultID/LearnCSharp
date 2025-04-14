using System.Diagnostics;

namespace LearnCSharp.Professional
{
    class LearnAsyncProgramming
    {

        public static void StartLearnAsyncProgramming()
        {
            string title = "001 进程：Process类代码示例\n" +
                "002 线程：Thread类代码示例\n" +
                "003 线程：线程传参\n" +
                "004 线程：互斥锁\n" +
                "005 进程：互斥体\n" +
                "006 线程：信号量\n" +
                "007 线程：读写锁\n" +
                "008 等待句柄：AutoResetEvent\n" +
                "009 等待句柄：ManualResetEvent\n" +
                "010 CountdownEvent\n" +
                "011 Barrier\n" +
                "012 线程池\n" +
                "013 CancellationToken";


            do
            {
                Console.WriteLine("【进程与线程】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
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
