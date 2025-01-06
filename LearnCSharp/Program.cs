using System.Runtime.InteropServices;
using System.Text;

namespace LearnCSharp
{
    public class Program
    {
        #region Singleton 单例模式
        private static Program? program;
        private static object obj = new object();

        private Program() { }
        public static Program CreateProgram()
        {
            if(program is null)
            {
                lock (obj)
                {
                    program ??= new Program();
                    return program;
                }
            }
            return program;
        }
        }
        #endregion
        private static void Initialize()
        {
            //如果当前系统默认代码页不是UTF8，则将输入编码更改为UTF8以支持简体中文输入。
            if (Console.InputEncoding.CodePage != Encoding.UTF8.CodePage)
                Console.InputEncoding = Encoding.UTF8;

            Console.Title = ".NET和C#修行";
            Console.WriteLine("当前操作系统：{0}", RuntimeInformation.OSDescription);
            Console.WriteLine("当前计算机架构：{0}", RuntimeInformation.OSArchitecture);
            Console.WriteLine("当前操作系统.NET信息：{0}", RuntimeInformation.FrameworkDescription);
            Console.WriteLine("当前程序设置代码页：{0}", Console.InputEncoding.CodePage);
            Console.WriteLine();
        }


        public static void Main(string[] args)
        {
            Initialize();

            if (args.Length == 0)
            {
                do
                {
                    string title = "001 C#学习--基础篇\n" +
                        "002 C#学习--高级篇\n" +
                        "003 .NET基础类库\n" +
                        "004 代码示例\n" +
                        "005 退出\n";

            if (args.Length == 0)
                Menus.ShowMenu(MenuType.Project);
            else
            {
                foreach (var item in args)
                {
                    if (item is not null && Menus.ProjectMethods.TryGetValue(item, out Action? action))
                        action.Invoke();
                    else
                        Environment.Exit(0);
                }

        static void RunLearnCode(string? code)
        {
            Console.WriteLine();
            switch (code)
            {
                case "001": Basics.LearnBasic(); break;
                case "002": Professionals.LearnProfessional(); break;
                case "003": BCLs.LearnBCL(); break;
                case "004": Examples.ShowExamples(); break;
                case "005":
                    Console.WriteLine("再见，五秒后将自动关闭程序");
                    Thread.Sleep(5000);
                    Process.GetCurrentProcess().Kill(); break;
                default: Console.WriteLine("未查询到相应章节！"); break;
            }
            Console.Title = ".NET和C#修行";
            Console.WriteLine();
        }
    }
}