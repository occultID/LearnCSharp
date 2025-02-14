/*【学习方法】
 * 方法的由来
	• 方法（method）的前身是C/C++语言的函数（function）
		○ 方法是面向对象范畴的概念，在非面向对象语言中仍然称为函数
	• 永远都是类（或结构、记录）的成员
		○ C#语言中函数不可能独立于类（或结构、记录）之外
		○ 只有作为类（或结构、记录）的成员时才被称为方法
		○ C++中类是可以独立于类或结构之外的，成为“全局函数”
	• 是类（或结构体、记录）最基本的成员之一
		○ 最基本的成员只有两个——字段与方法（成员变量与成员函数），本质上还是数据+算法
		○ 方法表示类（或结构、记录）“能做什么事情”
	• 需要方法和函数的原因
		○ 目的1：隐藏复杂的逻辑
		○ 目的2：把大算法分解为小算法
		○ 目的3：复用（reuse，重用）

 * 方法的声明与调用
	• 方法
		○ 方法是包含一系列语句的代码块
		○ 程序通过调用方法并指定方法所需的参数使语句得以执行
	• 声明方法的语法详解
		○ 访问级别 有效的方法修饰符组合 partial 返回类型 方法名称<泛型参数列表>(参数列表) 泛型约束 { 实现代码 }
			§ 访问级别：该项是可选配置，如public、protect、private，不声明则默认private
			§ 有效的方法修饰符组合：该项是可选配置，根据需求选择组合以下任意修饰符
				□ 无：不需要任何修饰的方法
				□ static：声明该方法为静态方法
				□ sealed：声明该方法为密封方法，防止派生类进一步重写方法，必须与override同时使用
				□ abstract：声明该方法为抽象方法，抽象方法只能出现在抽象类中且不能实现
				□ virtual：声明该方法为虚拟方法，说明该方法在子类中可使用override修饰同签名方法来进行重写实现
				□ new：声明该方法为覆盖方法，说明该方法会覆盖基类中同签名的方法实现
				□ override：声明该方法为替代方法或重写基方法，该修饰符使用要求基类同签名方法使用了virtual修饰
				□ extern：声明该方法为外部方法，即方法在外部实现，通常是C#以外的语言
				□ async： 声明该方法为异步方法
				□ unsafe：声明该方法为不安全方法，即方法返回类型或内部实现会使用非托管代码
			§ partial：该项是可选配置，标识方法为分部方法
				□ 使用partial修饰方法时，不能为方法定义访问级别
				□ 使用partial修饰方法时，参数列表不能有输出参数
				□ 使用parital修饰方法时，返回类型必须是void
				□ 使用partial修饰方法时，标识方法是分开实现的，且可能位于多个partial类中
			§ 返回类型：该项是必选配置，即该方法需要返回的最终结果
				□ 任何类型：方法可返回任何类型
				□ void：如果方法不返回任何类型即使用该类型作为返回类型
			§ 方法名称：该项是必选配置，即该方法的名称
				□ 方法名称按规范使用Pascal命名法命名
				□ 方法名称按规范使用动词或动词短语命名
			§ 泛型参数列表：该项是可选配置
				□ 对于使用extern修饰的方法，不得使用该项
				□ 根据实际需要选择是否将方法定义为泛型方法
				□ 类型参数列表放置于方法名称和“()”之间，需使用“<>”包括起来
				□ 泛型参数列表的参数为表示数据类型的形式参数，多个参数使用“,”进行分隔
			§ ()：该项是必选配置，这是方法调用运算符，中间是参数列表
			§ 参数列表：该项是可选配置
				□ 不使用参数则该方法为无参方法
				□ 使用参数则调用方法时需提供匹配的实参
				□ 这里的参数是形式参数，类型分为值参数、引用参数、输出参数、只读参数、数组参数和可选参数
			§ 泛型约束：该项是可选配置
				□ 该项对泛型参数列表类的类型参数进行条件约束
				□ 该项使用的前提是方法使用了泛型参数，对于使用extern修饰的方法，不得使用该项
				□ 即使方法使用了泛型参数，该项也是可选的
			§ {}：花括号内是方法的主体，即方法的实现，即实现该方法的逻辑代码
				□ abstract和extern修饰的方法该项不可选，即不需要实现该方法
				□ 接口中方法根据需要可选
				□ 其他情况必选，即必须为方法提供实现
	• 调用方法
		○ 调用方法：方法名（实际参数）
		○ Argument：实际参数，简称实参
		○ 调用方法时的argument列表要与定义方法时的parameter列表相匹配
			§ C#是强类型语言，argument是值、parameter是变量，值与变量一定要相匹配，不然编译器会报错
		○ 静态方法通过类型调用，实例方法通过实例调用

 * 构造器
	• 构造器（constructor）是类型的成员之一
	• 狭义的构造器指的是“实例构造器”（instance constructor）
	• 调用构造器
		○ 调用：new 类名(实际参数opt)
	• 声明构造器
		○ 声明：访问修饰符  类名（形式参数opt）
	• 构造器的内存原理
		○ 首先，现在stack上分配一个4字节的空间存储引用变量
		○ 然后系统计算实例所需要的内存空间，并在heap上分配实例所需的空间
		○ 如果构造器带参数，就在分配的内存空间中存入相应的数据
		○ 把实例在heap上的起点地值存储到引用变量的储存空间
		○ Ps:如果参数列表中含有引用类型的参数，那么就在实例分配的内存空间中再申请一块4字节的空间存储引用参数的实例地址，然后在heap中再找一块区域存放引用参数的实例

 * 方法的重载
	• 声明带有重载的方法
		○ 同声明一个方法，但是每一个方法的方法签名中方法名称相同，除此之外都要有所差异
	• 方法签名和重载决策
		○ 方法签名（method signature）由方法的名称、类型形参的个数和它的每一个形参（按从左到右的顺序）的类型和种类（值、引用或输出）组成。
		○ 重载决策中方法签名不包含返回类型，
		○ 方法作为委托实例时，需要将方法返回类型纳入方法签名考虑
		○ 实例构造函数签名由它的每一个形参（按从左到右的顺序）的类型和种类（值、引用或输出）组成。
		○ 重载决策（到底调用哪一个重载）：用于在给定了参数列表和一组候选函数成员的情况下，选择一个最佳函数成员来实施调用。
		○ 参数使用ref、out或in进行修饰视为同一签名特征，故不能以此作为重载依据

 * 对方法进行调试
	• 设置断点（breakpoint）
	• 观察方法调用时的call stack
	• Step-in，Step-over，Step-out
	• 观察局部变量的值与变化

 * 方法的调用与栈
	• 方法调用时栈内存的分配
		○ stack frame ：一个方法被调用时，其在栈内存中的布局
		○ 调用一个带参方法，其参数在内存中归主调者管理
			§ 举例：比如main方法调用带参方法c(var),择压栈时先把var压入栈，然后才调用c方法
		○ 主调者对被调方法参数压栈时，顺序为从左到右，即按顺序从第一个参数开始压栈
		○ 被调方法的返回值一般存在CPU的寄存器中，当寄存器无法存储返回值时再在栈上申请空间存储
		○ 当返回值返回主调者后，被调函数占用的栈内存空间即被系统释放
 */

