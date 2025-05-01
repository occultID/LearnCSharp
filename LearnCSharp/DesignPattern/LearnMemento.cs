/*【：行为型————备忘录模式】
 * 备忘录模式（Memento Pattern）是一种行为型设计模式，它允许在不暴露对象内部状态的情况下捕获对象的内部状态，并在需要时恢复该状态。
 * 备忘录模式通常用于需要保存和恢复对象状态的场景，例如撤销操作、游戏存档等。
 * 备忘录模式的主要组成部分包括: 
 *  1. 发起人（Originator）：创建一个备忘录来保存当前状态，并可以使用备忘录恢复状态。
 *  2. 备忘录（Memento）：存储发起人的内部状态，但不允许外部访问。
 *  3. 管理者（Caretaker）：负责保存和管理备忘录，但不允许访问备忘录的内容。
 *  4. 具体发起人（Concrete Originator）：实现了发起人接口，定义了创建和恢复备忘录的方法。
 *  5. 具体备忘录（Concrete Memento）：实现了备忘录接口，存储发起人的状态。
 *  6. 具体管理者（Concrete Caretaker）：实现了管理者接口，负责保存和恢复备忘录。
 */

namespace LearnCSharp.DesignPattern
{
    internal class LearnMemento
    {
    }
}
