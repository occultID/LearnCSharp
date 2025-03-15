/*【学习类型转换】
 * 类型转换
	• 为什么需要类型转换：
		○ .NET预定义了大量类型，用户也能自定义无限数量的类型，所以类型之间必然会出现相互转换的需求
	• 什么是类型转换
		○ 将一个类型转换为另一种类型就是类型转换
		○ 类型转换包括三种情况
			§ 转型，也称为隐式类型转换，包含.NET内置支持的隐式转换和用户自定义的隐式转换
			§ 强制类型转换，也称为显示类型转换，包含.NET内置支持的显式转换和用户自定义的显示转换
			§ 借助类型内部方法或者帮助程序类来进行非兼容类型之间的转换
	• 两个类型能够进行转换至少要满足以下任一前提
		○ 这两个类型是.NET内置支持隐式/显示转换的关联类型
		○ 这两个类型存在继承关系
		○ 至少其中一个类型内部必须定义隐式或显示类型转换运算符关联这两个类型
			§ 类型转换运算符的参数即操作数为待转换类型
			§ 类型转换运算符的返回值为目标转换类型
		○ 至少其中一个类型内部定义了用于关联转换这两个类型的方法
		○ 这两个类型是.NET内置类型且非兼容类型，但内置帮助程序类提供了方法进行转换

 * 隐式类型转换
	• 隐式转型要求转换过程中不会丢失数据，而且不会抛出异常（无论操作数的类型是什么）
	• 从一个类型转换为另一个类型时，不会发生精度丢失或数据丢失，而且值不会发生根本性的改变，则代码只需指定赋值运算符，转换将隐式地发生
	• 两个类型能够进行隐式类型转换必须满足以下任一前提：
		○ 至少其中一个类型内部定义了关联这两个类型的隐式转换运算符
		○ 一个类型是另外一个类型的子类型
		○ 两个类型是.NET内置支持隐式转换的类型
	• 隐式转换的形式：
		○ 目标数据类型 变量名 = 待转换变量/待转换字面值/待转换表达式
	• 内置数值类型之间的预定义隐式转换：
		○ 待转换类型	目标类型
		   sbyte		short、int、long、float、double、decimal 或 nint。
		   byte			short、ushort、int、uint、long、ulong、float、double、decimal、nint 或 nuint
		   short		int、long、float、double、decimal 或 nint
		   ushort		int、uint、long、ulong、float、double、decimal、nint 或 nuint
		   int			long、float、double、decimal 或 nint
		   uint			long、ulong、float、double、decimal 或 nuint
		   long			float、double 或 decimal
		   ulong		float、double 或 decimal
		   float		double
		   nint			long、float、double 或 decimal
		   nuint		ulong、float、double 或 decimal
		○ 注意：
			§ 从整数类型到浮点数类型的隐式转换可能会丢失精准度，但绝不会丢失一个数量级
			§ 除上述情况外的其他隐式数值转换不会丢失任何信息
	
 * 显示类型转换
	• 显示类型转换就是需要开发者使用强制转换表达式才能执行的转换
	• 在转换中可能丢失信息时或在出于其他原因转换可能不成功时，必须进行强制转换
	• 两个类型能够进行显示类型转换必须满足以下任一前提：
		○ 至少其中一个类型内部定义了关联这两个类型的显示转换运算符
		○ 一个类型是另一个类型的父类
		○ 两个类型是.NET内置支持显示转换的类型
	• 显示转换的形式：
		○ 目标数据类型 变量名 = (目标数据类型)待转换变量/待转换字面值/待转换表达式
		○ 说明：()在这里为 强制转换器，其内部为目标数据类型
	• 内置数值类型之间的预定义显示转换:
		○ 待转换类型	目标类型
		   sbyte		byte、ushort、uint、ulong 或 nuint
		   byte			sbyte
		   short		sbyte、byte、ushort、uint、ulong 或 nuint
		   ushort		sbyte、byte 或 short
		   int			sbyte、byte、short、ushort、uint、ulong 或 nuint。
		   uint			sbyte、byte、short、ushort 或 int
		   long			sbyte、byte、short、ushort、int、uint、ulong、nint 或 nuint
		   ulong		sbyte、byte、short、ushort、int、uint、long、nint 或 nuint
		   float		sbyte、byte、short、ushort、int、uint、long、ulong、decimal、nint 或 nuint
		   double		sbyte、byte、short、ushort、int、uint、long、ulong、float、decimal、nint 或 nuint
		   decimal		sbyte、byte、short、ushort、int、uint、long、ulong、float、double、nint 或 nuint
		   nint			sbyte、byte、short、ushort、int、uint、ulong 或 nuint
		   nuint		sbyte、byte、short、ushort、int、uint、long 或 nint
		○ 注意：
			§ 显示数值转换可能会导致数据丢失精度或引发异常
			§ 异常通常为OverflowException
 * 使用帮助程序类进行转换
	• 若要在两个非兼容内置类型之间转换，可以使用内置的System.BitConverter类、System.Convert类或者内置数值类型的Parse方法。
	• 示例：使用int类型的Parse方法将字符串形式的数字字面量转换为int类型
		○ int integer = int.Parse("100");
 */
