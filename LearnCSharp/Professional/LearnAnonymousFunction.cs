/*【学习匿名函数】
 * 匿名函数
	• 匿名函数，顾名思义，即没有名字只有主体的函数
		○ 匿名函数是表示“内联”方法定义的表达式
		○ 匿名函数本身不具有值或类型，但可以转换为兼容的委托或表达式树类型
		○ 匿名函数转换的计算取决于转换的目标类型
			§ 如果该类型为委托类型，则转换为引用匿名函数定义的方法的委托实例
			§ 如果该类型为表达式树类型，则转换为表达式树，该树将方法的结构表示为对象结构
	• 匿名函数包含匿名方法和Lambda表达式
		○ 匿名方法
		○ Lambda表达式

 * 匿名方法
	• 在C#支持Lambda语句或表达式之前（即C#2.0），是使用匿名方法来实现相关功能的
	• 匿名方法（Anonymous methods） 提供了一种传递代码块作为委托参数的技术
	• 匿名方法是没有名称只有主体的方法
		○ 在匿名方法中您不需要指定返回类型，它是从方法主体内的 return 语句推断的
	• 匿名方法使用delegate运算符来创建，且匿名方法不能独立出现
		○ "delegate"关键字也说明匿名方法一般是用于初始化委托使用的
		○ 匿名方法只能作为赋值运算符（=、+=、-=）的右操作数
		○ 匿名方法的形式
			§ 可选有效的修饰符 delegate(参数列表){ 方法体 }
				□ 可选的有效修饰符：该项是可选配置
					® static：标识匿名方法为静态匿名方法
						◊ 静态匿名方法无法从封闭范围捕获局部变量或实例状态
				□ delegate：该项是必选配置，这是声明匿名方法的运算符
				□ (参数列表)：该项是可选配置，参数规则同常规方法参数
					® 该项可省略，当省略时该匿名方法可以作为任何参数为“传值参数”委托类型的实例
                    ® 如果无参但未省略括号，则编译器能推断出兼容其的委托类型，否则则无法推断
				□ { 方法体 }：该项是必选配置，这是匿名方法的方法体
					® 方法体内的代码要求同常规方法

 * Lambda表达式
	• 使用Lambda表达式来创建匿名函数
		○ 使用Lambda声明运算符=>从其主体中分离Lambda参数列表
			§ 声明Lambda表达式需要在=>左侧指定输入参数（如果有）
				□ 如果无参数，=>的“()”不能省略，这是和匿名方法的明显区别之一
			§ 声明Lambda表达式需要在=>右侧输入表达式或语句块
		○ Lambda表达式可采用以下任意一种形式
			§ 表达式Lambda：表达式为其主体
				□ 表达式位于=>运算符右侧的Lambda表达式称为“表达式Lambda”
				□ 表达式Lambda会返回表达式的结果
				□ 表法是Lambda的基本形式
					® (参数列表) => 表达式
				□ 表达式Lambda的主体可以是一个方法调用
					® 若使用表达式Lambda创建在CLR上下文之外计算的表达式树，则不得使用方法调用作为主体
			§ 语句Lambda：语句块作为其主体
				□ 使用{}将语句列表包含并将其放置于=>右侧的Lambda表达式称为“语句Lambda”
				□ 语句Lambda的主体可以包含任意数量的语句
					® 但实际运用中通常不会多于两到三个
					® 如果包含语句数量太多，应考虑将语句列表提取为一个方法或局部方法
				□ 语句Lambda的基本形式
					® (参数列表) => { 语句列表 }
		○ Lambda表达式的输入参数
			§ 将Lambda表达式的输入参数括在括号“()”中，使用空括号指定零个输入参数
			§ 如果Lambda表达式只有一个输入参数，则括号是可选的
			§ 如果Lambda表达式有两个或更多输入参数，则使用逗号“,”加以分隔
			§ 如果编译器可以推断输入参数的类型，则可以省略参数列表形参的数据类型声明
				□ 否则，必须显示声明参数的数据类型
				□ 输入参数类型必须全部为显示或全部为隐式声明
		○ Lambda表达式不能独立作为语句出现，它需要放在赋值操作符右侧进行使用
			§ 赋值操作符左侧可以是委托实例、表达式树实例或事件
	• 异步Lambda
		○ 通过使用async和await关键字，可以轻松创建包含异步处理的Lambda表达式和语句
		○ 关于异步编程参阅后续异步编程相关内容
	• Lambda表达式和元组
		○ C#语言提供对元组的内置支持
			§ 可以提供一个元组作为Lambda表达式的参数
			§ Lambda表达式可以返回元组
			§ 某些情况下，编译器使用类型推理来确定元组组件的类型
	• Lambda表达式中的类型推理
		○ 编写Lambda表达式时，通常不必为输入参数指定类型，也无需指定返回类型
			§ 编译器可以根据Lambda主体、参数类型以及C#语言规范中描述的其他因素来推断类型
		○ Lambda类型推理的一般规则
			§ Lambda包含的参数数量必须与委托类型包含的参数数量相同
			§ Lambda中的每个输入参数必须都能够隐式转换为其对应的委托参数
			§ Lambda的返回值（如果有）必须能够隐式转换为委托的返回类型
	• Lambda表达式的自然类型
		○ Lambda表达式本身没有类型
			§ 通用类型系统没有“Lambda表达式”这一固有概念
		○ 有时以非正式的方式谈论Lambda表达式的“类型”会很方便
			§ 该非正式“类型”是指委托类型或Lambda表达式所转换到的表达式树（Expression）类型
		○ 从C#10开始，Lambda表达式可能具有自然类型
			§ 编译器不会强制要求为Lambda表达式声明委托类型
				□ 编译器可以根据Lambda表达式推断委托类型，即可以使用var关键字来声明一个变量来接收Lambda表达式
					® var 变量名 = Lambda表达式
				□ 编译器会根据Lambda表达式来推断选择可用的Action或Func委托（如果存在合适的委托）
					® 否则编译器将合成一个委托类型来接收Lambda表达式
						◊ 例如Lambda表达式具有ref、in、out参数，则合成委托类型
			§ 如果Lambda表达式具有自然类型，可将其分配给不太显示的类型
				□ 例如 System.Object 或 System.Delegate
			§ 只有一个重载的方法组（即没有参数列表的方法名称）具有自然类型
				□ 即该方法没有被重载的情况
			§ 如果将具有自然委托类型的Lambda表达式分配给 System.Linq.Expressions.LambdaExpression 或 System.Linq.Expressions.Expression 
				□ 则表达式的自然类型为 System.Linq.Expressions.Expression<TDelegate> 其中自然委托类型用作类型参数形参的实参
	• 显示返回类型
		○ 通常Lambda表达式的返回类型时显而易见的并且是编译器推断出来的
		○ 从C#10.0开始，可以在输入参数前面指定Lambda表达式的返回类型
			§ 指定显示返回类型时，必须将输入参数括起来，即使只有一个输入参数
			§ 建议当且仅当编译器无法推断出具体返回类型时才显示指定返回类型
	• 附加特性
		○ 从C#10.0开始，可以将特性附加到Lambda表达式极其参数
		○ 将特性附加到Lambda表达式或其参数时，必须将输入参数用括号括起来，即使只有一个输入参数
	• Lambda表达式捕获外部变量和变量范围
		○ Lambda表达式可以引用外部变量
			§ 外部变量是在定义Lambda表达式的方法中或包含Lambda表达式的类型中的范围内的变量
			§ Lambda表达式捕获了的外部变量将进行存储以备在Lambda表达式中使用
				□ 即使在其他情况下，这些变量将超出范围并进行垃圾回收
			§ 必须明确地分配外部变量，然后才能在Lambda表达式中使用该变量
		○ 下列规则适用于Lambda表达式中的变量范围
			§ 捕获的变量将不会被作为垃圾回收，直至引用变量的委托符合垃圾回收的条件
			§ 在封闭方法中看不到Lambda表达式内引入的变量
			§ Lambda表达式无法从封闭方法中直接捕获in、ref或out参数
			§ Lambda表达式中的return语句不会导致封闭方法返回
			§ Lambda表达式不得使用goto、break或continue语句跳转至位于Lambda表达式块之外的目标
				□ 同样，Lambda表达式外部也不能使用跳转语句跳转至Lambda表达式主体内的目标
		○ Lambda表达式捕获了封闭范围中的外部变量时
			□ 编译器会将Lambda表达式和其捕捉的外部变量封装成一个内部类
				® 外部变量成为这个类的实例字段
				® Lambda表达式会成为这个类的实例方法
				® Lambda表达式内捕获的外部变量此时在实例方法中替换成了对应的实例成员
			□ 包含成员原本的实现会改变为
				® 先声明并初始化一个内部类的实例
				® 将原本的外部变量替换成实例字段
				® 将原本的Lambda表达式替换成实例方法并赋值给委托
			□ 如果外部变量是循环变量
				® 循环变量和Lambda表达式会封装在一个内部类01中成为实例字段和实例方法
				® 如果同时捕获了非循环变量的外部变量，编译器会将该外部变量单独封装成一个内部类02
					◊ 同时会在内部类01中创建一个内部类02类型的实例字段
		○ 静态Lambda表达式
			§ 从C#9.0开始，可以将static修饰符应用于Lambda表达式
			§ 静态Lambda表达式无法从封闭范围中捕获本地变量或实例状态
			§ 静态Lambda表达式可以引用外部静态成员和常量

 * 本地函数与Lambda表达式
	• 本地函数与Lambda表达式许多情况下可由开发者自行考虑选择使用
	• 区别
		○ 声明和命名
			§ 本地函数的声明和命名方式与常规方法几乎相同
			§ Lambda表达式是一种匿名方法，需要分配给委托（delegate）类型的变量
			§ 本地函数可以独立声明和使用，Lambda表达式需要先赋值于委托再由委托来调用
		○ 函数签名与Lambda表达式类型
			§ 本地函数需要明确指定返回类型和参数类型（如有）
			§ Lambda表达式可以根据委托类型来自行推断其返回类型和参数类型
		○ 明确赋值
			§ 本地函数在编译时定义
				□ 可以在定义本地函数的块内任意位置调用本地函数
				□ 当成员内部有功能需要使用递归来实现时，本地函数是一个不错的选择
			§ Lambda表达式是在运行时分配对象
				□ Lambda表达式不能直接单独定义
				□ 必须先声明并定义一个委托类型实例，并将Lambda表达式赋值给该实例
		○ 实现为委托
			§ 本地函数可以直接调用，也可在包含成员内部赋值于类型兼容的委托
			§ Lambda表达式在声明时即要求转换为类型兼容的委托实例
				□ Lambda表达式通过基础委托类型调用
				□ 委托的Invoke方法不会检查Lambda表达式的附加特性
					◊ 调用Lambda表达式时，对附加特性不产生任何影响
					◊ Lambda表达式的附加特性对于代码分析很有用，可以通过反射发现
					◊ System.Diagnostics.ConditionalAttribute不能应用于Lambda表达式
		○ 外部变量捕获
			§ 外部变量
				□ 该变量是一个局部变量，该变量属于包含本地函数的成员
				□ 该变量在本地函数外部声明，并在本地函数内部使用
				□ 本地函数的形参不属于外部变量
				□ 将本地函数外部声明的变量作为实参传递不算做捕获
			§ 本地函数能够在封闭范围内明确分配捕获的变量
			§ 本地函数捕获了封闭范围中的外部变量时，本地函数将作为委托类型实现
				□ 本地函数会被转换成一个委托，委托的参数列表是由本地函数原本的参数列表和封装外部变量的值类型组成
					® 封装外部变量的值类型是以引用参数形式传递的
				□ 外部变量会被封装成一个值类型的成员，这个值类型是包含成员所在类的一个内部类
				□ 如果外部变量是循环变量，每捕获一个外部变量都会为其单独创建一个值类型来封装
			§ Lambda表达式捕获了封闭范围中的外部变量时
				□ 编译器会将Lambda表达式和其捕捉的外部变量封装成一个内部类
					® 外部变量成为这个类的实例字段
					® Lambda表达式会成为这个类的实例方法
					® Lambda表达式内捕获的外部变量此时在实例方法中替换成了对应的实例成员
				□ 包含成员原本的实现会改变为
					® 先声明并初始化一个内部类的实例
					® 将原本的外部变量替换成实例字段
					® 将原本的Lambda表达式替换成实例方法并赋值给委托
				□ 如果外部变量是循环变量
					® 循环变量和Lambda表达式会封装在一个内部类01中成为实例字段和实例方法
					® 如果同时捕获了非循环变量的外部变量，编译器会将该外部变量单独封装成一个内部类02
						◊ 同时会在内部类01中创建一个内部类02类型的实例字段
		○ 堆分配
			§ 如果本地函数永远不会转换为委托，并且本地函数捕获的变量都不会被其他转换为委托的Lambda表达式或本地函数捕获，则编译器可以避免堆分配
			§ 满足以上条件可以将本地函数声明为静态本地函数来确保避免在堆上对其进行分配
			§ Lambda表达式将始终在堆上进行分配
		○ yield关键字的用法
			§ 当在迭代器中时，将本地函数作为迭代器实现可以使用yield return语句生成一系列值
			§ Lambda表达式是不允许使用yield return语句

 * Lambda表达式源起
	• Lambda表达式的概念来自阿隆佐·邱奇（Alonzo Church）
		○ 他于20世纪30年代发明了用于函数研究的 λ 演算（lambda calculus）系统
		○ 用邱奇的记号法，如果函数要获取参数x，最终的表达式是y，就将希腊字母 λ 作为前缀，再用点号分隔参数和表达式
            § 所以，C#的Lambda表达式 x=>y 用邱奇的记号法应写成 λx.y
			§ 由于在C#代码中不便输入希腊字母，而且点号在C#中有太多含义
				□ 所以C#委托选择“胖箭头”（fat arrow）记号法（=>）
                □ “Lambda表达式”提醒人们匿名函数的理论基础是λ演算，即使根本没有使用希腊字母λ
 */


