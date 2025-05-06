/*【308：结构型————组合模式】
 * 组合模式（Composite Pattern）是一种结构型设计模式，它允许你将对象组合成树形结构来表示部分-整体的层次结构。组合模式使得客户端对单个对象和组合对象的使用具有一致性。
 * 组合模式的主要优点是可以简化客户端代码，因为客户端可以使用相同的接口来处理单个对象和组合对象。组合模式通常用于表示树形结构，例如文件系统、组织结构等。
 * 组合模式的主要缺点是可能会导致设计过于复杂，因为它允许你将对象组合成任意层次结构，这可能会使得系统变得难以理解和维护。
 * 组合模式的主要组成部分包括：
 *  1. 抽象组件（Component）：定义了组合对象和叶子对象的共同接口。
 *  2. 叶子对象（Leaf）：实现了抽象组件接口，表示树形结构中的叶子节点。
 *  3. 组合对象（Composite）：实现了抽象组件接口，表示树形结构中的组合节点，可以包含叶子节点和其他组合节点。
 *  4. 客户端（Client）：使用组合对象和叶子对象的代码。
 * 组合模式的实现步骤：
 *  1. 定义一个抽象组件类，表示组合对象和叶子对象的共同接口。
 *  2. 定义一个叶子对象类，继承抽象组件类，实现具体的功能。
 *  3. 定义一个组合对象类，继承抽象组件类，实现组合对象的功能。
 *  4. 在组合对象类中，维护一个列表，用于存储子组件（叶子对象和其他组合对象）。
 *  5. 实现添加、删除和获取子组件的方法。
 *  6. 实现显示方法，用于递归显示组合结构。
 *  7. 客户端代码使用组合对象和叶子对象的接口来操作组合结构。
 *  8. 可以根据需要扩展新的叶子对象和组合对象，只需实现抽象组件类的方法，而不需要修改现有代码。
 */

