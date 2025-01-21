/*【简单了解数据类型】
 * 什么是类型
	• 又名数据类型（DataType）
		○ A data type is a homogeneous of values, effectively presented, equipped with a set of operations which manipulate these values.
		○ 是数据在内存中存储时的“型号”
		○ 小内存容纳大尺寸数据会丢失精确度、发生错误
		○ 大内存容纳小尺寸数据会导致浪费
		○ 编程语言的数据类型与数学的数据类型不完全相同
	• 强类型语言与弱类型语言的比较
		○ C语言示例：if条件
			§ 示例：if(x=5)… C能通过编译，C#不能，C#中if条件必须返回明确bool值
		○ JavaScript示例：动态类型
			§ 示例：声明变量，var a = 123;a = "qwe";JS中允许为a连续赋值且能付不同类型的值，但是C#中不允许这样，C#中在给a第一次赋值时就确认了a的实际数据类型，所以再次为a赋值时必须赋符合a类型的值
		○ C#语言对弱类型/动态类型的模仿
			§ dynamic类型

 * 类型在C#语言中的作用
	• 一个C#类型中所包含的信息有：
		○ 存储此类型变量所需的内存空间大小
		○ 此类型的值可表示的最大、最小值范围
		○ 此类型所包含的成员（如属性、方法、事件等）
		○ 此类型由何基类派生而来
		○ 程序运行的时候，此类型的变量被分配在内存的什么位置：运行~从硬盘装载到内存
			§ Stack（栈）
				□ 特点：分配空间小，一般也就1~2MB
				□ 作用：方法调用
			§ Stack Overflow
			§ Heap（堆）
				□ 特点：分配空间大
				□ 作用：存储对象
			§ 使用Performance Monitor查看进程的堆内存使用量
			§ 内存泄漏
		○ 此类型所允许的操作（运算）

 * C#语言的类型系统
	• C#的五大数据类型
		○ 类（Classes）：如Windows，Form，Console，String
		○ 结构体（Structures）：如：Int32，Int64，Single，Double
		○ 枚举（Enumerations）：如HorizontalAlignment，Visibility
		○ 接口（Interfaces）
		○ 委托（Delegates）
	• C#的派生谱系
		           C#中所有的类型均有一个共同的最终基类Object
		           C#中有两种类型：引用类型和值类型。 
					   -引用类型的变量存储对其数据（对象）的引用，而值类型的变量直接包含其数据。 
					   -对于引用类型，两种变量可引用同一对象；因此，对一个变量执行的操作会影响另一个变量所引用的对象。
                       -对于值类型，每个变量都具有其自己的数据副本，对一个变量执行的操作不会影响另一个变量（ref 和 out 参数变量除外）
		○ Object
			§ 引用类型（Reference Type）
				□ 类
					® 所有类的基类：object
					® Unicode字符串：string
					® 用于定义和声明类的关键字：class
				□ 接口
					® 用于定义和声明接口的关键字：interface
				□ 数组类型
                    ® 用于定义和声明数组的关键字：任意类型[]
				□ 委托
					® 用于定义和声明委托的关键字：delegate
                □ 记录
					® 用于定义和声明记录的关键字：record 或 record class
					® record类型是C#9.0引入的，record class是C#10.0引入的
			§ 值类型（Value Type）
				□ 结构体
					® 用于定义和声明结构体的关键字：struct
				□ 枚举
				    ® 用于定义和声明枚举的关键字：enum
                □ 元组
                    ® 用于定义和声明元组的关键字：(任意类型, 任意类型, ...)
					® 值类型元组是C#7.0引入的
                □ 记录结构
					® 用于定义和声明记录结构的关键字：record struct
                    ® 记录结构是C#10.0引入的   
*/
using System.Runtime.CompilerServices;

namespace LearnCSharp.Basic
{
    internal class LearnDataType
    {
        /*【学习object类型】 
         *	object是System.Object的别名，是C#保留关键字，它表示的就是System.Object
		 *	在 C# 的统一类型系统中，所有类型（预定义类型、用户定义类型、引用类型和值类型）都是直接或间接从 System.Object 继承的。 
		 *	object是所有.NET类的最终基类，是类型层次的根，并且为所有派生类提供低级别服务
			object类型的变量可以引用任何类型的对象，包括值类型和引用类型，这个操作称为装箱
            装箱操作是将值类型转换为引用类型的过程，装箱操作会在堆上创建一个对象，然后将值类型的值复制到这个对象中
			使用引用了其他类型的object对象时，需要先将其转换为原本兼容类型，这个过程称为拆箱*/
        public static void LearnObject()
		{
            //以下两种声明object类型的变量的方式是等价的
            Object obj1 = new Object();
            object obj2 = new object();

            Console.WriteLine("已创建两个object实例 obj1 和 obj2");

			PrintObjectInfo(obj1);
            PrintObjectInfo(obj2);

            //装箱
            object obj3 = 123;
            //拆箱
            int i = (int)obj3;
            Console.WriteLine("【装箱】已创建一个object实例obj3，并引用了一个整型数值");
            PrintObjectInfo(obj3);
            Console.WriteLine($"【拆箱】拆箱后的整型数值为：{i}");
        }