namespace LearnCSharp.Professional
{
    internal class LearnAnonymousFunction
    {
		/*【20201：匿名方法代码示例】*/
        public static void LearnAnonymousMethods()
        {
            Console.WriteLine("\n------示例：匿名方法------\n");
            //匿名方法的简单使用 声明一个具有两个整型参数并返回bool值的匿名方法，并将其赋值给一个Func<int, int, bool>委托
            Func<int, int, bool> compare = delegate (int x, int y) 
            {
                return x >= y;
            };
            
            Console.Write("请输入第一个整数：");
            int x = int.Parse(Console.ReadLine()!);
            Console.Write("请输入第二个整数：");
            int y = int.Parse(Console.ReadLine()!);

            Console.WriteLine("通过委托调用匿名方法输出 整数{0} >= 整数{1} 的比较结果：{2}。", x, y, compare(x, y));

            //匿名方法的简单使用 声明一个无参并返回string类型字符串的匿名方法
            Func<string> info = delegate //也可以写成delegate()；无参匿名方法可以省略()；
            {
                return "委托调用了我这个匿名方法，yaho！";
            };

            Console.WriteLine(info.Invoke());
        }

        /*【20202：语句Lambda代码示例】*/
        public static void LearnStatement_Lambda()
        {
            Console.WriteLine("\n------示例：语句Lambda------\n");
            //示例：无参无返回类型的Action委托接收一个语句Lambda表达式
            Action action = () =>
            {
                Console.WriteLine("这是一个“语句Lambda”表达式，无参，且无返回值");
                return;//这个语句Lambda表达式形参，且可根据接收它的委托推断出不需要返回值，所以return可选
            };

            //示例：有参有返回类型的Func委托接收一个语句Lambda表达式
            Func<int,int,(int,int)> swap = (x, y) =>
            {
                int temp = x;
                x = y;
                y = temp;
                return (x, y);
            };

            action.Invoke();
            var result = swap.Invoke(5, 6);
            Console.WriteLine("使用Func<int, int, (int, int)>委托调用其接收的Lambda表达式返回" +
                " 整数x:{0} 和 整数y:{1} 交换后的结果：x:{2} y:{3}", 5, 6, result.Item1, result.Item2); 
        }

