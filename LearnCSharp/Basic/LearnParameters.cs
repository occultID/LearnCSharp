/*【学习参数】
 * 参数
	• 参数是方法的可选签名之一，拥有参数的方法将利用参数进行数据传递
	• 参数分为实参和形参
		○ 在定义方法时，为方法声明的参数称为形式参数--Parameters
		○ 在调用方法时，作为形参值传递给方法的参数称为实参--Argument
		○ 实参可以按值或按引用传递给形参
			§ 按值传递就是将实参副本传递给方法
			§ 按引用传递就是将对实参的访问（即引用地址或存储位置）传递给方法
			§ 值类型的实参变量直接包含其数据
			§ 引用类型的实参变量包含对其数据的引用

 * 参数分类
	• 传值参数
		○ 不使用ref、out和in修饰符声明的形式参数
	• 引用参数
		○ 使用ref修饰符声明的形式参数，调用时传递的实参也需要使用ref修饰
	• 输出参数
		○ 使用out修饰符声明的形式参数，调用时传递的实参也需要使用out修饰
	• 只读参数
		○ 使用in修饰符声明的形式参数，调用时传递的实参也需要使用in修饰
	• 数组参数
		○ 使用params修饰的数组类型参数
		○ 数组参数应放置于参数列表最后
	• 可选参数
		○ 具有默认值的形式参数，在方法调用时可写参数，也可不写，不写则使用默认值
		○ 可选参数应放置在参数列表数组参数之前，其余参数之后
	• this参数
		○ 用于定义扩展方法时使用
		○ 必须处于方法参数列表第一个位置
	• 注意：
		○ ref、out、in修饰的参数不能用于以下方法
			§ 异步方法，通过使用async修饰符定义
			§ 迭代器方法，包括yield return或yield break语句
			§ 扩展方法的第一个参数不能有in修饰符，除非该参数时结构
            § 扩展方法的第一个参数，其中该参数是泛型类型（即使该类型被约束为结构）

 * 通过参数调用方法
	• 在调用含参数的方法时，需提供类型和数量与方法参数列表匹配的实际参数
	• 具名参数
		○ 在调用方法时，可以在参数列表以 形参名:实参表达式 的形式传递参数
		○ 所有形参都以具名参数传递值时，可以不考虑参数列表的顺序
 */
using System.Text;

namespace LearnCSharp.Basic
{
    internal static class LearnParameter
    {
        /*【11601：传值参数--代码示例】
         	传值参数将实参传递给形参的形式
				实参是值类型   --将实参的值拷贝一份给形参
				实参是引用类型 --将实参存储的引用地址拷贝一份给形参
         */
        public static void LearnValueParameter()
		{
            Console.WriteLine("\n------示例：传值参数------\n");
            //定义三个局部方法用于示例传值参数
            //该方法传值参数传递的实参是一个值类型
            void PassAValueType(int x)
			{
				Console.WriteLine($"传值参数-传递值类型-形参值：{x}");
				x += x;
				Console.WriteLine($"传值参数-传递值类型-形参重新赋值：{x}");
			}
			//该方法传值参数传递的实参是一个引用类型变量，并且会为该变量创建新实例
			void PassARefTypeButNewObject(Point2d point2D)
			{
				Console.WriteLine($"传值参数-传递引用类型-形参值：({point2D.X}, {point2D.Y})");
				point2D = new Point2d(point2D.X + 5, point2D.Y + 5);
				Console.WriteLine($"传值参数-传递引用类型-为形参新建一个实例-形参新值：({point2D.X}, {point2D.Y})");
			}
			//该方法传值参数传递的实参是一个引用类型变量，并且会修改该变量实例的成员值
			void PassARefTypeButChangeObject(Point2d point2D)
			{
				Console.WriteLine($"传值参数-传递引用类型-形参值：({point2D.X}, {point2D.Y})");
				point2D.X += 5;
				point2D.Y += 5;
				Console.WriteLine($"传值参数-传递引用类型-修改形参实例的值：({point2D.X}, {point2D.Y})");
			}

			int x = 5;
			Point2d point2D = new Point2d(5, 5);

			Console.WriteLine($"实参-值类型-调用方法前-值：{x}");
			PassAValueType(x);
			Console.WriteLine($"实参-值类型-调用方法后-值：{x}");

			Console.WriteLine();

			Console.WriteLine($"实参-引用类型-调用方法前-值：({point2D.X}, {point2D.Y})");
			PassARefTypeButNewObject(point2D);
            Console.WriteLine($"实参-引用类型-调用方法后-值：({point2D.X}, {point2D.Y})");

			Console.WriteLine();

            Console.WriteLine($"实参-引用类型-调用方法前-值：({point2D.X}, {point2D.Y})");
			PassARefTypeButChangeObject(point2D);
            Console.WriteLine($"实参-引用类型-调用方法后-值：({point2D.X}, {point2D.Y})");
        }

