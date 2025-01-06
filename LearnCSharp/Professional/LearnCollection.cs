/*【学习集合】
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
using System.Collections.Generic;

namespace LearnCSharp.Professional.LearnCollectionsSpace
{
    /*定义一个学生类
     *内部重写了Object类中的Equals、GetHashCode和ToString方法
     *内部实现了IEquatable<T>接口
     *内部重载了==和!=运算符
     */
    public class Student:IEquatable<Student>
    {
        public int StuID { get; private set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public static int CurrentYear { get; } = DateTime.Now.Year;
        public Student(int stuID, string name, int age)
        {
            StuID = stuID;
            Name = name;
            Age = age;
        }

        public Student(string name, int age) : this(CurrentYear * 1_0000 + Random.Shared.Next(1, 1_0000), name, age) { }

        public Student(Student student)
        {
            this.StuID = student.StuID;
            this.Name = student.Name;
            this.Age = student.Age;
        }

        public void IntroduceSelf()
        {
            Console.WriteLine($"大家好，我是{Name},今年{Age}岁,我的学号是{StuID}");
        }

        public bool Equals(Student? other)
        {
            return other is not null ? this.StuID == other.StuID : false;
        }

        public override bool Equals(object? obj)
        {
            return obj is Student s ? Equals(s) : false;
        }

        public override int GetHashCode()
        {
            return StuID.GetHashCode();
        }

        public override string ToString()
        {
            return $"Student {{StuID = {StuID}, Name = {Name}, Age = {Age}}}";
        }

        public static bool operator ==(Student left, Student right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Student left, Student right)
        {
            return !left.Equals(right);
        }
    }

    /*定义一个学生集合，用于存储和管理学生实例
     *该类实现了IEnumerable<T>、ICollection<T>和IList<T>接口 
     */
    public class Students : IEnumerable<Student>
    {
        //定义一个常量来设置默认容量
        private const int defaultCapacity = 10;
        //定义一个内部学生数组用于存储学生实例
        private Student?[] students;
        //定义一个属性Count用于显示当前已存容量
        public int Count { get; private set; } = 0;
        //定义一个属性用来确保数组的动态最大容量
        public int Capacity
        {
            get => students.Length;
            set
            {
                if (value >= students.Length)
                {
                    Increase(value);
                }
                else
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
        public bool IsReadOnly { get; set; } = false;

        //定义一个索引器，用于Students集合的实例可以如数组一样访问
        public Student this[int index]
        {
            get
            {
                if (index < 0 || index >= students.Length)
                    throw new IndexOutOfRangeException(nameof(index));

                return students[index];
            }
            set
            {

                if (index < 0 || index >= students.Length)
                    throw new IndexOutOfRangeException(nameof(index));

                if (value is null)
                    throw new ArgumentNullException(nameof(value));

                if (this.Contains(value))
                    throw new ArgumentException(message: "已存在该学生，请确认输入无误！",nameof(value));

                students[index] = value;
            }
        }

        public Students()
        {
            students = new Student[defaultCapacity];
            Count = 0;
        }

        public Students(int capacity)
        {
            students = new Student[capacity];
            Count = 0;
        }

        public Students(params Student[] students)
        {
            if (students is null || students.Length == 0)
                throw new ArgumentNullException();

            if (students.Length <= defaultCapacity)
                this.students = new Student[defaultCapacity];
            else
                this.students = new Student[students.Length];
            Count = students.Length;
            Array.Copy(students, this.students, Count);            
        }

        private void Increase(int newCapacity)
        {
            var temp = new Student[newCapacity];
            if (students is not null)
                this.CopyTo(temp, 0);
            students = temp;
        }

        public void Add(Student item)
        {
            if (Count >= Capacity)
                Increase(Capacity * 2);

            if (this.Contains(item))
                throw new ArgumentException("已存在该学生", nameof(item));

            students[Count] = item;
            
            Count++;
        }

        public void Clear()
        {
            for (int i = 0; i < students.Length; i++)
            {
                students[i] = null;
            }
            students = new Student[defaultCapacity];
            Count = 0;
        }

        public bool Contains(Student item)
        {
            if (item is null) 
                return false;

            for (int i = 0; i < Count; i++)
            {
                if (students[i] == item)
                    return true;
            }

            return false;
        }

        public void CopyTo(Student[] array, int arrayIndex)
        {
            if (array is null) throw new ArgumentNullException("array");
            if (array.Length - arrayIndex < Count) throw new ArgumentException("目标数组容量不足");

            for (int i = arrayIndex; i < arrayIndex+Count; i++)
            {
                array[i] = students[i - arrayIndex];
            }
        }

        public IEnumerator<Student> GetEnumerator()
        {
            return new StudentEnumerator(this);
        }

        public int IndexOf(Student item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (item == students[i])
                    return i;
            }

            return -1;
        }

        public void Insert(int index, Student item)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException("index");
            if (Contains(item)) throw new ArgumentException("请勿重复插入同一学生实例", nameof(item));
            if (Count + 1 > Capacity) Increase(Capacity * 2);

            for (int i = Count; i > index; i--)
            {
                students[i] = students[i - 1];
            }
            students[index] = item;
            Count++;
        }

        public bool Remove(Student item)
        {
            if(Contains(item))
            {
                this.RemoveAt(this.IndexOf(item));
                return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException("index");

            for (int i = index; i < Count; i++)
            {
                students[i] = students[i + 1];
            }
            Count--;
        }

        /*这个属性的get访问器返回一个迭代器
         *这个迭代器用于本类支持foreach循环
        IEnumerator Enumerator
        {
            get
            {
                if(students is not null)
                    for (int i = 0; i < this.Count; i++)
                    {
                        yield return this[i];
                    }
            }
        }*/

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return $"Students --Count：{Count} --Capacity：{Capacity}";
        }

        /*定义一个包装学生类数组的枚举器类
        *该类实现了IEnumerator<T>接口
        *该类用于在迭代器中返回一个枚举器，而无需用户自定义迭代器
        */
        class StudentEnumerator : IEnumerator<Student>
        {
            private Students students;
            private int position = -1;

            public StudentEnumerator(Students students)
            {
                this.students = students;
            }
            public Student Current => students[position];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                position++;
                return position < students.Count;
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}

namespace LearnCSharp.Professional
{
    internal class LearnCollection
    {
        public static void LearnSystemCollections()
        {
            Console.WriteLine("以系统内置的集合Dictionary<Tkey,TValue>和自建Student类实例为例学习该内置集合");
            Dictionary<int, Student> students = new Dictionary<int, Student>(10);

            Console.WriteLine($"已经创建集合Dictionary<int, Student>的一个实例students --output：Dictionary<int, Student> {{--Count：{students.Count}}}");
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                Student student = new Student($"学生<{i + 1:0000}>", Random.Shared.Next(11, 19));
                students.Add(student.StuID, student);
            }

            Console.WriteLine($"使用Add方法随机添加十个Student实例进入集合students --output：Dictionary<int, Student> {{--Count：{students.Count}}}");
            Console.WriteLine();
            Console.WriteLine("遍历输出已添加学生信息：");

            foreach (var student in students)
            {
                Console.WriteLine($"Key：{student.Key}  Value：{student.Value}");
            }
            Console.WriteLine();

            for (int i = 10; i < 15; i++)
            {
                Student student = new Student($"学生<{i + 1:0000}>", Random.Shared.Next(11, 19));
                students.Add(student.StuID, student);
            }

            Console.WriteLine($"再次使用Add方法随机添加五个Student实例进入集合students --output：Dictionary<int, Student> {{--Count：{students.Count}}}");
            Console.WriteLine();
            Console.WriteLine("遍历输出已添加学生信息：");

            foreach (var student in students)
            {
                Console.WriteLine($"Key：{student.Key}  Value：{student.Value}");
            }
            Console.WriteLine();
            
            Student stu = students[students.First().Key];

            Console.WriteLine($"使用索引访问的方式读取students集合索引为{students.First().Key}的Student实例的信息：students[{students.First().Key}] \n--output：{stu}\n");
            Console.Write($"将该实例使用Add方法再次加入集合，由于Dictionary<Tkey,TValue>本身支持根据键去重，故结果输出：");
            try
            {
                students.Add(stu.StuID, stu);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();

            Console.WriteLine($"使用Remove方法将该Student实例从集合移除，移除结果：{students.Remove(stu.StuID)}");
            Console.WriteLine($"此时students实例信息：Dictionary<int, Student> {{--Count：{students.Count}}}");
            Console.WriteLine($"再次遍历集合内Student实例信息");

            foreach (var student in students)
            {
                Console.WriteLine($"Key：{student.Key}  Value：{student.Value}");
            }
            Console.WriteLine();

            Console.WriteLine($"使用Add方法再次将该Student实例插入集合");

            students.Add(stu.StuID, stu);

            Console.WriteLine($"此时students实例信息：Dictionary<int, Student> {{--Count：{students.Count}}}");
            Console.WriteLine($"再次遍历集合内Student实例信息");

            foreach (var student in students)
            {
                Console.WriteLine($"Key：{student.Key}  Value：{student.Value}");
            }
            Console.WriteLine();

            students.Clear();
            Console.WriteLine($"使用Clear方法清除集合，此时students实例信息：Dictionary<int, Student> {{--Count：{students.Count}}}");
        }

        /*【使用自定义集合代码示例】
         * 使用自定义集合并测试相关功能
         */
        public static void LearnCustomerCollections()
        {
            Students students = new Students(10);

            Console.WriteLine($"已经创建集合Students的一个实例students --output：{students}");
            Console.WriteLine();

            for (int i = 0; i < 10; i++)
            {
                Student student = new Student($"学生<{i + 1:0000}>", Random.Shared.Next(11, 19));
                students.Add(student);
            }

            Console.WriteLine($"使用Add方法随机添加十个Student实例进入集合students --output：{students}");
            Console.WriteLine();
            Console.WriteLine("遍历输出已添加学生信息：");

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();

            for (int i = 10; i < 15; i++)
            {
                Student student = new Student($"学生<{i + 1:0000}>", Random.Shared.Next(11, 19));
                students.Add(student);
            }

            Console.WriteLine($"再次使用Add方法随机添加五个Student实例进入集合students --output：{students}");
            Console.WriteLine();
            Console.WriteLine("遍历输出已添加学生信息：");

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();

            Student stu = students[5];

            Console.WriteLine($"使用索引访问的方式读取students集合索引为5即第六个Student实例的信息：students[5] \n--output：{stu}\n");
            Console.Write($"将该实例使用Add方法再次加入集合，由于加入了去重验证，故结果输出：");
            try 
            {
                students.Add(stu);
            }
            catch (ArgumentException e) 
            {
                Console.WriteLine(e.Message); 
            }
            Console.WriteLine();

            Console.Write($"将该实例使用Insert方法再次加入集合，由于加入了去重验证，故结果输出：");
            try
            {
                students.Insert(10, stu);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();

            Console.WriteLine($"使用Remove方法将该Student实例从集合移除，移除结果：{students.Remove(stu)}");
            Console.WriteLine($"此时students实例信息：{students}");
            Console.WriteLine($"再次遍历集合内Student实例信息");

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();

            Console.WriteLine($"使用Insert方法再次将该Student实例插入集合原位置");

            students.Insert(5, stu);

            Console.WriteLine($"此时students实例信息：{students}");
            Console.WriteLine($"再次遍历集合内Student实例信息");

            foreach (var student in students)
            {
                Console.WriteLine(student);
            }
            Console.WriteLine();

            students.Clear();
            Console.WriteLine($"使用Clear方法清除集合，此时students实例信息：{students}");
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
                    case "001": LearnSystemCollections(); break;
                    case "002": LearnCustomerCollections(); break;
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
