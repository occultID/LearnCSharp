/*【314：行为型————策略模式】
 * 策略模式（Strategy Pattern）定义了一系列算法，将每一个算法封装起来，并使它们可以互换。此模式让算法独立于使用它的客户而独立变化。
 * 策略模式通常用于以下场景：
 *  1. 当有多个类只区别在于行为时，可以使用策略模式来抽象出公共的行为。
 *  2. 当需要使用不同的算法来处理同一类问题时，可以使用策略模式来封装这些算法。
 *  3. 当需要动态地选择算法时，可以使用策略模式来实现。
 *  4. 当需要避免使用多重条件语句时，可以使用策略模式来替代。
 *  5. 当需要将算法的实现与使用算法的代码分离时，可以使用策略模式来实现。
 *  6. 当需要在运行时选择算法时，可以使用策略模式来实现。
 *  7. ……
 * 策略模式的优点：
 *  1. 策略模式可以避免使用多重条件语句，使代码更加清晰易懂。
 *  2. 策略模式可以将算法的实现与使用算法的代码分离，使代码更加灵活。
 *  3. 策略模式可以在运行时选择算法，使代码更加灵活。
 *  4. 策略模式可以将算法的实现与使用算法的代码分离，使代码更加灵活。
 * 策略模式的缺点：
 *  1. 策略模式会增加类的数量，使代码更加复杂。
 * 策略模式的主要组成部分：
 *  1. 抽象策略类（Strategy）：定义了一个接口，用于所有支持的算法。
 *  2. 具体策略类（ConcreteStrategy）：实现了抽象策略类的接口，提供具体的算法实现。
 *  3. 上下文类（Context）：持有一个策略类的引用，使用该策略类来完成某个任务。
 *  4. 客户端（Client）：使用上下文类来完成某个任务。
 * 策略模式的实现步骤：
 *  1. 定义一个抽象策略类，定义一个接口，用于所有支持的算法。
 *  2. 定义一个具体策略类，实现抽象策略类的接口，提供具体的算法实现。
 *  3. 定义一个上下文类，持有一个策略类的引用，使用该策略类来完成某个任务。
 *  4. 定义一个客户端，使用上下文类来完成某个任务。
 *  5. 在客户端中，创建一个上下文类的实例，并设置一个具体策略类的实例，然后调用上下文类的方法来完成某个任务。
 */

