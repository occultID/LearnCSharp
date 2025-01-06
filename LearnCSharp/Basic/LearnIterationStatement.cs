/*【学习迭代语句】
    迭代语句用于重复执行语句或语句块
    迭代语句也称为循环语句
    迭代语句包括以下四种
        Foreach 语句
        For 语句
        Do-While 语句
        While 语句
    除了以上迭代语句，使用以下方案也能实现循环执行代码
        goto语句和带标签的语句结合使用
        递归方法
 */
namespace LearnCSharp.Basic
{
    internal static class LearnIterationStatement
    {        
        /*【学习foreach语句】
            foreach语句为类型实例中实现了IEnumerable或IEnumerable<T>接口的每个元素执行语句或语句块
            foreach语句并不限于这些类型。可以将其与满足以下条件的任何类型的实例一起使用
                类型具有公共无参数GetEnumerator方法
                    从C#9.0开始，GetEnumerator方法可以是类型的扩展方法
                GetEnumerator方法的返回类型具有公共Current属性和公共无参数MoveNext方法(其返回值为bool)
            foreach语句一般形式
                await foreach(ref 元素数据类型 元素变量 in 可以foreach的实例)
                {
                    处理元素变量的语句或语句块
                }
                注意：await是可选的，根据是否需要处理异步流决定是否使用
                注意：ref是可选的，根据枚举器的Curret属性返回值是否是ref引用来决定是否使用ref
                注意：元素数据类型可使用var隐式声明，编译器可以自动推导
        */
        private static uint Sum0ToMaxByForeach(uint max)
        {
            uint sum = 0;
            uint[] nums; //预置一个内部含0正整数数组，用于Foreach示例

            if (max == 0 || max == 1) return max;
            else
            {
                nums = new uint[max];
                for (uint i = 0; i < nums.Length; i++)
                {
                    nums[i] = i + 1;
                }
            }

            foreach (var num in nums)
            {
                sum += num;
            }
            return sum;
        }

        /*【学习for语句】
            在指定的布尔表达式的计算结果为true时，for语句会执行一条语句或一个语句块
            for语句的一般形式
                for(初始化表达式; 布尔表达式; 迭代器)
                {
                    语句或语句列表
                }
                初始化表达式部分仅在进入循环前执行一次，通常在该部分中声明并初始化一个局部循环变量
                    该循环变量在for语句外部无法访问，其作用域仅限for语句
                布尔表达式部分确定是否应执行循环中的下一个迭代，通常该部分使用循环迭代变量和一个表示结束条件的值进行比较
                    比较结果为true则继续下一个迭代，否则退出循环
                迭代器部分定义执行每一次循环语句后将执行的操作，通常是改变循环变量的值使其不断趋近于表示结束条件的值，直至能结束循环
         */
        private static uint Sum0ToMaxByFor(uint max)
        {
            uint sum = 0;
            for (uint i = 0; i <= max; i++)
            {
                sum += i;
            }
            return sum;
        }

        /*【学习do-while语句】
            当指定的布尔表达式的计算结果为true时，do语句会执行一条语句或一个语句块
            无论是否进入循环，Do语句块内的语句都会先于判断前执行一次
            do-while语句一般形式：
                do
                {
                    语句或语句列表
                }while(布尔表达式)
        */
        private static uint Sum0ToMaxByDoWhile(uint max)
        {
            uint i = 0;
            uint sum = 0;
            do
            {
                sum += i;
                i++;
            } while (i <= max);
            return sum;
        }

        /*【学习while语句】
            当指定的布尔表达式的计算结果为true时，do语句会执行一条语句或一个语句块
            该循环执行零次或多次，而do语句至少执行一次循环体
            while语句一般形式
                while(布尔表达式)
                {
                    语句或语句块
                }
        */
        private static uint Sum0ToMaxByWhile(uint max)
        {
            uint sum = 0;
            uint i = 0;
            while (i <= max)
            {
                sum += i;
                i++;
            }
            return sum;
        }

        /*【学习递归方法】
            递归方法实际是一种逆向思维的处理方式
            用一个方法循环调用自身直至满足递归结束条件而得出结果
         */
        private static uint Sum0ToMaxByRecursion(uint max)
        {
            if(max == 0) 
                return 0;//递归结束判断
            else 
                return max + Sum0ToMaxByRecursion(max - 1);
        }

        /*【使用goto和带标签的语句实现循环】
            一般情况下不建议滥用 goto语句，除非有必要
            滥用goto语句容易造成逻辑出错或代码混淆难懂
            这里指示作为示例展示了解
         */
        private static uint Sum0ToMaxByGoto(uint max)
        {
            uint sum = 0;
            uint i = 0;

            if (max == 0 || max == 1)
            {
                sum = max;
                goto end;
            }

            start: sum += i;
            i++;

            if (i <= max)
                goto start;
            else
                goto end;

            end: return sum;
        }



        //以给定的循环方式计算从零到给定的最大正整数的和
        public static uint Sum0ToMax(uint max, Loops loops)
        {
            switch (loops)
            {
                case Loops.Foreach:
                    return Sum0ToMaxByForeach(max);
                case Loops.For:
                    return Sum0ToMaxByFor(max);
                case Loops.DoWhile:
                    return Sum0ToMaxByDoWhile(max);
                case Loops.While:
                    return Sum0ToMaxByWhile(max);                 
                case Loops.Recursion:
                    return Sum0ToMaxByRecursion(max);
                case Loops.Goto:
                    return Sum0ToMaxByGoto(max);
                default:
                    return 0;
            }
        }

        //以给定的循环方式计算从零到给定的最大正整数的和-直接输出结果
        public static void OutputSum0ToMax(uint max, Loops loops)
        {
            Console.WriteLine("当前使用{0}循环计算[ 0 ]至[ {1} ]的和为：{2}", loops, max, Sum0ToMax(max, loops));
        }
        public static void StartLearnIterationStatement()
        {
            string title = "001 Foreach语句 循环\n" +
                "002 For语句 循环\n" +
                "003 Do-While语句 循环\n" +
                "004 While语句 循环\n" +
                "005 递归方法循环\n" +
                "006 goto方式的循环\n";

            do
            {
                Console.WriteLine("【学习循环语句(或方法)】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行：");

                string? input = Console.ReadLine();

                Console.Write("计算0到给定正整数的值，请输入一个正整数：");
                
                if(uint.TryParse(Console.ReadLine(),out uint max))
                {
                    Console.WriteLine();
                    switch (input)
                    {
                        case "001": OutputSum0ToMax(max, Loops.Foreach); break;
                        case "002": OutputSum0ToMax(max, Loops.For); break;
                        case "003": OutputSum0ToMax(max, Loops.DoWhile); break;
                        case "004": OutputSum0ToMax(max, Loops.While); break;
                        case "005": OutputSum0ToMax(max, Loops.Recursion); break;
                        case "006": OutputSum0ToMax(max, Loops.Goto); break;
                        default: Console.WriteLine("输入错误！"); break;
                    }
                }
                else
                    Console.WriteLine("请输入一个整数！");

                Console.WriteLine();
                Console.WriteLine("是否继续查询和运行本章节其他代码：直接按下Enter继续，否则即退出");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;
                Console.WriteLine("\n");
            }
            while (true);
        }
    }

    internal enum Loops : byte
    {
        While = 0,      //While循环
        DoWhile = 1,    //Do-While循环
        For = 2,        //For循环
        Foreach = 3,    //Foreach循环
        Recursion = 4,  //递归循环
        Goto = 5        //goto循环
    }
}
