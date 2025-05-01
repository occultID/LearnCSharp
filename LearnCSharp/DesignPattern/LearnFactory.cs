/*【302：创建型————工厂模式】*/

using LearnCSharp.DesignPattern.LearnFactorySpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnFactory
    {
        /*【30201：简单工厂模式】*/
        public static void LearnSimpleFactory()
        {
            Console.WriteLine("\n------示例：简单工厂模式------\n");
            Console.WriteLine("》》》通过简单工厂模式创建不同对象《《《");
            Console.WriteLine("-----------------------------------------------");
            // 创建一个村民对象
            INPC villager = NPCFactory.CreateShape(NPCType.Villager, "村民");
            villager.Speak(); // 输出：村民：你好！我是村民
            // 创建一个冒险者对象
            INPC adventurer = NPCFactory.CreateShape(NPCType.Adventurer, "冒险者");
            adventurer.Speak(); // 输出：冒险者：你好！我是冒险者
            // 创建一个怪物对象
            INPC monster = NPCFactory.CreateShape(NPCType.Monster, "怪物");
            monster.Speak(); // 输出：怪物：你好！我是怪物
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30202：工厂方法模式】*/
        public static void LearnFactoryMethod()
        {
            Console.WriteLine("\n------示例：工厂方法模式------\n");
            Console.WriteLine("》》》通过工厂方法模式创建不同对象《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建一个Word文档工厂
            IDocumentFactory<WordDocument> wordFactory = new DocumentFactory<WordDocument>();
            DocumentFactoryClient<WordDocument> wordClient = new DocumentFactoryClient<WordDocument>(wordFactory);
            wordClient.GenerateDocument(); // 输出：生成Word文档

            // 创建一个PDF文档工厂
            IDocumentFactory<PdfDocument> pdfFactory = new DocumentFactory<PdfDocument>();
            DocumentFactoryClient<PdfDocument> pdfClient = new DocumentFactoryClient<PdfDocument>(pdfFactory);
            pdfClient.GenerateDocument(); // 输出：生成PDF文档

            // 创建一个Excel文档工厂
            IDocumentFactory<ExcelDocument> excelFactory = new DocumentFactory<ExcelDocument>();
            DocumentFactoryClient<ExcelDocument> excelClient = new DocumentFactoryClient<ExcelDocument>(excelFactory);
            excelClient.GenerateDocument(); // 输出：生成Excel文档
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30203：抽象工厂模式】*/
        public static void LearnAbstractFactory()
        {
            Console.WriteLine("\n------示例：抽象工厂模式------\n");
            Console.WriteLine("》》》通过抽象工厂模式创建不同对象《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建一个Windows UI工厂
            IUIFactory windowsFactory = new WindowsUIFactory();
            UIClient windowsClient = new UIClient(windowsFactory);
            windowsClient.RenderUI(); // 输出：渲染Windows按钮，渲染Windows文本框
            windowsClient.InteractWithUI(); // 输出：点击Windows按钮，输入文本：Hello, World!

            // 创建一个Mac UI工厂
            IUIFactory macFactory = new MacUIFactory();
            UIClient macClient = new UIClient(macFactory);
            macClient.RenderUI(); // 输出：渲染Mac按钮，渲染Mac文本框
            macClient.InteractWithUI(); // 输出：点击Mac按钮，输入文本：Hello, World!
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnFactorySpace
{
    // 工厂模式
    // 1. 定义一个接口或抽象类，表示要创建的对象的类型。
    // 2. 创建一个具体的工厂类，实现接口或继承抽象类，并实现创建对象的方法。
    // 3. 客户端代码使用工厂类来创建对象，而不是直接实例化对象。
    // 4. 可以根据需要扩展新的产品类型，只需添加新的工厂类和产品类，而不需要修改现有代码。

    //--------------------------------------------------------------------------------------

    #region 简单工厂模式
    /*【30201：简单工厂模式】
     * 适用场景：当需要创建的对象数量较少，且对象之间没有复杂的关系时，可以使用简单工厂模式。
     * 优点：简单易懂，易于实现和维护。
     *      适合产品类型固定的情况。
     *      客户端无需关注对象的创建细节，只需调用工厂方法即可。
     * 缺点：当需要创建的对象数量较多时，简单工厂模式会导致工厂类的代码变得复杂，难以维护。
     *      违反开闭原则：如果需要添加新的产品类型，需要修改工厂类的代码。
     */
    public interface INPC //定义一个接口，表示要创建的对象的类型
    {
        string Name { get;set; } //属性：名称
        void Speak(); //方法：说话
    }

    public class Villager : INPC    //村民类
    {
        public string Name { get; set; } //属性：名称
        public Villager(string name) => Name = name; //构造函数：初始化名称
        public void Speak() => Console.WriteLine($"{Name}：你好！我是{Name}"); //方法：说话
    }

    public class Adventurer : INPC //冒险者类
    {
        public string Name { get; set; } //属性：名称
        public Adventurer(string name) => Name = name; //构造函数：初始化名称
        public void Speak() => Console.WriteLine($"{Name}：你好！我是{Name}"); //方法：说话
    }

    public class Monster : INPC //怪物类
    {
        public string Name { get; set; } //属性：名称
        public Monster(string name) => Name = name; //构造函数：初始化名称
        public void Speak() => Console.WriteLine($"{Name}：你好！我是{Name}"); //方法：说话
    }

    public enum NPCType //枚举：NPC类型
    {
        Villager, //村民
        Adventurer, //冒险者
        Monster //怪物
    }

    public class NPCFactory   //简单工厂类
    {
        // 创建一个形状对象
        public static INPC CreateShape(NPCType type, string name)
        {
            return type switch
            {
                NPCType.Villager => new Villager(name), //创建村民对象
                NPCType.Adventurer => new Adventurer(name), //创建冒险者对象
                NPCType.Monster => new Monster(name), //创建怪物对象
                _ => throw new ArgumentException("Invalid NPC type") //抛出异常：无效的NPC类型
            };
        }
    }
    #endregion

    #region 工厂方法模式
    /*【30202：工厂方法模式】
     * 适用场景：当需要创建的对象数量较多，且对象之间有复杂的关系时，可以使用工厂方法模式。
     * 优点：符合开闭原则：可以通过添加新的工厂类和产品类来扩展新的产品类型，而不需要修改现有代码。
     *      可以将对象的创建和使用分离，提高代码的可维护性和可扩展性。
     * 缺点：增加了系统的复杂性，需要创建多个工厂类和产品类。
     */
    public interface IDocument  //文档接口
    {
        void Generate(); // 生成文档
    }

    public class WordDocument : IDocument   //Word文档类
    {
        public void Generate() => Console.WriteLine("生成Word文档");
    }

    public class PdfDocument : IDocument    //PDF文档类
    {
        public void Generate() => Console.WriteLine("生成PDF文档");
    }

    public class ExcelDocument : IDocument  //Excel文档类
    {
        public void Generate() => Console.WriteLine("生成Excel文档");
    }

    public interface IDocumentFactory<T> where T:class,IDocument   //文档工厂接口
    {
        T CreateDocument(); //创建文档

        void ExportDocument()
        {
            IDocument document = CreateDocument();
            document.Generate();
            Console.WriteLine("导出完成");
        }
    }

    public class DocumentFactory<T> : IDocumentFactory<T> where T:class,IDocument,new()//Word文档工厂类
    {
        public T CreateDocument() => new T();
    }

    public class DocumentFactoryClient<T> where T : class, IDocument, new()//文档工厂客户端
    {
        private readonly IDocumentFactory<T> _documentFactory;
        public DocumentFactoryClient(IDocumentFactory<T> documentFactory)
        {
            _documentFactory = documentFactory;
        }
        public void GenerateDocument()
        {
            _documentFactory.ExportDocument();
        }
    }
    #endregion

    #region 抽象工厂模式
    /*【30203：抽象工厂模式】
     * 适用场景：当需要创建的对象数量较多，且对象之间有复杂的关系时，可以使用抽象工厂模式。
     * 优点：部分符合开闭原则：可以通过添加新的工厂类来扩展新的产品族，而不需要修改现有代码。
     *      可以将对象的创建和使用分离，提高代码的可维护性和可扩展性。
     *      符合单一职责原则：每个工厂类只负责创建一种类型的产品。
     * 缺点：部分符合开闭原则：如果需要添加新的产品类型，需要修改工厂接口和所有实现类。
     *      增加了系统的复杂性，需要创建多个工厂类和产品类。
     */
    public interface IButton //按钮接口
    {
        void Render(); // 渲染按钮

        void Click(); // 点击按钮   
    }

    public interface ITextBox //文本框接口
    {
        void Render(); // 渲染文本框

        void Input(string text); // 输入文本
    }

    public class WindowsButton : IButton //Windows按钮类
    {
        public void Render() => Console.WriteLine("渲染Windows按钮");
        public void Click() => Console.WriteLine("点击Windows按钮");
    }

    public class WindowsTextBox : ITextBox //Windows文本框类
    {
        public void Render() => Console.WriteLine("渲染Windows文本框");
        public void Input(string text) => Console.WriteLine($"输入文本：{text}");
    }

    public class MacButton : IButton //Mac按钮类
    {
        public void Render() => Console.WriteLine("渲染Mac按钮");
        public void Click() => Console.WriteLine("点击Mac按钮");
    }

    public class MacTextBox : ITextBox //Mac文本框类
    {
        public void Render() => Console.WriteLine("渲染Mac文本框");
        public void Input(string text) => Console.WriteLine($"输入文本：{text}");
    }

    public interface IUIFactory //UI工厂接口
    {
        IButton CreateButton(); // 创建按钮
        ITextBox CreateTextBox(); // 创建文本框
    }

    public class WindowsUIFactory : IUIFactory //Windows UI工厂类
    {
        public IButton CreateButton() => new WindowsButton();
        public ITextBox CreateTextBox() => new WindowsTextBox();
    }

    public class MacUIFactory : IUIFactory //Mac UI工厂类
    {
        public IButton CreateButton() => new MacButton();
        public ITextBox CreateTextBox() => new MacTextBox();
    }

    public class UIClient //UI客户端
    {
        private readonly IButton _button;
        private readonly ITextBox _textBox;
        public UIClient(IUIFactory uiFactory)
        {
            _button = uiFactory.CreateButton();
            _textBox = uiFactory.CreateTextBox();
        }
        public void RenderUI()
        {
            _button.Render();
            _textBox.Render();
        }
        public void InteractWithUI()
        {
            _button.Click();
            _textBox.Input("Hello, World!");
        }
    }
    #endregion
}
