/*【313：行为型————访问者模式】
 * 访问者模式（Visitor Pattern）是一种对象行为模式，它允许你在不修改对象结构的前提下，定义新的操作。通过将操作封装在访问者对象中，可以在运行时动态地添加新操作，而不需要修改被访问的对象。
 * 访问者模式通常用于以下场景：
 *  1. 当你需要对一个对象结构中的元素进行多种操作，但又不想修改这些元素的类时。
 *  2. 当你需要对一个对象结构中的元素进行操作，但又不想让这些元素知道操作的具体实现时。
 *  3. 当你需要对一个对象结构中的元素进行操作，但又不想让这些元素的类层次结构变得复杂时。
 *  4. 当你需要将数据结构与操作分离时。
 * 访问者模式的优点：
 *  1. 可以在不修改对象结构的前提下，定义新的操作。
 *  2. 可以将操作封装在访问者对象中，使得操作与对象结构分离。
 *  3. 可以在运行时动态地添加新操作，而不需要修改被访问的对象。
 *  4. 可以将操作与对象结构分离，使得操作可以独立于对象结构进行扩展。
 * 访问者模式的缺点：
 *  1. 增加了系统的复杂性，因为需要创建多个访问者和元素类。
 *  2. 当对象结构发生变化时，需要修改所有的访问者类。
 *  3. 访问者模式不适用于对象结构频繁变化的场景，因为每次修改对象结构都需要修改所有的访问者类。
 *  4. 访问者模式不适用于对象结构中元素的类层次结构复杂的场景，因为每个访问者都需要实现对所有元素的访问方法。
 * 访问者模式的主要组成部分包括：
 *  1. 抽象访问者（Visitor）：定义一个访问操作的接口，通常包含多个访问方法，每个方法对应一个具体元素。
 *  2. 具体访问者（ConcreteVisitor）：实现抽象访问者接口，定义对每个具体元素的操作。
 *  3. 抽象元素（Element）：定义一个接受访问者的方法，通常包含一个接受访问者的接口。
 *  4. 具体元素（ConcreteElement）：实现抽象元素接口，定义具体的元素对象。
 *  5. 对象结构（ObjectStructure）：包含一组元素对象，可以遍历这些元素并调用相应的访问者方法。
 *  6. 客户端（Client）：创建具体访问者和具体元素对象，并将它们传递给对象结构进行操作。
 *
 */

using LearnCSharp.DesignPattern.LearnVisitorSpace;
using System.Text;

