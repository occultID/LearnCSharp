/*【学习引用类型之类（class）】
 * 类
	• 类是一种数据结构，它可能包含数据成员、函数成员和嵌套类
	• 类类型支持继承，这是一个派生类可以扩展和专用化基类的机制
	• 类是对现实世界事物的抽象模型
		○ 非静态类
			§ 对现实世界事物的共性的一个抽象描述，如人类
			§ 非静态类使用需进行实例化，实例是一个具体的对象，即需要为抽象描述赋予实际意义，比如每一个人
			§ 现实事物包括“物质”与“运动”，可以理解为“实体”和“逻辑”
				□ 非静态类就是对“实体”和“逻辑”进行统一建模
				□ 建模是一个去伪存真，由表及里的过程
		○ 静态类
			§ 静态类要求其所有成员都是静态的，即所有成员都是属于类的
			§ 静态类相当于抽象描述和实际意义是一体的
			§ 静态类一般用于实现本身就具有一定抽象意义的现实概念，比如数学计算

 * 类的声明与定义
	• 类类型需要使用class关键字来声明
	• 类的声明和定义是一体的，声明类即需实现结构
	• 类的声明定义形式：
		○ [特性]
		访问级别 有效的类修饰符组合 partial class 类名<泛型参数列表>:基类或接口 泛型参数约束 { 类成员 }
			§ [特性]：该项为可选配置，标识类是否被附加特性
			§ 访问级别：该项为可选配置，标识该类的访问权限
				□ 无：默认为internal
				□ public：公开访问级别
				□ protected：受保护的访问级别
				□ internal：内部访问级别
				□ private：私有访问级别
			§ 有效的类修饰符组合：该项为可选配置，标识类的类别
				□ static：标识声明的类为静态类
				□ sealed：标识声明的类为密封类，该类不能被继承
				□ abstract：标识声明的类为抽象类，抽象类不能直接实例化，只能作为其它类的基类
				□ unsafe：标识声明的类可用于非托管代码或内部含有非托管类型的成员
				□ new：只能用于修饰嵌套于类或结构中的嵌套类或嵌套结构
			§ partial：该项为可选配置，标识类是否分为多个部分来实现
				□ 无：该类在声明时即需完整定义
				□ 有：至少有一个分部类有实现
			§ class：该项为必选配置，这是声明并定义类的必须关键字
			§ 类名：该项为必选配置，这是类类型的名字
				□ 类名通常采用Pascal命名法
				□ 类名通常是一个名词，该名词应可以概括一系列有共性的现实事物
			§ <泛型参数列表>：该项为可选配置，标识类是否有泛型实现
				□ <>尖括号标识泛型参数，其内部为表示数据类型的形式参数
				□ 如有多个泛型参数，每个参数使用“,”隔开
			§ :基类或接口：该项为可选配置，标识类是否有继承某个基类或实现某些接口
				□ “:”标识类继承于某一基类或将实现标识的接口
				□ 类只能单继承，一次只能继承一个基类
				□ 被实现的接口可以有一个或多个，多个接口使用“,”进行分隔
				□ 如果类同时继承于某一基类和实现多个接口
					® 基类应放置于列表最前方，其后跟随接口
					® 基类与接口、接口与接口之间使用“,”进行分隔
			§ 泛型参数约束：该项为可选配置，标识泛型参数是否需要进行某种约束
				□ 使用该项的前提是类使用了泛型参数
				□ 即使类使用了泛型参数，该项也是可选的
			§ { 类成员 }：该项为必选配置，即类的主体实现
				□ {}花括号用于包含类的主体实现
				□ 类的主体可能包括数据成员、函数成员或嵌套类
		
 * 类成员详解
	• 常量
		○ 常量定义
			§ 参考C#语言定义文档 
			§ 常量（constant）是表示常量值（即可以在编译时计算的值）的类成员
			§ 常量隶属于类型而不是对象（实例），即没有“实例常量”
				□ “实例常量”的角色由只读实例字段来担当
			§ 注意区分成员常量与局部常量
			§ 常量在程序的生命周期内不会改变
			§ 仅C#内置类型（不包括System.Object)可以声明为常量
			§ 用户定义的类型（包括类、结构和数组）不能为常量
			§ C#不支持const方法、属性或事件
		○ 常量的声明
			§ 形式：访问修饰符 const 数据类型 常量名 = 常量值
			§ 常量一但声明就必须初始化赋值，且不可改变
		○ 各种“只读”的应用场景
			§ 为了提高程序可读性和执行效率——常量
			§ 为了防止对象的值被改变——只读字段
			§ 向外暴露不允许修改的数据——只读属性（静态或非静态），功能与常量有一些重叠
			§ 当希望成为常量的值其类型不能被常量声明接受时（类/自定义结构体）——静态只读字段
	• 字段
		○ 字段定义
			§ 字段（field）是一种表示与对象或类型（类与结构体）关联的变量
			§ 字段是类型的成员，旧称“成员变量”
			§ 与对象关联的字段亦称“实例字段”
				□ 实例字段帮助实例（对象）来存储数据，隶属于某一个实例
				□ 实例字段的组合用来表示对象当前的状态
			§ 与类型关联的字段称为“静态字段”，由static修饰
				□ 静态字段帮助某一个数据类型来存储数据，隶属于某一个数据类型
				□ 静态字段的组合表示的是某个数据类型的当前状态
		○ 声明字段
			§ 参考C#语言定义文档 
			§ 尽管字段声明带有分号，但它不是语句
			§ 字段的名字一定是名词
				□ 一般情况下字段命名采用首个单词首字母小写或者首个单词首字母大写并在前加一个_
			§ 声明字段的形式：
				□ 其一：访问修饰符 可选有效的修饰符组合 数据类型 字段名;
				□ 其二：访问修饰符 可选有效的修饰符组合 数据类型 字段名 = 初始化表达式或值;
				□ 注意：一般情况下 字段 属于内部数据不应该直接暴露 所以访问修饰符应指定为 private
		○ 字段的初始值
			§ 无显式初始化时，字段获得其类型的默认值，所以字段“永远都不会未被初始化”
			§ 实例字段初始化的时机——对象创建时
			§ 静态字段初始化的时机——类型被加载（load）时（此时会调用一次类型的静态构造函数且只调用一次）
		○ 只读字段：声明时使用readonly关键字修饰的字段
			§ 实例只读字段
			§ 静态只读字段
			§ 只读字段只能在声明时或者构造函数中赋值，一旦赋值后不支持改变值
	• 属性
		○ 属性定义
			§ 参考C#语言定义文档 
			§ 属性（property）是一种用于访问对象或类型的特征的成员，特征反映了状态
			§ 属性提供灵活的机制来读取、写入或计算私有字段的值
			§ 属性是字段的自然扩展
				□ 从命名上看，field更偏向于实例对象在内存中的布局，property更偏向于反映现实世界对象的特征
				□ 对外：暴露数据，数据可以是存储在字段里的，也可以是动态计算出来的
				□ 对内：保护字段不被非法值污染
			§ 属性由Get/Set方法对进化而来
			§ 又一个“语法糖”——属性背后的秘密
		○ 属性的声明
			§ 完整声明——后台（back）成员变量与访问器
				□ 示例
				class PropertySample
				{
					private type filed;//字段，成员变量
					
					public type Property//属性，一般情况下属性名称和其封装的字段名称一致但首字母大写
					{
						get
						{
							return filed;
						}
						set
						{
							filed = value;
						}
					}
				}
			§ 动态计算值的属性
				□ get和set访问器的取值赋值的对象不是固定隐藏的某一个字段，而是通过多个字段或其他属性进行计算
			§ 注意实例属性和静态属性
				□ 实例属性无static关键字修饰，静态属性有static关键字进行修饰
				□ 实例属性可以包含静态成员和实例成员，静态属性只能包含静态成员
				□ 实例属性属于实例，静态属性属于类
			§ 属性的名字一定是名词
				□ 属性的名字一般采用Pascal命名法，即组成名字的每个单词的首字母都采取大写
				□ 如果一个属性是对一个已声明字段的封装，其名字应和包容字段相同，区别为首个单词首字母大写而字段不是
			§ 必需的属性
				□ 从C#11开始，可以添加required成员以强制客户端代码初始化任何属性或字段
				□ 示例
					® public required type Property{ get; set; }
			§ 只读属性
				□ 只读属性指仅含get访问器不含set访问器的属性		            
				□ 尽管语法上正确，几乎没有人写“只写属性”，因为属性的主要目的就是通过向外暴露数据二表示对象/类型的状态
			§ 自动属性
				□ 当属性的get和set访问器中不需要包含任何其他逻辑时，自动实现的属性会使属性声明更加简洁
				□ 自动属性声明——只有访问器【Get/Set访问器】
					® 示例
					class PropertySample
					{
						public type Property{get; set;}//只有get，set访问器
					}
				□ 自动实现的只读属性（C# 6.0引入）
					® 声明时只包含一个未实现的get访问器的属性
					® 编译器会自动实现其只读属性特性，会自动为其生成一个私有隐藏字段
					® 自动实现的只读属性可直接使用=进行初始化赋值，或者只能在构造函数中进行初始化
					® 自动实现的只读属性可以不写访问器，可使用表达式体属性初始化器（=>），编译器会自动实现get访问器
			§ 属性与字段的关系
				□ 一般情况下，它们都用于表示实体（对象或类型）的状态
				□ 属性大多数情况下是字段的封装器（wrapper）
				□ 建议：永远使用属性而不是字段来暴露数据，即字段永远都是private或protected的
	• 方法
		○ 常规方法
			§ 方法定义
				□ 方法是包含一系列语句的代码块
			§ 关于方法更多的信息
				□ 参见方法部分详述
		○ 构造函数
			§ 构造函数（constructor）是类型的成员之一
				□ 构造函数也称构造器，是一种特殊的方法
				□ 狭义的构造函数指的是“实例构造函数”（instance constructor）
			§ 调用构造函数
				□ 调用实例构造函数：new 类名(实际参数)
				□ 调用静态构造函数：运行时会在创建第一个实例或引用任何静态成员之前自动调用静态构造函数
					® 由上可知静态构造函数不能直接调用，且用户无法控制其被调用的时间
			§ 声明构造函数
				□ 声明实例构造函数：访问修饰符  类名（形式参数）
					® 一个类可以有零个或多个显示实例构造函数，只要签名不同即可，但必须至少有一个public构造函数
				□ 声明静态构造函数：static 类名()
					® 一个类只能有一个静态构造函数(可以显示声明和实现，也可不声明由编译器自动实现)，静态构造函数不能继承或重载
					® 静态构造函数不能使用访问修饰符和不具有参数
			§ 构造函数的内存原理
				□ 首先，现在stack上分配一个4字节的空间存储引用变量
				□ 然后系统计算实例所需要的内存空间，并在heap上分配实例所需的空间
				□ 如果构造函数带参数，就在分配的内存空间中存入相应的数据
				□ 把实例在heap上的起点地值存储到引用变量的储存空间
					® Ps:如果参数列表中含有引用类型的参数，那么久在实例分配的内存空间中再申请一块4字节的空间存储引用参数的实例地址，然后在heap中再找一块区域存放引用参数的实例
			§ 静态构造函数用法
				□ 静态构造函数的一种典型用法是在类使用日志文件且将构造函数用于将条目写入到此文件中时使用。
				□ 静态构造函数对于创建非托管代码的封装类也非常有用，这种情况下构造函数可调用 LoadLibrary 方法。
				□ 也可在静态构造函数中轻松地对无法在编译时通过类型参数约束检查的类型参数强制执行运行时检查。
			§ 构造函数链：使用this关键字调用另一个构造函数
				□ 如果一个类有多个构造函数，且每个构造函数在构造类过程中使用了很多重复代码，为了能对这些重复代码提高复用性和可读可维护性，我们可以在一个构造函数中调用另一个构造函数，这称为 构造函数链 ，用 构造函数初始化器 实现
				□ 构造函数初始化器会在执行当前构造函数之前，判断要调用另外哪一个构造函数
				□ 针对相同对象实例，为了从一个构造函数中调用另一个构造函数，C#语法在一个构造函数声明后添加冒号再添加this关键字，再添加被调用构造函数的参数列表
				□ 通常采取的构造函数链构建方式为：参数最少的构造函数调用参数最多的构造函数，为未知参数传递默认值
				□ 如果多个构造函数中存在大量重复代码，但参数列表无关联性，那么可以考虑将重复代码提取为一个初始化方法，然后再在每个构造函数中调用该方法即可
		○ 析构函数
			§ 关于析构函数
				□ 析构函数也称为终结器
				□ 析构函数用于在垃圾回收器收集类实例时执行任何必要的最终清理操作
				□ 在大多数情况下，通过使用System.Runtime.InteropServices.SafeHandle或派生类包装任何非托管句柄，可以免去编写终结器的过程
			§ 析构函数的声明
				□ 形式：
					® class 类名
					{
						~类名()
						{
							进行清理操作的语句块
						}
					}
			§ 注意事项
				□ 无法在结构中定义析构函数
				□ 一个类只能由一个析构函数
				□ 不能继承或重载析构函数
				□ 不能手动调用析构函数，由运行时自动调用
				□ 析构函数不使用修饰符或参数
		○ 解构函数
			§ 解构函数
				□ C#7.0开始，可以声明一个解构函数把一个封装好的对象拆分为它的各个组成部分
				□ 可以将对象实例赋值给一个元组，从而隐式调用解构函数
					® 只允许用元组语法向那些和out参数匹配的变量赋值
					® 不允许向元组类型的变量赋值
			§ 解构函数的声明形式
				□ void Deconstruct(out 参数列表)
				{
					out参数列表组成的元组 = 对应属性组成的元组;
				}
		○ 运算符
			§ 运算符，也称为操作符
			§ 运算符是用来操作数据的，被运算符操作的数据称为操作数(Operand)
				□ 操作数可以是文本(字面量)、字段、局部变量和表达式
			§ 重载运算符是一种特殊的方法，用于使类型支持简单的数据操作
				□ 比如定义一个类与另一个类的隐式或显示转换
			§ 运算符详解参考 运算符 部分
	• 索引器
		○ 索引器允许类或结构的实例就像数组一样进行索引
			§ 无需显示指定类型或实例成员即可设置或检索索引值
			§ 索引器类似于属性，不同之处在于它们的访问器需要使用参数
	• 事件
		○ 关于事件
			§ 类或对象可以通过事件向其他类或对象通知发生的相关事情
				□ 发送或引发事件的类称为“发布者”
				□ 接收或处理事件的类称为“订阅者”
		○ 事件详解参考 事件 部分
	• 静态成员
		○ 使用static关键字修饰的类成员
	• 嵌套类型
		○ 在类、结构或接口中定义的类型称为嵌套类型
			§ 不论外部类型是类、结构或接口，嵌套类型均默认为private
			§ 嵌套类仅可从其包含类型进行访问
			§ 嵌套类可访问包含其类型的静态成员，但不能访问包含其类型的实例成员
			§ 对于嵌套类和包含其类型之外的类来说，嵌套类的外部类有些类似一个命名空间的作用
			§ 嵌套类也可以使用public、protect、internal、protect internal、private internal等修饰
				□ 但是非必要不要这么做，编程不应该增加代码的复杂性
				□ 使嵌套类在外部可见违反了C#代码质量规则CA1034: 嵌套类型不应是可见的
			§ 嵌套类型的包含类型也不能直接访问嵌套类的实例成员
				□ 如果嵌套类极其包含类型如果需要访问彼此的实例成员，可使用如下所示形式：
				public class Container //嵌套类的外部包含类
				{
					private Nested inner;//声明一个嵌套类类型的字段，后续通过该字段访问嵌套类实例成员
		
					public Container()
					{
						inner = new Nested(this);
					}
		
					private class Nested //嵌套类
					{
						private Container parent; //声明一个包含类类型的字段
		
						public Nested() { }
		
						public Nested( Container parent )
						{
							this.parent = parent;//通过构造函数将包含类的实例引用给嵌套类对应字段
						}
					}
				}
	• unsafe成员
		○ 具有非托管类型或代码的成员

 * 类的实例化与成员访问
	• 类的实例化形式
		○ 形式一：类名 实例名 = new 类名(实参);
		○ 形式二：类名 实例名 = new (实参);
		○ 形式三：类名 实例名 = new 类名(实参){对象初始化器} 
		○ 形式四：类名 实例名 = new (实参){对象初始化器} 
		○ 说明：
			§ 实参根据定义的构造函数来确定有无和数量
			§ 对象初始化器即{}内对属性进行初始化赋值操作，每一个赋值操作使用“,”分隔
	• 类的实例成员访问：
		○ 非索引器：实例名.成员名
		○ 索引器：实例名[索引]
	• 类的静态成员和常量访问
		○ 常量：类名.成员名
		○ 静态成员：类名.常量名

 * 类的三大成员
	• 属性（Property）
		○ 存储数据，组合起来表示类或对象的当前状态
	• 方法（Method）
		○ 由C语言中的函数（function）进化而来，表示类或对象“能做什么”
		○ 工作中90%的时间是在与方法打交道，因为它是“真正做事”、“构成逻辑”的成员
	• 事件（Event）
		○ 类或对象通知其它类或对象的机制，为C#所特有
		○ 善用事件机制非常重要
*/
using LearnCSharp.Basic.LearnClassSpace;
using System.Runtime.CompilerServices;
using System.Drawing;

