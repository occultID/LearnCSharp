/*【学习委托】
 * 委托
	• 委托是一种引用类型，表示对具有特定参数列表和返回类型的方法的引用
		○ 在实例化委托时，可以将其实例与任何具有兼容签名和返回类型的方法相关联
		○ 委托实例封装一个调用列表，它是一个或多个方法的列表，其中每个方法都称为可调用实体
			§ 对于实例方法，可调用实体包含实例和实例方法
			§ 对于静态方法，可调用实体仅包含方法
		○ 可以通过委托实例调用其封装的方法
			§ 使用适当的参数集调用委托实例会导致使用给定的参数集调用每个委托的可调用实体
	• 委托声明定义派生自类System.Delegate
		○ System.Delegate 类及其单个直接子类 System.MulticastDelegate 提供框架支持，以便创建委托、将方法注册为委托目标以及调用注册为委托目标的所有方法
		○ System.Delegate 和 System.MulticastDelegate 类本身不是委托类型
			§ 它们为所有特定委托类型提供基础
			§ 相同的语言设计过程要求不能声明派生自 Delegate 或 MulticastDelegate 的类
		○ C# 编译器会在使用 delegate 关键字声明委托类型时，创建派生自 MulticastDelegate 的类的实例
			§ 用户定义的委托不能显示的派生自 Delegate 或 MulticastDelegate 的类
	• 委托可以实现其他语言（如C++、Pascal和Modula）使用函数指针进行寻址的方案
		○ 委托是完全面向对象的，委托封装对象实例和方法
	• 委托用于将方法作为参数传递给其他方法
		○ 事件处理程序就是通过委托调用的方法
		○ 可以将任何可访问类或结构中与委托类型匹配的任何方法分配给委托
		○ 将方法作为参数进行引用的能力使委托成为定义回调方法的理想选择
	• 委托实例不关心也不必知道它所封装的方法属于哪个类，它只关心这些方法是否与委托的类型声明兼容
		○ 这使委托非常适合用于“匿名”调用

 * 委托的声明与定义
	• 委托使用delegate关键字进行声明和定义
	• 声明形式
		○ [特性]
		访问级别 有效的委托修饰符 delegate 返回类型 委托名称<泛型参数列表>(参数列表) 泛型参数约束
			§ [特性]：该项为可选配置，标识委托是否被附加某种特性
			§ 访问级别：该项为可选配置，标识委托的访问级别
				□ public：标识公开访问级别
				□ protected：标识受保护访问级别
				□ internal：标识程序域内部访问级别
				□ private：标识私有访问级别
			§ 有效的委托修饰符：该项为可选配置，标识委托的功能修饰
				□ new：只能用于在类、结构或记录内部声明的委托，用于覆盖已有成员
				□ unsafe：标识委托为非托管类型或用于封装非托管类型
			§ delegate：该项为必选配置，这是声明委托的必须关键字
			§ 返回类型：该项为必选配置，标识封装方法的返回类型
				□ void：如果方法无返回值，则标识为void类型
			§ 委托名称：该项为必选配置，标识委托的名称
				□ 委托名称通常采用Pascal命名法，即所有单词首字母大写
			§ <泛型参数列表>：该项为可选配置，标识委托是否为泛型实现
				□ 泛型参数形参可有多个，每个参数使用“,”隔开
			§ (参数列表)：该项为必选配置，标识封装方法的参数列表
				□ 如果方法是一个无参方法，则参数列表为空，但“()”不能省略
				□ 参数可以有多个且参数类型同方法参数一致
			§ 泛型参数约束：该项为可选配置，标识对泛型参数的约束
				□ 该项使用的前提是委托使用了泛型参数
				□ 即使使用了泛型参数，该项也是可选的
	• C#中结构等效但名称不同的两个委托是不同的委托类型

 * 委托的实例化与调用
	• 实例化的形式
		○ 其一：委托类型 委托实例名 = new 委托类型(匹配该委托的方法的名称);
		○ 其二：委托类型 委托实例名 = 匹配该委托的方法的名称;
	• 一个委托实例封装多个方法的形式
		○ 当某个委托实例已经声明，可以通过“+=”或“-=”来增加或删减封装的兼容方法
			§ 增加：委托实例名 += 匹配该委托的方法的名称或同一委托类型的其他实例;
			§ 删减：委托实例名 -= 已经添加到委托封装方法列表内的方法名称或同一委托类型的其他实例;
	• 委托的调用
		○ 直接使用委托实例调用
			§ 委托实例名(实参列表)
		○ 使用委托实例的Invoke方法调用
			§ 委托实例名.Invoke(实参列表)
		○ 注意：委托如果封装了多个方法，调用委托时，会依次调用方法列表内的所有方法

 * 委托的使用
	• 系统内置委托类型：.NET内置了三个预定义的委托类型可以覆盖大多数使用场景
		○ Action委托
			§ Action委托类型实例用于封装无返回值的方法，内置提供了17个变体
				□ 即方法参数列表的参数从0到16个
				□ 方法参数列表的预定义是泛型实现，每个参数的类型实际可以完全自定义
			§ Action委托的实例化形式（以无参和一个参数来示例）
				□ Action 实例名 = 兼容该委托的方法的名称/兼容该委托的匿名方法
				□ Action<实参数据类型> 实例名 = 兼容该委托的方法的名称/兼容该委托的匿名方法
		○ Func委托
			§ Func委托类型实例用于封装有返回值的方法，内置提供了17个变体
				□ 即方法参数列表的参数从0到16个
				□ 方法参数列表的预定义是泛型实现，每个参数的类型实际可以完全自定义
				□ 泛型参数列表的最后一个永远是该委托封装方法的返回类型
			§ Func委托的实例化形式（以无参和一个参数来示例）
				□ Func<实际返回值数据类型> 实例名 = 兼容该委托的方法的名称/兼容该委托的匿名方法
				□ Func<实参数据类型, 实际返回值数据类型> 实例名 = 兼容该委托的方法的名称/兼容该委托的匿名方法
		○ Predicate委托
			§ Predicate委托类型实例用于封装具有一个方法参数且最终返回一个布尔值的方法
				□ 通常封装的方法具有使用单个方法参数进行判断得出一个布尔值结果的作用
				□ 方法参数的预定义是泛型实现，参数的实际类型可以完全自定义
			§ Predicate委托的实例化形式
				□ Predicate<实参数据类型> 实例名 = 兼容该委托的方法的名称/兼容该委托的匿名方法
	• 自定义委托
		○ 自定义委托的实例化与使用与内置委托类型无异
		○ 当且仅当内置委托类型在描述或功能上无法满足实际需求时，才需要进行自定义委托
 */

