/*【学习变量】
 * 变量、对象与内存
	• 变量：参考C#语言定义文档
		○ 变量是以变量名所对应的内存地址为起点，以其数据类型所要求的存储空间为长度的一块内存区域
		○ 表面上来看，变量的用途是存储数据
		○ 实际上，变量表示了存储位置，并且每个变量都有一个类型，以决定什么样的值能够存入变量
		○ 变量种类（7种）
			§ 静态变量：用static修饰符声明的字段称为静态变量
			§ 实例变量：未用static修饰符声明的字段
			§ 数组元素
			§ 值参数：未用ref或out修饰符声明的参数
			§ 引用参数：使用ref修饰符声明的参数
			§ 输出形参：使用out修饰符声明的参数
			§ 局部变量
			§ 示例：
			class Student
			{
			        public static amount;//声明一个静态整型变量表示学生总人数
			        public string studentName;//声明一个字符串类型变量表示姓名
			        public int[] stuLinked;//声明一个数组存储学生多个联系方式
			        public int GetScore(string course){}//course即为参数
			} 
		○ 狭义的变量指局部变量，因为其他种类的变量都有自己的约定名称
			§ 简单地讲，局部变量就是方法体（函数体）里声明的变量
		○ 变量的声明
			§ 有效的修饰符组合opt  类型  变量名  初始化器opt
	• 值类型变量
		○ 以byte/sbyte/short/ushort为例
		○ 值类型没有实例，所谓的“实例”与变量合而为一
		○ 在内存中的存储方式：
			§ 在stack上分配内存
			§ 声明了一个值类型变量，在程序运行时在stack有空余的空间上寻找到一块可以放下这个变量的区域，这个区域的起点即为变量名所对应的首地址，区域的大小由该变量的数据类型所需要的存储空间决定。
	• 引用类型的变量与实例
		○ 引用类型变量与实例的关系：引用类型变量里存储的数据是对象的内存地址
		○ 在内存中的存储方式：
			§ 在heap上分配内存存储实例，在stack上分配一块4字节（32bit）大小的内存空间存储引用变量实例的地址
	• 局部变量是在stack上分配内存
	• 变量的默认值
		○ 以下类别的变量会自动初始化为其默认值
			§ 静态变量
			§ 类实例的实例变量 
			§ 数组元素
		○ 变量的默认值取决于变量的类型，并按如下所示确定：
			§ 对于值类型变量，默认值与值类型的默认构造函数计算的值相同
			§ 对于引用类型变量，默认值为null
		○ 明确赋值
			§ 变量在使用(读取值)前必须明确赋值
	• 常量（值不可以改变的变量）
	• 装箱与拆箱（Boxing & Unboxing）
 */

namespace LearnCSharp.Basic
{
    internal class LearnVariables
    {
		/*【11501：静态变量】
		 *	使用static修饰符声明的字段称为静态变量
		 *	静态变量将在执行其包含类型的静态构造函数之前存在，并在关联的应用程序停止运行时结束生命周期
		 *	静态变量的初始值是其声明类型的默认值，也可以在声明时进行显示赋值操作
		 *	出于所有静态变量都必须明确赋值，故所有静态变量都会默认赋予其声明类型的默认值
		 *	静态变量只能是类、结构的静态成员，不能在属性、方法等内部声明，C#没有局部静态变量
		 *	静态变量属于类或结构本身，不能通过类或结构实例来访问，在类和结构内部可以被任何成员访问
		 */
		private static string staticVariable = "这是一个静态变量";//这是一个静态变量，在类中被称为静态字段
		public static void LearnStaticVariables() => Console.WriteLine(staticVariable);

        /*【11502：实例变量】
		 *	不使用static修饰符声明的字段称为实例变量
		 *	实例变量只能是类或结构的实例成员，且其生命周期和类或结构的实例生命周期同步
		 *	实例变量在类或结构内部不能被静态成员访问
		 */
        private string instanceVariable = "这是一个实例变量";
		public static void LearnInstanceVariables() => Console.WriteLine(new LearnVariables().instanceVariable);

		/*【11503：数组元素】
		 *	数组元素，即数组的元素
		 *	当创建数组实例时，数组的元素便会存在，并且当该数组被释放时，其元素的生命周期同时结束
		 *	数组中每个元素都具有初始默认值，即元素数据类型的默认值
		 */
		public static void LearnArrayElements()
		{
			char[] chars = new char[] { 'I', ' ', 'L', 'O', 'V', 'E', ' ', 'C', '#' };
			for (int i = 0; i < chars.Length; i++)
			{
				Console.WriteLine("这是第{0}个数组元素：{1}", i + 1, chars[i]);
			}
		}

        /*【11504：传值参数】
		 *	不带ref、out或in修饰符声明的方法参数
		 *	值参数可在调用函数成员或参数所属的匿名函数时存在，并使用调用中给定的实参的值进行初始化
		 *	值参数的生命周期通常在函数执行结束时结束，但如果该参数被一个委托实例的匿名函数捕获，则会延长至该委托实例被释放的时间
		 *	参数学习具体内容参考 LearnParameter部分
		 */
        public static void LearnValueParameters()
		{
			LearnParameter.LearnValueParameter();
		}

        /*【11505：引用参数】
		 *	使用ref修饰符声明的方法参数
		 *	引用参数不会创建新的存储位置，相反，引用参数将实参的存储位置进行传递
		 *	引用参数的值始终与实参的值相同
		 *	参数学习具体内容参考 LearnParameter部分
		 */
        public static void LearnReferenceParameters()
		{
			LearnParameter.LearnReferenceParameter();
		}

		/*【11506：输出参数】
		 *	使用out修饰符声明的方法参数
		 *	输出参数是一种特殊的引用参数，同样不会创建新的存储位置，而是将实参的存储位置进行传递
		 *	输出参数的值始终与实参的值相同
		 *	方法在执行到每一个终结点前都必须明确为输出参数进行赋值
		 *	参数学习具体内容参考 LearnParameter部分
		 */
		public static void LearnOutputParameters()
		{
			LearnParameter.LearnOutputParameter();
		}

        /*【11507：局部变量】
         * 局部变量：变量是存储位置的符号名称，程序以后可以对该存储位置进行赋值和修改。
                    局部 意味着变量在方法或代码块（一对{}）内部声明，其作用域“局部”于当前代码块
                    声明变量：即定义一个变量，我们需要：
                                    指定变量要包含的数据的类型
                                    为它分配标识符，即变量名
                    初始化变量：使用等号“=”为变量赋值，等号左边为变量名，右边为兼容变量类型的变量值
         */
        public static void LearnLocalVariable()
        {
            int number = 5;//声明一个类型为 int 名字为 number 的整型变量，并使用整数 5 对其进行初始化赋值
            string words = "你好啊！";//声明一个类型为 string 名字为 words 的字符串变量，并使用 你好啊 对其进行初始化赋值

			Console.WriteLine($"输出局部变量值--number：{number}，words：{words}");
        }

		public static void StartLearnVariables()
		{
			string title = "001 静态变量\n" +
				"002 实例变量\n" +
				"003 数组元素\n" +
				"004 值参数\n" +
				"005 引用参数\n" +
				"006 输出参数\n" +
				"007 局部变量";

            do
            {
                Console.WriteLine("【学习变量】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnStaticVariables(); break;
                    case "002": LearnInstanceVariables(); break;
                    case "003": LearnArrayElements(); break;
                    case "004": LearnValueParameters(); break;
                    case "005": LearnReferenceParameters(); break;
                    case "006": LearnOutputParameters(); break;
                    case "007": LearnLocalVariable(); break;
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