        /*【学习动态类型】 
         *	动态类型：表示变量的使用和对其成员的引用绕过编译时类型检查。 改为在运行时解析这些操作。 
		 *	dynamic 类型简化了对 COM API（例如 Office Automation API）、动态 API（例如 IronPython 库）和 HTML 文档对象模型 (DOM) 的访问
		 *	在大多数情况下，dynamic 类型与 object 类型的行为类似。 
				具体而言，任何非 Null 表达式都可以转换为 dynamic 类型。 
		 *	dynamic 类型与 object 的不同之处在于
		 		编译器不会对包含类型 dynamic 的表达式的操作进行解析或类型检查。 
				编译器将有关该操作信息打包在一起，之后这些信息会用于在运行时评估操作。 
				在此过程中，dynamic 类型的变量会编译为 object 类型的变量。 
				因此，dynamic 类型只在编译时存在，在运行时则不存在。*/
        public static void LearnDynamic()
        {
            //声明三个动态类型的变量，并分别赋值为整型数值、字符串和一个object对象
            dynamic dyn1 = 123;
            dynamic dyn2 = "Hello World";
            dynamic dyn3 = new object();

            Console.WriteLine("已创建三个dynamic实例 dyn1、dyn2 和 dyn3，并分别引用整型数值、字符串和一个object对象");

            PrintDynamicInfo(dyn1);
            PrintDynamicInfo(dyn2);
            PrintDynamicInfo(dyn3);

            //新建一个object对象，并装箱一个整型数值
            //object obj = 123;
            //obj = obj + 1;//这里注释了该语句是因为会编译不通过，因为object类型在编译时已经确定类型，其不支持+运算符
            dyn1 = dyn1 + 1;//动态类型不会在编译时进行类型检查，所以这样写可以通过编译
            Console.WriteLine($"【动态类型】\n" +
                $"dyn1实例在运行时为整型，数值加1后的值为：{dyn1}");
        }

        public static void LearnValueType()
		{
			//值类型
			Console.WriteLine("请在 [C# 学习--基础篇] 章节中选择以下章节：\n" +
                    "006 .NET内置简单类型\n" +
                    "007 元组\n" +
                    "010 枚举\n" +
                    "026 结构\n");
        }

		public static void LearnRefType()
		{
			//引用类型
			Console.WriteLine("请在 [C# 学习--基础篇] 章节选择以下章节：\n" +
                    "006 .NET内置简单类型\n" +
                    "009 数组\n" +
                    "022 类\n" +
                    "025 接口\n" +
                    "027 记录\n" +
                    "\n请在 [C# 学习--高级篇] 章节选择以下章节：\n" +
					"001 委托\n");
		}
        private static void PrintObjectInfo(object obj, [CallerArgumentExpression("obj")] string? argumentName = null)
        {
			Console.WriteLine($"【Object类型】\n" +
				$"实例：[{argumentName}]\n" +
				$"实际类型：[{obj.GetType()}]\n" +
				$"哈希值：[{obj.GetHashCode()}]\n");
        }
        private static void PrintDynamicInfo(dynamic dyn, [CallerArgumentExpression("dyn")] string? argumentName = null)
        {
            Console.WriteLine($"【Dynamic类型】\n" +
                $"实例：[{argumentName}]\n" +
                $"实际类型：[{dyn.GetType()}]\n" +
                $"哈希值：[{dyn.GetHashCode()}]\n");
        }

        public static void StartLearnDataType()
        {
            string title = "001 object类型\n" +
                "002 动态类型\n" +
                "003 值类型\n" +
                "004 引用类型";

            do
            {
                Console.WriteLine("【数据类型学习】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnObject(); break;
                    case "002": LearnDynamic(); break;
                    case "003": LearnValueType(); break;
                    case "004": LearnRefType(); break;
                    default: Console.WriteLine("输入错误！"); break;
                }

                Console.WriteLine();
                Console.WriteLine("是否继续查询和运行本章节其他代码：直接按下Enter继续，否则即退出");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;
                Console.WriteLine("\n");
            }
            while (true);
        }
    }
}
