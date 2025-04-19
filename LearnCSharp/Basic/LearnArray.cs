/*【108：学习并了解数组类型】
 * 数组简介
	• 可以将同一类型的多个变量存储在一个数组数据结构中。
	• 通过指定数组的元素类型来声明数组。 
	• 如果希望数组存储任意类型的元素，可将其类型指定为 object。 在 C# 的统一类型系统中，所有类型（预定义类型、用户定义类型、引用类型和值类型）都是直接或间接从 Object 继承的。

 * 数组概述
	• 数组具有以下属性：
		○ 数组可以是一维、多维或交错的。
		○ 创建数组实例时，将建立维度数量和每个维度的长度。 这些值在实例的生存期内无法更改。
		○ 数值数组元素的默认值设置为零，而引用元素设置为 null。
		○ 交错数组是数组的数组，因此其元素为引用类型且被初始化为 null。
		○ 数组从零开始编制索引：包含 n 元素的数组从 0 索引到 n-1。
		○ 数组元素可以是任何类型，其中包括数组类型。
		○ 数组类型是从抽象的基类型 Array 派生的引用类型。 所有数组都会实现 IList 和 IEnumerable。 可以使用 foreach 语句循环访问数组。 单维数组还实现了 IList<T> 和 IEnumerable<T>。
	• 默认值行为
		○ 对于值类型，使用默认值（0 位模式）初始化数组元素；元素将具有值 0。
		○ 所有引用类型（包括不可为 null 的类型）都具有值 null。
		○ 对于可为 null 的类型，HasValue 设置为 false，元素将设置为 null。

 * 数组是一个引用类型
	• 在 C# 中，数组实际上是对象，而不只是如在 C 和 C++ 中的连续内存的可寻址区域。
    • Array 是所有数组类型的抽象基类型。 可以使用 Array 具有的属性和其他类成员。
 */

namespace LearnCSharp.Basic
{
    internal class LearnArray
    {
        /* 【10801：一维数组】
         * 一维/单维数组：将同一类型的多个数据存储在一个单一链上的数据结构
		 * 单维数组的声明和初始化方式：
				其一 数据类型[] 变量名 = new 数据类型[数组长度];//这种方式后续单独为每一个元素进行初始化
			    其二 数据类型[] 变量名 = new 数据类型[数组长度]{元素一, 元素二, ..., 元素n}; //大括号内为初始化每个元素，元素数量不得超过数组长度
		        其三 数据类型[] 变量名 = new 数据类型[]{元素一, 元素二, ..., 元素n};//大括号内为初始化每个元素，编译器根据元素数量决定数组长度
		        其四 数据类型[] 变量名 = {元素一, 元素二, ..., 元素n};//大括号内为初始化每个元素，编译器根据元素数量决定数组长度
		   原则上每一个数组元素应是声明时的数据类型，但也可以是能隐式转换成声明时数据类型的类型。
		   声明时使用object声明，则元素可以为任意不同类型。
		   数组一旦声明并初始化，数组的长度即容量即不能再次改变，但其中的每一个值可以改变。
		   数组中元素的索引序号是从0开始的，即第一个元素的索引序号是0

		 * 访问单维数组的方式：
				其一 变量名[索引序号]--返回指定索引序号的值，即数组中第“索引序号+1”个值，索引序号从0开始，不能大于等于数组长度
		        其二 变量名[^倒数序号]--返回指定倒数序号的值，即数组中倒数第“倒数序号”个值，倒数序号从1开始，不能大于数组长度
		        其三 变量名[起始序号..结束序号]--返回指定起始序号(包含)和结束序号(不包含)之间的值的新数组（切片模式）
		   关于范围访问需要注意：范围索引默认永远是正序索引的，即起始序号正序来看必须小于结束序号
		   比如有一长度为7的数组a,a[1..5]、a[^6..^1]、a[2..^1]、a[^5..5]是合法的，而a[5..1]、a[^1..^6]、a[^1..2]、a[5..^5]是会报错的*/
        public static void LearnSingleDimensionalArray()
		{
            Console.WriteLine("\n------示例：一维数组------\n");
            //创建一个长度为10的整数数组，初始化为{ 12, 10, 15, -3, 2, 0, 300, 45, -20, 10 }
            int[] integers = new int[10] { 12, 10, 15, -3, 2, 0, 300, 45, -20, 10 };

            Console.Write("已成功创建数组，遍历其每一个值：");

            //遍历数组中的每一个元素
            foreach (var item in integers)
			{
                Console.Write($"{item}  ");
			}
			Console.WriteLine();

            //通过索引访问数组中的元素
            Console.WriteLine($"用integers[2]访问数组第三个元素，值为：{integers[2]}");
            Console.WriteLine($"使用integers[^2]访问数组倒数第二个元素，值为：{integers[^2]}");

            /*范围索引 起始序号..结束序号 从正序来看起始序号永远应在结束序号之前或相同
			  以下代码在编译时不会报错，但是在运行时会报错
			  int[] newIntegers1 = integers[5..1]; -- 正确访问应是：integers[2..6]
              int[] newIntegers2 = integers[^1..^6]; -- 正确访问应是：integers[^5..^0]
              int[] newIntegers3 = integers[6..^9]; -- 正确访问应是：integers[^8..7]
              int[] newIntegers4 = integers[^1..5]; -- 正确访问应是：integers[6..^0];*/
            //使用范围索引访问数组中的元素
            Console.Write("使用integers[1..5]访问数组第二到第五个元素，值为：");

            foreach (var item in integers[1..5])
			{
                Console.Write($"{item}  ");
			}
			Console.WriteLine();

            Console.Write("使用integers[^7..^1]访问数组倒数第七个到倒数第二个元素：");
			foreach (var item in integers[^7..^1])
			{
                Console.Write($"{item}  ");
			}
            Console.WriteLine();
            Console.WriteLine();
        }

