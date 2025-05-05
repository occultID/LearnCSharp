/*【311：结构型————外观模式】
 * 外观模式（Facade Pattern）是一个结构型设计模式，它为复杂的子系统提供一个简单的接口。
 * 外观模式隐藏了系统的复杂性，并向客户端提供了一个简单的接口，使得客户端可以更容易地使用子系统。
 * 外观模式通常用于以下场景：
 *  1. 当你想为一个复杂的子系统提供一个简单的接口时。
 *  2. 当你想将一个复杂的子系统与客户端解耦时。
 *  3. 当你想简化客户端的使用时。
 *  4. 当你想隐藏系统的复杂性时。
 *  5. ……
 * 外观模式的主要组成部分包括：
 *  1. 外观类（Facade）：提供一个简单的接口，隐藏了系统的复杂性。
 *  2. 子系统类（Subsystem）：实现了系统的具体功能。
 *  3. 客户端（Client）：使用外观类来访问子系统。
 *  4. 适配器类（Adapter）：将子系统的接口转换为客户端所期望的接口。
 */
using LearnCSharp.DesignPattern.LearnFacadeSpace;
using System.Drawing;

namespace LearnCSharp.DesignPattern
{
    internal class LearnFacade
    {
        /*【31101：基础外观模式】*/
        public static void LearnBasicFacadePattern()
        {
            Console.WriteLine("\n------示例：基础外观模式------\n");
            Console.WriteLine("》》》通过外观模式创建智能家居系统《《《");
            Console.WriteLine("-----------------------------------------------");
            
            // 创建一个智能家居外观类
            SmartHomeFacade smartHome = new SmartHomeFacade();

            // 客户端使用外观类来访问子系统
            smartHome.LeaveHome();  // 离家
            Console.WriteLine();
            smartHome.ArriveHome(); // 到家

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31102：可配置外观模式】*/
        public static void LearnConfigFacadePattern()
        {
            Console.WriteLine("\n------示例：可配置外观模式------\n");
            Console.WriteLine("》》》通过外观模式创建智能家居系统《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建一个智能家居配置类
            SmartHomeConfig config = new SmartHomeConfig(Color.LightBlue, DateTime.Now.AddHours(8), true);
            SmartHomeConfigFacade smartHome = new SmartHomeConfigFacade(config);
            
            // 客户端使用外观类来访问子系统
            smartHome.LeaveHome();  // 离家
            Console.WriteLine();
            smartHome.ArriveHome(); // 到家

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }

        /*【31103：分层外观模式】*/
        public static void LearnLayeredFacadePattern()
        {
            Console.WriteLine("\n------示例：分层外观模式------\n");
            Console.WriteLine("》》》通过外观模式创建智能家居系统《《《");
            Console.WriteLine("-----------------------------------------------");

            // 创建一个智能家居顶层外观类
            SmartHomeControlFacade smartHome = new SmartHomeControlFacade();

            // 客户端使用外观类来访问子系统
            smartHome.LeaveHome();  // 离家
            Console.WriteLine();
            smartHome.ArriveHome(); // 到家

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();
        }
    }
}

namespace LearnCSharp.DesignPattern.LearnFacadeSpace
{
    // 外观模式
    // 外观模式是一种结构型设计模式，它为复杂的子系统提供一个简单的接口。
    // 外观模式通常用于以下场景：
    // 1. 当你想为一个复杂的子系统提供一个简单的接口时。
    // 2. 当你想将一个复杂的子系统与客户端解耦时。
    // 3. 当你想简化客户端的使用时。
    // 4. 当你想隐藏系统的复杂性时。

    //---------------------------------

    #region 基础外观模式
    /*【31101：基础外观模式】*
     * 外观模式的基础实现
     * 特点：隐藏子系统的复杂性，提供一个简单的接口
     *      客户端只需要了解外观类的接口，而不需要了解子系统的实现细节
     *      子系统的实现可以独立地变化，而不影响客户端
     */
    public partial class LightSystem //灯光系统
    {
        public void TurnOn()
        {
            Console.WriteLine("灯光已经打开");
        }
        public void TurnOff()
        {
            Console.WriteLine("灯光已经关闭");
        }
    }

    public partial class ClimateSystem //温控系统
    {
        public void SetConfortMode()
        {
            Console.WriteLine("空调已设置为舒适模式");
        }

        public void SetAwayMode()
        {
            Console.WriteLine("空调已设置为离家模式");
        }
    }

    public partial class SecuritySystem //安防系统
    {
        public void ArmSystem()
        {
            Console.WriteLine("安防系统已启动");
        }

        public void DisarmSystem()
        {
            Console.WriteLine("安防系统已关闭");
        }
    }

    public class SmartHomeFacade //智能家居外观类
    {
        private LightSystem lightSystem;
        private ClimateSystem climateSystem;
        private SecuritySystem securitySystem;

        public SmartHomeFacade()
        {
            lightSystem = new LightSystem();
            climateSystem = new ClimateSystem();
            securitySystem = new SecuritySystem();
        }

        public void ArriveHome()
        {
            lightSystem.TurnOn();
            climateSystem.SetConfortMode();
            securitySystem.DisarmSystem();

            Console.WriteLine("欢迎回家！");
        }

        public void LeaveHome()
        {
            lightSystem.TurnOff();
            climateSystem.SetAwayMode();
            securitySystem.ArmSystem();

            Console.WriteLine("再见，祝你有美好的一天！");
        }
    }
    #endregion

