/*【310：结构型————代理模式】
 * 代理模式（Proxy Pattern）是一种结构型设计模式，它为其他对象提供一种代理以控制对这个对象的访问。
 * 代理模式通常用于以下场景：
 *  1. 当需要控制对某个对象的访问时，可以使用代理模式来提供一个代理对象。
 *  2. 当需要在访问某个对象之前或之后执行一些操作时，可以使用代理模式来提供一个代理对象。
 *  3. 当需要延迟加载某个对象时，可以使用代理模式来提供一个代理对象。
 *  4. 当需要在访问某个对象时进行权限控制时，可以使用代理模式来提供一个代理对象。
 *  5. 当需要在访问某个对象时进行日志记录时，可以使用代理模式来提供一个代理对象。
 *  6. 当需要在访问某个对象时进行性能监控时，可以使用代理模式来提供一个代理对象。
 *  7. 当需要在访问某个对象时进行缓存时，可以使用代理模式来提供一个代理对象。
 *  8. 当需要在访问某个对象时进行事务控制时，可以使用代理模式来提供一个代理对象。
 *  9. ……
 * 代理模式的优点：
 *  1. 代理模式可以为其他对象提供一种代理以控制对这个对象的访问。
 *  2. 代理模式可以在访问某个对象之前或之后执行一些操作。
 *  3. 代理模式可以延迟加载某个对象。
 *  4. 代理模式可以在访问某个对象时进行权限控制。
 *  5. 代理模式可以在访问某个对象时进行日志记录。
 *  6. 代理模式可以在访问某个对象时进行性能监控。
 *  7. 代理模式可以在访问某个对象时进行缓存。
 *  8. 代理模式可以在访问某个对象时进行事务控制。
 *  9. ……
 * 代理模式的缺点：
 *  1. 代理模式会增加系统的复杂性。
 *  2. 代理模式会增加系统的开销。
 *  3. 代理模式会增加系统的维护成本。
 *  4. 代理模式会增加系统的耦合度。
 *  5. 代理模式会增加系统的开发成本。
 *  6. 代理模式会增加系统的测试成本。
 *  7. 代理模式会增加系统的部署成本。
 *  8. 代理模式会增加系统的运行成本。
 *  9. ……
 * 代理模式的主要组成部分：
 *  1. 抽象主题（Subject）：定义了代理和真实主题的共同接口。
 *  2. 真实主题（RealSubject）：实现了抽象主题接口，定义了代理所代表的对象。
 *  3. 代理（Proxy）：实现了抽象主题接口，持有对真实主题的引用，并控制对真实主题的访问。
 *  4. 客户端（Client）：通过代理访问真实主题。
 */
using LearnCSharp.DesignPattern.LearnProxySpace;
using System.Diagnostics;

