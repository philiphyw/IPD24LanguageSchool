using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YZYLibrary;

namespace YZY
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        YZYDbContext ctx;   
        public MainWindow()
        {
            InitializeComponent();
            ctx = new YZYDbContext();
            lvUsers.ItemsSource = ctx.Users.OrderBy(u => u.FName).ToList();
        }
    }
}
