/*【学习标识符】
 * 标识符：标识符是程序员为类、方法、变量等选择的名字。
           标识符应该是一个完整且有意义的词，它由以字母或下划线开头的Unicode字符构成。
           C#标识符是区分大小写的。例如Name和name会被C#认为是两个不同的标识符。
           通常约定参数、局部变量以及私有字段的首个单词应该以小写字母开头（camel大小写），如inputString
           通常约定除上述以外的其他类型的标识符首个单词以大写字母开头（Pascal大小写）,如LearnKeyWords、WriteUserInput
 
 * 关键字：关键字是预定义的保留标识符，对编译器有特殊意义。
           大部分关键字是保留的，这部分关键字在程序任意部分都作为保留关键字，不得直接用作常规标识符
           还有一部分关键字是上下文相关的，这部分关键字只在部分程序上下文中有特殊意义
           如确有必要将关键字用作普通标识符，需要在其有特殊含义范围内时在其前面加@前缀进行强制
           保留关键字和上下文关键字在本部分最后的注释中提供
 
 * 注意：在Microsoft的实现中，还有4个未文档化的保留关键字：__arglist, __makeref, __reftype, __refvalue
        它们仅在罕见的互操作情形下才需要使用，平时可以完全忽略。
        注意这四个特殊关键字都已双下划线开头。
        C#设计者保留将来把这种标识符转化为关键字的权利，为安全起见，最好不要自己创建这样的标识符

 */
extern alias Helper;
using System.Reflection;
using LearnCSharp.Basic.LearnKeyWordsSpace;
using Help = Helper.HelperLibForLearnCSharp;
using static Helper.HelperLibForLearnCSharp.AccessibilityTest;