		/*【20203：表达式Lambda代码示例】*/
        public static void LearnExpression_Lambda()
        {
            Console.WriteLine("\n------示例：表达式Lambda------\n");
            //示例：使用Action委托接收一个仅含一句输出语句的Lambda表达式
            Action action = () => Console.WriteLine("这是一个“表达式Lambda”表达式，无参，且无返回值");
            
            //示例：使用Func<int,int,int>委托接收一个使用两个整数返回最大数的Lambda表达式
            Func<int, int, int> max = (x, y) => Math.Max(x, y);

            action.Invoke();
            var result = max.Invoke(5, 6);
            Console.WriteLine("使用Func<int, int, int>委托调用其接收的Lambda表达式返回" +
                " 整数{0} 和 整数{1} 之中最大值的结果：{2}", 5, 6, result);
        }

        /*关于Lambda表达式和局部变量的关系与实现
         *CLR不知道何谓Lambda表达式和匿名方法。相反，编译器遇到匿名函数时，会把它转换成特殊的
          隐藏类、字段和方法，从而实现我们希望的语义。
         *Lambda表达式只是C#给我们的一个“语法糖”，用于提升代码的简洁性和可读性、可维护性。
         */

        /*【20204：Lambda表达式（或匿名方法）未使用其主体外部声明的变量】
         *该情况下 编译器遇到匿名函数会将其转换成单独的、由编译器内部声明的静态方法。
         该静态方法再实例化成一个委托并作为参数传递。
        */
        private static void PrintSomething(Func<string> print) => Console.WriteLine(print.Invoke());
        public static void LearnLambdaNotCatchOuterVariable()
        {
            Func<string> func = () =>
            {
                Console.Write("请输入您的姓名：");
                string s = Console.ReadLine()!;
                return string.Format("Welcome To C# World！{0}！", s);
            };

            PrintSomething(func);

            /*对于该方法中的Lambda表达式会被编译器进行如下转换
             * 
               public static void LearnLambdaNotCatchOuterVariable()
              {
                    Func<string> func = LearnLambda.__AnonymousMethod_00000000;
                    PrintSomething(func);
               }

               private static string __AnonymousMethod_00000000()
              {
                    Console.Write("请输入您的姓名：");
                    string s = Console.ReadLine()!;
                    return string.Format("Welcome To C# World！{0}！", s);
               }
             */
        }

