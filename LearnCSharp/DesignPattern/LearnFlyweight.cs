/*【309：结构型————享元模式】
 * 享元模式（Flyweight Pattern）是一种结构型设计模式，旨在通过共享相似的对象来减少内存使用和提高性能。
 * 享元模式的核心思想是将对象的状态分为内部状态和外部状态。
 * 享元对象存储内部状态，而外部状态则由客户端传递。
 * 享元模式适用于大量相似对象的场景，例如文本编辑器中的字符对象、图形编辑器中的形状对象等。
 * 享元模式的优点是可以显著减少内存使用，提高性能。
 * 享元模式的缺点是实现复杂，可能会导致代码难以理解和维护。
 * 享元模式通常与其他设计模式结合使用，例如单例模式、工厂模式等。
 * 享元模式的实现通常包括以下几个角色：
 *  1. Flyweight（享元）：定义了一个接口，用于实现共享对象的公共方法。
 *  2. ConcreteFlyweight（具体享元）：实现了 Flyweight 接口，表示具体的共享对象。
 *  3. FlyweightFactory（享元工厂）：负责创建和管理享元对象，确保共享相同的对象。
 *  4. Client（客户端）：使用享元对象，并将外部状态传递给享元对象。
 *  5. UnsharedConcreteFlyweight（非共享具体享元）：表示不需要共享的对象，通常用于存储外部状态。
 *  6. Context（上下文）：表示外部状态的对象，通常由客户端传递给享元对象。
 */


