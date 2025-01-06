/*【学习运算符】
 * 运算符概览：参考C#语言定义规范
	• 运算符，也称为操作符
	• 运算符是用来操作数据的，被运算符操作的数据称为操作数(Operand)
		○ 操作数可以是文本(字面量)、字段、局部变量和表达式
	• C#内置的运算符如下所示，类别从上至下优先级依次降低，同一行中的运算符运算优先级是一样的
		条例				类别				运算符
		主要表达式			主					x.y、f(x)、a[i]、x?.y、x?[i]、x++、x--、x!、new、typeof、checked、unchecked、default、delegate、nameof、sizeof、stackalloc、x->y
		一元运算符			一元				+x、-x、!x、~x、++x、--x、^x、(T)x、await、&x、*x、true、false
		范围运算符			二元|范围			x..y
		switch和with表达式	switch和with表达式	switch、with
		算术运算符			二元|乘法			*、/、%
		算术运算符			二元|加减			+、-
		移位运算符			二元|移位			<<、>>
		关系类型检测运算符	二元|关系和类型检测	<、>、<=、>=、is、as
		关系类型检测运算符	二元|相等			==、!=
		逻辑运算符			二元|逻辑与			&
		逻辑运算符			二元|逻辑XOR		^
		逻辑运算符			二元|逻辑或			|
		条件逻辑运算符		二元|条件与			&&
		条件逻辑运算符		二元|条件或			||
		Null合并运算符		二元|null合并		??
		条件运算符			三元|条件			?:
		赋值运算符，		赋值和lambda表达式	=、*=、/=、%=、+=、-=、<<=、>>=、&=、^=、|=、??=、=>
		匿名函数表达式

 * 运算符的本质
	• 运算符的本质是函数（即算法）的“简记法”
		○ 举例解释：假如没有发明“+”运算符，只有Add函数，算式3+4+5将只能写成Add（Add（3,4）,5），当算式相加很复杂时，函数的表示也将更为复杂
	• 运算符不能脱离与它关联的数据类型
		○ 可以说运算符就是与固定数据类型相关联的一套基本算法的简记法
		○ 可以自定义数据类型重载运算符
	
 * 运算符的优先级与运算顺序
	• 运算符的优先级
		○ 可以使用圆括号提高被括起来的表达式的优先级
		○ 圆括号可以嵌套
		○ 嵌套的圆括号最里层的圆括号优先级最高
	• 同优先级运算符的运算顺序
		○ 除了带有赋值功能的运算符，同优先级运算符都是由左向右进行运算
		○ 带有赋值功能的运算符的运算顺序是由右向左
		○ 与数学运算不同，计算机语言的同优先级运算没有“结合律”
			§ 3+4+5在计算机语言中只能理解为Add(Add(3,4),5)，不能理解为Add(3,Add(4,5))

 * 运算符重载
	• 所有的运算符在C#中都有对应的预定义实现，其中所有的一元运算符和二元运算符都具有在任何表达式中自动可用的预定义实现
	• 除了预定义的运算符实现外，在有需求时可以在自定义类型或结构中含声明来引入用户自定义的运算符实现，这称为运算符重载
		○ 严格来说，这不算是自定义运算符，用户只能在自定义类型或结构中重新定义已有的运算符来实现对关联类型的操作
		○ 运算符重载时，必须指定至少一个操作数的类型为重载运算符所在的类型
		○ 只有以下运算符支持有用户自定义重载实现
			§ 可重载的一元运算符：+、-、！、~、++、--、true、false
			§ 可重载的二元运算符：+、-、*、/、%、&、|、^、<<、>>、==、!=、>、<、>=、<=
			§ 注意：重载二元运算符（+、-、*、/、%、&、|、^、<<、>>）时会自动重载其对应的复合赋值运算符（+=、-=、*=、/=、%=、&=、|=、^=、<<=、>>=）
			§ 注意：重载二元运算符（==、>、>=）同时必须重载二元运算符（!=、<、<=）
			§ 注意：true、false运算符的重载实际是使重载它的类型支持用于表示true或false，重载该运算符的返回值只能是bool值且必须两者同时实现
			§ 注意：对于隐式转换和显示转换，分别使用implicit和explicit关键字来定义重载
	• 重载运算符的优先级
		○ 用户定义的重载运算符的优先级始终优先于预定义的运算操作符
			§ 只有当不存在适用的用户定义运算符实现时，编译器才会考虑预定义的运算符重载
	• 重载运算符的方式
		○ 一元运算符--在包含类中声明并实现如下方法
			§ public static 返回数据类型 operator 运算符(操作数数据类型 形参名)
			   { 
					实现代码返回对应类型值
			   }
			§ 注意：当运算符为true或false时，返回数据类型必须为bool，但无论如何，返回数据类型不能为void
			§ 注意：运算符操作数即参数的数据类型必须为其包含类型
		○ 二元运算符--在包含类中声明并实现如下方法
			§ public static 返回数据类型 operator 运算符(左操作数数据类型 形参名, 右操作数数据类型 形参名)
			   {
					实现代码返回对应类型值
			   }
			§ 注意：当运算符为==、!=、>、<、>=、<=时，返回数据类型必须为bool，但无论如何，返回数据类型不能为void
			§ 注意：运算符的左右操作数即参数的数据类型，至少有一个必须是包含类型
		○ 隐式类型转换--在包含类中声明并实现如下方法
			§ public static implicit operator 返回数据类型(操作数数据类型 形参名)
			   {
					实现代码返回对应类型值
			   }
			§ 注意：返回数据类型和操作数数据类型中至少有一个必须为包含类型
		○ 显示类型转换--在包含类中声明并实现如下方法
			§ public static explicit operator 返回数据类型(操作数数据类型 形参名)
			   {
					实现代码返回对应类型值
			   }
			§ 注意：返回数据类型和操作数数据类型中至少有一个必须为包含类型
 */