namespace LearnCSharp.Basic.LearnClassSpace
{
	//定义一个名为Book的类，用它来表示现实生活中的书籍
	//原则上我们应该保证类名和代码所属文件名相同，且一个文件最好只包含一个类
	//这里由于便于学习，先不考虑编程规范问题，本部分会在一个文件内定义多个类且类名和文件名未完全保持一致
	public class NovelBook
	{
		#region 学习字段
		/*【学习字段】
			• 字段
				○ 字段（field）是一种表示与对象或类型（类与结构体）关联的变量
				○ 字段是类型的成员，旧称“成员变量”
				○ 与对象关联的字段亦称“实例字段”
					§ 实例字段帮助实例（对象）来存储数据，隶属于某一个实例
					§ 实例字段的组合用来表示对象当前的状态
				○ 与类型关联的字段称为“静态字段”，由static修饰
					§ 静态字段帮助某一个数据类型来存储数据，隶属于某一个数据类型
					§ 静态字段的组合表示的是某个数据类型的当前状态
			• 声明字段
				○ 参考C#语言定义文档 
				○ 尽管字段声明带有分号，但它不是语句
				○ 字段的名字一定是名词
					§ 一般情况下字段命名采用首个单词首字母小写或者首个单词首字母大写并在前加一个_
				○ 声明字段的形式：
					§ 其一：访问修饰符 可选有效的修饰符组合 数据类型 字段名;
					§ 其二：访问修饰符 可选有效的修饰符组合 数据类型 字段名 = 显示初始化表达式或值;
					§ 注意：一般情况下 字段 属于内部数据不应该直接暴露 所以访问修饰符应指定为 private
			• 字段的初始值
				○ 无显式初始化时，字段获得其类型的默认值，所以字段“永远都不会未被初始化”
				○ 实例字段初始化的时机——对象创建时
				○ 静态字段初始化的时机——类型被加载（load）时（此时会调用一次类型的静态构造函数且只调用一次）
			• 只读字段：声明时使用readonly关键字修饰的字段
				○ 实例只读字段
				○ 静态只读字段
				○ 只读字段只能在声明时或者构造函数中赋值，一旦赋值后不支持改变值
		*/
		private string? bookName; //声明一个实例字段，字符串类型，表示书名，该字段属于Book类的实例
		private string? author; //声明一个实例字段，字符串类型，表示作者，该字段属于Book类的实例
		private Color coverColor; //声明一个实例字段，Color类型，表示封面颜色，该字段属于Book类的实例
		private string? coverImage; //声明一个实例字段，字符串类型，表示封面图片路径，该字段属于Book类的实例
		private DateTime publishTime;//声明一个只读DateTime字段，用于表示书籍出版时间，该字段属于Book类的实例
		private readonly Guid bookID;//声明一个只读Guid字段，用于表示书籍唯一代码，该字段属于Book类的实例
		private readonly DateTime createTime;//声明一个只读DateTime字段，用于表示书籍创建时间，该字段属于Book类的实例
		private static uint bookCounts;//声明一个静态字段，字符串类型，表示书籍总数，该字段属于Book类的静态
		private static readonly string[] bookMakerVersion = new[] { "NM", "0", "0", "1" };
		#endregion

