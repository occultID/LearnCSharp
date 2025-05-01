/*【行为型————迭代器模式】
 * 迭代器模式（Iterator Pattern）是一种对象行为型设计模式，它提供了一种方法来顺序访问一个集合对象中的元素，而不暴露该对象的内部表示。
 * 迭代器模式通常用于集合类，允许客户端以统一的方式遍历不同类型的集合。
 * 迭代器模式的主要组成部分包括：
 * 1. 迭代器（Iterator）：定义了访问和遍历元素的接口。
 * 2. 聚合（Aggregate）：定义了创建迭代器的方法。
 * 3. 具体迭代器（Concrete Iterator）：实现了迭代器接口，维护当前遍历的位置。
 * 4. 具体聚合（Concrete Aggregate）：实现了聚合接口，返回一个具体迭代器的实例。
 */

namespace LearnCSharp.DesignPattern
{
    internal class LearnIterator
    {
    }
}
