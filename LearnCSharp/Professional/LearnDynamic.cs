/*【学习dynamic类型】
 * dynamic类型
	• dynamic本身是一种静态类型，但它提供了使C#语言拥有动态语言特性的作用
	• dynamic类型标识变量的使用和对其成员的引用绕过编译时类型检查
		○ 这些操作由运行时解析
	• 在编译时，dynamic类型的元素假定为支持任何操作
		○ 因此不必关心该对象从什么地方获取其值，代码无效的情况下运行时会捕获错误
		○ 大多数对dynamic元素的动态操作的返回结果在编译时也是dynamic类型
			§ 结果不为dynamic的操作包括
				□ 对dynamic类型元素执行了类型转换
				□ dynamic类型元素作为参数的构造函数调用
	• dynamic类型转换
		○ dynamic对象和其它类型转换非常简单
			§ 任何对象都可以隐式转换为dynamic类型
			§ dynamic类型可以隐式转换为任何类型
				□ 运行时转换无效的情况会捕获错误
	• dynamic类型元素作为参数的方法重载决策
		○ 以下情况的方法调用会在运行时来进行重载决策
			§ 调用方提供的实参是dynamic类型
			§ 方法的形参具有dynamic类型
	• dynamic类型和object类型
		○ 大多数情况下，dynamic类型的功能和object类型一样，且它们都是不具备描述性的类型
		○ 当COM互操作、反射操作或与其他动态语言交互时，如果可能存在大量强制转换则建议使用dynamic类型接收数据
	• dynamic类型和var类型
		○ dynamic是通知编译器生成代码的指令，其对象会跳过编译时类型检查，而在运行时确定
		○ var类型是告诉编译器根据赋值运算符右侧的操作数进行类型推导，类型一旦确认则该变量就是该类型
		○ dynamic类型变量在编译时可以为其不断赋予不同类型的值
		○ var类型变量一旦确认类型则只能赋予该类型的值
	• dynamic类型的运行时表示
		○ dynamic类型和object类型有深度的等价关系
			§ 在运行时，对dynamic和object类型本身使用typeof运算符得到的结果进行相等性比较结果为true
				□ typeof运算符对dynamic和object类型的数组或构造类型的运算结果进行相等性比较结果也为true
			§ dynamic类型和object类型的引用都可以指向任意非指针类型的对象
			§ 在运行时，对dynamic对象和object对象调用GetType方法得到的结果都会是引用对象的实际类型
			§ 对dynamic类型的成员使用反射，可以得到结果是该成员类型是object且附加了特性System.Runtime.CompilerServices.DynamicAttribute
	• 动态表达式
		○ 类型或值的类型为dynamic类型的表达式
		○ 字段、属性、方法、事件、构造器、索引器、运算符和转换等等都可以动态调用
			§ 具体的绑定操作会在运行时才执行
			§ 对于动态调用，以下情况不能使用动态类型变量作为接收者否则会在运行时抛出异常
				□ 方法形参或传入实参为dynamic类型但返回值为void类型
			§ 对于动态调用，以下情况可以使用静态类型作为接收者，但会将该动态表达式转换为静态表达式
				□ dynamic类型隐式转换为某个具体静态类型，但运行时代码无效会抛出异常
				□ 方法形参或传入实参为dynamic类型但返回值为某个具体静态类型
					® 当方法为构造函数时，且接收者为静态类型时，则方法调用始终产生静态表达式
			§ 下列类型的方法不支持动态调用
				□ 扩展方法
				□ 必须将类型转换为接口才能调用的接口成员
				□ 基类中被子类隐藏的成员

 * 动态绑定
	• 动态绑定（dynamic binding）将绑定（即解析类型、成员和操作的过程）从编译时延迟到运行时
		○ 动态绑定适用于开发者知道某个特定的函数、成员或操作的存在而编译器不知道的情况
		○ 动态绑定可能需要使用动态类型作为接收者，动态类型变量使用dynamic关键字声明
	• 静态绑定与动态绑定
		○ 绑定：解析类型、成员和操作的过程通常称为“绑定”
			§ 静态绑定--符合以下条件的绑定
				□ 解析类型、成员和操作的过程在编译时已明确确定
				□ 如果绑定包含错误，编译器将检测并报告错误
			§ 动态绑定--符合以下条件的绑定
				□ 解析类型、成员和操作的过程在运行时才确定
				□ 表达式的类型是动态类型(dynamic)，所有的绑定都是基于运行时中对象的实际类型而非编译时指定的类型
				□ 采取动态绑定操作时，编译器不会执行任何检查
				□ 如果运行时绑定失败，则会在运行时抛出异常
		○ C#中的以下操作服从绑定
			§ 成员访问
			§ 方法调用
			§ 委托调用
			§ 元素访问
			§ 对象创建
			§ 重载的一元运算符
			§ 重载的二元运算符
			§ 赋值运算符
			§ 隐式和显示转换
		○ 绑定时间
			§ 静态绑定在编译时进行
			§ 动态绑定在运行时进行
	• 自定义绑定
		○ 自定义绑定是通过动态对象实现IDynamicMetaObjectProvider（IDMOP）接口来实现的
		○ 通常情况下大多数时间会从基于DLR实现的动态语言中获得IDMOP对象而很少需要自己来写一个实现了IDMOP的类型
		○ 关于自定义绑定，更多知识笔记参考 动态编程 部分
	• 语言绑定
		○ 语言绑定是在动态对象未实现IDMOP时发生的
		○ 语言绑定在处理设计不当的类型或绕过.NET本身类型系统的限制时是非常有用的
		○ 关于语言绑定，更多知识笔记参考 动态编程 部分
	• 动态绑定失败
		○ 如果成员在运行时绑定失败，则程序会抛出RuntimeBinderException异常
 */