using System.Numerics;
using System.Runtime.CompilerServices;
using LearnCSharp.Professional.LearnDelegateSpace;

namespace LearnCSharp.Professional.LearnDelegateSpace
{
    //自定义一个泛型委托
    //该委托用于封装支持计算两个同数据类型操作数并返回一个同数据类型结果的方法
    public delegate T CalcDelegate<T>(T x, T y);

	//自定义一个泛型类
	//该类封装了几个兼容上述自定义委托的方法
    public class Calculators<T> where T : INumber<T>
    {
        public T Add(T x, T y)
		{
			var result = x + y;
			Console.WriteLine($"Calculators->Add(x, y) --input: x：{x} y：{y} --output: {result}");
			return result;
		}

        public T Sub(T x, T y)
		{
            var result = x - y;
            Console.WriteLine($"Calculators->Sub(x, y) --input: x：{x} y：{y} --output: {result}");
            return result;
        }

        public T Mul(T x, T y)
		{
            var result = x * y;
            Console.WriteLine($"Calculators->Mul(x, y) --input: x：{x} y：{y} --output: {result}");
            return result;
        }

        public T Div(T x, T y)
		{
            var result = x / y;
            Console.WriteLine($"Calculators->Div(x, y) --input: x：{x} y：{y} --output: {result}");
            return result;
        }
    }
}

