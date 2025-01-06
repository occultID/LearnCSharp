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
		○ 复合语句(包含嵌入式语句)的执行规则规定了控制到一个达嵌入语句的终结点时所采取的操作
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
			§ embedded-statement-unsafe    unsafe语句
 */

/* 顶级语句示例
 * 被注释的部分语句可以称为顶级语句
 * 取消注释后该序列语句可执行且会隐式的成为本程序入口点，编译器会忽略所有显示定义的Main方法
 * 取消注释后该文件会成为顶级文件，整个程序有且仅能有一个顶级文件，其他文件不能再使用顶级语句。
 */
//Console.WriteLine("请输入你的姓名：");
//string name = Console.ReadLine();
//Console.WriteLine($"你好，{name}");


using System.Numerics;

namespace LearnCSharp.Basic
{
    internal class LearnStatements
    {
		/*【学习终结点和可访问性】
			定义见顶部笔记同名章节
			下面是示例代码
		 */
		public static void LearnEnd_pointsAndReachability(bool isReachable = false)//该方法具有一个拥有默认值的参数
        {
            const bool isUnreachable = true;

            if (isReachable)
                Console.WriteLine("可访问");   //该语句可访问，只是此时不执行，但有被执行的可能性所以可访问
            else if (!isUnreachable)
                Console.WriteLine("不可访问"); //isUnreachable是常量，所以流程判断永远不会执行到该语句，故该语句永远不可能被访问
            else
				Console.WriteLine("到这里就真快结束啦");

			//以上每一个Console.WriteLine();语句最后结束的位置都是这个语句的终结点
        }

        /*【学习带标签的语句】
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
			//带标签的语句示例
			integer: int x = 5;             //integer是标签
			number: double number = 7.8;    //number是标签，有独立的声明空间，所以可以和变量同名

			string outputString = "以下两行代码即为带标签的语句，其中integer:和number:为标签前缀：\n\n" +
					"integer: int x = 5;             //integer是标签\n" +
					"number: double number = 7.8;    //number是标签，有独立的声明空间，所以可以和变量同名\n";

			Console.WriteLine(outputString);
            Console.WriteLine($"输出局部变量值--number：{number}，x：{x}");
        }

        /*【学习声明语句】
			声明语句是用于声明局部变量或常量的语句
			声明语句允许在块中使用，但不允许作为嵌入语句
		 */
        public static void LearnDeclarationStatement()
		{
			//以下均为声明语句
			int x1;					//声明一个局部变量
			string s1, s2;			//声明两个同类型的局部变量，变量可用,隔开
			var aChar = 'a';	//声明一个隐式类型的局部变量，编译器可以根据赋值推导实际类型
			int x2 = 2;             //声明一个局部变量并进行初始化

			unsafe
			{
				int* intPtr = &x2;  //声明一个局部变量，即使包含指针和引用这也是一个语句
			}

			string outputString = "以下代码均为声明语句：\n\n" +
				"int x1;				//声明一个局部变量\n" +
				"string s1, s2;			//声明两个同类型的局部变量，变量可用,隔开\n" +
				"var aChar = 'a';		//声明一个隐式类型的局部变量，编译器可以根据赋值推导实际类型\n" +
				"int x2 = 2;			//声明一个局部变量并进行初始化\n\n" +
				"unsafe\n" +
				"{\n" +
				"	int* intPtr = &x2;  //声明一个局部变量，即使包含指针和引用这也是一个语句\n" +
				"}\n";

			Console.WriteLine(outputString);
        }

        /*【学习块】
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
        {   //方法的主体其实就是一个块
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

        /*【学习表达式语句】
			表达式语句计算给定表达式的值
			由表达式计算的值(如果有)将被丢弃
			并非所有表达式都允许作为语句
				比如x+y和x==1这样仅计算将被放弃值的表达式不允许作为语句
				只有赋值表达式、方法或委托调用表达式、递增递减表达式和new表达式可以作为语句
		 */
		public static void LearnExpressionStatement()
		{
			int x = 12, y = 13;
			//x + y;  //这样的表达式不能作为语句
			//x == y; //这样的表达式不能作为语句

			Console.WriteLine($"声明并初始化两个整数变量x={x},y={y}");
            //只有赋值表达式、方法或委托调用表达式、递增递减表达式和new表达式可以作为语句
            y += x;
            Console.WriteLine($"y += x;表达式语句执行后y的值：{y}");
            x++;
			Console.WriteLine($"x++;表达式语句执行后x的值：{x}");
		}

        /*【学习选择语句】
			选择语句根据某个表达式的值来将控制跳转到多个可能的语句之一
			具体内容参见 LearnSelectionStatement部分
		 */
        public static void LearnSelectionStatement()
        {
            LearnCSharp.Basic.LearnSelectionStatement.StartLearnSelectionStatement();
        }

        /*【学习迭代语句】
			迭代语句用于重复执行嵌入语句或语句块
			具体内容参见 LearnIterationStatement部分
		 */
        public static void LearnIterationStatement()
        {
            LearnCSharp.Basic.LearnIterationStatement.StartLearnIterationStatement();
        }

