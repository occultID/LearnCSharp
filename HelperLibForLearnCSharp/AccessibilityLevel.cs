using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelperLibForLearnCSharp
{
    //定义不同访问级别的成员来在程序集内外部进行测试学习
    public class AccessibilityLevel
    {
        private string PrivateInfo { get; set; } = "private级别属性";
        protected string ProtectedInfo { get; set; } = "protected级别属性";
        internal string InternalInfo { get; set; } = "internal级别属性";
        public string PublicInfo { get; set; } = "public级别属性";
        protected internal string ProtectedInternalInfo { get; set; } = "protected internal级别属性";

        private protected string PrivateProtectedInfo { get; set; } = "private protected级别属性";

        public virtual void PrintAccessibilityLevel()
        {
            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{this.GetType()}");
            Console.WriteLine($"------通过当前类型内部访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{nameof(PrivateInfo),-22} | 属性访问级别：{"private",-18} | 属性值：{PrivateInfo}】");
            Console.WriteLine($"【属性名：{nameof(ProtectedInfo),-22} | 属性访问级别：{"protected",-18} | 属性值：{ProtectedInfo}】");
            Console.WriteLine($"【属性名：{nameof(InternalInfo),-22} | 属性访问级别：{"internal",-18} | 属性值：{InternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(PrivateProtectedInfo),-22} | 属性访问级别：{"private protected",-18} | 属性值：{PrivateProtectedInfo}】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();

            AccessibilityLevel levelInner = new AccessibilityLevel();
            levelInner.PrivateInfo = $"levelInner--{this.PrivateInfo}";
            levelInner.ProtectedInfo = $"levelInner--{this.ProtectedInfo}";
            levelInner.InternalInfo = $"levelInner--{this.InternalInfo}";
            levelInner.PublicInfo = $"levelInner--{this.PublicInfo}";
            levelInner.ProtectedInternalInfo = $"levelInner--{this.ProtectedInternalInfo}";
            levelInner.PrivateProtectedInfo = $"levelInner--{this.PrivateProtectedInfo}";

            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{levelInner.GetType()}");
            Console.WriteLine($"------通过当前类型内部声明本类型的实例来访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{nameof(levelInner.PrivateInfo),-22} | 属性访问级别：{"private",-18} | 属性值：{levelInner.PrivateInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelInner.ProtectedInfo),-22} | 属性访问级别：{"protected",-18} | 属性值：{levelInner.ProtectedInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelInner.InternalInfo),-22} | 属性访问级别：{"internal",-18} | 属性值：{levelInner.InternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelInner.PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{levelInner.PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelInner.ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{levelInner.ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelInner.PrivateProtectedInfo),-22} | 属性访问级别：{"private protected",-18} | 属性值：{levelInner.PrivateProtectedInfo}】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();
        }
    }

    public class AccessibilityLevelChild : AccessibilityLevel
    {
        public override void PrintAccessibilityLevel()
        {
            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{this.GetType()}");
            Console.WriteLine($"基类：{this.GetType().BaseType}");
            Console.WriteLine($"------通过当前类型在程序集内部的派生类访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法访问基类中的private成员】");
            Console.WriteLine($"【属性名：{nameof(ProtectedInfo),-22} | 属性访问级别：{"protected",-18} | 属性值：{ProtectedInfo}】");
            Console.WriteLine($"【属性名：{nameof(InternalInfo),-22} | 属性访问级别：{"internal",-18} | 属性值：{InternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(PrivateProtectedInfo),-22} | 属性访问级别：{"private protected",-18} | 属性值：{PrivateProtectedInfo}】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();

            AccessibilityLevel levelInChild = new AccessibilityLevel();
            //levelInChild.PrivateInfo = $"levelInChild--{this.PrivateInfo}";
            //levelInChild.ProtectedInfo = $"levelInChild--{this.ProtectedInfo}";
            levelInChild.InternalInfo = $"levelInChild--{this.InternalInfo}";
            levelInChild.PublicInfo = $"levelInChild--{this.PublicInfo}";
            levelInChild.ProtectedInternalInfo = $"levelInChild--{this.ProtectedInternalInfo}";
            //levelInChild.PrivateProtectedInfo = $"levelInChild--{this.PrivateProtectedInfo}";

            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{levelInChild.GetType()}");
            Console.WriteLine($"------通过当前类型在程序集内部的派生类内部声明本类型的实例来访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法通过派生类中的基类实例访问private成员】");
            Console.WriteLine($"【属性名：{"ProtectedInfo",-22} | 属性访问级别：{"protected",-18} | 错误：无法通过派生类中的基类实例访问protected成员】");
            Console.WriteLine($"【属性名：{nameof(levelInChild.InternalInfo),-22} | 属性访问级别：{"internal",-18} | 属性值：{levelInChild.InternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelInChild.PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{levelInChild.PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelInChild.ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{levelInChild.ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{"PrivateProtectedInfo",-22} | 属性访问级别：{"private protected",-18} | 错误：无法通过派生类中的基类实例访问private protected成员");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();

            AccessibilityLevelChild childInner = new AccessibilityLevelChild();
            //childInner.PrivateInfo = $"childInner--{this.PrivateInfo}";
            childInner.ProtectedInfo = $"childInner--{this.ProtectedInfo}";
            childInner.InternalInfo = $"childInner--{this.InternalInfo}";
            childInner.PublicInfo = $"childInner--{this.PublicInfo}";
            childInner.ProtectedInternalInfo = $"childInner--{this.ProtectedInternalInfo}";
            childInner.PrivateProtectedInfo = $"childInner--{this.PrivateProtectedInfo}";

            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{childInner.GetType()}");
            Console.WriteLine($"基类：{childInner.GetType().BaseType}");
            Console.WriteLine($"------通过当前类型在程序集内部的派生类内部声明派生类的实例访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法通过派生类中的派生类实例访问private成员】");
            Console.WriteLine($"【属性名：{nameof(childInner.ProtectedInfo),-22} | 属性访问级别：{"protected",-18} | 属性值：{childInner.ProtectedInfo}】");
            Console.WriteLine($"【属性名：{nameof(childInner.InternalInfo),-22} | 属性访问级别：{"internal",-18} | 属性值：{childInner.InternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(childInner.PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{childInner.PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(childInner.ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{childInner.ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(childInner.PrivateProtectedInfo),-22} | 属性访问级别：{"private protected",-18} | 属性值：{childInner.PrivateProtectedInfo}】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();
        }
    }

    public class AccessibilityTest
    {
        public static void TestAccessibilityLevel()
        {
            AccessibilityLevel level = new AccessibilityLevel();
            level.PrintAccessibilityLevel();

            AccessibilityLevelChild child = new AccessibilityLevelChild();
            child.PrintAccessibilityLevel();

            PrintAccessibilityLevel();
        }

        public static void PrintAccessibilityLevel() 
        {
            AccessibilityLevel levelOuter = new AccessibilityLevel();
            //levelOuter.PrivateInfo = $"levelOuter--{levelOuter.PrivateInfo}";
            //levelOuter.ProtectedInfo = $"levelOuter--{levelOuter.ProtectedInfo}";
            levelOuter.InternalInfo = $"levelOuter--{levelOuter.InternalInfo}";
            levelOuter.PublicInfo = $"levelOuter--{levelOuter.PublicInfo}";
            levelOuter.ProtectedInternalInfo = $"levelOuter--{levelOuter.ProtectedInternalInfo}";
            //levelOuter.PrivateProtectedInfo = $"levelOuter--{levelOuter.PrivateProtectedInfo}";

            Console.WriteLine($"程序集：{Assembly.GetExecutingAssembly().GetName()}");
            Console.WriteLine($"类型：{levelOuter.GetType()}");
            Console.WriteLine($"------通过当前类型所属程序集内部的其他类型内的本类型实例来访问各级别属性并输出------");
            Console.WriteLine($"【属性名：{"PrivateInfo",-22} | 属性访问级别：{"private",-18} | 错误：无法通过程序集内其它类中的当前类实例访问private成员】");
            Console.WriteLine($"【属性名：{"ProtectedInfo",-22} | 属性访问级别：{"protected",-18} | 错误：无法通过程序集内其它类中的当前类实例访问protected成员】");
            Console.WriteLine($"【属性名：{nameof(levelOuter.InternalInfo),-22} | 属性访问级别：{"internal",-18} | 属性值：{levelOuter.InternalInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelOuter.PublicInfo),-22} | 属性访问级别：{"public",-18} | 属性值：{levelOuter.PublicInfo}】");
            Console.WriteLine($"【属性名：{nameof(levelOuter.ProtectedInternalInfo),-22} | 属性访问级别：{"protected internal",-18} | 属性值：{levelOuter.ProtectedInternalInfo}】");
            Console.WriteLine($"【属性名：{"PrivateProtectedInfo",-22} | 属性访问级别：{"private protected",-18} | 错误：无法通过程序集内其它类中的当前类实例访问private protected成员】");
            Console.WriteLine();
            Console.WriteLine("************************************");
            Console.WriteLine();
        }
    }
}