using System;

namespace LearnCSharp.Basic
{
    /// <summary>
    /// 本示例用于演示一般方法的声明、重载与调用
	/// 构造器内容代码示例请参考LearnClass.cs
	/// 本地函数内容代码示例请参考LearnLocalFunction.cs
	/// 匿名函数内容代码示例请参考LearnAnonymousFunction.cs
    /// </summary>
    internal class LearnMethod
    {
		/*【11201：方法示例】*/
        public static void StartLearnMethod()
        {
            Console.WriteLine("\n------示例：方法------\n");

            Console.WriteLine("已创建四个同名（PrintString）方法，它们通过不同的签名实现了重载，接下来将依次调用这四个方法：");
            PrintString();
            PrintString("Hello, World!");
            PrintString("Hello, World!", 5);
            PrintString("Hello, World!", 5, 10);
        }

        private static void PrintString()
		{
            Console.WriteLine($"【方法输出】\n方法名：PrintString\n返回类型：void\n参数：无\n");
		}

        //重载方法决策不包含返回类型，故不能以此作为重载依据
        //public static string PrintString() { }

        private static void PrintString(string str)
        {
            Console.WriteLine($"【方法输出】\n方法名：PrintString\n返回类型：void\n参数：[形参类型：{str.GetType()} | 形参名：str | 实参值：{str}]\n");
        }

        private static void PrintString(string str, int num)
        {
            Console.WriteLine($"【方法输出】\n方法名：PrintString\n返回类型：void\n参数1：[形参类型：{str.GetType()} | 形参名：str | 实参值：{str}]\n参数2：[形参类型：{num.GetType()} | 形参名：num | 实参值：{num}]\n");
        }

        private static bool PrintString(string str, uint cutStartIndex, uint cutEndIndex)
        {
            bool result = false;
			string cutStr = "";
			if (cutStartIndex < cutEndIndex && cutEndIndex < str.Length)
            {
                cutStr = str.Substring((int)cutStartIndex, (int)(cutEndIndex - cutStartIndex));
                result = true;
            }
			else
			{
				result = false;
            }
            Console.WriteLine($"【方法输出】\n方法名：PrintString\n返回类型：bool\n" +
				$"参数1：[形参类型：{str.GetType()} | 形参名：str | 实参值：{str}]\n" +
                $"参数2：[形参类型：{cutStartIndex.GetType()} | 形参名：cutStartIndex | 实参值：{cutStartIndex}]\n" +
                $"参数3：[形参类型：{cutEndIndex.GetType()} | 形参名：cutEndIndex | 实参值：{cutEndIndex}]\n" +
				$"输出值：{cutStr}\n返回值：{result}\n");
            return result;
        }
    }
}
