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
namespace LearnCSharp.Basic
{
    internal class LearnDataType
    {
        public static void LearnObject()
		{
			/*System.Object o = new Object();
			  object是System.Object的别名，是C#保留关键字，它表示的就是System.Object
			  object是所有.NET类的最终基类，是类型层次的根，并且为所有派生类提供低级别服务*/
			object o = new object();

            //o.GetType()返回的是o的实际类型
            string outputString = $"这是所有.NET类的最终基类：{o.GetType()}";

			Console.WriteLine(outputString);
		}

		public static void LearnValueType()
		{
			//值类型
		}

		public static void LearnRefType()
		{
			//引用类型
		}





        public static void StartLearnDataType()
        {
            string title = "001 object类型\n" +
                "002 值类型\n" +
                "003 引用类型";

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
                    case "002": LearnValueType(); break;
                    case "003": LearnRefType(); break;
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
