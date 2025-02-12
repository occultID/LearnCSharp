using LearnCSharp.Basic.LearnPatternSpace;

namespace LearnCSharp.Basic
{
    internal class LearnPattern
    {
        public static void StartLearnPattern()
        {
        }

        /*【12601：常量模式】 原本C#6.0之前用法
        //switch语句在C# 6.0极其之前的版本，匹配表达式只支持字符串、字符、整型、bool、枚举几个类型
        */
        public static void LearnConstantPattern()
        {
            while (true)
            {
                Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    switch (number)
                    {
                        //下列注释写法会报错，case语句必须显示跳出，不能贯穿
                        //case 0:
                        //    Console.ForegroundColor = (ConsoleColor)number;
                        //case 1:
                        //    Console.ForegroundColor = (ConsoleColor)number;
                        //下列未注释语句不会出问题，因为C#允许组和多个case为一组知道最后一个标签后跟随执行代码和显示跳出
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                            Console.ForegroundColor = (ConsoleColor)number;
                            break;
                        case 16:
                            goto default;//匹配到该标签可以使用goto显示跳转到default，但注意不要随意使用goto，以及避免造成循环，比如goto case 16
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("暂无该颜色编码，已重置为灰色。");
                            break;
                    }
                    break;
                }
            }
            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /*【12602：关系模式】 匹配标签可以为用于匹配变量和某个常量值的比较表达式
         */
        public static void LearnRelationalPattern()
        {
            while (true)
            {
                Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    switch (number)
                    {
                        case < 0:
                            goto default;
                        case <= 15:
                            Console.ForegroundColor = (ConsoleColor)number;
                            break;
                        case > 15:
                            goto default;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("暂无该颜色编码，已重置为灰色。");
                            break;
                    }
                    break;
                }
            }
            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /*【12603：逻辑模式】 在关系模式基础上使用not、and、or关键字进行多条件判断*/
        public static void LearnLogicalPattern()
        {
            while (true)
            {
                Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    switch (number)
                    {
                        case > 0 and <= 15:
                            Console.ForegroundColor = (ConsoleColor)number;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("暂无该颜色编码，已重置为灰色。");
                            break;
                    }
                    break;
                }
            }
            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /*【12604：声明模式】 通过case标签声明变量，如果匹配变量能转换为该类型变量，则将匹配变量的值赋予该声明变量*/
        public static void LearnDeclarationPattern()
        {
            object[] objects = { 5, 6.0F, 3.14D, 500L, "你好" };

            foreach (var item in objects)
            {
                switch (item)
                {
                    case int i when i > 0: //声明模式下可以对声明变量使用 when 关键字继续进行判断
                        Console.WriteLine($"这是一个int类型且为正整数，值为：{i}");
                        break;
                    case float f:
                        Console.WriteLine($"这是一个float类型，值为：{f}");
                        break;
                    case double d:
                        Console.WriteLine($"这是一个double类型，值为：{d}");
                        break;
                    case long l:
                        Console.WriteLine($"这是一个long类型，值为：{l}");
                        break;
                    case string s:
                        Console.WriteLine($"这是一个string类型，值为：{s}");
                        break;
                    default:
                        Console.WriteLine($"这是一个object");
                        break;
                }
            }
        }

        /*【12605：类型模式】 类型模式可以理解为在声明模式中使用了弃元，只判断类型，放弃取值*/
        public static void LearnTypePattern()
        {
            object[] objects = { 5, 6.0F, 3.14D, 500L, "你好" };

            foreach (var item in objects)
            {
                switch (item)
                {
                    case int:
                        Console.WriteLine($"这是一个int类型");
                        break;
                    case float:
                        Console.WriteLine($"这是一个float类型");
                        break;
                    case double:
                        Console.WriteLine($"这是一个double类型");
                        break;
                    case long:
                        Console.WriteLine($"这是一个long类型");
                        break;
                    case string:
                        Console.WriteLine($"这是一个string类型");
                        break;
                    default:
                        Console.WriteLine($"这是一个object");
                        break;
                }
            }
        }

        /*【12606：属性模式】 匹配变量是一个对象实例 case标签用于对实例的属性进行匹配*/
        public static void LearnPropertyPattern()
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                double x = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                double y = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                points.Add(new Point(x, y));
            }

