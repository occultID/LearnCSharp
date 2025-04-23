using System.Text;
using System.Windows;
using LearnWPF.Pages;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearnWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.rbtCSharp.Checked+=(sender,e) =>
            {
                //this.frm.Source = new Uri("pack://application:,,,/LearnCSharp;component/Pages/PageCSharp.xaml", UriKind.Absolute);
                this.frm.Navigate(new PageCSharp());
            };
            this.rbtCSharp.Unchecked += (sender, e) =>
            {
                this.frm.Navigate(null);
            };
        }
    }
}