namespace LearnCSharp.DesignPattern
{
    internal class LearnProxy
    {
        /*【31001：虚拟代理模式】*/
        public static void LearnVirtualProxy()
        {
            Console.WriteLine("\n------示例：虚拟代理模式------\n");
            Console.WriteLine("》》》通过虚拟代理模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            Stopwatch stopwatch = new Stopwatch();
            List<IImage> images = new List<IImage>();

            // 创建真实对象
            Console.WriteLine("》》》模拟使用真实对象读取高分辨率大型图片");
            stopwatch.Start();
            for (int i = 0; i < 10; i++)
            {
                HighResolutionImage hrImage = new HighResolutionImage($"图片 {i:00}.jpeg");
                images.Add(hrImage);
            }
            stopwatch.Stop();
            Console.WriteLine($"创建真实对象耗时：{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"-----显示图片-----");
            images.ForEach(image => image.Display()); // 显示图片

            images.Clear(); // 清空列表
            Console.WriteLine();

            // 创建虚拟代理对象
            Console.WriteLine("》》》模拟使用虚拟代理读取高分辨率大型图片");
            stopwatch.Restart(); // 重置计时器
            for (int i = 0; i < 10; i++)
            {
                IImage image = new ImageProxy($"image_{i}.jpg");
                images.Add(image);
            }
            stopwatch.Stop();
            Console.WriteLine($"创建虚拟代理对象耗时：{stopwatch.ElapsedMilliseconds}ms");
            Console.WriteLine($"-----在需求时显示图片-----");
            images.ForEach(image => image.Display()); // 显示图片
            images.Clear(); // 清空列表

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31002：保护代理模式】*/
        public static void LearnProtectionProxy()
        {
            Console.WriteLine("\n------示例：保护代理模式------\n");
            Console.WriteLine("》》》通过保护代理模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建真实对象
            Console.WriteLine("》》》模拟使用真实对象读取数据库");
            RealDatabase realDatabase = new RealDatabase();
            realDatabase.Query("SELECT * FROM Users"); // 执行查询

            Console.WriteLine();

            // 创建保护代理对象
            Console.WriteLine("》》》模拟使用保护代理读取数据库（管理员权限）");
            ProtectedDatabaseProxy protectedProxy = new ProtectedDatabaseProxy("admin");
            protectedProxy.Query("SELECT * FROM Users"); // 执行查询

            Console.WriteLine();

            Console.WriteLine("》》》模拟使用保护代理读取数据库（用户权限）");
            protectedProxy = new ProtectedDatabaseProxy("user");
            protectedProxy.Query("SELECT * FROM Users"); // 执行查询

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31003：远程代理模式】*/
        public static void LearnRemoteProxy()
        {
            Console.WriteLine("\n------示例：远程代理模式------\n");
            Console.WriteLine("》》》通过远程代理模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建远程代理对象
            Console.WriteLine("》》》模拟使用远程代理下载远端文件");
            RemoteServiceProxy remoteService = new RemoteServiceProxy("127.0.0.1", 1111, "example.txt");
            remoteService.Download(); // 执行操作

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31004：智能代理模式】*/
        public static void LearnSmartProxy()
        {
            Console.WriteLine("\n------示例：智能代理模式------\n");
            Console.WriteLine("》》》通过智能代理模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");
            
            // 创建智能代理对象
            Console.WriteLine("》》》模拟使用智能代理计算器");
            SmartCalculatorProxy smartCalculator = new SmartCalculatorProxy();
            Console.WriteLine($"加法结果：{smartCalculator.Add(1, 2)}"); // 执行加法
            Console.WriteLine($"减法结果：{smartCalculator.Subtract(5, 3)}"); // 执行减法
            Console.WriteLine($"加法结果：{smartCalculator.Add(1, 2)}"); // 执行加法
            Console.WriteLine($"减法结果：{smartCalculator.Subtract(5, 3)}"); // 执行减法
            Console.WriteLine($"真实计算的次数：{smartCalculator.RealCalculatorCount}"); // 获取真实计算器的数量

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnProxySpace
{
    // 代理模式
    // 代理模式（Proxy Pattern）是一种结构型设计模式，它为其他对象提供一种代理以控制对这个对象的访问。

    //-------------------------------------

    #region 虚拟代理模式
    /*【31001：虚拟代理模式 延迟加载】
     * 虚拟代理模式（Virtual Proxy Pattern）是一种结构型设计模式，它为其他对象提供一种代理以控制对这个对象的访问。
     * 特点：延迟高开销对象的创建和初始化，直到真正需要它们时才创建和初始化。
     *      保持与真实对象相同的接口。
     *      优化系统启动性能，避免不必要的资源消耗。
     */
    public interface IImage // 抽象主题
    {
        void Display(); // 显示图片
    }

    public class HighResolutionImage : IImage // 真实主题
    {
        private string fileName;
        public HighResolutionImage(string fileName)
        {
            this.fileName = fileName;
            LoadImageFromDisk(); // 模拟高开销的操作
        }
        private void LoadImageFromDisk()
        {
            Console.WriteLine($"正在从磁盘中加载图片：{fileName}");
            Thread.Sleep(2000); // 模拟加载时间
        }
        public void Display()
        {
            Console.WriteLine($"显示图片：{fileName}");
        }
    }

    public class ImageProxy : IImage // 虚拟代理
    {
        private readonly string fileName;
        private HighResolutionImage? realImage; // 延迟加载真实主题

        public ImageProxy(string fileName)
        {
            this.fileName = fileName;
            Console.WriteLine($"已定位按需加载图片：{fileName}");
        }

        public void Display()
        {
            if (realImage == null)
            {
                realImage = new HighResolutionImage(fileName); // 创建真实主题
            }
            realImage.Display(); // 调用真实主题的方法
        }
    }
    #endregion

    #region 保护代理模式
    /*【31002：保护代理模式 权限控制】
     * 保护代理模式（Protection Proxy Pattern）是一种结构型设计模式，它为其他对象提供一种代理以控制对这个对象的访问。
     * 特点：在访问真实主题之前或之后执行一些操作。
     *      控制对真实主题的访问权限。
     *      保护真实主题的安全性和完整性。
     */
    public interface IDarabase // 抽象主题
    {
        void Query(string sql); // 查询数据
    }

    public class RealDatabase : IDarabase // 真实主题
    {
        public void Query(string sql)
        {
            Console.WriteLine($"执行查询：{sql}");
        }
    }

    public class ProtectedDatabaseProxy : IDarabase // 保护代理
    {
        private readonly RealDatabase realDatabase;
        private readonly string userRole;
        public ProtectedDatabaseProxy(string userRole)
        {
            this.realDatabase = new RealDatabase();
            this.userRole = userRole;
        }
        public void Query(string sql)
        {
            if (userRole == "admin")
            {
                realDatabase.Query(sql); // 执行查询
            }
            else
            {
                Console.WriteLine("权限不足，无法执行查询。");
            }
        }
    }
    #endregion

    #region 远程代理模式
    /*【31003：远程代理模式 网络访问抽象】
     * 远程代理模式（Remote Proxy Pattern）是一种结构型设计模式，它为其他对象提供一种代理以控制对这个对象的访问。
     * 特点：在网络上访问真实主题。
     *      隐藏网络通信的复杂性。
     *      提供与真实主题相同的接口。
     */
    public interface IRemoteService // 本地代理
    {
        void Download(); // 执行操作
    }

    public class RemoteServiceProxy : IRemoteService // 远程代理
    {
        private readonly string ipAddress;
        private readonly int port;
        private readonly string filename;
        public RemoteServiceProxy(string ipAddress, int port, string filename)
        {
            this.ipAddress = ipAddress;
            this.port = port;
            this.filename = filename;
            Console.WriteLine($@"【已定位远程服务】https://{ipAddress}:{port}/{filename}");
        }

        public void Download()
        {
            Console.WriteLine($@"【请求远程文件】https://{ipAddress}:{port}/{filename}");
            Thread.Sleep(2000); // 模拟网络延迟
            Console.WriteLine($"下载完成：{filename}");
        }
    }
    #endregion

    #region 智能代理模式
    /*【31004：智能代理模式 增强功能】
     * 智能代理模式（Smart Proxy Pattern）是一种结构型设计模式，它为其他对象提供一种代理以控制对这个对象的访问。
     * 特点：在访问真实主题之前或之后执行一些操作。
     *      控制对真实主题的访问权限。
     *      提供与真实主题相同的接口。
     */
    public interface ICalculator // 抽象主题
    {
        int Add(int a, int b); // 加法
        int Subtract(int a, int b); // 减法
    }

    public class Calculator : ICalculator // 真实主题
    {
        public int Add(int a, int b)
        {
            return a + b; // 执行加法
        }
        public int Subtract(int a, int b)
        {
            return a - b; // 执行减法
        }
    }

    public class SmartCalculatorProxy : ICalculator
    {
        private readonly Calculator calculator;
        private readonly Dictionary<string, int> cache=new(); // 缓存结果

        public int RealCalculatorCount => cache.Count; // 真实计算器的数量

        public SmartCalculatorProxy()
        {
            calculator = new Calculator(); // 创建真实主题
        }

        public int Add(int a, int b)
        {
            string key = $"Add_{a}_{b}";
            if (cache.ContainsKey(key))
            {
                Console.WriteLine($"从缓存中获取结果：{key} = {cache[key]}");
                return cache[key]; // 从缓存中获取结果
            }
            else
            {
                int result = calculator.Add(a, b); // 执行加法
                cache[key] = result; // 缓存结果
                return result;
            }
        }

        public int Subtract(int a, int b)
        {
            string key = $"Subtract_{a}_{b}";
            if (cache.ContainsKey(key))
            {
                Console.WriteLine($"从缓存中获取结果：{key} = {cache[key]}");
                return cache[key]; // 从缓存中获取结果
            }
            else
            {
                int result = calculator.Subtract(a, b); // 执行减法
                cache[key] = result; // 缓存结果
                return result;
            }
        }
    }
    #endregion
}
