/*【305：结构型————适配器模式】*/

using LearnCSharp.DesignPattern.LearnAdapterSpace;
using System.Runtime.CompilerServices;
using System.Text;

namespace LearnCSharp.DesignPattern
{
    internal class LearnAdapter
    {
        /*【30501：类适配器模式】*/
        public static void LearnClassAdapter()
        {
            Console.WriteLine("\n------示例：类适配器模式------\n");
            Console.WriteLine("》》》通过类适配器模式创建适配对象《《《");
            Console.WriteLine("-----------------------------------------------");

            IUser user = new UserAdapter("张三", 25, new List<string> { "游泳", "读书" }, "1234567890", "北京市");
            Console.WriteLine($"【用户信息】{user.GetUserInfo()}");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30502：对象适配器模式】*/
        public static void LearnObjectAdapter()
        {
            Console.WriteLine("\n------示例：对象适配器模式------\n");
            Console.WriteLine("》》》通过对象适配器模式创建适配对象《《《");
            Console.WriteLine("-----------------------------------------------");

            CloudStorageService cloudStorageService = new CloudStorageService();
            NASService nasService = new NASService();
            IFileStorage fileStorageAdapter = new FileStorageAdapter(cloudStorageService, nasService);
            using MemoryStream memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("Hello World!"));
            fileStorageAdapter.SaveFile("test.txt", memoryStream);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30503：接口适配器模式】*/
        public static void LearnInterfaceAdapter()
        {
            Console.WriteLine("\n------示例：接口适配器模式------\n");
            Console.WriteLine("》》》通过接口适配器模式创建适配对象《《《");
            Console.WriteLine("-----------------------------------------------");

            IAdvancedOperations iao = new SimpleSave();
            iao.Save("只需实现这个方法!");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnAdapterSpace
{
    // 适配器模式
    // 适配器模式是一种结构型设计模式，它允许将一个类的接口转换成客户端所期望的另一个接口。
    // 适配器模式使得原本由于接口不兼容而不能一起工作的类可以一起工作。
    // 适配器模式通常用于以下场景：
    // 1. 当你想使用一个类，但它的接口与现有代码不兼容时。
    // 2. 当你想创建一个可以与多个不同接口的类一起工作的类时。
    // 3. 当你想使用一个现有的类，但不想修改它的代码时。
    // 4. 当你想将一个类的接口转换成另一个接口时。
    //-------------------------------------------------

    // 适配器模式的实现步骤：
    // 1. 定义一个目标接口，表示客户端所期望的接口。
    // 2. 创建一个被适配的类，表示现有的类，它的接口与目标接口不兼容。
    // 3. 创建一个适配器类，实现目标接口，并持有被适配的类的实例。
    // 4. 在适配器类中实现目标接口的方法，将调用转发到被适配的类的实例上。
    // 5. 客户端代码使用适配器类来调用被适配的类的方法，而不是直接调用被适配的类的方法。
    // 6. 可以根据需要扩展新的适配器类，只需实现目标接口，并持有被适配的类的实例，而不需要修改现有代码。
    //-------------------------------------------------


    #region 类适配器模式
    /*【30501：类适配器模式 继承实现】
     * 类适配器模式是通过继承被适配的类来实现的。
     * 特点：通过多重继承实现适配器类（类适配器），可以直接访问被适配的类的方法和属性。（C#不支持多重继承，需通过组合替代）
     *      适配器直接继承被适配的类，并实现目标接口的方法。
     *      会暴露被适配的类的所有方法和属性，违反接口隔离原则。
     */
    public class User //已有的不兼容的类，被适配的类
    {
        protected Guid guid;

        public string Name { get; set; }
        public int Age { get; set; }

        public List<string> Hobbies { get; set; }

        public User(string name, int age, List<string> hobbies)
        {
            guid = Guid.NewGuid();
            Name = name;
            Age = age;
            Hobbies = hobbies;
        }

        public override string ToString()
        {
            return $"用户类 --- 姓名: {Name}, 年龄: {Age}, 爱好: {string.Join(", ", Hobbies)}";
        }
    }

    interface IUser //目标接口
    {
        string PhoneNumber { get; set; }
        string Address { get; set; }

        string GetGuid();
        string GetUserInfo();
        string ToString();
    }

    public class UserAdapter : User, IUser //适配器类
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public UserAdapter(string name, int age, List<string> hobbies, string phoneNumber, string address) : base(name, age, hobbies)
        {
            PhoneNumber = phoneNumber;
            Address = address;
        }

        public string GetGuid()
        {
            return guid.ToString();
        }

        public string GetUserInfo()
        {
            return this.ToString().Replace("适配器类 --- ", "");
        }

        public override string ToString()
        {
            return $"适配器类 --- 姓名: {Name}, 年龄: {Age}, 爱好: {string.Join(", ", Hobbies)}, 电话: {PhoneNumber}, 地址: {Address}";
        }
    }
    #endregion

    #region 对象适配器模式
    /*【30502：对象适配器模式 组合实现】
     * 对象适配器模式是通过组合被适配的类来实现的。
     * 特点：通过组合实现适配器类（对象适配器），可以直接访问被适配的类的方法和属性。
     *      适配器持有被适配的类的实例，并实现目标接口的方法。
     *      不会暴露被适配的类的所有方法和属性，符合接口隔离原则。
     *      符合组合优于继承的原则。
     */
    public class CloudStorageService //模拟第三方服务接口，被适配的类
    {
        public void UploadFile(byte[] data)
        {
            Console.WriteLine($"已上传到云存储服务 --- 数据：{Encoding.UTF8.GetString(data)} | 大小：共 {data.Length} 字节");
        }
    }

    public class NASService //模拟第三方服务接口，被适配的类
    {
        public void SaveFile(byte[] data)
        {
            Console.WriteLine($"已保存到NAS服务 --- 数据：{Encoding.UTF8.GetString(data)} | 大小：共 {data.Length} 字节");
        }
    }

    public interface IFileStorage //模拟系统标准接口，目标接口
    {
        void SaveFile(string path, Stream fileStrem);
    }

    public class FileStorageAdapter : IFileStorage //适配器类
    {
        private readonly CloudStorageService cloudStorageService;
        private readonly NASService nasService;
        public FileStorageAdapter(CloudStorageService cloudStorageService, NASService nasService)
        {
            this.cloudStorageService = cloudStorageService;
            this.nasService = nasService;
        }
        public void SaveFile(string path, Stream fileStrem)
        {
            using MemoryStream memoryStream = new MemoryStream();
            fileStrem.CopyTo(memoryStream);
            cloudStorageService.UploadFile(memoryStream.ToArray());//适配调用 模拟保存到云存储服务
            nasService.SaveFile(memoryStream.ToArray());//适配调用 模拟保存到NAS服务
        }
    }
    #endregion

    #region 接口适配器模式
    /*【30503：接口适配器模式 缺省实现】
     * 接口适配器模式是通过创建一个抽象类来实现的。
     * 特点：通过创建一个抽象类来实现适配器类（接口适配器），可以直接访问被适配的类的方法和属性。
     *      适配器持有被适配的接口的实例，并按需实现目标接口的方法。
     *      不会暴露被适配的类的所有方法和属性，符合接口隔离原则。
     *      符合组合优于继承的原则。
     * 适用场景：当接口有多个方法，但只需要实现其中的部分方法时，可以使用接口适配器模式。
     *         兼容旧版接口，避免修改现有代码。
     *         框架扩展点设计
     */
    public interface IAdvancedOperations
    {
        void Save(string data);
        void Load(int id);
        void Validate();
        void Encrypt();
    }

    public abstract class AdvancedOperationsAdapter : IAdvancedOperations
    {
        public virtual void Save(string data) { }
        public virtual void Load(int id) { }
        public virtual void Validate() { }
        public virtual void Encrypt() { }
    }

    public class SimpleSave : AdvancedOperationsAdapter
    {
        public override void Save(string data)
        {
            Console.WriteLine($"保存数据：{data}");
        }
    }
    #endregion
}
