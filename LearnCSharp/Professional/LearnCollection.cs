/*【204：学习集合】
 * 集合
	• 集合是一种用于创建和管理对象组的类
		○ 集合可以像数组一样来创建、管理和使用对象元素，但相比数组来说更灵活
		○ 集合的对象组随着程序更改的需要来动态地放大和缩小容量
		○ 对于某些集合，你可以为放入集合中的任何对象分配一个密钥，这样便可以通过密钥来快速检索对象
	• 集合与数组的对比
		○ 集合实例在创建后能存储的对象容量是可变的，而数组实例一旦创建则容量就会固定
		○ 集合（内部实现了索引器前提下）和数组都是采取同样的访问方式访问存储的对象元素，即实例名[索引]
			§ 集合的索引可以是任意数据类型
			§ 数组的索引只能是整型数字
		○ 集合和数组的对象元素都是强类型的，只能存储同一兼容类型的元素
			§ 因为object是所有类型的最终基类，所以当集合内部存储结构的类型和数组的类型是object时，其存储元素可以是任何类型
			§ 在设计程序时，如非必要不要采用object作为集合内部存储结构的类型或数组类型
			§ 如需考虑代码重用性和通用性，建议采取泛型来实现泛型集合
		○ 集合相对数组来说，可以更好的管理对象组
			§ 集合提供了很多内部方法和扩展方法来管理对象组
			§ 自定义集合可以比声明定义一个数组实现更多的逻辑尤其是复杂逻辑来管理对象组
				□ 比如自定义集合可在内部实现对象去重来保障一个对象组的所有对象都是不重复的

 * 内置通用集合
	• .NET内置了一系列通用集合
		○ 这些通用集合覆盖了大多数常用场景，用户可以直接使用
		○ 自定义集合在进行设计时，内置通用集合可作为很好参考，并且可以用于作为更专门化的自定义集合内部从存储结构
		○ 如果集合中只包含一种数据类型或其派生类型和其他兼容类型的元素，则可以优先考虑采用内置通用集合而非自定义
	• 常用内置通用集合简介
		○ 命名空间：System.Collections.Generic
			§ 该命名空间内的预置集合均为泛型集合
				□ 集合中所有数据都具有相同的数据类型，有泛型参数被赋予的实际数据类型决定
				□ 泛型集合通过仅允许添加所需的数据类型来强制实施类型化，故存取元素通常都不必进行类型转换
					® 泛型参数被赋予为object时需要注意装箱拆箱和显示类型转换等情况
					® 泛型参数被赋予的类型有继承关系关联时，存取时需注意类型是否是集合元素类型的基类或派生类
			§ 该命名空间内的常用类型
				□ 类	说明
				Dictionary<Tkey, TValue>	表示基于键进行组织的键/值对的集合，即字典
				List<T>	表示可按索引访问的对象的列表集合，提供用于对列表进行搜索、排序和修改的方法
				Queue<T>	表示对象的先进先出（FIFO）集合，即队列
				SortedList<Tkey, TValue>	表示基于相关的Icomparer<T>实现按键进行排序的键/值对的集合
				Stack<T>	表示对象的后进先出（LIFO）集合，即栈
				LinkedList<T>	表示双重链接列表
				LinkedListNode<T>	表示双重链接列表中的节点
				HashSet<T>	表示值的集合
		○ 命名空间：System.Collections.Concurrent
			§ 该命名空间内的预置集合均为线程安全的泛型集合
				□ 可提供高效的线程安全操作，以便从多个线程访问集合项
				□ 只要多个线程同时访问集合，就应考虑使用该命名空间内的集合
			§ 该命名空间内的常用类型
				□ 类	说明
				BlockingCollection<T>	为实现IProducerConsumerCollection<T>的所有类型提供限制和阻止功能
				ConcurrentDictionary<Tkey,TValue>	键值对字典的线程安全实现
				ConcurrentQueue<T>	先进先出队列的线程安全实现
				ConcurrentStack<T>	后进先出队列的线程安全实现
				ConcurrentBag<T>	无序元素集合的线程安全实现
				IProducerConsumerCollection<T>	类型必须实现该接口以在BlockingCollection中使用
		○ 命名空间：System.Collections
			§ 该命名空间内的预置集合不会将元素作为特别类型化的对象存储
				□ 所有元素会作为object对象进行存取
				□ 存取过程极有可能存在类型转换或装箱拆箱，随着集合增大则必然出现性能问题
				□ 由于可以往这些集合内存入任意数据类型数据，在读取对象进行使用时极其可能出现各种复杂逻辑甚至错误
				□ 只要可能，则应使用System.Collection.Generic或System.Collection.Concurrent命名空间中的泛型集合
			§ 该命名空间内的常用类型
				□ 类	描述
				ArrayList	表示对象的数组，这些对象的大小会根据需要动态增加
				BitArray	表示管理位值的压缩数组，这些值以布尔值的形式表示
				Hashtable	表示根据键的哈希代码进行组织的键值对的集合
				Queue	表示对象的先进先出集合
				Stack	表示对象的后进先出集合
		○ 命名空间：System.Collections.Specialized
			§ 该命名空间内的预置集合包含专门类型化和强类型化的集合
				□ 这些强类型集合只适用于特定的数据类型
			§ 该命名空间内的常用集合或类型
				□ 类	描述
				CollectionsUtil	用于创建忽略字符串大小写的集合
				HybridDictionary	通过以下方法来实现IDictionary：在集合较小时使用ListDictionary，然后在集合变大时切换到Hashtable
				ListDictionary	使用单向链接列表实现IDictionary，通常用于包含少于10项的结合
				NameValueCollection	表示可通过键或索引访问的关联string键和string值的集合
				OrderedDictionary	表示可通过键或索引访问的键值对的集合
				StringCollection	表示字符串的集合
				StringDictionary	使用字符串（而不是对象）强类型的键和值来实现哈希表

 * 自定义集合
	• 可以通过实现Ienumerable<T>或IEnumerable接口来定义集合
	• 尽管可以自定义集合，但通常最好使用包含在.NET中的集合
	• 集合是一个类，其内部成员和类一致，但会有部分成员用于更好的或专门的支持类成为集合类
		○ 索引器
			§ 索引器允许类或结构的实例就像数组一样进行索引
				□ 无需显示指定类型或实例成员，即可设置或检索索引值
				□ 对于集合类索引器是可选的，如果不使用索引器会导致集合实例无法同数组一样进行使用
			§ 索引器类似于属性，不同之处在于它的访问器需要使用参数
				□ 索引器的参数不要求必须为整数值
				□ 索引器的参数可以是多个，每个参数使用“,”分隔
			§ 索引器的声明定义形式
				□ 访问级别 有效的索引器修饰符 返回类型 this[索引参数]
				{
					get{ 返回对应类型的值 }
					set{ 为value进行值分配 }
				}
					® 访问级别：任意访问级别都可以
					® 有效的索引器修饰符：
						◊ abstract：标识索引器为抽象成员，此时无需实现索引器且只能在抽象类中声明
						◊ virtual：标识索引器在派生类中可以被重写
						◊ override：标识在派生类中重写了基类索引器
						◊ sealed：标识在当前类接下来的派生类中无法再重写该索引器，需要搭配override使用
						◊ unsafe：标识索引器内会使用到非托管类型数据或代码
					® 返回类型：即索引器索引的值的类型
					® this：标识该类实例可以如同数组一样进行索引
					® [索引参数]：
						◊ 索引参数即用于索引值的键
						◊ 索引参数不必须要求是整数值类型
						◊ 索引参数可以有多个，每个参数使用“,”分隔
				□ 如需索引器只提供索引而不提供修改，则需定义只读索引器，即只有get访问器的索引器
			§ 索引器使用
				□ 读取：数据类型 变量名 = 实例名[索引参数]
				□ 写入：实例名[索引参数] = 值
		○ 迭代器
			§ 迭代器用于对集合执行自定义迭代，逐步遍历集合
				□ 迭代器可以是一种方法，或是一个get访问器，或是一个类
					® 迭代器方法具有位于for循环中的yield return语句
					® 迭代器使用yield return语句返回集合的每一个元素，每次返回一个元素
					® 迭代器的返回类型可以是IEnumerable、IEnumerable<T>、IEnumerator或IEnumerator<T>
					® 实现迭代器方法
						◊ 步骤一：为集合实现一个IEnumerable或IEnumerable<T>接口
						◊ 步骤二：为集合新增一个返回类型为IEnumerator或IEnumerator<T>的无参实例方法或只读属性
						◊ 步骤三：在该方法或只读属性的get访问器内实现以下任意一个逻辑
							} 其一：如果集合用于存储数据的结构本身是一个已实现迭代器的集合或数组，那么就直接返回这个结构的枚举器
							} 其二：使用yield return来进行迭代
							} 其三：实例化自己的IEnumerator或IEnumerator<T>实现类并返回该实例
						◊ 步骤四：将步骤二的方法或属性在实现IEnumerable或IEnumerable<T>接口的GetEnumerator方法中作为返回值返回
						◊ 注意：可以不做步骤二而将步骤三的逻辑直接在GetEnumerator方法中实现
				□ 迭代器也可以是一个类，这个类被称为“枚举类型”或“枚举器”
					® 枚举器是一个只读的且只能在值序列上迁移的游标
					® 满足以下任意规则的类型可以作为枚举器使用
						◊ 实现了System.Collections.IEnumerator接口
						◊ 实现了System.Collections.Generic.IEnumerator<T>接口
						◊ 拥有public的无参MoveNext方法，并且拥有一个public的名为Current的属性
				□ foreach语句用来在可枚举对象上执行迭代操作
					® 可枚举对象指逻辑上的序列
						◊ 它本身不是游标
						◊ 它可以在对象自身上生成游标
					® 满足以下任意规则的类型就是一个可枚举类型
						◊ 实现了System.Collections.IEnumerable接口
						◊ 实现了System.Collections.Generic.IEnumerable<T>接口
                        ◊ 拥有public的无参GetEnumerator方法，且该方法返回一个枚举器
			§ 迭代器由用户使用foreach语句遍历集合时自动调用
				□ foreach循环的每次迭代都会调用迭代器
				□ 迭代器到达yield return语句时，会返回一个表达式，并保留当前在代码中的位值
                □ 下次调用迭代器时，将从该位置重新开始执行
 */

