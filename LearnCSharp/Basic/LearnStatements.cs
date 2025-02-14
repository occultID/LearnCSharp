/*【学习语句】
 * 语句的定义
	• 语句是高级语言的语法
		○ 编译语言和机器语言只有指令
		○ 高级语言中的表达式对应低级语言中的指令，语句等价于一个或一组有明显逻辑关联的指令
	• 语句的作用和其他说明
		○ 作用
			§ 陈述算法思想 即让程序员“顺序的”表达算法思想
			§ 控制逻辑走向 即通过条件判断、跳转和循环等方法控制程序逻辑的走向
			§ 基于以上两点完成有意义的动作
		○ 其他说明
			§ 一个程序要执行的动作序列就是以语句的形式来表示的
			§ 程序是由语句构成的，语句执行的顺序称为控制流或执行流
			§ 程序编写好后，语句就固定下来了不会改变，但每次执行时，程序的控制流却有可能改变
			§ C#语言的语句由分号（;）结尾，但由分号结尾的不一定都是语句
			§ 语句一定出现在方法体内
				□ 特例：顶级语句
					® 从C#9开始，入口点可以是一系列语句，这些语句可直接在一个程序的某个代码文件中编写，这个文件被称为顶级文件，这些语句被称为顶级语句，这个文件内无需显示的包含命名空间、类和Main方法
					® 有且仅能有一个顶级文件。一个程序只能有一个入口点，一个项目只能有一个包含顶级语句的文件。
					® 使用顶级语句的程序项目不能同时拥有其他入口点
					® 顶级语句隐式位于全局命名空间中
					® 如果顶级文件中有使用using引入命名空间，顶级语句必须存在于using命名空间之后
	• 语句列表
		○ 语句列表由一个或多个按顺序写入的语句组成
		
 * 终结点和可访问性
	• 终结点
		○ 每个语句都有一个终结点，也称作结束点
		○ 简而言之，语句的终结点是紧跟在语句之后的位置
		○ 复合语句(包含嵌入式语句)的执行规则规定了控制到达一个嵌入语句的终结点时所采取的操作
	• 可访问性
		○ 如果语句通过执行流程可能被访问，则称该语句可访问或可到达
		○ 如果语句永远无法被访问，则称该语句不可访问或不可到达
	
 * 语句分类
	• Statement
		○ labeled-statement 标签语句
		○ declaration-statement 声明语句
		○ embedded-statement 嵌入式语句
			§ block                        块
			§ expression-statement         表达式语句
			§ selection-statement          选择（判断、分支）语句
			§ iteration-statement          迭代（循环）语句
			§ jump-statement               跳转语句
			§ try-statement                try…catch…finally语句
			§ using-statement              using语句
			§ yield-statement              yield语句
			§ checked-statement            checked语句
			§ unchecked-statement          unchecked语句
			§ lock-statement               lock语句（用于多线程）
			§ empty-statement              空语句
			§ unsafe-statement             unsafe语句
            § fixed-statement              fixed语句
 */

/* 顶级语句示例
 * 被注释的部分语句可以称为顶级语句
 * 取消注释后该序列语句可执行且会隐式的成为本程序入口点，编译器会忽略所有显示定义的Main方法
 * 取消注释后该文件会成为顶级文件，整个程序有且仅能有一个顶级文件，其他文件不能再使用顶级语句。
 */
//Console.WriteLine("请输入你的姓名：");
//string name = Console.ReadLine();
//Console.WriteLine($"你好，{name}");


namespace LearnCSharp.Basic
{
    internal class LearnStatements
    {
        /*【11101：终结点和可访问性】
			定义见顶部笔记同名章节
			下面是示例代码
		 */
        public static void LearnEndPointsAndReachability() => LearnEndPointsAndReachability(false);

        private static void LearnEndPointsAndReachability(bool isReachable)//该方法具有一个拥有默认值的参数
        {
            Console.WriteLine("\n------示例：终结点和可访问性------\n");

            const bool isUnreachable = true;

            if (isReachable)
                Console.WriteLine("可访问");   //该语句可访问，只是此时不执行，但有被执行的可能性所以可访问
            else if (!isUnreachable)
                Console.WriteLine("不可访问"); //isUnreachable是常量，所以流程判断永远不会执行到该语句，故该语句永远不可能被访问
            else
				Console.WriteLine("到这里就真快结束啦");

			//以上每一个Console.WriteLine();语句最后结束的位置都是这个语句的终结点
        }

