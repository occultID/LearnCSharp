/*【学习记录】
 * 记录简介
	• 记录是C#9.0引入的一种引用类型，类似于类
		○ 记录使用record关键字来进行声明
		○ 记录用来提供用于封装数据的内置功能
		○ 可以理解为“具有值类型特征”的类
	• C#10.0的更新
		○ C#10开始允许使用record class语法作为同义词来阐明引用类型
		○ C#10开始可以使用record struct使用相同功能定义值类型
	• 通过使用位置参数或标准属性语法，可以创建具有不可变属性的记录类型
		○ 实际上也可创建具有可变属性和字段的记录
		○ 但大多数情况下记录是用于支持不可变的数据模型
	• 记录本质上还是一个类或结构，只是对其进行了一定的特殊化
		○ 这种特殊化仅仅只是针对编写代码时作为“语法糖”
		○ 代码编译完成后或者在运行时，其本身还是一个类或者结构

 * 记录的声明与定义
	• 记录需要使用record关键字进行声明
	• 记录的声明定义形式
		○ [特性]
		访问级别 有效的记录修饰符组合 partial record class/struct 记录名<泛型参数列表>(位置参数列表):基记录/接口 泛型参数约束 { 记录体 } 
			§ [特性]：该项是可选配置，标识记录是否附加了特性
			§ 访问级别：该项是可选配置，标识记录的访问级别
				□ public：公开访问
				□ protected：受保护的访问
				□ internal：内部访问
				□ private：私有访问
			§ 有效的记录修饰符组合：该项是可选配置，标识记录的类别
				□ abstract：标识记录是一个抽象记录，该项只能用于record引用类型
				□ sealed：标识记录是密封的不能被继承，该项只能用于record引用类型
				□ readonly：标识记录结构只读，该项只能用于record struct
				□ unsafe：标识记录会包含非托管类型和代码
			§ partial：该项是可选配置，标识记录是分为多个部分实现的
				□ 分部记录最多只能有一个分部来提供记录参数
			§ record：该项是必选配置，声明定义记录的必须关键字
			§ class/struct：该项根据以下情况选择
				□ 当声明一个记录引用类型时，class是可选的，不能使用struct关键字
				□ 当声明一个记录结构类型时，struct是必选的，不能使用class关键字
			§ 记录名：该项是必选配置，标识记录的名称
				□ 通常采用Pascal命名法进行命名
			§ <泛型参数列表>：该项是可选配置，标识记录是否采用泛型实现
				□ <>尖括号标识泛型参数，其内部为表示数据类型的形式参数
				□ 如有多个泛型参数，每个参数使用“,”隔开
			§ (位置参数列表)：该项是可选配置，标识记录是否采用参数来进行初始化构造
				□ 位置参数也可称为记录参数或记录构造参数
				□ 位置参数列表用于作为记录的主构造函数参数使用
				□ 记录会在内部自动生成和位置参数对应的公共属性
				□ 位置参数的形式类似于方法参数
					® 由于记录会根据位置参数生成对应的同签名只读属性，故其名称采用Pascal命名法
				□ 位置参数不能使用ref、out或this进行修饰，但可以使用in和params进行修饰
			§ :基记录/接口：该项为可选配置，标识结构是否有继承某个基记录或实现某些接口
				□ “:”标识记录会继承某个基记录或内部将实现某些接口
				□ 记录引用类型可以继承基记录或者实现某些接口，记录结构只能实现接口而不能继承记录
				□ 记录引用类型继承只支持单继承，即一次只能继承一个记录
				□ 记录不能从类继承，除非这个类是最终基类Object
				□ 类不能从记录继承
				□ 被实现的接口可以有一个或多个，多个接口使用“,”进行分隔
			§ 泛型参数约束：该项为可选配置，标识泛型参数是否需要进行某种约束
				□ 使用该项的前提是记录使用了泛型参数
				□ 即使记录使用了泛型参数，该项也是可选的
			§ { 记录体 }：该项为可选配置，即记录的主体实现
				□ {}花括号用于包含记录体的主体实现
				□ 记录体即所有的记录成员
				□ 如无需该项配置，需使用“;”完成声明
		
 * 记录成员详解
	• 常量
		○ 记录中的常量与类中的常量具有相同含义
	• 字段
		○ 记录中的字段与类中的字段具有相同含义
		○ 原则上应将字段设置成readonly以保障记录不可变
	• 属性
		○ 记录中的属性与类中的属性具有相同含义
		○ 原则上应将属性设置成只读的以保障记录不可变
		○ 如果需要在实例化时使用初始化器来初始化只读属性，可设置init访问器
		○ 如果记录声明时提供了位置参数列表
			§ 记录引用类型和只读记录结构类型会在内部自动生成与参数同签名的只读init属性
			§ 记录结构类型会在内部自动生成与参数同签名的可读写属性
			§ 此时如果用户在记录体重新定义了同签名的属性，则其会隐藏并覆盖位置参数
				□ 如需将自定义属性关联指向构造参数，需将位置参数直接赋值给对应属性
		○ 记录会自动生成一个类型为Type和名为EqualityContract的只读属性
			§ 如果记录是直接派生自Object的，那么该属性形式如下
				□ 记录为密封(sealed)或为记录结构：private Type EqualityContract{ get; }
				□ 记录非密封或非记录结构：protected virtual Type EqualityContract{ get; }
			§ 如果记录是派生自一个基记录，那么该属性形式如下
				□ 记录有可能再次派生：protected override Type EqualityContract{ get; }
				□ 记录密封不再会派生：protected sealed override Type EqualityContract{ get; }
	• 方法
		○ 常规方法
			§ 记录中的常规方法与类中的常规方法具有相同含义
			§ 记录内部重写了Object的Equals和GetHashCode方法来更高效的完成其“值相等”比较
			§ 记录内部重写了Object的ToString方法来更好的输出其字符串输出形式
			§ 记录会自动生成一个返回类型为bool，名称为PrintMembers且含单个类型为StringBuilder的参数的方法
				□ 该方法用于在重写的ToString方法中来构建格式化字符串
				□ 如果记录是直接派生自Object的，那么该方法形式如下
					® 记录为密封或为记录结构：private bool PrintMembers(StringBuilder builder)
					® 记录非密封或非记录结构：protected virtual bool PrintMembers(StringBuilder builder)
				□ 如果记录是派生自一个基记录，那么该方法形式如下
					® 记录有可能再次派生：protected override bool PrintMembers(StringBuilder builder)
					® 记录密封不再会派生：protected sealed override bool PrintMembers(StringBuilder builder)
			§ 记录会自动生成一个保留名称的公共无参数实例克隆方法
				□ 该方法用于支持with表达式实现相关功能
				□ 用户不能自定义克隆方法来替换该方法
				□ 用户不能在记录体内定义名为Clone的任何成员
				□ 该方法的形式可能会如下：
					® public virtual 记录名 <Clone>$() 
		○ 构造函数
			§ 记录中的构造函数与类中的构造函数具有相同含义
			§ 如果记录声明时提供了位置参数列表
				□ 则记录会自动生成一个同参数列表的主构造函数
					® 该构造函数不能被覆盖，即开发者不能手动声明定义一个同签名构造函数
				□ 则记录会自动生成一个含单个该记录类型的参数的复制构造函数
					® 自动生成的复制构造函数是受保护访问级别的
					® 记录引用类型用户可以重新自定义一个复制构造函数来覆盖自动生成的复制构造函数
					® 记录结构不内置支持复制构造函数，用户也无需自定义，因为一个结构类型的值赋值给另一个结构类型的变量本身就是复制一个拷贝
				□ 此时开发者如果定义了其他构造函数，则必须使用this关键字指向主构造函数来构建构造函数链
		○ 析构函数
			§ 记录中的析构函数与类中的析构函数具有相同含义
			§ 当记录为记录结构时，不能声明和定义析构函数
		○ 解构函数
			§ 记录中的解构函数与类中的解构函数具有相同含义
			§ 如果记录声明时提供了位置参数列表
				□ 记录类型会自动生成解构函数无需用户手动声明定义
				□ 用户手动声明的解构函数如果同记录生成的签名相同，则会覆盖自动生成的解构函数
		○ 运算符
			§ 记录中的运算符与类中的运算符具有相同含义
			§ 记录内部重载了==和!=运算符来实现“值相等”比较
	• 索引器
		○ 记录中的索引器与类中的索引器具有相同含义
	• 事件
		○ 记录中的事件与类中的事件具有相同含义
	• 静态成员
		○ 记录中的静态成员与类中的静态成员具有相同含义
	• 嵌套类型
		○ 记录中的嵌套类型与类中的嵌套类型具有相同含义
	• unsafe成员
		○ 记录中的unsafe成员与类中的unsafe成员具有相同含义
		○ 记录的实例成员不能为非托管类型的

 * 记录的继承
	• 记录的继承与类的继承大同小异
		○ 记录和类之间不能相互继承，但记录本身是直接派生自Object的，可显示指定记录的基类为Object
		○ 记录和类一样只支持单继承，而可以实现多个接口
		○ 使用了位置参数的记录，在继承时应保持相同规范
		○ 记录结构不支持继承，只能实现接口
	• 记录自动生成的以下成员可在派生记录中进行显示重写
		○ 复制构造函数
		○ EqualityContract属性
			§ 不能改变其访问级别protected
		○ PrintMembers方法
			§ 不能改变其访问级别protected

 * 非破坏性变化与with表达式
	• 如果需要复制包含一些修改的实例，可以使用with表达式来实现非破坏性变化
		○ 非破坏性变化，即可以根据原始(只读)对象来创建一个新的副本，而这个副本相对有变化，但不会影响原本的对象
	• with表达式事项
		○ with表达式不能独立作为语句
		○ 有效的with表达式包含具有非void类型的接收方，接收方类型必须是一条记录
		○ with表达式创建一个新的记录实例，该实例是现有记录实例的一个副本，但可能修改了指定属性和字段
		○ with表达式实际就是通过记录生成的保留实例Clone方法来调用复制构造函数创建新实例
		○ with表达式的使用前提是record属性是只读init属性或可读写属性
		○ with表达式的结果是一个浅克隆副本，这意味着对于引用类型的属性，只复制对实例的引用
			§ 原始记录和副本记录最终都具有对同一实例的引用
	• with表达式的形式
		○ 记录类型 实例名 = 原记录实例名 with { 初始化器 }
		○ 初始化器的使用方式同对象初始化器
 */

