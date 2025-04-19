/*【代码说明】
 *01.本项目是一个C#学习项目，包含了C#的基础知识、面向对象编程、LINQ、异步编程等内容。
 *02.本项目使用了项目编写时已发布的C#的最新特性，如记录类型、元组、LINQ等。
 *03.本项目使用了.NET 9.0 进行学习。
 *04.本项目使用了C# 13.0 进行学习。
 *05.本项目每页代码开头都尽可能包含了笔记，如果笔记中的内容与代码不一致，请以代码为准。
 *06.本项目每页代码的笔记中如果出现了对齐不齐的情况，请使用Visual Studio 2022打开项目并将编辑器字体设置为JetBrains Mono。
 *07.本项目所有笔记和注释均为本人所写，可能存在错误和不准确的地方，请读者自行判断。
 *08.本项目所有笔记和注释均为中文，可能存在与官方英文原文档翻译不准确的地方，请读者自行判断。
 *09.本项目所有代码均为本人所写，可能存在错误和不准确的地方，请读者自行判断。
 *10.运行本项目代码时，请尽量不使用目录模式，直接输入代码注释中的数字进行运行。
 *11.运行本项目代码时，如需输入时，请尽量使用英文输入，如果已设置控制台字符格式为Unicode或系统语言使用Unicode进行输出，则可放心输入中文。
 */

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

        /// <summary>
        /// 初始化方法
        /// 用于显示当前操作系统、计算机架构、.NET信息和程序设置代码页
        /// 用于设置控制台输入编码为UTF8
        /// </summary>
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

            //如果没有参数，直接运行本程序主方法
            if (args.Length == 0)
            {
                while (true)
                {
                    Console.WriteLine("-------请输入各章节注释标注代码直接运行对应代码-------");
                    Console.WriteLine("【如需目录模式请输入“menu”；如需退出请输入“exit”】");
                    Console.WriteLine("【输入完成后按下“Enter”键确认】");

                    Console.WriteLine();
                    Console.Write("输入：");

                    string? input = Console.ReadLine();

                    switch (input.ToLower())
                    {
                        case "menu":
                            Menus.ShowMenu(MenuType.Project);
                            break;
                        case "exit":
                            Environment.Exit(0);
                            break;
                        default:
                            DirectNavigation.DirectNavigate(input);
                            break;
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                //如果有参数，先运行参数中的代码，然后再运行主方法
                foreach (var item in args)
                {
                    DirectNavigation.DirectNavigate(item);
                }

                //运行主方法
                Main(Array.Empty<string>());
            }
        }
    }
}