        /*【11602：引用参数--代码示例】
			引用参数将实参传递给形参的形式
				传递值类型   --将实参的存储位置直接传递给形参
				传递引用类型 --将实参的引用存储位置直接传递给形参
			调用含引用参数方法时，实参传递时也需要使用ref关键字修饰

            ref可以理解为——形参是实参的别名
		 */
        public static void LearnReferenceParameter()
        {
            Console.WriteLine("\n------示例：引用参数------\n");
            //该方法引用参数传递的实参是一个值类型
            void PassAValueType(ref int x)
            {
                Console.WriteLine($"引用参数-传递值类型-形参值：{x}");
                x += x;
                Console.WriteLine($"引用参数-传递值类型-形参重新赋值：{x}");
            }
            //该方法引用参数传递的实参是一个引用类型变量，并且会为该变量创建新实例
            void PassARefTypeButNewObject(ref Point2d point2D)
            {
                Console.WriteLine($"引用参数-传递引用类型-形参值：({point2D.X}, {point2D.Y})");
                point2D = new Point2d(point2D.X + 5, point2D.Y + 5);
                Console.WriteLine($"引用参数-传递引用类型-为形参新建一个实例-形参新值：({point2D.X}, {point2D.Y})");
            }
            //该方法引用参数传递的实参是一个引用类型变量，并且会修改该变量实例的成员值
            void PassARefTypeButChangeObject(ref Point2d point2D)
            {
                Console.WriteLine($"引用参数-传递引用类型-形参值：({point2D.X}, {point2D.Y})");
                point2D.X += 5;
                point2D.Y += 5;
                Console.WriteLine($"引用参数-传递引用类型-修改形参实例的值：({point2D.X}, {point2D.Y})");
            }

            int x = 5;
            Point2d point2D = new Point2d(5, 5);

            Console.WriteLine($"实参-值类型-调用方法前-值：{x}");
            PassAValueType(ref x);
            Console.WriteLine($"实参-值类型-调用方法后-值：{x}");

            Console.WriteLine();

            Console.WriteLine($"实参-引用类型-调用方法前-值：({point2D.X}, {point2D.Y})");
            PassARefTypeButNewObject(ref point2D);
            Console.WriteLine($"实参-引用类型-调用方法后-值：({point2D.X}, {point2D.Y})");

            Console.WriteLine();

            Console.WriteLine($"实参-引用类型-调用方法前-值：({point2D.X}, {point2D.Y})");
            PassARefTypeButChangeObject(ref point2D);
            Console.WriteLine($"实参-引用类型-调用方法后-值：({point2D.X}, {point2D.Y})");
        }

        /*【11603：输出参数--代码示例】
			输出参数将实参传递给形参的形式
				传递值类型   --将实参的存储位置直接传递给形参
				传递引用类型 --将实参的引用存储位置直接传递给形参
			调用含输出参数方法时，实参传递时也需要使用out关键字修饰
            输出参数必须在方法结束前明确为其赋值
		 */
        public static void LearnOutputParameter()
        {
            Console.WriteLine("\n------示例：输出参数------\n");
            //该方法输出参数传递的实参是一个值类型
            void PassAValueType(out int x)
            {
                x = 10;
                Console.WriteLine($"输出参数-传递值类型-输出形参值：{x}");
            }
            //该方法输出参数传递的实参是一个引用类型
            void PassARefType(out Point2d point2D)
            {
                point2D = new Point2d(10, 10);
                Console.WriteLine($"输出参数-传递引用类型-输出形参值：({point2D.X}, {point2D.Y})");
            }

            int x = 0;
            Point2d? point2D = null;

            Console.WriteLine($"实参-值类型-调用方法前-值：{x}");
            PassAValueType(out x);
            Console.WriteLine($"实参-值类型-调用方法后-值：{x}");

            Console.WriteLine();

            Console.WriteLine("实参-引用类型-调用方法前-值：{0}", point2D is null ? null : $"({point2D.X}, {point2D.Y})");
            PassARefType(out point2D);
            Console.WriteLine($"实参-引用类型-调用方法后-值：({point2D.X}, {point2D.Y})");
        }

