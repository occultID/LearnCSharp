/*【学习继承与多态】
 * 继承
	• 继承、封装和多态性是面向对象编程的三个主要特征
	• 继承是面向对象的编程的一种基本特性
		○ 借助继承，能够定义可重用、扩展或修改父类行为的子类
		○ 成员被继承的类称为基类
		○ 继承基类成员的类称为派生类
		○ 派生类是基类的专门化
	• C#和.NET只支持单一继承
		○ 派生类只能继承自一个基类
		○ 继承是可以传递的
	• .NET类型系统中的所有类型除了可以通过单一继承进行继承之外，还可以隐式继承自Object或其派生的类型。
		○ Object的常用功能可用于任何类型
			§ 公共ToString方法
			§ 三个用于测试两个对象是否相等的方法
				□ 公共实例Equals(Object obj)方法
				□ 公共静态Object.Equals(Object obj1，Object obj2)方法
				□ 公共静态Object.ReferenceEquals(Object obj1, Object obj2)方法
				□ 默认情况下这三个方法测试的是引用相等性
					® 如果两个实例变量引用同一个对象，则它们相等
			§ 公共GetType()方法
			§ 受保护的Finalize()方法：用于在垃圾回收器回收对象的内存之前释放非托管资源
			§ 受保护的MemberswiseClone()方法：创建当前对象的浅表复制
		○ 根据实际需求，很多时候我们需要在自己的类中重新实现Object中的方法来满足需求
		○ 如下为C#中创建的各种类型极其隐式继承的基类，每个基类型通过继承向隐式派生的类型提供一组不同的成员
			§ 类型类别	隐式继承的基类
			class	Object
			struct	ValueType，Object
			enum	Enum，ValueType，Object
			delegate	MulticastDelegate，Delegate，Object
	• 类型转换：
		○ 派生类可隐式转换为其基类
		○ 基类需进行显示转换才能转换为其派生类

 * 继承相关概念
	• 基类：
		○ 成员可以被继承的类
		○ 基类设计应考虑其是现实事物的最简单抽象
		○ 如果抽象的模型不具有实体的对应，可以考虑设计基类为abstract类
	• 派生类：
		○ 继承基类成员的类
		○ 派生类是对基类的更专门化实现的类
		○ 派生类进行实例化时都会先调用基类的构造函数再调用自己的构造函数
	• 抽象类：
		○ 使用abstract修饰符声明的类
		○ 抽象类不能进行实例化
		○ 无法使用sealed修饰符来修饰抽象类
		○ 抽象类可以包含抽象成员，但这是可选的
			§ 抽象成员：使用abstract修饰符声明的类成员
				□ 抽象成员只能是属性、方法、索引器和事件
				□ 抽象成员不能提供实现，而是由具体的派生类来实现
				□ 抽象类的派生类也可以是抽象类，会继承基类的抽象成员，并且最终派生具体实现的类必须实现所有抽象成员
			§ 只有抽象类中才允许声明抽象成员
			§ 抽象方法隐式为虚方法，所以不能同时使用abstract和virtual来修饰抽象方法
			§ 抽象成员不能是静态成员
	• 基类成员对于派生类的可访问性
		○ 静态构造函数不能被派生类继承
		○ 实例构造函数不能被派生类继承
		○ 析构函数（终结器）不能被派生类继承
		○ 受保护成员仅在派生类中可见
		○ 内部成员仅在与基类同属一个程序集的派生类中可见，否则不可见
		○ 公共成员在派生类中可见，并且属于派生类的公共接口
		○ 私有成员仅在派生类为基类嵌套类时对派生类可见
	• 类成员修饰符和继承相关关键字
		○ abstract：标识成员为抽象成员
			§ 只能出现在抽象类中
			§ 其修饰对象可为属性、方法、索引器或事件
			§ abstract成员隐式为virtual成员
		○ virtual：标识成员为虚成员
			§ 在派生类中该成员可被使用override修饰的同签名成员重新针对派生类实现
			§ 其修饰对象可为属性、方法、索引器或事件
		○ override：标识成员为重写成员或替代成员
			§ 在派生类中使用该修饰符来重写基类中virtual修饰的同签名成员
			§ 其修饰对象可为属性、方法、索引器或事件
			§ override成员隐式为virtual成员
			§ 要求基类同签名成员必须有virtual进行修饰
		○ new：标识成员为复写并隐藏基类成员的成员
			§ 重写并隐藏基类同签名成员
			§ 基类同签名成员不是必须要由virtual修饰
			§ 其修饰对象可为属性、方法、索引器或事件
		○ sealed：标识该重写成员不能被该类的派生类再次重新实现
			§ sealed需和override搭配使用
		○ base：可以从派生类中使用base关键字调用基类方法

 * 多态性
	• 关于多态性 Polymorphism
		○ 多态性常被视为自封装和继承之后，面向对象编程的第三个支柱
		○ 多态性即指多种形态
			§ 在运行时，在方法参数和集合或数组等位置，派生类的对象可以作为基类的对象处理
				□ 在出现此多态性时，该对象的声明类型不再与运行时类型相同
			§ 基类可以定义并实现虚方法，派生类可以重写这些方法，即派生类提供自己的定义和实现
				□ 在运行时，客户端代码调用该方法，CLR查找对象的运行时类型，并调用虚方法发重写方法。
				□ 开发者可以在源代码中调用基类的方法，执行该方法的派生类版本
			§ 虚方法允许开发者以统一方式处理多组相关的对象
				□ 创建一个类层次结构，其中每个特定派生类均派生自一个公共基类
				□ 使用虚方法通过对基类方法的单个调用来调用任何派生类上的相应方法
	• 多态性概述
		○ 虚拟成员
			§ 当派生类从基类继承时，它会获得基类的所有方法、字段、属性和事件
			§ 派生类的设计器可以针对虚拟方法的行为做出不同的选择
				□ 派生类可以重写基类中的虚拟成员，并定义新行为
				□ 派生类可能会继承最接近的基类方法而不重写方法，同时保留现有的行为，但允许进一步派生的类重写方法
				□ 派生类可以定义隐藏基类实现的成员的新非虚实现
			§ 仅当基类成员声明为virtual或abstract时，派生类才能重写基类成员
			§ 派生成员必须使用override关键字显示指示该方法将参与需调用
			§ 字段不能是虚拟的，只有方法、属性、事件和索引器才可以是虚拟的
			§ 虚成员允许派生类扩展基类而无需使用基类实现
		○ 使用新成员隐藏基类成员
			§ 如果希望派生类具有与基类中的成员同签名的成员，则可以使用new关键字隐藏基类成员
			§ new关键字放置在要替换的类成员的返回类型之前
			§ 隐藏与重写的区别
				□ 隐藏基类成员后，当使用基类引用派生类再通过基类访问隐藏成员时，访问的是原成员而非新成员
				□ 重写基类成员后，当使用基类引用派生类再通过基类访问重写成员时，访问的是派生类的重写成员
		○ 阻止派生类重写虚拟成员
			§ 无论在虚拟成员和最初声明虚拟成员的类之间已声明了多少个类，虚拟成员都是虚拟的
			§ 派生类可以通过将重写声明为sealed来停止虚拟成员继承
				□ sealed关键字需要放置在类成员声明中的override之前
		○ 从派生类访问基类虚拟成员
			§ 已替换或重写某个方法或属性的派生类仍然可以使用base关键字访问基类的该方法或属性
			§ 建议
				□ 虚拟成员在它们自己的实现中使用base来调用该成员的基类实现
				□ 允许基类行为发生使得派生类能够集中得力实现特定于派生类的行为
				□ 未调用基类实现时，由派生类负责使它们的行为与基类的行为兼容
 */
