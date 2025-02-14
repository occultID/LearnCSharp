/*【学习结构】
 * 结构类型
	• 结构是一种可封装数据和相关功能的值类型
	• 结构具有值语义
		○ 结构不需要进行堆分配
		○ 结构类型的变量直接包含结构的数据，即结构类型的变量直接存储结构实例
	• 结构特别适合用于封装包含值语义的小型数据结构
		○ 这些数据结构通常只包含少量的数据成员
		○ 这些数据结构通常只有很少的行为或没有行为
		○ 这些数据结构通常不需要使用继承或引用标识
		○ 这些数据结构通常可以使用赋值语义来方便地实现
	• 对于结构类型的变量，将另一个同类型的结构变量赋值给它是复制而不是拷贝引用
	• 在C#内置简单类型中，整数类型、浮点数类型、字符类型和布尔类型实际上都是一个结构类型

 * 结构的声明与定义
	• 结构需要使用struct关键字来进行声明
	• 结构的声明和定义是一体的，声明结构即需实现结构
	• 结构的声明定义形式
		○ [特性]
		访问级别 有效的结构修饰符 partial struct 结构名称<泛型参数列表>:接口 泛型参数约束 { 结构体 }
			§ [特性]：该项为可选配置，标识结构是否被附加特性
			§ 访问级别：该项为可选配置，标识结构的访问权限
				□ 无：默认为internal
				□ public：公开访问级别
				□ protected：受保护的访问级
				□ internal：内部访问级别
				□ private：私有访问级别
			§ 有效的结构修饰符：该项为可选配置，为结构提供一些特征修饰以标识其专属特征
				□ readonly：标识声明的结构类型不可变，其所有数据成员都必须是只读的
				□ ref：标识声明的结构为ref结构
				□ record：将结构声明为record struct类型，可以和readonly结合使用，不能同时和ref一起使用
				□ unsafe：标识声明的结构支持不安全的访问，即内部有非托管类型成员或相关代码，结构本身也能直接用于非托管代码
				□ new：只允许用于修饰内嵌于结构中的类或结构
			§ partial：该项为可选配置，标识结构的实现是否为分开实现
				□ 无 该结构声明后必须完整实现
				□ 有 该结构可在多个文件中进行实现，但必须保障每一个文件中的声明定义签名是一致的，且至少有一个分部必须有实现
			§ struct：该项为必选配置
				□ struct关键字为结构声明的必须关键字
			§ 结构名称：该项为必选配置，即结构的名称
				□ 结构名称通常采用Pascal命名法
				□ 结构名称通常采用一个名词
			§ <泛型参数列表>：该项为可选配置，标识结构有泛型实现
				□ <>尖括号标识泛型参数，其内部为表示数据类型的形式参数
				□ 如有多个泛型参数，每个参数使用“,”隔开
			§ :接口：该项为可选配置，标识结构是否有实现某个或某些接口
				□ “:”标识结构内部将实现标识的接口
				□ 被实现的接口可以有一个或多个，多个接口使用“,”进行分隔
				□ 结构没有继承，只能实现接口
			§ 泛型参数约束：该项为可选配置，标识泛型参数是否需要进行某种约束
				□ 使用该项的前提是结构使用了泛型参数
				□ 即使结构使用了泛型参数，该项也是可选的
			§ { 结构体 }：该项为必选配置，即结构的主体实现
				□ {}花括号用于包含结构体的主体实现
				□ 结构体即所有的结构成员

 * 结构成员：该部分可参考类成员简介
	• 字段
	• 属性
	• 索引器
	• 方法
		○ 常规方法
		○ 构造函数
		○ 解构函数
		○ 运算符
	• 事件
	• 静态成员
	• 嵌套类型
	• unsafe成员

 * 结构类型详解
	• 值语义
		○ 结构是一种值类型，故结构具有值语义
		○ 结构类型的变量直接包含结构的数据
			§ 基于该特性，结构类型内部不能包含用该结构声明的字段或属性，否则会造成无限递归循环
			§ 基于该特性，多个结构不能在彼此内部互相声明对方类型的字段或属性，否则会造成无限递归循环
			§ 如果确实有必要，可以将对应字段或属性声明为静态的
		○ 结构类型的变量值不能为null，除非声明变量时将其声明为可空的结构类型
	• 继承
		○ 所有结构类型都隐式继承自类 System.ValueType，后者又继承自类object
		○ 结构类型永远不会是抽象类型，并且始终隐式密封。
			§ 不能使用abstract和sealed修饰符修饰结构
			§ 不能使用abstract、sealed和virtual修饰符修饰结构成员
			§ 不能使用protected和protected internal来作为结构成员的访问级别
			§ override修饰符只允许重写结构从System.ValueType继承的方法
		○ 结构类型不支持继承，也不能为结构指定基类或将结构作为基类
		○ 结构类型支持实现接口
	• 赋值与分配
		○ 结构的默认值
			§ 结构的默认值与结构的默认构造函数返回的值相对应
			§ 结构的值类型成员默认值将初始化其预定义的默认值
			§ 结构的引用类型成员默认值将初始化为null
		○ 结构成员初始化
			§ 在C#10以前，不能在声明实例字段或属性时对它们进行初始化
		○ 结构类型变量赋值
			§ 对结构类型变量赋值会创建要分配的值的副本
			§ 当结构作为值参数传递或作为函数成员的结果返回时，将创建结构的副本
			§ 当结构的属性或索引器时赋值的目标时，与属性或索引器访问关联的实例表达式必须归类为变量
	• 装箱与拆箱
		○ 装箱：将结构类型的变量使用System.ValueType、object或实现的接口进行包装引用即为装箱
		○ 拆箱：装箱的逆过程
	• this访问器
		○ 结构成员中使用的“this”被分类为变量
		○ 结构成员中可以使用=为“this”赋值
		○ 结构成员中可以使用=将“this”赋值给变量
	• 构造函数与析构函数
		○ 在C#10以前不允许用户声明无参构造函数
			§ 自C#10开始用户可声明无参构造函数
			§ 即便如此，也不建议用户声明无参构造函数，而由编译器自动完成
		○ 在C#11以前结构类型的构造函数必须初始化该类型的所有实例字段
			§ 自C#11开始，构造函数不必初始化所有实例字段
		○ 结构类型内不能声明析构函数（终结器）

 * readonly结构
	• 可以使用readonly修饰符来声明结构类型为不可变
	• readonly结构的所有数据成员都必须是只读的
		○ 任何字段声明都必须具有readonly修饰符
		○ 任何属性（包括自动实现的属性）都必须是只读的
			§ 在C#9.0和更高版本中，属性可以具有init访问器
		○ 在readonly结构中，可变引用类型的数据成员仍可改变其自身的状态
			§ 例如，不能替换数组实例，但是可以修改数组元素的值
	• readonly实例成员
		○ 可以使用readonly修饰符来声明实例成员而不会修改结构的状态
			§ 提供一种方法来指定结构上的单个实例成员不修改状态，其方式与readonly struct指定无实例成员修改状态的方式相同
		○ 如果不能将整个结构声明为readonly，可使用readonly修饰符标记不会修改结构状态的实例成员
			§ 在readonly实例成员内，不能分配结构的实例字段
			§ readonly成员可以调用非readonly成员
				□ 编译器将创建结构实例的副本，并调用该副本上的非readonly成员
		○ readonly修饰符在结构中可应用于以下成员
			§ 方法
			§ 属性
				□ 可使用readonly修饰属性，此时属性不能对包含的字段进行值修改
				□ 对于自动实现的只读属性，编译器会自动为其get访问器声明readonly，而不管属性声明中是否存在readonly修饰符
			§ 索引器
			§ 事件
				□ 只能用于手动实现的事件
				□ 不能用于类似于字段的事件
			§ 静态字段
				□ 对于静态属性和静态方法，不能使用readonly进行修饰
	• 编译器能使用readonly修饰符进行性能优化

 * ref结构
	• 可以在结构的声明中使用ref修饰符
	• ref struct类型的实例是在堆栈上分配的，不能转义到托管堆
	• 使用限制
		○ ref结构不能是数组的元素类型
		○ ref结构不能是类或非ref结构的字段的声明类型
		○ ref结构不能实现接口
		○ ref结构不能被装箱为System.ValueType或System.Object
		○ ref结构不能是类型参数
		○ ref结构变量不能由Lambda表达式或本地函数捕获
		○ ref结构变量不能在async方法中使用
			§ 但是，可以在同步方法中使用ref结构变量
		○ ref结构变量不能在迭代器中使用
	• 可以定义一次性的ref结构
		○ 需确保ref结构符合一次性模式
		○ 该ref结构需有一个实例Dispose方法或扩展的Dispose方法
	• ref结构可以包含ref字段（从C#11开始）
	• 如需组合使用readonly修饰符，readonly修饰符必须位于ref修饰符之前

 * 常规结构设计准则
	• 请勿为结构提供无参构造函数
		○ 遵循此准则，便可创建结构数组，而不必对每个数组项运行该构造函数
		○ 即使C#10开始允许用户声明定义无参构造函数，非必要也不要这样做
	• 请勿定义可变值类型
		○ 可变值类型有几个问题
			§ 当属性的get访问器返回值类型时，调用方会收到一个副本，由于该副本是隐式创建的，因此开发人员可能不会意识到他们正在改变副本而非原始值
			§ 某些语言（尤其是动态语言）在使用可变值类型方面存在问题，因为即使是本地变量，在被取消引用后，也会导致生成一个副本
	• 请勿显示扩展ValueType
		○ 事实上，大多数语言都禁止这样做
	• 请务必确保将所有实例数据设置为零、false或null(视情况而定)的状态有效
		○ 防止在创建结构数组时意外创建无效的实例
	• 请务必在值类型上实现Iequatable<T>
		○ 值类型上默认的Object.Equals方法会导致装箱，且其默认实现效率不高，因为它使用反射
		○ 手动实现Equals可具有更好的性能，并且可得到实施，这样就不会导致装箱

 * 非破坏性变化与with表达式
	• C#10.0中对结构和匿名类型也提供了with表达式支持
	• 如果需要复制包含一些修改的实例，可以使用with表达式来实现非破坏性变化
		○ 非破坏性变化，即可以根据原始(只读)对象来创建一个新的副本，而这个副本相对有变化，但不会影响原本的对象
	• with表达式事项
		○ with表达式不能独立作为语句
		○ 有效的with表达式包含具有非void类型的接收方，接收方类型必须是一条结构或记录
		○ with表达式创建一个新的结构/记录实例，该实例是现有结构/记录实例的一个副本，但可能修改了指定属性和字段
		○ with表达式的使用前提是结构/记录属性是只读init属性或可读写属性
		○ with表达式的结果是一个浅克隆副本，这意味着对于引用类型的属性，只复制对实例的引用
			§ 原始记录和副本记录最终都具有对同一实例的引用
	• with表达式的形式
		○ 结构/记录类型 实例名 = 原接口/记录实例名 with { 初始化器 }
		○ 初始化器的使用方式同对象初始化器
 */


