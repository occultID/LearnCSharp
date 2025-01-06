using System.Reflection.Emit;

namespace LearnCSharp.Example
{
    //实现一个算法来实现汉诺塔游戏的玩法
    //假设三个塔柱分别为A、B、C，假设由用户定义max个圆盘从大到小依次累积于A柱上
    //现在要将A柱的圆盘按规则依次移动到C柱上
    //规则为整个移动过程中，任何一根柱子上小圆盘永远只能在大圆盘之上
    //我们可以这样考虑，每次移动大盘从A柱到C柱需要怎么做
    //已假定共有max个圆盘 要移动编号max的圆盘
    //首先先将编号max-1圆盘借助C柱移动到B柱
    //然后将max号圆盘移动到C柱
    //然后将B柱上的max-1圆盘借助A柱移动到C柱
    internal class Hanoi
    {
        private static Stack<int> A;
        private static Stack<int> B;
        private static Stack<int> C;

        private static int Max;
        private static int Count;
        private static void StartGame(int max)
        {
            if (max <= 0)
            {
                Console.WriteLine("游戏结束！");
            }
            else if (max > 0 && max <= 16)
            {
                CreateHanoi(max);
            }
            else if (max > 16 && max <= 26)
            {
                Console.WriteLine("当A柱圆盘数超过16个后，至少需要131,071次移动才能完成，是否继续进行过程展示：Y/N");
                char.TryParse(Console.ReadLine()?.ToUpper(), out char result);
                if (result == 'Y')
                    CreateHanoi(max);
            }
            else
            {
                Console.WriteLine("当A柱圆盘数超过26个后，至少需要134,217,727次移动才能完成，游戏只展示需要移动次数而不展示过程：");
                Console.WriteLine("将[{0:00000000}]个圆盘按汉诺塔规则从【A柱】移动到【C柱】至少需要移动[{1:000000000000}]次", max, Math.Pow(2, max) - 1);
            }
        }

        public static void StartGame()
        {
            label:
            Console.Write("请输入圆盘数：");
            if (int.TryParse(Console.ReadLine(), out int max))
            {
                StartGame(max);
                Play();
            }
            else
            {
                Console.WriteLine("输入错误！");
                goto label;
            }
        }

        //初始化一个汉诺塔游戏
        private static void CreateHanoi(int max)
        {
            Max = max;
            A = new Stack<int>(Max);
            for (int i = Max; i > 0; i--)
            {
                A.Push(i);
            }
            B = new Stack<int>(Max);
            C = new Stack<int>(Max);

            Console.WriteLine("汉诺塔已初始化，A柱现在已经放置圆盘，从上至下编号为：");
            foreach (var item in A.ToArray())
            {
                Console.Write("{0:000}\t", item);
            }
            Console.WriteLine();
        }

        private static void move(char x, char y)
        {
            int temp = 0;
            switch (x)
            {
                case 'A':
                    temp = A.Pop();
                    break;
                case 'B':
                    temp = B.Pop();
                    break;
                case 'C':
                    temp = C.Pop();
                    break;
                default:
                    break;
            }

            switch (y)
            {
                case 'A':
                    A.Push(temp);
                    break;
                case 'B':
                    B.Push(temp);
                    break;
                case 'C':
                    C.Push(temp);
                    break;
                default:
                    break;
            }

            Console.ForegroundColor = (ConsoleColor)(temp % 16);
            Console.WriteLine("第{3:00000000}次移动->编号盘:[{0:000}] 从【{1}柱】移动到【{2}柱】", temp, x, y, ++Count);
        }

        private static void Play(int max, char a = 'A', char b = 'B', char c = 'C')
        {
            if (max == 1)
            {
                move(a, c);
            }
            else
            {
                Play(max - 1, a, c, b);
                move(a, c);
                Play(max - 1, b, a, c);
            }
        }

        private static void Play()
        {
            Play(Max);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n汉诺塔已移动完毕，A柱圆盘已全部移动至C柱，C柱圆盘从上至下编号为：");
            foreach (var item in C.ToArray())
            {
                Console.Write("{0:000}\t", item);
            }
            Count = 0;
            Console.WriteLine();
        }
    }
}
