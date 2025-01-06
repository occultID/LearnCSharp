/*【学习泛型】
 * 泛型
	• 泛型将类型参数的概念引入.NET已用于设计具有以下特征的类和方法
		○ 在客户端代码声明并初始化这些类或方法之前，这些类或方法会延迟指定一个或多个类型
	• 泛型类和泛型方法兼具以下特性
		○ 可重用性
		○ 类型安全性
		○ 效率
	• 泛型通常于集合以及作用于集合的方法一起使用
	• 可以创建泛型的类型或成员：接口、类、结构、记录、委托、事件、方法
		○ 可以对泛型参数进行约束以访问特定数据类型
	• 在泛型数据类型中所用类型的信息可在运行时通过使用反射来获取
	
 * 泛型详解
	• 泛型类型或成员的声明
		○ 参考接口、类、结构、记录、委托、事件、方法章节中声明与定义的完整形式
	• 泛型类型参数（下面全部以“类型参数”标识该项）
		○ 在泛型类型或方法定义中，类型参数是在其创建泛型类型的一个实例时，客户端指定的具体特定类型的占位符
			§ 泛型类型或泛型方法无法按原样使用，因为它不是真正的类型
			§ 泛型类型更像是类型的蓝图
			§ 若要使用泛型类型或泛型方法，必须通过使用具体的特定类型来指定尖括号内的类型参数来声明并实例化构造类型
				□ 该特定类型必须是可以被编译器识别的任何类型
			§ 在泛型类的实例中，每个泛型类型参数在运行时都会被指定的具体特定类型一一替换
				□ 可以指定不同具体类型创建不同实例提高了代码重用性
				□ 因为使用前必须确定类型参数的具体类型，所以保障了类型安全性
				□ 因为运行时类型是明确的，不存在装箱拆箱和类型转换，所以运行效率得以保障
		○ 类型参数命名指南
			§ 使用描述性名称命名类型参数，除非单个字母名称完全具有自我说明性且描述性名称不会增加任何作用
				□ 对具有单个字母类型参数的类型，考虑使用T作为类型参数名称
					® 比如List<T>
				□ 在泛型类型参数描述性名称前添加前缀T
					® 比如Dictionary<Tkey, TValue>
			§ 考虑可参数名称中指示出类型参数的约束
				□ 比如某个类型参数约束为实现IEnumerator，则可命名为Tenumerator
	• 泛型类型参数的约束
		○ 约束的作用和使用原因
			§ 约束告知编译器类型参数必须具备的功能
				□ 在没有任何约束的情况下，类型参数可以是任何类型
				□ 未指定约束的前提下，编译器只能假定当前类型参数为最终基类object，只能通过类型参数访问object的成员
			§ 约束指定类型参数的功能和预期
				□ 声明这些约束意味着用户可以使用约束类型的操作和方法调用
				□ 指定类型参数的约束后，编译器可以假定当前类型为约束类型来使其可以使用该约束类型的功能和成员
		○ 约束的声明
			§ 在泛型类型或泛型方法声明的最后，主体{}之前，使用如下格式进行类型参数约束声明
				□ where 类型参数: 约束
			§ 如有多个约束，则每个约束使用“,”进行分隔，约束不能互相冲突排斥
			§ 如有多个类型参数，则每个类型参数都应使用上述格式进行约束，但无需使用标点符号对每个声明进行分隔
		○ 类型参数约束：下表以T、U来标识类型参数，这些约束是C#中支持的约束
			§ 约束	描述
			where T: struct 	类型参数实参必须是不可为null的值类型，该约束不能和new()约束、unmanaged约束结合使用
			where T: class		类型参数实参必须是引用类型（任何类、接口、委托或数组），在可为null的上下文中，T必须不可为null
			where T: class?		类型参数实参必须是可为null或不可为null的引用类型
			where T: notnull	类型参数实参必须是不可为null的类型
			where T: default	重写方法或提供显示接口实现时，如果需要指定不受约束的类型参数，此约束可解决歧义
			where T: unmanaged	类型参数实参必须是不可为null的非托管类型
			where T: new()		类型参数实参具有公共无参构造函数
			where T: 基类名		类型参数实参必须是指定的基类或该基类的派生类，且在可为null的上下文中，T必须不可为null
			where T: 基类名?	类型参数实参必须是指定的基类或该基类的派生类
			where T: 接口名		类型参数实参必须是指定的接口或该接口的实现类型，且在可为null的上下文中，T必须不可为null
			where T: 接口名?	类型参数实参必须是指定的接口或该接口的实现类型
			where T: U			为T提供的类型参数实参必须是为U提供的实际类型或派生自为U提供的实际类型
		○ 未绑定的类型参数
			§ 没有约束的类型参数成为未绑定的类型参数
			§ 未绑定的类型参数具有以下规则
				□ 不能使用==和!=运算符，因为无法保证具体的类型参数实参能支持这些运算符
				□ 可以在它们和System.Object之间来回转换，或将它们显示转换为任何接口类型
				□ 可以将它们与null进行比较，但如果类型参数实参为值类型，则该比较将始终返回false
		○ 特殊的类型参数约束：下表以T标识类型参数，这些约束在C#语言中不允许，但是CLR始终允许这些约束
			§ 约束	描述
			where T: System.Delegate	使用System.Delegate约束，用户能够以类型安全的方式编写使用委托的代码
			where T: System.MulticastDelegate	
			where T: System.Enum		使用System.Enum的泛型提供类型安全的编程，缓存使用System.Enum中静态方法的结果

 * 泛型类型简介
	• 泛型类
		○ 泛型类用于封装不特定于特定数据类型的操作
			§ 最常见用法是用于链接列表、哈希表、、堆栈、队列和树等集合
			§ 无论存储数据的类型如何，添加项和从集合删除项等操作的执行方式基本相同
		○ 创建泛型类是从现有具体类开始，然后每次逐个将类型更改为类型参数，直到泛化和可用性达到最佳平衡
			§ 创建泛型类的注意事项
				□ 要将哪些类型泛化为类型参数
					® 可参数化的类型越多，代码就越灵活、其可重用性就越高
					® 过度泛化会造成其他开发人员难以阅读或理解代码
				□ 要将何种约束（如有）应用到类型参数
					® 应用最大程度的约束，同时仍可处理必须处理的类型
				□ 是否将泛型行为分解为基类和子类
					® 因为泛型类可用作基类，所以非泛型类的相同设计注意事项在此也适用
				□ 是否实现一个泛型接口还是多个泛型接口
	• 泛型接口
		○ 为泛型集合类或表示集合中的项的泛型类定义接口通常很有用处
			§ 为避免对值类型执行装箱和取消装箱操作，最好对泛型类使用泛型接口
			§ 接口被指定为类型参数的约束时，仅可使用实现接口的类型
	• 泛型委托
		○ 委托可以定义它自己的类型参数
		○ 引用泛型委托的代码可以指定类型参数以创建封闭式构造类型，就像实例化泛型类或调用泛型方法一样
	• 泛型方法
		○ 泛型方法是通过类型参数声明的方法

 * 泛型类型的开闭原则
	• 开放类型是指含有类型形参的类型
		○ 开放类型不具有实际类型，此时使用一个类型形式参数来暂时替代类型，这些替代类型包括
			§ 类型形参本身
			§ 以开放类型为元素类型的数组类型
			§ 开放类型的构造类型
		○ 开放类型需要先构造成封闭类型才能进行实例化
		○ 开放类型不是真正的数据类型
	• 封闭类型是指开放类型以外的所有类型
		○ 所有具有实际数据类型的类型
		○ 所有已经赋予类型实参的开放类型
		○ 封闭类型可用于创建实例

 * C++模板和C#泛型之间的区别
	• C#泛型和C++模板均是支持参数化类型的语言功能
	• C#泛型和C++模板之间的主要差异
		○ C#泛型的灵活性与C++模板不同
		○ C#不允许使用非类型模板参数
		○ C#不支持显示定制化，即特定类型模板的自定义实现
		○ C#不支持部分定制化：部分类型参数的自定义实现
		○ C#不允许将类型参数用作泛型类型的基类
		○ C#不允许类型参数具有默认类型
		○ 在C#中，泛型类型参数本身不能是泛型，但是构造类型可以用作泛型。C++允许使用模板参数
		○ C++允许在模板中使用可能并非对所有类型参数有效的代码，随后针对用作类型参数的特定类型检查此代码。C#中要求类中编写的代码可处理满足约束的任何类型
 */

