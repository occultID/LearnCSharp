/*【303：建造者模式】*/
using LearnCSharp.DesignPattern.LearnBuilderSpace;

namespace LearnCSharp.DesignPattern
{
    internal class LearnBuilder
    {
        /*【30301：基础建造者模式】*/
        public static void LearnBasicBuilder()
        {
            Console.WriteLine("\n------示例：基础构建者模式------\n");
            Console.WriteLine("》》》通过构建者模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            IComputerBuilder builder = new ComputerBuilder();
            ComputerDirector director = new ComputerDirector(builder);
            director.Construct("Intel i7", "16GB", "512GB SSD", "NVIDIA RTX 3060");
            Computer computer = builder.GetComputer();
            Console.WriteLine(computer); // 输出：Computer [CPU=Intel i7, RAM=16GB, Storage=512GB SSD, GPU=NVIDIA RTX 3060]

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30302：链式调用建造者模式】*/
        public static void LearnFluentBuilder()
        {
            Console.WriteLine("\n------示例：链式调用建造者模式------\n");
            Console.WriteLine("》》》通过链式调用构建者模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            FluentComputerBuilder builder = new FluentComputerBuilder();
            FluentComputerDirector director = new FluentComputerDirector(builder);
            Computer computer = director.Construct("AMD Ryzen 7", "32GB", "1TB SSD", "AMD Radeon RX 6800");
            Console.WriteLine(computer); // 输出：Computer [CPU=AMD Ryzen 7, RAM=32GB, Storage=1TB SSD, GPU=AMD Radeon RX 6800]

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【30303：带验证的链式调用建造者模式】*/
        public static void LearnValidatedFluentBuilder()
        {
            Console.WriteLine("\n------示例：带验证的链式调用建造者模式------\n");
            Console.WriteLine("》》》通过带验证的链式调用构建者模式创建对象《《《");
            Console.WriteLine("-----------------------------------------------");

            ValidatedComputerBuilder builder = new ValidatedComputerBuilder();
            ValidatedComputerDirector director = new ValidatedComputerDirector(builder);
            try
            {
                Computer computer = director.Construct("Intel i9", "64GB", "2TB SSD", "NVIDIA RTX 3090");
                Console.WriteLine(computer); // 输出：Computer [CPU=Intel i9, RAM=64GB, Storage=2TB SSD, GPU=NVIDIA RTX 3090]
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"构建失败: {ex.Message}");
            }

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnBuilderSpace
{
    // 建造者模式
    // 适用于需要创建复杂对象的场景
    // 通过分步骤构建对象，允许用户逐步设置对象的属性
    // 例如：构建一个复杂的HTML文档、构建一个复杂的数据库查询等

    //--------------------------------------------------------------------------------------

    #region 基础构造者模式
    /*【30301：基础建造者模式】
     * 特点：明确分离构造步骤
     *      支持逐步构建
     *      需要严格遵循构建顺序
     */
    public class Computer   //产品类
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string Storage { get; set; }
        public string GPU { get; set; }
        public override string ToString()
        {
            return $"Computer [CPU={CPU}, RAM={RAM}, Storage={Storage}, GPU={GPU}]";
        }
    }

    public interface IComputerBuilder  //建造者接口
    {
        void BuildCPU(string cpu);
        void BuildRAM(string ram);
        void BuildStorage(string storage);
        void BuildGPU(string gpu);
        Computer GetComputer();
    }

    public class ComputerBuilder : IComputerBuilder  //具体建造者
    {
        private Computer computer = new Computer();
        public void BuildCPU(string cpu)
        {
            computer.CPU = cpu;
        }
        public void BuildRAM(string ram)
        {
            computer.RAM = ram;
        }
        public void BuildStorage(string storage)
        {
            computer.Storage = storage;
        }
        public void BuildGPU(string gpu)
        {
            computer.GPU = gpu;
        }
        public Computer GetComputer()
        {
            return computer;
        }
    }

    public class ComputerDirector  //指挥者
    {
        private IComputerBuilder builder;
        public ComputerDirector(IComputerBuilder builder)
        {
            this.builder = builder;
        }
        public void Construct(string cpu, string ram, string storage, string gpu)
        {
            builder.BuildCPU(cpu);
            builder.BuildRAM(ram);
            builder.BuildStorage(storage);
            builder.BuildGPU(gpu);
        }
    }
    #endregion

    #region 链式调用建造者模式
    /*【30302：链式调用建造者模式】
     * 特点：使用链式调用来设置属性
     *      支持灵活的参数组合
     *      提高了代码的可读性
     *      允许用户以任意顺序设置属性
     *      适用于需要创建复杂对象的场景
     */
    public class FluentComputerBuilder  //流式建造者
    {
        private Computer computer = new Computer();
        public FluentComputerBuilder SetCPU(string cpu)
        {
            computer.CPU = cpu;
            return this;
        }
        public FluentComputerBuilder SetRAM(string ram)
        {
            computer.RAM = ram;
            return this;
        }
        public FluentComputerBuilder SetStorage(string storage)
        {
            computer.Storage = storage;
            return this;
        }
        public FluentComputerBuilder SetGPU(string gpu)
        {
            computer.GPU = gpu;
            return this;
        }
        public Computer Build()
        {
            return computer;
        }
    }

    public class FluentComputerDirector  //流式建造者指挥者
    {
        private FluentComputerBuilder builder;
        public FluentComputerDirector(FluentComputerBuilder builder)
        {
            this.builder = builder;
        }
        public Computer Construct(string cpu, string ram, string storage, string gpu)
        {
            return builder.SetCPU(cpu)
                          .SetRAM(ram)
                          .SetStorage(storage)
                          .SetGPU(gpu)
                          .Build();
        }
    }
    #endregion

    #region 带验证的链式调用建造者模式
    /*【30303：带验证的链式调用建造者模式】
     * 特点：在构建过程中进行参数验证
     *      提高了代码的可读性
     *      要求用户以规定顺序设置属性
     *      参数之间存在依赖关系
     *      确保最终产品的完整性
     *      适用于需要创建复杂对象的场景
     */
    public class ValidatedComputerBuilder  //带验证的流式建造者
    {
        private Computer computer = new Computer();
        private bool isCPUSet = false;
        private bool isRAMSet = false;
        private bool isStorageSet = false;
        private bool isGPUSet = false;
        public ValidatedComputerBuilder SetCPU(string cpu)
        {
            computer.CPU = cpu;
            isCPUSet = true;
            return this;
        }
        public ValidatedComputerBuilder SetRAM(string ram)
        {
            if (!isCPUSet)
                throw new InvalidOperationException("CPU 必须在建造 RAM 之前建造.");
            computer.RAM = ram;
            isRAMSet = true;
            return this;
        }
        public ValidatedComputerBuilder SetStorage(string storage)
        {
            if (!isRAMSet)
                throw new InvalidOperationException("RAM 必须在建造 Storage 之前建造.");
            computer.Storage = storage;
            isStorageSet = true;
            return this;
        }
        public ValidatedComputerBuilder SetGPU(string gpu)
        {
            if (!isStorageSet)
                throw new InvalidOperationException("Storage 必须在建造 GPU 之前建造.");
            computer.GPU = gpu;
            isGPUSet = true;
            return this;
        }
        public Computer Build()
        {
            if (!isGPUSet)
                throw new InvalidOperationException("必须建造所有组件才能建造完整 computer.");
            return computer;
        }
    }

    public class ValidatedComputerDirector  //带验证的流式建造者指挥者
    {
        private ValidatedComputerBuilder builder;
        public ValidatedComputerDirector(ValidatedComputerBuilder builder)
        {
            this.builder = builder;
        }
        public Computer Construct(string cpu, string ram, string storage, string gpu)
        {
            return builder.SetCPU(cpu)
                          .SetRAM(ram)
                          .SetStorage(storage)
                          .SetGPU(gpu)
                          .Build();
        }
    }
    #endregion
}
