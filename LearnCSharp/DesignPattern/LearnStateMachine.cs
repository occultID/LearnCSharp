/*【319：行为型————状态机模式】
 * 状态机模式（State Machine Pattern）是一种行为型设计模式，它允许对象在不同的状态下表现出不同的行为。
 * 状态机模式可以用来实现多种状态机的设计，包括状态机、状态转换图、状态转换条件等。
 * 状态机模式的主要优点是可以让代码易于理解和维护，它将状态的转换逻辑与状态的行为分离开来，使得代码更加容易维护。
 * 状态机模式的主要缺点是实现起来比较复杂，需要定义状态机的各个状态、状态之间的转换条件、状态的行为等。
 * 状态机模式适用于以下场景：
 * 1、行为随状态改变而改变的场景。
 * 2、复杂的状态转换逻辑的场景。
 * 3、需要实现状态机的场景。
 * 4、需要对状态机进行扩展的场景。
 * 5、需要实现状态机的监视、调试、跟踪等功能的场景。
 * 6、需要实现状态机的并发控制的场景。
 * 7、需要实现状态机的分布式控制的场景。
 * 8、……
 * 状态机模式的主要组成部分：
 * 1、Context：上下文，表示当前的状态机。
 * 2、State：状态，表示状态机的状态。
 * 3、Event：事件，表示状态机的事件。
 * 4、Transition：转换，表示状态机的状态转换。
 * 5、Action：行为，表示状态机的状态行为。
 * 6、Guard：条件，表示状态机的状态转换条件。
 * 7、State Machine：状态机，表示状态机的实现。
 * 状态机模式的实现步骤： 
 * 1、定义状态机的各个状态。
 * 2、定义状态机的事件。
 * 3、定义状态机的状态转换条件。
 * 4、定义状态机的状态转换。
 * 5、定义状态机的状态行为。
 * 6、实现状态机的实现。
 */

using LearnCSharp.DesignPattern.LearnStateMachineSpace;


