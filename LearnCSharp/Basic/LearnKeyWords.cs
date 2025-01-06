/*【学习标识符】
 * 标识符：标识符是程序员为类、方法、变量等选择的名字。
           标识符应该是一个完整且有意义的词，它由以字母或下划线开头的Unicode字符构成。
           C#标识符是区分大小写的。例如Name和name会被C#认为是两个不同的标识符。
           通常约定参数、局部变量以及私有字段的首个单词应该以小写字母开头（camel大小写），如inputString
           通常约定除上述以外的其他类型的标识符首个单词以大写字母开头（Pascal大小写）,如LearnKeyWords、WriteUserInput
 
 * 关键字：关键字是预定义的保留标识符，对编译器有特殊意义。
           大部分关键字是保留的，这部分关键字在程序任意部分都作为保留关键字，不得直接用作常规标识符
           还有一部分关键字是上下文相关的，这部分关键字只在部分程序上下文中有特殊意义
           如确有必要将关键字用作普通标识符，需要在其有特殊含义范围内时在其前面加@前缀进行强制
           保留关键字和上下文关键字在本部分最后的注释中提供
 
 *注意：在Microsoft的实现中，还有4个未文档化的保留关键字：__arglist, __makeref, __reftype, __refvalue
        它们仅在罕见的互操作情形下才需要使用，平时可以完全忽略。
        注意这四个特殊关键字都已双下划线开头。
        C#设计者保留将来把这种标识符转化为关键字的权利，为安全起见，最好不要自己创建这样的标识符
 */
namespace LearnCSharp.Basic //namespace 保留关键字
{
    internal class LearnKeyWords //internal、class 保留关键字
    {
        public static void StartLearnKeywords()
        {
            int number = 5; //;：结束语句 5：是一个字面量
            string words = "你好啊！";//“你好啊！”：是一个字面量

            string output = @"以下代码中，int、string是关键字；number、words是标识符；=是运算符；;是标点；5、""你好啊""是字面量
                            int number = 5; 
                            string words = ""你好啊！"";";
            
            //空白用于格式化代码
            Console.WriteLine(output);//.|()都是运算符

            Console.WriteLine($"\n输出局部变量值--number：{number}，words：{words}");
        }
    }
}

/* C# 保留关键字及其特殊含义
    abstract
    as
    base
    bool
    break
    byte
    case
    catch   
    char
    checked
    class
    const
    continue
    decimal
    default
    delegate
    do
    double
    else
    enum

    event
    explicit
    extern
    false
    finally
    fixed
    float
    for
    foreach
    goto
    if
    implicit
    in
    int
    interface
    internal
    is
    lock
    long

    namespace
    new
    null
    object
    operator
    out
    override
    params
    private
    protected
    public
    readonly
    ref
    return
    sbyte
    sealed
    short
    sizeof
    stackalloc
    
    static
    string
    struct
    switch
    this
    throw
    true
    try
    typeof
    uint
    ulong
    unchecked
    unsafe
    ushort
    using
    virtual
    void
    volatile
    while
*/

/* C# 上下文关键字极其含义
    add
    and
    alias
    ascending
    args
    async
    await
    by
    descending
    dynamic
    equals
    from

    get
    global
    group
    init
    into
    join
    let
    managed（函数指针调用约定）
    nameof
    nint
    not

    notnull
    nuint
    on
    or
    orderby
    partial（类型）
    partial（方法）
    record
    remove
    select

    set
    unmanaged（函数指针调用约定）
    unmanaged（泛型类型约束）
    value
    var
    when（筛选条件）
    where（泛型类型约束）
    where（查询子句）
    with
    yield
*/
