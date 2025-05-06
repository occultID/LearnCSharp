/*【306：结构型————装饰器模式】
 * 装饰器模式（Decorator Pattern）是一种结构型设计模式，它允许在不改变对象的情况下，动态地向对象添加额外的功能。
 * 装饰器模式通常用于以下场景：
 *  1. 当你需要在运行时动态地添加功能到一个对象时。
 *  2. 当你需要在不改变对象的情况下，扩展对象的功能时。
 *  3. 当你需要在不使用子类化的情况下，添加功能到一个对象时。
 *  4. 当你需要在不改变对象的情况下，添加行为到一个对象时。
 * 装饰器模式的优点：
 *  1. 可以在运行时动态地添加功能到一个对象。
 *  2. 可以在不改变对象的情况下，扩展对象的功能。
 *  3. 可以在不使用子类化的情况下，添加功能到一个对象。
 *  4. 可以在不改变对象的情况下，添加行为到一个对象。
 *  5. 可以通过组合多个装饰器来创建复杂的功能、行为、对象。
 * 装饰器模式的缺点：
 *  1. 增加了系统的复杂性，因为需要创建多个装饰器类。
 *  2. 可能会导致类的数量增加，从而增加了系统的复杂性。
 *  3. 可能会导致装饰器的嵌套过深，从而增加了系统的复杂性。
 *  4. 可能会导致装饰器的顺序影响最终结果，从而增加了系统的复杂性。
 *  5. 可能会导致装饰器的组合过多，从而增加了系统的复杂性。
 * 装饰器模式的主要组成部分包括：
 *  1. 抽象组件（Component）：定义一个接口，用于定义被装饰的对象的行为。
 *  2. 具体组件（ConcreteComponent）：实现抽象组件接口，定义被装饰的对象的行为。
 *  3. 抽象装饰器（Decorator）：实现抽象组件接口，持有一个抽象组件的引用，并定义装饰器的行为。
 *  4. 具体装饰器（ConcreteDecorator）：实现抽象装饰器接口，定义具体的装饰器的行为。
 *  5. 客户端（Client）：使用抽象组件接口来操作被装饰的对象。
 * 装饰器模式的实现步骤：
 *  1. 定义一个抽象组件接口，定义被装饰的对象的行为。
 *  2. 实现抽象组件接口，定义被装饰的对象的行为。
 *  3. 定义一个抽象装饰器类，实现抽象组件接口，持有一个抽象组件的引用，并定义装饰器的行为。
 *  4. 实现抽象装饰器类，定义具体的装饰器的行为。
 *  5. 在客户端使用抽象组件接口来操作被装饰的对象。
 */
using LearnCSharp.DesignPattern.LearnAdapterSpace;
using LearnCSharp.DesignPattern.LearnDecoratorSpace;
namespace LearnCSharp.DesignPattern
{
    internal class LearnDecorator
    {
        /*【30601：装饰器模式】*/
        public static void LearnDecoratorDesignPattern()
        {
            Console.WriteLine("\n------示例：装饰器模式------\n");
            Console.WriteLine("》》》通过装饰器模式创建装饰对象《《《");
            Console.WriteLine("-----------------------------------------------");

            Console.WriteLine("》》》创建一个武器——剑，并进行附魔");
            IWeapon sword = new Sword(); // 创建一个剑
            sword.Attack(); // 普通攻击
            IWeapon fireSword = new FireDecorator(sword); // 添加火焰装饰器
            fireSword.Attack(); // 火焰攻击
            IWeapon poisonSword = new PoisonDecorator(sword); // 添加毒素装饰器
            poisonSword.Attack();
            // 火焰+毒素攻击
            IWeapon firePoisonSword = new FireDecorator(new PoisonDecorator(sword)); // 添加火焰+毒素装饰器
            firePoisonSword.Attack(); // 火焰+毒素攻击

            Console.WriteLine();

            Console.WriteLine("》》》创建一个武器——弓，并进行附魔");
            IWeapon bow = new Bow(); // 创建一个弓
            bow.Attack();
            IWeapon fireBow = new FireDecorator(bow); // 添加火焰装饰器
            fireBow.Attack(); // 火焰攻击
            IWeapon poisonBow = new PoisonDecorator(bow); // 添加毒素装饰器
            poisonBow.Attack(); // 毒素攻击
            // 火焰+毒素攻击
            IWeapon firePoisonBow = new FireDecorator(new PoisonDecorator(bow)); // 添加火焰+毒素装饰器
            firePoisonBow.Attack();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
        
    }
}

namespace LearnCSharp.DesignPattern.LearnDecoratorSpace
{
    #region 装饰器模式
    /*【30601：装饰器模式】
     * 定义：装饰器模式是一种结构型设计模式，它允许在不改变对象的情况下，动态地向对象添加额外的功能。
     * 特点：运行时动态添加功能，灵活性高。
     *      装饰顺序会影响最终结果。
     *      保持接口一致性，避免了类爆炸。
     *      保持接口原有的功能，避免了类的继承。
     */
    #region 模拟原本存在的类型
    public interface IWeapon    // 抽象组件：武器接口
    {
        double Damage { get; } // 武器伤害
        void Attack();         // 攻击方法
    }

    public class Sword : IWeapon //具体组件：具体武器类
    {
        public double Damage => 10; // 武器伤害
        public void Attack()         // 攻击方法
        {
            Console.WriteLine($"普通剑攻击，伤害：{Damage}"); // 攻击实现
        }
    }

    public class Bow : IWeapon   // 具体组件：具体武器类
    {
        public double Damage => 8; // 武器伤害
        public void Attack()         // 攻击方法
        {
            Console.WriteLine($"普通弓攻击，伤害：{Damage}"); // 攻击实现
        }
    }
    #endregion

    public abstract class WeaponDecorator : IWeapon // 装饰器抽象类
    {
        protected IWeapon weapon; // 被装饰的武器

        public WeaponDecorator(IWeapon weapon) // 构造函数
        {
            this.weapon = weapon;
        }

        public virtual double Damage => weapon.Damage; //属性装饰器：武器伤害

        public virtual void Attack() //方法/行为/功能装饰器：攻击方法
        {
            weapon.Attack(); // 调用被装饰的武器的攻击方法
        }
    }

    public class FireDecorator : WeaponDecorator // 装饰器类：火焰装饰器
    {
        public FireDecorator(IWeapon weapon) : base(weapon) { } // 构造函数
        public override double Damage => weapon.Damage + 5; // 属性装饰器：武器伤害
        public override void Attack() // 方法/行为/功能装饰器：攻击方法
        {
            base.Attack(); // 调用被装饰的武器的攻击方法
            Console.WriteLine($"附魔火焰攻击，额外伤害：{Damage - weapon.Damage}"); // 攻击实现
        }
    }

    public class PoisonDecorator : WeaponDecorator // 装饰器类：毒素装饰器
    {
        public PoisonDecorator(IWeapon weapon) : base(weapon) { } // 构造函数
        public override double Damage => weapon.Damage + 3; // 属性装饰器：武器伤害
        public override void Attack() // 方法/行为/功能装饰器：攻击方法
        {
            base.Attack(); // 调用被装饰的武器的攻击方法
            Console.WriteLine($"附魔毒素攻击，额外伤害：{Damage - weapon.Damage}"); // 攻击实现
        }
    }
    #endregion
}
