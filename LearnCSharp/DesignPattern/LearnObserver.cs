/*【312：行为型————观察者模式】*
 * 观察者模式（Observer Pattern）是一种行为型设计模式，它定义了一种一对多的依赖关系，让多个观察者对象同时监听某一个主题对象。这个主题对象在状态发生变化时，会通知所有观察者对象，使它们能够自动更新自己。
 * 观察者模式通常用于事件处理系统、数据绑定和MVC（模型-视图-控制器）架构等场景。
 * 观察者模式的优点包括：
 *  1. 降低了主题和观察者之间的耦合度，主题不需要知道观察者的具体实现。
 *  2. 支持广播通信，主题可以同时通知多个观察者。
 *  3. 观察者可以动态添加或移除，灵活性高。
 *  4. 适用于需要实时更新的场景，如股票价格、天气预报等。
 *  5. 观察者模式可以实现事件驱动编程，简化代码结构。
 *  6. 观察者模式可以实现数据的自动更新，减少手动更新的工作量。
 *  7. 观察者模式可以实现松耦合的设计，使得系统更易于扩展和维护。
 *  8. ……
 * 观察者模式的缺点包括：
 *  1. 可能会导致过多的通知，增加系统的复杂性。
 *  2. 观察者和主题之间的关系可能会导致内存泄漏，特别是在使用弱引用时。
 *  3. 观察者模式可能会导致性能问题，特别是在观察者数量较多时。
 *  4. 观察者模式可能会导致代码的可读性下降，特别是在观察者数量较多时。
 *  5. 观察者模式可能会导致代码的可维护性下降，特别是在观察者数量较多时。
 *  6. 观察者模式可能会导致代码的可扩展性下降，特别是在观察者数量较多时。
 *  7. 观察者模式可能会导致代码的可测试性下降，特别是在观察者数量较多时。
 *  8. ……
 * 观察者模式的主要组成部分包括：
 *  1. 主题（Subject）：被观察的对象，维护观察者列表，并在状态变化时通知观察者。
 *  2. 观察者（Observer）：观察主题对象的对象，当主题状态变化时，观察者会收到通知并做出相应的处理。
 *  3. 具体主题（ConcreteSubject）：实现主题接口的具体类，维护状态并通知观察者。
 *  4. 具体观察者（ConcreteObserver）：实现观察者接口的具体类，定义在主题状态变化时的响应行为。
 */