        /*【11102：带标签的语句】
			带标签语句即可以为语句定义一个标签名前缀，并在其后接一个冒号（:），随后在跟随一个语句
			带标签语句允许出现在块中，但不允许作为嵌入语句使用
			标签具有自己的声明空间，不会干扰其他标识符，比如标签可以和变量同名
			带标签语句的使用：
				使用用户给定的标签名称来为语句进行标记，且作用域内不能出现同名标签
				可以在标签作用域范围内使用goto语句跳转至指定标签执行标记的语句
				标签的作用域为定义其的块和这个块的嵌套块
		 */
        public static void LearnLabeledStatement()
        {
            Console.WriteLine("\n------示例：带标签的语句------\n");

            uint result = 0;

			//带标签的语句示例
			input: Console.Write("请输入一个正整数："); //input是标签，有独立的声明空间，所以可以和作用域内变量同名
            string? input = Console.ReadLine();

			if (string.IsNullOrWhiteSpace(input) || !uint.TryParse(input, out uint maxNumber))
				goto input; //可以使用goto跳转到对应标签
			else
			{
                for (uint i = 1; i <= maxNumber; i++)
                {
                    result += i;
                }
				goto end; //可以使用goto跳转到对应标签
            }

			end:
            Console.WriteLine($"从 0 到 {maxNumber} 的所有正整数的和为：{result}");//end是标签
        }

        /*【11103：声明语句】
			声明语句是用于声明局部变量或常量的语句
			声明语句允许在块中使用，但不允许作为嵌入语句
		 */
        public static void LearnDeclarationStatement()
        {
            Console.WriteLine("\n------示例：声明语句------\n");
            
			//以下均为声明语句
            int x1;							//声明一个局部变量,并且不赋值
			string? s1, s2 = null;			//声明两个同类型的局部变量，变量可用,隔开，并且对其中一个变量不赋值

			var @char = 'a';	//声明一个隐式类型的局部变量，编译器可以根据赋值推导实际类型
			int x2 = 2;             //声明一个局部变量并进行初始化
			
			x1 = 3;
			s1 = "new string";

            Console.WriteLine($"输出声明的 {x1.GetType(),-15} 类型变量 {nameof(x1)} 的值：{x1}");
			Console.WriteLine($"输出声明的 {x2.GetType(),-15} 类型变量 {nameof(x2)} 的值：{x2}");
			Console.WriteLine($"输出声明的 {s1.GetType(),-15} 类型变量 {nameof(s1)} 的值：{s1}");
			Console.WriteLine($"输出声明的 {typeof(string),-15} 类型变量 {nameof(s2)} 的值：{s2}");
            Console.WriteLine($"输出声明的 {@char.GetType(),-15} 类型变量 {nameof(@char)} 的值：{@char}");


            unsafe
			{
				int* intPtr = &x2;  //声明一个局部变量，即使包含指针和引用这也是一个语句

                Console.WriteLine($"输出声明的 {typeof(int*),-15} 类型变量 {nameof(intPtr)} 的值：{(int)intPtr}");
            }
        }

        /*【11104：块】
			块即一个代码块，使用一对花括号{}将零个或多个语句组成的语句列表包含其中
			如果块内无任何语句，则称块为空
			块可以嵌套
			块可以包含声明语句，块中声明的局部变量或常量的范围为块极其嵌套块
			块可以包含带标签的语句，块中声明的标签作用域为块及其嵌套块
			块的执行方式：
				如果块为空，执行控制将被传输到块的终结点
				如果块不为空，执行控制将转移到语句列表
					当执行控制到语句列表的终点时，会被继续传输到块的终结点
				如果块本身是可访问的，则块的语句列表是可访问的
				如果块为空或语句列表的终结点是可访问的，则块的终结点是可访问的
				
		 */
        public static void LearnBlockStatement()
        {
            Console.WriteLine("\n------示例：块------\n");
            //方法的主体其实就是一个块
            if (true)
            {
                //if语句的子语句也可以是一个块
                //块很多时候都是作为嵌入语句使用
                Console.WriteLine("这是if语句嵌入块中输出");
            }

            string outputString = "这是一个方法主体块的局部变量字符串";
            Console.WriteLine(outputString);

            {//可以随意用一个块来包含一系列语句，但通常不会这么干
                string outputInnerString = "这是一个方法主体内部某个块内的输出";
                Console.WriteLine(outputInnerString);
                Console.WriteLine("这是在块内输出包含此块的包含块的字符串：{0}", outputString);
            }
            //Console.WriteLine(outputInnerString);//错误，outputInnerString的作用域不能超出声明它的块

        }

        /*【11105：表达式语句】
			表达式语句计算给定表达式的值
			由表达式计算的值(如果有)将被丢弃
			并非所有表达式都允许作为语句
				比如x+y和x==1这样仅计算将被放弃值的表达式不允许作为语句
				只有赋值表达式、方法或委托调用表达式、递增递减表达式和new表达式可以作为语句
		 */
		public static void LearnExpressionStatement()
		{
            Console.WriteLine("\n------示例：表达式语句------\n");

            int x = 12, y = 13;
			//x + y;  //这样的表达式不能作为语句
			//x == y; //这样的表达式不能作为语句

            //只有赋值表达式、方法或委托调用表达式、递增递减表达式和new表达式可以作为语句
            y += x;
            Console.WriteLine($"y += x;表达式语句执行后y的值：{y}");

            x++;
			Console.WriteLine($"x++;表达式语句执行后x的值：{x}");
		}

