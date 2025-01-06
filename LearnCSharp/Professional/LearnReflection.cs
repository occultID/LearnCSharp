/*【学习反射】
 * 反射概述
	• 反射提供封装程序集、模块和类型的对象
		○ 程序集包含模块，模块包含类型，类型包含成员
	• System.Reflection命名空间中的类与System.Type类使用户能获取有关加载的程序集和其中定义的类型的信息
		○ 可以使用反射在运行时创建、调用和访问类型实例
	• 反射用法概述
		○ 动态地创建类型的实例
			§ 检查和实例化程序集中已有的类型
		○ 将类型绑定到现有对象
		○ 从现有对象中获取类型
			§ 随即可调用其方法
			§ 随即可访问其字段和属性
			§ 随即可访问其或其成员所被附加的特性（如有）
		○ 在运行时构建新类型
			§ 使用System.Reflection.Emit中的类来实现
		○ 执行后期绑定
			§ 访问在运行时创建的类型上的方法

 * 反射技术的常用类型
	• System.Type类
		○ Type类是反射的中心
		○ 当反射提出请求时，CLR为已加载的类型创建Type对象
		○ 可使用Type对象的方法、字段、属性和嵌套类来查找该类型的任何信息
			§ 举例：使用Type类的GetType方法从已加载的程序集中获取Type对象
			§ 举例：使用Type类的GetMembers方法查找已获取Type对象类型的所有成员并获取一组描述当前类型各成员的MemberInfo对象
	• System.Reflection命名空间中常用类用法
		○ Assembly类
			§ 用于定义和加载程序集
			§ 用于加载程序集清单中列出的模块
			§ 用于在此程序集中定位一个类型并创建一个它的实例
				□ 举例：使用Assembly类的GetType和GetTypes方法从尚未加载的程序集中获取Type对象，传入所需类型的名称
		○ Module类
			§ 用于发现如包含模块的程序集和模块中的类的信息
				□ 举例：使用Module类的GetType和GetTypes方法获取模块Type对象
			§ 用于获取所有全局方法或模块上定义的其它特定的非全局方法
		○ ConstructorInfo类
			§ 用于发现构造函数的名称、参数、访问级别（如public、private）和实现详细信息（如abstract、virtual）等信息
			§ 使用Type类型的GetConstructors或GetConstructor方法来调用特定构造函数
		○ MethodInfo类
			§ 用于发现方法的名称、返回类型、参数、访问级别和实现详细信息等信息
			§ 使用Type类的GetMethods或GetMethod方法来调用特定方法
		○ FieldInfo类
			§ 用于发现一个字段的名称、访问级别和实现详细信息等信息
			§ 用于运行时获取或设置字段值
		○ EventInfo类
			§ 用于发现事件的名称、事件处理器数据类型、自定义特性、声明类型以及事件的反射类型等信息
			§ 用于运行时添加或删除事件处理程序
		○ PropertyInfo类
			§ 用于发现属性的名称、数据类型、声明类型、反射类型和读写状态等信息
			§ 用于运行时获取或设置属性值
		○ ParameterInfo类
			§ 用于发现参数的名称、数据类型、参数类型以及在方法签名中的位置等信息
		○ CustomAttributeData类
			§ 用于在应用程序域的仅反射上下文中工作时发现有关自定义特性的信息
			§ 用于检查特性而无需创建它们的实例
	• System.Reflection.Emit命名空间
		○ 该命名空间的类提供一种专用形式的反射用于能够在运行时生成类型
	• 其他典型用法
		○ 使用反射来创建称为类型浏览器的应用程序，使用户能选择类型并查看类型信息
		○ JS等语言的编译器使用反射来构造符号表
		○ System.Runtime.Serialization命名空间中的类使用反射来访问数据并确定要保存哪些字段
		○ System.Runtime.Remoting命名空间中的类通过序列化间接使用反射

 * 反射中的运行时类型
	• 反射提供如Type、MethodInfo等类用于表示类型、成员、参数或其他代码实体
	• 使用反射时，并不能直接使用这些类，其中大部分类均是抽象的
		○ 相反，使用由公共语言运行时（CLR）提供的类型
		○ 使用C#中的typeof运算符获取类型的Type对象时
			§ 该对象实际上是一个RuntimeType
			§ RunTimeType派生自Type，并提供所有抽象方法的实现
		○ 反射中的运行时类型都是internal的，它们没有与其基类分开记录
			§ 它们的行为由其基类文档来描述
	• 反射类型与泛型类型
		○ 从反射的角度来说，泛型类型和普通类型之间的区别在于泛型类型具有与之关联的一组类型形参（若是泛型类型定义）或类型实参（若是构造类型）
		○ 反射处理泛型类和泛型方法的方式
			§ 泛型类型定义和泛型方法定义的类型参数由Type类的实例表示
				□ Type对象表示泛型类型参数时，Type的很多属性和方法具有不同的行为
			§ 如果Type的实例表示泛型类型，则它包含表示类型形参（对于泛型类型定义）或类型实参（对于构造类型）的类型数组
		○ 反射提供Type和MethodInfo的方法，允许访问类型形参的数组并确定Type的实例是表示类型形参还是表示实际类型
			§ 使用Type类的IsGenericType属性来确定未知类型是否为泛型，使用MethodInfo类的IsGenericMethod属性来确定未知方法是否为泛型方法
			§ 使用Type类的IsGenericTypeDefinition属性来确定Type对象是否表示泛型类型定义，使用MethodBase类的IsGenericMethodDefinition属性来确定MethodInfo是否表示泛型方法定义
			§ 使用Type类的ContainsGenericParameters属性来确定Type对象是否为开放式或封闭式的，同理可使用MethodBase类的ContainsGenericParameters执行相同的功能
				□ 如果确认Type对象是开放式的泛型定义
					® 使用Type类的MakeGenericType方法来创建封闭式泛型类型
					® 使用MethodInfo类的MakeGenericMethod方法来创建封闭式泛型方法
				□ 如果确认Type对象式封闭式的泛型类型或方法
					® 使用Type类的GetGenericTypeDefinition方法来获取泛型类型定义
					® 使用MethodInfo类的GetGenericMethodDefinition方法来获取泛型方法定义
			§ 使用Type类的GetGenericArguments方法获取Type对象的类型参数数组，同理使用MethodInfo类的GetGenericArguments方法来为泛型方法执行相同操作
				□ 使用Type类的IsGenericParameter属性来确定类型参数数组的某个特定元素是类型形参还是类型实参
			§ 使用Type类的下述属性或方法确认泛型参数的源，即确认泛型类型形参来自于要检查的类型、封闭类型还是泛型方法
				□ 首先使用DeclaringMethod属性确定类型形参是否来自泛型方法
					® 如果值不是null引用，则源为泛型方法
				□ 如果不是来自泛型方法，则使用DeclaringType属性来确定泛型类型形参所属的泛型类型
					® 如果源是泛型方法，则DeclaringType属性返回声明泛型方法的类型
				□ 当确认来源后，可使用GenericParameterPosition属性来确定泛型参数在定义它的类型形参列表中的位置
			§ 使用Type类的下述属性或方法可以获取泛型参数的更多信息
				□ 使用GetGenericParameterConstraints方法来获取类型形参的基类型约束和接口约束
				□ 使用GenericParameterAttribute属性获取指示类型形参的协变逆变和特殊约束的值
					® 将GenericParameterAttribute.VarianceMask掩码应用到该属性，如果返回值为GenericParameterAttribute.None则类型形参不变
					® 将GenericParameterAttribute.SpecialConstraintMask掩码应用到该属性，如果返回值为GenericParameterAttribute.None则没有任何特殊约束

 * 反射的安全注意事项
	• 使用反射获取有关类型和成员的信息是不受限制的
		○ 所有代码都可以使用反射来执行以下任务
			§ 枚举类型和成员，并检查其元数据
			§ 枚举并检查程序集和模块
	• 使用反射来访问成员会受到限制
		○ 从.NET Framework 4开始
			§ 只有受信任的代码才能使用反射来访问安全关键（safe-critical）成员
			§ 只有受信任的代码才能使用反射来访问已编译的代码中无法直接访问的非公共成员
			§ 使用反射访问安全关键成员的代码必须具有安全关键成员要求的任何权限
		○ 具有必要权限的情况下，代码可以使用反射来执行以下任务
			§ 访问不是安全关键成员的公共成员
			§ 访问已编译代码中不是安全关键成员的非公共成员
				□ 调用代码的基类的受保护成员
					® 在反射中，这称为family-level访问
				□ 调用代码的程序集中的internal成员
					® 在反射中，这称为程序集级别的访问
				□ 包含调用代码的类的其他实例的私有成员
	• 访问安全关键（security-critical）成员
		○ 一个成员如果满足下列任意一个条件则该成员为安全关键成员
			§ 它具有SecurityCriticalAttribute特性
			§ 它属于具有SecurityCriticalAttribute特性的类型
			§ 它属于具有SecurityCriticalAttribute特性的程序集
		○ 访问安全关键成员的规则
			§ 透明代码不能使用反射来访问安全关键成员，即使是完全受信任的代码
			§ 使用部分信任运行的代码将被视为透明
			§ 无论是通过已编译代码直接访问还是通过反射访问安全关键成员，这些规则都不会变
			§ 从命令行运行的应用程序代码将以“完全信任”运行
				□ 只要不被标记为透明，它就可以使用反射来访问安全关键成员
		○ 透明代码
			§ CLR从若干方面确定一个类型或成员的透明度级别，包括程序集和应用程序域的信任级别
			§ 反射提供了IsSecurityCritical、IsSecuritySafeCritical和IsSecurityTransparent属性，以便发现类型的透明度级别
				□ 安全级别	IsSecurityCritical	IsSecuritySafeCritical	IsSecurityTransparent
				关键	true	false	false
				安全关键	true	true	false
				透明	false	false	true
	• 访问通常不可访问的成员
		○ 根据CLR的可访问性规则，使用反射来调用无法访问的成员，代码必须获得以下两个权限之一
			§ 若要允许代码调用任何非公共成员
				□ 代码必须获得带有ReflectionPermissionFlag.MemberAccess标志的ReflectionPermission
			§ 若要允许代码调用任何非公共成员，只要包含调用成员的程序集的授予集与包含调用代码的程序集的授予集相同或与子集相同
				□ 代码必须获得带有ReflectionPermissionFlag.RestrictedMemberAccess标志的ReflectionPermission
	• 序列化
		○ 对于序列化，带SecurityPermissionAttribute.SerializationFormatter标志的SecurityPermission，无论其访问级别是什么，都能获取和设置序列化类型的成员

 * 动态加载和使用类型
	• 反射提供语言编译器为实现隐式后期绑定而使用的基础结构
		○ 声明与唯一指定的类型相对应，绑定是查找声明的过程，运行时发生此过程就称为后期绑定
	• 反射还可以在代码中显示用于完成后期绑定
 */