using System.Diagnostics.CodeAnalysis;
using LearnCSharp.Basic.LearnStructSpace;

namespace LearnCSharp.Basic.LearnStructSpace
{
    //定义一个结构来表示平面向量
    //结构是值类型，应当在有必要时才进行自定义结构体
    //结构体作为值类型，按规范应保障其具有“不可变”的属性，所以可定义其为“只读（readonly）”的
    //
    //PS：按C#规范和原则来说,定义的类型名称应该和该类型源代码文件名相同，这里为了方便整理故没按规范来
    public readonly struct Vector : IEquatable<Vector>
    {
        public int VectorX { get; }
        public int VectorY { get; }

        public static Vector ZeroVector { get; } = (0, 0);

        //结构体最好不要定义自定义默认构造函数 CLR会自动完成这一步
        //public Vector()
        //{
        //    VectorX = 0;
        //    VectorY = 0;
        //}

        public Vector(int x, int y)
        {
            VectorX = x;
            VectorY = y;
        }

        public override string ToString()
        {
            return string.Format("This is a vector :({0},{1})", VectorX, VectorY);
        }

        public override int GetHashCode()
        {
            return (VectorX, VectorY).GetHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Vector vector)
            {
                return Equals(vector);
            }
            return false;
        }

        public bool Equals(Vector other)
        {
            return VectorX == other.VectorX && VectorY == other.VectorY;
        }

