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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YZYLibrary;

namespace DotnetProject_YZY
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
            //lvUsers.ItemsSource = ctx.Users.OrderBy(u => u.FName).ToList();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }     
        
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        CoursEditDialog ced = new CoursEditDialog();

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            CoursEditDialog ced = new CoursEditDialog();
            ced.Owner = this;
            ced.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudentDialog sd = new StudentDialog();
            sd.Owner = this;
            sd.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ResgiterDialog rd = new ResgiterDialog();
            rd.Owner = this;
            rd.ShowDialog();
        }
    }
}
