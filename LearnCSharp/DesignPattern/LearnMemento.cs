/*【：行为型————备忘录模式】
 * 备忘录模式（Memento Pattern）是一种行为型设计模式，它允许在不暴露对象内部状态的情况下捕获对象的内部状态，并在需要时恢复该状态。
 * 备忘录模式通常用于需要保存和恢复对象状态的场景，例如撤销操作、游戏存档等。
 * 备忘录模式的主要组成部分包括: 
 *  1. 发起人（Originator）：创建一个备忘录来保存当前状态，并可以使用备忘录恢复状态。
 *  2. 备忘录（Memento）：存储发起人的内部状态，但不允许外部访问。
 *  3. 管理者（Caretaker）：负责保存和管理备忘录，但不允许访问备忘录的内容。
 *  4. 具体发起人（Concrete Originator）：实现了发起人接口，定义了创建和恢复备忘录的方法。
 *  5. 具体备忘录（Concrete Memento）：实现了备忘录接口，存储发起人的状态。
 *  6. 具体管理者（Concrete Caretaker）：实现了管理者接口，负责保存和恢复备忘录。
 */

using LearnCSharp.DesignPattern.LearnMementoSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnMemento
    {
        /*【32101：备忘录模式】*/
        public static void LearnMementoDesignPattern()
        {
            Console.WriteLine("\n------示例：备忘录模式------\n");
            Console.WriteLine("》》》通过备忘录模式模拟文本编辑器存写状态《《《");
            Console.WriteLine("-----------------------------------------------");

            char[] chars = ['A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J'];

            // 创建发起人
            TextEditor editor = new TextEditor();

            // 创建管理者
            HistoryManager historyManager = new HistoryManager(editor);

            // 连续写入十个字符
            Console.WriteLine("连续写入十个字符:");
            for (int i = 0; i < 10; i++)
            {
                editor.Write($"{chars[i]}");
                historyManager.Save(editor);
                editor.Show();
            }

            Console.WriteLine();

            // 连续撤销五个字符
            Console.WriteLine("连续撤销五个字符:");
            for (int i = 0; i < 5; i++)
            {
                historyManager.Undo(editor);
                editor.Show();
            }

            Console.WriteLine();

            // 连续输入四个字符，随后连续撤销两个字符
            Console.WriteLine("连续输入四个字符，随后连续撤销两个字符:");
            for (int i = 0; i < 4; i++)
            {
                editor.Write($"{chars[i + 5]}");
                historyManager.Save(editor);
                editor.Show();
            }

            Console.WriteLine();

            for (int i = 0; i < 2; i++)
            {
                historyManager.Undo(editor);
                editor.Show();
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnMementoSpace
{
    #region 备忘录模式基础结构
    /*【32100：备忘录模式基础结构】*/
    public interface IOriginator // 发起人接口
    {
        object State { get; set; }
        Memento CreateMemento(); // 创建备忘录

        void RestoreMemento(Memento memento); // 恢复备忘录
    }

    public interface ICaretaker // 管理者接口
    {
        void SaveMemento(Memento memento); // 保存备忘录

        Memento GetMemento(); // 获取备忘录
    }

    public sealed class Memento // 备忘录
    {
        private readonly object state; // 存储的状态数据，只读保证不可变
        
        public Memento(object state) // 构造函数，传入状态数据
        {
            this.state = state;
        }

        public object GetSavedState() // 获取存储的状态数据
        {
            return state;
        }
    }

    public class ConcreteOriginator : IOriginator // 具体发起人
    {
        private object state; // 状态数据

        public object State // 状态数据属性
        {
            get => state;
            set => state = value;
        }

        public ConcreteOriginator(object state) // 构造函数，传入状态数据
        {
            this.state = state;
        }

        public Memento CreateMemento() // 创建备忘录
        {
            return new Memento(state);
        }

        public void RestoreMemento(Memento memento) // 恢复备忘录
        {
            state = memento.GetSavedState();
        }
    }

    public class ConcreteCaretaker : ICaretaker // 具体管理者
    {
        private Memento memento; // 备忘录

        public ConcreteCaretaker(Memento memento) // 构造函数，传入备忘录
        {
            this.memento = memento;
        }

        public void SaveMemento(Memento memento) // 保存备忘录
        {
            this.memento = memento;
        }

        public Memento GetMemento() // 获取备忘录
        {
            return memento;
        }
    }

    public class Client
    {
        public void TestMemento()
        {
            // 创建发起人
            ConcreteOriginator originator = new ConcreteOriginator("初始状态");

            // 创建备忘录
            Memento memento = originator.CreateMemento();

            // 创建管理者
            ConcreteCaretaker caretaker = new ConcreteCaretaker(memento);

            // 修改状态
            originator.State = "修改后的状态";

            // 保存备忘录
            caretaker.SaveMemento(originator.CreateMemento());

            // 恢复备忘录
            originator.RestoreMemento(caretaker.GetMemento());

            // 输出状态
            Console.WriteLine(originator.State); 
        }
    }
    #endregion

    #region 备忘录模式应用
    /*【32101：备忘录模式应用】*/
    public class TextEditor // 发起人,需要保存和恢复文本编辑器的状态
    {
        private string text = string.Empty; // 文本内容

        public void Write(string text)=> this.text += text; // 写入文本

        public TextSnapshot CreateSnapshot() => new TextSnapshot(text); // 创建快照(备忘录)

        public void Restore(TextSnapshot snapshot) => text = snapshot.GetSavedText(); // 恢复状态(从备忘录恢复)

        public void Show() => Console.WriteLine($"当前文本内容: {text}"); // 显示文本内容
    }

    public class TextSnapshot // 备忘录,存储文本编辑器的状态
    {
        private readonly string text; // 文本内容
        private readonly DateTime timestamp; // 快照时间戳

        public TextSnapshot(string text)
        {
            this.text = text;
            timestamp = DateTime.Now;
        }

        public string GetSavedText() => text; // 获取存储的文本内容
        public DateTime GetTimestamp() => timestamp; // 获取快照时间戳
    }

    public class HistoryManager // 管理者,负责保存和恢复文本编辑器的状态
    {
        private bool saveBeforeUndo = false; // 撤销前一次操作是否是保存快照
        private readonly Stack<TextSnapshot> snapshots = new Stack<TextSnapshot>(); // 保存的快照

        public HistoryManager(TextEditor editor)
        {
            if (editor is null) throw new ArgumentException("editor cannot be null");

            snapshots.Push(editor.CreateSnapshot()); // 保存初始状态
        }

        public void Save(TextEditor editor) // 保存快照
        {
            snapshots.Push(editor.CreateSnapshot());
            saveBeforeUndo = true;
        }

        public void Undo(TextEditor editor)
        {
            if (snapshots.Count > 0)
            {
                if (saveBeforeUndo)
                    snapshots.Pop();
                editor.Restore(snapshots.Pop());
            }

            saveBeforeUndo = false;
        }
    }
    #endregion
}
