/*【304：创建型————原型模式】*/

using System.Text.Json;
using System.Text.Json.Serialization;
using LearnCSharp.DesignPattern.LearnPrototypeSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnPrototype
    {
        /*【30401：浅拷贝原型模式】*/
        public static void LearnShallowCopyPrototype()
        {
            Console.WriteLine("\n------示例：浅拷贝原型模式------\n");
            Console.WriteLine("》》》通过浅拷贝原型模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            ShallowCopyPrototype original = new ShallowCopyPrototype("Alice", 25);
            ShallowCopyPrototype clone = original.Clone();

            Console.WriteLine($"原对象: {original.Name}, {original.Age}, {string.Join(", ", original.Hobbies)}，{original.GetHashCode()}");
            Console.WriteLine($"新对象: {clone.Name}, {clone.Age}, {string.Join(", ", clone.Hobbies)}, {clone.GetHashCode()}");

            // 修改克隆对象的属性
            clone.Name = "Bob";
            clone.Age = 30;
            clone.Hobbies.Add("Cooking");
            Console.WriteLine($"修改克隆对象后输出信息:");
            Console.WriteLine($"原对象: {original.Name}, {original.Age}, {string.Join(", ", original.Hobbies)}，{original.GetHashCode()}");
            Console.WriteLine($"新对象: {clone.Name}, {clone.Age}, {string.Join(", ", clone.Hobbies)}, {clone.GetHashCode()}");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30402：深拷贝原型模式】*/
        public static void LearnDeepCopyPrototype()
        {
            Console.WriteLine("\n------示例：深拷贝原型模式------\n");
            Console.WriteLine("》》》通过深拷贝原型模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            DeepCopyPrototype original = new DeepCopyPrototype("Alice");
            DeepCopyPrototype clone = original.Clone();
            Console.WriteLine($"原对象: {original.Name}, {string.Join(", ", original.Config)}，{original.GetHashCode()}");
            Console.WriteLine($"新对象: {clone.Name}, {string.Join(", ", clone.Config)}, {clone.GetHashCode()}");
            // 修改克隆对象的属性
            clone.Name = "Bob";
            clone.Config["Version"] = "2.0";
            Console.WriteLine($"修改克隆对象后输出信息:");
            Console.WriteLine($"原对象: {original.Name}, {string.Join(", ", original.Config)}，{original.GetHashCode()}");
            Console.WriteLine($"新对象: {clone.Name}, {string.Join(", ", clone.Config)}, {clone.GetHashCode()}");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30403：序列化深拷贝原型模式】*/
        public static void LearnSerializableDeepCopyPrototype()
        {
            Console.WriteLine("\n------示例：序列化深拷贝原型模式------\n");
            Console.WriteLine("》》》通过序列化深拷贝原型模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            SerializableDeepCopyPrototype original = new SerializableDeepCopyPrototype("Alice");
            SerializableDeepCopyPrototype clone = original.Clone();
            Console.WriteLine($"原对象: {original.Name}, {string.Join(", ", original.Config)}，{original.GetHashCode()}");
            Console.WriteLine($"新对象: {clone.Name}, {string.Join(", ", clone.Config)}, {clone.GetHashCode()}");

            // 修改克隆对象的属性
            clone.Name = "Bob";
            clone.Config["Version"] = "2.0";
            Console.WriteLine($"修改克隆对象后输出信息:");
            Console.WriteLine($"原对象: {original.Name}, {string.Join(", ", original.Config)}，{original.GetHashCode()}");
            Console.WriteLine($"新对象: {clone.Name}, {string.Join(", ", clone.Config)}, {clone.GetHashCode()}");

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnPrototypeSpace
{
    // 原型模式
    // 通过复制现有对象来创建新对象，而不是通过构造函数
    // 适用于需要创建大量相似对象的场景
    // 例如：游戏中的角色、图形对象等
    // 通过实现 ICloneable 接口来支持克隆

    //--------------------------------------------------------------------------------------

    /*【30400：原型模式的使用】
     * 原型模式的使用场景：
     * 1. 创建大量相似对象时
     * 2. 对象的创建成本较高时
     * 3. 对象的状态需要频繁修改时
     * 4. 对象的结构复杂时*/
    public interface IPrototype<T> //基础原型接口
    {
        T Clone();
    }

    /*【30401：浅拷贝原型模式】
     * 浅拷贝是指创建一个新对象，但新对象的字段仍然引用原对象的字段。
     * 换句话说，浅拷贝只复制对象的引用，而不复制对象本身。
     * 特点：使用 MemberwiseClone() 方法来创建新对象。
     *      值类型字段/属性独立复制
     *      引用类型字段/属性仍然引用原对象的字段
     *      实现简单但存在副作用风险：比如修改引用类型字段会影响原对象
     */
    public class ShallowCopyPrototype : IPrototype<ShallowCopyPrototype>
    {
        public string Name { get; set; }    // 字符串是不可变的引用类型
        public int Age { get; set; }        // 整数是值类型

        public List<string> Hobbies { get; set; } = new List<string>(); // 引用类型

        public ShallowCopyPrototype(string name, int age)
        {
            Name = name;
            Age = age;
            Hobbies.Add("Reading");
            Hobbies.Add("Traveling");
        }
        public ShallowCopyPrototype Clone()
        {
            return (ShallowCopyPrototype)this.MemberwiseClone();
        }
    }

    /*【30402：深拷贝原型模式】
     * 深拷贝是指创建一个新对象，并且新对象的字段也会复制原对象的字段。
     * 换句话说，深拷贝会复制对象本身，而不仅仅是引用。
     * 特点：值类型字段/属性独立复制
     *      引用类型字段/属性也会独立复制
     *      副本完全独立于原对象
     *      性能开销较大
     *      实现复杂但没有副作用风险
     */
    public class DeepCopyPrototype : IPrototype<DeepCopyPrototype>
    {
        public int Version { get; set; } = 1;
        public string Name { get; set; }    // 字符串是不可变的引用类型

        public Dictionary<string, string> Config { get; set; } = new Dictionary<string, string>(); // 引用类型

        public DeepCopyPrototype(string name)
        {
            Name = name;
            Config.Add("Language", "C#");
            Config.Add("Version", "1.0");
        }

        public DeepCopyPrototype Clone()
        {
            // 创建新对象
            DeepCopyPrototype clone = (DeepCopyPrototype)this.MemberwiseClone();
            // 深拷贝引用类型字段
            clone.Config = new Dictionary<string, string>(this.Config);
            return clone;
        }
    }

    /*【30403：序列化深拷贝原型模式】
     * 序列化深拷贝是指通过序列化和反序列化来创建新对象。
     * 特点：值类型字段/属性独立复制
     *      引用类型字段/属性也会独立复制
     *      副本完全独立于原对象
     *      性能开销较大
     *      实现复杂但没有副作用风险
     */
    [JsonSerializable(typeof(SerializableDeepCopyPrototype))]
    [JsonSerializable(typeof(Dictionary<string, string>))]
    internal partial class SerializableDeepCopyPrototypeContext : JsonSerializerContext
    {
    }

    public class SerializableDeepCopyPrototype : IPrototype<SerializableDeepCopyPrototype>
    {
        public int Version { get; set; } = 1;
        public string Name { get; set; }    // 字符串是不可变的引用类型
        public Dictionary<string, string> Config { get; set; } = new Dictionary<string, string>(); // 引用类型
        public SerializableDeepCopyPrototype(string name)
        {
            Name = name;
            Config.Add("Language", "C#");
            Config.Add("Version", "1.0");
        }

        public SerializableDeepCopyPrototype Clone()
        {
            //===序列化深拷贝===
            var json = JsonSerializer.SerializeToUtf8Bytes(this,SerializableDeepCopyPrototypeContext.Default.SerializableDeepCopyPrototype);
            return JsonSerializer.Deserialize<SerializableDeepCopyPrototype>(json,SerializableDeepCopyPrototypeContext.Default.SerializableDeepCopyPrototype)!;
        }
    }
}