using LearnCSharp.Professional.LearnCollectionsSpace;
using System.Collections;

namespace LearnCSharp.Professional.LearnCollectionsSpace
{
    /*【自定义集合示例】
     * 定义一个自定义集合类Books
	 *该类用于存储Book类对象实例
	 *Book类的定义在本类后面
	 */
    public class Books : IEnumerable<Book>
    {
        private const int defaultCapacity = 10;
        private Book[] books;

		public bool IsReadOnly { get; }

        public int Count { get; private set; }
		public int Capacity
		{
			get => books.Length;
		}

		public Books() : this(defaultCapacity) { }

		public Books(int capacity)
		{
			if (capacity <= 0) throw new ArgumentException(nameof(capacity));
			books = new Book[capacity];
			Count = 0;
		}

		public Books(bool isReadOnly = false, params Book[] books)
		{
			if (books == null || books.Length == 0) throw new ArgumentNullException();

			int countElement = 0;
			bool allElementsAreNull = true;
			for (int i = 0; i < books.Length; i++)
			{
				if (books[i] is not null)
				{
					allElementsAreNull = false;
                    countElement++;
                }
			}

			if (allElementsAreNull) throw new ArgumentNullException();

			this.books = new Book[countElement];

            int mark = 0;
            for (int i = 0; i < countElement; i++)
			{
				for (int j = mark; j < books.Length; j++)
				{
					if (books[j] is not null)
					{
						this.books[i] = books[j];
						mark += 1;
						break;
					}
				}
			}

			Count = countElement;
			IsReadOnly = isReadOnly;
		}