using LearnCSharp.Basic.LearnInheritanceAndPolymorphismSpace;

/*【继承代码示例】
 * 下面命名空间中有四个类，它们存在继承关系
 * Animal类是一个抽象类，它隐式继承于Object类
 * Human类是继承于Animal类的一个更专门化的派生类
 * Student类是继承于Human类的一个更进一步专门化的派生类
 * Dog类是继承于Animal类的一个专门化的派生类
 */
namespace LearnCSharp.Basic.LearnInheritanceAndPolymorphismSpace
{
	public abstract class Animal
	{
		public abstract string Name { get; set; }

		public abstract void Eat();
		public virtual void Move()
		{
            //如果派生类使用了new隐藏，则在使用基类访问派生类该方法时会输出本数据
            Console.WriteLine($"【Animal类】Move方法 | 输出：这是某个动物正在移动");
		}

		//重写基类Object的ToString成员
		public override string ToString()
		{
			return "这是Animal类，是一个抽象类";
		}

		public Animal()
		{
			Console.WriteLine("【Animal类】抽象基类 | 无参构造函数");
		}
	}

	public class Human : Animal
	{
		private string? name;
		public sealed override string Name
		{
			get { return name; }
			set { name = !string.IsNullOrWhiteSpace(value) ? value : "无名氏"; }
		}

		public string NationalID { get; private set; }

		public Human()
		{
			Console.WriteLine("【Human类】派生自Animal类 | 无参构造函数");
		}

		public Human(string name, string nationalID)
		{
			Name = name;
			NationalID = nationalID;
			Console.WriteLine("【Human类】派生自Animal类 | 有参构造函数 | 参数1：name | 参数2：nationalID");
		}

		public override void Eat()
		{
			Console.WriteLine("【Human类】重写Animal类Eat方法 | 输出：这是人类在进食");
		}

		public override void Move()
		{
			Console.WriteLine("【Human类】重写Animal类Move方法 | 输出：这是人类在移动");
		}

		public override string ToString()
		{
			return "这是一个Human类，继承于Animal类";
		}

