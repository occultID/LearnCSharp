﻿/*【学习选择语句】
 * 选择语句分为if-else语句和switch语句
 * 选择语句根据表达式的值从许多可能的路径中选择要执行的语句
 
 * if-else语句根据布尔表达式的值选择要执行的语句
 * switch语句根据与表达式的模式匹配选择要执行的语句列表
 */
namespace LearnCSharp.Basic
{
    internal class LearnSelectionStatement
    {
        /*【学习if-else语句】
            if语句基于布尔表达式的值来选择要执行的语句
            if-else语句是用于进行简单的条件判断语句，可以嵌套使用和连续使用
            如果仅作简单判断，else语句可省略
           
         *  if语句的形式：
                if(返回bool值的表达式)
                {
                    判断为true时要执行的语句(块)
                 }
                else if(额外用来判断的条件)
                {
                    继续判断为true时要执行的语句
                 }
                else
                {
                    判断为false时要执行的语句(块)
                 }
            当进行判断后如果接下来仅执行一句语句，{}可以省略
            仅作一次判断，可以省略else if和其代码块，如要连续进行条件判断，可在if和else中间不断使用else if{}
            if语句也可以如下进行嵌套使用：
                if(...)
                {
                    if(...)
                    {
                        ......
                     }
                 }
         */
        public static void LearnIfStatement()
        {
            Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");
            //先进行一次判断输入数据是否是整数，如果是，则执行if内部代码块，否则执行匹配的else内代码块
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                if (number >= 0 && number <= 15)
                {
                    Console.ForegroundColor = (ConsoleColor)number;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("暂无该颜色编码，已重置为灰色。");
                }
            }
            else
                Console.WriteLine("请输入一个整数数字！");

            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /* 【学习switch语句】
             switch语句形式：
                switch(匹配变量)
                {
                    case 匹配标签:
                        匹配成功执行的语句或语句块；
                        这里的语句块应省略大括号{}，因为case和break这两个关键字本身就指示了块的开始和结束
                        break/goto 匹配标签/return/throw等语句结束以显示退出switch语句
                    case 匹配标签:
                        匹配成功执行的语句或语句块；
                        break/goto 匹配标签/return/throw等语句结束以显示退出switch语句
                    ...任意个case代码块
                    default:
                        默认匹配成功后需执行的语句块；
                        break/goto 匹配标签/return/throw等语句结束以显示退出switch语句
                }
             在C#中switch语句各个case语句不允许贯穿，每个case语句执行完成后必须显示退出
             在C#中switch语句允许多个case语句组，即case匹配标签后面不写任何代码，即可和下一case语句共同执行其匹配的语句段
             在C#6.0极其之前，switch语句作用很有限，常被做为是if-else语句的替代品
             C#7.0极其之后，switch的限制得到放宽，每个case标签不在只能是一个常量，而是一个模式
             C#7.0为switch语句引入了模式匹配，switch语句可以使用任何数据类型。
         */

        //示例一 常量模式 原本C#6.0之前用法
        //switch语句在C# 6.0极其之前的版本，匹配表达式只支持字符串、字符、整型、bool、枚举几个类型
        public static void LearnSwitchStatement()
        {
            Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");
            
            if(int.TryParse(Console.ReadLine(), out int number))
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
            }
            Console.WriteLine("测试颜色：当前字符串颜色为：{0}", Console.ForegroundColor.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 示例二 关系模式 匹配标签可以为用于匹配变量和某个常量值的比较表达式
        public static void LearnNewSwitchStatement2()
        {
            Console.Write("请输入0~15的任意整数数字来改变控制台文字颜色：");

            if (int.TryParse(Console.ReadLine(), out int number))
            {
                switch (number)
                {
                    case <0:
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
                    case >0 and <= 15:
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
                    case int i when i >0: //声明模式下可以对声明变量使用 when 关键字继续进行判断
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

            switch(dateTime) 
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
            (int X,int Y)[] coordinates = { (1, 2), (3, -4), (-1, 7), (0,0)};
 
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
            DateTime dateTime= DateTime.Now;

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
                { Day: <11 } => "今天属于上旬",                        //属性模式
                DateTime date when date.Day < 21 => "今天属于中旬",    //声明模式
                _ => "今天属于下旬"                                    //弃元模式
            } ;

            Console.WriteLine(result);
        }





        public static void StartLearnSelectionStatement()
        {
            string title = "001 if语句\n" +
                "002 基础switch语句-常量模式\n" +
                "003 switch语句-匹配模式-关系模式\n" +
                "004 switch语句-匹配模式-逻辑模式\n" +
                "005 switch语句-匹配模式-声明模式\n" +
                "006 switch语句-匹配模式-类型模式\n" +
                "007 switch语句-匹配模式-属性模式\n" +
                "008 switch语句-匹配模式-位置模式\n" +
                "009 switch语句-匹配模式-Var模式\n" +
                "010 switch语句-匹配模式-弃元模式\n" +
                "011 switch表达式";

            do
            {
                Console.WriteLine("【学习选择语句】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnIfStatement(); break;
                    case "002": LearnSwitchStatement(); break;
                    case "003": LearnNewSwitchStatement2(); break;
                    case "004": LearnNewSwitchStatement3(); break;
                    case "005": LearnNewSwitchStatement4(); break;
                    case "006": LearnNewSwitchStatement5(); break;
                    case "007": LearnNewSwitchStatement6(); break;
                    case "008": LearnNewSwitchStatement7(); break;
                    case "009": LearnNewSwitchStatement8(); break;
                    case "010": LearnNewSwitchStatement9(); break;
                    case "011": LearnSwitcExpression(); break;
                    default: Console.WriteLine("输入错误！"); break;
                }

                Console.WriteLine();
                Console.WriteLine("是否继续查询和运行本章节其他代码：直接按下Enter继续，否则即退出");

                if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                    break;
                Console.WriteLine("\n");
            }
            while (true);
        }
    }
}
