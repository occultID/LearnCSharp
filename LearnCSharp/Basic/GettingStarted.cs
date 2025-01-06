using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Basic
{
    /// <summary>
    /// 初识C#，这是一个xml注释
    /// 这个类用于初步认识C#的基础知识
    /// </summary>
    internal class GettingStarted
    {
        //这是一个单行注释，用于说明下面的方法是一个私有静态字段
        private static string skill = "数学计算";

        //这是一个单行注释，用于说明下面的方法是一个公共静态属性，用于包装私有静态字段
        public static string Skill
        {
            get { return skill; }
            set { skill = value; }
        }

        /* 这是一个多行注释
         * 用于说明下面的方法是一个公共静态方法
         * 这个方法不含参数，也没有返回值
         */
        public static void GettingStartedWithCSharp() 
        {
            Console.WriteLine($"一起来学习{Skill}吧~~\n");

            Console.WriteLine("------使用两种不同的方法来计算从1到100所有整数的和------");
            Console.WriteLine($"For循环计算：{SumFrom1To100()}");
            Console.WriteLine($"递归循环计算：{SumFrom1To100(100)}");
        }

        /// <summary>
        /// 使用for循环计算从1到100所有整数的和
        /// </summary>
        /// <returns></returns>
        public static int SumFrom1To100()
        {
            int sum = 0;
            for (int i = 1; i <= 100; i++)
            {
                sum += i;
            }
            return sum;
        }

        /// <summary>
        /// 使用递归计算从1到100所有整数的和
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int SumFrom1To100(int x = 100)
        {
            if(x==1)
            {
                return 1;
            }
            return x + SumFrom1To100(x - 1);
        }
    }
}