        /*【11106：选择语句】
 			选择语句分为if-else语句和switch语句
			选择语句根据表达式的值从许多可能的路径中选择要执行的语句
 
			if-else语句根据布尔表达式的值选择要执行的语句
			switch语句根据与表达式的模式匹配选择要执行的语句列表
		*/
        public static void LearnSelectionStatement()
        {
            Console.WriteLine("\n------示例：选择语句------\n");
            LearnCSharp.Basic.LearnSelectionStatement.StartLearnSelectionStatement();
        }

        /*【11107：迭代语句】
			迭代语句用于重复执行语句或语句块
			迭代语句也称为循环语句
			迭代语句包括以下四种
				Foreach 语句
				For 语句
				Do-While 语句
				While 语句
			除了以上迭代语句，使用以下方案也能实现循环执行代码
				goto语句和带标签的语句结合使用
				递归方法
		*/
        public static void LearnIterationStatement()
        {
            Console.WriteLine("\n------示例：迭代语句------\n");
            LearnCSharp.Basic.LearnIterationStatement.StartLearnIterationStatement();
        }

        /*【11108：跳转语句之goto语句】
			goto语句总是固定形式的：使用关键字goto以及后跟标签名
				标签名可以是用户定义的标签，也可以是switch语句中的“case 匹配标签”和“default”
			goto语句用于将控制转交给由标签标记的语句
				goto语句的目标是带有给定标签的标记语句
				goto语句只能跳转到其所在的标签作用域内的标签
			注意：非必要不要使用goto语句和带标签的语句
				  非必要不要使用goto语句和带标签的语句
				  非必要不要使用goto语句和带标签的语句
				  滥用goto语句很有可能会造成逻辑的混乱和代码的复杂度，流程控制有很多更好的实现
		 */
        public static void LearnGotoStatement()
		{
            Console.WriteLine("\n------示例：goto语句------\n");

			start: Console.WriteLine("请输入一个整数");  //定义了一个start标签来标记语句

			if (int.TryParse(Console.ReadLine(), out int integer))
				goto end;  //跳转到执行end标签标记的语句
			else
				goto start;//跳转到执行start标签标记的语句

			end: Console.WriteLine("你输入的数字是：{0}",integer); //定义了一个start标签来标记语句
		}

		/*【11109：跳转语句之break、continue语句】
			break语句将终止最接近的封闭迭代语句（while、do-while、for、foreach语句）或switch语句
				break语句只能退出包含它的那一个switch或迭代语句
				break语句将控制权转交给已终止语句后面的语句（如有）
			continue语句用于跳过包含它的最接近的while、do-while、for或foreach语句的其中一次迭代并启动下一次迭代
			
			如果要从嵌套的多个循环中跳转多层控制，需合理使用goto语句和带标签的语句
		 */
		public static void LearnBreakAndContinueStatement() 
		{
            Console.WriteLine("\n------示例：break语句 和 continue语句------\n");

			int[] integers = new int[10];

			for (int i = 0; i < integers.Length; i++)
			{
				start:
				Console.Write($"请输入第 {i + 1:00} 个整数：");
				if (int.TryParse(Console.ReadLine(), out int integer))
					integers[i] = integer;
				else
					goto start;
            }

			Console.WriteLine($"已创建一个整数数组：{{{string.Join<int>(',', integers)}}}\n");

			int temp = 0;
			bool exchanged = false;

			//使用冒泡排序对数组进行排序
			for (int i = 0; i < integers.Length - 1; i++)
            {
                Console.WriteLine($"进行第{i + 1}轮排序");
                
				exchanged = false;
				for (int j = 1; j < integers.Length - i; j++) 
				{
					if (integers[j - 1] <= integers[j])
						continue;	//满足条件时跳过本次循环，并继续下一次循环；

                    temp = integers[j - 1];
                    integers[j - 1] = integers[j];
                    integers[j] = temp;
                    exchanged = true;
                }

                Console.WriteLine($"当前数组排序结果：{{{string.Join<int>(',', integers)}}}\n");

				if (!exchanged)
					break;	//满足条件时跳出循环；
			}
		}