using System.Collections;
using LGT = LearnCSharp.Professional.LearnGenericTypeSpace;

/*【泛型学习代码示例】
 * 下列代码使用泛型模式来自定义了一个双链表集合类
 * 该集合类以双链表的形式存储数据类型为T的数据
 */
namespace LearnCSharp.Professional.LearnGenericTypeSpace
{
    public class LinkedList<T> : IEnumerable<T> where T : notnull
    {
        private Node? FirstNode { get; set; }
        private Node? LastNode { get; set; }

        public int Count { get; private set; }
        public T First { get => FirstNode.Value; }
        public T Last { get => LastNode.Value; }

        public T this[int index]
        {
            get
            {
                if (index < 1 || index > Count)
                    throw new IndexOutOfRangeException(nameof(index));

                var pointer = FirstNode;
                for (int i = 1; i < index; i++)
                {
                    pointer = pointer.Next;
                }
                return pointer.Value;
            }
        }
        public void AddFirst(T item)
        {
            var node = new Node(item);

            if (FirstNode is null)
            {
                FirstNode = node;
                LastNode = node;
                Count++;
                return;
            }

            node.Next = FirstNode;
            node.Next.Previous = node;
            FirstNode = node;
            Count++;
        }

        public void AddLast(T item)
        {
            if (FirstNode is null)
            {
                AddFirst(item);
                return;
            }

            var node = new Node(item);
            node.Previous = LastNode;
            node.Previous.Next = node;
            LastNode = node;
            Count++;
        }