using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;

namespace LearnCSharp.Basic
{
    internal class LearnOperator
    {
		//声明并定义一些字段用于后面学习运算符演示示例
		private static int[] integers = new int[5] { 1, 2, 3, 4, 5 };
		/*主要表达式操作数示例*/
		public static void LearnPrimaryExpressionsOperators()
		{
			string outputString = "成员访问运算符【.】：用于访问成员\n" +
				".左边为命名空间、类、结构、枚举、元组、记录等类型或其实例\n" +
				".右边为其包含可访问成员\n" +
				"【.】--示例：Week week = Week.Weekend; //访问Week枚举成员Weekend\n" +
				$"【Week.Weekend】--output:{Week.Weekend}\n";

			Console.WriteLine(outputString);

            outputString = "方法/委托调用运算符【()】：用于调用方法\n" +
                "()外部左边为方法名称或委托实例名称\n" +
                "()内部为方法的参数，无参则留空，多参数则每个参数使用逗号【,】分隔\n" +
                "【()】--示例：Console.Write(\"你好\"); //调用Console类的Write方法输出“你好”\n" +
                $"【Console.Write(\"你好\")】--output:";
			Console.Write("你好");
            Console.WriteLine(outputString);
			Console.WriteLine();

            outputString = "集合访问运算符【[]】：用于访问成员\n" +
                "[]外部左边为集合或数组或字符串类型的实例名称\n" +
                "[]内部为序号索引或范围索引\n" +
                "【[]】--示例：int a = integers[1];\n //访问Week枚举成员Weekend\n" +
				$"【integers[1]】--output:{integers[1]}\n";

            Console.WriteLine(outputString);

			outputString = "成员访问运算符【?.】和集合访问运算符【?[]】：用于访问成员\n" +
				"?.运算符的使用和.一致，区别在于它表示实例的类型可空，如果是空值则返回null，否则返回成员值" +
				"?[]运算符的使用和[]一致，区别在于它表示集合可空，如果空则返回空值，否则返回成员值\n";

            Console.WriteLine(outputString);

        }
        public static void StartLearnOperator()
		{
			LearnPrimaryExpressionsOperators();
        }

		//定义一个内部Point类来进行自定义运算符重载实现的示例
		//关于类我们后续会详细说明，这里只用知道这是我们自定义的一个类型
        private class Point
		{
			//声明并定义两个属性表示坐标点的X和Y值
			int X { get; set; }
			int Y { get; set; }

			//自定义一个隐式类型转换，可以将Point类型隐式转换成(double,double)元组类型
			public static implicit operator (double,double)(Point point)
			{
				return (point.X, point.Y);
			}

			//自定义一个显示类型转换，可以将(double, double)元组类型强制转换为Point类型
			public static explicit operator Point((double,double) pointTuple)
			{
				return new Point() { X = (int)pointTuple.Item1, Y = (int)pointTuple.Item2 };
			}

			//重载一个二元-的实现用于计算两个Point类型表示的坐标点的距离
			public static double operator -(Point pointLeft,Point pointRight) 
			{
				int subX = pointLeft.X - pointRight.X;
				int subY = pointLeft.Y - pointRight.Y;

				return Math.Sqrt(subX * subX + subY * subY);
			}

			//重载一个一元-的实现用于返回Point类型表示的坐标点关于原点的对称点
			public static Point operator -(Point point)
			{
				return new Point() { X = -point.X, Y = -point.Y };
			}

			//重载一对特殊的一元运算符true/false
			public static bool operator true(Point point)
			{
				return point.X > 0 && point.Y > 0;
			}
			public static bool operator false(Point point)
			{
				return point.X < 0 && point.Y < 0;
			}
		}


    }
}