        /*【学习跳转语句之goto语句】
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
			start: Console.WriteLine("请输入一个整数");  //定义了一个start标签来标记语句

			if (int.TryParse(Console.ReadLine(), out int integer))
				goto end;  //跳转到执行end标签标记的语句
			else
				goto start;//跳转到执行start标签标记的语句

			end: Console.WriteLine("你输入的数字是：{0}",integer); //定义了一个start标签来标记语句
		}

		/*【学习跳转语句之break、continue语句】
			break语句将终止最接近的封闭迭代语句（while、do-while、for、foreach语句）或switch语句
				break语句只能退出包含它的那一个switch或迭代语句
				break语句将控制权转交给已终止语句后面的语句（如有）
			continue语句用于跳过包含它的最接近的while、do-while、for或foreach语句的其中一次迭代并启动下一次迭代
			
			如果要从嵌套的多个循环中跳转多层控制，需合理使用goto语句和带标签的语句
		 */
		public static void LearnBreakAndContinueStatement() 
		{
			int[] integers = { 1, -2, 3, -4, 5, 6, -1, 0, -3, 4};
			int count = 1;

			Console.WriteLine("现有一个整数数组：1，-2，3，-4，5，6，-1，0，-3，4");
			//设计嵌套两个循环
			//内层循环遍历数组integers，
				//如果遇到元素0则修改该元素为10，并结束本次循环
				//如果遇到元素10则跳出所有循环并结束
				//其他时候输出正整数
			while(true)
			{
				Console.WriteLine($"第{count}次进入内部for循环：");
				for (int i = 0; i < integers.Length; i++)
				{
					if (integers[i] < 0)
					{
						Console.WriteLine("\t使用continue语句跳过负数");
						continue;
					}
					else if (integers[i] == 0)
					{
						integers[i] = 10;
						Console.WriteLine("\t遇到元素0，将其修改为了10，并使用break语句跳出本次for循环");
						break;
					}
					else if (integers[i] == 10)
					{
						Console.WriteLine("\t遇到元素10，使用goto语句跳出所有循环");
						goto end;
					}
					else
					{
						Console.WriteLine($"\t输出数组中遍历到的正整数:{integers[i]}");
					}
				}
				count++;
			}

		end: Console.WriteLine("使用goto跳出所有循环，本次执行结束");
		}

		/*【学习跳转语句之throw、return语句】
			throw语句用于抛出异常或抛出被catch捕获的异常
				throw语句常结合try语句使用，更多知识参考LearnException部分
			return语句终止它所在的函数的执行，并将控制权和结果（如有）返回给当前调用方
		 */
		public static void LearnThrowAndReturnStatement()
		{
			//定义一个局部方法用于演示throw和return
			int Sum(int left,int right)
			{
				long result = (long)left + (long)right;
				if (result > int.MaxValue || result < int.MinValue)
				{
					throw new OverflowException(nameof(result)); //两个int类型整数的和如果超出int类型存储范围了就抛出异常
				}
				
				return (int)result;	//当方法有返回类型时，return后面必须跟一个兼容返回类型的值或表达式
									//如果方法内具有选择分支语句，则必须确保每一个分支结束都能返回兼容类型的值或表达式
			}

			Console.WriteLine("使用局部方法计算两个int类型整数" +
				"{0}和{1}的值：{2}", 100, 200, Sum(100, 200));

			//下列注释代码运行时会出现异常，异常信息由Sum方法内throw抛出
            //Console.WriteLine("使用局部方法计算两个int类型整数" +
            //    "{0}和{1}的值：{2}", int.MaxValue, 1, Sum(int.MaxValue, 1));

            return; //方法无返回类型时可直接用return结束函数执行
					//但一般如果是在无返回类型方法的最后面，return是可选的，默认不需要
					//如果是在执行过程中由控制流判断需要结束函数执行，则必须使用
		}

