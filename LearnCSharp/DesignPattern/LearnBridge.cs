/*【307：结构型————桥接模式】
 * 桥接模式（Bridge Pattern）是将抽象部分与实现部分分离，使它们可以独立地变化。
 * 桥接模式通常用于将一个类的抽象部分与其实现部分分离，使得它们可以独立地变化。
 * 桥接模式的主要组成部分包括：
 *  1. 抽象类（Abstraction）：定义了抽象部分的接口，并维护一个对实现部分对象的引用。
 *  2. 扩展抽象类（Refined Abstraction）：扩展了抽象类，定义了具体的实现。
 *  3. 实现类（Implementor）：定义了实现部分的接口。
 *  4. 具体实现类（Concrete Implementor）：实现了实现类接口，提供具体的实现。
 *  5. 客户端（Client）：使用抽象类和实现类的组合来完成具体的功能。
 */

using LearnCSharp.DesignPattern.LearnBridgeSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnBridge
    {
        /*【30701：结构型————桥接模式】*/
        public static void LearnBridgeDesignPattern()
        {
            Console.WriteLine("\n------示例：桥接模式------\n");
            Console.WriteLine("》》》通过桥接模式创建角色和武器《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建一个武器
            IWeapon sword = new Sword();
            IWeapon wand = new Wand();
            IWeapon lance = new Lance();

            // 创建一个战士角色
            Warrior warrior = new Warrior(sword);
            warrior.Attack(); // 战士攻击
            // 为战士换武器
            warrior.SetWeapon(wand);
            warrior.Attack();
            // 切换到法师角色
            Mage mage = new Mage(wand);
            mage.Attack(); // 法师攻击

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnBridgeSpace
{
    // 桥接模式
    // 桥接模式是一种结构型设计模式，它将抽象部分与实现部分分离，使它们可以独立地变化。
    // 桥接模式通常用于以下场景：
    // 1. 当你想将一个类的抽象部分与其实现部分分离，使得它们可以独立地变化时。
    // 2. 当你想在多个不同的实现之间进行切换时。
    // 3. 当你想在运行时动态地选择实现时。
    // 4. 当你想避免在类中使用多个继承时。
    // 5. 当你想在一个类中使用多个实现时。
    // 6. 当你想在一个类中使用多个接口时。
    // 7. 当你想在一个类中使用多个抽象类时。
    // 8. 当你想在一个类中使用多个实现类时。

    //------------------------------------------------

    // 桥接模式的实现步骤：
    // 1. 定义一个抽象类，表示抽象部分，并维护一个对实现部分对象的引用。
    // 2. 定义一个实现类，表示实现部分，并提供具体的实现。
    // 3. 定义一个扩展抽象类，表示扩展的抽象部分，并实现抽象类的方法。
    // 4. 定义一个具体实现类，表示具体的实现部分，并实现实现类的方法。
    // 5. 客户端代码使用扩展抽象类和具体实现类的组合来完成具体的功能。
    // 6. 可以根据需要扩展新的扩展抽象类和具体实现类，只需实现抽象类和实现类的方法，而不需要修改现有代码。

    //------------------------------------------------

    #region 桥接模式
    /*【30701：桥接模式】
     * 定义：是一种结构型设计模式，它将抽象部分与实现部分分离，使它们可以独立地变化。
     * 特点：双重抽象--分离抽象和实现，形成两个独立的层次结构
     *      独立演化--抽象化和实现化可以独立演化
     *      运行时绑定--可以在运行时选择实现化，可以在运行时动态替换实现
     *      减少子类--避免因多维度扩展导致的类爆炸问题
     */
    public interface IWeapon //实现接口：武器接口，用于实现角色和武器的桥接，实现角色使用武器的功能
    {
        double Damage { get; } // 武器伤害
        void Attack();
    }

    public class Sword : IWeapon // 具体实现类：剑
    {
        public double Damage => 10; // 武器伤害
        public void Attack()         // 攻击方法
        {
            Console.WriteLine($"普通剑攻击，伤害：{Damage}"); // 攻击实现
        }
    }

    public class Lance : IWeapon // 具体实现类：长矛
    {
        public double Damage => 20; // 武器伤害
        public void Attack()         // 攻击方法
        {
            Console.WriteLine($"长矛攻击，伤害：{Damage}"); // 攻击实现
        }
    }

    public class Wand : IWeapon // 具体实现类：魔法杖
    {
        public double Damage => 15; // 武器伤害
        public void Attack()         // 攻击方法
        {
            Console.WriteLine($"魔法杖攻击，伤害：{Damage}"); // 攻击实现
        }
    }

    public abstract class Character // 抽象类：角色
    {
        protected IWeapon weapon; // 武器

        public Character(IWeapon weapon) // 构造函数
        {
            this.weapon = weapon;
        }
        public virtual void SetWeapon(IWeapon weapon) // 设置武器
        {
            this.weapon = weapon;
        }
        public abstract void Attack(); // 攻击方法
    }

    public class Warrior : Character // 具体类：战士
    {
        public Warrior(IWeapon weapon) : base(weapon) // 构造函数
        {
            Console.WriteLine("创建了一个战士");
        }

        public override void Attack() // 攻击方法
        {
            Console.WriteLine("战士攻击：");
            weapon.Attack(); // 使用武器攻击
        }
    }

    public class Mage : Character // 具体类：法师
    {
        public Mage(IWeapon weapon) : base(weapon) // 构造函数
        {
            Console.WriteLine("创建了一个法师");
        }
        public override void Attack() // 攻击方法
        {
            Console.WriteLine("法师攻击：");
            weapon.Attack(); // 使用武器攻击
        }
    }
    #endregion


}
