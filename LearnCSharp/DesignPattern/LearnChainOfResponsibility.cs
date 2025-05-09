/*【318：行为型————责任链模式】
 * 责任链模式（Chain of Responsibility Pattern）是一种行为型设计模式，它使多个对象都有机会处理请求，从而避免请求的发送者和接收者之间的耦合关系。
 * 责任链条上的每个对象都有责任处理请求，如果该对象不能处理该请求，它会把请求传给下一个对象，依此类推，直到请求被处理。
 * 责任链模式通常包含一个抽象处理者（Handler）和多个具体处理者（ConcreteHandler），处理者之间形成一条链，请求在这条链上传递，直到有对象处理它为止。
 * 责任链模式通常用于以下场景：
 * 1. 日志记录：记录软件运行过程中的事件，如错误、警告、信息等。
 * 2. 验证：对用户输入、数据、文件等进行验证，如密码验证、身份验证等。
 * 3. 事件处理：允许多个对象处理同一事件，如鼠标点击、键盘按下等。
 * 4. 职责链：多个对象可以处理请求，并将请求沿着链传递，直到有对象处理它为止。
 * 5. 资源池：对象可以请求资源，如果资源池中有空闲资源，则分配资源；否则，将请求传递给下一个对象。
 * 6. 事务处理：多个对象可以处理事务，如提交、回滚等。
 * 7. 多级反馈：一个请求可以沿着链传递，并得到反馈，如批准、拒绝、重试等。
 * 责任链模式的优点：
 * 1. 降低耦合度：它将请求的发送者和接收者解耦，使得发送者无需知道接收者的存在，接收者也无需知道链中下一个对象是谁。
 * 2. 简化对象：使得对象不必相互了解，只需知道链中下一个对象是谁即可，简化了对象的设计。
 * 3. 增强给对象指派职责：可以动态地增加、删除或者修改链中对象，使得系统更加灵活。
 * 4. 增加新的请求处理方式：可以根据需要增加新的处理方式，使得系统更加灵活。
 * 责任链模式的缺点：
 * 1. 请求可能涉及到很多对象，系统性能可能受到影响。
 * 2. 调试困难：由于每个对象都有责任处理请求，调试时可能需要逐个排查。
 * 3. 可能会造成请求的处理速度慢。
 * 4. 可能会造成内存溢出。
 * 责任链的主要组成部分：
 * 1. 抽象处理者（Handler）：定义了一个接口，处理请求的方法。
 * 2. 具体处理者（ConcreteHandler）：实现了抽象处理者接口，处理请求。
 * 3. 客户端（Client）：向链头的具体处理者发送请求。
 * 4. 链（Chain）：由多个具体处理者组成，按顺序串联。
 * 5. 链头（Chain Head）：链的第一个具体处理者。
 * 6. 链尾（Chain Tail）：链的最后一个具体处理者。
 * 7. 请求（Request）：客户端向链头发送的请求。
 * 责任链模式的实现步骤：
 * 1. 定义一个抽象处理者接口。
 * 2. 定义多个具体处理者，实现抽象处理者接口。
 * 3. 客户端向链头的具体处理者发送请求。
 * 4. 具体处理者处理请求，如果不能处理，则将请求传给下一个具体处理者。
 * 5. 链尾的具体处理者处理请求。
 * 6. 链尾的具体处理者返回结果。
 * 7. 客户端接收结果。
 * 8. 重复步骤4-7，直到请求被处理。
 */

using LearnCSharp.DesignPattern.LearnChainOfResponsibilitySpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnChainOfResponsibility
    {
        /*【31801：责任链模式】*/
        public static void LearnChainOfResponsibilityDesignPattern()
        {
            Console.WriteLine("\n------示例：责任链模式------\n");
            Console.WriteLine("》》》通过责任链模式模拟日志记录功能《《《");
            Console.WriteLine("-----------------------------------------------");

            // 定义日志记录处理链式建造器
            var loggerChainsBuilder = new LogProcessorChainBuilder();

            // 定义日志记录处理链
            var loggerChain = loggerChainsBuilder.AddProcessor(new ConsoleLogProcessor())
                .AddProcessor(new FileLogProcessor())
                .AddProcessor(new DatabaseLogProcessor())
                .Build();

            // 模拟创建日志
            var logMessage = new LogMessage("系统启动");
            
            // 客户端发送请求
            loggerChain.Process(logMessage);

            // 输出日志处理链处理日志的过程
            Console.WriteLine("-");
            Console.WriteLine("责任链模式处理日志的过程：");
            Console.WriteLine(string.Join(" -> ", logMessage.Processors));

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
        
    }
}

namespace LearnCSharp.DesignPattern.LearnChainOfResponsibilitySpace
{
    #region 责任链模式结构

    /*【31800：责任链模式结构】*/
    #region 基础结构
    public abstract class Handler // 抽象处理者
    {
        protected Handler? next; // 下一个处理者

        public Handler SetNext(Handler? next) // 设置下一个处理者
        {
            this.next = next;
            return next;
        }

        public abstract void HandleRequest(int request); // 处理请求
    }

    public class ConcreteHandlerA : Handler // 具体处理者A
    {
        public override void HandleRequest(int request) // 处理请求
        {
            if (request <= 10)
            {
                Console.WriteLine($"请求{request}由HandlerA处理");
            }
            else
            {
                next?.HandleRequest(request);
            }
        }
    }

    public class ConcreteHandlerB : Handler // 具体处理者B
    {
        public override void HandleRequest(int request)
        {
            if (request <= 20)
            {
                Console.WriteLine($"请求{request}由HandlerA处理");
            }
            else
            {
                next?.HandleRequest(request);
            }
        }
    }

