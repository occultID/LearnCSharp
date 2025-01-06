/*【学习特性】
 * 特性概述
	• C#提供了一种扩展机制用于将自定义信息添加到代码元素，即特性
		○ CLR使用户能添加类似于关键字的描述性声明来批注编程元素（如类型、字段、方法和属性）
		○ 编译运行时的代码时，特性将被转换为MSIL，并和编译器生成的元数据一起放置在可移植可执行（PE）文件内
	• 特性可以有效地将元数据或声明性信息与代码（程序集、类型、方法、属性等）相关联
		○ 将特性与程序实体相关联后，可以在运行时使用“反射”这项技术查询特性
	• 特性具有如下属性：
		○ 特性向程序添加元数据
			§ 元数据是程序中定义的类型的相关信息
			§ 所有.NET程序集都包含一组指定的元素据，用于描述程序集中定义的类型和类型成员
			§ 可以添加自定义特性来指定所需的其他任何信息
		○ 可以将一个或多个特性应用于整个程序集、模块或较小的程序元素（如类和属性）
		○ 特性可以像方法和属性一样接收自变量
		○ 程序可使用反射来检查自己的元数据或其他程序中的元数据
	• 特性是通过“特性类”来声明定义的
		○ 这些特性类可能具有位置特性参数和命名特性参数
			§ 位置特性参数对应特性类的公有构造函数的参数
			§ 命名特性参数对应特性类的公开字段或公开属性
		○ 特性使用特性规范附加到C#程序中的实体，可以在运行时作为特性实例检索

 * 特性类
	• 直接或间接派生自抽象类System.Attribute的类是一个特性类
		○ 特性类声明定义了一个特性，可以将其放置在新类型的声明上
		○ 特性类也是一个类类型，故其声明与定义同类类型一致，但需额外考虑如下规则
			§ 按照约定，需要为特性类附加AttributeUsageAttribute特性
				□ AttributeUsageAttitude包含下列三个成员，这对创建自定义特性非常重要
					® AttributeTargets成员
						◊ 该成员的数据类型是一个带标记的枚举类型--AttributeTargets
						◊ 该成员用于指定自定义特性可以应用于那些目标程序元素
						◊ 该成员默认值为AttributeTargets.All，表示该特性可应用于所有程序元素
					® Inherited属性
						◊ 该属性指明该特性对应用了该特性的类的派生类是否依旧有效
						◊ 如果派生类可以继承该特性，则设置值为true，否则为false
						◊ 该属性默认值为true
					® AllowMultiple属性
						◊ 该属性指明元素能否包含特性的多个实例
						◊ 允许包含特性的多个实例，则设置值为true，否则为false
						◊ 该属性默认值为false
			§ 按照约定，特性类的命名需添加后缀Attitude
				□ 使用特性时可以包含或省略此后缀
	• 特性类的声明定义
		○ 声明形式：
			§ [AttributeUsage(AttributeTargets枚举值, Inherited=布尔值，AllowMultiple=布尔值)]
			public class 特性名: Attribute
			{
				类成员
			}
				□ AttributeUsageAttribute特性在使用中可以省略Attribute后缀
					® AttributeTarget枚举值：必须明确指示，因为这是AttributeUsageAttribute构造函数要求的
					® Inherited = 布尔值：是采用命名参数的形式显示为Inherited属性赋值，如果使用默认值则可以省略该项
					® AllowMultiple = 布尔值：是采用命名参数的形式显示为AllowMultiple属性赋值，如果使用默认值则可以省略该项
					® 声明定义特性类时AttributeUsageAttribute特性在C#中是可选的，在VB中是必选的
				□ public：特性类必须声明为公共类
				□ class：声明类的关键字
				□ 特性名：特性类的名称
					® 通常特性类的名称以单词Attribute作为后缀
						◊ 这不是必须的，但建议按照约定采取使用Attribute后缀的命名方式
					® 在实际应用特性时，Attribute后缀是可以省略的
				□ :Attribute：所有特性类必须直接或间接从System.Attribute类继承
				□ { 类成员 }：特性类的类成员定义与传统类一致
					® 通常情况下特性的类成员一般只包含构造函数和属性
						◊ 声明构造函数
							} 特性类通过构造函数初始化
							} 构造函数用于初始化特性的数据成员
								– 构造函数可以提供参数列表，内部使用参数来对数据成员进行初始化
							} 可以重载构造函数以适应值的各种组合
							} 如果定义了属性，可以在实例化特性时，在()内使用属性名进行命名参数赋值来初始化属性值
							} 构造函数的参数作为位置特性参数使用
								– 如果存在参数，则在实例化特性时必须提供，故其为必选参数
						◊ 声明公开属性或公开字段
							} 公开属性或字段作为命名特性参数使用
								– 如果定义了它们，则在实例化特性时可选提供，故其为可选参数，不提供则使用默认值
							} 属性用于提供一种简单的方法来返回由特性存储的值
					® 特性参数类型
						◊ 位置特性参数和命名特性参数的数据类型仅限于下述类型
							} .NET内置基本值类型
							} string和object类型
							} System.Type类型
							} public访问级别的枚举类型
							} 上述所有类型对应的一维数组
							} 不具有上述任意类型之一的构造函数参数或公共字段、属性不能用作特性规范中的位置参数或命名参数
			
 * 应用特性
	• 特性类的实例化和传统类有所区别
		○ 特性类可以直接将构造函数放置于一个[]中进行实例化
		○ 特性类采用上述方式实例化时可以省略Attribute后缀
	• 通过以下步骤来应用特性类
		○ 定义新特性或者使用现有的.NET特性
		○ 通过将特性置于紧邻元素之前，将该特性应用于代码元素
			§ 在C#中，使用方括号[]将特性括起来，并通过空格或换行于元素分开
			§ 需要注意特性是否对当前元素可用
		○ 指定特性的位置特性参数和命名特性参数
			§ 位置参数是必须的且位于所有命名参数之前
				□ 它们对应于特性类中某个构造函数的参数
			§ 命名参数是可选的且对应于特性类的读/写属性
	• 编译代码时，特性将被发到元数据中
		○ 通过运行时反射服务元数据可用于CLR和任何自定义工具或应用程序
	• 特性目标
		○ 特性目标是指应用特性的实体
			§ 很多时候我们无需显示指定特性目标
			§ 默认情况下，特性应用于紧跟在它后面的元素
		○ 显示指定特性目标
			§ 形式：[目标值: 特性]
				目标值		适用对象
				assembly	整个程序集
				module		当前程序集模块
				field		类或结构中的字段
				event		事件
				method		方法或get/set访问器
				param		方法参数或属性set访问器参数
				property	类或结构中的属性
				return		方法或属性/索引器get访问器的返回值
				type		结构、类、接口、枚举或委托
	• 特性的常见用途
		○ 在Web服务中使用WebMethodAttribute特性标记方法，以指明方法应可通过SOAP协议进行调用
		○ 描述托管代码在与非托管代码互操作时如何封送方法参数——MarshalAsAttribute特性
		○ 描述类、方法和接口的COM属性
		○ 使用DllImportAttribute类调用非托管代码
		○ 从标题、版本、说明或商标方面描述程序集
		○ 描述要序列化并暂留类的哪些成员
		○ 描述如何为了执行XML序列化在类成员和XML节点之间进行映射
		○ 描述方法的安全要求
		○ 指定用于强制实施安全规范的特征
		○ 通过实时编译器（JIT）控制优化，这样代码将一直易于调试
		○ 获取方法调用方的相关信息
 */