		/*【11110：跳转语句之throw、return语句】
			throw语句用于抛出异常或抛出被catch捕获的异常
				throw语句常结合try语句使用，更多知识参考LearnException部分
			return语句终止它所在的函数的执行，并将控制权和结果（如有）返回给当前调用方
		 */
		public static void LearnThrowAndReturnStatement()
		{
			//定义一个局部方法用于演示throw和return
			int Sum(int left, int right)
			{
				string error = $"【溢出错误】int类型变量 {nameof(left)}：{left} 与 {nameof(right)}：{right} 的和超出了int类型的容量范围！";

				var result = (left, right) switch
                {
					//可以使用throw语句对运行时错误进行抛出来结束方法，外部调用方法时需要使用异常语句捕捉并处理错误，或者继续使用throw语句向外部抛出错误
                    ( > 0, > 0) => int.MaxValue - left >= right ? left + right : throw new ArithmeticException(error),
                    ( < 0, < 0) => int.MinValue - left <= right ? left + right : throw new ArithmeticException(error),
                    (_, _) => left + right
                };

				//当方法中包含返回类型时，必须使用return返回兼容返回类型的值或表达式
				//如果存在选择分支，则必须确保所有分支的终结点都能返回兼容返回类型的值或表达式
				return result;
            }

            Console.WriteLine("\n------示例：throw语句 和 return语句------\n");

			first:
			Console.Write("请输入第一个整数：");
			if (!int.TryParse(Console.ReadLine(), out int left))
				goto first;

            second:
            Console.Write("请输入第二个整数：");
            if (!int.TryParse(Console.ReadLine(), out int right))
                goto second;

            Console.WriteLine();

			try
			{
				Console.WriteLine($"{left} 与 {right} 的和为 {Sum(left, right)}");
			}
			catch(ArithmeticException e)
			{
				Console.WriteLine(e.Message);
			}

            //方法无返回类型时可直接用return结束函数执行
            //但一般如果是在无返回类型方法的最后面，return是可选的，默认不需要
            //如果是在执行过程中由控制流判断需要结束函数执行，则必须使用
            return; 
		}

		/*【11111：try语句】
			try语句提供了一种机制用于捕获在执行块期间发生的异常
			try语句提供了在捕获异常时保证仍需执行的代码块始终能得到执行的能力
			try语句的形式：
				try
				{
					可能会出现异常的语句列表
				}
				catch(指定捕获的异常类型表达式) //这是可选的，如果需要推荐这样的具体捕获，可以连续使用多个catch
				{
					处理捕获的异常或抛出异常的语句列表
				}
				catch //这是可选的，如果需要优先使用指定捕获异常的catch，不指定的只能放置于所有catch语句段的最后且仅能出现一次
				{
					处理捕获的异常或抛出异常的语句列表
				}
				finally //这是可选的，但是如需使用有且只能有一个finally并且只能放置于最后
				{
					无论是否捕获异常都会执行的语句列表
					//一般用于放置处理释放非托管资源的代码
				}
			本部分内容详细参见 LearnException部分
		 */
		public static void LearnTryStatement()
		{
            //下列代码只是用于简单的学习和了解try语句
            //具体怎样合理的使用try语句进行异常处理和捕获参考LearnException部分

            Console.WriteLine("\n------示例：异常捕捉语句------\n");

            int[] integers = { 1, 2, 0, -8, 6 };

			Console.WriteLine($"已创建一个数组：{{{string.Join('，',integers)}}}\n数组长度：{integers.Length}\n");

			for (int i = 0; i < 10; i++)
			{
				try
				{
					Console.WriteLine($"try：输出数组第 {i + 1} 个元素：{integers[i]}");
				}
				catch(IndexOutOfRangeException e)
				{
					Console.WriteLine($"catch 异常信息：{e.Message}");
					Console.WriteLine("catch 异常处理：使用break结束循环");
					break;
				}
				catch
				{
					throw;
				}
				finally
				{
					Console.WriteLine($"finally：第 {i + 1} 次循环执行完毕");
				}
			}
		}

		/*【11112：using语句】
			using语句用于获取一个或多个资源，然后执行语句，最后释放资源
				资源是指实现了System.IDisposable接口的类或结构，其中包括一个名为Dispose的无参方法
				using语句可以在使用资源的代码时自动调用Dispose方法来释放资源而无需开发者手动调用
			using语句分为三部分即获取、使用和处理
			using语句的一般形式：
				using(资源类型 资源变量 = 能返回资源类型的表达式)
				{
					使用资源的语句列表
				}//执行结束后自动调用资源的Dispose方法
				备注：using语句使用资源可以是多个，()内使用,将不同资源隔开，等效于嵌套分开使用using语句
		 */
		public static void LearnUsingStatement()
		{
            Console.WriteLine("\n------示例：using语句------\n");

            //示例创建并写入和读取一个log.txt文件
            //TextWriter类和TextReader类都实现了IDisposable接口
            //这两个类会使用非托管的资源，故需即时使用Dispose方法释放
            if (!File.Exists("log.txt"))
			{
				using var stream = File.CreateText("log.txt");
				//using语句从C#8.0开始可以简化为如上形式，在其后可继续编写代码
				//处理资源，当该using所在的块执行到终结点时会自动调用资源的dispose方法执行释放
			}

			using(TextWriter writer = File.AppendText("log.txt"))
			{
				writer.WriteLine($"记录写入日志：由LearnUsingStatement写入日志 行01 | 时间：{DateTime.Now}");
                writer.WriteLine($"记录写入日志：由LearnUsingStatement写入日志 行02 | 时间：{DateTime.Now}");
            }

			using(TextReader reader = File.OpenText("log.txt"))
			{
                Console.WriteLine("--读取日志--");
				string? s;
				while ((s = reader.ReadLine()) != null) 
				{
					Console.WriteLine(s);
				}
			}
		}