        //定义一个隐式转换 可将一个（int,int）元组隐式转换为一个向量
        public static implicit operator Vector((int, int) value)
        {
            return new Vector(value.Item1, value.Item2);
        }

		//定义一个隐式转换 可将一个Vector类型转换成一个(int,int)元组
		public static implicit operator (int,int)(Vector vector)
		{
			return (vector.VectorX, vector.VectorY);
		}

        public static bool operator ==(Vector left, Vector right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return !left.Equals(right);
        }

        //重载+运算符 用于计算两个向量的加法
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.VectorX + vector2.VectorX, vector1.VectorY + vector2.VectorY);
        }

        //重载-运算符 用于计算两个向量的减法
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.VectorX - vector2.VectorX, vector1.VectorY - vector2.VectorY);
        }

        //重载*运算符 用于计算两个向量的数量积
        //定义：两个向量的数量积（内积、点积）是一个数量（没有方向），记作a·b
        //定义：已知两个非零向量a,b，作OA=a,OB=b，则∠AOB称作向量a和向量b的夹角，记作θ并规定0≤θ≤π
        public static int operator *(Vector vector1, Vector vector2)
        {
            return vector1.VectorX * vector2.VectorX + vector1.VectorY * vector2.VectorY;
        }

        //重载*运算符 用于计算向量的数乘
        public static Vector operator *(Vector vector, int coefficient)
        {
            return new Vector(vector.VectorX * coefficient, vector.VectorY * coefficient);
        }
        public static Vector operator *(int coefficient, Vector vector)
        {
            return new Vector(vector.VectorX * coefficient, vector.VectorY * coefficient);
        }

        public double Mod()
        {
            return Math.Sqrt(Math.Abs(VectorX * VectorX) + Math.Abs(VectorY * VectorY));
        } 

        public void Deconstruct(out int x, out int y)
        {
            x = VectorX;
            y = VectorY;
        }
    }

    //定义一个结构体来表示平面坐标的点
    public readonly struct Point
    {
        public int X { get; }
        public int Y { get; }

        //静态方法，用于求平面坐标系两点之间的距离
        public static double GetDistance(Point end,Point start)
        {
            return (end - start).Mod();
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        //定义一个隐式转换 可将一个（int,int）元组隐式转换为一个平面坐标
        public static implicit operator Point((int, int) value)
        {
            return new Point(value.Item1, value.Item2);
        }

        //重载-运算符 用于计算平面坐标两点间的向量表示
        public static Vector operator -(Point end, Point start)
        {
            return new Vector(end.X - start.X, end.Y - start.Y);
        }

        public static Point operator +(Point start, Vector vector)
        {
            return new Point(start.X + vector.VectorX, start.Y + vector.VectorY);
        }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }    
}