namespace LearnCSharp.DesignPattern
{
    internal class LearnVisitor
    {
        /*【31301：访问者模式】*/
        public static void LearnVisitorDesignPattern()
        {
            Console.WriteLine("\n------示例：基础访问者模式------\n");
            Console.WriteLine("》》》通过访问者模式访问图形对象并计算总面积《《《");
            Console.WriteLine("-----------------------------------------------");

            //创建形状对象
            Circle circle = new Circle { Radius = 10 }; //半径为10的圆形
            Rectangle rectangle = new Rectangle { Width = 4, Height = 6 }; //宽度为4，高度为6的矩形
            Circle circle1 = new Circle { Radius = 20 }; //半径为20的圆形
            Rectangle rectangle1 = new Rectangle { Width = 2, Height = 3 }; //宽度为2，高度为3的矩形

            //创建形状组对象
            ShapeGroup shapeGroup = new ShapeGroup();
            shapeGroup.AddShape(new Circle { Radius = 1 }); //添加半径为1的圆形
            shapeGroup.AddShape(new Rectangle { Width = 3, Height = 4 }); //添加宽度为3，高度为4的矩形
            shapeGroup.AddShape(new Circle { Radius = 1 }); //添加半径为1的圆形
            shapeGroup.AddShape(new Rectangle { Width = 5, Height = 6 }); //添加宽度为5，高度为6的矩形

            //创建访问者对象
            AreaCalculator areaCalculator = new AreaCalculator();

            //接受访问者
            circle.Accept(areaCalculator); //访问圆形
            rectangle.Accept(areaCalculator); //访问矩形
            circle1.Accept(areaCalculator); //访问圆形
            rectangle1.Accept(areaCalculator); //访问矩形
            shapeGroup.Accept(areaCalculator); //访问形状组

            //输出总面积
            Console.WriteLine($"总面积：{areaCalculator.TotalArea}"); //输出总面积

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnVisitorSpace
{
    #region 访问者模式基础结构
    /*【31300：访问者模式基础结构】*/
    public interface IVisitor //访问者接口
    {
        void Visit(ConcreteElementA element); //具体元素A
        void Visit(ConcreteElementB element); //具体元素B
    }

    public class ConcreteVisitor : IVisitor //具体访问者
    {
        public void Visit(ConcreteElementA element)
        {
            Console.WriteLine("访问者访问了具体元素A");
        }
        public void Visit(ConcreteElementB element)
        {
            Console.WriteLine("访问者访问了具体元素B");
        }
    }

    public interface IElement //元素接口
    {
        void Accept(IVisitor visitor); //接受访问者
    }

    public class ConcreteElementA : IElement //具体元素A
    {
        public void Accept(IVisitor visitor) //接受访问者
        {
            visitor.Visit(this); //关键的双重分派
        }
    }

    public class ConcreteElementB : IElement //具体元素B
    {
        public void Accept(IVisitor visitor) //接受访问者
        {
            visitor.Visit(this); //关键的双重分派
        }
    }

    public class ObjectStructure //对象结构
    {
        private List<IElement> elements = new List<IElement>(); //元素集合
        public void Attach(IElement element) //添加元素
        {
            elements.Add(element);
        }
        public void Detach(IElement element) //移除元素
        {
            elements.Remove(element);
        }
        public void Accept(IVisitor visitor) //接受访问者
        {
            foreach (var element in elements)
            {
                element.Accept(visitor);
            }
        }
    }
    #endregion

    #region 基础访问者模式
    /*【31301：基础访问者模式】
     * 访问者模式的基本实现
     */
    public interface IShape //形状接口
    {
        void Accept(IShapeVisitor visitor); //接受访问者
    }

    public class Circle : IShape //圆形
    {
        public double Radius { get; set; } //半径

        public void Accept(IShapeVisitor visitor) //接受访问者
        {
            visitor.Visit(this); //关键的双重分派
        }
    }

    public class Rectangle : IShape //矩形
    {
        public double Width { get; set; } //宽度

        public double Height { get; set; } //高度

        public void Accept(IShapeVisitor visitor) //接受访问者
        {
            visitor.Visit(this); //关键的双重分派
        }
    }

    public class ShapeGroup : IShape //形状组
    {
        private readonly List<IShape> shapes = new List<IShape>(); //形状集合
        public List<IShape> Shapes => shapes; //形状集合
        
        public void AddShape(IShape shape) //添加形状
        {
            shapes.Add(shape);
        }

        public void RemoveShape(IShape shape) //移除形状
        {
            shapes.Remove(shape);
        }

        public void Accept(IShapeVisitor visitor) //接受访问者
        {

            foreach (var shape in shapes) //遍历形状集合
            {
                shape.Accept(visitor); //递归访问
            }
        }
    }

    public partial interface IShapeVisitor //形状访问者接口
    {
        void Visit(Circle circle); //访问圆形
        void Visit(Rectangle rectangle); //访问矩形

        void Visit(ShapeGroup shapeGroup); //访问形状组
    }

    public class AreaCalculator : IShapeVisitor //面积计算访问者
    {
        public double TotalArea { get; private set; } //总面积

        public void Visit(Circle circle) //访问圆形
        {
            TotalArea += Math.PI * circle.Radius * circle.Radius; //计算圆形面积
        }

        public void Visit(Rectangle rectangle) //访问矩形
        {
            TotalArea += rectangle.Width * rectangle.Height; //计算矩形面积
        }

        public void Visit(ShapeGroup shapeGroup) //访问形状组
        {
            foreach (var shape in shapeGroup.Shapes) //遍历形状集合
            {
                if (shape is Circle circle) //如果是圆形
                {
                    Visit(circle); //计算圆形面积
                }
                else if (shape is Rectangle rectangle) //如果是矩形
                {
                    Visit(rectangle); //计算矩形面积
                }
            }
        }
    }
    #endregion
}

