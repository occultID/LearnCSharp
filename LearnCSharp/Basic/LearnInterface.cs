﻿/*【学习接口】
 * 接口
	• 接口定义协定
		○ 实现接口的类或结构必须遵循接口的协定
			§ 接口实现关系是一种“能做”关系
				□ 类型或结构“能做”接口所规定的事情
			§ 在“实现接口的类型”和“使用接口的代码”之间，接口订立了“契约”
		○ 接口可以从多个基接口继承
		○ 类或者结构可以实现多个接口
		○ 接口能完全隔离实现细节和提供的服务
	• 接口可以包含方法、属性、事件和索引器
		○ 在C#8.0之前接口本身不提供它定义的成员的实现
		○ 在C#8.0之前接口仅指定实现接口的类或结构必须提供的成员
		○ 从C#8.0开始，接口可以提供默认实现
			§ 原因：兼容旧代码的前提下为接口引入新成员
		○ 从C#11.0开始，接口可以定义static abstract或static virtual成员来声明实现类型必须提供声明的成员
			§ 通常，static virtual方法声明是用于定义一组重载运算符

 * 接口的声明与定义
	• 接口需要使用interface关键字来声明
	• 接口的声明形式
		○ [特性]
		访问级别 有效的接口修饰组合 partial interface 接口名<泛型参数列表>:基接口 泛型参数约束 { 接口体 }
			§ [特性]：该项是可选配置，标识接口是否被附加特性
			§ 访问级别：该项是可选配置，标识接口的访问级别
				□ public：公开访问级别
				□ protected：受保护的访问级别
				□ internal：内部访问级别
				□ private：私有访问级别
			§ 有效的接口访问修饰符组合：该项为可选配置
				□ unsafe：标识接口中会使用到非托管类型或代码
				□ new：仅允许在类中定义的嵌套接口上使用new修饰符，标识接口中有成员将隐藏基接口成员
			§ partial：该项为可选配置，标识接口的定义分开为多个部分来定义
			§ interface：该项为必选配置，这是声明定义接口的必须关键字
			§ 接口名：该项为必选配置，标识接口的名称
				□ 接口名通常使用Pascal命名方式
				□ 接口名通常使用大写字母“I”开头加一个概述接口功能的词汇组成
			§ <变体泛型参数列表>：该项为可选配置，标识接口是否采用了泛型实现
				□ <>中为泛型参数列表，即表示数据类型的形式参数列表
				□ 泛型参数如果有多个，每一个参数使用“,”分隔
				□ 变体指每个泛型参数可选支持协变(out)或逆变(in)修饰
			§ :基接口：该项为可选配置，标识接口是否继承于一个或多个基接口
				□ “:”标识后面跟随需要继承的基接口
				□ 如果继承多个基接口，每一个基接口使用“,”分隔
			§ 泛型参数约束：该项为可选配置，标识对泛型参数列表的约束
				□ 该项使用的前提是接口使用了泛型参数列表
				□ 即使接口使用了泛型参数列表，该项也是可选的
			§ {接口体}：该项为必选配置，即标识接口的成员
				□ 接口体即接口成员

 * 接口成员详解
	• 接口声明可以声明零个或多个成员
	• 接口成员
		○ 属性
			§ 属性声明的特性、类型与类中的属性具有相同含义
			§ 接口不支持字段，所以不支持属性进行封装字段的默认实现
			§ 接口不支持自动实现的只读属性
			§ 接口属性可以使用abstract修饰，此时不能显示实现get访问器和set访问器
		○ 方法
			§ 方法声明的特性、类型和参数列表等与类中的方法具有相同含义
			§ 接口方法可以有默认实现
			§ 接口方法可以使用virtual或abstract修饰，使用abstract修饰时不能提供默认实现
			§ 接口方法可以使用sealed修饰，此时方法必须提供默认实现且不能被类或接口重写
			§ 接口方法可以使用extern修饰，此时方法是一个外部方法，不能提供实现
		○ 索引器
			§ 索引器声明的特性、类型和索引参数列表与类中的索引器具有相同含义
			§ 索引器可以有默认实现
			§ 索引器如果存在get访问器，则接口索引器的类型必须是输出安全的
			§ 索引器如果存在set访问器，则接口索引器的类型必须是输入安全的
		○ 事件
			§ 事件声明的特性、类型和标识符与类中的事件声明具有相同含义
			§ 事件可以有默认实现
			§ 接口事件的类型必须是输入安全类型
		○ 常量
			§ 常量声明的类型、标识符和赋值等与类中的常量声明具有相同含义
		○ 运算符
			§ 接口中可以定义重载运算符
		○ 嵌套类型
			§ 接口中可以声明嵌套类型，其余类中的嵌套类声明具有相同意义
		○ 静态成员
			§ 接口中可以声明静态构造函数、字段、方法、属性、索引器和事件
			§ 接口中可以声明作为程序入口的静态Main方法
			§ 接口中静态方法可以使用extern修饰
			§ 从C#11.0开始，接口可以定义static abstract或static virtual成员来声明实现类型必须提供声明的成员
				□ 通常，static virtual方法声明是用于定义一组重载运算符
		○ unsafe成员
			§ 非托管类型成员或含非托管代码的成员
	• 默认接口成员
		○ 接口成员可以声明主体
			§ 接口中的成员主体是其默认实现
		○ 具有默认实现的成员允许接口为不提供重写实现的类和结构提供“默认”实现
	• 接口成员不能是以下成员
		○ 实例字段
	• 接口升级
		○ C#8.0之前接口成员只能是属性、方法、索引器和事件，且不能提供默认实现
		○ C#8.0开始接口成员只要求不能是实例字段、实例构造函数和析构函数
	• 接口成员访问
		○ 对于显示实现的接口，只能将实现类或结构的实例赋值给接口的变量后，再通过接口实例来访问对应成员
		○ 对于隐式实现的接口，可以通过实现类或结构的实例来访问其成员，也能通过将实例赋值给接口变量再通过接口实例来访问对应成员

 * 接口的继承与实现
	• 接口继承
		○ 接口可以从零个或多个接口类型继承
		○ 这些接口类型称为接口的显示基接口
		○ 接口的基接口必须至少具有与接口本身相同的访问级别
	• 接口的实现
		○ 实现接口的类或结构在其内部必须实现接口协定的所有需实现成员
			§ 如果接口本身有继承其他基接口，那么实现类或接口中也必须实现基接口协定的所有需实现成员
		○ 接口实现有以下两种方案
			§ 显示实现
				□ 类或结构可以声明显示接口成员实现
				□ 显示接口成员实现指在实现类或结构中采用完全限定的接口成员名称来进行实现
				□ 显示接口成员实现的前提下，只能将类或结构的实例赋值给基接口的变量并通过接口实例来访问接口成员
				□ 显示接口成员实现不能使用访问级别修饰
			§ 隐式实现
				□ 类或结构可以声明隐式接口成员实现
				□ 隐式接口成员实现指在实现类或结构中使用与接口协定成员同签名的成员来提供实现
				□ 隐式接口成员实现的前提下，可以通过实现类或结构的实例来访问接口成员，也可以将类或结构的实例赋值给基接口的变量并通过接口实例来访问接口成员
		○ 接口映射
			§ 类或结构必须提供类或结构的基类列表中列出的接口的所有成员的实现
			§ 在实现类或结构中查找接口成员的实现的过程称为接口映射
		○ 类继承中的接口实现
			§ 类继承其基类提供的所有接口实现
			§ 如果没有显示重新实现接口，派生类将无法以任何方式更改它从其基类继承的接口映射
		○ 类继承中的接口重新实现
			§ 派生类可以将基类实现的接口包含在自己的继承列表中来重新实现接口
			§ 接口的重新实现与接口的初始实现具有完全相同的接口映射规则

 * 接口与抽象类和特性的主要区别
	• 与抽象类
		○ 关于成员
			§ 抽象类内可以声明任何类成员，但是静态成员不能声明为抽象成员或虚成员
			§ 接口不能声明实例字段、构造函数和析构函数
			§ 抽象类成员除非显示声明为abstract、virtual或override，否则都是非虚的
			§ 接口所有实例成员默认都是虚成员，静态方法也可以用abstract和virtual声明为虚的
		○ 关于继承
			§ 类只能是单继承，且抽象类的派生类必须实现抽象类的抽象成员，除非派生类也是抽象类
			§ 接口可以多继承，一个接口可以继承多个结构，一个类或结构可以实现一个或多个接口
	• 与特性
		○ 接口应表示类型能执行的功能
		○ 特性用于陈述类型的事实
 */

