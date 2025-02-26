﻿/*【学习元组】
 * 元组简介
	• 元组是C#7.0引入的一种新的值类型数据类型
	• 元组是提供了简洁的语法来将多个数据结构分组成一个轻型数据结构
	• 元组的元素是公共字段，这使得元组成为了可变的值类型
	• 元组是一种轻型数据结构，其用一对圆括号将元素组合成一个数据组，每个元素使用一个逗号分隔，形式如 (元素一, 元素二, …, 元素N)
	• C#7.0及之后中，元组是一种值类型，其内部实现是System.ValueTuple结构，但为了代码可读性和简洁性，上述表示法被C#采取为了一个”语法糖“

 * 元组声明、初始化与元素访问
	• 元组的声明与初始化方式
		○ 其一：(数据类型 变量名, 数据类型 变量名, …, 数据类型 变量名)  = (值, 值, …, 值)									//访问元组元素：(变量名, 变量名, …, 变量名).ItemN | (变量名, 变量名, …, 变量名).变量名 | 直接通过变量名访问
		○ 其二：(变量名, 变量名, …, 变量名) = (值, 值, …, 值) --所有变量必须在声明元组前明确进行类型声明						//访问元组元素：(变量名, 变量名, …, 变量名).ItemN | (变量名, 变量名, …, 变量名).变量名 | 直接通过变量名访问
		○ 其三：(var 变量名, var 变量名, …, var 变量名) = (值, 值, …, 值)													//访问元组元素：(变量名, 变量名, …, 变量名).ItemN | (变量名, 变量名, …, 变量名).变量名 | 直接通过变量名访问
		○ 其四：var (变量名, 变量名, …, 变量名) = (值, 值, …, 值)															//访问元组元素：(变量名, 变量名, …, 变量名).ItemN | (变量名, 变量名, …, 变量名).变量名 | 直接通过变量名访问
		○ 其五：var 元组变量名 = (变量名: 值, 变量名: 值, …, 变量名: 值)														//访问元组元素：元组变量名.ItemN | 元组变量名.变量名
		○ 其六：(数据类型 变量名, 数据类型 变量名, …, 数据类型 变量名) 元组变量名 = (值, 值, …, 值)							//访问元组元素：元组变量名.ItemN | 元组变量名.变量名
		○ 其七：(数据类型 变量名, 数据类型 变量名, …, 数据类型 变量名) 元组变量名 = (变量名: 值, 变量名: 值, …, 变量名: 值)	//访问元组元素：元组变量名.ItemN | 元组变量名.变量名
		○ 其八：(数据类型, 数据类型, …, 数据类型) 元组变量名 = (值, 值, …, 值)												//访问元组元素：元组变量名.ItemN
		○ 其九：var 元组变量名 = (值, 值, …, 值)																				//访问元组元素：元组变量名.ItemN
	• 注意项目
		○ 元组内所有元素的变量名不得相同
		○ 元组内如果所有元素数据类型相同，可以根据实际情况和需求来决定使用元组还是数组
		○ 对于其二的声明：元组元素的所有变量必须在用来声明元组前明确定义其数据类型
		○ 对于其五和其九以外的声明：=右边的值元组和=左边声明部分的元组变量的元素数量必须相同
		○ 对于其一、其二、其六、其七、其八的声明：=右边的值元组和=左边声明部分的元组变量的元素数据类型必须按顺序一一对应，即对应位置元素类型兼容
		○ 对于其五和其九的声明：元组变量内的元素值的数量和类型由=右边用于赋值的元组决定，元组变量的元素数据类型由编译器自动推导
		○ 对于其七：虽然=右边的值元组每个元素可以具名，但是其名称与类型 和=左边声明的元组变量的元素名称与数据类型一一对应，即对应位置名称相同和类型兼容
		○ 对于其五和其九以外的声明：如果=左边声明的元组某个元素不需要值，可将该元素的数据类型与变量名使用下划线”_“替代，表示放弃该元素，即”弃元“
		○ 元组的元素也称为其字段，上述声明中，其五至其七中的变量名均可为定义了的字段名，建议使用Pascal命名法，即名称所有单词的首字母大写
		○ 上述的 元组变量名 一般采用camel命名法。即首单词首字母小写
	• 元组访问
		○ 对于其一至其四声明的元组：可以使用 (变量名, 变量名, …, 变量名).ItemN 或 (变量名, 变量名, …, 变量名).变量名 或 直接通过变量名 来访问元组元素
		○ 对于其五至其七声明的元组：可以使用 元组变量名.ItemN 或 元组变量名.变量名 来访问元组元素
		○ 对于其八、其九声明的元组：只能使用 元组变量名.ItemN 来访问元素
		○ 对于定义了元组变量名的元组，其元素只能通过元组变量名来访问，对于没有定义元组变量名的元组，还可以直接通过元素变量名访问

 * 元组的赋值与析构
	• C#支持同时满足以下两个条件的元组类型之间的赋值操作
		○ 两个元组类型有相同数量的元素
		○ 对于每个元组位置，右侧元组元素的类型与左侧相应位置的元组元素的类型相同或可以隐式转换为左侧相应位置的元素的类型
	• 元组元素是按照元组元素的顺序赋值的。元组字段的名称会被忽略且不会被赋值，即元组赋值不会考虑对应位置字段名称是否相同
	• 元组的析构
		○ 元组提供一种从方法调用中检索多个值的轻量级方法
		○ C#提供内置的元组析构支持，可在单个操作中解包一个元组中的所有项。
		○ 用于析构元组的常规语法与用于声明元组的语法相似：将要向其分配元素的变量放在赋值语句左侧的括号中
		○ 析构形式：
			§ 其一：(数据类型 变量名, 数据类型 变量名, …, 数据类型 变量名) 元组变量名 = 可析构为元组的项
			§ 其二：(数据类型, 数据类型, …,数据类型) 元组变量名 = 可析构为元组的项
			§ 其三：var 元组变量名 = 可析构为元组的项
			§ 其四：(数据类型 变量名, 数据类型 变量名, …, 数据类型 变量名) = 可析构为元组的项
			§ 其五：var (变量名, 变量名, …, 变量名) = 可析构为元组的项
			§ 其六：(var 变量名, var 变量名, …, var 变量名) = 可析构为元组的项
			§ 其七：(变量名, 变量名, …, 变量名) = 可析构为元组的项 //左侧元组元素变量必须在使用前明确声明其类型
		○ 关于可析构为元组的项：
			§ 对于上述所有析构形式，等号右边的可析构为元组的项：均可为元组或已初始化的元组变量，均可为返回值为元组的方法
			§ 对于其一到其三的析构形式：不能直接用于析构含“解构函数”的class、record和struct实例
			§ 对于其四到其七的析构形式：可用于直接析构含“解构函数”的class、record和struct实例
			§ 对于除了其三所述以外的其他析构形式：等号右边指向元组的元素数量必须和左侧相同，类型必须和左侧元组依次一一类型兼容

 * 元组比较
	• 元组支持 == 和 != 运算符
	• 元组相等性比较是按照元组元素的顺序将左侧操作数的成员与相应的右侧操作数的成员进行比较
	• 元组相等性比较不会考虑元组字段名称即不考虑元组元素的变量名
	• 同时满足以下两个条件，两个元组可比较：
		○ 两个元组具有相同数量的元素
		○ 对于每个对应位置的两个元素，可以使用 == 和 != 运算符进行比较

 * 元组的用例
	• 作为方法的返回类型：可以在适合的情况下使用元组作为返回类型，而不是定义out方法参数
	• 作为方法的out参数：当out参数的数量较多时，可以考虑将多个out参数组合为一个out元组参数，或者重构方法为返回一个元组类型
	• 用于析构record类型实例或者含“解构函数”的class或struct实例
    • 其它需要用到这类轻型数据结构时进行使用
 */
