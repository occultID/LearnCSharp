/*【学习C#内置的基本类型】
 * 基本类型
	• C#内置了一组基本值类型和一组基本引用类型
	• 这些预定义的基本类型都有保留关键字与其对应
	• 这些预定义的基本类型除了dynamic类型以外都是对应.NET类型的别名，彼此可以互换使用

 * 内置值类型
	• C#内置了一组预定义的值类型，其类型分类、关键字与对应.NET类型如下
		○ 整数类型
			§有符号整数类型
				sbyte	System.Sbyte	8位整数	        -128~127
				short	System.Int16	16位整数    	-3,2768~3,2767
				int 	System.Int32	32位整数    	-21,4748,3648~21,4748,3647
				long	System.Int64	64位整数	    -922,3372,0368,5477,5808~922,3372,0368,5477,5807
				nint	System.IntPtr	32位或64位整数	取决于运行时的平台CPU架构
			§无符号整数类型
				byte	System.Byte   	8位整数     	0~255
				ushort	System.UInt16	16位整数	    0~6,5535
				uint	System.UInt32	32位整数	    0~42,9496,7295
				ulong	System.UInt64	64位整数	    0~1,844,6744,0737,0955,1615
				nuint	System.UIntPtr	3位2或64位整数	取决于运行时的平台CPU架构
		○ 浮点数类型
			float	System.Single	4个字节	精度有效数字6~9位	±1.5 x 10^−45 ~ ±3.4 x 10^38
			double	System.Double	8个字节	精度有效数字15~17位	±5.0 × 10^−324 ~ ±1.7 × 10^30
			decimal	System.Decimal	16个字节	精度有效数字28~29位	±1.0 x 10^-28 ~ ±7.9228 x 10^28
		○ 字符类型
			char	System.Char	16位	U+0000~U+FFFF
		○ 布尔类型
			bool	System.Boolean	32位	Ture/False
		
 * 内置引用类型
	• C#内置了一组预定义的简单引用类型，其类型关键字和对应.NET类型如下
		object	System.Object	所有.NET类型的最终基类
		string	System.String	零个或多个Unicode字符的序列
		dynamic	System.Object	动态类型。表示变量的使用和对其成员的引用绕过编译时类型检查，改为在运行时解析这些操作。
	
 * 可以了解一下的知识点：
   非托管类型
	• 如果某个类型是以下类型之一，则它是非托管类型：
		○ 所有内置值类型
		○ 任何枚举类型
		○ 任何指针类型
		○ 任何仅包含非托管类型字段的用户定义的结构类型

 */

using System.Runtime.InteropServices;

namespace LearnCSharp.Basic
{
    internal class LearnSimpleType
    {
        /*【学习整型数值类型】
         *	整型数值类型：表示整数。 
		 *	所有的整型数值类型均为值类型。 
		 *	它们还是简单类型，可以使用文本进行初始化。 
		 *	所有整型数值类型都支持算术、位逻辑、比较和相等运算符。*/
        public static void LearnIntegerType()
        {
			Console.WriteLine($"sbyte类型，对应.NET的{typeof(sbyte)}类型，它表示内存占用{sizeof(sbyte) * 8}位整数，范围包含[{sbyte.MinValue},{sbyte.MaxValue}]");
            Console.WriteLine($"short类型，对应.NET的{typeof(short)}类型，它表示内存占用{sizeof(short) * 8}位整数，范围包含[{short.MinValue},{short.MaxValue}]");
            Console.WriteLine($"int类型，对应.NET的{typeof(int)}类型，它表示内存占用{sizeof(int) * 8}位整数，范围包含[{int.MinValue},{int.MaxValue}]");
            Console.WriteLine($"long类型，对应.NET的{typeof(long)}类型，它表示内存占用{sizeof(long) * 8}位整数，范围包含[{long.MinValue},{long.MaxValue}]");
            Console.WriteLine($"byte类型，对应.NET的{typeof(byte)}类型，它表示内存占用{sizeof(byte) * 8}位整数，范围包含[{byte.MinValue},{byte.MaxValue}]");
            Console.WriteLine($"ushort类型，对应.NET的{typeof(ushort)}类型，它表示内存占用{sizeof(ushort) * 8}位整数，范围包含[{ushort.MinValue},{ushort.MaxValue}]");
            Console.WriteLine($"uint类型，对应.NET的{typeof(uint)}类型，它表示内存占用{sizeof(uint) * 8}位整数，范围包含[{uint.MinValue},{uint.MaxValue}]");
            Console.WriteLine($"ulong类型，对应.NET的{typeof(ulong)}类型，它表示内存占用{sizeof(ulong) * 8}位整数，范围包含[{ulong.MinValue},{ulong.MaxValue}]");
			Console.WriteLine();

			//nint、nuint是“native sized integer”，其大小是和运行时的平台CPU架构决定的
			//故查询其内存占用大小时需放置在unsafe语句块中
			unsafe
			{
				Console.WriteLine("当前平台CPU架构：{0}", RuntimeInformation.OSArchitecture);
                Console.WriteLine($"nint类型，对应.NET的{typeof(nint)}类型，它当前表示内存占用{sizeof(nint) * 8}位整数，范围包含[{nint.MinValue},{nint.MaxValue}]");
                Console.WriteLine($"nuint类型，对应.NET的{typeof(nuint)}类型，它当前表示内存占用{sizeof(nuint) * 8}位整数，范围包含[{nuint.MinValue},{nuint.MaxValue}]");
            }		
		}