using LearnCSharp.Basic.LearnInterfaceSpace;

/*【12101：接口代码示例】
 * 下面命名空间中设计了一个接口IMove和两个实现该接口的类Box和Car
 * IMove接口提供了一系列协定，表示某个事物能进行移动的动作，且需要有速度、时间和路程
 * Box类实现IMove接口采用了显示实现，因为Box表示的箱子本身不具有移动的功能，但可以被移动，故接口成员只做辅助
 * Car类实现IMove接口采用了隐式实现，因为Car表示的车子的核心功能之一就是移动
 * 这里也可以看出，接口是对类或结构的一种协定，这些协定就是实现接口的类或结构能做什么的协定，可以看作是对不同事物共性特征或行为的抽象
 */
namespace LearnCSharp.Basic.LearnInterfaceSpace
{
	public interface IMove
	{
		public double Velocity { get; set; }
		public abstract double Distance { get;}
		public double Time { get; set; }

		public virtual void Move()
		{
			Console.WriteLine($"以速度{Velocity}移动了{Velocity * Time}距离");
		}
	}

	public class Box : IMove
	{
        private double velocity;
        private double time;
		private double Distance => velocity * time;

        public string BoxID { get; set; }
        public Box(string boxID)
		{
			BoxID = boxID;
        }
		//显示实现接口IMove
		double IMove.Distance => Distance;
        double IMove.Velocity
		{
            get => velocity;
            set => velocity = value;
        }
		double IMove.Time
        {
            get => time;
            set => time = value;
        }
        void IMove.Move()
		{
            Console.WriteLine($"箱子{BoxID}被以速度{velocity}移动了时间{time}，共移动距离{Distance}");			
		}
	}

