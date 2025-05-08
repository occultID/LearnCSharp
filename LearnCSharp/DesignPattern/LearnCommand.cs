/*【317：行为型————命令模式】
 * 命令模式（Command Pattern）是将请求封装为对象，从而使你可用不同的请求对客户进行参数化。
 * 命令模式可以将请求排队或记录请求日志，以及支持可撤销的操作。
 * 命令模式通常用于以下场景：
 *  1. 需要将请求调用者和请求接收者解耦。
 *  2. 需要支持撤销操作。
 *  3. 需要支持日志记录或请求排队。
 *  4. 需要支持宏命令、事务、异步操作、多线程操作、分布式操作。
 *  5. ……
 * 命令模式的优点:
 *  1. 将请求封装为对象，可以将请求参数化。
 *  2. 可以将请求排队或记录请求日志。
 *  3. 可以支持可撤销的操作。
 *  4. 可以支持宏命令、事务、异步操作、多线程操作、分布式操作。
 *  5. 可以将请求调用者和请求接收者解耦。
 * 命令模式的缺点:
 *  1. 需要定义命令接口和具体命令类，增加了系统的复杂性。
 *  2. 需要定义命令对象，增加了系统的内存开销。
 *  3. 需要定义命令对象的生命周期，增加了系统的复杂性。
 *  4. ……
 * 命令模式的主要组成部分:
 *  1. 命令接口（Command）：定义命令的接口，声明执行命令的方法。
 *  2. 具体命令类（ConcreteCommand）：实现命令接口，定义请求的接收者和执行请求的方法。
 *  3. 请求接收者（Receiver）：定义请求的接收者，执行请求的方法。
 *  4. 调用者（Invoker）：定义请求的调用者，调用命令对象的方法。
 *  5. 客户端（Client）：创建命令对象和请求接收者，设置请求的参数。
 *  6. 命令对象（Command Object）：封装请求的对象，包含命令接口和具体命令类。
 *  7. 命令队列（Command Queue）：存储命令对象的队列，支持命令的排队和执行。
 *  8. 命令日志（Command Log）：存储命令对象的日志，支持命令的记录和撤销。
 *  9. 命令管理器（Command Manager）：管理命令对象的生命周期，支持命令的创建和销毁。
 *  10. ……
 * 命令模式的实现步骤:
 *  1. 定义命令接口，声明执行命令的方法。
 *  2. 定义具体命令类，实现命令接口，定义请求的接收者和执行请求的方法。
 *  3. 定义请求接收者，执行请求的方法。
 *  4. 定义请求调用者，调用命令对象的方法。
 *  5. 定义客户端，创建命令对象和请求接收者，设置请求的参数。
 */

