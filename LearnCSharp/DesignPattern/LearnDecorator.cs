/*【306：结构型————装饰器模式】
 * 定义：装饰器模式是一种结构型设计模式，它允许在不改变对象的情况下，动态地向对象添加额外的功能。
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
    // 装饰器模式
    // 装饰器模式允许在不修改对象的情况下，动态地向对象添加额外的功能。
    // 这种模式创建了一个装饰类，用于包装原始类，并在保持类方法签名的情况下提供额外的功能。
    // 适用场景：
    // 1. 当你需要在不改变对象的情况下，动态地添加功能时。
    // 2. 当你需要在运行时组合对象的功能时。
    // 3. 当你需要在不使用子类化的情况下，扩展对象的功能时。
    // 4. 当你需要在运行时添加或删除对象的功能时。
    // 5. 当你需要在运行时组合多个对象的功能时。

    //-------------------------------------------------

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
