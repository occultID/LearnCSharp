using LearnCSharp.Basic;
using LearnCSharp.Professional;
using LearnCSharp.BCL;
using LearnCSharp.Example;
using System.Diagnostics;

namespace LearnCSharp
{
    internal enum MenuType
    {
        Project,
        BasicChapter,
        ProfessionalChapter,
        BCLChapter,
        ExampleChapter,
        Exit
    }
    internal class Menus
    {
        public const string ProjectName = "C#.NET修行之路";
        public const string ProjectMenu = "001 C#学习--基础篇\n" +
                        "002 C#学习--高级篇\n" +
                        "003 .NET基础类库\n" +
                        "004 代码示例\n" +
                        "005 退出\n";
        public static readonly Dictionary<string, Action> ProjectMethods = new Dictionary<string, Action>()
        {
            ["001"] = () => ShowMenu(MenuType.BasicChapter),
            ["002"] = () => ShowMenu(MenuType.ProfessionalChapter),
            ["003"] = () => ShowMenu(MenuType.BCLChapter),
            ["004"] = () => ShowMenu(MenuType.ExampleChapter),
            ["005"] = Exit
        };

        public const string BasicChapterName = "C# 学习--基础篇";
        public const string BasicChapterMenu = "001 HelloWorld\n" +
                    "002 关键字\n" +
                    "003 更多标识符\n" +
                    "004 数据类型基础\n" +
                    "005 .NET内置简单类型\n" +
                    "006 元组\n" +
                    "007 类型转换\n" +
                    "008 数组\n" +
                    "009 枚举\n" +
                    "010 运算符和表达式\n" +
                    "011 语句\n" +
                    "012 方法\n" +
                    "013 本地函数\n" +
                    "014 扩展方法\n" +
                    "015 变量\n" +
                    "016 方法参数\n" +
                    "017 命名空间\n" +
                    "018 类\n" +
                    "019 表达式主体成员定义\n" +
                    "020 继承与多态\n" +
                    "021 接口\n" +
                    "022 结构\n" +
                    "023 记录\n" +
                    "024 匿名类型\n" +
                    "025 异常处理\n";

        public static readonly Dictionary<string, Action> BasicChapterMethods = new Dictionary<string, Action>()
        {
            ["001"] = HelloWorld.SayHello,
            ["002"] = LearnKeyWords.StartLearnKeywords,
            ["003"] = LearnLiteralsAndOthers.StartLearnLiteralsAndOthers,
            ["004"] = LearnDataType.StartLearnDataType,
            ["005"] = LearnSimpleType.StartLearnSimpleType,
            ["006"] = LearnTuple.StartLearnTuple,
            ["007"] = LearnCastingAndTypeConversion.StartLearnCastingAndTypeConversion,
            ["008"] = LearnArray.StartLearnArray,
            ["009"] = LearnEnum.StartLearnEnum,
            ["010"] = LearnOperatorAndExpression.StartLearnOperatorAndExpression,
            ["011"] = LearnStatements.StartLearnStatements,
            ["012"] = LearnMethod.StartLearnMethod,
            ["013"] = LearnLocalFunction.StartLearnLocalFunction,
            ["014"] = LearnExtensionMethod.StartLearnExtensionMethod,
            ["015"] = LearnVariables.StartLearnVariables,
            ["016"] = LearnParameters.StartLearnParameters,
            ["017"] = LearnNamespace.StartLearnNamespace,
            ["018"] = LearnClass.StartLearnClass,
            ["019"] = LearnExpressionBodied.StartLearnExpressionBodied,
            ["020"] = LearnInheritanceAndPolymorphism.StartLearnInheritanceAndPolymorphism,
            ["021"] = LearnInterface.StartLearnInterface,
            ["022"] = LearnStruct.StartLearnStruct,
            ["023"] = LearnRecord.StartLearnRecord,
            ["024"] = LearnAnonymousType.StartLearnAnonymousType,
            ["025"] = LearnExceptionHandling.StartLearnExceptionHandling
        };