		/*【学习try语句】
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
			int[] integers = { 1, 2, 0, -8, 6 };

			Console.WriteLine("提供一个数组长度为5的整数数组：1，2，0，-8，6\n" +
				"使用for循环遍历输出每一个元素但是迭代次数大于数组长度\n");

			for (int i = 0; i < 10; i++)
			{
				try
				{
					Console.WriteLine("输出整数数组第{0}个元素：{1}", i + 1, integers[i]); ;
				}
				catch(IndexOutOfRangeException e)
				{
					Console.WriteLine("出现异常：{0}", e.Message);
					Console.WriteLine("处理异常：使用break结束循环");
					break;
				}
				catch
				{
					throw;
				}
				finally
				{
					Console.WriteLine("程序输出执行完毕");
				}
			}
		}

		/*【学习using语句】
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
			//示例创建并写入和读取一个log.txt文件
			//TextWriter类和TextReader类都实现了IDisposable接口
			//这两个类会使用非托管的资源，故需即时使用Dispose方法释放

			if(!File.Exists("log.txt"))
			{
				using var stream = File.CreateText("log.txt");
				//using语句从C#8.0开始可以简化为如上形式，在其后可继续编写代码
				//处理资源，当该using所在的块执行到终结点时会自动调用资源的dispose方法执行释放
			}

			using(TextWriter writer = File.AppendText("log.txt"))
			{
				writer.WriteLine("记录写入日志：由LearnUsingStatement写入日志 行01 | 时间：{0}", DateTime.Now);
                writer.WriteLine("记录写入日志：由LearnUsingStatement写入日志 行02 | 时间：{0}", DateTime.Now);
            }

			using(TextReader reader = File.OpenText("log.txt"))
			{
				string? s;
				while ((s = reader.ReadLine()) != null) 
				{
					Console.WriteLine("读取日志--{0}", s);
				}
			}
		}

		/*【学习yield语句】
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
			IEnumerable<int> OutputIfPositive(int[] integers)
			{
				Console.WriteLine("迭代器：开始运行");
				for (int i = 0; i < integers.Length; i++)
				{
					Console.WriteLine("迭代器：即将提供该值：{0}", integers[i]);

					if (integers[i] > 0)
					{
						yield return integers[i];
						Console.WriteLine("迭代器：已经提供迭代值：{0}", integers[i]);
					}
					else
					{
						Console.WriteLine("迭代器：发现负数，即将结束迭代");
                        yield break;//显示结束迭代
                    }					
				}
				Console.WriteLine("迭代器：结束");
			}

			int[] integers = { 1, 5, -9, 0, 6, -3, 4 };

			//支持迭代器的成员支持以foreach循环遍历器数据
			Console.WriteLine("现有一个数组：1，5，-9，0，6，-3，4\n" +
				"现在使用一个迭代器返回其中的正整数");
			Console.WriteLine("调用：迭代");

			foreach (var positive in OutputIfPositive(integers))
			{
				Console.WriteLine("调用方获得并处理迭代值：{0}", positive);
				Console.WriteLine("这是数组中的正整数:{0}", positive);
			}
		}

        /*【学习checked和unchecked语句】
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
			checked
			{
				//byte byteNumber = (byte)256;
				//因为checked语句会告知编译器进行溢出检查，故注释语句无法通过编译

				int maxByte = byte.MaxValue;
				//这是合法的赋值，故能通过溢出检查并通过编译
				Console.WriteLine("int类型变量maxByte能存储byte类型的最大值:{0}",maxByte);
			}

			unchecked
			{
				int integer = (int)double.MaxValue;
				//将double最大值强制转换为int类型会导致溢出异常
				//使用unchecked语句编译时不检查 但运行时会输出溢出后的错误值
				Console.WriteLine("unchecked语句下不进行溢出检查\n" +
					"将double类型能存储的最大值转型赋给int类型导致溢出，输出结果：{0}",integer);
			}
		}

        /*【学习lock语句】
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
            var SampleMethodDelegate = () =>
            {
                lock (objLock)
                {
                    countCalled++;
                    Console.WriteLine("本次程序执行中调用了SampleMethod方法{0}次", countCalled);
                }
            };//这是一个委托，现在不用作具体了解，只用知道它可以托管一个或多个兼容的方法

            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(SampleMethodDelegate);
            }
            await Task.WhenAll(tasks);
        }

        /*【学习空语句】
		    空语句即不执行任何操作的语句
			在需要语句的上下文中没有要执行的操作时，将使用空语句
			空语句的执行只是将控制转移到语句的终结点。
				因此如果可以访问空语句，就可以访问空语句的结束点
		 */
        public static void LearnEmptyStatement()
		{
			const bool isUnreachable = false;

			if (isUnreachable)
				Console.WriteLine("可访问");
			else
			{
				Console.WriteLine("跳转到空语句");
                goto Exit;
            }				

			Exit:;//这里是一个空语句，仅有一个分号表示语句结束，语句并无内容与操作
		}

		/*【学习unsafe语句】
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
			string outputString = "以下代码即unsafe语句，嵌入其中的语句属于非托管代码：\n\n" +
                "unsafe\n" +
				"{\n" +
				"	string* stringPtr = &outputString;\n" +
				"	Console.WriteLine(\"内存地址：{0}\", (int)stringPtr);\n" +
				"	Console.WriteLine(\"通过指针输出字符串--{0}\", *stringPtr);\n" +
				"}\n";
			
			unsafe
			{
				string* stringPtr = &outputString;
				Console.WriteLine("内存地址：{0}", (int)stringPtr);
				Console.WriteLine("通过指针输出字符串--{0}", *stringPtr);
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
				"017 unsafe语句";

            do
            {
                Console.WriteLine("【学习语句】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001":LearnEnd_pointsAndReachability(); break;
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
                    case "015":LearnLockStatement(); break;
                    case "016":LearnEmptyStatement(); break;
                    case "017":LearnUnsafeStatement(); break;
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