using LearnCSharp.DesignPattern.LearnCompositeSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnComposite
    {
        /*【30801：透明式组合模式】*/
        public static void LearnTransparentCompositePattern()
        {
            Console.WriteLine("\n------示例：透明式组合模式------\n");
            Console.WriteLine("》》》透明式组合模式《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建叶子节点
            NPCInfo hugeMan = new NPCInfo { Name = "HugeMan", Description = "This is a HugeMan" };
            NPCInfo smallMan = new NPCInfo { Name = "SmallMan", Description = "This is a SmallMan" };
            NPCInfo snakeMonster = new NPCInfo { Name = "SnakeMonster", Description = "This is a SnakeMonster" };
            NPCInfo dragonMonster = new NPCInfo { Name = "DragonMonster", Description = "This is a DragonMonster" };

            // 创建组合节点
            NPCTypeInfo npc = new NPCTypeInfo { Name = "NPC", Description = "This is NPC infos" };
            NPCTypeInfo human = new NPCTypeInfo { Name = "Human", Description = "This is Human infos" };
            NPCTypeInfo monster = new NPCTypeInfo { Name = "Monster", Description = "This is Monster infos" };

            // 添加叶子节点到组合节点
            npc.Add(human);
            npc.Add(monster);
            human.Add(hugeMan);
            human.Add(smallMan);
            monster.Add(snakeMonster);
            monster.Add(dragonMonster);

            // 显示组合结构
            Console.WriteLine("NPC 组合结构：");
            npc.Display(1);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30802：安全式组合模式】*/
        public static void LearnSafeCompositePattern()
        {
            Console.WriteLine("\n------示例：安全式组合模式------\n");
            Console.WriteLine("》》》安全式组合模式《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建一个叶子节点
            SafeLeaf safeLeaf1 = new SafeLeaf { Name = "SafeLeaf 1" };
            SafeLeaf safeLeaf2 = new SafeLeaf { Name = "SafeLeaf 2" };

            // 创建一个组合节点
            SafeComposite safeComposite = new SafeComposite { Name = "SafeComposite" };
            safeComposite.Add(safeLeaf1);
            safeComposite.Add(safeLeaf2);

            // 显示组合结构
            safeComposite.Display(1);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnCompositeSpace
{
    #region 透明式组合模式
    /*【30801：透明式组合模式 标准实现】*
     * 透明式组合模式（Transparent Composite Pattern）是一种组合模式的实现方式，它允许客户端使用相同的接口来处理单个对象和组合对象。透明式组合模式的主要优点是简化了客户端代码，因为客户端可以使用相同的接口来处理单个对象和组合对象。
     * 特点：统一叶子与容器的接口，客户端可以使用相同的接口来处理单个对象和组合对象。
     *      客户端无需区分节点类型
     *      叶子节点需实现空管理方法，这违反接口隔离原则
     */
    public abstract class AbsNPCInfo //抽象组件：定义统一的接口
    {
        //公共方法
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public abstract void Display(int depth); //显示方法

        //透明式添加管理方法（叶子节点需要空实现）
        public virtual void Add(AbsNPCInfo npcInfo) //添加方法
        {
            throw new NotImplementedException("Cannot add to a leaf");
        }

        public virtual void Remove(AbsNPCInfo npcInfo)
        {
            throw new NotImplementedException("Cannot remove from a leaf");
        }

        public virtual AbsNPCInfo GetChild(int index)
        {
            throw new NotImplementedException("Cannot get child from a leaf");
        }
    }

    public class NPCInfo : AbsNPCInfo //叶子节点 无子项
    {
        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-',depth)} {Name}：{Description}");
        }
    }

    public class NPCTypeInfo : AbsNPCInfo //容器节点 可包含子项
    {
        private readonly List<AbsNPCInfo> children = new List<AbsNPCInfo>();

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)} {Name}: {Description}");
            //递归显示子项
            foreach (var child in children)
            {
                child.Display(depth + 2);
            }
        }

        public override void Add(AbsNPCInfo npcInfo) //添加方法
        {
            children.Add(npcInfo);
        }

        public override void Remove(AbsNPCInfo npcInfo) //删除方法
        {
            children.Remove(npcInfo);
        }

        public override AbsNPCInfo GetChild(int index) //获取子项
        {
            return children[index];
        }
    }
    #endregion

    #region 安全式组合模式
    /*【30802：安全式组合模式 变体实现】*
     * 安全式组合模式（Safe Composite Pattern）是一种组合模式的实现方式，它允许客户端使用不同的接口来处理单个对象和组合对象。安全式组合模式的主要优点是避免了在叶子节点中实现空管理方法，从而遵循了接口隔离原则。
     * 特点：不统一叶子与容器的接口，客户端需区分节点类型
     *      叶子节点不需实现空管理方法，遵循接口隔离原则
     *      类型安全更明确
     *      失去透明性优势
     */
    public abstract class SafeComponent //抽象组件：仅定义公共方法
    {
        public virtual string Name { get; set; }
        public abstract void Display(int depth); //显示方法
    }

    public interface IComposite //容器接口：单独定义管理方法
    {
        void Add(SafeComponent component);
        void Remove(SafeComponent component);
        SafeComponent GetChild(int index);
    }

    public class SafeLeaf : SafeComponent //叶子节点 无子项
    {
        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)} {Name}");
        }
    }

    public class SafeComposite : SafeComponent, IComposite //容器节点 可包含子项
    {
        private readonly List<SafeComponent> children = new List<SafeComponent>();
        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)} {Name}");
            //递归显示子项
            foreach (var child in children)
            {
                child.Display(depth + 2);
            }
        }
        public void Add(SafeComponent component) //添加方法
        {
            children.Add(component);
        }
        public void Remove(SafeComponent component) //删除方法
        {
            children.Remove(component);
        }
        public SafeComponent GetChild(int index) //获取子项
        {
            return children[index];
        }
    }
    #endregion
}
