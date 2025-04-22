/*【114：学习扩展方法】
 * 扩展方法
	• 扩展方法使开发者能够向现有类型“添加”方法，而无需创建新的派生类型、重新编译或以其他方式修改原始类型
	• 扩展方法的声明与定义
		○ 扩展方法必须定义在一个静态类内部
			§ 按规范这个类的名字应是被扩展类类名加一个Extension后缀
			§ 这个类应该是一个公开类，即使用public关键字进行修饰
		○ 扩展方法必须是一个静态方法
			§ 扩展方法的访问级别应该是公开的，即public
			§ 扩展方法的返回类型不做强制要求
			§ 扩展方法的参数列表第一个参数必须是this关键字修饰的参数，其类型必须是被扩展类型
				□ this参数不能使用 out、ref 和 in 关键字进行修饰
				□ 特例：当被扩展类型属于结构时，可根据需要同时使用 in 关键字进行修饰
			§ 扩展方法也可以有多个参数，但都必须跟在this参数之后
			§ 对于扩展方法，this参数有且只能有一个
			§ 针对同一类型的扩展方法组按规范应集中放置到同一个静态类中
		○ 扩展方法定义的完整形式
			§ public static class 被扩展类型名Extension
			{
				public static 返回类型 扩展方法名(this 被扩展类型 变量名)
				{
					语句块
				}
			}
	• 扩展方法的调用
		○ 扩展方法可用的前提是将扩展方法放置到需要使用的程序集中并在需要的位置使用using指令引入其命名空间
		○ 通过定义扩展方法的类来进行常规静态方法的形式调用
			§ 形式：被扩展类型名Extension.扩展方法名()
		○ 通过被扩展类型的实例来如实例方法一般调用
			§ 扩展方法是一种静态方法，但可以向扩展类型上的实例方法一样进行调用
			§ 扩展方法的调用形式是一种“语法糖”
			§ 形式：被扩展类型实例名.扩展方法名()
	• 扩展方法注意事项
		○ 慎用扩展方法，比如不要为object类型定义扩展方法
		○ 避免随意定义控制方法，尤其是不要为自己无所有权的类型定义，除非实际需求需要
		○ 如果为某个类型扩展了某个方法，但在未来该类型实现了一个同名方法，则扩展方法会被覆盖和忽略
		○ 扩展方法可以配合接口来更好的发挥作用
 */

namespace LearnCSharp.Basic
{
	/*【扩展方法示例】
	 * 定义一个名为Int32Extension的静态类
	 * 该类用于放置为System.Int32类型扩展的方法
	 */
    public static class Int32Extension
    {
        /// <summary>
        /// 将整数转换为二进制字符串
        /// </summary>
        /// <param name="integer">this参数的类型即为需要进行扩展的System.Int32类型</param>
        /// <returns></returns>
        public static string ToBinaryString(this int integer)
        {
            char[] intBits = new char[39];
            int mask = 1 << 31;

            for (int i = 0; i < 39; i++)
            {
				if ((i + 1) % 5 == 0)
					intBits[i] = '_';
				else
				{
					intBits[i] = ((mask & integer) != 0) ? '1' : '0';
					mask >>>= 1;
				}
			}
            return $"0b_{new string(intBits)}";
        }
    }

    internal class LearnExtensionMethod
    {
		/*【11401：扩展方法示例】*/
		public static void StartLearnExtensionMethod()
		{
            Console.WriteLine("\n------示例：扩展方法------\n");

			start: Console.Write("请输入一个整数：");

            if (int.TryParse(Console.ReadLine(), out int integer))
                goto end;
            else
                goto start;

			end: string result = $"使用扩展方法输出整数{integer}的二进制形式：{integer.ToBinaryString()}";

			Console.WriteLine(result);
			Console.WriteLine();
        }
    }
}