using System.Reflection;

namespace LearnCSharp.Professional
{
    internal class LearnReflection
    {
        public static void StartLearnReflection()
        {
			Console.WriteLine("【学习反射 -- 以检查和实例化泛型类型代码为例】\n");

			Console.WriteLine("使用typeof运算符获取一个Dictionary<,>的Type对象dictType");

			Type dictType = typeof(Dictionary<,>);

			Console.WriteLine($"--dictType类型名--output:{dictType.Name}\n");
			Console.WriteLine("使用IsGenericType属性确定类型是否为泛型，然后使用 IsGenericTypeDefinition 属性确定类型是否为泛型类型定义");

			var isGenericType = dictType.IsGenericType;
			var isGenericTypeDefinition = dictType.IsGenericTypeDefinition;

			Console.WriteLine($"--dict是否是泛型：{isGenericType}\n--dict是否是泛型类型定义{isGenericTypeDefinition}\n");
			Console.WriteLine("使用 GetGenericArguments 方法获取包含泛型类型参数的数组,并检查泛型参数相关信息");

			var typeParameters = dictType.GetGenericArguments();
			Console.WriteLine("--------------------------------------------------------------------------------------------");
			ShowGenericParameterInfo( typeParameters );
            Console.WriteLine("--------------------------------------------------------------------------------------------");

			Console.WriteLine("使用Dictionary<,>泛型定义来通过反射构建一个新的具体的Dictionary<int,string>实例dict\n");
			var dict = CreateDictionatyFromReflection<int, string>(dictType);

			Console.WriteLine("为创建的dict实例随机添加5个数据并遍历输出结果：");

			for (int i = 1; i <= 5; i++)
			{
				dict.Add(DateTime.Now.Year * 1_0000 + i, $"姓名{i:00}");
			}

			foreach (var item in dict)
			{
				Console.WriteLine($"Key:{item.Key}  Value:{item.Value}");
			}
        }

