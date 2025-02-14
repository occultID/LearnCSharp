/*【学习匿名类型】
 * 匿名类型
	• 匿名类型提供了一种方便的方法，可用来将一组只读属性封装到单个对象中，而无需首先显示定义一个类型
		○ 类型名由编译器生成，且不能再源代码级使用
		○ 每个属性的类型由编译器推断
	• 可结合使用new运算符和对象初始化器创建匿名类型
		○ 匿名类型包含一个或多个公共只读属性
		○ 匿名类型不能包含除公共属性以外的其他类成员
	• 匿名类型是class类型
		○ 匿名类型直接派生自object类型
		○ 匿名类型无法被强制转换为除object以外的任何类型
	• 匿名类型通常用于查询表达式的select子句，以便返回源序列中每个对象的属性子集
	• 匿名类型支持采用with表达式的非破坏性修改
	• C#为匿名类型重写了object的基类方法
		○ Equals方法和GetHashCode方法
			§ 根据匿名类型的属性的数据类型所拥有的Equals方法和GetHashCode方法进行定义
			§ 当同一匿名类型的两个示例的所有属性都相等时，两个实例相等
		○ ToString方法
			§ 使用花括号“{}”将每个属性的名称和值包围并输出

 * 匿名类型的声明
	• 形式
		○ 其一：new{ 属性名 = 值, 属性名 = 值, …, 属性名 = 值 }
		○ 其二：new{ 已知类型属性成员, 已知类型属性成员, …, 已知类型属性成员 }
		○ 其三：new{ 含值变量, 含值变量, …, 含值变量 }
	• 可以将匿名类型声明作为var声明变量的值
	• 可以将匿名类型声明作为LINQ查询表达式的子表达式
	• 采取其二其三声明的匿名类型，编译器根据 已知类型属性成员 或 含值变量 的名称来自动为匿名类型生成和它们顺序与名称相同的属性
	• 可以采取类似的方式声明匿名类型的数组类型
		○ 通过隐式键入的本地变量与隐式键入的数组相结合创建匿名键入的元素的数组
		○ 形式
			§ var 变量名 = new[]{ 匿名类型, 匿名类型, …, 匿名类型 }
				□ 每个匿名类型应该结构相同，即公共属性的顺序和名称相同
 */
using System;
using System.Reflection.Metadata.Ecma335;

namespace LearnCSharp.Basic
{
    internal class LearnAnonymousType
    {
		/*【12401：匿名类型示例】*/
        public static void StartLearnAnonymousType()
        {
            Console.WriteLine("\n------示例：匿名类型------\n");
            /*匿名类型代码示例
			  使用如下代码新建三个匿名类型实例person01、person02、person03
			  person02实例是使用with表达式基于person01生成
			  person03实例初始化器中使用了person01的属性作为属性*/
            var person01 = new { Name = "Person01", Age = 15, Gender = '男' };
			var person02 = person01 with { Name = "Person02", Gender = '女' };
			var person03 = new { Name = "Person03", Age = 20, person01.Gender };

            //使用如下代码新建一个匿名类型数组，并将person01、person02、person03添加为数组元素，并额外添加一个相同的匿名类型
            var persons = new[] 
			{
				person01,
				person02,
				person03,
				new { Name = "Person04", Age = 35, Gender = '女' }
            };

            //遍历输出匿名类型数组persons的数据，对于其元素实例，直接采取C#为匿名类型重写的ToString方法输出
            Console.WriteLine($"【匿名类型数组】\n数组名：persons\n类型：{persons.GetType()}\n元素数量：{persons.Length}\n");
			for( int i = 0; i < persons.Length; i++ )
				Console.WriteLine($"【匿名类型】\n元素序号：{i}\n类型：{persons[i].GetType()}\n成员信息：{persons[i]}\n");
        }
    }
}