using LearnCSharp.Basic.LearnRecordSpace;

/*【记录代码示例】
 * 下面命名空间中设计了三种声明定义的记录
 */
namespace LearnCSharp.Basic.LearnRecordSpace
{
	public record Point2DWithParams(int X,int Y);

	public record Point2DWithBody
	{
		public int X { get; init; }
		public int Y { get; init; }

		public Point2DWithBody(int x,int y)
		{
			X = x;
			Y = y;
		}

		//该声明模式下需要自己定义解构函数
        public void Deconstruct(out int x,out int y)
		{
			(x, y) = (X, Y);
		}
    }

	public sealed record Point2DWithParamsAndBody(int X,int Y)
	{
		//对于使用了位置参数的记录，如果用户手动定义了同签名的属性，需要使用如下语法进行关联
		//否则位置参数会被忽略
		public int X { get; init; } = X;
		public int Y { get; init; } = Y;

	}
}

namespace LearnCSharp.Basic
{
    public class LearnRecord
    {
		public static void StartLearnRecord()
		{
			string outputString = "根据已经定义好的三个记录来创建三个不同的记录实例：\n" +
				"Point2DWithParams p2dPara = new Point2DWithParams(10, 20);\n" +
				"Point2DWithBody p2dBody = new Point2DWithBody(15, 25);\n" +
				"Point2DWithParamsAndBody p2dFull = new Point2DWithParamsAndBody(30, 40);\n";

			Console.WriteLine(outputString) ;

            Point2DWithParams p2dPara = new Point2DWithParams(10, 20);
			Point2DWithBody p2dBody = new Point2DWithBody(15, 25);
			Point2DWithParamsAndBody p2dFull = new Point2DWithParamsAndBody(30, 40);

			Console.WriteLine("分别输出三个记录，可以发现记录内部重写的ToString方法输出：");
			Console.WriteLine(p2dPara);
			Console.WriteLine(p2dBody);
			Console.WriteLine(p2dFull);
			Console.WriteLine();

			Console.WriteLine("以p2dFull为例使用with表达式非破坏性拷贝出两个不同实例，前者不作属性改变，后者改变属性Y值。\n" +
                "var p2dFullCopy1 = p2dFull with { };\nvar p2dFullCopy2 = p2dFull with { Y=30 };\n");

			var p2dFullCopy1 = p2dFull with { };
            var p2dFullCopy2 = p2dFull with { Y=30 };

			Console.WriteLine("分别输出实例p2dFull、p2dFullCopy1、p2dFullCopy2的ToString字符串：");
            Console.WriteLine(p2dFull);
			Console.WriteLine(p2dFullCopy1);
			Console.WriteLine(p2dFullCopy2);
			Console.WriteLine();

			Console.WriteLine("以上三个实例对比展示值相等性：");
			Console.WriteLine($"p2dFull == p2dFullCopy1 --output:{p2dFull == p2dFullCopy1}");
			Console.WriteLine($"p2dFull == p2dFullCopy2 --output:{p2dFull == p2dFullCopy2}");
        }
    }
}