	public class Car : IMove
	{
		public string CarID { get; set; }

		public Car(string carID)
		{
			CarID = carID;
		}

		//隐式实现接口IMove
		public double Distance => Velocity * Time;
		public double Velocity { get; set; }
		public double Time { get; set; }
		public void Move()
		{
            Console.WriteLine($"车牌号为{CarID}的车子正以速度{Velocity}行驶了时间{Time}，移动距离{Distance}");
		}
	}
}

namespace LearnCSharp.Basic
{
	public class LearnInterface
	{
		/*【12101：接口演示示例】*/
		public static void StartLearnInterface()
		{
            //以下代码用于实践访问Box类显示实现的接口成员
            Box box = new Box("Box No.0001");
			//student.Move();   //无法访问接口成员，因为Student类中接口是被显示实现的
			IMove boxMove = box;
			boxMove.Velocity = 5;
            boxMove.Time = 6;
            boxMove.Move();

			Console.WriteLine();

            //以下代码用于实践访问Car类隐式实现的接口成员
            Car car = new Car("No.00001");
			car.Velocity = 80;  //隐式实现的接口成员也是实现类的成员，故可通过类实例访问
			car.Time = 5;
			car.Move();

			IMove carMove = car;
			carMove.Move();     //隐式实现的接口成员也可通过将实例赋值给接口变量再通过接口实例访问
		}
	}
}