		#region 学习属性
		/*【学习属性】
			• 属性
				○ 参考C#语言定义文档 
				○ 属性（property）是一种用于访问对象或类型的特征的成员，特征反映了状态
				○ 属性是字段的自然扩展
					§ 从命名上看，field更偏向于实例对象在内存中的布局，property更偏向于反映现实世界对象的特征
					§ 对外：暴露数据，数据可以是存储在字段里的，也可以是动态计算出来的
					§ 对内：保护字段不被非法值污染
				○ 属性由Get/Set方法对进化而来
				○ 又一个“语法糖”——属性背后的秘密
			• 属性的声明
				○ 完整声明——后台（back）成员变量与访问器
					§ 示例
					class PropertySample
					{
						private type filed;//字段，成员变量
			
				        public type Property//属性，一般情况下属性名称和其封装的字段名称一致但首字母大写
				        {
			                get
			                {
			                        return filed;
			                }
			                set
			                {
			                        filed = value;
			                }
					    {
					}
				○ 动态计算值的属性
					§ get和set访问器的取值赋值的对象不是固定隐藏的某一个字段，而是通过多个字段或其他属性进行计算
				○ 注意实例属性和静态属性
					§ 实例属性无static关键字修饰，静态属性有static关键字进行修饰
					§ 实例属性可以包含静态成员和实例成员，静态属性只能包含静态成员
					§ 实例属性属于实例，静态属性属于类
				○ 属性的名字一定是名词
					§ 属性的名字一般采用Pascal命名法，即组成名字的每个单词的首字母都采取大写
					§ 如果一个属性是对一个已声明字段的封装，其名字应和包容字段相同，区别为首个单词首字母大写而字段不是
				○ 只读属性
					§ 只读属性指仅含get访问器不含set访问器的属性		            
					§ 尽管语法上正确，几乎没有人写“只写属性”，因为属性的主要目的就是通过向外暴露数据二表示对象/类型的状态
				○ 自动属性
					§ 当属性的get和set访问器中不需要包含任何其他逻辑时，自动实现的属性会使属性声明更加简洁
						 - 自动属性声明——只有访问器【Get/Set访问器】
						   示例
						   class PropertySample
						   {
								public type Property{get; set;}//只有get，set访问器
						   }
					§ 自动实现的只读属性（C# 6.0引入）
						- 声明时只包含一个未实现的get访问器的属性
						- 编译器会自动实现其只读属性特性，会自动为其生成一个私有隐藏字段
						- 自动实现的只读属性可直接使用=进行初始化赋值，或者只能在构造函数中进行初始化
			• 属性与字段的关系
				○ 一般情况下，它们都用于表示实体（对象或类型）的状态
				○ 属性大多数情况下是字段的封装器（wrapper）
				○ 建议：永远使用属性而不是字段来暴露数据，即字段永远都是private或protected的
		 */

