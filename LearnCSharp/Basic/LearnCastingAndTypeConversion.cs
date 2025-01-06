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
namespace LearnCSharp.Basic
{
    internal class LearnCastingAndTypeConversion
    {
		public static void StartLearnCastingAndTypeConversion() 
		{
			string outputString = "使用以下代码示例内置值类型的隐式转换：\n\n" +
				"int maxInt = int.MaxValue;\nlong int64 = maxInt;\n\n" +
				"int类型可以安全且隐式转换为long类型，不丢失精度。int64--output:";

            int maxInt = int.MaxValue;
			long int64 = maxInt;

			Console.WriteLine(outputString + $"{int64}\n");

			outputString = "使用以下代码示例内置值类型的显示转换：\n\n" +
                "double pi = 3.1415926;\nint integer = (int)pi;\n\n" +
                "double类型显示式转换为int类型，丢失精度，小数部分舍去。integer--output:";

            double pi = 3.1415926;
			int integer = (int)pi;
			//以下代码进行的强制转换会引发异常，因为int类型的容量无法存储double类型的最大值
			//double number = double.MaxValue;
			//int integer = (int)number

			Console.WriteLine(outputString + $"{integer}\n");

			outputString = "使用以下代码示例内置帮助程序类为非兼容内置类型进行转换：\n\n" +
                "byte[] bytes = new byte[10] { 12, 13, 15, 1, 0, 5, 255, 13, 3, 10 };\nint integerFromBytes = System.BitConverter.ToInt32(bytes,0);\n\n" +
				"使用System.BitConverter类的ToInt32方法将字节数组bytes转换为int类型。integerFromBytes--output:";

			byte[] bytes = new byte[10] { 12, 13, 15, 1, 0, 5, 255, 13, 3, 10 };
			int integerFromBytes = System.BitConverter.ToInt32(bytes,0);

			Console.WriteLine(outputString + $"{integerFromBytes}\n");
		}
    }
}
