using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

namespace LearnWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex mutex;

        // 引入Win32 API
        [LibraryImport("user32.dll")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool SetForegroundWindow(IntPtr hWnd);

        [LibraryImport("user32.dll")]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvStdcall) })]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_RESTORE = 9;
        private const string mutexName = @"Global\LearnWPF"; // 互斥体名称

        protected override void OnStartup(StartupEventArgs e)
        {
            bool isNewInstance;
            mutex = new Mutex(true, mutexName, out isNewInstance);

            if (!isNewInstance)
            {
                // 查找并激活已有实例窗口
                ActivateExistingWindow();
                Shutdown();
                return;
            }

            base.OnStartup(e);
            //new MainWindow().Show();
        }

        private void ActivateExistingWindow()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if (process.Id == currentProcess.Id) continue;

                IntPtr handle = process.MainWindowHandle;
                if (handle == IntPtr.Zero) continue;

                // 恢复窗口并前置
                ShowWindow(handle, SW_RESTORE);
                SetForegroundWindow(handle);
                break;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            mutex?.Dispose();
            base.OnExit(e);
        }
    }

}