using Microsoft.CSharp.RuntimeBinder;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LearnCSharp.Professional
{
    internal class LearnDynamic
    {
		public static void LearnDynamicVariable()
		{
			Console.WriteLine("使用以下代码声明一个dynamic类型变量dyn，并赋予一个int类型值5，然后对dyn进行一次加法操作加5：\n" +
                "dynamic dyn = 5;\ndyn += 5;");

			dynamic dyn = 5;
			dyn += 5;

			Console.WriteLine($"动态类型变量dyn在运行时类型：{dyn.GetType().Name},值为：{dyn}\n");

            Console.WriteLine("使用以下代码为dynamic类型变量dyn重新赋予一个double类型值10.0\n" +
                "dyn = 10.0d;");

			dyn = 10.0d;

            Console.WriteLine($"动态类型变量dyn在运行时类型：{dyn.GetType().Name},值为：{dyn}\n");

            Console.WriteLine("使用以下代码声明一个dynamic类型变量dynStr，并赋予一个string类型值\"你好\"\n" +
                "dynamic dynStr = \"你好\";");

			dynamic dynStr = "你好";

            Console.WriteLine($"动态类型变量dynStr在运行时类型：{dynStr.GetType().Name},值为：{dynStr}\n");

            Console.WriteLine("使用以下代码声明一个string类型变量str，并使用动态表达式 dynamic result = dynStr + dyn 求值并将值赋予它\n" +
                "dynamic result = dynStr + dyn;\nstring str = result;");

			dynamic result = dynStr + dyn;
            string str = result;

            Console.WriteLine($"动态类型变量result隐式转换为string类型值,运行时代码有效并成功转换，值为：{str}\n");

            Console.WriteLine("使用以下代码声明一个int类型变量integer，并将动态类型变量dynStr隐式转换为int类型并赋值给integer\n" +
                "int integer = dynStr;");

            try
            {
                int integer = dynStr;
            }
            catch (RuntimeBinderException e)
            {
                Console.WriteLine($"动态类型变量dynStr隐式转换为int类型值,运行时代码无效并转换失败，抛出异常为：{e.Message}\n");
            }
        }

		public static void LearnDynamicInvoke()
		{
			Console.WriteLine("这里提供了具有继承链的接口和类：\n" +
				"--接口IFunc：具有一个方法成员Func()\n" +
				"--类DynSample：显式实现了接口IFunc，并具有以下成员\n" +
				"    --属性Value 类型dynamic\n" +
				"    --方法GetValue(dynamic dyn) 返回类型dynamic\n" +
				"    --方法Add(string left, string right) 返回类型dynamic\n" +
                "    --方法Add(int left, int right) 返回类型dynamic\n" +
                "    --方法Add(dynamic left, dynamic right) 返回类型dynamic\n" +
                "    --方法PrintInfo() 返回类型void\n" +
                "    --虚方法PrintInfoVirtual() 返回类型void\n" +
				"--类DynSampleChild：派生自类DynSample，并进行了以下操作\n" +
                "    --方法PrintInfo() 使用new关键字隐藏了基类同签名方法\n" +
				"    --虚方法PrintInfoVirtual() 使用override关键字声明重写了基类同签名方法\n");

			Console.WriteLine("声明一个DynSample类型变量dynSample，并实例化一个DynSample对象赋予给它\n" +
                "声明一个DynSample类型变量dynChildBase，并实例化一个DynSampleChild对象赋予给它\n" +
                "声明一个IFunc类型变量iFunc，并将对象dynSample赋予给它\n");

			DynSample dynSample = new DynSample();
			DynSample dynChildBase = new DynSampleChild();
			IFunc iFunc = dynSample;

			Console.WriteLine("使用以下代码分别将上述三个变量赋值给三个对应的动态类型变量dyns、dync、dynf\n" +
                "dynamic dyns = dynSample;\ndynamic dync = dynChildBase;\ndynamic dynf = iFunc;\n");
			
			dynamic dyns = dynSample;
			dynamic dync = dynChildBase;
			dynamic dynf = iFunc;

			Console.WriteLine(">>>分别使用三个动态类型变量进行动态调用<<<");

			var result = dynSample.Add("0x", 1211);

			Console.WriteLine($"使用代码\nvar result = dynSample.Add(\"0x\", 1211);\n对DynSample类重载方法Add进行动态调用\n" +
				$"运行时动态调用了重载方法Add中的Add(dynamic,dynamic)方法，返回结果：{result}\n");

			dyns.Value = "Value";
			var prop = dynSample.GetType().GetProperty("Value");
			var attr = prop.GetCustomAttribute<DynamicAttribute>();

            Console.WriteLine($"使用代码【dyns.Value = \"Value\";】动态调用DynSample类属性Value并赋值“Value”\n随后反射获取DynSample类型的属性Value并查看信息\n" +
                $"  --属性名：{prop.Name} --属性运行时类型：{prop.PropertyType} --属性值：{prop.GetValue(dynSample)} --属性编译时类型：dynamic\n" +
				$"  --属性附加特性：{attr.TypeId}\n");

			string err = "";
			try
			{
				dynf.Func();
			}
			catch(RuntimeBinderException e)
			{
				err = e.Message;
			}
			Console.WriteLine($"使用代码【dynf.Func();】尝试动态调用IFunc接口已被实现的Func方法\n" +
				$"运行时绑定失败，代码无效，错误：{err}\n" +
				$"通过反射获取dynf运行时类型：{dynf.GetType().Name}，该类型实例无法调用Func方法\n");

			Console.Write($"使用代码【dync.PrintInfo();】尝试动态调用PrintInfo方法\n" +
				$"运行时绑定成功，但实际绑定的方法与预期不同\n" +
				$"由于是将子类DynSampleChild实例引用给基类DynSample实例，故基类实例调用的应是其本身的PrintInfo方法\n" +
				$"预期输出：");

			dynChildBase.PrintInfo();

			Console.Write($"实际输出：");

            dync.PrintInfo();

            Console.WriteLine($"通过反射获取dync运行时类型：{dync.GetType().Name}，该类型实例实际调用的是实际类型中覆盖了基类的PrintInfo方法\n");
        }

		public static void LearnDynamicAndObjec()
		{
			Console.WriteLine(">>>编译时类型检查区别<<<\n使用以下代码作为演示：\n" +
                "object objInt = 5;\ndynamic dynInt = 5;\n" +
				"objInt += 5;        //无法通过编译\n" +
				"dynInt += 5;        //跳过编译时检查\n");

			object objInt = 5;
			dynamic dynInt = 5;

			//objInt += 5;	//无法通过编译，因为object类型会在编译时进行安全检查，object类型不支持+=运算符操作
			dynInt += 5;    //dynamic类型会跳过编译时检查，故该语句可通过编译，运行时dynInt获得实际类型后如代码有效则正常执行，否则抛出异常

			Console.WriteLine($"无法对object类型变量进行+=运算符操作，编译无法通过\n" +
				$"dynamic类型变量dynInt运行时实际类型：{dynInt.GetType().Name}，支持+=运算符操作，计算结果值：{dynInt}\n\n");

			Console.WriteLine(">>>调用方法性能区别<<<");
			Console.WriteLine("现提供一个类Calculator，其内部包含一个Add方法，用于计算两个参数的和");
			Console.WriteLine("声明了一个使用类object构造的List<T>实例objects实例，并向其中添加了100000个Calculator实例");
			Console.WriteLine("使用不同方法遍历调用objects中元素的Add方法，测试用时如下：\n");

			List<object> objects1 = new List<object>();
            List<object> objects2 = new List<object>();
            List<object> objects3 = new List<object>();
            object[] oParams = { 2, 3 };

			for (int i = 0; i < 100000; i++)
			{
				Calculator calculator = new Calculator();
				objects1.Add(calculator);
				objects2.Add(calculator);
				objects3.Add(calculator);
			}

			Stopwatch stopwatch = new Stopwatch();

			stopwatch.Start();
			foreach (object item in objects1)
			{
				((Calculator)item).Add(2, 3);
			}
			stopwatch.Stop();

			var timeUsedByObj = stopwatch.Elapsed.TotalMilliseconds;

			stopwatch.Reset();
			stopwatch.Start();
			foreach (dynamic item in objects2)
			{
				item.Add(2, 3);
			}
			stopwatch.Stop();

			var timeUsedByDyn = stopwatch.Elapsed.TotalMilliseconds;

			stopwatch.Reset();
            stopwatch.Start();
            foreach (object item in objects3)
            {
				item.GetType().GetMethod("Add").Invoke(item, oParams);
            }
            stopwatch.Stop();

            var timeUsedByReflect = stopwatch.Elapsed.TotalMilliseconds;


            Console.WriteLine($"使用object作为遍历元素类型循环调用{objects1.Count}个Calculator实例Add方法用时：{timeUsedByObj:000000}毫秒");
            Console.WriteLine($"使用dynamic作为遍历元素类型循环调用{objects2.Count}个Calculator实例Add方法用时：{timeUsedByDyn:000000}毫秒");
            Console.WriteLine($"使用object作为遍历元素类型并对元素使用反射循环调用{objects3.Count}个Calculator实例Add方法用时：{timeUsedByReflect:000000}毫秒");
        }
        public static void StartLearnDynamic()
		{
            string title = "001 动态类型\n" +
                "002 动态绑定与调用\n" +
				"003 dynamic与object引用对象区别\n";

            do
            {
                Console.WriteLine("【学习dynamic类型】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnDynamicVariable(); break;
                    case "002": LearnDynamicInvoke(); break;
					case "003": LearnDynamicAndObjec();break;
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
		private interface IFunc
		{
			public void Func();
		}

        private class DynSample : IFunc
        {
			public dynamic Value { get; set; }
			public dynamic GetValue(dynamic dyn)
			{
				return dyn;
			}

			public dynamic Add(string left, string right) => left + right;
			public dynamic Add(int left, int right) => left + right;

			public dynamic? Add(dynamic left , dynamic right)
			{
				dynamic result = null;
				try
				{
					result = left + right;
				}
				catch(RuntimeBinderException e)
				{
					Console.WriteLine(e.Message);
				}

				return result;
			}
			public void PrintInfo()
			{
				Console.WriteLine("这是类DynSample类实例中的PrintInfo方法输出");
			}

			public virtual void PrintInfoVirtual()
			{
				Console.WriteLine("这是DynSample类实例中的PrintInfoVirtual虚方法输出");
			}
            void IFunc.Func()
            {
				Console.WriteLine("显示实现接口成员");
            }
        }

		private class DynSampleChild : DynSample
		{
            public new void PrintInfo()
            {
                Console.WriteLine("这是类DynSample类派生的DynSampleChild类实例中的PrintInfo方法输出，该方法使用new关键字隐藏了基类中同签名方法");
            }

            public override void PrintInfoVirtual()
            {
                Console.WriteLine("这是DynSample类派生的DynSampleChild类实例中重写的PrintInfoVirtual方法输出");
            }
        }

		private class Calculator
		{
			public void Add(int a, int b)
			{
				var c = a + b;
			}
		}
    }
}