using LearnCSharp.DesignPattern.LearnFacadeSpace;
using LearnCSharp.DesignPattern.LearnObserverSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnObserver
    {
        /*【31201：经典推模型观察者模式】*/
        public static void LearnPushModelObserver()
        {
            Console.WriteLine("\n------示例：推模型观察者模式------\n");
            Console.WriteLine("》》》通过观察者模式模拟通知服务《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建主题对象
            var notificationService = new NotificationService();

            // 创建观察者对象
            var phone = new Phone();
            var computer = new Computer();
            var watch = new Watch();

            // 注册观察者
            notificationService.RegisterDevice(phone);
            notificationService.RegisterDevice(computer);
            notificationService.RegisterDevice(watch);

            // 发送通知
            notificationService.NotifyDevices("您有新的消息！");
            Console.WriteLine("通知已发送！");

            // 注销观察者
            notificationService.UnregisterDevice(computer);
            Console.WriteLine("电脑设备已注销！");

            // 发送通知
            notificationService.NotifyDevices("您有新的消息！");
            Console.WriteLine("通知已发送！");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31202：拉模型观察者模式】*/
        public static void LearnPullModelObserver()
        {
            Console.WriteLine("\n------示例：拉模型观察者模式------\n");
            Console.WriteLine("》》》通过观察者模式模拟天气通知服务《《《");
            Console.WriteLine("-----------------------------------------------");
            
            // 创建主题对象
            var weatherService = new WeatherNotificationService();
            
            // 创建观察者对象
            var phone = new Phone();
            var computer = new Computer();
            var watch = new Watch() { IsMustUpdate = false };
            
            // 注册观察者
            weatherService.RegisterObserver(phone);
            weatherService.RegisterObserver(computer);
            weatherService.RegisterObserver(watch);
            
            // 发送通知
            weatherService.NotifyObservers();

            // 观察者请求天气数据
            phone.GetWeatherData(weatherService);
            computer.GetWeatherData(weatherService);
            watch.GetWeatherData(weatherService);
            Console.WriteLine("天气数据已请求！手表不需要请求天气信息");

            // 注销观察者
            weatherService.UnregisterObserver(computer);
            Console.WriteLine("电脑设备已注销！");

            // 发送通知
            weatherService.NotifyObservers();

            // 观察者请求天气数据
            phone.GetWeatherData(weatherService);
            computer.GetWeatherData(weatherService);
            watch.GetWeatherData(weatherService);
            Console.WriteLine("天气数据已请求！手表不需要请求天气信息");

            Console.WriteLine("-----------------------------------------------");
        }

        /*【31203：.NET内置观察者模式 基于事件的观察者模式】*/
        public static void LearnDotNetObserver()
        {
            Console.WriteLine("\n------示例：.NET内置观察者模式------\n");
            Console.WriteLine("》》》通过观察者模式模拟图像处理器《《《");

            Console.WriteLine("-----------------------------------------------");
            
            // 创建主题对象
            var imageProcessor1 = new ImageProcessor();
            var imageProcessor2 = new ImageProcessor();
            var imageProcessor3 = new ImageProcessor();

            // 事件处理程序 相当于观察者
            async void ImageProcessed1(object sender, ImageProcessedEventArgs e)
            {
                await Task.Delay(0);
                Console.WriteLine($"事件处理程序1---{(sender as ImageProcessor)?.Name} 处理完成：{e.ImagePath}");
            }

            async void ImageProcessed2(object sender, ImageProcessedEventArgs e)
            {
                await Task.Delay(0);
                Console.WriteLine($"事件处理程序2---{(sender as ImageProcessor)?.Name} 处理完成：{e.ImagePath}");
            }

            // 订阅事件
            imageProcessor1.ImageProcessed += ImageProcessed1!;
            imageProcessor2.ImageProcessed += ImageProcessed1!;
            imageProcessor3.ImageProcessed += ImageProcessed1!;
            imageProcessor1.ImageProcessed += ImageProcessed2!;

            // 处理图像
            List<Thread> threads = new List<Thread>();

            threads.Add(new Thread(() => imageProcessor1.ProcessImage("image1.jpg")));
            threads.Add(new Thread(() => imageProcessor2.ProcessImage("image2.jpg")));
            threads.Add(new Thread(() =>
            {
                imageProcessor1.ProcessImage("image3.jpg");
                imageProcessor2.ProcessImage("image4.jpg");
                imageProcessor3.ProcessImage("image5.jpg");
            }));

            threads.ForEach(t => t.Start());
            threads.ForEach(t => t.Join()); // 等待所有线程完成

            Console.WriteLine("-----------------------------------------------");
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnObserverSpace
{

    #region 基础观察者模式结构
    /*【31200：基础观察者模式结构】*/
    public interface IObserver // 观察者接口
    {
        void Update(string message);
    }

    public class ConcreteObserver : IObserver // 具体观察者
    {
        private string _name;
        public ConcreteObserver(string name)
        {
            _name = name;
        }
        public void Update(string message)
        {
            Console.WriteLine($"{_name} 收到通知：{message}");
        }
    }

    public interface ISubject // 主题接口 被观察者
    {
        void Attach(IObserver observer); // 添加观察者
        void Detach(IObserver observer); // 移除观察者
        void Notify(string message); // 通知观察者
    }

    public class ConcreteSubject : ISubject // 具体主题
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
    #endregion

    #region 推模型观察者模式示例
    /*【31201：经典推模型观察者模式】
     * 经典推模型观察者模式是观察者模式的一种实现方式，在这种实现中，主题对象在状态变化时主动推送更新给所有注册的观察者。每个观察者都可以接收主题对象的状态变化通知，并根据需要进行处理。
     * 经典推模型观察者模式的优点是：
     *  1. 简单易懂，易于实现。
     *  2. 主题对象和观察者之间的耦合度较低，便于扩展和维护。
     *  3. 支持一对多的通知机制，适用于多个观察者需要同时接收通知的场景。
     *  4. 可以实现动态添加和移除观察者，灵活性高。
     *  5. ……
     */
    public interface IDevice //设备接口
    {
        void ReceiveNotification(string message);
    }

    public partial class Phone : IDevice //手机设备
    {
        public void ReceiveNotification(string message)
        {
            Console.WriteLine($"手机收到通知：{message}");
        }
    }

    public partial class Computer : IDevice //电脑设备
    {
        public void ReceiveNotification(string message)
        {
            Console.WriteLine($"电脑收到通知：{message}");
        }
    }

    public partial class Watch : IDevice //手表设备
    {
        public void ReceiveNotification(string message)
        {
            Console.WriteLine($"手表收到通知：{message}");
        }
    }

    public interface INotificationService //通知服务接口
    {
        void RegisterDevice(IDevice device); //注册设备
        void UnregisterDevice(IDevice device); //注销设备
        void NotifyDevices(string message); //通知设备
    }

    public class NotificationService : INotificationService
    {
        private List<IDevice> devices = new List<IDevice>();
        public void RegisterDevice(IDevice device)
        {
            devices.Add(device);
        }
        public void UnregisterDevice(IDevice device)
        {
            devices.Remove(device);
        }
        public void NotifyDevices(string message)
        {
            foreach (var device in devices)
            {
                device.ReceiveNotification(message);
            }
        }
    }
    #endregion

    #region 拉模型观察者模式示例
    /*【31202：拉模型观察者模式】
     * 拉模型观察者模式是观察者模式的另一种实现方式，在这种实现中，观察者在接收到通知时主动向主题对象请求最新的状态信息。主题对象只负责通知观察者状态发生了变化，而不直接推送具体的状态信息。
     * 拉模型观察者模式的优点是：
     *  1. 观察者可以根据需要选择获取最新的状态信息，避免了不必要的数据传输。
     *  2. 可以减少主题对象和观察者之间的耦合度，提高系统的灵活性。
     *  3. 支持动态添加和移除观察者，便于扩展和维护。
     *  4. ……
     */

    public interface IWeatherObserver //天气观察者接口
    {
        bool IsMustUpdate { get; set; } //是否必须更新
        bool IsReceived { get; set; } //是否接收到通知
        void WeatherUpdate(); //已更新数据

        void GetWeatherData(IWeatherNotificationService service); //模拟请求天气数据
    }

    public partial class Phone : IWeatherObserver //手机天气观察者
    {
        public bool IsMustUpdate { get; set; } = true; //是否必须更新
        public bool IsReceived { get; set; } = false;

        public void WeatherUpdate()
        {
            Console.WriteLine("天气数据已更新！已通知到手机！");
            IsReceived = true;
        }

        public void GetWeatherData(IWeatherNotificationService service)
        {
            if (IsReceived && IsMustUpdate)
            {
                Console.WriteLine("手机请求天气数据...");
                Console.WriteLine(service.GetWeatherData("Beijing"));
            }
            IsReceived = false; //重置接收状态
        }
    }

    public partial class Computer : IWeatherObserver //电脑天气观察者
    {
        public bool IsMustUpdate { get; set; } = true; //是否必须更新
        public bool IsReceived { get; set; } = false; //是否接收到通知

        public void WeatherUpdate()
        {
            Console.WriteLine("天气数据已更新！已通知到电脑！");
            IsReceived = true;
        }
        public void GetWeatherData(IWeatherNotificationService service)
        {
            if (IsReceived && IsMustUpdate)
            {
                Console.WriteLine("电脑请求天气数据...");
                Console.WriteLine(service.GetWeatherData("Shanghai"));
            }
            IsReceived = false; //重置接收状态
        }
    }

    public partial class Watch : IWeatherObserver //手表天气观察者
    {
        public bool IsMustUpdate { get; set; } = true; //是否必须更新
        public bool IsReceived { get; set; } = false; //是否接收到通知

        public void WeatherUpdate()
        {
            Console.WriteLine("天气数据已更新！已通知到手表！");
            IsReceived = true;
        }
        public void GetWeatherData(IWeatherNotificationService service)
        {
            if (IsReceived && IsMustUpdate)
            {
                Console.WriteLine("手表请求天气数据...");
                Console.WriteLine(service.GetWeatherData("Guangzhou"));
            }
            IsReceived = false; //重置接收状态
        }
    }

    public interface IWeatherNotificationService //天气通知服务接口
    {
        void RegisterObserver(IWeatherObserver observer); //注册观察者
        void UnregisterObserver(IWeatherObserver observer); //注销观察者
        void NotifyObservers(); //通知观察者

        string GetWeatherData(string location); //获取天气数据
    }

    public class WeatherNotificationService : IWeatherNotificationService //天气通知服务
    {
        private List<IWeatherObserver> observers = new List<IWeatherObserver>();    //观察者列表

        public void RegisterObserver(IWeatherObserver observer) //注册观察者
        {
            observers.Add(observer);
        }

        public void UnregisterObserver(IWeatherObserver observer)   //注销观察者
        {
            observers.Remove(observer);
        }

        public void NotifyObservers()   //通知观察者
        {
            foreach (var observer in observers)
            {
                observer.WeatherUpdate();
            }
        }
        public string GetWeatherData(string location) //获取天气数据
        {
            // 模拟获取天气数据
            return $"Location: {location}, Temperature: 25°C, Humidity: 60%, WindSpeed: 10km/h, Condition: Sunny";
        }
    }
    #endregion

    #region .NET内置观察者模式 基于事件的观察者模式
    /*【31203：.NET内置观察者模式 基于事件的观察者模式】
     * .NET内置观察者模式是基于事件的观察者模式实现，使用事件和委托来实现观察者模式。主题对象通过事件通知观察者，观察者通过订阅事件来接收通知。
     * .NET内置观察者模式的优点是：
     *  1. 简单易用，易于实现。
     *  2. 支持多播委托，可以同时通知多个观察者。
     *  3. 支持动态添加和移除观察者，便于扩展和维护。
     *  4. ……
     */
    public class ImageProcessedEventArgs : EventArgs //图像处理事件参数
    {
        public string ImagePath { get; }
        public ImageProcessedEventArgs(string imagePath)
        {
            ImagePath = imagePath;
        }
    }

    public class ImageProcessor //图像处理器
    {
        public string Name { get; }

        public ImageProcessor()
        {
            Name = $"图像处理器 {Random.Shared.Next(100, 500)}";
        }

        public event EventHandler<ImageProcessedEventArgs>? ImageProcessed; //图像处理完成事件

        public void ProcessImage(string imagePath)
        {
            // 模拟图像处理
            Console.WriteLine($"正在处理图片: {imagePath}");
            Thread.Sleep(Random.Shared.Next(1000, 5000)); // 模拟耗时操作
            // 触发事件，通知观察者
            OnImageProcessed(new ImageProcessedEventArgs(imagePath));
        }

        protected virtual void OnImageProcessed(ImageProcessedEventArgs e)
        {
            ImageProcessed?.Invoke(this, e);
        }
    }
    #endregion
}

