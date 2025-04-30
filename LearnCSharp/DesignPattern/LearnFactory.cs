/*【302：工厂模式】*/

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

            // 创建一个圆形对象
            IShape circle = ShapeFactory.CreateShape("圆形");
            circle.Draw(); // 输出：绘制一个圆形

            // 创建一个矩形对象
            IShape rectangle = ShapeFactory.CreateShape("矩形");
            rectangle.Draw(); // 输出：绘制一个矩形

            // 创建一个多边形对象
            IShape polygon = ShapeFactory.CreateShape("多边形");
            polygon.Draw(); // 输出：绘制一个多边形

            // 创建一个无效类型的对象
            try
            {
                IShape invalidShape = ShapeFactory.CreateShape("无效类型");
                invalidShape.Draw();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message); // 输出：Invalid shape type
            }

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
            IDocumentFactory wordFactory = new WordDocumentFactory();
            DocumentFactoryClient wordClient = new DocumentFactoryClient(wordFactory);
            wordClient.GenerateDocument(); // 输出：生成Word文档

            // 创建一个PDF文档工厂
            IDocumentFactory pdfFactory = new PdfDocumentFactory();
            DocumentFactoryClient pdfClient = new DocumentFactoryClient(pdfFactory);
            pdfClient.GenerateDocument(); // 输出：生成PDF文档

            // 创建一个Excel文档工厂
            IDocumentFactory excelFactory = new ExcelDocumentFactory();
            DocumentFactoryClient excelClient = new DocumentFactoryClient(excelFactory);
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
    public interface IShape //定义一个接口，表示要创建的对象的类型
    {
        void Draw();// 绘制形状
    }

    public class Circle : IShape    //圆形类
    {
        public void Draw()=>Console.WriteLine("绘制一个圆形");
    }

    public class Rectangle : IShape //矩形类
    {
        public void Draw() => Console.WriteLine("绘制一个矩形");
    }

    public class Polygon : IShape   //多边形类
    {
        public void Draw() => Console.WriteLine("绘制一个多边形");
    }

    public class ShapeFactory   //简单工厂类
    {
        // 创建一个形状对象
        public static IShape CreateShape(string shapeType)
        {
            return shapeType switch
            {
                "圆形" => new Circle(),
                "矩形" => new Rectangle(),
                "多边形" => new Polygon(),
                _ => throw new ArgumentException("无效的图形类型")
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

    public interface IDocumentFactory   //文档工厂接口
    {
        IDocument CreateDocument(); //创建文档

        void ExportDocument()
        {
            IDocument document = CreateDocument();
            document.Generate();
            Console.WriteLine("导出完成");
        }
    }

    public class WordDocumentFactory : IDocumentFactory //Word文档工厂类
    {
        public IDocument CreateDocument() => new WordDocument();
    }

    public class PdfDocumentFactory : IDocumentFactory  //PDF文档工厂类
    {
        public IDocument CreateDocument() => new PdfDocument();
    }

    public class ExcelDocumentFactory : IDocumentFactory//Excel文档工厂类
    {
        public IDocument CreateDocument() => new ExcelDocument();
    }

    public class DocumentFactoryClient  //文档工厂客户端
    {
        private readonly IDocumentFactory _documentFactory;
        public DocumentFactoryClient(IDocumentFactory documentFactory)
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
     * 优点：符合开闭原则：可以通过添加新的工厂类和产品类来扩展新的产品类型，而不需要修改现有代码。
     *      可以将对象的创建和使用分离，提高代码的可维护性和可扩展性。
     *      符合单一职责原则：每个工厂类只负责创建一种类型的产品。
     * 缺点：增加了系统的复杂性，需要创建多个工厂类和产品类。
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