		//声明并定义一个自动实现只读属性，表示书籍编号
		public string? BookCode => $"{Author!.Substring(0,1)}-{BookName!.Substring(0,1)}-{CreateTime.Year}";

        //完整声明并定义一个实例属性，用于隐藏并封装bookName字段，表示书名
        public string? BookName
		{
			get => bookName;
			set => bookName = value;
		}

		//完整声明并定义一个实例属性，用于隐藏并封装author字段，表示作者
		public string? Author
		{
			get => author;
			set => author = value;
		}

		//完整声明并定义一个实例属性，用于隐藏并封装coverColor字段，表示封面颜色
		public Color CoverColor
		{
			get => coverColor;
			set => coverColor = value;
		}

		//完整声明并定义一个实例属性，用于隐藏并封装coverImage字段，表示封面图片路径
		public string? CoverImage
		{
			get => coverImage;
			set => coverImage = value;
		}

		//完整声明并定义一个只读实例属性，用于隐藏并封装bookID字段，表示书籍唯一代码
		public Guid BookID
		{
			get => bookID;
			init => bookID = value;
		}

		//完整声明并定义一个只读实例属性，用于隐藏并封装CreateTime字段，表示书籍创建时间
		public DateTime CreateTime
		{
			get => createTime;
			init => createTime = value;
		}