        public Book this[int index] 
		{
			get
			{
				if (index < 0 || index >= Count) 
					throw new IndexOutOfRangeException();
				return books[index];
			}
			set
			{
                if (IsReadOnly) throw new InvalidOperationException("只读集合禁止操作元素");
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
                if (value is null) throw new ArgumentNullException();

				books[index] = value;
			}
		}

		private void Increase(uint newCapacity)
		{
			if (Count >= int.MaxValue) throw new InvalidOperationException();

			if (newCapacity > (uint)int.MaxValue) 
				newCapacity = (uint)int.MaxValue;

			var temp = books;
			books = new Book[newCapacity];
			Array.Copy(temp, books, temp.Length);
			Array.Clear(temp);
		}

        public void Add(Book book)
        {
            if(IsReadOnly) throw new InvalidOperationException("只读集合禁止操作元素");
			if(book is null) throw new ArgumentNullException("不得添加null实例");
			if (Contains(book)) throw new ArgumentException("请勿重复添加相同元素");

			if (Count >= Capacity)
				Increase((uint)Capacity * 2);

			books[Count] = book;
			Count++;
        }

        public void Clear()
        {
            if (IsReadOnly) throw new InvalidOperationException("只读集合禁止操作元素");
            Array.Clear(books);
			books = new Book[defaultCapacity];
			Count = 0;
        }

