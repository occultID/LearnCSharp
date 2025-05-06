/*【315：行为型————模板方法模式】*
 * 模板方法模式（Template Method Pattern）定义了一个操作中的算法的骨架，而将一些步骤延迟到子类中。模板方法使得子类可以在不改变算法结构的情况下重新定义该算法的某些特定步骤。
 * 模板方法模式通常用于一些算法的框架已经确定，但具体的实现细节需要在子类中实现的场景。
 * 模板方法模式的主要优点是可以将算法的结构和实现分离，使得代码更加清晰和易于维护。同时，模板方法模式也可以提高代码的复用性，因为子类可以重用父类中的算法框架，而只需要实现特定的步骤。
 * 模板方法模式的主要缺点是增加了类的数量，因为每个子类都需要实现特定的步骤。同时，模板方法模式也可能导致代码的复杂性增加，因为算法的结构和实现被分离到不同的类中。
 * 模板方法模式的主要组成部分包括：
 *  1. 抽象类（Abstract Class）：定义了算法的骨架，并声明了抽象方法和具体方法。
 *  2. 具体类（Concrete Class）：实现了抽象类中的抽象方法，并提供了具体的实现。
 *  3. 客户端（Client）：使用抽象类和具体类来执行算法。
 *  4. 模板方法（Template Method）：在抽象类中定义的算法的骨架，通常是一个具体的方法。
 *  5. 抽象方法（Abstract Method）：在抽象类中声明的方法，子类需要实现。
 *  6. 钩子方法（Hook Method）：在抽象类中声明的方法，子类可以选择性地重写。
 * 模板方法模式的实现步骤如下：
 *  1. 定义一个抽象类，声明一个模板方法和一些抽象方法。
 *  2. 在抽象类中实现模板方法，调用抽象方法和具体方法。
 *  3. 定义一个或多个具体类，继承抽象类，并实现抽象方法。
 *  4. 客户端使用具体类来执行算法。
 *  5. 客户端可以通过调用模板方法来执行算法，而不需要关心具体的实现细节。
 */

using LearnCSharp.DesignPattern.LearnTemplateMethodSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnTemplateMethod
    {
        /*【31501：模板方法模式】*/
        public static void LearnTemplateMethodDesignPattern()
        {
            Console.WriteLine("\n------示例：模板方法模式------\n");
            Console.WriteLine("》》》通过模板方法模式实现游戏固定流程《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建一个弓箭手的行为对象
            NpcBehavior archerBehavior = new ArcherBehavior();

            // 执行弓箭手的行为
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"第 {i + 1} 次执行：");

                archerBehavior.PerformAction();

                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnTemplateMethodSpace
{
    #region 模板方法模式基础结构
    /*【31500：模板方法模式基础结构】*/
    public abstract class AbstractClass // 模板类
    {
        // 模板方法，定义了算法的骨架
        public void TemplateMethod()
        {
            Step1();
            Step2();
            Step3();
        }
        // 抽象方法，子类需要实现
        protected abstract void Step1();
        // 钩子方法/虚方法，子类可选择实现
        protected virtual void Step2()
        {
            Console.WriteLine("Step2");
        }
        // 具体方法，子类可以重用
        protected void Step3()
        {
            Console.WriteLine("Step 3");
        }
    }

    public class ConcreteClassA : AbstractClass // 具体类，实现了抽象类中的抽象方法
    {
        protected override void Step1()
        {
            Console.WriteLine("ConcreteClassA Step 1");
        }
        protected override void Step2()
        {
            Console.WriteLine("ConcreteClassA Step 2");
        }
    }
    public class ConcreteClassB : AbstractClass // 具体类，实现了抽象类中的抽象方法
    {
        protected override void Step1()
        {
            Console.WriteLine("ConcreteClassB Step 1");
        }
        protected override void Step2()
        {
            Console.WriteLine("ConcreteClassB Step 2");
        }
    }
    #endregion

    #region 模板方法模式示例
    /*【31501：模板方法模式】*/
    public abstract class NpcBehavior // 抽象类，规定NPC的行为类
    {
        public void PerformAction() //NPC行为的模板方法
        {
            // 选择目标
            SelectTarget();

            // 判断是否需要移动，如果是，则移动到目标位置
            if (ShouldMove())
                MoveToTarget();

            // 执行动作
            ExecuteAction();

            // 行为冷却
            Cooldown();
        }

        protected abstract void SelectTarget(); // 抽象方法，选择目标

        protected abstract void ExecuteAction(); // 抽象方法，执行动作

        protected abstract void Cooldown(); // 抽象方法，行为冷却

        protected virtual bool ShouldMove() // 钩子方法/虚方法，判断是否需要移动
        {
            return true; // 默认需要移动
        }

        protected virtual void MoveToTarget() // 钩子方法/虚方法，移动到目标位置
        {
            Console.WriteLine("向目标移动");
        }
    }

    public class ArcherBehavior : NpcBehavior // 具体类，弓箭手的行为
    {
        protected override void SelectTarget()
        {
            Console.WriteLine("选择最近的目标");
        }

        protected override void ExecuteAction()
        {
            Console.WriteLine("射击目标");
        }

        protected override void Cooldown()
        {
            Console.WriteLine("射击冷却");
            Thread.Sleep(1000); // 模拟冷却时间
        }

        protected override bool ShouldMove() // 重写钩子方法，弓箭手不需要移动
        {
            return Random.Shared.Next(0, 10) > 3; // 70%概率移动
        }
    }
    #endregion
}