using LearnCSharp.DesignPattern.LearnFlyweightSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnFlyweight
    {
        /*【30901：基础享元模式】*/
        public static void LearnBasicFlyweight()
        {
            Console.WriteLine("\n------示例：基础享元模式------\n");
            Console.WriteLine("》》》通过享元模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建享元工厂
            PlayerFactory playerFactory = new PlayerFactory();

            // 创建享元对象
            IPlayer player1 = playerFactory.GetPlayer("李白", "剑客");
            IPlayer player2 = playerFactory.GetPlayer("扁鹊", "治疗");
            IPlayer player3 = playerFactory.GetPlayer("李白", "剑客"); // 重复创建相同的对象

            // 执行操作
            player1.Move(10, 20); // 玩家1移动到(10, 20)
            player2.Move(30, 40); // 玩家2移动到(30, 40)
            player3.Move(50, 60); // 玩家3移动到(50, 60)

            // 输出享元对象数量
            Console.WriteLine($"享元对象数量：{playerFactory.GetPlayerCount()}"); // 享元对象数量：2

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30902：复合享元模式】*/
        public static void LearnCompositeFlyweight()
        {
            Console.WriteLine("\n------示例：复合享元模式------\n");
            Console.WriteLine("》》》通过复合享元模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建复合享元工厂
            CompositeFlyweightFactory compositeFlyweightFactory = new CompositeFlyweightFactory();
            
            // 创建复合享元对象
            CompositeFlyweight composite1 = (CompositeFlyweight)compositeFlyweightFactory.GetCompositeFlyweight("Composite1");
            CompositeFlyweight composite2 = (CompositeFlyweight)compositeFlyweightFactory.GetCompositeFlyweight("Composite2");
            CompositeFlyweight composite3 = (CompositeFlyweight)compositeFlyweightFactory.GetCompositeFlyweight("Composite3"); 
            CompositeFlyweight composite4 = (CompositeFlyweight)compositeFlyweightFactory.GetCompositeFlyweight("Composite1"); // 重复创建相同的对象

            // 创建叶子节点
            ICompositeFlyweight leaf1 = new LeafFlyweight("Leaf1");
            ICompositeFlyweight leaf2 = new LeafFlyweight("Leaf2");
            ICompositeFlyweight leaf3 = new LeafFlyweight("Leaf3");
            ICompositeFlyweight leaf4 = new LeafFlyweight("Leaf4");
            ICompositeFlyweight leaf5 = new LeafFlyweight("Leaf5");

            // 添加叶子节点到复合享元对象
            composite1.Add(leaf1);
            composite1.Add(leaf2);
            composite2.Add(leaf3);
            composite2.Add(leaf4);
            composite3.Add(composite1);
            composite3.Add(leaf5);
            composite1.Add(leaf5);

            // 执行操作
            composite1.Operation("外部状态1"); // 执行操作 - 内部状态：Leaf1, 外部状态：外部状态1
            composite2.Operation("外部状态2"); // 执行操作 - 内部状态：Leaf2, 外部状态：外部状态2
            composite3.Operation("外部状态3"); // 执行操作 - 内部状态：Composite1, 外部状态：外部状态3
            composite4.Operation("外部状态4"); // 执行操作 - 内部状态：Composite2, 外部状态：外部状态4

            // 输出复合享元对象数量
            Console.WriteLine($"复合享元对象数量：{compositeFlyweightFactory.GetCompositeFlyweightCount()}"); // 复合享元对象数量：2
            
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnFlyweightSpace
{
    // 享元模式
    // 享元模式（Flyweight Pattern）是一种结构型设计模式，旨在通过共享相似的对象来减少内存使用和提高性能。
    // 享元模式的核心思想是将对象的状态分为内部状态和外部状态。
    // 享元对象存储内部状态，而外部状态则由客户端传递。

    //--------------------------------------------------------

    // 享元模式的实现通常包括以下几个角色：
    // 1. Flyweight（享元）：定义了一个接口，用于实现共享对象的公共方法。
    // 2. ConcreteFlyweight（具体享元）：实现了 Flyweight 接口，表示具体的共享对象。
    // 3. FlyweightFactory（享元工厂）：负责创建和管理享元对象，确保共享相同的对象。
    // 4. Client（客户端）：使用享元对象，并将外部状态传递给享元对象。
    // 5. UnsharedConcreteFlyweight（非共享具体享元）：表示不需要共享的对象，通常用于存储外部状态。
    // 6. Context（上下文）：表示外部状态的对象，通常由客户端传递给享元对象。

    //--------------------------------------------------------
    #region 享元模式基础结构
    /*【30900：享元模式基础结构】*/
    public interface IFlyweight //享元接口 定义共享对象的操作
    {
        void Operation(string extrinsicState);
    }

    public class ConcreteFlyweight : IFlyweight //具体享元类 实现共享对象的操作
    {
        private readonly string intrinsicState; //内部状态 可共享
        public ConcreteFlyweight(string intrinsicState)
        {
            this.intrinsicState = intrinsicState;
            Console.WriteLine($"创建享元对象，内部状态：{intrinsicState}");
        }
        public void Operation(string extrinsicState) //外部状态
        {
            Console.WriteLine($"执行操作 - 内部状态：{intrinsicState}, 外部状态：{extrinsicState}");
        }
    }

    public class FlyweightFactory //享元工厂类 负责创建和管理享元对象
    {
        private Dictionary<string, IFlyweight> flyweights = new Dictionary<string, IFlyweight>();

        public IFlyweight GetFlyweight(string key)
        {
            if (!flyweights.TryGetValue(key, out var flyweight))
            {
                flyweight = new ConcreteFlyweight(key);
                flyweights.Add(key, flyweight);
            }
            return flyweight;
        }

        public int GetFlyweightCount()
        {
            return flyweights.Count;
        }
    }
    #endregion

    #region 基础享元模式
    /*【30901：基础享元模式】
     * 享元模式的基础实现
     * 特点：内部状态和外部状态分离
     *      内部状态被共享
     *      外部状态由客户端维护
     *      工厂确保唯一实例
     */
    #region 基础享元模式示例
    public interface IPlayer
    {
        void Move(float positionX, float positionY);
    }

    public class Player : IPlayer
    {
        public string Name { get; } //内部状态 共享 玩家名称
        public string Type { get; } //内部状态 共享 玩家类型
        public (float positionX, float positionY) BirthPosition { get; } //内部状态 共享 出生位置
        public (float positionX, float positionY) Position { get; private set; } //非共享 用于存储外部动态 玩家位置
        
        public Player(string name, string type)
        {
            Name = name;
            Type = type;
            BirthPosition = (0, 0); //默认出生位置
        }

        public void Move(float positionX, float positionY)
        {
            Position = (positionX, positionY);
            Console.WriteLine($"【内部状态】玩家 {Name} ({Type}) 移动到【外部动态】 ({positionX}, {positionY})");
        }
    }

    public class PlayerFactory //玩家工厂类 负责创建和管理玩家对象
    {
        private Dictionary<string, IPlayer> players = new Dictionary<string, IPlayer>();
        public IPlayer GetPlayer(string name, string type)
        {
            if (!players.TryGetValue(name, out var player))
            {
                player = new Player(name, type);
                players.Add(name, player);
            }
            return player;
        }
        public int GetPlayerCount()
        {
            return players.Count;
        }
    }
    #endregion
    #endregion

    #region 复合享元模式
    /*【30902：复合享元模式】
     * 复合享元模式的实现
     * 特点：内部状态和外部状态分离
     *      内部状态被共享
     *      外部状态由客户端维护
     *      工厂确保唯一实例
     *      支持树形结构嵌套
     *      所有节点都实现相同的接口
     */
    #region 复合享元模式基础结构
    public interface ICompositeFlyweight //复合享元接口 定义复合享元对象的操作
    {
        void Operation(string extrinsicState);
    }

    public class LeafFlyweight : ICompositeFlyweight //叶子节点 简单享元类 实现复合享元对象的操作
    {
        private readonly string intrinsicState; //内部状态 可共享

        public LeafFlyweight(string intrinsicState)
        {
            this.intrinsicState = intrinsicState;
            Console.WriteLine($"创建叶子享元对象，内部状态：{intrinsicState}");
        }

        public void Operation(string extrinsicState) //外部状态
        {
            Console.WriteLine($"执行操作 - 内部状态：{intrinsicState}, 外部状态：{extrinsicState}");
        }
    }

    public class CompositeFlyweight : ICompositeFlyweight //容器节点 复合享元类 实现复合享元对象的操作
    {
        private readonly List<ICompositeFlyweight> flyweights = new List<ICompositeFlyweight>();

        public void Add(ICompositeFlyweight flyweight)
        {
            flyweights.Add(flyweight);
        }

        public void Remove(ICompositeFlyweight flyweight)
        {
            flyweights.Remove(flyweight);
        }

        public void Operation(string extrinsicState)
        {
            Console.WriteLine($"【外部状态】{extrinsicState}");
            foreach (var flyweight in flyweights)
            {
                flyweight.Operation(extrinsicState); //调用叶子节点的操作
            }
        }
    }

    public class CompositeFlyweightFactory //复合享元工厂类 负责创建和管理复合享元对象
    {
        private Dictionary<string, ICompositeFlyweight> compositeFlyweights = new Dictionary<string, ICompositeFlyweight>();
        public ICompositeFlyweight GetCompositeFlyweight(string key)
        {
            if (!compositeFlyweights.TryGetValue(key, out var compositeFlyweight))
            {
                compositeFlyweight = new CompositeFlyweight();
                compositeFlyweights.Add(key, compositeFlyweight);
            }
            return compositeFlyweight;
        }
        public int GetCompositeFlyweightCount()
        {
            return compositeFlyweights.Count;
        }
    }
    #endregion

    #region 复合享元模式示例
    #endregion

    #endregion
}