using System.Net.WebSockets;
using System.Numerics;
using System.Text.RegularExpressions;

namespace LearnCSharp.Basic
{
    internal class LearnCastingAndTypeConversion
    {
		/*【10701：隐式转换】*/
		public static void LearnImplicitConversion()
		{
            Console.WriteLine("\n------示例：隐式转换------\n");

			//以内置类型int、double为例示例隐式转换
			int integer = Random.Shared.Next(-999, 1000);
            double num = integer;

            Console.WriteLine($"原类型：{integer.GetType()} | 数据：{integer}");
            Console.WriteLine($"转换类型：{num.GetType()} | 数据：{num} | 转换形式：内置类型隐式转换");
            Console.WriteLine();

            //以用户定义类型的隐式转换为例示例隐式转换
            double x = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
            double y = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();

			(double, double) pointTuple = (x, y);
			Point point = pointTuple;

            Console.WriteLine($"原类型：{pointTuple.GetType()} | 数据：{pointTuple}");
            Console.WriteLine($"转换类型：{point.GetType()} | 数据：{point} | 转换形式：用户定义类型隐式转换");
            Console.WriteLine();
        }

		/*【10702：显示转换（强制转换）】*/
        public static void LearnExplicitCasting()
        {
            Console.WriteLine("\n------示例：显示转换------\n");

			//以内置类型int、double为例示例强制转换
			double num = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
			int integer = (int)num;

            Console.WriteLine($"原类型：{num.GetType()} | 数据：{num}");
            Console.WriteLine($"转换类型：{integer.GetType()} | 数据：{integer} | 转换形式：内置类型显示强制转换");
            Console.WriteLine();

			//以用户定义类型的显示转换为例示例强制转换
			double x = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
			double y = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();

			Point point = new Point(x, y);
			(int ix, int iy) pointTuple = ((int, int))point;

            Console.WriteLine($"原类型：{point.GetType()} | 数据：{point}");
            Console.WriteLine($"转换类型：{pointTuple.GetType()} | 数据：{pointTuple} | 转换形式：用户定义类型显示强制转换");
            Console.WriteLine();
        }

