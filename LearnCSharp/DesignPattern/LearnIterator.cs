/*【316：行为型————迭代器模式】
 * 迭代器模式（Iterator Pattern）是一种对象行为型设计模式，它提供了一种方法来顺序访问一个集合对象中的元素，而不暴露该对象的内部表示。
 * 迭代器模式通常用于集合类，允许客户端以统一的方式遍历不同类型的集合。
 * 迭代器模式的主要优点是可以将遍历逻辑与集合类的实现分离，使得代码更加清晰和易于维护。同时，迭代器模式也可以提高代码的复用性，因为不同的集合类可以使用相同的迭代器接口来遍历元素。
 * 迭代器模式的主要缺点是增加了类的数量，因为每个集合类都需要实现一个迭代器接口。同时，迭代器模式也可能导致代码的复杂性增加，因为需要维护多个迭代器和集合类之间的关系。
 * 迭代器模式的主要组成部分包括：
 *  1. 迭代器（Iterator）：定义了访问和遍历元素的接口。
 *  2. 聚合（Aggregate）：定义了创建迭代器的方法。
 *  3. 具体迭代器（Concrete Iterator）：实现了迭代器接口，维护当前遍历的位置。
 *  4. 具体聚合（Concrete Aggregate）：实现了聚合接口，返回一个具体迭代器的实例。
 * 迭代器模式的实现步骤如下：
 *  1. 定义一个迭代器接口，声明访问和遍历元素的方法。
 *  2. 定义一个聚合接口，声明创建迭代器的方法。
 *  3. 定义一个具体迭代器类，实现迭代器接口，维护当前遍历的位置。
 *  4. 定义一个具体聚合类，实现聚合接口，返回一个具体迭代器的实例。
 *  5. 客户端使用具体聚合类来创建迭代器，并使用迭代器来遍历元素。
 *  6. 客户端可以通过迭代器来访问集合中的元素，而不需要关心集合的具体实现细节。
 */

using System.Collections;
using LearnCSharp.DesignPattern.LearnIteratorSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnIterator
    {
        /*【31601：迭代器模式】*/
        public static void LearnIteratorDesignPattern()
        {
            Console.WriteLine("\n------示例：迭代器模式------\n");
            Console.WriteLine("》》》使用迭代器模式遍历一个整数集合《《《");
            Console.WriteLine("-----------------------------------------------");
            
            // 创建一个整数集合
            IntegerCollection collection = new IntegerCollection();
            for (int i = 0; i < 20; i++)
            {
                collection.AddItem(Random.Shared.Next(100, 1000));
            }
            
            // 创建一个迭代器
            IIterator<int> iterator = collection.CreateIterator();
            
            // 遍历集合
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.Next());
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31602：.NET中的迭代器模式】
         * 示例：使用.NET中的迭代器模式遍历一个整数集合
         */
        public static void LearnDotNetIteratorDesignPattern()
        {
            Console.WriteLine("\n------示例：.NET中的迭代器模式------\n");
            Console.WriteLine("》》》使用.NET中的迭代器模式遍历一个整数集合《《《");
            Console.WriteLine("-----------------------------------------------");
            
            // 创建一个整数集合
            CustomCollection collection = new CustomCollection();
            for (int i = 0; i < 20; i++)
            {
                collection.Add(Random.Shared.Next(100, 1000));
            }

            // 使用foreach遍历集合
            Console.WriteLine("使用foreach遍历集合");
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            // 使用自定义迭代器遍历集合
            Console.WriteLine("使用自定义迭代器遍历集合");
            IEnumerator<int> enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }
            enumerator.Reset(); // 重置迭代器

            Console.WriteLine();

            // 使用yield return遍历集合
            Console.WriteLine("使用yield return遍历集合");
            foreach (var item in collection.GetEnumeratorByYieldReturn())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnIteratorSpace
{
    #region 迭代器模式基础结构
    /*【31600：迭代器模式基础结构】*/
    public interface IIterator<T> // 迭代器接口
    {
        bool HasNext(); // 判断是否还有下一个元素
        T Next(); // 获取下一个元素
    }

    public interface IAggregate<T> // 聚合接口
    {
        int Count { get; } // 获取元素数量
        void AddItem(T item); // 添加元素
        T GetItem(int index); // 获取指定索引的元素
        IIterator<T> CreateIterator(); // 创建迭代器
    }

    public class ConcreteIterator<T> : IIterator<T> // 具体迭代器
    {
        private readonly IAggregate<T> aggregate;
        private int currentIndex = 0;
        public ConcreteIterator(IAggregate<T> aggregate)
        {
            this.aggregate = aggregate;
        }
        public bool HasNext()
        {
            return currentIndex < aggregate.Count;
        }
        public T Next()
        {
            return aggregate.GetItem(currentIndex++);
        }
    }

    public class ConcreteAggregate<T> : IAggregate<T> // 具体聚合
    {
        private readonly List<T> items = new List<T>();
        public int Count => items.Count;
        public void AddItem(T item)
        {
            items.Add(item);
        }
        public T GetItem(int index)
        {
            return items[index];
        }
        public IIterator<T> CreateIterator()
        {
            return new ConcreteIterator<T>(this);
        }
    }
    #endregion

    #region 迭代器模式示例
    /*【31601：迭代器模式】
     * 示例：使用迭代器模式遍历一个整数集合
     */
    public class IntegerCollection : IAggregate<int> // 整数集合
    {
        private readonly List<int> items = new List<int>();
        public int Count => items.Count;
        public void AddItem(int item)
        {
            items.Add(item);
        }
        public int GetItem(int index)
        {
            return items[index];
        }
        public IIterator<int> CreateIterator()
        {
            return new ConcreteIterator<int>(this);
        }
    }

    public class IntegerIterator : IIterator<int>
    {
        private readonly IntegerCollection collection;
        private int currentIndex = 0;

        public IntegerIterator(IntegerCollection collection)
        {
            this.collection = collection;
        }

        public bool HasNext()
        {
            return currentIndex < collection.Count;
        }

        public int Next()
        {
            return collection.GetItem(currentIndex++);
        }
    }
    #endregion

    #region .NET中的迭代器模式
    /*【31602：.NET中的迭代器模式】
     * 在.NET中，迭代器模式通常通过实现IEnumerable和IEnumerator接口来实现。
     * IEnumerable接口定义了一个GetEnumerator方法，返回一个IEnumerator对象。
     * IEnumerator接口定义了用于遍历集合的方法，如MoveNext、Reset和Current属性。
     * 通过实现这两个接口，可以轻松地创建自定义集合类，并使用foreach语句遍历集合中的元素。
     */
    public class CustomCollection : IEnumerable<int> // 自定义集合
    {
        private readonly List<int> items = new List<int>();

        public int Count => items.Count;

        public void Add(int item)
        {
            items.Add(item);
        }

        public IEnumerable<int> GetEnumeratorByYieldReturn()
        {
            for (int i = 0; i < items.Count; i++)
            {
                yield return items[i];
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new CustomEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class CustomEnumerator : IEnumerator<int> // 自定义枚举器
        {
            private readonly CustomCollection collection;
            private int currentIndex = -1;

            public CustomEnumerator(CustomCollection collection)
            {
                this.collection = collection;
            }

            public int Current => collection.items[currentIndex];

            object IEnumerator.Current => Current;


            public bool MoveNext()
            {
                currentIndex++;
                return currentIndex < collection.Count;
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            public void Dispose()
            {
                // 释放资源
            }
        }
    }
    #endregion
}
