/*【：行为型————中介者模式】
 * 中介者模式（Mediator Pattern）是一种行为型设计模式，它定义了一个中介对象来封装一组对象之间的交互。通过引入中介者对象，减少了对象之间的直接依赖关系，使得系统更加松耦合。
 * 中介者模式通常用于多个对象之间存在复杂的交互关系时，可以通过中介者来简化这些交互。
 * 中介者模式的主要组成部分包括：
 *  1. 中介者（Mediator）：定义了与各个同事对象交互的接口。
 *  2. 具体中介者（Concrete Mediator）：实现了中介者接口，协调各个同事对象之间的交互。
 *  3. 同事（Colleague）：定义了与中介者的交互接口。
 *  4. 具体同事（Concrete Colleague）：实现了同事接口，向中介者发送请求。
 *  5. 客户端（Client）：创建具体同事对象，并设置它们的中介者。
 */

using LearnCSharp.DesignPattern.LearnMediatorSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnMediator
    {
        /*【32001：中介者模式】*/
        public static void LearnMediatorDesignPattern()
        {
            Console.WriteLine("\n------示例：中介者模式------\n");
            Console.WriteLine("》》》通过中介者模式模拟UI事件通知《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建中介者
            var form = new Form();

            // 模拟UI事件通知
            form.SimulateUIEvents();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnMediatorSpace
{
    #region 中介者模式基础结构
    /*【32001：中介者模式基础结构】*/
    public interface IMediator // 中介者接口
    {
         void SendMessage(string message, IColleague colleague); // 发送消息
         void AddColleague(IColleague colleague); // 添加同事
    }

    public interface IColleague // 同事接口
    {
         void ReceiveMessage(string message, IMediator mediator); // 接收消息
    }

    public class ConcreteMediator : IMediator // 具体中介者
    {
         private List<IColleague> colleagues = new List<IColleague>(); // 同事集合

         public void AddColleague(IColleague colleague)
         {
             colleagues.Add(colleague);
         }

         public void SendMessage(string message, IColleague colleague)
         {
             foreach (var c in colleagues)
             {
                 if (c!= colleague)
                 {
                     c.ReceiveMessage(message, this);
                 }
             }
         }
    }

    public class ConcreteColleague : IColleague // 具体同事
    {
         private string name;
         private IMediator mediator;

         public ConcreteColleague(string name, IMediator mediator)
         {
             this.name = name;
             this.mediator = mediator;
             mediator.AddColleague(this);
         }

         public void ReceiveMessage(string message, IMediator mediator)
         {
             Console.WriteLine(name + " received message: " + message);
         }

         public void SendMessage(string message)
         {
             mediator.SendMessage(message, this);
         }
    }

    public class Client
    {
         public void TestMediator()
         {
             ConcreteMediator mediator = new ConcreteMediator();
             ConcreteColleague colleague1 = new ConcreteColleague("Colleague1", mediator);
             ConcreteColleague colleague2 = new ConcreteColleague("Colleague2", mediator);

             colleague1.SendMessage("Hello, Colleague2!");
             colleague2.SendMessage("Hello, Colleague1!");
         }
    }
    #endregion

    #region 中介者模式示例
    /*【32001：中介者模式示例】*/
    public interface IFormMediator // 中介者接口
    {
        void Notify(object sender, string eventType); // 通知
    }

    public abstract class Control // 控件基类
    {
        protected IFormMediator mediator; // 中介者

        public Control(IFormMediator mediator) // 构造函数
        {
            this.mediator = mediator;
        }
    }

    public class Button : Control // 按钮控件
    {
        public string Name{get;set;} = "Button";

        public Button(IFormMediator mediator) : base(mediator) { }

        public void Click()
        {
            Console.WriteLine("按钮点击");
            mediator.Notify(this, "Click");
        }
    }

    public class TextBox : Control // 文本框控件
    {
        public string Name { get; set; } = "TextBox";

        public string Text { get; set; }

        public TextBox(IFormMediator mediator) : base(mediator) { }

        public void Input(string text)
        {
            Text = text;
            Console.WriteLine("文本框输入");
            mediator.Notify(this, "Input");
        }
    }

    public class Form : IFormMediator // 事件中介者
    {
        private Button button;
        private TextBox textBox;

        public Form()
        {
            button = new Button(this);
            textBox = new TextBox(this);

            button.Name = "btnIssue";
            textBox.Name = "txtMessage";
        }
        
        public void Notify(object sender, string eventType)
        {
            switch (eventType)
            {
                case "Click":
                    Console.WriteLine($"按钮{(sender as Button)!.Name}被点击");
                    break;
                case "Input":
                    Console.WriteLine($"文本框{(sender as TextBox)!.Name}中输入：{(sender as TextBox)!.Text}");
                    break;
            }
        }

        public void SimulateUIEvents()
        {
            button.Click();
            textBox.Input("Hello, Mediator!");
        }
    }
    #endregion
}
