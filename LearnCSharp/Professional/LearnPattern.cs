using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Professional
{
    internal class LearnPattern
    {
        public static void StartLearnPattern()
        {
            // 1. 常量模式
            // 2. 类型模式
            // 3. 变量模式
            // 4. 递归模式
            // 5. 构造模式
            // 6. 属性模式
            // 7. 元组模式
            // 8. 位置模式
            // 9. 非空模式
            // 10. 非常量模式
            // 11. or模式
            // 12. and模式
            // 13. not模式
            // 14. var模式
            // 15. switch表达式
            // 16. switch语句
            // 17. switch模式
            // 18. switch模式的when子句
            // 19. switch模式的递归模式
            // 20. switch模式的构造模式
            // 21. switch模式的属性模式
            // 22. switch模式的元组模式
            // 23. switch模式的位置模式
            // 24. switch模式的非空模式
            // 25. switch模式的非常量模式
            // 26. switch模式的or模式
            // 27. switch模式的and模式
            // 28. switch模式的not模式
            // 29. switch模式的var模式
            // 30. switch模式的switch表达式
            // 31. switch模式的switch语句
            // 32. switch模式的switch模式
            // 33. switch模式的switch模式的when子句
            // 34. switch模式的switch模式的递归模式
            // 35. switch模式的switch模式的构造模式
            // 36. switch模式的switch模式的属性模式
            // 37. switch模式的switch模式的元组模式
            // 38. switch模式的switch模式的位置模式
            //
        }



        // 示例二 关系模式 匹配标签可以为用于匹配变量和某个常量值的比较表达式
        public static void LearnNewSwitchStatement2()
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
            }
            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 示例三 逻辑模式 在关系模式基础上使用not、and、or关键字进行多条件判断
        public static void LearnNewSwitchStatement3()
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
            }
            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 示例四 声明模式 通过case标签声明变量，如果匹配变量能转换为该类型变量，则将匹配变量的值赋予该声明变量
        public static void LearnNewSwitchStatement4()
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

        // 示例五 类型模式 类型模式可以理解为在声明模式中使用了弃元，只判断类型，放弃取值
        public static void LearnNewSwitchStatement5()
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

        // 示例六 属性模式 匹配变量是一个对象实例 case标签用于对实例的属性进行匹配
        public static void LearnNewSwitchStatement6()
        {
            DateTime dateTime = DateTime.Now;

            switch (dateTime)
            {
                case { Year: 2022 or 2021, Month: <= 6 }:
                    Console.WriteLine("这是2021或者2022年的上半年");
                    break;
                case { Year: not 2022 }:
                    Console.WriteLine("这不是2022年");
                    break;
                case { DayOfWeek: not DayOfWeek.Saturday and not DayOfWeek.Sunday }:
                    Console.WriteLine("这是工作日");
                    break;
                default:
                    Console.WriteLine(DateTime.Now);
                    break;
            }
        }

        // 实例七 位置模式 位置模式采用解构的特性来说明指定的模式是否匹配
        public static void LearnNewSwitchStatement7()
        {
            (int X, int Y)[] coordinates = { (1, 2), (3, -4), (-1, 7), (0, 0) };

            foreach (var item in coordinates)
            {
                switch (item)
                {
                    case ( > 0, > 0):
                        Console.WriteLine("坐标({0},{1})位于第一象限", item.X, item.Y);
                        break;
                    case ( < 0, > 0):
                        Console.WriteLine("坐标({0},{1})位于第二象限", item.X, item.Y);
                        break;
                    case ( < 0, < 0):
                        Console.WriteLine("坐标({0},{1})位于第三象限", item.X, item.Y);
                        break;
                    case ( > 0, < 0):
                        Console.WriteLine("坐标({0},{1})位于第四象限", item.X, item.Y);
                        break;
                    case (0, not 0):
                        Console.WriteLine("坐标({0},{1})位于Y轴", item.X, item.Y);
                        break;
                    case (not 0, 0):
                        Console.WriteLine("坐标({0},{1})位于X轴", item.X, item.Y);
                        break;
                    default:
                        Console.WriteLine("坐标({0},{1})位于坐标原点", item.X, item.Y);
                        break;
                }
            }
        }

        // 实例八 var模式 var模式往往和属性模式和位置模式结合,用于提取属性变量
        public static void LearnNewSwitchStatement8()
        {
            DateTime dateTime = DateTime.Now;

            switch (dateTime)
            {
                case DateTime { Year: var year, Month: var month, Day: var day }:
                    Console.WriteLine("今天是{0}年{1}月{2}日", year, month, day);
                    break;
                default:
                    Console.WriteLine("NUll, 啥也没有");
                    break;
            }
        }

        // 实例九 弃元模式 弃元模式在switch语句中不常用,一般多用于switch表达式
        //                 弃元,即以_表示放弃一个值,本例为基于声明模式但放弃取值,相当于类型模式
        public static void LearnNewSwitchStatement9()
        {
            object dateTime = DateTime.Now;

            switch (dateTime)
            {
                case DateTime _:
                    Console.WriteLine("这是一个时间");
                    break;
                case string _:
                    Console.WriteLine("这是一个字符串");
                    break;
                case double _:
                    Console.WriteLine("这是一个double型数字");
                    break;
                case int _:
                    Console.WriteLine("这是一个int型数字");
                    break;

            }
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
                以下示例会用到Lambda表达式，现在不理解可以先不管，后续会专门学习Lambda表达式。
                现在只用明白，当匹配变量满足=>左边的“匹配标签”时，=>右边的表达式会为switch表达式返回指定值。
         */
        public static void LearnSwitcExpression()
        {
            DateTime dateTime = DateTime.Now;

            //使用switch表达式获取当前日期是属于上旬、中旬下旬
            string result = dateTime switch
            {
                { Day: < 11 } => "今天属于上旬",                        //属性模式
                DateTime date when date.Day < 21 => "今天属于中旬",    //声明模式
                _ => "今天属于下旬"                                    //弃元模式
            };

            Console.WriteLine(result);
        }
    }
}