		//重写最终基类Object的Equals方法
		public override bool Equals(object? obj)
		{
			if (obj is Human human)
				return human.NationalID == NationalID;
			else
				return false;
		}
		//重写Equals方法应同步重写GetHashCode方法
        public override int GetHashCode()
        {
			return NationalID.GetHashCode();
        }

        public virtual void IntroduceSelf()
		{
			Console.WriteLine($"【Human类】IntroduceSelf方法 | 输出：大家好，我是{Name}");
		}
	}

	public class Student : Human
	{
		private int id;
		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		public Student()
		{
			Console.WriteLine("【Student类】派生自Human类 | 无参构造函数");
		}

		public Student(string name, string nationalID) : base(name, nationalID)
		{
			ID = Random.Shared.Next(10000, 100000);
			Console.WriteLine("【Student类】派生自Human类 | 有参构造函数 | 参数1：name | 参数2：nationalID | 调用Human类同参数构造函数进行初始化");
		}

		public Student(Student student)
		{
			this.Name = student.Name;
			ID = student.ID;
			Console.WriteLine("【Student类】派生自Human类 | 有参构造函数 | 参数1：student");
		}

		public override string ToString()
		{
			return "这是一个Student类，继承于Human类";
		}

		public new void Eat()
		{
			Console.WriteLine("【Student类】重写并隐藏Human类Eat方法 | 输出：这是一个学生在进食");
		}

		public override void Move()
		{
			Console.WriteLine("【Student类】重写Human类Move方法 | 输出：这是一个学生在移动");
		}

		public override void IntroduceSelf()
		{
			Console.WriteLine($"【Student类】重写Human类IntroduceSelf方法 | 输出：大家好，我是{Name}，我的学号是{ID}");
		}
	}

	public class Dog : Animal
	{
		public override string Name { get; set; }

		public Dog()
		{
			Console.WriteLine("【Dog类】派生自Animal类 | 无参构造函数");
		}

		public override void Eat()
		{
			Console.WriteLine("【Dog类】重写Animal类Eat方法 | 输出：这是狗狗在进食");
		}

		public new void Move()
		{
			Console.WriteLine("【Dog类】重写并隐藏Animal类Move方法 | 输出：这是狗狗在移动");
		}

		public override string ToString()
		{
			return "这是Dog类，继承于Animal类";
		}
	}
}

namespace LearnCSharp.Basic
{
    public class LearnInheritanceAndPolymorphism
    {
        public static void StartLearnInheritanceAndPolymorphism()
        {
            /*通过以下代码的输出可以发现，当派生类进行实例化时
              所有基类同签名的构造函数或派生类使用base关键字指定匹配签名的构造函数会被依次优先调用
              即使没有同签名或指定的基类构造函数，也会调用基类的默认构造函数*/

            Console.WriteLine("初始化派生类Student时继承链各类构造函数调用情况：");
            Student student1 = new Student();  //会依次调用Animal()、Human()、Student()
            Console.WriteLine();
            Student student2 = new Student("张三", "10002001");  //会依次调用Animal()、Human(string name,string nationalID)、Student(string name,string nationalID)
            Console.WriteLine();
            Student student3 = new Student(student2);   //会依次调用Animal()、Human()、Student(Student student)

            Console.WriteLine("\n");

            /*通过以下代码可以演示多态性，将Human、Student和Dog类的实例都引用给Animal类的变量
              则可通过基类来调用派生类实现的基类成员
              如果该成员是通过override重写，则调用的实际是派生类中重写的基类成员
              如果该成员是通过new隐藏基类成员，则调用的是基类成员
			  如果派生类未实现其基类的virtual成员，则调用的就是基类的成员*/

            Console.WriteLine("使用基类调用派生类方法成员的输出：");
            Animal ani1 = new Student("张三", "10002001");
            ani1.Eat();
            ani1.Move();
			Console.WriteLine();
            Animal ani2 = new Human("李四", "10002002");
            ani2.Eat();
            ani2.Move();
			Console.WriteLine();
            Animal ani3 = new Dog();
            ani3.Eat();
            ani3.Move();
			Console.WriteLine();
			Human human = new Student("张三", "10002001");
			human.Eat();
            human.Move();
            human.IntroduceSelf();

            Console.WriteLine("\n");

            /*由于所有类型都隐式继承于Object，所以可以重写Object中的可继承成员
			  以下代码以Human类为例演示重写Object的Equals和ToString方法后的输出*/

			Human human1 = new Human("张三", "1002301");
			Human human2 = new Human("张三", "1002301");
			bool isSameHuman = human1.Equals(human2);
			Console.WriteLine("以Human类为例演示重写Object的Equals和ToString方法后的输出：");
			Console.WriteLine($"------human1和human2相等性：{isSameHuman}");
			Console.WriteLine($"------Human类重写后的ToString()输出：{human1.ToString()}");
        }
    }
}
