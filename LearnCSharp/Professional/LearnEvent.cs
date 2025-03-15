/*【学习事件】
 * 事件概述
	• 类或对象可以通过事件向其他类或对象通知发生的相关事情
		○ 发送（或引发）事件的类称为“发布者”
		○ 接收（或处理）事件的类称为“订阅者”
	• 和委托类似，事件是后期绑定机制
		○ 事件是建立在委托的语言支持之上的
	• 事件的特征
		○ 发布者确定何时引发事件，订阅者确定对事件作出何种响应
		○ 一个事件可以有多个订阅者，订阅者可以处理来自多个发布者的多个事件
		○ 没有订阅者的事件永远也不会引发
		○ 事件通常用于表示用户操作
		○ 当事件具有多个订阅者时，引发该事件时会同步调用事件处理程序
		○ 在.NET类库中，事件基于EventHandler委托和EventArgs基类
	• 事件支持的设计目标
		○ 在事件源和事件接收器之间启用非常小的耦合
		○ 订阅事件并从同一事件取消订阅应该非常简单
		○ 事件源应支持多个事件订阅服务器
			§ 它还应支持不附加任何事件订阅服务器

 * 事件的语言支持
	• 用于定义事件以及订阅或取消订阅事件的语法是对委托语法的扩展
	• 事件使用event关键字声明和定义
		○ 声明形式
			§ 访问级别 有效的事件修饰符组合 event 委托类型 事件名 {事件主体}
				□ 访问级别：该项是可选配置，不选则默认为private
					® public：公开访问级别
					® protected：受保护的访问级别
					® internal：应用程序域内部访问级别
					® private：私有访问级别
				□ 有效的事件修饰符组合：该项为可选配置
					® static：标识事件为静态事件
					® abstract：标识事件为抽象事件，只能位于abstract抽象类中
					® virtual：标识事件为虚事件，在派生类中事件可被重写
					® override：标识事件为重写事件，重写了基类中的同签名virtual事件
					® sealed：标识事件在接下来的派生类中不再能重写，需要和override搭配使用
					® new：标识隐藏或覆盖基类中的同签名事件
					® unsafe：标识事件包装的委托兼容的方法含非托管类型或代码
				□ event：该项是必选配置，这是声明事件的必须关键字
				□ 委托类型：该项是必选配置，事件的类型必须为委托类型
					® 事件不是独立的数据类型，数据是事件的“包装器”
					® 这个包装器对委托字段的访问起限制作用，相当于一个“蒙版”
					® 封装（encapsulation）的一个重要功能就是隐藏
					® 事件对外界隐藏了委托实例的大部分功能，仅暴露添加/移除事件处理器的功能
				□ 事件名：该项是必选配置，即事件的名称
					® 事件的命名通常采用带有时态的动词或者动词短语
						◊ 事件发布者“正在做”什么事情，用进行时
						◊ 事件发布者“做完了”什么事情，用完成时
				□ {事件主体}：该项是可选配置，标识事件对委托的封装，不选编译器也会默认生成
					® add访问器
						◊ 为事件封装的委托附加委托实例
					® remove访问器
						◊ 为事件封装的委托移除委托实例
					® 类似于属性，事件主体只能包含以上两个访问器，不能在访问器外添加其他代码
	• 事件“包装”的委托形式
		○ 用于声明Foo事件的委托，一般命名为FooEventHandler（除非是一个非常通用的事件约束）
		○ FooEventHandler委托的参数一般有两个（由Win32 Api演化而来，历史悠久）
			§ 第一个是object类型，名字为sender，实际上就是事件的发布者、事件的source；
			§ 第二个是EventArgs类的派生类，类名一般为FooEventArgs，参数名为e，也就是事件参数
			§ 我们可以把委托的参数列表看做是事件发生后发送给事件订阅者的“事件消息”
		○ 触发Foo事件的方法一般命名为OnFoo，即“因何引发”、“事出有因”
			§ 访问级别为protect，不能为public，不然又成了可以“借刀杀人”了

 * 标准.NET事件模式
	• 事件数据类
		○ 事件数据也称事件参数、事件消息、事件信息
		○ 事件数据类用于传递事件消息
		○ 事件数据类通常继承于.NET内置的EventArgs类
		○ 事件数据类命名通常采用名词+EventArgs
		○ 事件数据类的访问级别对于事件发布者和订阅者应均为可见的
			§ 在数据类添加所需成员以保留事件数据
	• 事件委托签名
		○ 事件委托的标准签名是
			§ 访问级别 delegate void 委托名称(object sender, EventArgs args);
		○ 事件委托的访问级别对事件的发布者和订阅者应是可见的
		○ 事件委托的返回类型必须是void
		○ 事件委托的参数列表包含两种参数
			§ object sender：标识事件的发布者
			§ EventArgs args：标识事件的数据，类型可以是EventArgs类型或其派生类型
				□ 如果事件不需要传递任何信息，仍需提供该参数，可使用特殊值EventArgs.Empty来表示事件不包含任何附加信息
	• 定义并引发类似字段的事件
		○ 使用上述“事件的语言支持”部分给出的方式进行声明和定义事件
	• 订阅者订阅事件和取消订阅事件
		○ 在订阅者类中需提供与事件委托匹配的事件处理器，即兼容该委托的方法
		○ 使用下一部分中知识来订阅和取消订阅事件

 * 事件的订阅和取消订阅
	• 使用Visual Studio IDE订阅事件
		○ 当使用WinForm或者WPF进行有UI的程序编程时，可以通过属性窗口中的控件事件属性功能来创建事件
			§ 双击属性功能对应的事件，则会在后台代码中生成一个对应的空事件处理方法
	• 以编程方式订阅事件
		○ 定义一个事件处理程序方法，其签名与该事件的委托签名匹配
		○ 使用加法赋值运算符（+=）来为事件附加事件处理程序，即订阅事件
		○ 使用减法赋值运算符（-=）来为事件取消事件处理程序，即取消订阅事件
	• 使用匿名函数订阅事件
		○ 如果以后不必取消订阅某个事件，则可以使用加法赋值运算符（+=）将匿名函数作为事件处理程序进行附加
		○ 如果使用匿名函数订阅事件，事件的取消订阅过程将比较麻烦
			§ 将匿名函数存储在委托变量，然后将该委托实例附加到事件中
			§ 如需取消订阅，则使用减法赋值运算符（-=）将该委托实例从事件中移除来进行取消订阅

 * 事件注意事项
	• 事件处理器是方法成员
	• 挂接事件处理器的时候，可以使用委托实例，也可以直接使用方法名，这是个“语法糖”
	• 事件处理器对事件的订阅不是随意的，匹配与否由声明事件时所使用的委托类型来检测
	• 事件可以同步调用也可以异步调用

 * 事件与委托的关系
	• 事件不是委托字段与实例，而是基于委托，是委托的包装器
	• 使用委托类型声明事件的原因
		○ 站在发布者角度，是为了表明发布者能对外传递哪些消息
		○ 站在发布者角度来看，它是一种约定，是为了约束能够使用什么样签名的方法来处理（响应）事件
		○ 委托类型的实例将用于存储（应用）事件处理器
	• 对比事件与属性
		○ 属性不是字段——很多时候属性是字段的包装器，这个包装器用来保护字段不被滥用
		○ 事件不是委托字段——它是委托字段的包装器，这个包装器用来保护委托字段不被滥用
		○ 包装器永远都不可能是被包装的东西
 */