		//完整声明并定义一个只读实例属性，用于隐藏并封装publishTime字段，表示书籍出版时间
		public DateTime PublishTime
		{
			get => publishTime;
			set => publishTime = value;
		}

		//完整声明并定义一个静态属性，用于隐藏并封装bookCounts字段，表示书籍总数
		public static uint BookCounts
		{
			get => bookCounts;
			set => bookCounts = value;
		}

		//自动实现的只读静态属性，表示书籍制作器版本
		public static string[] BookMakerVersion => bookMakerVersion;
        #endregion

        #region 学习常量
        /*【学习常量】
			• 常量
				○ 参考C#语言定义文档 
				○ 常量（constant）是表示常量值（即可以在编译时计算的值）的类成员
				○ 常量隶属于类型而不是对象（实例），即没有“实例常量”
					§ “实例常量”的角色由只读实例字段来担当
				○ 注意区分成员常量与局部常量
				○ 常量在程序的生命周期内不会改变
				○ 仅C#内置类型（不包括System.Object)可以声明为常量
				○ 用户定义的类型（包括类、结构和数组）不能为常量
				○ C#不支持const方法、属性或事件
			• 常量的声明
				○ 形式：访问修饰符 const 数据类型 常量名 = 常量值
				○ 常量一但声明就必须初始化赋值，且不可改变
			• 各种“只读”的应用场景
				○ 为了提高程序可读性和执行效率——常量
				○ 为了防止对象的值被改变——只读字段
				○ 向外暴露不允许修改的数据——只读属性（静态或非静态），功能与常量有一些重叠
				○ 当希望成为常量的值其类型不能被常量声明接受时（类/自定义结构体）——静态只读字段
		 */
        //声明一个常量，表示书籍制作器名称
        public const string BookMaker = "NovelMaker";
        #endregion