using LearnCSharp.Professional.LearnAttributesSpace;
using System.Reflection;
using System.Runtime.CompilerServices;

//这里演示了一个应用于整个程序集的特性
//AssemblyDescriptionAttribute特性用于描述程序集的信息
[assembly: AssemblyDescription("该程序用于学习和演示C#代码")]

namespace LearnCSharp.Professional.LearnAttributesSpace
{
	//自定义一个特性类，用于添加对任意程序元素的描述
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		public bool HasDescription { get; set; } = false;
		public string? Description { get; set; } = null;
		public DescriptionAttribute(string description) => Description = description;
	}
}

namespace LearnCSharp.Professional
{
	[Description("类型--LearnAttributes", HasDescription = true)]
    internal class LearnAttribute
    {
		private static string outPutString = "[Description(\"类型：LearnAttributes\",HasDescription = true)]\n" +
                "internal class LearnAttributes\n{" +
                "    [property: Description(\"属性名：ClassName 存储值：LearnAttributes\",HasDescription = true)]\n" +
                "    public static string ClassName { get; } = \"LearnAttributes\";\n\n" +
                "    [method:Description(\"StartLearnAttribute方法\")]\n" +
                "    public static void StartLearnAttribute()\n    {\n" +
                "        [Description(\"局部方法LocalFunc位于方法StartLearnAttribute中\",HasDescription = true)]\n" +
                "        void LocalFunc(int number, [CallerArgumentExpression(\"number\")] string member = null)\n{\n" +
                "        Console.WriteLine($\"局部方法形参名称：number\\n调用方传入实参名称：{member}\\n传入值：{number}\");\n    }\n\n" +
                "    int num = 5;\n" +
                "    LocalFunc(num);\n}\n";