using LearnCSharp.Professional.LearnEventSpace;
using System.Collections;

/*【.NET标准事件模式代码定义】
 * 下列命名空间将会以标准事件模式来完整声明和定义一个事件
 */
namespace LearnCSharp.Professional.LearnEventSpace
{
    /// <summary>
    /// 为事件声明的数据类，名字使用EventArgs为后缀
    /// EventArgs：
    /// 1、提示该类是专门用于声明事件委托
    /// 2、约束事件需要传递的信息
    /// 3、专门用于存储事件的数据
    /// </summary>
    public class ElementChangedEventArgs<T>:EventArgs where T:notnull
	{
		//集合元素
		public T Element { get; set; }
		//变化信息
		public string ChangedInfo { get; set; }
		//集合元素数量
		public int CountElement { get; set; }

		public ElementChangedEventArgs(T element, string changedInfo, int countElement)
		{
			Element = element;
			ChangedInfo = changedInfo;
			CountElement = countElement;
		}
	}

    /// <summary>
    /// 为事件声明的委托，其返回值类型规定为void，名字使用EventHandler为后缀
    /// EventHandler：
    /// 1、提示该委托是专门用于声明事件
    /// 2、约束事件处理器
    /// 3、专门用于存储事件处理器
    /// </summary>
    /// <param name="sender">参数1:事件的发布者</param>
    /// <param name="e">参数2:事件的消息</param>
    public delegate void ElementChangedEventHandler<T>(object sender, ElementChangedEventArgs<T> e) where T : notnull;

