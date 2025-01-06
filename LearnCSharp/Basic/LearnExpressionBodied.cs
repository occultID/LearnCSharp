﻿/*【学习表达式主体定义】
 * 表达式主体定义
	• 通过表达式主体定义，可采用非常简洁的可读形式提供成员的实现
	• 只要任何支持的成员（如方法或属性）的逻辑包含单个表达式，就可以使用表达式主体定义
	• 表达式主体定义常规语法
		○ member => expression;
			§ expression是有效的表达式

 * 表达式主体定义的可用对象
	• 方法
		○ 表达式主体方法包含单个表达式
			§ 它返回的值的类型与方法的返回类型匹配
			§ 对于返回void的方法，其表达式则执行某些操作
		○ 语法
			§ 有具体返回类型：返回类型 方法名称( 参数列表 ) => 返回对应类型的单个表达式;
			§ void返回类型 ：void 方法名称( 参数列表 ) => 执行某些操作的单个表达式;
	• 只读属性
		○ 可使用表达式主体定义来实现只读属性
		○ 语法
			§ 属性类型 属性名称 => 返回对应封装类型的单个表达式;
	• 属性
		○ 可使用表达式主体定义来实现属性的get和set访问器
		○ 语法
			§ 属性类型 属性名称
			{
				get => 封装字段 或 返回属性类型的单个表达式
				set => 封装字段 = value 或 将value进行处理的单个表达式
			}
	• 构造函数
		○ 构造函数的表达式主题定义通常包含单个赋值表达式或一个方法调用
		○ 该方法调用可处理构造函数的参数，也可初始化实例状态
		○ 语法
			§ 类型名( 参数列表 ) => 单个赋值表达式或一个方法调用
	• 终结器
		○ 终结器的表达式主体定义通常包含清理语句
		○ 语法
			§ ~类型名() => 用于清理资源的单个表达式
	• 索引器
		○ 可使用表达式主体定义来实现索引器的get和set访问器
		○ 语法
			§ 索引器类型 this[索引]
			{
				get => 返回索引目标[索引]值的单个表达式;
				set => 索引目标[索引] = value;
			}
 */

namespace LearnCSharp.Basic
{
    internal class LearnExpressionBodied
    {
        /*【实践表达式主体定义】
		 * 根据上述知识，这里将Student类作为本类的内部类来定义用于学习表达式主体定义
		 */
        private class Student
		{
			private int stuID;
			private string? name;
			private static readonly int latestAdmissionYear = DateTime.Now.Year;

			//使用表达式主体定义属性
			public int StuID
			{
				get => LatestAdmissionYear*1_0000 + stuID;
				private set
				{
					if (value > LatestAdmissionYear * 1_0000 && value <= LatestAdmissionYear * 1_0000 + 9999)
					{
						stuID = value % 1_0000;
					}
					else
						throw new ArgumentOutOfRangeException(nameof(value));
				}	
			}

			//使用表达式主体定义属性
			public string Name
			{
				get => name!;
				set => name = !string.IsNullOrWhiteSpace(value) ? value : "无名氏";
			}

			//使用表达式主体定义只读属性
			public static int LatestAdmissionYear => latestAdmissionYear;

			//使用表达式主体定义构造函数
			private Student() => StuID = LatestAdmissionYear * 1_0000 + Random.Shared.Next(1, 1_0000);

            //使用表达式主体定义构造函数
            public Student(string name) : this() => Name = name;

			//使用表达式主体定义方法
			public void IntroduceSelf() => Console.WriteLine($"大家好，我是{Name}，学号{StuID}，入学年份是{latestAdmissionYear}年");

			//使用表达式主体定义解构方法
			public void Deconstruct(out int stuID, out string name) => (stuID, name) = (StuID, Name);
        }

		public static void StartLearnExpressionBodied()
		{
            /*表达式主体定义只是一个“语法糖”
			  当成员实现只有一个有效表达式或一个语句时，使用表达式主体定义只是使代码更加简洁和可读而已
			  对于编译器来说，最终的实现和成员原本的默认实现是一样的
			  实例化内部成员使用表达式主体定义的Student类，对成员的访问依然如旧，不受影响*/

            //输出示例代码
            string outputString = "Student student = new Student(\"张三\");\n" +
				"student.IntroduceSelf(); //访问方法\n" +
				"(int StuID, string Name) = student; //解构实例\n" +
				"Console.WriteLine($\"解构实例({StuID}, {Name})\");\n";

			Console.WriteLine(outputString);

            Student student = new Student("张三");
			student.IntroduceSelf(); //访问方法
			(int StuID, string Name) = student; //解构实例
			Console.WriteLine($"解构实例({StuID}, {Name})");
		}
    }
}