		/*【11113：yield语句】
			在迭代序列中，使用迭代器中的yield语句提供序列的下一个值
			yield是一个上下文关键字，仅当其在迭代器上下文中时才有意义
			yield语句有两种形式：
				yield return--在迭代器中提供下一个值
				yield break --显示指示迭代结束
			该部分详情参见 LearnCollections
		 */
		public static void LearnYieldStatement()
		{
            //定义一个局部方法用于演示迭代器和yield语句
            IEnumerable<int> OddAndEvenEnumerator(int countNumber = 10, bool isOutputOdd = false)
            {
                Console.WriteLine("【运行】迭代器：开始运行");

                if (countNumber<=0) 
				{
                    Console.WriteLine("【结束】迭代器：生成数量小于等于0，即将结束迭代");
                    yield break;//显示结束迭代
                }

				for (int i = 0; i < countNumber; i++)
				{
					var randomInteger = Random.Shared.Next(int.MinValue, int.MaxValue);
					var result = 0;

					if (!isOutputOdd)
						result = randomInteger % 2 == 0 ? randomInteger : randomInteger + 1;
					else
						result = randomInteger % 2 == 0 ? randomInteger + 1 : randomInteger;


					Console.WriteLine($"【预载】迭代器：即将提供该值：{result}");
					
					//阻塞线程查看迭代器运行步骤
					Thread.Sleep(1000);
					yield return result;

					Console.WriteLine($"【已供】迭代器：已经提供迭代值：{result}\n");
                    Thread.Sleep(1000);
                }

                Console.WriteLine("【结束】迭代器：结束运行");
            }

            Console.WriteLine("\n------示例：yield语句------\n");

			IEnumerable<int> oeEnumerator;

			inputNum:
			Console.Write("请输入一个整数来确认即将生成的数字个数：");

			if (!int.TryParse(Console.ReadLine(), out int countNumber))
				goto inputNum;

            Console.WriteLine();
			
			inputOE:
			Console.Write("请确认即将输出的数字是偶数还是奇数（E/O）：");
			if (Console.ReadKey(true).Key == ConsoleKey.E)
				oeEnumerator = OddAndEvenEnumerator(countNumber);
			else if (Console.ReadKey(true).Key == ConsoleKey.O)
				oeEnumerator = OddAndEvenEnumerator(countNumber, true);
			else
			{
                Console.WriteLine();
                goto inputOE;
            }

            //支持迭代器的成员支持以foreach循环遍历器数据
            Console.WriteLine("【调用】迭代");

			foreach (var number in oeEnumerator)
			{
				Console.WriteLine($"【输出】调用方输出值：{number}");
				Thread.Sleep(1000);
			}
		}

        /*【11114：checked和unchecked语句】
			checked和unchecked语句用于控制整型算术运算和转换时的溢出检查
			checked语句 告知编译器在编译时需要对整数类型运算或转换要进行溢出检查 这是默认行为 不必须显示使用该语句
			unchecked语句 告知编译器在编译时不需要对整数类型运算或转换要进行溢出检查 需显示使用该语句 非必要不要使用
			checked和unchecked语句形式：
				checked/unchecked
				{
					整数类型运算或转型相关的语句列表
				}
			
		 */
        public static void LearnCheckedAndUncheckedStatement()
		{
            Console.WriteLine("\n------示例：checked语句 和 unchecked语句------\n");
            int maxInt = int.MaxValue;

			//checked语句中将会检查计算中的溢出错误
			checked
			{
				try
				{
					Console.WriteLine(maxInt + 1);
				}
				catch (ArithmeticException e)
				{
                    Console.WriteLine($"checked------运行时异常：{e.Message}");
				}
			}

            //unchecked语句中不会检查计算中的溢出错误
            unchecked
            {
                try
				{
					Console.WriteLine($"unchecked------运行时溢出，结果截断输出：{maxInt + 1}");
                }
				catch (Exception e)
				{
                    Console.WriteLine($"运行时异常：{e.Message}");
				}
			}
		}

        /*【11115：lock语句】
			lock语句获取给定对象的互斥lock,执行语句块，然后释放lock
			持有lock时，持有lock的线程可以再次获取并释放lock
			阻止任何其他线程获取lock并等待释放lock
			lock语句可确保单个线程具有对该对象的独占访问权限
				lock语句的形式：
					lock(表达式)
					{
						语句列表
					}
				其中表达式必须有明确的值且必须为引用类型
			lock语句和相关知识详解后续会见 LearnAsyncProgramming 和 LearnParallelProgramming了解
		 */
        private static object objLock = new object(); //定义一个静态对象用于演示锁定它来更改静态数据
        public static int countCalled = 0;//定义一个静态字段作为需要在lock中更改的数据
        public static async void LearnLockStatement()
        {
			//
			async void TestLock(Action action)
			{
                var tasks = new Task[10];
                for (int i = 0; i < tasks.Length; i++)
                {
                    tasks[i] = Task.Run(action);
                }
                await Task.WhenAll(tasks);
            }

            Console.WriteLine("\n------示例：lock语句------\n");

            var lockDelegate = () =>
            {
                lock (objLock)
                {
                    countCalled++;
                    Console.WriteLine("【lock】本次程序执行中调用了lock方法{0}次", countCalled);
                }
            };//这是一个委托，现在不用作具体了解，只用知道它可以托管一个或多个兼容的方法

			TestLock(lockDelegate);
			await Task.Delay(1500);

			countCalled = 0;
			var noLockDelegate = () =>
			{
                countCalled++;
                Console.WriteLine("【no lock】本次程序执行中调用了nolock方法{0}次", countCalled);
            };

			TestLock(noLockDelegate);
            await Task.Delay(1500);
        }