namespace LearnCSharp.Professional
{
    internal class LearnDelegate
    {
		/*【20101：系统内置委托示例】
		 */
		public static void LearnSystemDelegate()
		{
            //声明一个Calculators实例
            Calculators<double> calculators = new Calculators<double>();

			//使用内置Func委托封装兼容方法
			Func<double, double, double> funcAdd = calculators.Add;
			Func<double, double, double> funcSub = calculators.Sub;
            Func<double, double, double> funcMul = calculators.Mul;
			Func<double, double, double> funcDiv = calculators.Div;

            double x, y;
            Console.WriteLine("请依次输入两个数字");

			first: Console.Write("请输入第一个数字：");
            if (!double.TryParse(Console.ReadLine(), out x))
                goto first;

            second: Console.Write("请输入第二个数字：");
            if (!double.TryParse(Console.ReadLine(), out y))
                goto second;

            //声明一个局部方法用于将委托作为参数传入进行执行
            void CalculateResult(Func<double, double, double> func, [CallerArgumentExpression("func")] string? message = null)
            {
                Console.WriteLine($"通过Func委托实例 {message} 调用其封装方法，{message}.Invoke({x}, {y})输出结果：");
                Console.Write("	");

                //执行委托调用封装的方法
                func.Invoke(x, y);

                Console.WriteLine();
            }

            Console.WriteLine("调用委托来执行相应方法：");

            CalculateResult(funcAdd);
            CalculateResult(funcSub);
            CalculateResult(funcMul);
            CalculateResult(funcDiv);
        }

		/*【20102：自定义委托示例】
		 */
		public static void LearnCustomerDelegate()
		{
            //声明一个Calculators实例
            Calculators<double> calculators = new Calculators<double>();

			//使用自定义委托封装兼容方法
			CalcDelegate<double> add = calculators.Add;
			CalcDelegate<double> sub = calculators.Sub;
			CalcDelegate<double> mul = calculators.Mul;
			CalcDelegate<double> div = calculators.Div;

            double x, y;
            Console.WriteLine("请依次输入两个数字");

			first: Console.Write("请输入第一个数字：");
            if (!double.TryParse(Console.ReadLine(), out x))
                goto first;

            second: Console.Write("请输入第二个数字：");
            if (!double.TryParse(Console.ReadLine(), out y))
                goto second;

            //声明一个局部方法用于将委托作为参数传入进行执行
            void CalculateResult(CalcDelegate<double> calculator, [CallerArgumentExpression("calculator")] string? message = null)
			{
				Console.WriteLine($"通过委托实例 {message} 调用其封装方法，{message}.Invoke({x}, {y})输出结果：");
				Console.Write("	");

				//执行委托调用封装的方法
				calculator.Invoke(x, y);
				
				Console.WriteLine();
			}

			Console.WriteLine("调用委托来执行相应方法：");

			CalculateResult(add);
			CalculateResult(sub);
			CalculateResult(mul);
			CalculateResult(div);
		}

		/*【20103：多播委托示例】
		 */
		public static void LearnMultiDelegate()
		{
            //声明一个Calculators实例
            Calculators<double> calculators = new Calculators<double>();

            //使用自定义委托封装多个兼容方法
            CalcDelegate<double> cal = calculators.Add;
            cal += calculators.Sub;
            cal += calculators.Mul;
            cal += calculators.Div;

            //声明一个局部方法用于将委托作为参数传入进行执行
            void CalculateResult(CalcDelegate<double> calculator, [CallerArgumentExpression("calculator")] string? message = null)
            {
                double x, y;
                Console.WriteLine("请依次输入两个数字");

				first: Console.Write("请输入第一个数字：");
                if (!double.TryParse(Console.ReadLine(), out x))
                    goto first;

                second: Console.Write("请输入第二个数字：");
                if (!double.TryParse(Console.ReadLine(), out y))
                    goto second;

                Console.WriteLine($"通过委托实例 {message} 调用其封装方法列表，{message}.Invoke({x}, {y})输出结果：");
				Console.WriteLine();

                //执行委托调用封装的方法
                calculator.Invoke(x, y);

                Console.WriteLine();
            }

            Console.WriteLine("调用委托来执行封装的方法列表：");

            CalculateResult(cal);

			//从多播委托中移除某一委托
			cal -= calculators.Mul;

            Console.WriteLine("调用委托来执行封装的方法列表：");
            CalculateResult(cal);
        }

        public static void StartLearnDelegate()
        {
            string title = "001 内置委托（以Func委托为例）\n" +
                "002 自定义委托\n" +
				"003 多播委托";

            do
            {
                Console.WriteLine("【学习委托】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnSystemDelegate(); break;
                    case "002": LearnCustomerDelegate(); break;
					case "003": LearnMultiDelegate(); break;
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
