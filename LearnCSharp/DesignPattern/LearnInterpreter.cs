/*【：行为型————解释器模式】
 * 解释器模式（Interpreter Pattern）是一种设计模式，用于定义一种语言的文法，并提供一个解释器来处理该语言的句子。
 * 解释器模式通常用于需要解析和执行特定语言或表达式的场景，例如编程语言、查询语言等。
 * 解释器模式的主要组成部分包括：
 *  1. 抽象表达式（Abstract Expression）：定义了一个解释操作的接口。
 *  2. 终结符表达式（Terminal Expression）：实现了抽象表达式接口，表示文法中的终结符。
 *  3. 非终结符表达式（Non-terminal Expression）：实现了抽象表达式接口，表示文法中的非终结符。
 *  4. 上下文（Context）：包含解释器所需的信息，例如变量值等。
 *  5. 客户端（Client）：使用解释器来解释和执行句子。
 *  6. 语法树（Syntax Tree）：表示文法的结构，通常由非终结符和终结符组成。
 *  7. 解释器（Interpreter）：使用语法树来解释和执行句子。
 *  8. 语法分析器（Parser）：将输入的句子解析为语法树。
 *  9. 语法规则（Grammar）：定义了语言的结构和规则。
 *  10. 语法分析（Parsing）：将输入的句子转换为语法树的过程。
 */

using LearnCSharp.DesignPattern.LearnInterpreterSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnInterpreter
    {
        /*【32201：解释器模式】*/
        public static void LearnInterpreterDesignPattern()
        {
            Console.WriteLine("\n------示例：解释器模式------\n");
            Console.WriteLine("》》》通过解释器模式模拟加法代码的解释与执行《《《");
            Console.WriteLine("-----------------------------------------------");

            // 构建表达式
            var context = new CalculatorContext();
            context.SetVariable("a", 10);
            context.SetVariable("b", 20);

            var addExpression1 = new AdditionExpression(new VariableExpression("b"), new NumberExpression(-15));
            var addExpression2 = new AdditionExpression(new VariableExpression("a"), addExpression1);

            // 解释表达式
            var result = addExpression2.Interpret(context);
            Console.WriteLine("结果：" + result);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnInterpreterSpace
{
    #region 解释器模式基础结构
    /*【32200：解释器模式基础结构】*/
    public class Context //上下文环境：包含解释器所需的全局信息
    {
        private readonly Dictionary<string, object> symbols = new Dictionary<string, object>(); //使用字典存储符号表/变量表

        public void SetSymbol(string name, object value)=> symbols[name] = value; //设置符号表/变量表

        public object? GetSymbol(string name) => symbols.TryGetValue(name, out object? value)? value : null; //获取符号表/变量表
    }

    public interface IExpression //抽象表达式：定义解释操作的接口
    {
        object? Interpret(Context context); //解释操作
    }

    public class TerminalExpression : IExpression //终结符表达式：实现抽象表达式接口，表示文法中的终结符——基础不可再分的表达式
    {
        private readonly string symbolKey;
        
        public TerminalExpression(string symbolKey) => this.symbolKey = symbolKey; //构造函数，传入符号名称

        public object? Interpret(Context context) => context.GetSymbol(symbolKey); //解释操作，从符号表/变量表中获取符号的值
    }

    public class NonTerminalExpression : IExpression //非终结符表达式：实现抽象表达式接口，表示文法中的非终结符——由其他表达式组合而成的表达式
    {
        private readonly IExpression left;
        private readonly IExpression right;

        public NonTerminalExpression(IExpression left, IExpression right) => (this.left, this.right) = (left, right); //构造函数，传入左右子表达式

        public object? Interpret(Context context) //解释操作，由右子表达式的解释结果决定
        {
            // 示例组合逻辑，获取左右子表达式的解释结果，并进行运算
            // 实际应用中，可能需要根据具体业务逻辑进行修改
            var leftValue = Convert.ToInt32(left.Interpret(context));
            var rightValue = Convert.ToInt32(right.Interpret(context));
            return leftValue + rightValue;
        }
    }
    #endregion

    #region 解释器模式示例
    /*【32201：解释器模式示例】*/
    public class CalculatorContext
    {
        private readonly Dictionary<string, int> variables = new Dictionary<string, int>(); //变量表

        public void SetVariable(string name, int value) => variables[name] = value; //设置变量

        public int GetValue(string name) => variables.TryGetValue(name, out int value)? value : 0; //获取变量
    }

    public interface ICalculatorExpression //定义计算表达式接口
    {
        int Interpret(CalculatorContext context); //解释操作，返回计算结果
    }

    public class VariableExpression : ICalculatorExpression //变量表达式：实现计算表达式接口，表示变量
    {
        private readonly string variableName;

        public VariableExpression(string variableName) => this.variableName = variableName; //构造函数，传入变量名称

        public int Interpret(CalculatorContext context) => context.GetValue(variableName); //解释操作，从变量表中获取变量的值
    }

    public class NumberExpression : ICalculatorExpression //数字表达式：实现计算表达式接口，表示数字
    {
        private readonly int number;

        public NumberExpression(int number) => this.number = number; //构造函数，传入数字值

        public int Interpret(CalculatorContext context) => number; //解释操作，直接返回数字值
    }

    public class AdditionExpression : ICalculatorExpression //加法表达式：实现计算表达式接口，表示加法运算
    {
        private readonly ICalculatorExpression left;
        private readonly ICalculatorExpression right;

        public AdditionExpression(ICalculatorExpression left, ICalculatorExpression right) => (this.left, this.right) = (left, right); //构造函数，传入左右子表达式

        public int Interpret(CalculatorContext context) => left.Interpret(context) + right.Interpret(context); //解释操作，由左右子表达式的解释结果决定
    }
    #endregion
}