        public bool Contains(Book book)
        {
			if (book is null)
				return false;

			for (int i = 0; i < Count; i++) 
			{
				if(books[i] == book) return true;
			}

			return false;
        }

        public void CopyTo(Book[] books, int booksIndex)
        {
			if (books == null) throw new ArgumentNullException();
			if (booksIndex < 0 || booksIndex >= books.Length) throw new IndexOutOfRangeException();
			if (books.Length - booksIndex < Count) throw new ArgumentOutOfRangeException();

            for (int i = 0; i < Count; i++)
            {
				books[booksIndex + i] = this.books[i];
            }
        }

        public IEnumerator<Book> GetEnumerator()
        {
			//for (int i = 0; i < Count; i++) { yield return books[i]; }
			return new BookEnumerator(this);
        }

        public int IndexOf(Book book)
        {
			if (book is null)
				return -1;

            for (int i = 0; i < Count; i++)
            {
                if (books[i] == book) return i;
            }

			return -1;
        }

        public void Insert(int index, Book book)
        {
            if (IsReadOnly) throw new InvalidOperationException("只读集合禁止操作元素");
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            if (book is null) throw new ArgumentNullException("不得插入null实例");
            if (Contains(book)) throw new ArgumentException("请勿重复添加相同元素");

			if (Count >= Capacity)
				Increase((uint)Capacity * 2);

			for (int i = Count; i > index; i--)
			{
				books[i] = books[i - 1];
			}

			books[index] = book;
			Count++;
        }

        public bool Remove(Book book)
        {
			int indexOfBook = IndexOf(book);
			if(indexOfBook >= 0)
			{
				RemoveAt(indexOfBook);
				return true;
			}
			return false;
        }

        public void RemoveAt(int index)
        {
            if (IsReadOnly) throw new InvalidOperationException("只读集合禁止操作元素");
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

			for (int i = index; i < Count-1; i++)
			{
				books[i] = books[i + 1];
			}
			books[Count - 1] = null;
			Count--;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

		/*【自定义迭代器】*/
        private sealed class BookEnumerator : IEnumerator<Book>
        {
			private Books books;
			private int index = -1;

            public Book Current => books[index];

            object IEnumerator.Current => Current;

			public BookEnumerator(Books books)
			{
				if (books is null) throw new ArgumentNullException(nameof(books));
				this.books = books;
			}

            public void Dispose()
            {
                Reset();
            }

            public bool MoveNext()
            {
				index++;
				return index < books.Count;
            }

            public void Reset()
            {
				index = -1;
            }
        }
    }

