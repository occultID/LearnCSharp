/*【代码说明】
 *01.该代码用于导航运行不同的章节和方法。
 *02.该代码使用了字典来存储每个章节对应的方法，并通过输入方法注释代码来调用相应的方法。
 */

using LearnCSharp.Basic;
using LearnCSharp.DesignPattern;
using LearnCSharp.Professional;

namespace LearnCSharp
{
    internal static class DirectNavigation
    {
        private readonly static Dictionary<string, Action> funcDic = new Dictionary<string, Action>()
        {
            ["10101"] = HelloWorld.SayHello,                                                        //Hello World
            ["10201"] = LearnKeyWords.LearnAccessibilityLevelKeywords,                              //访问权限关键字
            ["10210"] = LearnKeyWords.LearnLiteralKeywords,                                         //文本关键字
            ["10301"] = LearnLiteralsAndOthers.StartLearnLiteralsAndOthers,                         //字面量、标点、空白和注释
            ["10401"] = LearnDataType.LearnObject,                                                  //Object类型
            ["10402"] = LearnDataType.LearnDynamic,                                                 //Dynamic类型
            ["10501"] = LearnSimpleType.LearnIntegerType,                                           //整数类型
            ["10502"] = LearnSimpleType.LearnFloatingPointType,                                     //浮点数类型
            ["10503"] = LearnSimpleType.LearnCharacterType,                                         //字符类型
            ["10504"] = LearnSimpleType.LearnBooleanType,                                           //布尔类型
            ["10505"] = LearnSimpleType.LearnStringType,                                            //字符串类型
            ["10506"] = LearnSimpleType.LearnObjectType,                                            //Object类型
            ["10507"] = LearnSimpleType.LearnDynamicType,                                           //Dynamic类型
            ["10601"] = LearnTuple.StartLearnTuple,                                                 //元组
            ["10701"] = LearnCastingAndTypeConversion.LearnImplicitConversion,                      //隐式类型转换
            ["10702"] = LearnCastingAndTypeConversion.LearnExplicitCasting,                         //显式类型转换
            ["10703"] = LearnCastingAndTypeConversion.LearnCastingTool,                             //类型转换工具
            ["10801"] = LearnArray.LearnSingleDimensionalArray,                                     //一维数组
            ["10802"] = LearnArray.LearnMultiDimensionalArray,                                      //多维数组
            ["10803"] = LearnArray.LearnJaggedArray,                                                //交错数组
            ["10901"] = LearnEnum.StartLearnEnum,                                                   //枚举
            ["11001"] = LearnOperatorAndExpression.LearnArithmeticOperator,                         //算术运算符
            ["11002"] = LearnOperatorAndExpression.LearnBooleanLogicalOperator,                     //布尔逻辑运算符
            ["11003"] = LearnOperatorAndExpression.LearnBitwiseAndShiftOperator,                    //位运算符
            ["11004"] = LearnOperatorAndExpression.LearnEqualityAndComparisonOperator,              //相等和比较运算符
            ["11005"] = LearnOperatorAndExpression.LearnMemberAccessOperator,                       //成员访问运算符
            ["11006"] = LearnOperatorAndExpression.LearnNullOperator,                               //空合并运算符
            ["11007"] = LearnOperatorAndExpression.LearnCollectionExpressionOperator,               //集合表达式运算符
            ["11008"] = LearnOperatorAndExpression.LearnTypeTestingAndCastOperator,                 //类型测试和转换运算符
            ["11009"] = LearnOperatorAndExpression.LearnAssignmentOperator,                         //赋值运算符
            ["11010"] = LearnOperatorAndExpression.LearnConditionalOperator,                        //三元运算符
            ["11011"] = LearnOperatorAndExpression.LearnAnonymousFuncAndLambdaOperator,             //匿名函数和Lambda表达式运算符
            ["11012"] = LearnOperatorAndExpression.LearnPointerOperator,                            //指针运算符
            ["11013"] = LearnOperatorAndExpression.LearnOtherPrimaryOperator,                       //其他主运算符
            ["11014"] = LearnOperatorAndExpression.LearnOtherUnaryOperator,                         //一元运算符
            ["11015"] = LearnOperatorAndExpression.LearnSwitchAndWithOperator,                      //switch和with运算符
            ["11016"] = LearnOperatorAndExpression.LearnOperatorOverload,                           //运算符重载
            ["11101"] = LearnStatements.LearnEndPointsAndReachability,                              //语句的终止点和可达性
            ["11102"] = LearnStatements.LearnLabeledStatement,                                      //标签语句
            ["11103"] = LearnStatements.LearnDeclarationStatement,                                  //声明语句
            ["11104"] = LearnStatements.LearnBlockStatement,                                        //块语句
            ["11105"] = LearnStatements.LearnExpressionStatement,                                   //表达式语句
            ["11106"] = LearnStatements.LearnSelectionStatement,                                    //选择语句
            ["11107"] = LearnStatements.LearnIterationStatement,                                    //迭代语句
            ["11108"] = LearnStatements.LearnGotoStatement,                                         //Goto语句
            ["11109"] = LearnStatements.LearnBreakAndContinueStatement,                             //Break和Continue语句
            ["11110"] = LearnStatements.LearnThrowAndReturnStatement,                               //Throw和Return语句
            ["11111"] = LearnStatements.LearnTryStatement,                                          //Try语句
            ["11112"] = LearnStatements.LearnUsingStatement,                                        //Using语句
            ["11113"] = LearnStatements.LearnYieldStatement,                                        //Yield语句
            ["11114"] = LearnStatements.LearnCheckedAndUncheckedStatement,                          //Checked和Unchecked语句
            ["11115"] = LearnStatements.LearnLockStatement,                                         //Lock语句
            ["11116"] = LearnStatements.LearnEmptyStatement,                                        //空语句
            ["11117"] = LearnStatements.LearnUnsafeStatement,                                       //Unsafe语句
            ["11118"] = LearnStatements.LearnFixedStatement,                                        //Fixed语句
            ["11201"] = LearnMethod.StartLearnMethod,                                               //方法
            ["11301"] = LearnLocalFunction.StartLearnLocalFunction,                                 //本地函数
            ["11401"] = LearnExtensionMethod.StartLearnExtensionMethod,                             //扩展方法
            ["11501"] = LearnVariables.LearnStaticVariables,                                        //静态变量
            ["11502"] = LearnVariables.LearnInstanceVariables,                                      //实例变量
            ["11503"] = LearnVariables.LearnArrayElements,                                          //数组元素
            ["11504"] = LearnVariables.LearnValueParameters,                                        //值参数
            ["11505"] = LearnVariables.LearnReferenceParameters,                                    //引用参数
            ["11506"] = LearnVariables.LearnOutputParameters,                                       //输出参数
            ["11507"] = LearnVariables.LearnLocalVariable,                                          //本地变量
            ["11601"] = LearnParameter.LearnValueParameter,                                         //值参数
            ["11602"] = LearnParameter.LearnReferenceParameter,                                     //引用参数
            ["11603"] = LearnParameter.LearnOutputParameter,                                        //输出参数
            ["11604"] = LearnParameter.LearnInParameter,                                            //只读参数
            ["11605"] = LearnParameter.LearnParameterArray,                                         //参数数组
            ["11606"] = LearnParameter.LearnDefaultParameter,                                       //默认参数
            ["11607"] = LearnParameter.LearnThisParameter,                                          //this参数
            ["11608"] = LearnParameter.LearnNamelyArgument,                                         //具名参数
            ["11701"] = LearnNamespace.StartLearnNamespace,                                         //命名空间
            ["11801"] = LearnClass.StartLearnClass,                                                 //类
            ["11901"] = LearnExpressionBodied.StartLearnExpressionBodied,                           //表达式主体定义
            ["12001"] = LearnInheritanceAndPolymorphism.StartLearnInheritanceAndPolymorphism,       //继承与多态
            ["12101"] = LearnInterface.StartLearnInterface,                                         //接口
            ["12201"] = LearnStruct.StartLearnStruct,                                               //结构
            ["12301"] = LearnRecord.StartLearnRecord,                                               //记录
            ["12401"] = LearnAnonymousType.StartLearnAnonymousType,                                 //匿名类型
            ["12601"] = LearnPattern.LearnConstantPattern,                                          //模式匹配：常量模式
            ["12602"] = LearnPattern.LearnRelationalPattern,                                        //模式匹配：关系模式
            ["12603"] = LearnPattern.LearnLogicalPattern,                                           //模式匹配：逻辑模式
            ["12604"] = LearnPattern.LearnDeclarationPattern,                                       //模式匹配：声明模式
            ["12605"] = LearnPattern.LearnTypePattern,                                              //模式匹配：类型模式
            ["12606"] = LearnPattern.LearnPropertyPattern,                                          //模式匹配：属性模式
            ["12607"] = LearnPattern.LearnPositionalPattern,                                        //模式匹配：位置模式
            ["12608"] = LearnPattern.LearnVarPattern,                                               //模式匹配：var模式
            ["12609"] = LearnPattern.LearnDiscardPattern,                                           //模式匹配：弃元
            ["12610"] = LearnPattern.LearnParenthesizedPattern,                                     //模式匹配：括号模式
            ["12611"] = LearnPattern.LearnListPattern,                                              //模式匹配：列表模式
            ["12612"] = LearnPattern.LearnSlicePattern,                                             //模式匹配：切片模式
            ["20101"] = LearnDelegate.LearnSystemDelegate,                                          //系统委托
            ["20102"] = LearnDelegate.LearnCustomerDelegate,                                        //自定义委托
            ["20103"] = LearnDelegate.LearnMultiDelegate,                                           //多播委托
            ["20201"] = LearnAnonymousFunction.LearnAnonymousMethods,                               //匿名方法
            ["20202"] = LearnAnonymousFunction.LearnStatement_Lambda,                               //语句Lambda表达式
            ["20203"] = LearnAnonymousFunction.LearnExpression_Lambda,                              //表达式Lambda表达式
            ["20204"] = LearnAnonymousFunction.LearnLambdaNotCatchOuterVariable,                    //Lambda表达式不捕获外部变量
            ["20205"] = LearnAnonymousFunction.LearnLambdaCatchOuterVariable,                       //Lambda表达式捕获外部变量
            ["20206"] = LearnAnonymousFunction.LearnLambdaCatchOuterForeachLoopVariable,            //Lambda表达式捕获foreach循环变量
            ["20207"] = LearnAnonymousFunction.LearnLambdaCatchOuterForLoopVariable,                //Lambda表达式捕获for循环变量
            ["20301"] = LearnEvent.StartLearnEvent,                                                 //事件
            ["20401"] = LearnCollection.LearnSystemCollection,                                      //系统集合
            ["20402"] = LearnCollection.LearnCustomerCollection,                                    //自定义集合
            ["20501"] = LearnGenericType.StartLearnGenericType,                                     //泛型
            ["20601"] = LearnLinq.LearnLINQQuerySyntax,                                             //LINQ查询语法
            ["20602"] = LearnLinq.LearnLINQMethodSyntax,                                            //LINQ方法语法
            ["21001"] = LearnProcessAndThread.LearnProcess,                                         //进程
            ["21002"] = LearnProcessAndThread.LearnThread,                                          //线程
            ["21003"] = LearnProcessAndThread.LearnThreadWithParams,                                //线程参数
            ["21004"] = LearnProcessAndThread.LearnLockAndMonitor,                                  //锁和监视器
            ["21005"] = LearnProcessAndThread.LearnMutex,                                           //互斥体
            ["21006"] = LearnProcessAndThread.LearnSemaphore,                                       //信号量
            ["21007"] = LearnProcessAndThread.LearnReaderWriterLock,                                //读写锁
            ["21008"] = LearnProcessAndThread.LearnAutoResetEvent,                                  //自动重置事件
            ["21009"] = LearnProcessAndThread.LearnManualResetEvent,                                //手动重置事件
            ["21010"] = LearnProcessAndThread.LearnCountdownEvent,                                  //倒计时事件
            ["21011"] = LearnProcessAndThread.LearnBarrier,                                         //屏障
            ["21012"] = LearnProcessAndThread.LearnThreadPool,                                      //线程池
            ["21013"] = LearnProcessAndThread.LearnCancellationToken,                               //取消令牌
            ["21101"] = LearnAsyncProgramming.LearnAsyncDelegate,                                   //AMP模式：基于AsyncResult的异步模式
            ["21102"] = LearnAsyncProgramming.LearnAsyncByEvent,                                    //EAP模式：基于事件的异步模式
            ["21103"] = LearnAsyncProgramming.LearnTask,                                            //Task
            ["21104"] = LearnAsyncProgramming.LearnAsyncMethod,                                     //异步方法
            ["21105"] = LearnAsyncProgramming.LearnSynchronizationContext,                          //同步上下文
            ["21106"] = LearnAsyncProgramming.LearnConfigureAwait,                                  //ConfigureAwait
            ["21107"] = LearnAsyncProgramming.LearnAsyncVoid,                                       //异步void方法
            ["21108"] = LearnAsyncProgramming.LearnMultiTask,                                       //多任务
            ["21109"] = LearnAsyncProgramming.LearnCancelTask,                                      //取消任务
            ["21110"] = LearnAsyncProgramming.LearnWhenAllWhenAnyAndContinueWith,                   //WhenAll、WhenAny和ContinueWith
            ["21111"] = LearnAsyncProgramming.LearnTimer,                                           //计时器
            ["21112"] = LearnAsyncProgramming.LearnChannel,                                         //通道
            ["21201"] = LearnParallelProgramming.LearnParallelForAndForEach,                        //并行迭代
            ["21202"] = LearnParallelProgramming.LearnParallelInvoke,                               //并行调用
            ["30101"] = LearnSingleton.LearnSingletonByEager,                                       //单例模式-饿汉式
            ["30102"] = LearnSingleton.LearnSingletonByEagerByMultiThread,                          //单例模式-饿汉式
            ["30103"] = LearnSingleton.LearnSingletonByLazyUnsafe,                                  //单例模式-懒汉式
            ["30104"] = LearnSingleton.LearnSingletonByLazySingleCheck,                             //单例模式-懒汉式
            ["30105"] = LearnSingleton.LearnSingletonByLazyDoubleCheck,                             //单例模式-懒汉式
            ["30106"] = LearnSingleton.LearnSingletonByInnerStaitcClass,                            //单例模式-内部静态类
            ["30107"] = LearnSingleton.LearnSingletonByInnerStaitcClassByMultiThread,               //单例模式-内部静态类
            ["30108"] = LearnSingleton.LearnSingletonByDotNetLazy,                                  //单例模式-使用Lazy<T>
            ["30109"] = LearnSingleton.LearnSingletonByDotNetLazyByMultiThread,                     //单例模式-使用Lazy<T>
            ["00000"] = Test.TestFunc                                                               //测试函数
        };

        public static void DirectNavigate(string code)
        {
            if (!string.IsNullOrWhiteSpace(code) && funcDic.TryGetValue(code, out Action? action)) 
                action!.Invoke();
            else
            {
                Console.WriteLine(@"【错误】输入的代码不存在或不合法，请重新输入！");
                Console.WriteLine(@"【提示】如需查看目录请输入“menu”；如需退出请输入“exit”;如需查看对应方法运行请输入对应代码");
                Console.WriteLine();
            }
        }
    }
}