        /*【11604：只读参数--代码示例】
			只读参数将实参传递给形参的形式
				传递值类型   --将实参的存储位置直接传递给形参
				传递引用类型 --将实参的引用存储位置直接传递给形参
			调用含只读参数方法时，实参传递时也需要使用in关键字修饰
            只读参数在方法内不能修改其值，但可修改其状态
                所谓修改状态即可修改类或结构实例成员的值或数组的元素
		 */
        public static void LearnInParameter()
        {
            Console.WriteLine("\n------示例：只读参数------\n");
            //该方法只读参数传递的实参是一个值类型
            void PassAValueType(in int x)
            {
                //x = 10;
                Console.WriteLine("参数值只能使用不能修改");
                Console.WriteLine($"只读参数-传递值类型-读取形参值：{x}");
            }
            //该方法只读参数传递的实参是一个引用类型
            void PassARefType(in Point2d point2D)
            {
                //point2D = new Point2d(10, 10);
                Console.WriteLine("参数变量只能使用不能为其创建新实例");
                Console.WriteLine("参数实例的成员值可以修改");
                point2D.X += 5;
                point2D.Y += 5;
                Console.WriteLine($"只读参数-传递引用类型-读取形参值并修改其状态(成员值)：({point2D.X}, {point2D.Y})");
            }

            int x = 0;
            Point2d point2D = new Point2d(5, 5);

            Console.WriteLine($"实参-值类型-调用方法前-值：{x}");
            PassAValueType(in x);
            Console.WriteLine($"实参-值类型-调用方法后-值：{x}");

            Console.WriteLine();

            Console.WriteLine("实参-引用类型-调用方法前-值：{0}", point2D is null ? null : $"({point2D.X}, {point2D.Y})");
            PassARefType(in point2D);
            Console.WriteLine($"实参-引用类型-调用方法后-值：({point2D.X}, {point2D.Y})");
        }

        /*【11605：数组参数--代码示例】
            设定方法时如果方法的参数数量不能确定即可使用参数数组
            数组参数使用params修饰符声明，表示该方法可接收零个或多个参数
            数组参数可以和其他参数组合使用，但永远只能放在参数列表最后
            数组参数不能同时使用ref、out和in修饰
            数组参数不是说实参可以只是一个形参兼容类型的数组，也可以是调用方传入多个兼容类型的实参，而这个实参列表会以数组的形式传入方法
         */
        public static void LearnParameterArray()
        {
            Console.WriteLine("\n------示例：数组参数------\n");

            void OutputParametersValue(params int[] integers)
            {
                int countParameters = integers.Length;

                if(countParameters == 0)
                {
                    Console.WriteLine("无参数传入");
                    return;
                }
                else
                    Console.WriteLine($"有{countParameters}个参数传入");

                for (int i = 0; i < integers.Length; i++)
                {
                    Console.WriteLine($"第{i + 1}个参数--值为：{integers[i]}");
                }
            }

            Console.WriteLine("不传入参数调用方法");
            OutputParametersValue();

            Console.WriteLine();

            int[] ints = { 2, 3, 4 };
            Console.WriteLine("使用一个形参兼容的数组作为参数调用方法：{2，3，4}");
            OutputParametersValue(ints);
            
            Console.WriteLine();

            Console.WriteLine("使用一系列兼容形参数组元素类型的参数列调用方法：(2, -5, 7, 0, 6)");
            OutputParametersValue(2, -5, 7, 0, 6);
        }

        /*【11606：可选参数--代码示例】
            可选参数即具有显示初始化默认值的参数，也称为默认参数
            当调用具有可选参数的方法时，调用方可以选择是否传入可选参数，如果传入实参则使用实参值，不传入则使用默认值
            当参数列表存在多种参数时，可选参数应位于数组参数之前，其他必须参数之后
            如果参数列表最后为数组参数且调用方法时不想传入可选参数
                数组参数需要具名调用，否则必须传入可选参数对应的参数
                如果数组参数是多个参数，此时还需将多个参数先显示生成一个数组再传入
         */
        public static void LearnDefaultParameter()
        {
            Console.WriteLine("\n------示例：可选参数------\n");

            void UseDefaultParameter(string defaultParam = "这是可选参数的显示声明默认值")
            {
                Console.WriteLine("【方法内输出】" + defaultParam);
            }

            Console.WriteLine("调用方法不传入参数而使用其默认可选参数\n--output:");
            UseDefaultParameter();
            
            Console.WriteLine();

            Console.WriteLine("调用方法并传入一个实参“这是调用方传入的参数”覆盖默认值\n--output:");
            UseDefaultParameter("这是调用方传入的参数");
        }

        /*【11607：this参数--代码示例】
            this参数在定义扩展方法时使用
            this参数必须在参数列表第一位
                当被扩展类型是结构时，this参数也可用in修饰
                其他任何情况下，都不能使用out、ref和in修饰符修饰
         */
        public static string ToChineseNumber(this int x)
        {

            char[] chars = x.ToString().ToCharArray();
            int digits = Math.Abs(x).ToString().Length;
            StringBuilder  chineseNumber = new StringBuilder();
            var sep = int.DivRem(digits, 4);

            Array.Reverse(chars);

            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = chars[i] switch
                {
                    '0' => '〇',
                    '1' => '一',
                    '2' => '二',
                    '3' => '三',
                    '4' => '四',
                    '5' => '五',
                    '6' => '六',
                    '7' => '七',
                    '8' => '八',
                    '9' => '九',
                    '-' => '负',
                    _ => ' '
                };
            }