namespace LearnCSharp.DesignPattern
{
    internal class LearnStateMachine
    {
        /*【31901：状态机模式示例】*/
        public static void LearnStateMachineDesignPattern()
        {
            Console.WriteLine("\n------示例：状态机模式------\n");
            Console.WriteLine("》》》通过状态机模式模拟玩家状态机《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建状态机
            Console.WriteLine("创建玩家状态机，初始状态为：停止");
            var playerStateMachine = new PlayerStateMachine(new StoppedState());
            playerStateMachine.Update(); // 更新状态

            // 处理输入并更新状态
            Console.WriteLine("模拟玩家输入：奔跑");
            playerStateMachine.HandleInput(Input.Run); // 处理输入
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("模拟玩家输入：跳跃");
            playerStateMachine.HandleInput(Input.Jump); // 处理输入
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("模拟玩家输入：攻击");
            playerStateMachine.HandleInput(Input.Attack);// 处理输入
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("模拟玩家输入：停止");
            playerStateMachine.HandleInput(Input.Stop); // 处理输入
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31902：状态表驱动状态机】*/
        public static void LearnStateTableDrivenStateMachine()
        {
            Console.WriteLine("\n------状态表驱动状态机------\n");
            Console.WriteLine("》》》通过状态表驱动状态机模拟玩家状态机《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建状态表配置
            var config = new StateTableConfig();
            config.Initialize();

            // 创建状态机
            var playerStateMachine = new TableDrivenPlayerStateMachine(PlayerState.Stopped, config);
            playerStateMachine.Update(); // 更新状态

            // 处理输入并更新状态
            Console.WriteLine("模拟玩家输入：奔跑");
            playerStateMachine.HandleInput(InputEvent.Run); // 处理输入：转换到奔跑状态
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("模拟玩家输入：跳跃");
            playerStateMachine.HandleInput(InputEvent.Jump); // 处理输入：转换到跳跃状态
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("模拟玩家输入：攻击");
            playerStateMachine.HandleInput(InputEvent.Attack); // 处理输入：转换到攻击状态
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("模拟玩家输入：停止");
            playerStateMachine.HandleInput(InputEvent.Stop); // 处理输入：转换到停止状态
            playerStateMachine.Update(); // 更新状态
            Thread.Sleep(1500);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnStateMachineSpace
{
    #region 状态机模式结构
    /*【31900：状态机模式结构】*/
    public interface IState // 状态接口
    {
        void Handle(Context context); // 处理事件
    }

    public class StartState:IState // 开始状态
    {
        public void Handle(Context context)
        {
            Console.WriteLine("处于开始状态");
            context.State = new StopState(); // 转换到停止状态
        }
    }

    public class StopState:IState // 停止状态
    {
        public void Handle(Context context)
        {
            Console.WriteLine("处于停止状态");
            context.State = new StartState(); // 转换到开始状态
        }
    }

    public class Context
    {
        private IState state; // 当前状态

        public IState State
        {
            get => state;
            set
            {
                state = value;
                Console.WriteLine($"状态转换：{state.GetType().Name}");
            }
        }

        public Context(IState state)
        {
            this.state = state;
        }

        public void Request()
        {
            state.Handle(this); // 处理事件
        }
    }

    public class Client // 客户端
    {
        public void TestStateMachine()
        {
            Context context = new Context(new StartState());// 实例化上下文，设置初始状态为开始状态
            context.Request(); // 请求状态切换：切换到停止状态
            context.Request(); // 请求状态切换：切换到开始状态
        }
    }
    #endregion

    #region 状态机模式实现
    /*【31901：基本状态机模式示例】*/
    public enum Input
    {
        Run,
        Stop,
        Jump,
        Attack
    }

    public interface IPlayerState // 玩家状态接口
    {
        IPlayerState? Update(PlayerStateMachine stateMachine); // 更新状态
        IPlayerState? HandleInput(PlayerStateMachine stateMachine, Input input); // 处理输入
    }

    public class RunningState : IPlayerState // 奔跑状态
    {
        public IPlayerState? HandleInput(PlayerStateMachine stateMachine, Input input) // 处理输入
        {
            return input switch
            {
                Input.Run => null, // 保持当前状态
                Input.Stop => new StoppedState(), // 转换到停止状态
                Input.Jump => new JumpingState(), // 转换到跳跃状态
                Input.Attack => new AttackingState(), // 转换到攻击状态
                _ => null
            };
        }

        public IPlayerState? Update(PlayerStateMachine stateMachine) // 更新状态
        {
            Console.WriteLine("更新奔跑状态");
            return null; // 保持当前状态
        }
    }

    public class StoppedState : IPlayerState // 停止状态
    {
        public IPlayerState? HandleInput(PlayerStateMachine stateMachine, Input input)
        {
            return input switch
            {
                Input.Run => new RunningState(), // 转换到奔跑状态
                Input.Stop => null, // 保持当前状态
                Input.Jump => new JumpingState(), // 转换到跳跃状态
                Input.Attack => new AttackingState(), // 转换到攻击状态
                _ => null
            };
        }

        public IPlayerState? Update(PlayerStateMachine stateMachine)
        {
            Console.WriteLine("更新停止状态");
            return null; // 保持当前状态
        }
    }

    public class JumpingState : IPlayerState // 跳跃状态
    {
        public IPlayerState? HandleInput(PlayerStateMachine stateMachine, Input input)
        {
            return input switch
            {
                Input.Run => new RunningState(), // 转换到奔跑状态
                Input.Stop => new StoppedState(), // 转换到停止状态
                Input.Jump => null, // 保持当前状态
                Input.Attack => new AttackingState(), // 转换到攻击状态
                _ => null
            };
        }

        public IPlayerState? Update(PlayerStateMachine stateMachine)
        {
            Console.WriteLine("更新跳跃状态");
            return null; // 保持当前状态
        }
    }

    public class AttackingState : IPlayerState // 攻击状态
    {
        public IPlayerState? HandleInput(PlayerStateMachine stateMachine, Input input)
        {
            return input switch
            {
                Input.Run => new RunningState(), // 转换到奔跑状态
                Input.Stop => new StoppedState(), // 转换到停止状态
                Input.Jump => new JumpingState(), // 转换到跳跃状态
                Input.Attack => null, // 保持当前状态
                _ => null
            };
        }

        public IPlayerState? Update(PlayerStateMachine stateMachine)
        {
            Console.WriteLine("更新攻击状态");
            return null; // 保持当前状态
        }
    }

    public class PlayerStateMachine // 状态机
    {
        private IPlayerState state; // 当前状态

        public IPlayerState State // 状态属性
        {
            get => state;
            set
            {
                state = value;
                Console.WriteLine($"状态转换：{value.GetType().Name}");
            }
        }

        public PlayerStateMachine(IPlayerState initialState) // 构造函数
        {
            this.state = initialState;
        }

        public void Update() // 更新状态
        {
            var newState = state.Update(this); // 更新状态
            if (newState!= null)
                state = newState;
        }

        public void HandleInput(Input input) // 处理输入
        {
            var newState = state.HandleInput(this, input); // 处理输入
            if (newState!= null)
                state = newState;
        }
    }
    #endregion

    #region 状态表驱动状态机
    /*【31902：状态表驱动状态机】
     * 状态表驱动状态机（Table-Driven State Machine，TDSM）是一种基于状态表的状态机实现方法。
     * 状态表驱动实现的核心思想是将状态转换规则集中管理，使用数据结构（如字典）明确定义状态转换关系，相比传统状态模式有以下优势：
        集中管理：所有转换规则一目了然
        易于维护：修改转换逻辑无需改动状态类
        数据驱动：可通过配置文件修改状态机行为
        减少类膨胀：避免为每个状态创建单独类
     */
    public enum PlayerState
    {
        Stopped,
        Running,
        Jumping,
        Attacking
    }

    public enum InputEvent
    {
        Run,
        Stop,
        Jump,
        Attack
    }

    public delegate void StateBehavior(TableDrivenPlayerStateMachine state); // 状态行为委托
    
    public class StateTableConfig // 状态表配置
    {
        public Dictionary<(PlayerState, InputEvent), PlayerState> Transitions { get; } =new Dictionary<(PlayerState, InputEvent), PlayerState>(); // 状态转换表 当前状态+输入事件 -> 下一状态

        public Dictionary<PlayerState, StateBehavior> Behaviors { get; } = new Dictionary<PlayerState, StateBehavior>(); // 状态行为表 当前状态 -> 状态行为

        public void Initialize() // 初始化状态表
        {
            Transitions[(PlayerState.Stopped, InputEvent.Run)] = PlayerState.Running;
            Transitions[(PlayerState.Stopped, InputEvent.Stop)] = PlayerState.Stopped;
            Transitions[(PlayerState.Stopped, InputEvent.Jump)] = PlayerState.Jumping;
            Transitions[(PlayerState.Stopped, InputEvent.Attack)] = PlayerState.Attacking;

            Transitions[(PlayerState.Running, InputEvent.Run)] = PlayerState.Running;
            Transitions[(PlayerState.Running, InputEvent.Stop)] = PlayerState.Stopped;
            Transitions[(PlayerState.Running, InputEvent.Jump)] = PlayerState.Jumping;
            Transitions[(PlayerState.Running, InputEvent.Attack)] = PlayerState.Attacking;

            Transitions[(PlayerState.Jumping, InputEvent.Run)] = PlayerState.Running;
            Transitions[(PlayerState.Jumping, InputEvent.Stop)] = PlayerState.Stopped;
            Transitions[(PlayerState.Jumping, InputEvent.Jump)] = PlayerState.Jumping;
            Transitions[(PlayerState.Jumping, InputEvent.Attack)] = PlayerState.Attacking;

            Transitions[(PlayerState.Attacking, InputEvent.Run)] = PlayerState.Running;
            Transitions[(PlayerState.Attacking, InputEvent.Stop)] = PlayerState.Stopped;
            Transitions[(PlayerState.Attacking, InputEvent.Jump)] = PlayerState.Jumping;
            Transitions[(PlayerState.Attacking, InputEvent.Attack)] = PlayerState.Attacking;

            Behaviors[PlayerState.Stopped] = stateMachine => Console.WriteLine("处于停止状态");
            Behaviors[PlayerState.Running] = stateMachine => Console.WriteLine("处于奔跑状态");
            Behaviors[PlayerState.Jumping] = stateMachine => Console.WriteLine("处于跳跃状态");
            Behaviors[PlayerState.Attacking] = stateMachine => Console.WriteLine("处于攻击状态");
        }
    }

    public class TableDrivenPlayerStateMachine // 状态机
    {
        private readonly StateTableConfig config; // 状态表配置

        public PlayerState CurrentState { get; private set; } // 当前状态

        public TableDrivenPlayerStateMachine(PlayerState initialState, StateTableConfig config)
        {
            this.config = config;
            CurrentState = initialState;
        }

        public void HandleInput(InputEvent input) // 处理输入
        {
            var key = (CurrentState, input); // 状态+输入

            if (config.Transitions.TryGetValue(key, out var nextState))
            {
                Console.WriteLine($"状态转换：{CurrentState} -> {nextState}");
                CurrentState = nextState; // 更新状态
            }
        }

        public void Update() // 更新状态
        {
            if (config.Behaviors.TryGetValue(CurrentState, out var behavior))
            {
                behavior.Invoke(this); // 执行状态行为
            }
        }
    }
    #endregion
}