using LearnCSharp.DesignPattern.LearnCommandSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnCommand
    {
        /*【31701：命令模式】*/
        public static void LearnCommandDesignPattern()
        {
            Console.WriteLine("\n------示例：命令模式------\n");
            Console.WriteLine("》》》通过命令模式模拟文本编辑器的编辑功能《《《");
            Console.WriteLine("-----------------------------------------------");

            //创建接收者和调用者对象
            var txtEditor = new TxtEditor();
            var toolbar = new TxtEditorToolBar();

            //为接收者绑定命令
            var editorCommand = new EditorCommand(txtEditor);

            //为调用者绑定命令
            toolbar.SetCommand(editorCommand);

            Console.WriteLine("》》》通过调用者连续进行20次文本编辑");

            //通过调用者调用命令
            for (int i = 0; i < 20; i++)
            {
                if (i % 3 == 0)
                    toolbar.InputString(); //模拟文本输入
                else
                    toolbar.ClickRandomWrite(); //随机生成字符串
            }

            Console.WriteLine();
            Console.WriteLine("》》》当前显示文本：");

            toolbar.Display();

            Console.WriteLine();

            Console.ReadKey();
            Console.WriteLine("》》》通过调用者连续进行12次撤销");
            
            for (int i = 0; i < 12; i++)
            { 
                toolbar.ClickUndo(); //模拟撤销操作，连续撤销十二次
            }

            Console.WriteLine();
            Console.WriteLine("》》》当前显示文本：");

            toolbar.Display();

            Console.WriteLine();

            Console.ReadKey();
            Console.WriteLine("》》》通过调用者连续进行12次重做");
            
            for (int i = 0; i < 12; i++)
            {
                toolbar.ClickRedo(); //模拟重做操作，连续重做十二次
            }

            Console.WriteLine();
            Console.WriteLine("》》》当前显示文本：");

            toolbar.Display();

            Console.WriteLine();

            Console.ReadKey();
            Console.WriteLine("》》》通过调用者连续进行8次撤销，再进行3次重做，再连续输入2次，再进行1次重做和10次撤销");
            
            for (int i = 0; i < 8; i++)
            {
                toolbar.ClickUndo(); //连续撤销8次
            }

            Console.WriteLine();

            for (int i = 0;i < 3; i++)
            {
                toolbar.ClickRedo(); //连续重做3次
            }

            Console.WriteLine();

            for (int i = 0; i < 2; i++)
            {
                toolbar.ClickRandomWrite(); //连续输入2次
            }

            Console.WriteLine();
            
            toolbar.ClickRedo(); //重做

            Console.WriteLine();
            
            for (int i = 0; i < 10; i++)
            {
                toolbar.ClickUndo(); //连续撤销10次
            }

            Console.WriteLine();
            Console.WriteLine("》》》当前显示文本：");

            toolbar.Display();

            Console.WriteLine();

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnCommandSpace
{
    #region 命令模式基础结构
    /*【31700：命令模式基础结构】*/
    public interface ICommand // 命令接口
    {
        void Execute(); // 执行命令
        void Undo(); // 撤销命令
    }

    public class ConcreteCommand : ICommand // 具体命令类
    {
        private readonly Receiver receiver; // 请求接收者
        public ConcreteCommand(Receiver receiver)
        {
            this.receiver = receiver;
        }
        public void Execute() // 执行命令
        {
            receiver.Action(); // 调用请求接收者的方法
        }
        public void Undo() // 撤销命令
        {
            receiver.UndoAction(); // 调用请求接收者的撤销方法
        }
    }

    public class Receiver // 请求接收者 实际执行操作
    {
        public void Action() // 执行请求的方法
        {
            Console.WriteLine("接收者：操作执行");
        }
        public void UndoAction() // 撤销请求的方法
        {
            Console.WriteLine("接收者：操作撤销");
        }
    }

    public class Invoker // 请求调用者 触发命令
    {
        private ICommand command; // 命令对象
        public void SetCommand(ICommand command) // 设置命令对象
        {
            this.command = command;
        }
        public void ExecuteCommand() // 执行命令
        {
            command.Execute();
        }
        public void UndoCommand() // 撤销命令
        {
            command.Undo();
        }
    }

    public class Client // 客户端
    {
        public void Execute()
        {
            // 创建请求接收者
            Receiver receiver = new Receiver();
            // 创建具体命令对象
            ConcreteCommand command = new ConcreteCommand(receiver);
            // 创建请求调用者
            Invoker invoker = new Invoker();
            invoker.SetCommand(command); // 设置命令对象
            // 执行命令
            invoker.ExecuteCommand();
            // 撤销命令
            invoker.UndoCommand();
        }
    }
    #endregion

    #region
    /*【31701：命令模式示例】
     * 命令模式代码示例
     */
    public interface IEditorCommand //编辑操作命令
    {
        void ExecuteWriting(string text); //执行
        void Undo(); //撤销
        void Redo(); //重做

        void Print();
    }

    public class EditorCommand : IEditorCommand //具体的编辑命令
    {
        private readonly TxtEditor editor;

        public EditorCommand(TxtEditor editor)
        {
            this.editor = editor;
        }

        public void ExecuteWriting(string text)
        {
            editor.Write(text);
        }

        public void Print()
        {
            editor.PrintText();
        }

        public void Redo()
        {
            editor.ReWrite();
        }

        public void Undo()
        {
            editor.UnWrite();
        }
    }

    public class TxtEditor //接收者 编辑器类
    {
        private readonly Stack<string> strings = new Stack<string>();       //文本列表，用于存储写入的字符串
        private readonly Stack<string> undoStrings;                         //重做列表，用于存储撤销的字符串
        private int undoCount = 0;      //记录当前已撤销次数

        public int MaxUndoCount { get; private set; }           //存储最大允许的撤销和重做次数

        public TxtEditor(int maxUndoCount = 10)         //初始化编辑器
        {
            MaxUndoCount = maxUndoCount;
            undoStrings = new Stack<string>(maxUndoCount);
        }

        public bool CanUndo => strings.Count > 0;       //存储和验证当前是否支持撤销
        public bool CanRedo => undoStrings.Count > 0;   //存储和验证当前是否支持重做

        public void Write(string text)                  //模拟写入字符串
        {
            strings.Push(text);                         //存储写入的字符串
            undoStrings.Clear();                        //每次写入新字符串都将重置重做许可
            if(undoCount > 0)
                undoCount--;                            //每次写入新字符串都将恢复一次撤销次数
        }

        public void UnWrite()                           //撤销操作
        {
            if (CanUndo && undoCount < MaxUndoCount)    //验证是否可撤销
            {
                var undoString = strings.Pop();
                Console.WriteLine($"已撤销：{undoString}");
                undoStrings.Push(undoString);        //撤销上一步，并将内容记录到重做列表
                undoCount++;                            //消耗依次撤销步数
            }
            else
                Console.WriteLine("无法撤销");      //提示无法继续撤销
        }

        public void ReWrite()                           //重做操作
        {
            if (CanRedo)
            {
                var redoString = undoStrings.Pop();
                Console.WriteLine($"已重做：{redoString}");
                strings.Push(redoString);        //恢复下一步
                undoCount--;                            //恢复一次可撤销次数
            }
            else
                Console.WriteLine("无法重做");
        }

        public void PrintText()
        {
            if (!CanUndo)
                return;

            var descendingStrings = strings.ToArray();
            descendingStrings.Reverse();

            foreach (string text in descendingStrings) 
            {
                Console.WriteLine(text); 
            }
        }
    }

    public class TxtEditorToolBar //调用者，触发命令
    {
        private IEditorCommand command;
        private int index = 0;

        private List<string> poems = new List<string>()
            {
                "众里寻他千百度，蓦然回首，那人却在灯火阑珊处。",
                "无边落木萧萧下，不尽长江滚滚来。",
                "多情自古伤离别，更那堪冷落清秋节。",
                "泪眼问花花不语，乱红飞过秋千去。",
                "竹杖芒鞋轻胜马，谁怕？一蓑烟雨任平生。",
                "此情无计可消除，才下眉头，却上心头。",
                "昨夜西风凋碧树，独上高楼，望尽天涯路。",
                "最是人间留不住，朱颜辞镜花辞树。",
                "洛阳亲友如相问，一片冰心在玉壶。",
                "抽刀断水水更流，举杯消愁愁更愁。",
                "玲珑骰子安红豆，入骨相思知不知？",
                "两情若是久长时，又岂在朝朝暮暮。",
                "胭脂泪，相留醉，几时重？自是人生长恨水长东。",
                "曾经沧海难为水，除却巫山不是云。",
                "何当共剪西窗烛，却话巴山夜雨时。",
                "欲买桂花同载酒，终不似，少年游。",
                "梦里不知身是客，一晌贪欢。",
                "流水落花春去也，天上人间。",
                "林花谢了春红，太匆匆。无奈朝来寒雨晚来风。",
                "独自莫凭栏，无限江山。别时容易见时难。"
            };

        public void SetCommand(IEditorCommand command)
        {
            this.command = command;
        }

        public void InputString()
        {
            Console.Write("请输入任意文字：");
            string input = Console.ReadLine();
            
            Console.WriteLine();
            command.ExecuteWriting(input);
        }

        public void ClickRandomWrite()
        {
            if(index == 20)
                index = 0;

            Console.WriteLine($"自动随机输入：{poems[index]}");
            command.ExecuteWriting(poems[index]);
            index++;
        }

        public void ClickUndo()
        {
            command.Undo();
        }

        public void ClickRedo() 
        {
            command.Redo();
        }

        public void Display()
        {
            command.Print();
        }
    }
    #endregion
}