        #region 学习构造函数
        /*【学习构造函数】
			• 构造函数（constructor）是类型的成员之一
			• 构造函数也称构造器，是一种特殊的方法
			• 狭义的构造函数指的是“实例构造函数”（instance constructor）
			• 调用构造函数
				○ 调用实例构造函数：new 类名(实际参数)
				○ 调用静态构造函数：运行时会在创建第一个实例或引用任何静态成员之前自动调用静态构造函数
								   由上可知静态构造函数不能直接调用，且用户无法控制其被调用的时间
			• 声明构造函数
				○ 声明实例构造函数：访问修饰符  类名（形式参数）
					○ 一个类可以有零个或多个显示实例构造函数，只要签名不同即可，但必须至少有一个public构造函数
		        ○ 声明静态构造函数：static 类名()
					○ 一个类只能有一个静态构造函数(可以显示声明和实现，也可不声明由编译器自动实现)，静态构造函数不能继承或重载
					○ 静态构造函数不能使用访问修饰符和不具有参数
			• 构造函数的内存原理
				○ 首先，现在stack上分配一个4字节的空间存储引用变量
				○ 然后系统计算实例所需要的内存空间，并在heap上分配实例所需的空间
				○ 如果构造函数带参数，就在分配的内存空间中存入相应的数据
				○ 把实例在heap上的起点地值存储到引用变量的储存空间
				○ Ps:如果参数列表中含有引用类型的参数，那么久在实例分配的内存空间中再申请一块4字节的空间存储
				      引用参数的实例地址，然后在heap中再找一块区域存放引用参数的实例
			• 静态构造函数用法
				○ 静态构造函数的一种典型用法是在类使用日志文件且将构造函数用于将条目写入到此文件中时使用。
				○ 静态构造函数对于创建非托管代码的封装类也非常有用，这种情况下构造函数可调用 LoadLibrary 方法。
				○ 也可在静态构造函数中轻松地对无法在编译时通过类型参数约束检查的类型参数强制执行运行时检查。
			构造函数链：使用this关键字调用另一个构造函数
				○ 如果一个类有多个构造函数，且每个构造函数在构造类过程中使用了很多重复代码，
				   为了能对这些重复代码提高复用性和可读可维护性，我们可以在一个构造函数中调用另一个构造函数，
				   这称为 构造函数链 ，用 构造函数初始化器 实现
				○ 构造函数初始化器会在执行当前构造函数之前，判断要调用另外哪一个构造函数
				○ 针对相同对象实例，为了从一个构造函数中调用另一个构造函数，C#语法在一个构造函数声明后添加冒号再
				   添加this关键字，再添加被调用构造函数的参数列表
				○ 通常采取的构造函数链构建方式为：参数最少的构造函数调用参数最多的构造函数，为未知参数传递默认值
				○ 如果多个构造函数中存在大量重复代码，但参数列表无关联性，那么可以考虑将重复代码提取为一个初始化方法，
				   然后再在每个构造函数中调用该方法即可

		 */
        /*显示声明并实现一个静态构造函数
        static NovelBook()
		{
			 
		}*/