//下列各点知识对初学者仅作关键字介绍了解，具体代码不用过多专研，学习完成后续课程自然明白
namespace LearnCSharp.Basic //namespace 保留关键字
{
    internal class LearnKeyWords //类名LearnKeyWords是一个普通标识符
    {
        /*【10201：访问修饰符关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 保留关键字，用于指定访问权限
								修饰对象											针对类型本身和其对所属成员的访问性													针对类型实例对成员的访问性													备注

			internal			► 枚举|结构|接口|类|记录|记录结构					► 仅限当前程序集内访问类型															► 仅可通过当前程序集内类型的实例访问其internal级别成员						类型的默认访问级别
								► 类型的成员（枚举除外）							► 仅限当前程序集内类访问自身internal级成员
																					► 仅限当前程序集内派生类访问基类internal级成员

			public				► 枚举|结构|接口|类|记录|记录结构					► 类型本身和其对成员的访问均不受限制												► 可通过任意程序集内类型的实例访问其public级别成员	
								► 类型的成员（枚举除外）

			private				► 嵌套于其他类型的枚举|结构|接口|类|记录|记录结构	► 仅限于类型本身可访问自身private级成员												► 通常无法通过类型的实例访问其private级成员									成员的默认访问级别
								► 类型的成员（枚举除外）																												► 实例声明于所属类内部时可通过实例访问其private级成员

			protected			► 嵌套于其他类型的枚举|结构|接口|类|记录|记录结构	► 限于类型本身可访问自身protected级成员												► 通常无法通过类型的实例访问其protected级成员	
								► 类型的成员（枚举、结构除外）						► 限于派生类型访问基类protected级成员												► 实例声明于所属类内部时可通过实例访问其protected级成员

			protected internal	► 嵌套于其他类型的枚举|结构|接口|类|记录|记录结构	► 限于类型本身可访问自身protected internal级成员									► 可通过当前程序集内类型的实例访问其protected internal级别成员	
								► 类型的成员（枚举、结构除外）						► 限于派生类型访问基类protected internal级成员										► 实例声明于所属类内部时可通过实例访问其protect internal级成员
																																										► 注意：外部程序集中的当前类型的派生类，如果其实例在派生类型内部声明则可
																																										  通过其实例访问实例的protected internal级成员，但如果其实例在外部程序集
																																										  派生类型外部声明，则无法通过实例访问其实例的protected internal级成员

			private protected	► 嵌套于其他类型的枚举|结构|接口|类|记录|记录结构	► 限于类型本身可访问自身private protected级成员										► 通常无法通过类型的实例访问其private protected成员	
								► 类型的成员（枚举、结构除外）						► 限于当前程序集中派生类访问基类private protected级成员								► 实例声明于所属类内部时可通过实例访问其private protected级成员

			file				► 非嵌套的顶级枚举|结构|接口|类|记录|记录结构		► 仅限声明顶级类型的源代码文件内访问												► 无，file修饰符不能用于修饰类型成员	
								► 其余源代码文件内可声明同名类型而不冲突

		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
		 */
        public static void LearnAccessibilityLevelKeywords()
        {
            Console.WriteLine("\n------示例：访问修饰符关键字------\n");

            TestAccessibilityLevel();

            AccessibilityLevelChildInOuterAssembly levelInOuterAssembly = new AccessibilityLevelChildInOuterAssembly();
            levelInOuterAssembly.PrintAccessibilityLevel();

			Help::AccessibilityLevel levelOuterInOuterAssembly = new();
            //levelOuterInOuterAssembly.PrivateInfo = $"levelOuterInOuterAssembly--{levelOuterInOuterAssembly.PrivateInfo}";
            //levelOuterInOuterAssembly.ProtectedInfo = $"levelOuterInOuterAssembly--{levelOuterInOuterAssembly.ProtectedInfo}";
            //levelOuterInOuterAssembly.InternalInfo = $"levelOuterInOuterAssembly--{levelOuterInOuterAssembly.InternalInfo}";
            levelOuterInOuterAssembly.PublicInfo = $"levelOuterInOuterAssembly--{levelOuterInOuterAssembly.PublicInfo}";
            //levelOuterInOuterAssembly.ProtectedInternalInfo = $"levelOuterInOuterAssembly--{levelOuterInOuterAssembly.ProtectedInternalInfo}";
            //levelOuterInOuterAssembly.PrivateProtectedInfo = $"levelOuterInOuterAssembly--{levelOuterInOuterAssembly.PrivateProtectedInfo}";

            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{levelOuterInOuterAssembly.GetType()}");
            Console.WriteLine($"------通过当前类型所属程序集外部的其他类型内的本类型实例来访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法通过外部程序集其它类中的当前类实例访问private成员】");
            Console.WriteLine($"【属性名：{"ProtectedInfo",-22} | 属性访问级别：{"protected",-18} | 错误：无法通过外部程序集其它类中的当前类实例访问protected成员】");
            Console.WriteLine($"【属性名：{"InternalInfo",-22} | 属性访问级别：{"internal",-18} | 属性值：无法通过外部程序集其它类中的当前类实例访问internal成员】");
            Console.WriteLine($"【属性名：{nameof(levelOuterInOuterAssembly.PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{levelOuterInOuterAssembly.PublicInfo}】");
            Console.WriteLine($"【属性名：{"ProtectedInternalInfo",-22} | 属性访问级别：{"protected internal",-18} | 属性值：无法通过外部程序集其它类中的当前类实例访问protected internal成员】");
            Console.WriteLine($"【属性名：{"PrivateProtectedInfo",-22} | 属性访问级别：{"private protected",-18} | 错误：无法通过外部程序集其它类中的当前类实例访问private protected成员】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();
        }

        /*【10202：常用修饰符关键字】
         * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 通常可单独或组合用于修饰类型、类型成员或方法参数
						关键字类型		修饰对象													作用																					注意事项

			readonly	保留关键字		► 结构|记录结构												► 在 readonly struct 类型定义中，指示结构类型是不可变的									► 在readonly结构中对于静态属性和静态方法，不能使用readonly进行修饰
										► 字段、属性、方法、索引器									► 在字段声明中，指示只能在声明期间或在同一个类的构造函数中向字段赋值
																									► 在结构类型内的实例成员声明中，指示实例成员不修改结构的状态
																									► 在 ref readonly 方法返回中，指示该方法返回一个引用，且不允许向该引用写入内容

			static		保留关键字		► 类														► 标记一个类型为静态类型，其内部只含有静态成员											► 不能和abstract、virtual、override关键字一起使用
										► 字段、属性、方法和事件									► 声明属于类型本身而不是属于特定对象的静态成员

			unsafe		保留关键字		► 结构|接口|类|记录|记录结构								► 表示不安全上下文，该上下文是任何涉及指针的操作所必需的								► 使用不安全代码将引发安全风险和稳定性风险
										► 字段、属性、方法、索引器、事件																													► 必须使用 AllowUnsafeBlocks 编译器选项来编译包含不安全块的代码

			sealed		保留关键字		► 类|记录													► 应用于类时，sealed 修饰符会阻止其他类从该类继承										► 应用于方法或属性时，sealed 修饰符必须始终与 override 结合使用
										► 属性、方法、索引器和事件									► 使用sealed关键字阻止派生类型重写它们

			abstract	保留关键字		► 类|记录													► 指示被修改内容的实现已丢失或不完整													► 不能同时和sealed关键字一起使用
										► 方法、属性、索引器和事件（结构和记录结构的对应成员除外）	► 在类声明中指示某个类仅用作其他类的基类，不能实例化									► 不能同时和virtual、override关键字一起使用
																									► 标记为抽象的成员必须由派生自抽象类的非抽象类来实现									► 不能同时和static关键字一起使用
																									
			virtual		保留关键字		► 方法、属性、索引器和事件									► 用于使方法、属性、索引器或事件可在派生类中被重写										► 不能同时和abstract、override关键字一起使用
																																															► 不能同时和static关键字一起使用

			override	保留关键字		► 方法、属性、索引器和事件									► 扩展或修改继承的方法、属性、索引器或事件的抽象或虚拟实现								► 对应成员的基类必须有使用abstract、virtual或override修饰
																																															► 不能同时和abstract、virtual、new关键字一起使用
																																															► 不能同时和static关键字一起使用

			new			保留关键字		► 嵌套类型													► 用于强制隐藏从基类继承的成员（含sealed关键字修饰的成员）								► 这里的new是作为成员修饰符而非操作符
										► 类型成员																																			► 不能同时和abstract、virtual或override关键字一起使用

			const		保留关键字		► 常量（数字、布尔值、字符串 或 null引用）					► 用于声明常量字段或本地常量															► 声明时必须同时初始化
																									► 常量字段和局部常量不是变量，不能修改													► 不要创建一个常量来表示期望随时更改的信息
																																															► 常量的访问方式同静态成员
																																															► 不能和readonly关键字一起使用
																																															► 不能和static关键字一起使用

			volatile	保留关键字		► 字段（仅结构和类的字段）									► 指示一个字段可以由多个同时执行的线程修改												► volatile 的字段将从某些类型的优化中排除
																																															► 部分类型（包括 double 和 long）无法标记为 volatile，因为对这些类型的字段的读取和写入不能保证是原子的
			
			event		保留关键字		► 事件成员													► 用于声明事件成员																		► 事件成员必须是委托类型
			
			async		上下文关键字	► 方法														► 可将方法、lambda 表达式或匿名方法指定为异步方法										► 原则上来说async方法内部必须包含await表达式或语句
																																															► 异步方法的返回类型必须为void、Task或Task<T>、类似任务的类型、IAsyncEnumerable<T>或IAsyncEnumerator<T>
																																															► 异步方法不能声明任何in、ref或out修饰的参数，也能具有ref修饰的返回值

			extern		保留关键字		► 方法														► 用于声明在外部实现的方法																► 通常在使用Interop服务调入非托管代码时和DllImport特性一起使用，且此时必须同时使用static关键字
																																															► 不能和abstract关键字一起使用

			in			保留关键字		► 泛型参数													► 用于指示泛型类型参数是逆变的															► 仅能用于泛型接口和委托声明时的类型参数
										► 方法参数													► 方法参数：指示参数按引用而不是按值传递												► 在调用方法之前必须初始化参数
																																															► 该方法无法向参数赋新值

			out			保留关键字		► 泛型参数													► 用于指示泛型类型参数是协变的															► 仅能用于泛型接口和委托声明时的类型参数
										► 方法参数													► 方法参数：指示参数按引用而不是按值传递												► 该调用方法在调用方法之前不需要初始化参数
																																															► 该方法内部必须向参数赋值

			ref			保留关键字		► 方法参数													► 指示参数按引用而不是按值传递															► 在调用方法之前必须初始化参数
																																															► 该方法可以将新值赋给参数，但不需要这样做

			ref readonly保留关键字		► 方法参数													► 指示该方法期望自变量是变量，而非不是变量的表达式										► 在调用方法之前必须初始化参数
																																															► 该方法无法向参数赋新值

			params		保留关键字		► 方法参数													► 指示该方法的最后一个参数是一个调用时才传入确定长度的数组、集合						► params 参数的声明类型必须是集合类型
																									► 指示调用时可以传入能转化为定义时的数据类型的参数的逗号分隔列表						► params 参数必须放在参数列表最后一个位置
																																															► 一个方法至多只能存在一个params参数

			this		保留关键字		► 方法参数													► 创建扩展方法时作为第一个参数指示即将被扩展的数据类型	
	
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
         */
        public static void LearnModifierKeywords()
		{
            Console.WriteLine("如需了解修饰符关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
                    "012 方法\n" +
                    "014 扩展方法\n" +
                    "016 方法参数\n" +
                    "018 类\n" +
					"020 继承与多态\n");

            Console.WriteLine("如需了解修饰符关键字，请在 [C# 学习--高级篇] 章节中选择以下章节进行了解：\n" +
                    "003 事件\n");
        }

        /*【10203：语句关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • C#中为特定类型的语句设立的关键字，指示具有特定功能的语句
						关键字类型		指示语句			功能作用

			if			保留关键字		选择语句			► 包含 else 部分的 if 语句根据布尔表达式的值选择两个语句中的一个来执行
			else											► 不包含 else 部分的 if 语句仅在布尔表达式计算结果为 true 时执行其主体

			switch		保留关键字		选择语句			► switch 语句根据与匹配表达式匹配的模式来选择要执行的语句列表
			case												• switch指示匹配表达式
			default												• case指示匹配模式
																• default指示默认匹配模式

			do			保留关键字		迭代语句			► do-while语句在指定的布尔表达式的计算结果为 true 时，do 语句会执行一条语句或一个语句块
			while											► while语句在指定的布尔表达式的计算结果为 true 时，while 语句会执行一条语句或一个语句块

			for			保留关键字		迭代语句			► 在指定的布尔表达式的计算结果为 true 时，for 语句会执行一条语句或一个语句块

			foreach		保留关键字		迭代语句			► 为类型实例中实现 IEnumerable 或 IEnumerable<T> 接口的每个元素执行语句或语句块
			in

			break		保留关键字		跳转语句			► 将终止最接近的封闭迭代语句（即 for、foreach、while 或 do 循环）或 switch 语句

			continue	保留关键字		跳转语句			► 跳过当前迭代并启动最接近的封闭迭代语句（即 for、foreach、while 或 do 循环）的新迭代

			goto		保留关键字		跳转语句			► 将控制权转交给带有标签的语句

			return		保留关键字		跳转语句			► 终止它所在的函数的执行，并将控制权和函数结果（若有）返回给调用方

			try			保留关键字		异常处理语句		► 使用 try-catch 语句处理在执行代码块期间可能发生的异常
			catch											►  try-finally 语句中，当控件离开 try 块时，将执行 finally 块
			finally											► 使用 try-catch-finally 语句来处理在执行 try 块期间可能发生的异常，并指定在控件离开 try 语句时必须执行的代码

			throw		保留关键字		异常处理语句		► 引发异常

			checked		保留关键字		checked语句			► 指定对整型类型算术运算和转换的溢出进行检查

			unchecked	保留关键字		unchecked语句		► 指定对整型类型算术运算和转换的溢出不进行检查

			fixed		保留关键字		fixed语句			► 防止垃圾回收器重新定位可移动变量，并声明指向该变量的指针

			lock		保留关键字		lock语句			► 获取给定对象的互斥 lock，执行语句块，然后释放 lock
															► 确保在任何时候最多只有一个线程执行其主体

			using		保留关键字		using语句			► 确保正确使用 IDisposable 实例
															► 确保即使在 using 语句块内发生异常的情况下也会释放可释放实例

			yield		上下文关键字	yield语句			► 在迭代器中使用 yield 语句提供下一个值或表示迭代结束
																• yield return：在迭代中提供下一个值
																• yield break：显式示迭代结束

		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnStatementKeywords()
		{
			Console.WriteLine("如需了解语句关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
					"011 语句\n");
        }

        /*【10204：命名空间关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 用于声明或引入命名空间的关键字
						关键字类型			主要作用							详情

			namespace	保留关键字			► 声明命名空间						► 用于声明包含一组相关对象的作用域
																				► 用来组织代码元素并创建全局唯一类型
																				► 文件范围的命名空间声明使你能够作出以下声明：一个文件中的所有类型都在一个命名空间中

			using		保留关键字			► 引入命名空间						► 以基本形式从单个命名空间导入所有类型
											► 引入静态类型成员					► 允许使用在命名空间中定义的类型，而无需指定该类型的完全限定命名空间
											► 命名空间别名						► using static 指令命名了一种类型，无需指定类型名称即可访问其静态成员和嵌套类型
																				► 可以使用using指令为引入的命名空间或类型声明别名，并通过别名引用其类型或成员

			extern		保留关键字			► 外部别名							► 在同一应用程序中使用某程序集的两个或多个版本，通过使用外部程序集别名，可在别名命名的根级别命名空间内包装每个程序集的命名空间，使其能够在同一文件中使用。extern alias关键字需要联合使用。
			alias		上下文关键字											► 创建外部程序集别名后，可以通过使用using指令为外程序集的命名空间或类型创建别名

			global		上下文关键字		► 全局引入命名空间					► global using 意味着引用的命名空间或类型将应用于编译中的所有文件
											► 全局命名空间别名					► global单独使用可作为全局命名空间别名使用，使用时其后需要添加::操作符

		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnNamespaceKeywords()
		{
            Console.WriteLine("如需了解命名空间关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
                    "017 命名空间\n");
        }

        /*【10205：类型定义关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字用于定义某一种类型
						关键字类型		定义类型

			class		保留关键字		► 用于定义类

			enum		保留关键字		► 用于定义枚举

			struct		保留关键字		► 用于定义结构

			interface	保留关键字		► 用于定义接口

			record		上下文关键字	► 用于定义记录
										► 可搭配class或struct使用

			delegate	保留关键字		► 用于定义委托
			
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnTypedefKeywords()
		{
            Console.WriteLine("如需了解类型定义关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
                    "009 枚举\n" +
                    "018 类\n" +
                    "021 接口\n" +
                    "022 结构\n" +
                    "023 记录\n");

            Console.WriteLine("如需了解类型定义关键字，请在 [C# 学习--高级篇] 章节中选择以下章节进行了解：\n" +
                    "001 委托\n");
        }

        /*【10206：类型关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字用于表示.NET中的预定义类型，其中包含.NET基本类型在C#中的别名
					关键字类型		数据类型		符号	内存大小						数值范围												.NET类型

			bool	保留关键字		布尔型			-		1字节							► 默认值：false											System.Boolean
																							► 范围：true、false	

			char	保留关键字		字符型			无符号	2字节							► 默认值：'\0'											System.Char 
																							► 范围：U+0000 ~ U+FFFF

			sbyte	保留关键字		整型			有符号	1字节							► 默认值：0												System.SByte
																							► 范围：-128 ~ 127

			short	保留关键字		整型			有符号	2字节							► 默认值：0												System.Int16
																							► 范围： -32768 ~ 32767

			int		保留关键字		整型			有符号	4字节							► 默认值：0												System.Int32
																							► 范围： -2147483648 ~ 2147483647

			long	保留关键字		整型			有符号	8字节							► 默认值：0												System.Int64
																							► 范围： -9223372036854775808 ~ 9223372036854775807

			byte	保留关键字		整型			无符号	1字节							► 默认值：0												System.Byte
																							► 范围： 0 ~ 255

			ushort	保留关键字		整型			无符号	2字节							► 默认值：0												System.UInt16
																							► 范围： 0 ~ 65535

			uint	保留关键字		整型			无符号	4字节							► 默认值：0												System.UInt32
																							► 范围： 0 ~ 4294967295

			ulong	保留关键字		整型			无符号	8字节							► 默认值：0												System.UInt64
																							► 范围： 0 ~ 18446744073709551615

			nint	上下文关键字	整型			有符号	4字节/8字节 [由CPU架构决定]		► 默认值：0												System.IntPtr
																							► 范围： 32位平台同int，64位平台同long

			nuint	上下文关键字	整型			无符号	4字节/8字节 [由CPU架构决定]		► 默认值：0												System.UIntPtr
																							► 范围： 32位平台同uint，64位平台同ulong

			float	保留关键字		浮点型			有符号	4字节							► 默认值：0												System.Single
																							► 精度：大约 6-9 位有效数字
																							► 范围：±1.5 x 10^-45 ~ ±3.4 x 10^38

			double	保留关键字		浮点型			有符号	8字节							► 默认值：0												System.Double
																							► 精度：大约 15-17 位有效数字
																							► 范围： ±5.0 × 10^−324 ~ ±1.7 × 10^308

			decimal	保留关键字		浮点型			有符号	16字节							► 默认值：0												System.Decimal
																							► 精度：大约 28-29 位有效数字
																							► 范围： ±1.0 x 10^-28 ~ ±7.9228 x 10^28

			string	保留关键字		字符串			-										► 默认值：null											System.String

			object	保留关键字		object类型		-										► 默认值：null											System.Object

			dynamic	上下文关键字	动态类型		-		-	

			var		上下文关键字	声明局部变量时，可以让编译器从初始化表达式推断出变量的类型

			void	保留关键字		仅作为无返回值的方法的返回类型				
	
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
		 */
        public static void LearnTypeKeywords()
		{
            Console.WriteLine("如需了解类型关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
                    "004 数据类型基础\n" +
                    "005 .NET内置简单类型\n");
        }

        /*【10207：运算符关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字作为运算符使用
						关键字类型		功能作用																	备注

			await		上下文关键字	暂停对其所属的 async 方法的求值，直到其操作数表示的异步操作完成	

			default		保留关键字		生成类型的默认值	

			delegate	保留关键字		创建一个可以转换为委托类型的匿名方法	

			is			保留关键字		检查表达式结果的运行时类型是否与给定类型兼容	
										将表达式与模式相匹配

			as			保留关键字		将表达式结果显式转换为给定的引用或可以为 null 值的类型						如果无法进行转换，则 as 运算符返回 null

			not			上下文关键字	模式在否定模式与表达式不匹配时与表达式匹配	

			and			上下文关键字	模式在两个模式都与表达式匹配时与表达式匹配	

			or			上下文关键字	模式在任一模式与表达式匹配时与表达式匹配	

			true		保留关键字		返回 bool 值 true 来指示其操作数一定为 true	

			false		保留关键字		返回 bool 值 true 来指示其操作数一定为 false	

			typeof		保留关键字		用于获取某个类型的 System.Type 实例											typeof 运算符的实参必须是类型或类型形参的名称

			nameof		上下文关键字	生成变量、类型或成员的名称作为字符串常量									nameof 表达式在编译时进行求值，在运行时无效

			sizeof		保留关键字		返回给定类型的变量所占用的字节数											sizeof 运算符的参数必须是一个非托管类型的名称，或是一个限定为非托管类型的类型参数

			switch		保留关键字		根据与输入表达式匹配的模式，对候选表达式列表中的单个表达式进行求值	

			stackalloc	保留关键字		在堆栈上分配内存块

			with		上下文关键字	使用修改的特定属性和字段生成其操作数的副本

			new			保留关键字		创建类型的新实例

			operator	保留关键字		在用户定义的类型中用于重载预定义的 C# 运算符								必须同时包含 public 和 static 修饰符

			implicit	保留关键字		在用户定义类型中定义从或到另一个类型的自定义隐式转换						必须同时包含 public 和 static 修饰符，且必须同时使用 operator 操作符

			explicit	保留关键字		在用户定义类型中定义从或到另一个类型的自定义显式转换						必须同时包含 public 和 static 修饰符，且必须同时使用 operator 操作符
	
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
		 */
        public static void LearnOperatorKeywords()
		{
            Console.WriteLine("如需了解运算符关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
                    "010 运算符\n");

            Console.WriteLine("如需了解运算符关键字，请在 [C# 学习--高级篇] 章节中选择以下章节进行了解：\n" +
                    "002 匿名函数\n");
        }

        /*【10208：泛型类型约束关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字用作泛型类型参数的约束
						关键字类型		功能作用																备注

			where		上下文关键字	指定对用作泛型类型、方法、委托或本地函数中类型参数的参数类型的约束	

			new			保留关键字		指定泛型类或方法声明中的类型实参必须有公共的无参数构造函数				► 若要使用 new 约束，则该类型不能为抽象类型
																												► 当与其他约束一起使用时，new() 约束必须最后指定

			notnull		上下文关键字	指定类型参数必须是不可为 null 的值类型或不可为 null 的引用类型	

			unmanaged	上下文关键字	指定类型参数必须是不可为 null 的非托管类型	

		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnGenericTypeConstraintKeywords()
		{
            Console.WriteLine("如需了解泛型类型约束关键字，请在 [C# 学习--高级篇] 章节中选择以下章节进行了解：\n" +
                    "006 泛型\n");
        }

        /*【10209：访问关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字用于类型访问本身或其基类及其成员
					关键字类型		功能作用									备注

			base	保留关键字		用于从派生类中访问其直接继承的基类的成员	► 调用基类上已被其他方法重写的方法
																				► 指定创建派生类实例时应调用的基类构造函数

			this	保留关键字		指代类的当前实例	
	
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnAccessKeywords()
		{
            Console.WriteLine("如需了解访问关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
                    "020 继承与多态\n");
        }

        /*【10210：文字关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字通常用于表示一个文字值
					关键字类型		功能作用												备注	

			null	保留关键字		► 表示不引用任何对象的空引用的文字值					► null 是引用类型变量的默认值
																							► 普通值类型不能为 NULL，可为空的值类型除外

			default	保留关键字		► 指定 switch 语句中的默认事例。	
									► 作为 default 默认运算符或文本生成类型的默认值。
									► 作为泛型方法重写或显式接口实现上的 default 类型约束

			true	保留关键字		► 表示一个布尔值true	

			false	保留关键字		► 表示一个布尔值false	

		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnLiteralKeywords()
		{
            Console.WriteLine("\n------示例：文字关键字------\n");

            //使用null关键字为string类型变量设置null值
            string nullStr = null;
			Console.WriteLine($"字符串变量 {nameof(nullStr)} 的值是否为null：{nullStr is null}");
			Console.WriteLine();

			//以下两种方法均可以使用default关键字获取int类型变量的默认值
			int defaultInt = default;
			//int defaultInt = default(int);
			Console.WriteLine($"int类型变量 {nameof(defaultInt)} 的默认值为：{defaultInt}");
			Console.WriteLine();

			//使用true或false关键字为bool类型变量赋值
			bool trueResult = true;
			bool falseResult = false;
            Console.WriteLine($"bool类型变量 {nameof(trueResult)} 的值为：{trueResult}");
			Console.WriteLine($"bool类型变量 {nameof(falseResult)} 的值为：{falseResult}");
            Console.WriteLine();
        }

        /*【10211：上下文关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这些上下文关键字仅在特定情况下属于关键字，在其他地方则可作为普通标识符使用
					关键字类型		功能作用																										备注

			add		上下文关键字	► 用于定义自定义事件访问器，当客户端代码订阅事件时将调用该访问器												如果提供自定义 add 访问器，还必须提供 remove 访问器

			remove	上下文关键字	► 用于定义自定义事件访问器，当客户端代码取消订阅事件时将调用该访问器											如果提供自定义 remove 访问器，还必须提供 add 访问器

			get		上下文关键字	► 在属性或索引器中定义访问器方法，它将返回属性值或索引器元素	

			set		上下文关键字	► 在属性或索引器中定义访问器，它会向属性或索引器元素分配值	

			init	上下文关键字	► 在属性或索引器中定义访问器，它会向属性或索引器元素分配值，且一旦初始化对象，将无法更改属性或索引器值	

			value	上下文关键字	► 隐式参数 value 用于属性和索引器的set访问器，它是 set 访问器的输入参数	

			field	上下文关键字	► 用于自动实现的属性访问器，以访问编译器生成的属性后盾字段	

			when	上下文关键字	► 在 try-catch 或 try-catch-finally 语句的 catch 子句中指定筛选条件	
									► 作为 switch 语句中的 case guard
									► 作为 switch 表达式中的 case guard

			partial	上下文关键字	► 用于定义要拆分到多个定义中的类、结构、接口或记录	
									► 用于在分部类型中将有需要的属性、索引器或方法的声明与实现在不同分部类中进行定义

			args	上下文关键字	► 顶级语句可以引用 args 变量来访问输入的任何命令行参数															args 变量永远不会为 null，但如果未提供任何命令行参数，则其 Length 将为零

		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnContextualKeywords()
		{
            Console.WriteLine("如需了解上下文关键字，请在 [C# 学习--基础篇] 章节中选择以下章节进行了解：\n" +
                    "020 类\n");

            Console.WriteLine("如需了解上下文关键字，请在 [C# 学习--高级篇] 章节中选择以下章节进行了解：\n" +
                    "003 事件\n");
        }

        /*【10212：查询关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字仅用于LINQ查询语句中
						关键字类型		功能作用																					备注

			from		上下文关键字	► 指定表示源序列中每个元素的本地范围变量													查询表达式必须以 from 子句开头

			where		上下文关键字	► 基于由逻辑 AND 和 OR 运算符（&& 或 ||）分隔的一个或多个布尔表达式筛选源元素	

			select		上下文关键字	► 指定在执行查询时产生的值的类型															查询表达式必须以 select 子句或 group 子句结尾

			group		上下文关键字	► 返回一个 IGrouping<TKey,TElement> 对象序列，这些对象包含零个或更多与该组的键值匹配的项	查询表达式必须以 select 子句或 group 子句结尾

			into		上下文关键字	► 创建临时标识符，将 group、join 或 select 子句的结果存储至新标识符	

			orderby		上下文关键字	► 根据元素类型的默认比较器对查询结果进行升序或降序排序	

			join		上下文关键字	► 基于两个指定匹配条件间的相等比较而联接两个数据源	

			let			上下文关键字	► 引入范围变量，在查询表达式中存储子表达式结果	

			in			上下文关键字	► 用于在查询表达式的 from 子句中指定将在其上运行查询或子查询的数据源	
										► 用于在查询表达式的 join 子句指定用于联接的数据源

			on			上下文关键字	► 用于在查询表达式的 join 子句中指定联接条件	

			equals		上下文关键字	► 用于在查询表达式的 join 子句中比较两个序列的元素	

			by			上下文关键字	► 用于在查询表达式的 group 子句中指定应返回项的分组方式	

			ascending	上下文关键字	► 在查询表达式中的 orderby 子句中使用 ascending 上下文关键字指定排序顺序为从小到大	

			descending	上下文关键字	► 在查询表达式中的 orderby 子句中使用 descending 上下文关键字指定排序顺序为从大到小	
	
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
		 */
        public static void LearnQueryKeywords()
		{
            Console.WriteLine("如需了解查询关键字，请在 [C# 学习--高级篇] 章节中选择以下章节进行了解：\n" +
                    "007 Linq\n");
        }

        /*【10213：函数指针调用约定关键字】
		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	

		 • 这类关键字用于指定函数指针调用约定
							关键字类型		功能作用														备注

			delegate		保留关键字		定义安全函数指针对象，可以使用 delegate* 语法定义函数指针

			managed			上下文关键字	为 delegate* 指定调用约定										默认调用约定

			unmanaged		上下文关键字	为 delegate* 指定调用约定，每个声明都可不指定或选择性指定以下某个 ECMA 335 调用约定：Cdecl、Stdcall、Fastcall 或 Thiscall

		 * ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
		 */
        public static void LearnFunctionPointerCallingConventionKeywords()
		{

		}

        public static void StartLearnKeywords()
        {
            Console.WriteLine("本篇章意在介绍C#关键字的基本信息，请直接查看各小节示例或指引的源代码以作理解！\n");
            string title = "001 访问级别关键字\n" +
                "002 修饰符关键字\n" +
                "003 语句关键字\n" +
                "004 命名空间关键字\n" +
                "005 类型定义关键字\n" +
                "006 类型关键字\n" +
                "007 运算符关键字\n" +
                "008 泛型类型约束关键字\n" +
                "009 访问关键字\n" +
                "010 文字关键字\n" +
                "011 上下文关键字\n" +
                "012 查询关键字\n" +
                "013 函数指针调用约定关键字\n";

            do
            {
                Console.WriteLine("【学习关键字】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnAccessibilityLevelKeywords(); break;
                    case "002": LearnModifierKeywords(); break;
                    case "003": LearnStatementKeywords(); break;
                    case "004": LearnNamespaceKeywords(); break;
                    case "005": LearnTypedefKeywords(); break;
                    case "006": LearnTypeKeywords(); break;
                    case "007": LearnOperatorKeywords(); break;
                    case "008": LearnGenericTypeConstraintKeywords(); break;
                    case "009": LearnAccessKeywords(); break;
                    case "010": LearnLiteralKeywords(); break;
                    case "011": LearnContextualKeywords(); break;
                    case "012": LearnQueryKeywords(); break;
                    case "013": LearnFunctionPointerCallingConventionKeywords(); break;
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

//该命名空间及其内部内容仅供学习关键字使用，不具有实际意义
//【标识符】：标识符是程序员为类、方法、变量等选择的名字
namespace LearnCSharp.Basic.LearnKeyWordsSpace
{
    file class AccessibilityLevelChildInOuterAssembly : Help::AccessibilityLevel
    {
        public override void PrintAccessibilityLevel()
        {
            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{this.GetType()}");
            Console.WriteLine($"基类：{this.GetType().BaseType}");
            Console.WriteLine($"------通过当前类型在程序集外部的派生类访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法在外部程序集派生类中访问基类中的private成员】");
            Console.WriteLine($"【属性名：{nameof(ProtectedInfo),-22} | 属性访问级别：{"protected",-18} | 属性值：{ProtectedInfo}】");
            Console.WriteLine($"【属性名：{"InternalInfo",-22} | 属性访问级别：{"internal",-18} | 错误：无法在外部程序集派生类中访问基类中的internal成员】");
            Console.WriteLine($"【属性名：{nameof(PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{"PrivateProtectedInfo",-22} | 属性访问级别：{"private protected",-18} | 错误：无法在外部程序集派生类中访问基类中的private protected成员】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();

			Help::AccessibilityLevel levelInChildInOuterAssembly = new();
            //levelInChildInOuterAssembly.PrivateInfo = $"levelInChildInOuterAssembly--{this.PrivateInfo}";
            //levelInChildInOuterAssembly.ProtectedInfo = $"levelInChildInOuterAssembly--{this.ProtectedInfo}";
            //levelInChildInOuterAssembly.InternalInfo = $"levelInChildInOuterAssembly--{this.InternalInfo}";
            levelInChildInOuterAssembly.PublicInfo = $"levelInChildInOuterAssembly--{this.PublicInfo}";
            //levelInChildInOuterAssembly.ProtectedInternalInfo = $"levelInChildInOuterAssembly--{this.ProtectedInternalInfo}";
            //levelInChildInOuterAssembly.PrivateProtectedInfo = $"levelInChildInOuterAssembly--{this.PrivateProtectedInfo}";

            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{levelInChildInOuterAssembly.GetType()}");
            Console.WriteLine($"------通过当前类型在程序集外部的派生类内部声明本类型的实例来访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法通过外部程序集派生类中的基类实例访问private成员】");
            Console.WriteLine($"【属性名：{"ProtectedInfo",-22} | 属性访问级别：{"protected",-18} | 错误：无法通过外部程序集派生类中的基类实例访问protected成员】");
            Console.WriteLine($"【属性名：{"InternalInfo",-22} | 属性访问级别：{"internal",-18} | 错误：无法通过外部程序集派生类中的基类实例访问internal成员】");
            Console.WriteLine($"【属性名：{"PublicInfo",-22} | 属性访问级别：{"public",-18} | 属性值：{levelInChildInOuterAssembly.PublicInfo}】");
            Console.WriteLine($"【属性名：{"ProtectedInternalInfo",-22} | 属性访问级别：{"protected internal",-18} | 错误：无法通过外部程序集派生类中的基类实例访问protected internal成员】");
            Console.WriteLine($"【属性名：{"PrivateProtectedInfo",-22} | 属性访问级别：{"private protected",-18} | 错误：无法通过外部程序集派生类中的基类实例访问private protected成员");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();

            AccessibilityLevelChildInOuterAssembly childInnerInOuterAssembly = new AccessibilityLevelChildInOuterAssembly();
            //childInnerInOuterAssembly.PrivateInfo = $"childInnerInOuterAssembly--{this.PrivateInfo}";
            childInnerInOuterAssembly.ProtectedInfo = $"childInnerInOuterAssembly--{this.ProtectedInfo}";
            //childInnerInOuterAssembly.InternalInfo = $"childInnerInOuterAssembly--{this.InternalInfo}";
            childInnerInOuterAssembly.PublicInfo = $"childInnerInOuterAssembly--{this.PublicInfo}";
            childInnerInOuterAssembly.ProtectedInternalInfo = $"childInnerInOuterAssembly--{this.ProtectedInternalInfo}";
            //childInnerInOuterAssembly.PrivateProtectedInfo = $"childInnerInOuterAssembly--{this.PrivateProtectedInfo}";

            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{childInnerInOuterAssembly.GetType()}");
            Console.WriteLine($"基类：{childInnerInOuterAssembly.GetType().BaseType}");
            Console.WriteLine($"------通过当前类型在程序集外部的派生类内部声明派生类的实例访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法通过外部程序集派生类中的派生类实例访问private成员】");
            Console.WriteLine($"【属性名：{nameof(childInnerInOuterAssembly.ProtectedInfo),-22} | 属性访问级别：{"protected",-18} | 属性值：{childInnerInOuterAssembly.ProtectedInfo}】");
            Console.WriteLine($"【属性名：{"InternalInfo",-22} | 属性访问级别：{"internal",-18} | 错误：无法通过外部程序集派生类中的派生类实例访问internal成员】");
            Console.WriteLine($"【属性名：{nameof(childInnerInOuterAssembly.PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{childInnerInOuterAssembly.PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(childInnerInOuterAssembly.ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{childInnerInOuterAssembly.ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{"PrivateProtectedInfo",-22} | 属性访问级别：{"private protected",-18} | 错误：无法通过外部程序集派生类中的派生类实例访问private protected成员】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();
        }
    }
}