    /*定义一个Book类
     *内部重写了Object类中的Equals、GetHashCode和ToString方法
     *内部实现了IEquatable<T>接口
     *内部重载了==和!=运算符
     */
    public class Book : IEquatable<Book>
    {
		public Guid Guid { get; init; }
		public string BookName 
		{ 
			get;
			set 
			{
				if (!string.IsNullOrWhiteSpace(value))
					field = value;
				else
					throw new ArgumentException(nameof(BookName));
			} 
		}
		public string Author
		{
            get;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    field = value;
                else
                    throw new ArgumentException(nameof(Author));
            }
        }

		public Book(string bookName,string author)
		{
			Guid = Guid.NewGuid();
			BookName = bookName;
			Author = author;
		}

        public bool Equals(Book? other)
        {
            bool isSameGuid = this.Guid == other?.Guid;
            bool isSameName = this.BookName == other?.BookName;
			bool isSameAuthor = this.Author == other?.Author;

			if (isSameGuid) return true;
			if (isSameName && isSameAuthor) return true;

			return false;
        }

		public static bool operator ==(Book left, Book right)
		{
			return left.Equals(right);
;		}

		public static bool operator !=(Book left,Book right)
		{
			return !left.Equals(right);
		}

        public override bool Equals(object? obj)
        {
			return obj is Book book ? Equals(book) : false;
        }