        /*【学习浮点数值类型】
         *	浮点数值类型：表示实数。 
         *	所有浮点型数值类型均为值类型。 
         *	它们还是简单类型，可以使用文本进行初始化。 
         *	所有浮点数值类型都支持算术、比较和相等运算符。*/
        public static void LearnFloating_pointType()
        {
            Console.WriteLine($"float类型，对应.NET的{typeof(float)}类型，该类型内存占用{sizeof(float)}字节，精度约6~9位有效数字，范围包含[{float.MinValue},{float.MaxValue}]");
            Console.WriteLine($"double类型，对应.NET的{typeof(double)}类型，该类型内存占用{sizeof(double)}字节，精度约15~17位有效数字，范围包含[{double.MinValue},{double.MaxValue}]");
            Console.WriteLine($"decimal类型，对应.NET的{typeof(decimal)}类型，该类型内存占用{sizeof(decimal)}字节，精度约28~29位有效数字，范围包含[{decimal.MinValue},{decimal.MaxValue}]");
        }

        /*【学习字符类型】
		 *	字符类型：它表示 Unicode UTF-16 字符。
         *	char 类型关键字是 .NET System.Char 结构类型的别名
         *	类型支持比较、相等、增量和减量运算符。 
         *	此外，对于 char 操作数，算数和逻辑位运算符对相应的字符代码执行操作，并得出 int 类型的结果。
         */
        public static void LearnCharacterType()
		{
            Console.WriteLine($"char类型，对应.NET的{typeof(char)}类型，该类型内存占用{sizeof(char)}字节，它表示UTF-16字符，范围包含[{char.MinValue},{char.MaxValue}]");
        }

        /*【学习布尔类型】 
         *	布尔类型：表示一个布尔值，可为 true 或 false。
		 *	bool 类型关键字是 .NET System.Boolean 结构类型的别名。
		 *	若要使用 bool 类型的值执行逻辑运算，请使用布尔逻辑运算符。 
		 *	bool 类型是 比较和相等运算符的结果类型。  
		 *	bool 表达式可以是 if、do、while 和 for 语句中以及条件运算符 ?: 中的控制条件表达式。
		 *	bool 类型的默认值为 false
		 *	bool 类型的变量命名时应尽量选择具有判断意义的词汇，比如isAnimal、hasValue等等
		 */
        public static void LearnBooleanType()
		{
            Console.WriteLine($"bool类型，对应.NET的{typeof(bool)}类型，该类型内存占用{sizeof(bool)}字节，它表示一个布尔值，可为[{bool.TrueString} 或 {bool.FalseString}]\n");

			//举例，声明一个bool类型变量，其接收表达式3.67 is int返回的bool值，表示3.67是否是一个整数
			bool isInteger = 3.67 is int;

			string outputString = "声明一个bool类型变量，其接收表达式3.67 is int返回的bool值，表示3.67是否是一个整数:\n\n" +
				"bool isInteger = 3.67 is int;\n\n" +
				$"isInteger值为：{isInteger}\n";

			Console.WriteLine(outputString);
        }

        /*【学习object类型】 
         *	object 类型是 System.Object 在 .NET 中的别名。 
		 *	在 C# 的统一类型系统中，所有类型（预定义类型、用户定义类型、引用类型和值类型）都是直接或间接从 System.Object 继承的。 
		 *	可以将任何类型的值赋给 object 类型的变量。 可以使用文本 null 将任何 object 变量赋值给其默认值。 
		 *	将值类型的变量转换为对象的过程称为装箱。 将 object 类型的变量转换为值类型的过程称为取消装箱。*/
		public static void LearnObjectType()
		{
            Console.WriteLine($"object类型，对应.NET的{typeof(object)}类型，它是.NET类型系统中所有类型的最终基类");
			//可以将任何类型的值赋给object对象，可使用文本null对其赋予默认值
			object? o1 = null;

			object? o2 = new String("你好");
			object? o3 = 5; //装箱
			int number = (int)o3; //拆箱			
        }