		/*【10703：类型转换工具类、类型转换方法】*/
		public static void LearnCastingTool()
		{
            Console.WriteLine("\n------示例：类型转换工具类、类型转换方法------\n");
            //使用内置BitConverter类为非兼容类型进行转换：
            byte[] bytes = new byte[10] { 12, 13, 15, 1, 0, 5, 255, 13, 3, 10 };
            int integerFromBytes = System.BitConverter.ToInt32(bytes, 0);

			Console.WriteLine($"原类型：{bytes.GetType()} | 数据：({string.Join('，', bytes)})");
            Console.WriteLine($"转换类型：{integerFromBytes.GetType()} | 数据：{integerFromBytes} | 转换形式：内置转换工具类BitConverter");
            Console.WriteLine();

			//使用Convert类进行类型转换，转换时高精度数字向低精度数字转换时会采取四舍五入而非截断方式
			decimal decimalNumber = 26.99m;
			int explicitInt = (int)decimalNumber;
			int convertInt = Convert.ToInt32(decimalNumber);

            Console.WriteLine($"原类型：{decimalNumber.GetType()} | 数据：{decimalNumber}");
            Console.WriteLine($"转换类型：{explicitInt.GetType()} | 数据：{explicitInt} | 转换形式：int强制类型转换");
            Console.WriteLine($"转换类型：{convertInt.GetType()} | 数据：{convertInt} | 转换形式：Convert类ToInt32方法类型转换");
			Console.WriteLine();

            //使用内置类型的Parse或TryParse方法进行转换：
            Console.Write("请输入一个数字：");
			string str = Console.ReadLine();
			if (double.TryParse(str, out double dNumber))
			{
                Console.WriteLine($"原类型：{str.GetType()} | 数据：{str}");
                Console.WriteLine($"转换类型：{dNumber.GetType()} | 数据：{dNumber} | 转换形式：double类型的TryParse方法");
            }
			else
				Console.WriteLine("转换失败");
            Console.WriteLine();

            //使用用户定义类型的Parse或TryParse方法进行转换：
            Console.Write("请输入一个元组（数值，数值）：");
			str = Console.ReadLine();
			if (Point.TryParse(str, out Point point)) 
			{
                Console.WriteLine($"原类型：{str.GetType()} | 数据：{str}");
                Console.WriteLine($"转换类型：{point.GetType()} | 数据：{point} | 转换形式：Point结构的TryParse方法");
            }
			else
                Console.WriteLine("转换失败");
            Console.WriteLine();
        }

        public static void StartLearnCastingAndTypeConversion() 
		{
            string title = "001 隐式转换\n" +
                "002 显示转换\n" +
                "003 类型转换工具类/方法";

            do
            {
                Console.WriteLine("【类型转换】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnImplicitConversion(); break;
                    case "002": LearnExplicitCasting(); break;
                    case "003": LearnCastingTool(); break;
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

		//定义一个内部结构用于演示用户自定义的类型转换，这里用于演示故未考虑设计规范。
		private readonly struct Point
		{
			public double X { get; }
			public double Y { get; }

			public Point(double x, double y)
			{
				X = x; 
				Y = y;
			}

			//自定义一个从Point结构到（double，double）元组的隐式转换
			public static implicit operator (double,double)(Point point)
			{
				return (point.X, point.Y);
			}

            //自定义一个从（double，double）元组到Point结构的隐式转换
            public static implicit operator Point((double,double) pointTuple) 
			{
				return new Point(pointTuple.Item1, pointTuple.Item2);
			}

            //自定义一个从Point结构到（int, int）元组的显示转换
            public static explicit operator (int, int)(Point point) 
			{
				return ((int)point.X, (int)point.Y);
			}

			//将一个元组字符串转换为Point结构
			public static Point Parse(string s)
			{
                string str = Regex.Replace(s, @"\s", "");
                str = Regex.Match(str, @"^[（(](-?\d+(\.\d+)?[,，]-?\d+(\.\d+)?)[)）]$").Value;
                str = Regex.Replace(str, @"[（()）]", "");
				if (string.IsNullOrWhiteSpace(str))
					throw new InvalidCastException();

				string[] numStrs = str.Split(',', '，');

				double x = 0, y = 0;

				try
				{
					x = double.Parse(numStrs[0]);
					y = double.Parse(numStrs[1]);
				}
				catch
				{
					throw new InvalidCastException();
				}

				return new Point(x, y);
            }

            //尝试将一个元组字符串转换为Point结构
            public static bool TryParse(string s, out Point point)
			{
                string str = Regex.Replace(s, @"\s", "");
                str = Regex.Match(str, @"^[（(](-?\d+(\.\d+)?[,，]-?\d+(\.\d+)?)[)）]$").Value;
                str = Regex.Replace(str, @"[（()）]", "");
				if (!string.IsNullOrWhiteSpace(str))
				{
                    string[] numStrs = str.Split(',', '，');
                    bool canGetX = double.TryParse(numStrs[0], out double x);
                    bool canGetY = double.TryParse(numStrs[1], out double y);

                    if (canGetX && canGetY)
                    {
                        point = new Point(x, y);
                        return true;
                    }
                }

				point = new Point(double.NaN, double.NaN);
				return false;
            }

            public override string ToString()
            {
				return $"Point --- ({X}, {Y})";
            }
        }
    }
}