    /// <summary>
    /// 基础结构可选项：链式构造器
    /// </summary>
    public class HandlerBuilder
    {
        private Handler? first;
        private Handler? current;

        public HandlerBuilder AddHandler(Handler handler)
        {
            if (first == null)
            {
                first = handler;
                current = handler;
            }
            else
            {
                current!.SetNext(handler);
                current = handler;
            }
            return this;
        }

        public Handler Build()
        {
            return first;
        }
    }


    public class Client
    {
        public void TestChain()
        {
            //常规构建链
            var handlerChain = new ConcreteHandlerA().SetNext(new ConcreteHandlerB());

            handlerChain.HandleRequest(5);   //A处理
            handlerChain.HandleRequest(15);  //B处理
            handlerChain.HandleRequest(25);  //未被处理

            //链式构建链
            var builder = new HandlerBuilder().AddHandler(new ConcreteHandlerA()).AddHandler(new ConcreteHandlerB()).Build();

            builder.HandleRequest(5);   //A处理
            builder.HandleRequest(15);  //B处理
            builder.HandleRequest(25);  //未被处理
        }
    }
    #endregion

    #region 终止链结构 全链路处理
    public abstract class TerminateHandler
    {
        protected TerminateHandler? next; // 下一个处理者

        protected abstract bool CanHandle(string request); // 终止链判断

        public void SetNext(TerminateHandler? next) // 设置下一个处理者
        {
            this.next = next;
        }

        public void HandleRequest(string request) // 终止链处理
        {
            if (!CanHandle(request) && next != null)
            {
                next.HandleRequest(request); // 传递请求
            }
        }
    }

    public class CanHandleHandlerC : TerminateHandler
    {
        protected override bool CanHandle(string request)
        {
            Console.WriteLine($"请求{request}由CanHandleHandlerC处理");
            return false; // 永不终止处理链
        }
    }

    public class CanHandleHandlerD : TerminateHandler
    {
        protected override bool CanHandle(string request)
        {
            if (string.IsNullOrEmpty(request))
            {
                Console.WriteLine($"拒绝空请求{request}");
                return true; // 终止处理链
            }
            return false;
        }
    }

    public class ClientForTerminateHandler
    {
        public void TestTerminateChain()
        {
            var chain = new CanHandleHandlerC();
            chain.SetNext(new CanHandleHandlerD());

            chain.HandleRequest("请求1"); // CanHandleHandlerC处理
            chain.HandleRequest(""); // CanHandleHandlerD处理
            chain.HandleRequest("请求2"); // CanHandleHandlerC处理  
        }
    }
    #endregion
    #endregion

    #region 责任链模式应用
    /*【31801：责任链模式示例】*/
    /// <summary>
    /// 日志类
    /// </summary>
    public record LogMessage
    {
        public string Content { get; init; } // 日志内容

        public DateTime Timestamp { get; init; } // 日志时间

        public List<string> Processors { get; init; } // 处理日志的处理器

        public LogMessage(string content) // 构造函数
        {
            Content = content;
            Timestamp = DateTime.Now;
            Processors = new List<string>();
        }

        public void Deconstruct(out string content, out DateTime timestamp, out List<string> processors) // 解构函数
        {
            content = Content;
            timestamp = Timestamp;
            processors = Processors;
        }
    }

    /// <summary>
    /// 日志处理器抽象类
    /// </summary>
    public abstract class LogProcessor
    {
        private LogProcessor? next; // 下一个处理者

        protected abstract void Handle(LogMessage message); // 抽象方法，具体处理逻辑由子类实现

        public void SetNext(LogProcessor? next) // 设置下一个处理者（链式配置）
        {
            this.next = next;
        }

        public void Process(LogMessage message) // 处理日志的模板方法
        {
            Handle(message); // 执行当前处理器的具体操作

            next?.Process(message); // 强制传递日志到下一个处理者
        }
    }

    public class ConsoleLogProcessor : LogProcessor
    {
        protected override void Handle(LogMessage message)
        {
            Console.WriteLine($"控制台日志：[{message.Timestamp}] {message.Content}");
            message.Processors.Add(nameof(ConsoleLogProcessor));
        }
    }

    public class FileLogProcessor : LogProcessor
    {
        private readonly string filePath = "app.log";

        protected override void Handle(LogMessage message)
        {
            string cotent = $"文件日志：[{message.Timestamp}] {message.Content} | File={filePath}";
            Console.WriteLine(cotent);
            //File.AppendAllText(filePath, $"{cotent}\n");
            message.Processors.Add(nameof(FileLogProcessor));
        }
    }

    public class DatabaseLogProcessor : LogProcessor
    {
        private readonly string connectionString = "Data Source=app.db";
        
        protected override void Handle(LogMessage message)
        {
            string cotent = $"数据库日志：[{message.Timestamp}] {message.Content} | Database={connectionString}";
            Console.WriteLine(cotent);
            // 保存日志到数据库
            message.Processors.Add(nameof(DatabaseLogProcessor));
        }
    }

    public class LogProcessorChainBuilder
    {
        private LogProcessor? first;
        private LogProcessor? current;

        public LogProcessorChainBuilder AddProcessor(LogProcessor processor)
        {
            if (first == null)
            {
                first = processor;
                current = processor;
            }
            else
            {
                current!.SetNext(processor);
                current = processor;
            }
            return this;
        }

        public LogProcessor Build()
        {
            return first;
        }
    }
    #endregion

}