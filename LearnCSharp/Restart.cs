using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnCSharp
{
    internal class ReStart
    {
        public static void ReStartConsole(int minCode = 0, int maxCode = 0, int code = int.MaxValue)
        {
            var executablePath = Environment.ProcessPath!;
            string args;

            if (code >= minCode && code <= maxCode)
                args = $"{code}";
            else
                args = null;

            // 准备新进程启动参数
            var startInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = args,
                UseShellExecute = true,   // 允许创建新控制台窗口
                WorkingDirectory = Environment.CurrentDirectory
            };

            try
            {
                // 启动新进程
                Process.Start(startInfo);

                // 关闭当前进程（延迟100ms确保新进程启动）
                Thread.Sleep(100);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"重启失败: {ex.Message}");
            }
        }
    }
}