            for (int j = sep.Quotient; j >=0; j--)
            {
                int temp;

                if (j == sep.Quotient)
                    temp = sep.Remainder;
                else
                    temp = 3;

                for (int k = temp; k >=0; k--)
                {
                    if (j == 0 && k == 0) continue;

                    chineseNumber.Append(chars[j * 4 + k - 1]);

                    if (k == 2) chineseNumber.Append('十');
                    if (k == 3) chineseNumber.Append('百');
                    if (j >= 1 && k == 0) chineseNumber.Append('千');
                    if (j == 1 && k == 1) chineseNumber.Append('万');
                    if (j == 2 && k == 1) chineseNumber.Append('亿');
                }
            }

            string result = chineseNumber.ToString();
            if (digits < chars.Length) result = chars[^1] + result;

            return result;
        }

        public static void LearnThisParameter()
        {
            Console.WriteLine("\n------示例：this 参数------\n");

            Console.WriteLine("通过扩展方法，为int类型实例扩展支持了一个将整数转换成用中文形式表示");
            start: Console.WriteLine("请输入一个整数");
            if (int.TryParse(Console.ReadLine(), out int integer))
            {
                Console.WriteLine("直接调用扩展的ToChineseNumber方法输出整数{0}的中文表示形式：{1}", integer, integer.ToChineseNumber());
            }
            else
                goto start;
        }

        /*【11608：具名参数】
            具名参数是针对方法调用时定义的
            调用方法时，可在实参前加上形参名和一个冒号:作为前缀，该组合即是具名参数
            当使用具名参数调用方法时，可以不考虑定义方法时形参的顺序，但是实参必须数量和形参相同（可选参数除外）
            当具名参数对应的时一个数组参数时，实参只能是单个变量，如需传入多个参数，则应将这些参数先生成为数组在具名传入
         */
        public static void LearnNamelyArgument()
        {
            Console.WriteLine("\n------示例：具名参数------\n");

            void ShowNamelyArgument(int first, ref char second, string third = "这是默认可选参数", params int[] fourth)
            {
                Console.WriteLine($"【方法内输出】--第一个形参--值参数  --output：{first}");
                Console.WriteLine($"【方法内输出】--第二个形参--引用参数--output：{second}");
                Console.WriteLine($"【方法内输出】--第三个形参--可选参数--output：{third}");
                Console.Write($"【方法内输出】--第四个形参--数组参数--output：");

                if(fourth.Length == 0)
                    Console.Write("未使用数组参数");
                else
                    for (int i = 0; i < fourth.Length; i++)
                    {
                        Console.Write(fourth[i] + " ");
                    }
            }

            int aNumber = 88;
            char aChar = '♥';
            int[] anArray = { 1, 2, 3 };

            Console.WriteLine("现有三个作为实参的变量：\nint aNumber = 88;\nchar aChar = '♥'\nint[] anArray = { 1, 2, 3}");
            Console.WriteLine("现在使用具名参数按如下顺序排列实参\n\t(fouth:anArray, second:ref aChar, first:aNumber)\n" +
                "来调用方法ShowNamelyArgument(int first, ref char second, string third = \"这是默认可选参数\", params int[] fourth)");

            ShowNamelyArgument(fourth: anArray, second: ref aChar, first: aNumber);

            Console.WriteLine();
        }

        //定义一个内部类用于学习引用类型的参数
        private class Point2d
        {
            public int X { get; set; }
            public int Y { get; set; }

			public Point2d(int x, int y)
            {
                X = x;
                Y = y;
            }

            public double GetDistance(Point2d left, Point2d right)
            {
                int x = left.X - right.X;
                int y = left.Y - right.Y;

                return Math.Sqrt(x * x + y * y);
            }
        }





        public static void StartLearnParameters()
        {
            string title = "001 传值参数\n" +
                "002 引用参数\n" +
                "003 输出参数\n" +
                "004 只读参数\n" +
                "005 数组参数\n" +
                "006 可选参数\n" +
                "007 this参数\n" +
                "008 具名参数";

            do
            {
                Console.WriteLine("【学习参数】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001":LearnValueParameter(); break;
                    case "002": LearnReferenceParameter(); break;
                    case "003": LearnOutputParameter(); break;
                    case "004": LearnInParameter(); break;
                    case "005": LearnParameterArray(); break;
                    case "006": LearnDefaultParameter(); break;
                    case "007": LearnThisParameter(); break;
                    case "008": LearnNamelyArgument(); break;
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
