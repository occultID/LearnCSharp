using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.Example
{
    internal class Swapper
    {
        private static void Swap(int left, int right)
        {
            Console.WriteLine($"交换参数值--传值参数--形参--值类型--原始值：left--{left} | right--{right}");
            int temp = left;
            left = right;
            right = temp;
            Console.WriteLine($"交换参数值--传值参数--形参--值类型--交换值：left--{left} | right--{right}");
        }

        private static void Swap(ref int left, ref int right)
        {
            Console.WriteLine($"交换参数值--引用参数--形参--值类型--原始值：left--{left} | right--{right}");
            int temp = left;
            left = right;
            right = temp;
            Console.WriteLine($"交换参数值--引用参数--形参--值类型--交换值：left--{left} | right--{right}");
        }

        private static void Swap(Animal left,Animal right)
        {
            Console.WriteLine($"交换参数值--传值参数--形参--引用类型--原始值：left--{left.Name} | right--{right.Name}");
            var temp = left;
            left = right;
            right = temp;
            Console.WriteLine($"交换参数值--传值参数--形参--引用类型--交换值：left--{left.Name} | right--{right.Name}");
        }

        private static void Swap(ref Animal left, ref Animal right)
        {
            Console.WriteLine($"交换参数值--引用参数--形参--引用类型--原始值：left--{left.Name} | right--{right.Name}");
            var temp = left;
            left = right;
            right = temp;
            Console.WriteLine($"交换参数值--引用参数--形参--引用类型--交换值：left--{left.Name} | right--{right.Name}");
        }
        private class Animal 
        {
            public string Name { get; set; }
            public Animal(string name) => Name = name;
        }





        public static void Swap()
        {
            int left = 25, right = 52;
            Console.WriteLine($"交换参数值--实参--值类型--原始值：left--{left} | right--{right}");
            Swap(left, right);
            Console.WriteLine($"交换参数值--实参--值类型--交换值：left--{left} | right--{right}");

            Console.WriteLine();

            Console.WriteLine($"交换参数值--实参--值类型--原始值：left--{left} | right--{right}");
            Swap(ref left, ref right);
            Console.WriteLine($"交换参数值--实参--值类型--交换值：left--{left} | right--{right}");

            Console.WriteLine();

            Animal leftA = new Animal("狗"), rightA = new Animal("猫");
            Console.WriteLine($"交换参数值--实参--引用类型--原始值：left--{leftA.Name} | right--{rightA.Name}");
            Swap(leftA, rightA);
            Console.WriteLine($"交换参数值--实参--引用类型--交换值：left--{leftA.Name} | right--{rightA.Name}");

            Console.WriteLine();

            leftA = new Animal("狗"); rightA = new Animal("猫");
            Console.WriteLine($"交换参数值--实参--引用类型--原始值：left--{leftA.Name} | right--{rightA.Name}");
            Swap(ref leftA, ref rightA);
            Console.WriteLine($"交换参数值--实参--引用类型--交换值：left--{leftA.Name} | right--{rightA.Name}");
        }
    }
}