    #region 可配置外观模式
    /*【31102：可配置外观模式】*
     * 外观模式的可配置实现
     * 特点：允许客户端在运行时配置子系统的行为
     *      客户端可以根据需要选择不同的子系统实现
     */
    public record SmartHomeConfig(Color LightColor,DateTime AutoOpenWaterHeaterTime,bool IsKeepSecurity);//智能家居配置类

    public partial class LightSystem //灯光系统
    {
        public void TurnOn(Color color)
        {
            Console.WriteLine($"灯光已经打开，颜色为：{color}");
        }
    }

    public partial class ClimateSystem //温控系统
    {
        public void OpenWaterHeater(DateTime time)
        {
            Console.WriteLine($"热水器将在{time}自动打开");
        }
    }

    public partial class SecuritySystem //安防系统
    {
        public void SetKeepSecurity(bool isKeep)
        {
            if (isKeep)
            {
                Console.WriteLine("安防系统持续运行");
            }
            else
            {
                Console.WriteLine("安防系统自动启停");
            }
        }
    }

    public class SmartHomeConfigFacade //智能家居配置外观类
    {
        private LightSystem lightSystem;
        private ClimateSystem climateSystem;
        private SecuritySystem securitySystem;
        private SmartHomeConfig config;

        public SmartHomeConfigFacade(SmartHomeConfig config)
        {
            lightSystem = new LightSystem();
            climateSystem = new ClimateSystem();
            securitySystem = new SecuritySystem();
            this.config = config;
        }

        public void LeaveHome()
        {
            lightSystem.TurnOff();
            climateSystem.SetAwayMode();
            climateSystem.OpenWaterHeater(config.AutoOpenWaterHeaterTime);
            if(config.IsKeepSecurity)
            {
                securitySystem.SetKeepSecurity(config.IsKeepSecurity);
            }
            else
            {
                securitySystem.ArmSystem();
            }
            Console.WriteLine("再见，祝你有美好的一天！");
        }

        public void ArriveHome()
        {
            lightSystem.TurnOn(config.LightColor);
            climateSystem.SetConfortMode();
            if (config.IsKeepSecurity)
            {
                securitySystem.SetKeepSecurity(config.IsKeepSecurity);
            }
            else
            {
                securitySystem.DisarmSystem();
            }
            Console.WriteLine("欢迎回家！");
        }
    }
    #endregion

    #region 分层外观模式
    /*【31103：分层外观模式】*
     * 外观模式的分层实现
     * 特点：将外观类分为多个层次，每个层次负责不同的子系统
     *      客户端可以根据需要选择不同的外观类
     */
    public class BedroomLightSystem //卧室灯光系统
    {
        public void TurnOn()
        {
            Console.WriteLine("卧室灯光已经打开");
        }
        public void TurnOff()
        {
            Console.WriteLine("卧室灯光已经关闭");
        }
    }

    public class LivingRoomLightSystem //客厅灯光系统
    {
        public void TurnOn()
        {
            Console.WriteLine("客厅灯光已经打开");
        }
        public void TurnOff()
        {
            Console.WriteLine("客厅灯光已经关闭");
        }
    }

    public class BathroomLightSystem //卫生间灯光系统
    {
        public void TurnOn()
        {
            Console.WriteLine("卫生间灯光已经打开");
        }
        public void TurnOff()
        {
            Console.WriteLine("卫生间灯光已经关闭");
        }
    }

    public class KitchenLightSystem //厨房灯光系统
    {
        public void TurnOn()
        {
            Console.WriteLine("厨房灯光已经打开");
        }
        public void TurnOff()
        {
            Console.WriteLine("厨房灯光已经关闭");
        }
    }

    public class LightSystemFacade //灯光系统子外观类
    {
        private BedroomLightSystem bedroomLightSystem;
        private LivingRoomLightSystem livingRoomLightSystem;
        private BathroomLightSystem bathroomLightSystem;
        private KitchenLightSystem kitchenLightSystem;
        public LightSystemFacade()
        {
            bedroomLightSystem = new BedroomLightSystem();
            livingRoomLightSystem = new LivingRoomLightSystem();
            bathroomLightSystem = new BathroomLightSystem();
            kitchenLightSystem = new KitchenLightSystem();
        }
        public void TurnOnAllLights()
        {
            bedroomLightSystem.TurnOn();
            livingRoomLightSystem.TurnOn();
            bathroomLightSystem.TurnOn();
            kitchenLightSystem.TurnOn();
        }
        public void TurnOffAllLights()
        {
            bedroomLightSystem.TurnOff();
            livingRoomLightSystem.TurnOff();
            bathroomLightSystem.TurnOff();
            kitchenLightSystem.TurnOff();
        }
    }

    public class SmartHomeControlFacade //智能家居顶层外观类
    {
        private LightSystemFacade lightSystemFacade;

        public SmartHomeControlFacade()
        {
            lightSystemFacade = new LightSystemFacade();
        }
        public void ArriveHome()
        {
            lightSystemFacade.TurnOnAllLights();
            Console.WriteLine("欢迎回家！");
        }
        public void LeaveHome()
        {
            lightSystemFacade.TurnOffAllLights();
            Console.WriteLine("再见，祝你有美好的一天！");
        }
    }
    #endregion
}