/*【：行为型————中介者模式】
 * 中介者模式（Mediator Pattern）是一种行为型设计模式，它定义了一个中介对象来封装一组对象之间的交互。通过引入中介者对象，减少了对象之间的直接依赖关系，使得系统更加松耦合。
 * 中介者模式通常用于多个对象之间存在复杂的交互关系时，可以通过中介者来简化这些交互。
 * 中介者模式的主要组成部分包括：
 *  1. 中介者（Mediator）：定义了与各个同事对象交互的接口。
 *  2. 具体中介者（Concrete Mediator）：实现了中介者接口，协调各个同事对象之间的交互。
 *  3. 同事（Colleague）：定义了与中介者的交互接口。
 *  4. 具体同事（Concrete Colleague）：实现了同事接口，向中介者发送请求。
 *  5. 客户端（Client）：创建具体同事对象，并设置它们的中介者。
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp.DesignPattern
{
    internal class LearnMediator
    {
    }
}