        /*【20205：Lambda表达式（或匿名方法）使用了其主体外部声明的变量】
         *在Lambda表达式外部声明的局部变量（包括包容方法的参数）称为该Lambda的外部变量（this引用虽然技术上说不是变量，但也被视为外部变量）
         *如果Lambda表达式主体使用一个外部变量，那么就说该变量被该Lambda表达式捕捉
         */
        public static void LearnLambdaCatchOuterVariable()
        {
            List<string> names = new List<string> { "张三", "李四", "王五", "赵六" };
            int count = 0;
            Action action = () =>
            {
                //获取当前姓名列表内数量
                count = names.Count;
                Console.WriteLine("当前列表内有以下姓名：");
                foreach (var item in names)
                {
                    Console.Write(item + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("请继续输入你的姓名：");
                names.Add(Console.ReadLine()??"未输入");
                count++;
            };

            action.Invoke();

            Console.WriteLine("当前列表内有以下姓名：");
            foreach (var item in names)
            {
                Console.Write(item + "  ");
            }
            Console.WriteLine();
            Console.WriteLine("共计有{0}个姓名", count);

            /*对于该方法中的Lambda表达式会被编译器进行如下转换
             *注意：被捕捉的局部变量永远不会被“传递”或“拷贝”到别的地方，
              相反，被捕捉的局部变量作为实例字段实现，从而延长了其生存期。
              所有使用局部变量的地方都改为使用那个实例字段。
             *生成的__LocalsDisplayClass类型称为闭包(Closure)，它是一个数据结构
              （一个C#密封嵌套类），其中包含一个表达式以及对表达式求值所需的变量（即公共字段）
             
            private sealed class __LocalsDisplayClass_00000000
           {
                public List<string> names;
                public int count;
                public void __AnonymousMethod_00000000()
               {
                    //获取当前姓名列表内数量
                    count = names.Count;
                    Console.WriteLine("当前列表内有以下姓名：");
                    foreach (var item in names)
                    {
                        Console.Write(item + "  ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("请继续输入你的姓名：");
                    names.Add(Console.ReadLine());
                    count++;
                }
            }

            public static void LearnLambdaCatchOuterVariable()
           {
                __LocalsDisplayClass_00000000 locals = new __LocalsDisplayClass_00000000();
                locals.names = new List<string> { "张三", "李四", "王五", "赵六" };
                locals.count = 0;

                Action action = locals.__AnonymousMethod_00000000;
                action.Invoke();

                Console.WriteLine("当前列表内有以下姓名：");
                foreach (var item in locals.names)
               {
                    Console.Write(item + "  ");
                }
                Console.WriteLine();
                Console.WriteLine("共计有{0}个姓名", locals.count);
            }
            */
        }

        /*【20206：Lambda表达式（或匿名方法）使用了其主体外部声明的Foreach循环迭代变量】
         *关于Lambda表达式捕捉外部变量时，尤其要注意捕捉到循环中的迭代变量
         *以下方法执行的结果我们认为的会是：
          张三
          李四
          王五
          赵六
         *在C# 5.0及之后确实如此
         *但在C# 4.0及之前的输出会如下：
          赵六
          赵六
          赵六
          赵六
         *因为基于上一部分中的解释，本例中的Lambda表达式会被转换成一个嵌套类，其中item会
          转换为其一个公共实例字段，所以每一次循环实际都会重新为该字段赋值，表象来看就是
          Lambda表达式捕捉变量并总是使用其最新的值——而不是捕捉并保留变量在委托创建时的值
         *但在C# 5.0中，对foreach循环这一情况下Lambda捕捉外部变量的行为进行了改变，
          认为每一次循环迭代，foreach循环变量都应该是“新变量”。所以，每次创建委托，
          捕捉的都是不同的变量，不再共享同一个变量。
         */
        public static void LearnLambdaCatchOuterForeachLoopVariable()
        {
            string[] names = { "张三", "李四", "王五", "赵六" };
            int countOutput = 0;
            List<Action> actions = new List<Action>();

            foreach (var item in names)
            {
                Action action = () =>
                {
                    Console.WriteLine(item);
                    countOutput++;
                };
                actions.Add(action);
            }
            foreach (var item in actions)
            {
                item.Invoke();
            }
            Console.WriteLine("输出姓名次数：{0}\n", countOutput);
        }

        /*示例LearnLambdaCatchOuterForeachLoopVariable中的代码会被编译器转换为如下代码
        private sealed class __LocalsDisplayClass_00000000
        {
            public int countOutput;
        }

        private sealed class __LocalsDisplayClass_00000001
        {
            public string item;
            public __LocalsDisplayClass_00000000 locals1;

            public void __AnonymousMethod_00000001()
            {
                Console.WriteLine(item);
                locals1.countOutput++;
            }
        }

        public static void LearnLambdaCatchOuterForeachLoopVariable()
        {
            __LocalsDisplayClass_00000000 locals1 = new __LocalsDisplayClass_00000000();
            string[] names = new string[] { "张三", "李四", "王五", "赵六" };
            locals1.countOutput = 0;
            List<Action> actions = new List<Action>();

            foreach (var item in names)
            {
                __LocalsDisplayClass_00000001 locals = new __LocalsDisplayClass_00000001();
                locals.locals1 = locals1;
                locals.item = item;
                actions.Add(locals.__AnonymousMethod_00000001);
            }
            foreach (var item in actions)
            {
                item.Invoke();
            }
            Console.WriteLine(locals1.countOutput);
        }*/

        /*示例LearnLambdaCatchOuterLoopVariable中的代码会在C#4.0及之前被转换为如下代码
        private sealed class __LocalsDisplayClass_00000000
        {
            public int countOutput;
        }

        private sealed class __LocalsDisplayClass_00000001
        {
            public string item;
            public __LocalsDisplayClass_00000000 locals1;

            public void __AnonymousMethod_00000001()
            {
                Console.WriteLine(item);
                locals1.countOutput++;
            }
        }

        public static void LearnLambdaCatchOuterForeachLoopVariable()
        {
            __LocalsDisplayClass_00000001 locals = new __LocalsDisplayClass_00000001();
            locals.locals1 = new __LocalsDisplayClass_00000000();
            string[] names = new string[] { "张三", "李四", "王五", "赵六" };
            locals.locals1.countOutput = 0;
            List<Action> actions = new List<Action>();

            foreach (var item in names)
            {
                locals.item = item;
                actions.Add(locals.__AnonymousMethod_00000001);
            }
            foreach (var item in actions)
            {
                item.Invoke();
            }
            Console.WriteLine(locals.locals1.countOutput);
        }*/


        /*【20207：Lambda表达式（或匿名方法）使用了其主体外部声明的For循环迭代变量】
         * 上一条所述的更改不适用于其他循环，例如将foreach代码更改为如下代码，
          for (int i = 0; i < names.Length; i++)
          {
              actions.Add(() => Console.WriteLine(names[i]));
          }
          foreach (var item in actions)
          {
              item.Invoke();
          }
          每次循环创建委托后，变量都会保留其最新值4，如果继续执行委托调用，则会
          报错[Index was outside the bounds of the array.],因为不存在names[4]。
         */
        public static void LearnLambdaCatchOuterForLoopVariable()
        {
            string[] names = { "张三", "李四", "王五", "赵六" };
            int countOutput = 0;
            List<Action> actions = new List<Action>();

            try
            {
                for (int i = 0; i < names.Length; i++)
                {
                    actions.Add(() => { Console.WriteLine(names[i]); countOutput++; });
                }
                foreach (var item in actions)
                {
                    item.Invoke();
                }
                Console.WriteLine("输出姓名次数：{0}\n", countOutput);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*示例4会被编译器转换为如下代码
        private sealed class __LocalsDisplayClass_00000000
        {
            public string[] names;
            public int countOutput;
        }

        private sealed class __LocalsDisplayClass_00000001
        {
            public int i;
            public __LocalsDisplayClass_00000000 locals1;

            public void __AnonymousMethod_00000001()
            {
                Console.WriteLine(locals1.names[i]);
                locals1.countOutput++;
            }
        }

        public static void Test()
        {
            __LocalsDisplayClass_00000000 locals1 = new __LocalsDisplayClass_00000000();
            locals1.names = new string[] { "张三", "李四", "王五", "赵六" };
            locals1.countOutput = 0;
            List<Action> actions = new List<Action>();
            __LocalsDisplayClass_00000001 locals = new __LocalsDisplayClass_00000001();
            locals.locals1 = locals1;
            

            for (locals.i = 0; locals.i < locals.locals1.names.Length; locals.i++)
            {
                actions.Add(locals.__AnonymousMethod_00000001);
            }
            foreach (var item in actions)
            {
                item.Invoke();
            }

            Console.WriteLine(locals.locals1.countOutput) ;
        }*/

        /*注意，Lambda表达式是一个匿名函数，这个匿名函数会直接用于创建一个委托实例，
          在实际执行过程中顺序是如下的：
          将匿名函数转换为类型兼容的委托->委托使用Invoke方法调用该匿名函数->执行匿名方法
          所以需要注意，当Lambda表达式捕捉的外部变量是循环变量且作为循环判断条件使用时
          不要在使用Lambda表达式来循环创建委托实例时又在表达式主体内进行处理来结束循环，
          因为这样做的结果时运行时由于永远不会优先执行匿名函数导致循环变量不会改变而导致无限循环。
          如果代码如下，运行时将会进入无限循环：
          int i = 0;
          while(i < names.Length)
          {
              Action action = () =>
              {
                  Console.WriteLine(names[i]);
                  i++;
               };
              actions.Add(action);        
           }
            
          foreach (var item in actions)
          {
              item.Invoke();
           }
         */
        public static void StartLearnAnonymousFunction()
        {
            string title = "001 匿名方法\n" +
                "002 语句Lambda\n" +
                "003 表达式Lambda\n" +
                "004 Lambda表达式未捕获外部变量\n" +
                "005 Lambda表达式捕获外部变量：包含成员的局部变量\n" +
                "006 Lambda表达式捕获外部循环变量：foreach循环变量\n" +
                "007 Lambda表达式捕获外部循环变量：for循环变量\n";

            do
            {
                Console.WriteLine("【学习匿名函数--代码运行查看】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行：");

                string? input = Console.ReadLine();
                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnAnonymousMethods(); break;
                    case "002": LearnStatement_Lambda(); break;
                    case "003": LearnExpression_Lambda(); break;
                    case "004": LearnLambdaNotCatchOuterVariable(); break;
                    case "005": LearnLambdaCatchOuterVariable(); break;
                    case "006": LearnLambdaCatchOuterForeachLoopVariable(); break;
                    case "007": LearnLambdaCatchOuterForLoopVariable(); break;
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