	public class NotifyList<T>:IEnumerable<T> where T : notnull
	{
		private T[] t;
		private readonly int defaultCapacity = 10;
		private ElementChangedEventHandler<T> elementChanged;
        public int Count { get; private set; }
		public int Capacity { get => t.Length;}

		public event EventHandler<EventArgs> ElementCleared;
		public event ElementChangedEventHandler<T> ElementChanged
		{
			add { elementChanged += value; }
			remove { elementChanged -= value; }
		}

		public T this[int index]
		{
			get
			{
				if (t is null)
					throw new ArgumentNullException("NotifyList");

				if(index<0 || index > Count)
					throw new IndexOutOfRangeException("index");

				return t[index];
			}
		}

		public NotifyList()
		{
			t = new T[defaultCapacity];
		}

		public NotifyList(int capacity)
		{
			if (capacity <= 0)
				throw new ArgumentException();

			t = new T[capacity];
		}

        private void Increase(int newCapacity)
		{
            var temp = new T[newCapacity];
            if (t is not null)
                this.CopyTo(temp, 0);
            t = temp;
        }

		public void Add(T? item)
		{
			if(item is null)
				throw new ArgumentNullException("item");

            if (Count >= Capacity)
                Increase(Capacity * 2);

            if (this.Contains(item))
                throw new ArgumentException("已存在该实例", nameof(item));

            t[Count] = item;

            Count++;
			this.OnElementChanged(item, "Add Element", Count);
        }

        public void Insert(int index, T? item)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException("index");
            if (Contains(item)) throw new ArgumentException("请勿重复插入同一实例", nameof(item));
            if (Count  >= Capacity) Increase(Capacity * 2);

            for (int i = Count; i > index; i--)
            {
                t[i] = t[i - 1];
            }
            t[index] = item; 
            Count++;
			this.OnElementChanged(item, "Insert Element", Count);
        }

        public int IndexOf(T item)
		{
            for (int i = 0; i < Count; i++)
            {
                if (item.Equals(t[i]))
                    return i;
            }

            return -1;
        }