        public void InsertAt(int index, T item)
        {
            if (index < 1 || index > Count)
                throw new IndexOutOfRangeException(nameof(index));

            if (index == 1)
            {
                AddFirst(item);
                return;
            }

            var node = new Node(item);
            var pointer = FirstNode;

            for (int i = 1; i < index; i++)
            {
                pointer = pointer.Next;
            }

            node.Next = pointer;
            node.Previous = pointer.Previous;
            node.Previous.Next = node;
            node.Next.Previous = node;
            Count++;
        }

        public void RemoveAt(int index)
        {
            if (index < 1 || index > Count)
                throw new IndexOutOfRangeException(nameof(index));

            var pointer = FirstNode;

            for (int i = 1; i < index; i++)
            {
                pointer = pointer.Next;
            }

            if (Count == 1)
            {
                Clear();
                return;
            }

            if (pointer.Previous is null && Count > 1)
            {
                FirstNode = pointer.Next;
                FirstNode.Previous = null;
                Count--;
                return;
            }

            if (pointer.Next is null && Count > 1)
            {
                LastNode = pointer.Previous;
                LastNode.Next = null;
                Count--;
                return;
            }

            pointer.Previous.Next = pointer.Next;
            pointer.Next.Previous = pointer.Previous;
            Count--;
        }

        public void UpdateAt(int index, T item)
        {
            var pointer = FirstNode;
            for (int i = 1; i < index; i++)
            {
                pointer = pointer.Next;
            }

            pointer.Value = item;
        }

        public void Clear()
        {
            FirstNode = null;
            LastNode = null;
            Count = 0;
        }
        public IEnumerator<T> GetEnumerator()
        {
            var pointer = FirstNode;
            while (pointer is not null)
            {
                yield return pointer.Value;
                pointer = pointer.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Node
        {
            public Node Previous { get; set; }
            public T Value { get; set; }
            public Node Next { get; set; }

            public Node(T t)
            {
                Value = t;
                Previous = null;
                Next = null;
            }
        }
    }
}

namespace LearnCSharp.Professional
{
    internal class LearnGenericType
    {
        public static void StartLearnGenericType()
        {
            Console.WriteLine("【学习泛型】");

            LGT.LinkedList<string> listStrings = new LGT.LinkedList<string>();
            Console.WriteLine("使用以下代码来测试自己创建的双链表泛型集合类，类型参数使用的实际类型为string类型\nLinkedList<string> listStrings = new LinkedList<string>();");
            Console.WriteLine();
            Console.WriteLine("已经创建具体存储string类型数据的双链表实例listStrings");
            Console.WriteLine();

            Console.WriteLine("使用AddFirst方法依次前向添加三个节点，其数据为“I Love C#”、“Hello World”、“我爱编程”");
            Console.WriteLine();

            listStrings.AddFirst("I Love C#");
            Console.WriteLine($"--头节点：[ {listStrings.First} ] --尾节点：[ {listStrings.Last} ] --节点数：[ {listStrings.Count} ]");
            listStrings.AddFirst("Hello World");
            Console.WriteLine($"--头节点：[ {listStrings.First} ] --尾节点：[ {listStrings.Last} ] --节点数：[ {listStrings.Count} ]");
            listStrings.AddFirst("我爱编程");
            Console.WriteLine($"--头节点：[ {listStrings.First} ] --尾节点：[ {listStrings.Last} ] --节点数：[ {listStrings.Count} ]");

            Console.WriteLine();
            Console.WriteLine("使用AddLast方法依次后向添加三个节点，其数据为“你好.NET”、“学习.NET”、“学习写代码”");
            Console.WriteLine();

            listStrings.AddLast("你好.NET");
            Console.WriteLine($"--头节点：[ {listStrings.First} ] --尾节点：[ {listStrings.Last} ] --节点数：[ {listStrings.Count} ]");
            listStrings.AddLast("学习.NET");
            Console.WriteLine($"--头节点：[ {listStrings.First} ] --尾节点：[ {listStrings.Last} ] --节点数：[ {listStrings.Count} ]");
            listStrings.AddLast("学习写代码");
            Console.WriteLine($"--头节点：[ {listStrings.First} ] --尾节点：[ {listStrings.Last} ] --节点数：[ {listStrings.Count} ]");

            Console.WriteLine("依次遍历当前双链表中的数据：\n");

            foreach (var item in listStrings)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine("使用InsertAt方法在第四个位值出插入新数据--“我爱用C#写程序”");
            listStrings.InsertAt(4, "我爱用C#写程序");

            Console.WriteLine("使用UpdateAt方法将当前第五个节点数据更改为--“我们一起学习.NET”");
            listStrings.UpdateAt(5, "我们一起学习.NET");

            Console.WriteLine("使用RemoveAt方法将当前第六个节点数据移除");
            listStrings.RemoveAt(6);

            Console.WriteLine();
            Console.WriteLine("再次遍历当前双链表中的数据：\n");

            foreach (var item in listStrings)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine($"当前双链表基本信息：--头节点：[ {listStrings.First} ] --尾节点：[ {listStrings.Last} ] --节点数：[ {listStrings.Count} ]");
            Console.WriteLine($"使用集合访问运算符访问当前第四个节点：listStrings[4] = {listStrings[4]}");
        }
    }
}