        public override string ToString()
        {
			return $"书籍信息 --- 书籍ID：{Guid} | 书名：《{BookName}》 | 作者：{Author}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}

namespace LearnCSharp.Professional
{
    internal class LearnCollection
    {
		/*【20401：系统内置集合 --- 以List<T>为例】
		 */
		public static void LearnSystemCollection()
		{
            Console.WriteLine("\n------示例：系统内置集合（以List<T>为例）------\n");

            //实例化一个List<Book>集合,分别使用初始化器和Add方法添加一些元素
            List<Book> bookList = new List<Book>()
            {
                new Book("鬼 吹 灯","天下霸唱"),
                new Book("迷踪之国","天下霸唱"),
                new Book("死亡循环","天下霸唱"),
                new Book("盗墓笔记","南派三叔"),
                new Book("老 九 门","南派三叔"),
                new Book("斗罗大陆","唐家三少"),
                new Book("神印王座","唐家三少")
            };

            bookList!.Add(new Book("射雕英雄", "金庸"));
            bookList!.Add(new Book("神雕侠侣", "金庸"));
            bookList!.Add(new Book("天龙八部", "金庸"));

            Console.WriteLine("已创建一个List<Book>集合实例bookList，输出集合数据：");
            Console.WriteLine($"【集合】实例：bookList | 元素数量：{bookList.Count} | 集合容量：{bookList.Capacity}");
            foreach (Book book in bookList)
            {
                Console.WriteLine($" |---【元素】{book}");
            }
            Console.WriteLine();
            //下方注释了的代码添加了重复项，如果添加到集合会触发集合内定义了的排重机制引发异常
            //bookList!.Add(new Book("天龙八部", "金庸"));

            //使用Insert方法继续插入十个数据
            bookList.Insert(1, new Book("小李飞刀", "古龙"));
            bookList.Insert(3, new Book("圆月弯刀", "古龙"));
            bookList.Insert(5, new Book("楚 留 香", "古龙"));
            bookList.Insert(7, new Book("斗破苍穹", "天蚕土豆"));
            bookList.Insert(9, new Book("西 游 记", "吴承恩"));
            bookList.Insert(11, new Book("三国演义", "罗贯中"));
            bookList.Insert(13, new Book("水 浒 传", "施耐庵"));
            bookList.Insert(15, new Book("红 楼 梦", "曹雪芹"));
            bookList.Insert(17, new Book("聊斋志异", "蒲松龄"));
            bookList.Insert(18, new Book("白发魔女", "梁羽生"));

            Console.WriteLine("数据插入完成，输出集合数据：");
            Console.WriteLine($"【集合】实例：bookList | 元素数量：{bookList.Count} | 集合容量：{bookList.Capacity}");
            foreach (Book book in bookList)
            {
                Console.WriteLine($" |---【元素】{book}");
            }
            Console.WriteLine();

            //使用Remove和RemoveAt方法分别移除一个数据
            Book book03 = bookList[3];
            Book book19 = bookList[19];

            bookList.RemoveAt(3);
            bool isRemoved = bookList.Remove(book19);

            Console.WriteLine($"已移除【元素】{book03}");
            if (isRemoved) Console.WriteLine($"已移除【元素】{book19}");

            Console.WriteLine("数据移除完成，输出集合数据：");
            Console.WriteLine($"【集合】实例：bookList | 元素数量：{bookList.Count} | 集合容量：{bookList.Capacity}");
            foreach (Book book in bookList)
            {
                Console.WriteLine($" |---【元素】{book}");
            }
            Console.WriteLine();

			//使用Sort方法对List进行排序
			bookList.Sort((x, y) => x.Author.GetHashCode() - y.Author.GetHashCode());

            Console.WriteLine("数据排序完成，输出集合数据：");
            Console.WriteLine($"【集合】实例：bookList | 元素数量：{bookList.Count} | 集合容量：{bookList.Capacity}");
            foreach (Book book in bookList)
            {
                Console.WriteLine($" |---【元素】{book}");
            }
            Console.WriteLine();

            //使用CopyTo方法将bookList数据复制到一个Book数组
            Book[] bookArray = new Book[20];
            bookList.CopyTo(bookArray, 0);

            Console.WriteLine("数据复制完成，输出数组数据：");
            Console.WriteLine($"【数组】实例：bookArray | 数组长度：{bookArray.Length}");
            foreach (Book book in bookArray)
            {
                if (book is not null)
                    Console.WriteLine($" |---【元素】{book}");
                else
                    Console.WriteLine($" |---【元素】NULL");
            }
            Console.WriteLine();

            //使用Clear方法清除bookList所有数据
            bookList.Clear();

            Console.WriteLine("数据清除完成，输出集合数据：");
            Console.WriteLine($"【集合】实例：bookList | 元素数量：{bookList.Count} | 集合容量：{bookList.Capacity}");
            Console.WriteLine();
        }
       
		/*【20402：用户定义的集合示例】
		 */
		public static void LearnCustomerCollection()
		{
            Console.WriteLine("\n------示例：自定义集合------\n");

			//实例化一个自定义的List<Book>集合,分别使用初始化器和Add方法添加一些元素
			Books books = new Books()
			{
				new Book("鬼 吹 灯","天下霸唱"),
                new Book("迷踪之国","天下霸唱"),
                new Book("死亡循环","天下霸唱"),
                new Book("盗墓笔记","南派三叔"),
                new Book("老 九 门","南派三叔"),
                new Book("斗罗大陆","唐家三少"),
                new Book("神印王座","唐家三少")
            };

			books!.Add(new Book("射雕英雄", "金庸"));
			books!.Add(new Book("神雕侠侣", "金庸"));
			books!.Add(new Book("天龙八部", "金庸"));

            Console.WriteLine("已创建一个Books集合实例books，输出集合数据：");
            Console.WriteLine($"【集合】实例：books | 元素数量：{books.Count} | 集合容量：{books.Capacity}");
			foreach (Book book in books) 
			{
                Console.WriteLine($" |---【元素】{book}");
			}
            Console.WriteLine();
			//下方注释了的代码添加了重复项，如果添加到集合会触发集合内定义了的排重机制引发异常
			//books!.Add(new Book("天龙八部", "金庸"));

			//使用Insert方法继续插入十个数据
			books.Insert(1, new Book("小李飞刀", "古龙"));
            books.Insert(3, new Book("圆月弯刀", "古龙"));
            books.Insert(5, new Book("楚 留 香", "古龙"));
            books.Insert(7, new Book("斗破苍穹", "天蚕土豆"));
            books.Insert(9, new Book("西 游 记", "吴承恩"));
            books.Insert(11, new Book("三国演义", "罗贯中"));
            books.Insert(13, new Book("水 浒 传", "施耐庵"));
            books.Insert(15, new Book("红 楼 梦", "曹雪芹"));
            books.Insert(17, new Book("聊斋志异", "蒲松龄"));
            books.Insert(18, new Book("白发魔女", "梁羽生"));

            Console.WriteLine("数据插入完成，输出集合数据：");
            Console.WriteLine($"【集合】实例：books | 元素数量：{books.Count} | 集合容量：{books.Capacity}");
            foreach (Book book in books)
            {
                Console.WriteLine($" |---【元素】{book}");
            }
            Console.WriteLine();

			//使用Remove和RemoveAt方法分别移除一个数据
			Book book03 = books[3];
			Book book19 = books[19];

			books.RemoveAt(3);
			bool isRemoved = books.Remove(book19);

            Console.WriteLine($"已移除【元素】{book03}");
			if(isRemoved ) Console.WriteLine($"已移除【元素】{book19}");
            
			Console.WriteLine("数据移除完成，输出集合数据：");
            Console.WriteLine($"【集合】实例：books | 元素数量：{books.Count} | 集合容量：{books.Capacity}");
            foreach (Book book in books)
            {
                Console.WriteLine($" |---【元素】{book}");
            }
            Console.WriteLine();

			//使用CopyTo方法将books数据复制到一个Book数组
			Book[] bookArray = new Book[20];
			books.CopyTo(bookArray, 0);

            Console.WriteLine("数据复制完成，输出数组数据：");
            Console.WriteLine($"【数组】实例：bookArray | 数组长度：{bookArray.Length}");
            foreach (Book book in bookArray)
            {
				if(book is not null)
					Console.WriteLine($" |---【元素】{book}");
				else
					Console.WriteLine($" |---【元素】NULL");
            }
            Console.WriteLine();

			//使用Clear方法清除books所有数据
			books.Clear();

            Console.WriteLine("数据清除完成，输出集合数据：");
            Console.WriteLine($"【集合】实例：books | 元素数量：{books.Count} | 集合容量：{books.Capacity}");
            Console.WriteLine();

			//通过IsReadOnly属性构建只读的Books实例readonlyBooks
			Books readonlyBooks = new Books(true, bookArray);

            Console.WriteLine("已创建一个只读Books集合实例readonlyBooks，输出集合数据：");
            Console.WriteLine($"【集合】实例：readonlyBooks | 元素数量：{readonlyBooks.Count} | 集合容量：{readonlyBooks.Capacity} | 只读：{readonlyBooks.IsReadOnly}");
            foreach (Book book in readonlyBooks)
            {
                Console.WriteLine($" |---【元素】{book}");
            }
            Console.WriteLine();

            //对只读集合添加数据、插入数据、移除数据、清除数据均会引发异常
            Console.WriteLine("对只读集合实例readonlyBooks添加数据、插入数据、移除数据、清除数据：");
			try 
			{
                readonlyBooks.Add(new Book("千朵桃花一世开", "随宇而安"));
            }
			catch(Exception e)
			{
				Console.WriteLine($"添加数据 --- 错误：{e.Message}");
			}

            try
            {
                readonlyBooks.Insert(5,new Book("千朵桃花一世开", "随宇而安"));
            }
            catch (Exception e)
            {
                Console.WriteLine($"插入数据 --- 错误：{e.Message}");
            }

            try
            {
                readonlyBooks.RemoveAt(7);
            }
            catch (Exception e)
            {
                Console.WriteLine($"移除数据 --- 错误：{e.Message}");
            }

            try
            {
                readonlyBooks.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine($"清除数据 --- 错误：{e.Message}");
            }

            Console.WriteLine();
        }

        public static void StartLearnCollection()
        {
            string title = "001 内置集合（Dictionary<TKey, TValue>示例）\n" +
                "002 自定义集合";
			
            do
            {
                Console.WriteLine("【学习集合】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": ; break;
                    case "002": LearnCustomerCollection(); break;
                    default: Console.WriteLine("输入错误！"); break;
                }

                Console.WriteLine();
                Console.WriteLine("是否继续查询和运行本章节其他代码：直接按下Enter继续，否则即退出");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;
                Console.WriteLine("\n");
            }
            while (true);
        }
    }
}