            foreach (var point in points)
            {
                switch (point)
                {
                    case { X: > 0, Y: > 0 }:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第一象限");
                        break;
                    case { X: < 0, Y: > 0 }:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第二象限");
                        break;
                    case { X: < 0, Y: < 0 }:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第三象限");
                        break;
                    case { X: > 0, Y: < 0 }:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第四象限");
                        break;
                    case { X: 0, Y: not 0 }:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于Y轴");
                        break;
                    case { X: not 0, Y: 0 }:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于X轴");
                        break;
                    default:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于原点");
                        break;
                }
            }
        }

        /*【12607：位置模式】 位置模式采用解构的特性来说明指定的模式是否匹配*/
        public static void LearnPositionalPattern()
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                double x = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                double y = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                points.Add(new Point(x, y));
            }

            foreach (var point in points)
            {
                switch (point)
                {
                    case ( > 0, > 0):
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第一象限");
                        break;
                    case ( < 0, > 0):
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第二象限");
                        break;
                    case ( < 0, < 0):
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第三象限");
                        break;
                    case ( > 0, < 0):
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于第四象限");
                        break;
                    case (0, not 0):
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于Y轴");
                        break;
                    case (not 0, 0):
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于X轴");
                        break;
                    default:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于原点");
                        break;
                }
            }
        }

        /*【12608：var模式】 var模式往往和属性模式和位置模式结合,用于提取属性变量*/
        public static void LearnVarPattern()
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                double x = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                double y = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                points.Add(new Point(x, y));
            }

            foreach (var point in points)
            {
                switch (point)
                {
                    case { X: var x, Y: var y } when x > 0 && y > 0:
                        Console.WriteLine($"坐标（{x}，{y}）位于第一象限");
                        break;
                    case { X: var x, Y: var y } when x < 0 && y > 0:
                        Console.WriteLine($"坐标（{x}，{y}）位于第二象限");
                        break;
                    case { X: var x, Y: var y } when x < 0 && y < 0:
                        Console.WriteLine($"坐标（{x}，{y}）位于第三象限");
                        break;
                    case { X: var x, Y: var y } when x > 0 && y < 0:
                        Console.WriteLine($"坐标（{x}，{y}）位于第四象限");
                        break;
                    case { X: var x, Y: var y } when x == 0 && y != 0:
                        Console.WriteLine($"坐标（{x}，{y}）位于Y轴");
                        break;
                    case { X: var x, Y: var y } when x != 0 && y == 0:
                        Console.WriteLine($"坐标（{x}，{y}）位于X轴");
                        break;
                    default:
                        Console.WriteLine($"坐标（{point.X}，{point.Y}）位于原点");
                        break;
                }
            }
        }

        /*【12609：弃元模式】 弃元模式在switch语句中不常用,一般多用于switch表达式
        //                    弃元,即以_表示放弃一个值,本例为基于声明模式但放弃取值,相当于类型模式
        */
        public static void LearnDiscardPattern()
        {
            List<Point> points = new List<Point>();

            for (int i = 0; i < 10; i++)
            {
                double x = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                double y = Random.Shared.Next(-999, 1000) + Random.Shared.NextDouble();
                points.Add(new Point(x, y));
            }

            foreach (var point in points)
            {
                switch (point)
                {
                    case (double x, double y) when x == 0 || y == 0:
                        Console.WriteLine($"坐标（{x}，{y}）位于坐标轴上");
                        break;
                    case Point _:
                        Console.WriteLine($"坐标不在坐标轴上");
                        break;
                }
            }
        }

        /*【12610：带括号模式】 可在任何模式两边加上括号*/
        public static void LearnParenthesizedPattern()
        {

        }

        /*【12611：列表模式】 可以将数组或列表与模式的序列进行匹配*/
        public static void LearnListPattern()
        {

        }

        /*【12612切片模式】 匹配零个或多个元素。
        //                  最多可在列表模式中使用一个切片模式。
        //                  切片模式只能显示在列表模式中。
        */
        public static void LearnSlicePattern()
        {

        }

        /* 学习Switch表达式：
           C# 7.0同时引入了switch表达式，相对switch语句来说，形式有一定改变，但功能基本一致，但其可以为switch返回一个值。
           switch语句的匹配特性对switch表达式完全适用。

         * switch表达式形式：
                数据类型 变量 = 匹配变量 switch
                {
                    匹配标签 => 能返回对应数据类型值的表达式, //注意，这里是一系列表达式块，每个表达式使用 , 分隔
                    匹配标签 => 能返回对应数据类型值的表达式, 
                    _ => 弃元需执行的表达式  // _相当于default
                }; 
                //注意：switch表达式是一个表达式，不能单独使用，只能用于语句，作为语句最后一部分，最后需要 ; 来结束语句
         
         * 注意：
                当匹配变量满足=>左边的“匹配标签”时，=>右边的表达式会为switch表达式返回指定值。
         */
        public static void LearnSwitcExpression()
        {
            DateTime dateTime = DateTime.Now;

            //使用switch表达式获取当前日期是属于上旬、中旬下旬
            string result = dateTime switch
            {
                { Day: < 11 } => "今天属于上旬",                       //属性模式
                DateTime date when date.Day < 21 => "今天属于中旬",    //声明模式
                _ => "今天属于下旬"                                    //弃元模式
            };

            Console.WriteLine(result);
        }
    }
}

namespace LearnCSharp.Basic.LearnPatternSpace
{
    internal readonly struct Point
    {
        public double X { get; }
        public double Y { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public void Deconstruct(out double x, out double y)
        {
            x = X;
            y = Y;
        }
    }
}