		private static void ShowGenericParameterInfo(Type[] typeParameters)
		{
			int position = 1;
            foreach (var tp in typeParameters)
            {
				Console.WriteLine($">>>>>>>>>反射获取参数数组中第【{position}】个参数信息<<<<<<<<<");
				Console.WriteLine("使用 IsGenericParameter 属性确定泛型参数是类型形参还是类型实参");
                if (tp.IsGenericParameter)
				{
                    Console.WriteLine($"--类型形参：形参名--{tp.Name} 形参位置--{tp.GenericParameterPosition}");
                    Console.WriteLine();
                    Console.WriteLine($"使用 GetGenericParameterConstraints 方法获取类型形参{tp}的所有约束生成的数组，并遍历该数组确定泛型类型参数的基类型约束和接口约束");

                    var gpc = tp.GetGenericParameterConstraints();

                    if (gpc is null || gpc.Length == 0)
                        Console.WriteLine($"--{tp}无接口或基类约束");
					else
						foreach (var constraints in gpc)
						{
							if (constraints.IsInterface)
								Console.WriteLine($"--{tp}具有接口约束：{constraints}");
							else if (constraints.IsClass)
								Console.WriteLine($"--{tp}具有基类约束：{constraints}");
							else
								Console.WriteLine($"--{tp}无接口或基类约束");
						}

                    Console.WriteLine();
                    Console.WriteLine($"使用 GenericParameterAttributes 属性确认类型参数{tp}的逆变协变情况和特殊约束");

                    var vConstraints = tp.GenericParameterAttributes & GenericParameterAttributes.VarianceMask;
                    string vcResult = vConstraints switch
                    {
                        GenericParameterAttributes.Covariant => $"--类型形参{tp}是协变的",
                        GenericParameterAttributes.Contravariant => $"--类型形参{tp}是逆变的",
                        _ => $"--类型形参{tp}既无协变也无逆变"
                    };
                    Console.WriteLine(vcResult);

                    var sConstraints = tp.GenericParameterAttributes & GenericParameterAttributes.SpecialConstraintMask;
                    string scResult = sConstraints switch
                    {
                        GenericParameterAttributes.DefaultConstructorConstraint => $"--类型参数{tp}必须具有无参构造函数",
                        GenericParameterAttributes.ReferenceTypeConstraint => $"--类型参数{tp}必须是引用类型",
                        GenericParameterAttributes.NotNullableValueTypeConstraint => $"--类型参数{tp}必须是非空值类型",
                        GenericParameterAttributes.ReferenceTypeConstraint | GenericParameterAttributes.DefaultConstructorConstraint => $"--类型参数{tp}必须具有无参构造函数的引用类型",
                        GenericParameterAttributes.NotNullableValueTypeConstraint | GenericParameterAttributes.DefaultConstructorConstraint => $"--类型参数{tp}必须具有无参构造函数的非空值类型",
                        _ => $"--类型参数{tp}无特殊泛型约束"
                    };
                    Console.WriteLine(scResult);
                }
                else
                    Console.WriteLine($"--类型实参：{tp}");

				position++;
				Console.WriteLine();
            }
        }