        /*【11116：空语句】
		    空语句即不执行任何操作的语句
			在需要语句的上下文中没有要执行的操作时，将使用空语句
			空语句的执行只是将控制转移到语句的终结点。
				因此如果可以访问空语句，就可以访问空语句的结束点
		 */
        public static void LearnEmptyStatement()
		{
            Console.WriteLine("\n------示例：空语句------\n");

            Console.WriteLine("运行一个循环输出数字：0~9");
			for (int i = 0; i < 10; i++)
			{
				if (i == 5)
				{
                    Console.WriteLine($"遇到数字 {i}，跳转到空语句结束循环！");
					goto Exit;
				}

                Console.WriteLine($"输出数字：{i}");
			}			

			Exit:
			;//这里是一个空语句，仅有一个分号表示语句结束，语句并无内容与操作
		}

		/*【11117：unsafe语句】
			unsafe语句用于将非托管代码组织在unsafe块中
			unsafe语句形式：
				unsafe
				{
					非托管代码组成的语句列表
				}
			关于unsafe语句更多的知识学习和理解参考 LearnUnsafeCode部分
		 */
		public static void LearnUnsafeStatement()
		{
            Console.WriteLine("\n------示例：unsafe语句------\n");
            string outputString = "Love C#"; 


            unsafe
			{
				string* stringPtr = &outputString;
				Console.WriteLine($"内存地址：{(long)stringPtr}");
				Console.WriteLine($"通过指针输出字符串：{*stringPtr}");
            }
		}

        /*【11118：fixed语句】
            fixed语句可防止垃圾回收器重新定位可移动变量，并声明指向该变量的指针
            固定变量的地址在语句的持续时间内不会更改。 
            只能在相应的 fixed 语句中使用声明的指针。 
            声明的指针是只读的，无法修改
         */
        public static void LearnFixedStatement()
        {
            Console.WriteLine("\n------示例：fixed语句------\n");

            int[] arr = { 1, 2, 3 };

            unsafe
            {
                fixed(int* p = arr)
                {
                    Console.WriteLine($"内存地址：{(long)p}");
                    Console.WriteLine($"通过指针输出值：{*p}");
                }
            }
        }



