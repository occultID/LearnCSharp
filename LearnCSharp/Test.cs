using System.Text;

class Test
{
    private static int number = 0;
    /*【00000：测试方法】*/
    public static void TestFunc()
    {
        Thread current = Thread.CurrentThread;
        current.Name = "主线程";
        PrintNumber();

        Thread thread1 = new Thread(PrintNumber);
        Thread thread2 = new Thread(PrintNumber);
        Thread thread3 = new Thread(PrintNumber);

        thread1.Name = $"线程{thread1.ManagedThreadId}";
        thread2.Name = $"线程{thread2.ManagedThreadId}";
        thread3.Name = $"线程{thread3.ManagedThreadId}";

        thread1.Start();
        thread2.Start();
        thread1.Join();
        thread2.Join();
    }

    private static void PrintNumber()
    {
        var name = Thread.CurrentThread.Name;
        for (int i = 1; i <= 10; i++)
        {
            //Thread.Sleep(1000);//暂停线程，模拟耗时操作
            Console.ForegroundColor = (ConsoleColor)Random.Shared.Next(1, 16);
            Console.WriteLine($"{name}输出数字{++number:00}");
            //Thread.Sleep(1000);
        }
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}