        public bool Remove(T item)
        {
            if (Contains(item))
            {
                this.RemoveAt(this.IndexOf(item));
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException("index");

			var temp = t[index];
            for (int i = index; i < Count; i++)
            {
                t[i] = t[i + 1];
            }
            Count--;
			this.OnElementChanged(temp, "Remove Element", Count);
        }

        public bool Contains(T? item)
        {
            if (item is null)
                return false;

            for (int i = 0; i < Count; i++)
            {
				if (item.Equals(t[i]))
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null) throw new ArgumentNullException("array");
            if (array.Length - arrayIndex < Count) throw new ArgumentException("目标数组容量不足");

            for (int i = arrayIndex; i < arrayIndex + Count; i++)
            {
                array[i] = t[i - arrayIndex];
            }
        }

		public void Clear()
		{
			t = new T[defaultCapacity];
			Count = 0;
			this.OnElementCleared();
		}

        protected void OnElementChanged(T t, string changedInfo, int countElement) 
		{
			ElementChangedEventArgs<T> elementChangedEventArgs = new ElementChangedEventArgs<T>(t, changedInfo, countElement);
			this.elementChanged?.Invoke(this, elementChangedEventArgs);
		}

		protected void OnElementCleared()
		{
			this.ElementCleared?.Invoke(this, EventArgs.Empty);
		}

        public override string ToString()
        {
            return $"{typeof(T).Name} 类型数据列表 --Count：{Count} --Capacity：{Capacity}";
        }

        public IEnumerator<T> GetEnumerator()
        {
			if(t is not null)
				for (int i = 0; i < Count; i++)
				{
					yield return t[i];
				}
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

namespace LearnCSharp.Professional
{
    internal class LearnEvent
    {
		/*【20301：事件使用代码示例】
		 */
        public static void StartLearnEvent()
		{
            Console.WriteLine("\n------示例：事件------\n");
            NotifyList<string> strings = new NotifyList<string>();

            Console.WriteLine($"已经创建集合NotifyList<string>的一个实例strings --output：{strings}");
            Console.WriteLine();

            //使用以下委托封装一个匿名函数事件处理器
            ElementChangedEventHandler<string> processingEvent = (s, e) =>
			{
				string outputString = $"事件发布者：{s.GetType().Name}\n" +
				$"事件订阅者：{typeof(LearnEvent).Name}\n" +
				$"事件消息捕获：\n" +
				$"--数据：{e.Element}    --操作：{e.ChangedInfo}    --当前元素数量：{e.CountElement}\n";

				Console.WriteLine(outputString);
			};
			
			//使用以下代码来订阅strings发布的ElementChanged事件
			strings.ElementChanged += processingEvent;

			//下列代码对strings列表元素进行更改时会引发ElementChanged事件，上述提供的事件处理器则会响应事件并进行输出
			for (int i = 0; i < 10; i++)
			{
				strings.Add($"随机数字字符串：{Random.Shared.Next(10000, 100000)}");
			}

			Console.WriteLine($"strings列表当前状态：{strings.ToString()}\n");
            Console.WriteLine("使用实例strings的Insert方法在索引5的位置插入数据，插入数据会引发ElementChanged事件，事件处理器则会响应事件并进行输出\n");

            strings.Insert(5, $"随机数字字符串：{Random.Shared.Next(10000, 100000)}");

            Console.WriteLine($"strings列表当前状态：{strings.ToString()}\n");
            Console.WriteLine("使用实例strings的RemoveAt方法移除索引为5的数据，移除数据会引发ElementChanged事件，事件处理器则会响应事件并进行输出\n");

            strings.RemoveAt(5);

            Console.WriteLine($"strings列表当前状态：{strings.ToString()}\n");
            Console.WriteLine("使用以下代码来取消订阅strings发布的ElementChanged事件\nstrings.ElementChanged -= processingEvent;\n");

            //使用以下代码来取消订阅strings发布的ElementChanged事件
            strings.ElementChanged -= processingEvent;

			Console.WriteLine("使用实例strings的Insert方法在索引10的位置插入数据，由于取消订阅事件，事件处理器则不会再响应事件\n");

            strings.Insert(10, $"随机数字字符串：{Random.Shared.Next(10000, 100000)}");

			Console.WriteLine($"遍历strings列表内所有数据：{strings.ToString()}");

			foreach (var item in strings)
			{
				Console.WriteLine(item);
			}
			Console.WriteLine();

			//使用以下代码来订阅strings的ElementCleared事件，并附加一个匿名函数作为事件处理器
			strings.ElementCleared += (s,e) =>
			{
				Console.WriteLine($"事件发布者：{s.GetType().Name}\n" +
					$"事件订阅者：{typeof(LearnEvent).Name}\n" +
					"strings列表已清空数据，请勿再次采取除Add方法以外的其他方法进行列表操作\n");
			};

            Console.WriteLine("使用实例strings的Clear方法清空strings列表的数据，清除数据会引发ElementCleared事件，事件处理器则会响应事件并进行输出\n");
            
			strings.Clear();

            Console.WriteLine($"strings列表当前状态：{strings.ToString()}");
        }
    }
}