        /* 【10802：多维数组】
         * 多维数组：数组可具有多个维度，多维数组也可称其为矩形数组
		 * 多维数组的声明和赋值方式（以二维数组为例）：
				其一 数据类型[,] 变量 = new 数据类型[维度一长度,维度二长度];//这种方式后续单独为每一个元素进行初始化
				其二 数据类型[,] 变量 = new 数据类型[维度一长度,维度二长度]{{元素一, 元素二, ..., 元素n}, {元素一, 元素二, ..., 元素n}, ..., {元素一, 元素二, ..., 元素n}};
				其三 数据类型[,] 变量 = new 数据类型[,]{{元素一, 元素二, ..., 元素n}, {元素一, 元素二, ..., 元素n}, ..., {元素一, 元素二, ..., 元素n}};
				其四 数据类型[,] 变量 = {{元素一, 元素二, ..., 元素n}, {元素一, 元素二, ..., 元素n}, ..., {元素一, 元素二, ..., 元素n}};
		   多维数组的维度也称为秩，维度(秩)的数量由[]内的,数量决定，即维度数量=,数量+1
		   多维数组的元素总量(即数组长度)是所有秩值的乘积
		 */
        public static void LearnMultiDimensionalArray()
		{
            Console.WriteLine("\n------示例：多维数组------\n");
            //创建一个3行4列的二维整数数组，初始化为{ { 3, 4, 5, 6 }, { -1, 20, -30, 0 }, { 0, 60, -12, 12 } }
            int[,] matrix = new int[3, 4]
			{
				{  3,  4,   5, 6  },
				{ -1, 20, -30, 0  },
				{  0, 60, -12, 12 }
			};

			Console.WriteLine("已成功创建二维数组，遍历每一个维度的每一个值：");

			for (int i = 0; i < matrix.GetLength(0); i++) //多维数组GetLength方法可以传入一个维度序号变量来获取传入维度的长度
            {
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					Console.WriteLine($"matrix[{i}, {j}] = {matrix[i, j]}");
				}
			}

			Console.WriteLine();

            //创建一个2行3列4深度的三维整数数组，初始化为{ { { 1, 2, 3, 4 }, { 4, 5, 6, 7 }, { 7, 8, 9, 0 } }, { { -1, -2, -3, -4 }, { -4, -5, -6, -7 }, { -7, -8, -9, 0 } } }
            int[,,] matrix3d = new int[2, 3, 4]
			{
				{
					{ 1 ,2 ,3, 4 },
					{ 4, 5, 6, 7 },
					{ 7, 8, 9, 0 } 
				},
				{
					{ -1, -2, -3, -4 },
					{ -4, -5, -6, -7 },
					{ -7, -8, -9, 0  }
				}
			};

            Console.WriteLine("已成功创建三维数组，遍历每一个维度的每一个值：");