        public static void StartLearnStatements()
		{
            string title = "001 终结点和可访问性示例\n" +
                "002 带标签的语句\n" +
                "003 声明语句\n" +
                "004 块\n" +
                "005 表达式语句\n" +
                "006 选择语句\n" +
				"007 迭代语句\n" +
				"008 跳转语句--goto语句\n" +
				"009 跳转语句--break语句和continue语句\n" +
				"010 跳转语句--throw语句和return语句\n" +
				"011 try语句\n" +
				"012 using语句\n" +
				"013 yield语句\n" +
				"014 checked语句和unchecked语句\n" +
                "015 lock语句\n" +
				"016 空语句\n" +
				"017 unsafe语句" +
                "018 fixed语句";

            do
            {
                Console.WriteLine("【学习语句】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001":LearnEndPointsAndReachability(); break;
                    case "002":LearnLabeledStatement(); break;
                    case "003":LearnDeclarationStatement(); break;
                    case "004":LearnBlockStatement(); break;
                    case "005":LearnExpressionStatement(); break;
                    case "006":LearnSelectionStatement(); break;
                    case "007":LearnIterationStatement(); break;
					case "008":LearnGotoStatement(); break;
					case "009":LearnBreakAndContinueStatement(); break;
                    case "010":LearnThrowAndReturnStatement(); break;
                    case "011":LearnTryStatement(); break;
                    case "012":LearnUsingStatement(); break;
                    case "013":LearnYieldStatement(); break;
                    case "014":LearnCheckedAndUncheckedStatement(); break;
                    case "015":
						LearnLockStatement();
						Thread.Sleep(2500);
						break;
                    case "016":LearnEmptyStatement(); break;
                    case "017":LearnUnsafeStatement(); break;
                    case "018":LearnFixedStatement(); break;
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

    internal class LearnSelectionStatement
    {
        /*【学习if-else语句】
            if语句基于布尔表达式的值来选择要执行的语句
            if-else语句是用于进行简单的条件判断语句，可以嵌套使用和连续使用
            如果仅作简单判断，else语句可省略
           
         *  if语句的形式：
                if(返回bool值的表达式)
                {
                    判断为true时要执行的语句(块)
                 }
                else if(额外用来判断的条件)
                {
                    继续判断为true时要执行的语句
                 }
                else
                {
                    判断为false时要执行的语句(块)
                 }
            当进行判断后如果接下来仅执行一句语句，{}可以省略
            仅作一次判断，可以省略else if和其代码块，如要连续进行条件判断，可在if和else中间不断使用else if{}
            if语句也可以如下进行嵌套使用：
                if(...)
                {
                    if(...)
                    {
                        ......
                     }
                 }
         */
        public static void LearnIfStatement()
        {
            Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");
            //先进行一次判断输入数据是否是整数，如果是，则执行if内部代码块，否则执行匹配的else内代码块
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number >= 0 && number <= 15)
                {
                    Console.ForegroundColor = (ConsoleColor)number;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("暂无该颜色编码，已重置为灰色。");
                }
            }
            else
                Console.WriteLine("请输入一个整数数字！");

            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /* 【学习switch语句】
             switch语句形式：
                switch(匹配变量)
                {
                    case 匹配标签:
                        匹配成功执行的语句或语句块；
                        这里的语句块应省略大括号{}，因为case和break这两个关键字本身就指示了块的开始和结束
                        break/goto 匹配标签/return/throw等语句结束以显示退出switch语句
                    case 匹配标签:
                        匹配成功执行的语句或语句块；
                        break/goto 匹配标签/return/throw等语句结束以显示退出switch语句
                    ...任意个case代码块
                    default:
                        默认匹配成功后需执行的语句块；
                        break/goto 匹配标签/return/throw等语句结束以显示退出switch语句
                }
             在C#中switch语句各个case语句不允许贯穿，每个case语句执行完成后必须显示退出
             在C#中switch语句允许多个case语句组，即case匹配标签后面不写任何代码，即可和下一case语句共同执行其匹配的语句段
             在C#6.0极其之前，switch语句作用很有限，常被做为是if-else语句的替代品
             C#7.0极其之后，switch的限制得到放宽，每个case标签不在只能是一个常量，而是一个模式
             C#7.0为switch语句引入了模式匹配，switch语句可以使用任何数据类型。
         */

        //示例一 常量模式 原本C#6.0之前用法
        //switch语句在C# 6.0极其之前的版本，匹配表达式只支持字符串、字符、整型、bool、枚举几个类型
        public static void LearnSwitchStatement()
        {
            Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");

            if (int.TryParse(Console.ReadLine(), out int number))
            {
                switch (number)
                {
                    //下列注释写法会报错，case语句必须显示跳出，不能贯穿
                    //case 0:
                    //    Console.ForegroundColor = (ConsoleColor)number;
                    //case 1:
                    //    Console.ForegroundColor = (ConsoleColor)number;
                    //下列未注释语句不会出问题，因为C#允许组和多个case为一组知道最后一个标签后跟随执行代码和显示跳出
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        Console.ForegroundColor = (ConsoleColor)number;
                        break;
                    case 16:
                        goto default;//匹配到该标签可以使用goto显示跳转到default，但注意不要随意使用goto，以及避免造成循环，比如goto case 16
                    default:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("暂无该颜色编码，已重置为灰色。");
                        break;
                }
            }
            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void StartLearnSelectionStatement()
        {
            string title = "001 if语句\n" +
                "002 基础switch语句-常量模式\n";

            do
            {
                Console.WriteLine("【学习选择语句】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnIfStatement(); break;
                    case "002": LearnSwitchStatement(); break;
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
    internal class LearnIterationStatement
    {
        /*【学习foreach语句】
            foreach语句为类型实例中实现了IEnumerable或IEnumerable<T>接口的每个元素执行语句或语句块
            foreach语句并不限于这些类型。可以将其与满足以下条件的任何类型的实例一起使用
                类型具有公共无参数GetEnumerator方法
                    从C#9.0开始，GetEnumerator方法可以是类型的扩展方法
                GetEnumerator方法的返回类型具有公共Current属性和公共无参数MoveNext方法(其返回值为bool)
            foreach语句一般形式
                await foreach(ref 元素数据类型 元素变量 in 可以foreach的实例)
                {
                    处理元素变量的语句或语句块
                }
                注意：await是可选的，根据是否需要处理异步流决定是否使用
                注意：ref是可选的，根据枚举器的Curret属性返回值是否是ref引用来决定是否使用ref
                注意：元素数据类型可使用var隐式声明，编译器可以自动推导
        */
        private static uint Sum0ToMaxByForeach(uint max)
        {
            uint sum = 0;
            uint[] nums; //预置一个内部含0正整数数组，用于Foreach示例

            if (max == 0 || max == 1) return max;
            else
            {
                nums = new uint[max];
                for (uint i = 0; i < nums.Length; i++)
                {
                    nums[i] = i + 1;
                }
            }

            foreach (var num in nums)
            {
                sum += num;
            }
            return sum;
        }

        /*【学习for语句】
            在指定的布尔表达式的计算结果为true时，for语句会执行一条语句或一个语句块
            for语句的一般形式
                for(初始化表达式; 布尔表达式; 迭代器)
                {
                    语句或语句列表
                }
                初始化表达式部分仅在进入循环前执行一次，通常在该部分中声明并初始化一个局部循环变量
                    该循环变量在for语句外部无法访问，其作用域仅限for语句
                布尔表达式部分确定是否应执行循环中的下一个迭代，通常该部分使用循环迭代变量和一个表示结束条件的值进行比较
                    比较结果为true则继续下一个迭代，否则退出循环
                迭代器部分定义执行每一次循环语句后将执行的操作，通常是改变循环变量的值使其不断趋近于表示结束条件的值，直至能结束循环
         */
        private static uint Sum0ToMaxByFor(uint max)
        {
            uint sum = 0;
            for (uint i = 0; i <= max; i++)
            {
                sum += i;
            }
            return sum;
        }

        /*【学习do-while语句】
            当指定的布尔表达式的计算结果为true时，do语句会执行一条语句或一个语句块
            无论是否进入循环，Do语句块内的语句都会先于判断前执行一次
            do-while语句一般形式：
                do
                {
                    语句或语句列表
                }while(布尔表达式)
        */
        private static uint Sum0ToMaxByDoWhile(uint max)
        {
            uint i = 0;
            uint sum = 0;
            do
            {
                sum += i;
                i++;
            } while (i <= max);
            return sum;
        }

        /*【学习while语句】
            当指定的布尔表达式的计算结果为true时，do语句会执行一条语句或一个语句块
            该循环执行零次或多次，而do语句至少执行一次循环体
            while语句一般形式
                while(布尔表达式)
                {
                    语句或语句块
                }
        */
        private static uint Sum0ToMaxByWhile(uint max)
        {
            uint sum = 0;
            uint i = 0;
            while (i <= max)
            {
                sum += i;
                i++;
            }
            return sum;
        }

        /*【学习递归方法】
            递归方法实际是一种逆向思维的处理方式
            用一个方法循环调用自身直至满足递归结束条件而得出结果
         */
        private static uint Sum0ToMaxByRecursion(uint max)
        {
            if (max == 0)
                return 0;//递归结束判断
            else
                return max + Sum0ToMaxByRecursion(max - 1);
        }

        /*【使用goto和带标签的语句实现循环】
            一般情况下不建议滥用 goto语句，除非有必要
            滥用goto语句容易造成逻辑出错或代码混淆难懂
            这里作为示例展示了解
         */
        private static uint Sum0ToMaxByGoto(uint max)
        {
            uint sum = 0;
            uint i = 0;

            if (max == 0 || max == 1)
            {
                sum = max;
                goto end;
            }

        start: sum += i;
            i++;

            if (i <= max)
                goto start;
            else
                goto end;

            end: return sum;
        }



        //以给定的循环方式计算从零到给定的最大正整数的和
        public static uint Sum0ToMax(uint max, Loops loops)
        {
            switch (loops)
            {
                case Loops.Foreach:
                    return Sum0ToMaxByForeach(max);
                case Loops.For:
                    return Sum0ToMaxByFor(max);
                case Loops.DoWhile:
                    return Sum0ToMaxByDoWhile(max);
                case Loops.While:
                    return Sum0ToMaxByWhile(max);
                case Loops.Recursion:
                    return Sum0ToMaxByRecursion(max);
                case Loops.Goto:
                    return Sum0ToMaxByGoto(max);
                default:
                    return 0;
            }
        }

        //以给定的循环方式计算从零到给定的最大正整数的和-直接输出结果
        public static void OutputSum0ToMax(uint max, Loops loops)
        {
            Console.WriteLine($"使用[ {loops,-9} ]循环计算[ 0 ]至[ {max} ]的和为：{Sum0ToMax(max, loops)}");
        }

        public static void StartLearnIterationStatement()
        {
            Console.WriteLine("【循环语句(或方法)--计算0到给定正整数的和】");

        Input:
            Console.Write("请输入一个正整数：");

            if (uint.TryParse(Console.ReadLine(), out uint max))
            {
                Console.WriteLine();
                OutputSum0ToMax(max, Loops.Foreach);
                OutputSum0ToMax(max, Loops.For);
                OutputSum0ToMax(max, Loops.DoWhile);
                OutputSum0ToMax(max, Loops.While);
                OutputSum0ToMax(max, Loops.Recursion);
                OutputSum0ToMax(max, Loops.Goto);
            }
            else
                goto Input;

            Console.WriteLine();
        }
    }

    internal enum Loops : byte
    {
        While = 0,      //While循环
        DoWhile = 1,    //Do-While循环
        For = 2,        //For循环
        Foreach = 3,    //Foreach循环
        Recursion = 4,  //递归循环
        Goto = 5        //goto循环
    }
}