namespace LearnCSharp.Basic
{
	internal class LearnStruct
	{
		/*【12201：结构使用示例】*/
        public static void StartLearnStruct()
        {
            Console.WriteLine("\n------示例：结构------\n");
            //声明并初始化两个向量Vector实例
            Vector vector1 = new Vector(12,10);
			Vector vector2 = (-15, 6);	//Vector实现了隐式转换所以可以将(int,int)元组赋值给一个Vector实例

			//vector1.VectorX = 6;  //结构按照规范设计成了不可变的，故不能修改其值

			//Vector实现了隐式转换所以可以将Vector实例赋值给一个(int,int)类型的元组变量
			(int X, int Y) vector3 = vector1;
			//通过解构函数，只能将Vector实例赋值给一个(int，int)元组
			(int x, int y) = vector1;

			Console.WriteLine("声明并初始化了两个Vector结构实例用于表示两个向量\n" +
				"vector1:(12, 10)\nvector2(-15,6)\n");
			Console.WriteLine($"通过隐式转换将vector1赋值给了一个元组变量vector3--output:({vector3.X},{vector3.Y})");
			Console.WriteLine($"通过解构函数将vector1解构成一个(int x, int y)元组--output：{(x,y)}");
			Console.WriteLine($"通过Vector重载操作符==来计算vector1和vector2相等性--output：{vector1 == vector2}");
            Console.WriteLine($"通过Vector重载操作符!=来计算vector1和vector2相等性--output：{vector1 != vector2}");
			Console.WriteLine($"通过Vector重载操作符+来计算vector1+vector2的和--output：({(vector1 + vector2).VectorX},{(vector1 + vector2).VectorY})");
            Console.WriteLine($"通过Vector重载操作符-来计算vector1-vector2的差--output：({(vector1 - vector2).VectorX},{(vector1 - vector2).VectorY})");
            Console.WriteLine($"通过Vector重载操作符*来计算vector1和vector2的数量积--output：{vector1*vector2}");
            Console.WriteLine($"通过Vector重载操作符*来计算vector1和整数的数乘，这里整数以5为例子--output：({(vector1*5).VectorX}, {(vector1 * 5).VectorY})");
        }
    }
}