			for (int i = 0; i < matrix3d.GetLength(0); i++)
			{
				for (int j = 0; j < matrix3d.GetLength(1); j++)
				{
					for (int k = 0; k < matrix3d.GetLength(2); k++)
					{
						Console.WriteLine($"matrix3d[{i}, {j}, {k}] = {matrix3d[i, j, k]}");
					}
				}
			}

            Console.WriteLine();

			/*从三维数组可以看出，随着维度增加，直接初始化也会更复杂
			  所以当数组维度增加时，我们可以先声明，再依次赋值
			  比如声明一个四维数组
			  int[,,,] array4d = new int[2, 3, 4, 5];
			  array4d[0, 0, 0, 0] = 0;
			  array4d[0, 0, 0, 1] = 1;
			  ...
			  array4d[1, 2, 3, 4] = 119;*/
        }

        /* 【10803：交错数组】
         * 交错数组：交错数组是一个数组，其元素是数组，大小可能不同。 交错阵列有时称为“数组的数组”
         * 交错数组的声明和初始化的方式
				其一 数据类型[][] 变量名 = new 数据类型[数组长度][];//这种方式后续单独为每一个元素进行初始化
				其二 数据类型[][] 变量名 = new 数据类型[数组长度][]{ new 数据类型[]{元素一, 元素二, ..., 元素m},
						  											 new 数据类型[]{元素一, 元素二, ..., 元素n},
																	 ...,
																	 new 数据类型[]{元素一, 元素二, ..., 元素o}};
				其三 数据类型[][] 变量名 = new 数据类型[][]{ new 数据类型[]{元素一, 元素二, ..., 元素m},
															 new 数据类型[]{元素一, 元素二, ..., 元素n},
															 ...,
															 new 数据类型[]{元素一, 元素二, ..., 元素o}};
				其四 数据类型[][] 变量名 = { new 数据类型[]{元素一, 元素二, ..., 元素m},
											 new 数据类型[]{元素一, 元素二, ..., 元素n},
											 ...,
											 new 数据类型[]{元素一, 元素二, ..., 元素o}}
		  交错数组也拥有维度，即每一个[]就代表一个维度，所以可以连续使用多个[]来声明交错单维数组
		  [][]表示该交错数组的每个元素是单维数组，[][][]表示该交错数组的每个一维元素是交错数组，每个二维交错数组的元素是单维数组
		  最后一个维度即最后一个中括号[]表示的是最终的数组，它可以是一个单维数组，也可以是一个多维数组比如[,]
		  交错数组的同一维度的子数组并不要求长度相等，子数组的长度可以都不相同，但注意一旦定义后长度固定不能更改
		 */
        public static void LearnJaggedArray()
		{
            Console.WriteLine("\n------示例：交错数组------\n");
            //创建一个3行的交错整数数组，每一行的长度不同，初始化为{ { 1, 2, 3, 4 }, { 1, 2, 3, 4, 5}, { 1, 2, 3 } }
            int[][] jaggedIntegers = new int[3][]
			{
				new int[]{ 1, 2, 3, 4 },
				new int[]{ 1, 2, 3, 4, 5},
				new int[]{ 1, 2, 3 }
			};

			/*上述代码等效于下述已注释代码
			  int[][] jaggedIntegerss = new int[3][];
			  jaggedIntegers[0] = new int[] { 1, 2, 3, 4 };
			  jaggedIntegers[1] = new int[] { 1, 2, 3, 4, 5 };
			  jaggedIntegers[2] = new int[] { 1, 2, 3 };*/

            Console.WriteLine("已成功创建交错数组，遍历每一个维度的每一个值：");

			for (int i = 0; i < jaggedIntegers.Length; i++)
			{
				for (int j = 0; j < jaggedIntegers[i].Length; j++)
				{
					Console.WriteLine($"jaggedIntegers[{i}][{j}] = {jaggedIntegers[i][j]}");
				}
			}

            Console.WriteLine();
        }




		
		public static void StartLearnArray()
		{
            string title = "001 单维数组\n" +
                "002 多维数组\n" +
                "003 交错数组";

            do
            {
                Console.WriteLine("【数组学习】");
                Console.WriteLine(title);
                Console.Write("请输入上列编号（如001）查看对应知识点代码运行结果：");

                string? input = Console.ReadLine();

                Console.WriteLine();

                switch (input)
                {
                    case "001": LearnSingleDimensionalArray(); break;
                    case "002": LearnMultiDimensionalArray(); break;
                    case "003": LearnJaggedArray(); break;
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