        //声明一个private的默认构造函数用于阻止通过默认构造函数初始化本类
        private NovelBook() { }

		//声明一系列有参构造函数，并构建一个构造函数链，可通过这些构造函数初始化本类		
		public NovelBook(string bookName,string author,string coverImage,Color converColor,DateTime publishTime)
		{
			//在类的内部，我们可以使用this关键字来访问类的实例成员
			//这是为了避免歧义，表名这个成员是属于实例的
			this.BookName = bookName;
            //通常使用属性来对字段进行初始化赋值，而不是直接对字段进行赋值
            //这是为了保证字段的封装性，即外部无法直接访问字段，只能通过属性来访问
            //构造函数中的参数名和属性名不同时，可以省略this关键字
            Author = author;
            CoverImage = coverImage;
            CoverColor = converColor;
            PublishTime = publishTime;

            //初始化只读字段
            BookID = Guid.NewGuid();
            CreateTime = DateTime.Now.Date;
        }
        public NovelBook(string bookName, string author, Color converColor) : this(bookName, author, string.Empty, converColor, new DateTime(2100,12,31))
        {
        }
		public NovelBook(string bookName, string author) : this(bookName, author, Color.LemonChiffon)
        {
        }

        //声明一个复制构造函数，即参数本身为这个类的实例，以便创建一个新实例时，将实参实例的属性字段复制到新实例
        public NovelBook(NovelBook novelBook)
		{
			this.BookName = novelBook.BookName;
            this.Author = novelBook.Author;
            this.CoverImage = novelBook.CoverImage;
            this.CoverColor = novelBook.CoverColor;
            this.PublishTime = novelBook.PublishTime;
            this.BookID = novelBook.BookID;
            this.CreateTime = novelBook.CreateTime;
        }
		#endregion

		#region 声明并定义一些方法，方法的知识见【015 LearnMethod】
		public void ShowBook()
		{
			
			Console.WriteLine(
				$"小说编号：[{BookCode}]\n" +
				$"小说名称：[{BookName}]\n" +
				$"小说作者：[{Author}]\n" +
				$"小说唯一编码：[{BookID}]\n" +
                $"小说封面：[{CoverImage}]\n" +
                $"小说封面底色：[{CoverColor}]\n" +
                $"小说创建时间：[{CreateTime.Date}]\n" +
                $"小说发布时间：[{PublishTime.Date}]\n" +
				$"创建软件：[{BookMaker}]\n" +
				$"创建软件版本：[{BookMakerVersion[0]}.{BookMakerVersion[1]}.{BookMakerVersion[2]}.{BookMakerVersion[3]}]]\n");
		}
        #endregion

        #region 解构函数 C#7.0引入
        public void Deconstruct(out string bookName, out string author, out string bookID)
		{
			(bookName, author, bookID) = (this.BookName, this.Author, this.BookID.ToString());
		}
        #endregion

		

        #region 学习嵌套类
        /*【学习嵌套类】
			• 在类、结构或接口中定义的类型称为嵌套类型
			• 不论外部类型是类、结构或接口，嵌套类型均默认为private
			• 嵌套类仅可从其包含类型进行访问
			• 嵌套类可访问包含其类型的静态成员，但不能访问包含其类型的实例成员
			• 对与嵌套类和包含其类型之外的类来说，嵌套类的外部类有些类似一个命名空间的作用
			• 嵌套类也可以使用public、protect、internal、protect internal、private internal等修饰
			  但是非必要不要这么做，编程不应该增加代码的复杂性
			• 使嵌套类在外部可见违反了C#代码质量规则CA1034: 嵌套类型不应是可见的
			• 嵌套类型的包含类型也不能直接访问嵌套类的实例成员
			• 如果嵌套类极其包含类型如果需要访问彼此的实例成员，可使用如下所示形式：
					public class Container //嵌套类的外部包含类
					{
						private Nested inner;//声明一个嵌套类类型的字段，后续通过该字段访问嵌套类实例成员

						public Container()
						{
						inner = new Nested(this);
						}

						private class Nested //嵌套类
						{
							private Container parent; //声明一个包含类类型的字段

							public Nested() { }

							public Nested( Container parent )
							{
								this.parent = parent;//通过构造函数将包含类的实例引用给嵌套类对应字段
							}
						}
					}
		 */
        #endregion
    }
}