        /*【学习字符串类型】 
         *	字符串类型：表示零个或多个 Unicode 字符的序列，即char值的序列
		 *	string 是 System.String 在 .NET 中的别名。
		 *	尽管 string 为引用类型，但是定义相等运算符 == 和 != 是为了比较 string 对象（而不是引用）的值。 
		 *	基于值的相等性使得对字符串相等性的测试更为直观。
		 *	字符串是不可变的，即：字符串对象在创建后，其内容不可更改。
		 *	可以使用 + 连接两个字符串
		 *  
		 *	虽然字符串类型很多时候表现得会让我们觉得这是一个值类型，但实际上它是一个预定义好的引用类型。
		 */
        public static void LearnStringType()
		{
            Console.WriteLine($"string类型，对应.NET的{typeof(string)}类型，它是预定义好的用于表示零个或多个char值序列的引用类型");

			string s1 = "这是一个字符串";
			string s2 = "这是一个";
			string s3 = "这是一个字符串";
			string s4 = "这是一个新字符串";

			string outputString = "下面声明并初始化四个字符串：\n\n" +
				"string s1 = \"这是一个字符串\";\n" +
				"string s2 = \"这是一个\";\n" +
				"string s3 = \"这是一个字符串\";\n" +
				"string s4 = \"这是一个新字符串\";\n" +
				"\n代码执行之后观察结果：\n" +
				$"s1与s2比较--是否指向同一对象：{object.ReferenceEquals(s1, s2)} | 是否值相等：{s1 == s2}\n" +
				$"s1与s3比较--是否指向同一对象：{object.ReferenceEquals(s1, s3)} | 是否值相等：{s1 == s3}\n" +
				$"s1与s4比较--是否指向同一对象：{object.ReferenceEquals(s1, s4)} | 是否值相等：{s1 == s4}\n";

			s2 = s2 + "字符串";

            outputString = outputString + "\n我们重新为s2连接一个“字符串”使其值和s1值一样后再重新比较一次：\n\n" +
				"s2 = s2 + \"字符串\";\n\n" +
				$"此时s2的值：{s2}\n" +
				$"s1与s2比较--是否指向同一对象：{object.ReferenceEquals(s1, s2)} | 是否值相等：{s1 == s2}\n";

			Console.WriteLine(outputString);

            outputString = "由于string类型是零个或多个char值序列，所以我们可以如同访问数组元素一样访问每一个char值：\n" +
				"以s1为例，我们可以用以下代码访问其第四个char值或遍历输出每一个char值：\n\n" +
				"Console.WriteLine(\"s1中第四个char值为：{0}\", s1[3]);\n" +
				"for (int i = 0; i < s1.Length; i++)\n" +
				"{\n    Console.WriteLine(\"这是s1中第{0}个char值：{1}\", i + 1, s1[i]);\n}\n";

			Console.WriteLine(outputString);
			Console.WriteLine("s1中第四个char值为：{0}\n", s1[3]);
			for (int i = 0; i < s1.Length; i++)
			{
				Console.WriteLine("这是s1中第{0}个char值：{1}", i + 1, s1[i]);

            }
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
        public static void LearnDynamicType()
		{
            Console.WriteLine($"dynamic类型，它表示变量的使用和对其成员的引用绕过编译时类型检查。 改为在运行时解析这些操作。");

			//分别声明并初始化一个dynamic变量和objec变量
			dynamic dyn = 100d;
			object obj = 100d;
			
			//重新进行赋值操作
			dyn = dyn + 3;   //动态类型不会在编译时进行类型检查，所以这样写可以通过编译
			//obj = obj + 3; //这里注释了该语句是因为会编译不通过，因为object类型在编译时已经确定类型，其不支持+运算符

			string outputString = "分别声明并初始化一个dynamic变量和objec变量：\n" +
				"dynamic dyn = 100d\n" +
				"object obj = 100d\n" +
				$"执行代码后获取变量实际类型--dyn：{dyn.GetType()}  obj：{obj.GetType()}\n" +
				"如果进行如下重新赋值操作，dynamic能通过编译，object则无法：\n" +
				"dyn = dyn + 3\n" +
				"obj = obj + 3\n" +
				$"dyn运行时确定类型后可成功计算并得到结果:{dyn}\n";

			Console.WriteLine(outputString);
        }




		
		public static void StartLearnSimpleType()
		{
            string title = "001 整数类型\n" +
                "002 浮点数类型\n" +
                "003 字符类型\n" +
                "004 布尔类型\n" +
                "005 object类型\n" +
                "006 字符串类型\n" +
                "007 动态类型";

            do
            {
                Console.WriteLine("【C#内置简单类型语句学习】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnIntegerType(); break;
                    case "002": LearnFloating_pointType(); break;
                    case "003": LearnCharacterType(); break;
                    case "004": LearnBooleanType(); break;
                    case "005": LearnObjectType(); break;
                    case "006": LearnStringType(); break;
                    case "007": LearnDynamicType(); break;
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
