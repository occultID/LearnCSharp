using System.Runtime.InteropServices;
using System.Text;
using System.Collections;
using System.Numerics;
using LearnCSharp.Basic;

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
            if (program is null)
            {
                lock (obj)
                {
                    program ??= new Program();
                    return program;
                }
            }
            return program;
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
                while (true)
                {
                    Console.WriteLine("-------请输入各章节注释标注代码直接运行对应代码-------");
                    Console.WriteLine("【如需目录模式请输入“menu”；如需退出请输入“exit”】");
                    Console.WriteLine("【输入完成后按下“Enter”键确认】");

                    Console.WriteLine();
                    Console.Write("输入：");

                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "menu" or "Menu" or "MENU":
                            Menus.ShowMenu(MenuType.Project);
                            break;
                        case "exit" or "Exit" or "EXIT":
                            Environment.Exit(0);
                            break;
                        default:
                            DirectNavigation.DirectNavigate(input);
                            break;
                    }
                }
            }
            else
            {
                foreach (var item in args)
                {
                    if (item is not null && Menus.ProjectMethods.TryGetValue(item, out Action? action))
                        action.Invoke();
                    else
                        Environment.Exit(0);
                }

            }
        }
    }
}