using LearnCSharp.DesignPattern.LearnStrategySpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnStrategy
    {
        /*【31401：基础策略模式】*/
        public static void LearnBasicStrategy()
        {
            Console.WriteLine("\n------示例：基础策略模式------\n");
            Console.WriteLine("》》》通过策略模式动态选择排序算法《《《");
            Console.WriteLine("-----------------------------------------------");

            //创建两个数组
            int[] array1 = { 5, 2, 9, 1, 5, 6 };
            int[] array2 = array1.ToArray(); //复制数组

            //创建策略
            var bubbleSort = new BubbleSort();
            var quickSort = new QuickSort();

            //创建排序上下文对象并设置默认策略
            var sortContext = new SortContext(bubbleSort);

            Console.WriteLine("默认排序策略：冒泡排序");
            Console.WriteLine("排序前：{0}", string.Join(", ", array1));
            sortContext.Sort(array1);
            Console.WriteLine("排序后：{0}", string.Join(", ", array1));

            Console.WriteLine();

            //切换策略
            sortContext.SetSortingStrategy(quickSort);

            Console.WriteLine("快速排序");
            Console.WriteLine("排序前：{0}", string.Join(", ", array2));
            sortContext.Sort(array2);
            Console.WriteLine("排序后：{0}", string.Join(", ", array2));

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31402：策略工厂模式】*/
        public static void LearnStrategyFactory()
        {
            Console.WriteLine("\n------示例：策略工厂模式------\n");
            Console.WriteLine("》》》通过策略工厂模式动态选择排序算法《《《");
            Console.WriteLine("-----------------------------------------------");

            //创建两个数组
            int[] array1 = { 5, 2, 9, 1, 5, 6 };
            int[] array2 = array1.ToArray(); //复制数组
            
            //创建排序上下文对象
            var enhancedSortContext = new EnhancedSortingContext();
            
            //使用冒泡排序
            Console.WriteLine("使用冒泡排序");
            Console.WriteLine("排序前：{0}", string.Join(", ", array1));
            
            enhancedSortContext.Sort(array1, SortingType.BubbleSort);
            
            Console.WriteLine("排序后：{0}", string.Join(", ", array1));
            Console.WriteLine();
            
            //使用快速排序
            Console.WriteLine("使用快速排序");
            Console.WriteLine("排序前：{0}", string.Join(", ", array2));
            
            enhancedSortContext.Sort(array2, SortingType.QuickSort);
            
            Console.WriteLine("排序后：{0}", string.Join(", ", array2));
            Console.WriteLine("-----------------------------------------------");
        }

        /*【31403：.NET内置策略模式】*/
        public static void LearnDotNetStrategy()
        {
            Console.WriteLine("\n------示例：.NET内置策略模式------\n");
            Console.WriteLine("》》》通过委托参数动态选择排序算法《《《");
            Console.WriteLine("-----------------------------------------------");
            
            //创建两个数组
            int[] array1 = { 5, 2, 9, 1, 5, 6 };
            int[] array2 = array1.ToArray(); //复制数组
            
            //创建排序上下文对象
            var delegateSortContext = new DelegateSortingContext();
            
            //使用冒泡排序
            Console.WriteLine("使用冒泡排序");
            Console.WriteLine("排序前：{0}", string.Join(", ", array1));

            delegateSortContext.Sort(new BubbleSort().Sort, array1);

            Console.WriteLine("排序后：{0}", string.Join(", ", array1));
            Console.WriteLine();
            
            //使用快速排序
            Console.WriteLine("使用快速排序");
            Console.WriteLine("排序前：{0}", string.Join(", ", array2));

            delegateSortContext.Sort(new QuickSort().Sort, array2);

            Console.WriteLine("排序后：{0}", string.Join(", ", array2));
            Console.WriteLine("-----------------------------------------------");
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnStrategySpace
{
    #region 策略模式基础结构
    /*【31400：策略模式基础结构】*/
    public interface IStrategy //抽象策略类：策略接口
    {
        void Algorithm(); //算法方法
    }

    public class ConcreteStrategyA : IStrategy// 具体策略类A
    {
        public void Algorithm()
        {
            Console.WriteLine("使用算法A");
        }
    }
    
    public class ConcreteStrategyB : IStrategy// 具体策略类B
    {
        public void Algorithm()
        {
            Console.WriteLine("使用算法B");
        }
    }

    public class Context //上下文类
    {
        private IStrategy strategy; //持有一个策略类的引用

        public Context(IStrategy strategy) //构造函数
        {
            this.strategy = strategy;
        }

        public void SetStrategy(IStrategy strategy) //设置策略
        {
            this.strategy = strategy;
        }

        public void ExecuteAlgorithm() //执行算法
        {
            strategy.Algorithm();
        }
    }
    #endregion

    #region 基础策略模式示例
    /*【31401：策略模式示例】
     * 基础策略模式示例
     * 特点：算法实现与使用算法的代码分离
     *      符合开闭原则
     *      运行时动态切换策略
     */
    public interface ISortingStrategy //排序策略接口
    {
        void Sort(int[] array); //排序方法
    }

    public class BubbleSort : ISortingStrategy //冒泡排序
    {
        public void Sort(int[] array)
        {
            Console.WriteLine("使用冒泡排序");
            // 冒泡排序算法实现
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
        }
    }

    public class QuickSort : ISortingStrategy //快速排序
    {
        public void Sort(int[] array)
        {
            Console.WriteLine("使用快速排序");
            QuickSortHelper(array, 0, array.Length - 1);
        }

        private void QuickSortHelper(int[] array, int low, int high)
        {
            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);
                QuickSortHelper(array, low, pivotIndex - 1);
                QuickSortHelper(array, pivotIndex + 1, high);
            }
        }

        private int Partition(int[] array, int low, int high)
        {
            int pivot = array[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
                {
                    i++;
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
            int temp1 = array[i + 1];
            array[i + 1] = array[high];
            array[high] = temp1;
            return i + 1;
        }
    }

    public class SortContext //排序上下文类
    {
        private ISortingStrategy sortingStrategy; //持有一个排序策略的引用
        public SortContext(ISortingStrategy sortingStrategy) //构造函数
        {
            this.sortingStrategy = sortingStrategy;
        }
        public void SetSortingStrategy(ISortingStrategy sortingStrategy) //设置排序策略
        {
            this.sortingStrategy = sortingStrategy;
        }
        public void Sort(int[] array) //执行排序
        {
            sortingStrategy.Sort(array);
        }
    }
    #endregion

    #region 策略工厂模式
    /*【31402：策略工厂模式】
     * 策略工厂模式
     * 特点：算法实现与使用算法的代码分离
     *      符合开闭原则
     *      运行时动态切换策略
     */
    public enum SortingType //排序类型枚举
    {
        BubbleSort,
        QuickSort
    }

    public class SortingStrategyFactory //排序策略工厂
    {
        public ISortingStrategy GetSortingStrategy(SortingType sorting) => sorting switch
        {
            SortingType.BubbleSort => new BubbleSort(),
            SortingType.QuickSort => new QuickSort(),
            _ => throw new ArgumentException("无效的排序类型")
        };
    }

    public class EnhancedSortingContext
    {
        private SortingStrategyFactory strategyFactory = new SortingStrategyFactory(); //排序策略工厂

        public void Sort(int[] array, SortingType sortingType) //执行排序
        {
            ISortingStrategy sortingStrategy = strategyFactory.GetSortingStrategy(sortingType);
            sortingStrategy.Sort(array);
        }
    }
    #endregion

    #region .NET内置策略模式 ———— 委托参数
    /*【31403：.NET内置策略模式】
     * .NET内置策略模式
     * 特点：算法实现与使用算法的代码分离
     *      符合开闭原则
     *      运行时动态切换策略
     */
    public class DelegateSortingContext //委托排序上下文类
    {
        public void Sort(Action<int[]> sortStrategy, int[] array) //执行排序
        {
            sortStrategy.Invoke(array);
        }
    }
    #endregion
}
