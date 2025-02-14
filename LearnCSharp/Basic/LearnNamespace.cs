/*【学习命名空间】
 * 命名空间
	• 命名空间是.NET中提供应用程序代码容器的方式，这样就可以唯一地标识代码及其内容
	• 命名空间也用作.NET中给项分类的一种方式
		○ 大多数项是类型定义
	• 默认情况下，C#代码包含在全局命名空间中
		○ 对于包含在这段代码中的项，全局命名空间中的其他代码只要通过名称进行引用，就可以访问它们
	• 命名空间的定义
		○ 使用namespace关键字为命名空间体（花括号）中的代码块显示定义命名空间
		○ 命名空间的名字通常使用PascalCase命名方式
	• 限定名称
		○ 如果在一个命名空间代码的外部使用该命名空间中的名称，就必须写出该命名空间中的限定名称
		○ 限定名称——命名空间名+“.”+命名空间内的名称
		○ 限定名称包括它所有的分层信息
			§ 如果一个命名空间中的代码需要使用另一个命名空间中定义的名称，就必须包括对该命名空间的引用
			§ 限定名称在不同的命名空间级别直接使用句点字符“.”
	• 名称是由命名空间唯一定义的。
		○ 可在不同的命名空间中定义相同的名称，但在同一段代码中引用这些命名空间的名称时需要使用那个限定名称区分以避免发生冲突
	• using关键字
		○ 创建命名空间后，即可使用using语句简化对他们所含命名空间的访问
		○ 实际上，using语句可以理解为“我们需要这个命名空间中的名称，所以不要每次总是要求使用限定名称”
		○ 别名
			§ 有时，不同命名空间中的相同名称会产生冲突，而由于命名空间层次过多时完整限定名称太长，我们可以使用using语句为命名空间提供一个别名
			§ 别名定义： using  别名  =  完整命名空间名
 */
extern alias Helper;//引入外部程序集，并且为其创建一个别名

using System;//使用using引入System命名空间，即可简化对该空间内容的访问
using Sys = System;//使用using引入System命名空间并为其定义一个别名
using Help = Helper.HelperLibForLearnCSharp;

/*文件范围的命名空间声明使你能够作出以下声明：一个文件中的所有类型都在一个命名空间中
  一个代码文件中只能存在一个文本范围的命名空间，且必须放置于除using指令以外的其他元素前
  一旦声明文本范围的命名空间，则该代码文件内不得在声明其他任何命名空间或嵌套命名空间
  文件范围的命名空间声明方法如下：*/
//namespace LearnCSharp.Basic;

//这是我们自定义的命名空间
//这其实相当于一个嵌套命名空间
//嵌套命名空间可以简化成下面这个形式 命名空间名称.次级命名空间名称.三级命名空间名称
namespace LearnCSharp.Basic
{
    internal class LearnNamespace
    {
		/*【11701：命名空间示例】*/
		public static void StartLearnNamespace()
		{
			//使用完整限定名称访问System.Console.WriteLine方法：
			System.Console.WriteLine("这是使用完整限定名称访问System.Console.WriteLine方法的输出");
			//使用using System之后访问System.Console.WriteLine方法：
			Console.WriteLine("这是使用using System之后访问System.Console.WriteLine方法的输出");
			//使用using Sys=System之后用别名访问System.Console.WriteLine方法；
			Sys.Console.WriteLine("这是使用using Sys = System之后用别名访问System.Console.WriteLine方法的输出");
        }
    }
}

//命名空间嵌套也可以如下形式，和上一形式等效
namespace LearnCSharp
{
	namespace Basic
	{
		//internal class LearnNamespace 这里注释了原因是因为命名空间和上述等效，同一命名空间类名不能重复
		
	}
}

