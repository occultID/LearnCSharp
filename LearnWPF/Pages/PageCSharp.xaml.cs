using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearnWPF.Pages
{
    /// <summary>
    /// PageCSharp.xaml 的交互逻辑
    /// </summary>
    public partial class PageCSharp : Page
    {
        public PageCSharp()
        {
            InitializeComponent();

            ProcessStartInfo GetPsi(string arg) => new ProcessStartInfo
            {
                FileName = "LearnCSharp.exe",
                Arguments = arg,
                UseShellExecute = true,
                RedirectStandardOutput = false,
                CreateNoWindow = true
            };

            this.btnStartConsole.Click += async(sender, e) =>
            {
                this.btnStartConsole.Background = Brushes.BurlyWood;
                this.btnStartConsole.Content = "正在执行";
                this.btnStartConsole.IsEnabled = false;

                await Task.Run(() =>
                {
                    using (Process process = Process.Start(GetPsi(null)!)!)
                    {
                        process.WaitForExit();
                    }
                });

                this.btnStartConsole.Background = Brushes.LightBlue;
                this.btnStartConsole.Content = "开始执行";
                this.btnStartConsole.IsEnabled = true;
            };
        }
    }
}