		private static string attributesString = "[AttributeUsage(AttributeTargets.All)]\n" +
			"public class DescriptionAttribute : Attribute\n{\n" +
			"    public bool HasDescription { get; set; } = false;\n" +
			"    public string? Description { get; set; } = null;\n" +
			"    public DescriptionAttribute(string description) => Description = description;\n}\n";


        [Description("属性名--ClassName 存储值--LearnAttributes",HasDescription = true)]
		public static string ClassName { get; } = "LearnAttributes";

		[Description("StartLearnAttribute方法",HasDescription = true)]
		public static void StartLearnAttribute()
		{
			Console.WriteLine($"使用以下代码声明并定义了一个自定义特性类DescriptionAttribute：\n\n{attributesString}");
			Console.WriteLine($"以下代码使用了自定义特性Description来为类或成员附加了特性：\n\n{outPutString}");

			[Description("局部方法LocalFunc位于方法StartLearnAttribute中", HasDescription = true)]
			void LocalFunc(int number, [CallerArgumentExpression("number")] string member = null)
			{
				Console.WriteLine($"局部方法形参名称：number\n调用方传入实参名称：{member}\n传入值：{number}");
			}

			int num = 5;
			LocalFunc(num);

			Console.WriteLine();
			Console.WriteLine("使用反射获取该类使用的相关特性信息：\n");
			FindAttributeByReflection();
		}

		private static void FindAttributeByReflection()
		{
			var classType = typeof(LearnAttribute);
			Console.WriteLine($"类型名称：{classType.Name}");
			PrintAttributesInfo( classType );

			Console.WriteLine();

			var members = classType.GetMembers();
			foreach (var member in members)
			{
				Console.WriteLine($"成员名称：{member.Name} 成员：{member.MemberType} 所属类型：{member.DeclaringType.Name}");
				PrintAttributesInfo(member);
				Console.WriteLine();
			}

			void PrintAttributesInfo(MemberInfo member, string space = "")
			{
				var attrs = member.GetCustomAttributes<DescriptionAttribute>();
                if (attrs is null || attrs.Count() == 0)
                    Console.WriteLine($"{space}--无附加特性");
                else
                {
                    Console.WriteLine($"{space}--附加特性如下");
                    foreach (var attr in attrs)
                    {
                        Console.WriteLine($"{space}  --特性类：{attr.GetType().Name}");
                        foreach (var p in attr.GetType().GetProperties())
                        {
                            Console.WriteLine($"{space}    --特性参数：{p.Name}");
                            Console.WriteLine($"{space}    --特性值：{p.GetValue(attr)}");
                        }
                    }
                }
            }
		}
    }
}