namespace LearnCSharp.Basic
{
    /*【学习静态类】
	 * 静态类定义时需要使用static关键字修饰，一般如数学工具类等类型可以考虑设计成静态类
	 * 静态类即所有成员都是静态成员的类，使用该类时不能显示初始化，直接通过类名使用静态成员即可。
	 * 本章之前的所有类实际都可以定义为静态类，因为它们的成员都不是实例成员，而全是静态成员
	 */
    public static class LearnClass
    {
		public static void StartLearnClass()
		{
			//我们使用不同的构造函数和数据初始化出5个NovelBook实例
			NovelBook novelBook1 = new NovelBook("鬼吹灯", "天下霸唱");
			NovelBook novelBook2 = new NovelBook("鬼吹灯", "天下霸唱", Color.Red);
			NovelBook novelBook3 = new NovelBook(novelBook2);
			NovelBook novelBook4 = new NovelBook("鬼吹灯", "天下霸唱", "./cover.jpg", Color.Red, DateTime.Now);
			NovelBook novelBook5 = novelBook4;

			Console.WriteLine("已声明并初始化5个NovelBook类的实例，接下来显示实例信息：");
			PrintNovelInfo(novelBook1);
			PrintNovelInfo(novelBook2);
			PrintNovelInfo(novelBook3);
			PrintNovelInfo(novelBook4);
			PrintNovelInfo(novelBook5);
			Console.WriteLine();

			//novelBook1.BookCode="123";//尝试通过实例改变只读属性--报错
			//novelBook1.BookID = new Guid();//尝试为使用init访问器的属性赋值--报错
			//NovelBook.BookMaker= "新的制作器";//尝试为常量赋值--报错

			novelBook1.BookName = "迷踪之国";//可以通过实例单独更改其可读写属性，同时也会更改其封装字段
			NovelBook.BookMakerVersion[1] = "1";//可以通过集合类型的只读属性去更改封装的只读集合字段的元素，但不能为封装字段重新初始化一个新集合

			Console.WriteLine("通过修改可读写成员数据输出如下：");
			novelBook1.ShowBook();//查看更改后的信息

			Console.WriteLine();

			//类是一种引用类型,所以实例novelBook4和实例novelBook5指向的是同一对象
			Console.WriteLine($"snovelBook4和novelBook5指向同一引用对象：{object.ReferenceEquals(novelBook4, novelBook5)}\n");
            
			//使用以下代码修改novelBook5的可读写属性值，然后查看novelBook4实例的输出"
            novelBook5.BookName = "迷踪之国";
            novelBook5.PublishTime = new DateTime(2009,5,1);
            novelBook4.ShowBook();

            Console.WriteLine();

            /*基于以上情形，当我们需要复制一个实例而不修改原实例的各项数据时
              我们可以新建一个全新实例，然后重新将原实例的数据成员赋值给新实例
              又或者我们在创建类的时候，可以创建一个参数为类型本身的“复制”构造函数，然后我们通过将原实例作为参数传入构造函数来创建新实例
              后者的优势是在于，对于只读属性，后者也能进行“复制”。*/

            //实例novelBook2和实例novelBook3按照现实中的意义实际也是表示的同一对象
            //它们数据相同，但是在程序中又是不同的实例，因为实例students3是使用其复制构造函数将实例student2作为参数传入来构建的
            Console.WriteLine($"novelBook2和novelBook3指向同一引用对象：{object.ReferenceEquals(novelBook2, novelBook3)}\n");
            
			//这样的好处是在需要的时候，我们可以去修改一个实例的数据进行操作而不影响原实例，具体可根据编程时的实际需求来决定如何使用

            novelBook3.BookName = "盗墓笔记";
            novelBook3.Author = "南派三叔";
            PrintNovelInfo(novelBook3);
            PrintNovelInfo(novelBook2);


        }

        private static void PrintNovelInfo(NovelBook novelBook, [CallerArgumentExpression("novelBook")] string? argumentName = null)
        {
            Console.WriteLine($"NovelBook类\n" +
				$"实例：[{argumentName}]\n" +
				$"信息：");
			novelBook.ShowBook();
        }
    }
}