        public const string ProfessionalChapterName = "C# 学习--高级篇";
        public const string ProfessionalChapterMenu = "001 委托\n" +
                    "002 匿名函数\n" +
                    "003 事件\n" +
                    "004 模式匹配\n" +
                    "005 集合\n" +
                    "006 泛型\n" +
                    "007 Linq\n" +
                    "008 特性\n" +
                    "009 反射\n" +
                    "010 动态类型\n" +
                    "011 异步编程\n" +
                    "012 并行编程\n";
        public static readonly Dictionary<string, Action> ProfessionalChapterMethods = new Dictionary<string, Action>()
        {
            ["001"] = LearnDelegate.StartLearnDelegate,
            ["002"] = LearnAnonymousFunction.StartLearnAnonymousFunction,
            ["003"] = LearnEvent.StartLearnEvent,
            ["004"] = LearnPattern.StartLearnPattern,
            ["005"] = LearnCollection.StartLearnCollection,
            ["006"] = LearnGenericType.StartLearnGenericType,
            ["007"] = LearnLinq.StartLearnLinq,
            ["008"] = LearnAttribute.StartLearnAttribute,
            ["009"] = LearnReflection.StartLearnReflection,
            ["010"] = LearnDynamic.StartLearnDynamic,
            ["011"] = LearnAsyncProgramming.StartLearnAsyncProgramming,
            ["012"] = LearnParallelProgramming.StartLearnParallelProgramming
        };

        public const string BCLChapterName = ".NET 基础类库";
        public const string BCLChapterMenu = "001 HelloWorld\n";
        public static readonly Dictionary<string, Action> BCLChapterMethods = new Dictionary<string, Action>()
        {
            ["001"] = HelloWorld.SayHello
        };

        public const string ExampleChapterName = "代码示例";
        public const string ExampleChapterMenu = "001 传值参数与引用参数的特征\n" +
                    "002 汉诺塔游戏的实现\n";
        public static readonly Dictionary<string, Action> ExampleChapterMethods = new Dictionary<string, Action>()
        {
            ["001"] = Swapper.Swap,
            ["002"] = Hanoi.StartGame
        };

        public static void ShowMenu(MenuType menuType)
        {
            Dictionary<string, Action>? chapterMethods = null;
            switch (menuType)
            {
                case MenuType.Project:
                    Console.Title = ProjectName;
                    Console.WriteLine($"------------------{ProjectName}------------------");
                    Console.WriteLine(ProjectMenu);
                    chapterMethods = ProjectMethods;
                    break;
                case MenuType.BasicChapter:
                    Console.Title = BasicChapterName;
                    Console.WriteLine($"------------------{BasicChapterName}------------------");
                    Console.WriteLine(BasicChapterMenu);
                    chapterMethods = BasicChapterMethods;
                    break;
                case MenuType.ProfessionalChapter:
                    Console.Title = ProfessionalChapterName;
                    Console.WriteLine($"------------------{ProfessionalChapterName}------------------");
                    Console.WriteLine(ProfessionalChapterMenu);
                    chapterMethods = ProfessionalChapterMethods;
                    break;
                case MenuType.BCLChapter:
                    Console.Title = BCLChapterName;
                    Console.WriteLine($"------------------{BCLChapterName}------------------");
                    Console.WriteLine(BCLChapterMenu);
                    chapterMethods = BCLChapterMethods;
                    break;
                case MenuType.ExampleChapter:
                    Console.Title = ExampleChapterName;
                    Console.WriteLine($"------------------{ExampleChapterName}------------------");
                    Console.WriteLine(ExampleChapterMenu);
                    chapterMethods = ExampleChapterMethods;
                    break;
                default:
                    break;
            }
            Console.Write($"请输入对应章节代码查看结果：");

            string? chapterCode = Console.ReadLine();

            Console.WriteLine( );
            if (chapterCode is not null && chapterMethods!.TryGetValue(chapterCode,out Action? action))
                action.Invoke();
            else
                Console.WriteLine("\n*********未查询到相应章节！*********\n");

            Console.WriteLine("是否继续当前章节（Y/N）：\n");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
                ShowMenu(menuType);
            //else
            //    ShowMenu(MenuType.Project);
        }

        public static void Exit()
        {
            Console.Write("是否确认关闭程序(Y/N)：");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                Console.WriteLine("\n再见，2秒后将自动关闭程序");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
        }
    }
}