using LearnCSharp.Basic.LearnTupleSpace;
using System.Runtime.CompilerServices;


namespace LearnCSharp.Basic
{
    internal class LearnTuple
    {
		/*【10601：元组代码示例】*/
        public static void StartLearnTuple()
        {
            Console.WriteLine("\n------示例：元组------\n");

            //使用如下代码分别声明并初始化三个元组
            var (x,y) = (Random.Shared.Next(-999, 1000), Random.Shared.Next(-999, 1000));
			(double X, double Y) point2d = (Random.Shared.Next(-999, 1000), Random.Shared.Next(-999, 1000));
			(double, double) point2d1 = (Random.Shared.Next(-999, 1000), Random.Shared.Next(-999, 1000));

            Console.WriteLine("元组var (x, y)可以使用三种方法访问其元组元素：");
            Console.WriteLine($"方法一--通过变量名直接访问：(x, y) --- ({x}, {y})");
            Console.WriteLine($"方法一--通过元组本身访问：(x, y) --- ({(x, y).Item1}, {(x, y).Item2})");
            Console.WriteLine($"方法一--通过元组和变量名直接访问：(x, y) --- ({(x, y).x}, {(x, y).y})");
            Console.WriteLine();

            Console.WriteLine("元组(double X, double Y)point2d可以使用两种方法访问其元组元素：");
            Console.WriteLine($"方法一--通过元组变量访问：point2d --- ({point2d.Item1}, {point2d.Item2})");
			Console.WriteLine($"方法一--通过元组变量和变量名直接访问：point2d --- ({point2d.X}, {point2d.Y})");
            Console.WriteLine();

            Console.WriteLine("元组(double, double)point2d1只能通过元组变量名.Item方式访问元组元素：");
            Console.WriteLine($"方法--通过元组变量访问：point2d1 --- ({point2d1.Item1}, {point2d1.Item2})");
            Console.WriteLine();

            //使用如下代码可对上述元组一和元组三进行相等性比较，因为两个元组元素数量相同，对应元素类型兼容且支持相等性比较：

            bool isEqual = (x, y) == point2d1;

			Console.WriteLine($"对比元组(x, y)与元组point2d1相等性：{isEqual}");
			Console.WriteLine();

            //使用如下代码声明并初始化一个Point实例point，并使用元组对其解构和输出结果：
            double pointX = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
            double pointY = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
			Point point = new Point(pointX, pointY);

			//point2d = point;//具名元组不能用来解构record实例或者含有“解构函数”的class和struct实例
			//var point2d2 = point;//不能使用var关键字声明的变量来隐式析构record实例为元组，因为编译器会将实例赋给变量，并将变量推导为实例的类型
			
			//解构point结构实例
			var(px, py) = point;

            Console.WriteLine($"point结构实例信息：\n{point}\n通过元组解构：（{px}，{py}）");
            Console.WriteLine();

			//使用如下代码来使用元组析构GetStringInfo方法的返回值，并输出结果：
			(int Length, int CountSpaceChar, string @String) stringInfo;
			stringInfo = GetStringInfo("I Love .NET! I Love C#!");

            Console.WriteLine($"stringInfo--output:(Length:{stringInfo.Length}, CountSpaceChar:{stringInfo.CountSpaceChar}, String:{stringInfo.@String})\n" + "-------------------------------------------------------------------------\n");
        }

		//定义一个返回值为元组的方法
        private static (int Length, int CountSpaceChar, string @String) GetStringInfo(string inputString)
		{
			int length = inputString.Length;
			int countSpaceChar = inputString.Count(a => a == ' ');
			string @string = inputString;
			return (length, countSpaceChar, @string);
		}
    }
}

namespace LearnCSharp.Basic.LearnTupleSpace
{
	internal readonly struct Point
	{
		public double X { get; }
		public double Y { get; }

		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public void Deconstruct(out double x, out double y)
		{
			x = X;
			y = Y;
		}

		public override string ToString() 
		{
			return $"Point结构 --- 【属性】 --- X：{X} | Y：{Y}";
		}
    }
}
