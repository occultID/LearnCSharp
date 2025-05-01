/*【301：创建型————单例模式】*/
using LearnCSharp.DesignPattern.LearnSingletonSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnSingleton
    {
        private static bool isRestart = true;

        /*【30101：单例模式-饿汉式-单线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByEager()
        {
            //饿汉式单例
            if(isRestart)
            {
                Console.WriteLine("\n------示例：饿汉式单例(单线程)------\n");

                Console.WriteLine("》》》单线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 15; i++)
                {
                    var instace = SingletonByEager.GetSingleton();//获取单例对象
                    Console.WriteLine($"主线程 第{i:00}次 获取单例对象{i + 1:00}：{instace.GetHashCode()}");
                }

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30102：单例模式-饿汉式-多线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByEagerByMultiThread()
        {
            //饿汉式单例
            if (isRestart)
            {
                Console.WriteLine("\n------示例：饿汉式单例(多线程)------\n");

                Console.WriteLine("》》》多线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                Thread[] threads = new Thread[20];

                for (int i = 0; i < 20; i++)
                {
                    int index = i;
                    threads[i] = new Thread(() =>
                    {
                        var instace = SingletonByLazy.GetSingletonUnsafe();//获取单例对象
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取单例对象{index + 1:00}：{instace.GetHashCode()}");
                    });
                }
                Array.ForEach(threads, t => t.Start());//启动线程
                Array.ForEach(threads, t => t.Join());//等待线程结束

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30103：单例模式-懒汉式-无锁示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByLazyUnsafe()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：懒汉式单例------\n");

                List<SingletonByLazy> singletons = new List<SingletonByLazy>(20);
                Thread[] threads = new Thread[20];

                Console.WriteLine("》》》单线程创建多个单例对象(无锁·单线程)《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 20; i++)
                {
                    var instace = SingletonByLazy.GetSingletonUnsafe();//获取单例对象
                    Console.WriteLine($"主线程 第{i:00}次 获取单例对象{i + 1:00}：{instace.GetHashCode()}");
                    singletons.Add(instace);
                }

                singletons.ForEach(info =>
                {
                    info.Release();
                    info = null;
                });
                singletons.Clear();//释放资源

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();

                Console.ReadKey();

                Console.WriteLine("》》》多线程创建多个单例对象(无锁·多线程)《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 20; i++)
                {
                    int index = i;
                    threads[i] = new Thread(() =>
                    {
                        var instace = SingletonByLazy.GetSingletonUnsafe();//获取单例对象
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取单例对象{index + 1:00}：{instace.GetHashCode()}");
                        singletons.Add(instace);
                    });
                }
                Array.ForEach(threads, t => t.Start());//启动线程
                Array.ForEach(threads, t => t.Join());//等待线程结束

                singletons.ForEach(info =>
                {
                    info.Release();
                    info = null;
                });
                singletons.Clear();//释放资源

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30104：单例模式-懒汉式-单检有锁示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByLazySingleCheck()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：懒汉式单例------\n");

                List<SingletonByLazy> singletons = new List<SingletonByLazy>(20);
                Thread[] threads = new Thread[20];

                Console.WriteLine("》》》单线程创建多个单例对象(单检测·有锁·单线程)《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 20; i++)
                {
                    var instace = SingletonByLazy.GetSingletonBySingleCheck();//获取单例对象
                    Console.WriteLine($"主线程 第{i:00}次 获取单例对象{i + 1:00}：{instace.GetHashCode()}");
                    singletons.Add(instace);
                }

                singletons.ForEach(info =>
                {
                    info.Release();
                    info = null;
                });
                singletons.Clear();//释放资源

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();

                Console.ReadKey();

                Console.WriteLine("》》》多线程创建多个单例对象(单检测·有锁·多线程)《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 20; i++)
                {
                    int index = i;
                    threads[i] = new Thread(() =>
                    {
                        var instace = SingletonByLazy.GetSingletonBySingleCheck();//获取单例对象
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取单例对象{index + 1:00}：{instace.GetHashCode()}");
                        singletons.Add(instace);
                    });
                }
                Array.ForEach(threads, t => t.Start());//启动线程
                Array.ForEach(threads, t => t.Join());//等待线程结束

                singletons.ForEach(info =>
                {
                    info.Release();
                    info = null;
                });
                singletons.Clear();//释放资源

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30105：单例模式-懒汉式-双检有锁示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByLazyDoubleCheck()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：懒汉式单例------\n");

                List<SingletonByLazy> singletons = new List<SingletonByLazy>(20);
                Thread[] threads = new Thread[20];

                Console.WriteLine("》》》单线程创建多个单例对象(单检测·有锁·单线程)《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 20; i++)
                {
                    var instace = SingletonByLazy.GetSingletonByDoubleCheck();//获取单例对象
                    Console.WriteLine($"主线程 第{i:00}次 获取单例对象{i + 1:00}：{instace.GetHashCode()}");
                    singletons.Add(instace);
                }

                singletons.ForEach(info =>
                {
                    info.Release();
                    info = null;
                });
                singletons.Clear();//释放资源

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine();

                Console.ReadKey();

                Console.WriteLine("》》》多线程创建多个单例对象(单检测·有锁·多线程)《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 20; i++)
                {
                    int index = i;
                    threads[i] = new Thread(() =>
                    {
                        var instace = SingletonByLazy.GetSingletonByDoubleCheck();//获取单例对象
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取单例对象{index + 1:00}：{instace.GetHashCode()}");
                        singletons.Add(instace);
                    });
                }
                Array.ForEach(threads, t => t.Start());//启动线程
                Array.ForEach(threads, t => t.Join());//等待线程结束

                singletons.ForEach(info =>
                {
                    info.Release();
                    info = null;
                });
                singletons.Clear();//释放资源

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30106：单例模式-内部静态类-单线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByInnerStaitcClass()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：内部静态类单例(单线程)------\n");

                Console.WriteLine("》》》单线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 15; i++)
                {
                    var instace = SingletonByInnerStaticClass.GetSingleton();//获取单例对象
                    Console.WriteLine($"主线程 第{i:00}次 获取单例对象{i + 1:00}：{instace.GetHashCode()}");
                }

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();

        }

        /*【30107：单例模式-内部静态类-多线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByInnerStaitcClassByMultiThread()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：内部静态类单例(多线程)------\n");

                Console.WriteLine("》》》多线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                Thread[] threads = new Thread[20];

                for (int i = 0; i < 20; i++)
                {
                    int index = i;
                    threads[i] = new Thread(() =>
                    {
                        var instace = SingletonByInnerStaticClass.GetSingleton();//获取单例对象
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取单例对象{index + 1:00}：{instace.GetHashCode()}");
                    });
                }
                Array.ForEach(threads, t => t.Start());//启动线程
                Array.ForEach(threads, t => t.Join());//等待线程结束

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30108：单例模式-懒加载模式-单线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByDotNetLazy()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：懒加载式单例(单线程)------\n");

                Console.WriteLine("》》》单线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 15; i++)
                {
                    var instace = SingletonByDotNetLazy.GetSingleton();//获取单例对象
                    Console.WriteLine($"主线程 第{i:00}次 获取单例对象{i + 1:00}：{instace.GetHashCode()}");
                }

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();

        }

        /*【30109：单例模式-懒加载模式-多线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonByDotNetLazyByMultiThread()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：懒加载式单例(多线程)------\n");

                Console.WriteLine("》》》多线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                Thread[] threads = new Thread[20];

                for (int i = 0; i < 20; i++)
                {
                    int index = i;
                    threads[i] = new Thread(() =>
                    {
                        var instace = SingletonByDotNetLazy.GetSingleton();//获取单例对象
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取单例对象{index + 1:00}：{instace.GetHashCode()}");
                    });
                }
                Array.ForEach(threads, t => t.Start());//启动线程
                Array.ForEach(threads, t => t.Join());//等待线程结束

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30110：泛型单例模式-懒加载模式-单线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonGeneric()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：懒加载式单例(单线程)------\n");

                Console.WriteLine("》》》单线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                for (int i = 0; i < 15; i++)
                {
                    var instace = Singleton<object>.GetInstance();//获取单例对象
                    Console.WriteLine($"主线程 第{i:00}次 获取单例对象{i + 1:00}：{instace.GetHashCode()}");
                }

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        /*【30111：泛型单例模式-懒加载模式-多线程示例】
         * 每次测试请重启程序，避免已存在的单例对象影响测试结果
         */
        public static void LearnSingletonGenericByMultiThread()
        {
            if (isRestart)
            {
                Console.WriteLine("\n------示例：懒加载式单例(多线程)------\n");

                Console.WriteLine("》》》多线程创建多个单例对象《《《");
                Console.WriteLine("-----------------------------------------------");

                Thread[] threads = new Thread[20];

                for (int i = 0; i < 20; i++)
                {
                    int index = i;
                    threads[i] = new Thread(() =>
                    {
                        var instace = Singleton<object>.GetInstance();//获取单例对象
                        Console.WriteLine($"【线程 {Thread.CurrentThread.ManagedThreadId:00}】获取单例对象{index + 1:00}：{instace.GetHashCode()}");
                    });
                }
                Array.ForEach(threads, t => t.Start());//启动线程
                Array.ForEach(threads, t => t.Join());//等待线程结束

                Console.WriteLine("-----------------------------------------------");
            }
            else
                Console.WriteLine("请重启程序后再进行测试");

            Console.WriteLine();
            RestartToTestSingleton();
        }

        private static void RestartToTestSingleton()
        {
            while (true)
            {
                Console.WriteLine("是否需要重启程序来继续测试单例模式内容：Y/N");

                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.Y)
                {
                    ReStart.ReStartConsole();
                    break;
                }
                else
                {
                    isRestart=false;
                    break;
                }
            }
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnSingletonSpace
{
    /*【30100：单例模式的使用】
     * 单例模式的使用场景：
     * 1. 需要确保类只有一个实例时
     * 2. 需要提供全局访问点时
     * 3. 需要控制实例数量时
     * 4. 需要延迟加载时
     * 5. 需要共享资源时
     */
    // 例如：数据库连接池、线程池、日志记录器等
    //--------------------------------------------------------------------------------------


    /*【30101：饿汉式单例】
     * 饿汉式单例模式在类加载时就创建实例，确保线程安全。
     * 优点：简单易用，线程安全。
     * 缺点：如果实例创建开销大，可能会浪费资源。
     */
    public sealed class SingletonByEager
    {
        //私有构造函数，防止外部实例化
        private SingletonByEager() { }

        //饿汉式单例模式，直接创建实例
        private static readonly SingletonByEager instance = new SingletonByEager();

        public static SingletonByEager GetSingleton() { return instance; }
    }

    /*【30102：懒汉式单例】
     * 懒汉式单例模式在第一次使用时创建实例，确保线程安全。
     * 优点：节省资源，只有在需要时才创建实例。
     * 缺点：实现复杂，可能会出现线程安全问题。
     */
    public sealed class SingletonByLazy
    {
        private static readonly object objLock = new object();
        private SingletonByLazy() { }

        //懒汉式单例模式，实例在第一次使用时创建
        //volatile关键字用于确保多线程环境下避免指针重排
        private static volatile SingletonByLazy instance;

        //写一个方法暴露给外界，进行单例检测，但不进行线程安全检测
        public static SingletonByLazy GetSingletonUnsafe()
        {
            return instance ??= new SingletonByLazy();//如果实例为空，则创建实例
        }

        //写一个方法暴露给外界，进行单例检测和线程安全检测
        public static SingletonByLazy GetSingletonBySingleCheck()
        {
            lock (objLock)
            {
                instance ??= new SingletonByLazy();//如果实例为空，则创建实例
            }
            return instance;
        }

        //写一个方法暴露给外界，进行双重单例检测和线程安全检测
        public static SingletonByLazy GetSingletonByDoubleCheck()
        {
            if (instance == null)
            {
                lock (objLock)
                {
                    instance ??= new SingletonByLazy();//如果实例为空，则创建实例
                }
            }
            return instance;
        }

        public void Release()
        {
            //释放资源
            instance = null;
        }
    }

    /*【30103：内部静态类单例】
     * 内部静态类单例模式在第一次使用时创建实例，确保线程安全。
     * 优点：简单易用，线程安全。
     * 缺点：如果实例创建开销大，可能会浪费资源。
     */
    public sealed class SingletonByInnerStaticClass
    {
        private SingletonByInnerStaticClass() { }

        private static class InnerClass
        {
            public static readonly SingletonByInnerStaticClass instance = new SingletonByInnerStaticClass();
        }

        public static SingletonByInnerStaticClass GetSingleton()
        {
            return InnerClass.instance;
        }
    }

    /*【30104：懒加载式单例】
     * 懒加载式单例模式在第一次使用时创建实例，确保线程安全。
     * 优点：节省资源，只有在需要时才创建实例。
     * 
     */
    public sealed class SingletonByDotNetLazy
    {
        private SingletonByDotNetLazy() { }
        //使用Lazy<T>类实现线程安全
        private static readonly Lazy<SingletonByDotNetLazy> instance = new Lazy<SingletonByDotNetLazy>(() => new SingletonByDotNetLazy());

        public static SingletonByDotNetLazy GetSingleton()
        {
            return instance.Value;
        }
    }

    /*【30105：泛型单例模式】
     * 泛型单例模式在第一次使用时创建实例，确保线程安全。
     * 优点：节省资源，只有在需要时才创建实例。
     *      可以创建多个不同类型的单例对象。
     *      可以为本身未提供单例模式的类提供单例模式。
     * 
     */
    public sealed class Singleton<T> where T : new()
    {
        private Singleton() { }

        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T GetInstance()
        {
            return instance.Value;
        }
    } 
}