		private static Dictionary<TKey, TValue> CreateDictionatyFromReflection<TKey, TValue>(Dictionary<TKey, TValue> dict) where TKey : notnull
		{
			return CreateDictionatyFromReflection<TKey, TValue>(dict.GetType().GetGenericTypeDefinition());
		}

		private static Dictionary<TKey, TValue> CreateDictionatyFromReflection<TKey, TValue>(Type dictType) where TKey : notnull
		{
			if (dictType.IsGenericType)
			{
				if (!(dictType.GetGenericTypeDefinition() == typeof(Dictionary<,>)))
					throw new ArgumentException("请确定用于反射的类型是Dictionary<,>类型");
			}
			else
				throw new ArgumentException("请确定用于反射的类型是Dictionary<,>类型");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("构造用于反射构建Dictionary<,>类型参数的实参数组typeArgs：\n" +
                $"--Type[] typeArgs = new[] {{ typeof({typeof(TKey).Name}), typeof({typeof(TValue).Name}) }};");
			Console.WriteLine("使用typeArgs作为参数调用获得的Dictionary<,>泛型定义的MakeGenericType方法构造反射构建的具体Dictionary<,>类型的Type对象constructed：\n" +
                "--Type constructed = dictType.GetGenericTypeDefinition().MakeGenericType(typeArgs);");
            Console.WriteLine("使用constructed作为参数调用静态类Activator的CreateInstance方法创建反射构建的具体Dictionary<,>类型的实例：\n" +
                "--object dictObj = Activator.CreateInstance(constructed);");
            Console.WriteLine("--------------------------------------------------------------------------------------------");

            Type[] typeArgs = new[] { typeof(TKey), typeof(TValue) };
            Type constructed = dictType.GetGenericTypeDefinition().MakeGenericType(typeArgs);
            object dictObj = Activator.CreateInstance(constructed)!;

			return (Dictionary<TKey, TValue>)dictObj;	
		}
    }
}
