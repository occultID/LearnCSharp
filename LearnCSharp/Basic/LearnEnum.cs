/*【学习枚举】
 * 枚举类型简介
	• 枚举类型是由基础整型数值类型的一组命名常量定义的值类型
	• 若要定义枚举类型，需使用 enum 关键字并指定枚举成员名称

 * 枚举定义
	• 形式：
		○ 访问修饰符 enum 枚举类型名
			{
				枚举成员1,
				枚举成员2,
				…
				枚举成员n
			}
	• 默认情况下，枚举成员的关联常量值为int类型
	• 默认情况下，枚举成员的关联常量值从零开始，并按定义文本顺序递增1
		○ 如枚举成员1值为0，枚举成员2值为1
	• 可以显示指定任何其他整数数值类型作为枚举类型的基础类型
		○ 形式为：enum 枚举类型名: 整数数值类型
	• 可以显示指定成员关联的常数值
		○ 形式为：枚举成员1 = 整数值1，枚举成员2 = 整数值2
	• 枚举类型的定义内只能出现枚举成员，枚举成员就是一个关联了常数值的成员名称
	• 如果需要向枚举类型添加功能，需创建扩展方法

 * 枚举的访问
	• 枚举成员通过枚举类型本身来访问：枚举类型名.枚举成员

 * 枚举类型的默认值
	• 枚举类型E的默认值是由表达式(E)0生成的值，即使零没有相应的枚举成员也是如此

 * 作为位标志的枚举类型
	• 可以使用枚举类型表示选项组合
	• 如需表示选项组合，需满足以下条件：
		○ 将这些选项定义成枚举成员
		○ 使枚举成员成为 位字段，即枚举成员的关联值应该是2的幂
		○ 需要明确指示该枚举类型声明了位字段，即需要对其使用[Flags]属性
	• 使用 按位逻辑运算符 | 表示合并选项 或 使用按位逻辑运算符 & 表示交叉组合选项
	• 形式：
		○ [Flags]
			访问修饰符 enum 枚举类型名
			{
				枚举成员1 = 0b_0000_0000,
				枚举成员2 = 0b_0000_0001,
				枚举成员3 = 0b_0000_0010,
				…
				枚举成员9 = 0b_1000_0000,
				枚举成员10 = 枚举成员1 | 枚举成员3,
				…
			}

 * 关于枚举的类型转换
	• 对于任意枚举类型，都可以隐式转换成其父类型System.Enum
	• 对于任意枚举类型，枚举类型与其基础整型类型存在显示转换
		○ 如果将枚举类型转换为其基础类型，则结果为枚举成员的关联整数值
		○ 如果将整数值转换为枚举类型，则结果为关联该值的枚举成员或不转换(无关联值)
		○ 对于任意枚举类型，C#不支持不同枚举之间的转换
 */
using System.Runtime.CompilerServices;

namespace LearnCSharp.Basic
{
    //自定义一个枚举来表示性别
    public enum Gender
    {
        Unknown = 0,	//默认，表示未知
        Male = 1,		//男性，关联整数数值为1
        Female			//女性，关联整数数值为2
    }

    //自定义一个带标记的枚举来表示一个星期的每一天
	//该枚举关联的整数数值类型被设置为byte类型
    [Flags]
    public enum Week : byte
    {
        None = 0,						//0b_0000_0000
        Monday = 1 << 0,				//0b_0000_0001
        Tuesday = 1 << 1,				//0b_0000_0010
        Wednesday = 1 << 2,				//0b_0000_0100
        Thursday = 1 << 3,				//0b_0000_1000
        Friday = 1 << 4,				//0b_0001_0000
        Saturday = 1 << 5,				//0b_0010_0000
        Sunday = 1 << 6,                //0b_0100_0000
        Weekend = Saturday | Sunday		//0b_0110_0000
    }

    public static class LearnEnum
    {
		/*【10901：枚举代码示例】*/
        public static void StartLearnEnum()
        {
            Console.WriteLine("\n------示例：枚举------\n");
            //输出Gender枚举类型的定义
            string outputString = "访问Gender枚举的成员并使用显示转换输出关联整数值：\n" +
				$"Gender.Unknow --output:{Gender.Unknown} | 关联整数值 --output:{(int)Gender.Unknown}\n" +
				$"Gender.Male   --output:{Gender.Male}    | 关联整数值 --output:{(int)Gender.Male}\n" +
				$"Gender.Female --output:{Gender.Female}  | 关联整数值 --output:{(int)Gender.Female}\n";

			Console.WriteLine(outputString);
			Console.WriteLine();

            //输出Week枚举类型的定义
            outputString = "访问Week枚举的每一个项并输出结果，同时输出其对应整数值的二进制形式：\n" +
				$"Week.None --output:{Week.None}           | 关联整数值二进制形式 --output:{Week.None.ToBinaryString()}\n" +
				$"Week.Monday --output:{Week.Monday}       | 关联整数值二进制形式 --output:{Week.Monday.ToBinaryString()}\n" +
				$"Week.Tuesday --output:{Week.Tuesday}     | 关联整数值二进制形式 --output:{Week.Tuesday.ToBinaryString()}\n" +
				$"Week.Wednesday --output:{Week.Wednesday} | 关联整数值二进制形式 --output:{Week.Wednesday.ToBinaryString()}\n" +
				$"Week.Thursday --output:{Week.Thursday}   | 关联整数值二进制形式 --output:{Week.Thursday.ToBinaryString()}\n" +
				$"Week.Friday --output:{Week.Friday}       | 关联整数值二进制形式 --output:{Week.Friday.ToBinaryString()}\n" +
				$"Week.Saturday --output:{Week.Saturday}   | 关联整数值二进制形式 --output:{Week.Saturday.ToBinaryString()}\n" +
				$"Week.Sunday --output:{Week.Sunday}       | 关联整数值二进制形式 --output:{Week.Sunday.ToBinaryString()}\n" +
				$"Week.Weekend --output:{Week.Weekend}     | 关联整数值二进制形式 --output:{Week.Weekend.ToBinaryString()}\n";

			Console.WriteLine(outputString);

            //组合Week枚举成员表示一周中的工作日
            Week workDay = Week.Monday | Week.Tuesday | Week.Wednesday | Week.Thursday | Week.Friday;

			outputString = "将Week枚举的工作日项通过 | 运算符合并为一个整数值并输出结果，同时输出其对应整数值的二进制形式：\n";
            outputString += $"工作日 --output:{workDay}  | 合并选项的整数值二进制形式 --output:{workDay.ToBinaryString()}\n\n";

            //判断Monday是否是工作日
            bool isWorkDay = (Week.Monday & workDay) == Week.Monday;

			outputString += $"通过&运算符判断Manday是否工作日 --output:{isWorkDay}\n";

			Console.WriteLine(outputString);
        }

        /// <summary>
        /// 将枚举类型转换为二进制字符串
        /// </summary>
        /// <param name="week"></param>
        /// <returns></returns>
        public static string ToBinaryString(this Week week)
		{
			int size = 8;
			byte b = (byte)week;
			char[] bits = new char[size];

			byte mask = (byte)(1 << size - 1);

			for (int i = 0; i < size; i++)
			{
				bits[i] = ((mask & b) != 0) ? '1' : '0';
				mask >>= 1;
			}

			string bString = new(bits);
			return $"0b_{bString}";
		}
    }
}
