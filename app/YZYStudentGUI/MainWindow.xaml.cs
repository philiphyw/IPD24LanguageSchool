using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace YZYStudentGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //TODO: language has to be set here before initialize window
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();

            switch (ConfigurationManager.AppSettings["DefaultCulture"])
            {
                case "zh-Hans":
                    LanguageToggle.IsChecked = true;
                    break;
                case "en":
                    LanguageToggle.IsChecked = false;
                    break;
            }

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
        private void Search_CourseByCategory_Click(object sender, RoutedEventArgs e)
        {
            StudentSearchCoursViewModel regvmInstance = new StudentSearchCoursViewModel();
            this.DataContext = regvmInstance;
            regvmInstance.SelectedCategoryID = comboSearchCourse.SelectedIndex;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("DefaultCulture");
            if (LanguageToggle.IsChecked == true)
            {
                config.AppSettings.Settings.Add("DefaultCulture", "zh-Hans");// TODO: to add selected language string replacing "FIXME"
            }
            else
            {
                config.AppSettings.Settings.Add("DefaultCulture", "en");// TODO: to add selected language string replacing "FIXME"
            }
            config.Save(ConfigurationSaveMode.Modified);

        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterDialog rd = new RegisterDialog();
            rd.Owner = this;
            if(rd.ShowDialog() == true)
            {
                MessageBox.Show("Hello, Succuess!", "My App");
            }



        }

        private void Button_Search_Tearch_click(object sender, RoutedEventArgs e)
        {
            StudentSearchTeacherViewModel sstvmInstance = new StudentSearchTeacherViewModel();
            this.DataContext = sstvmInstance;
        }

        private void Button_Search_Course_Click(object sender, RoutedEventArgs e)
        {
            StudentSearchCoursViewModel regvmInstance = new StudentSearchCoursViewModel();
            this.DataContext = regvmInstance;
        }

        private void Button_Sign_Click(object sender, RoutedEventArgs e)
        {
            var loginDlg = new SignInDialog();
            loginDlg.Owner = this;
            if (loginDlg.ShowDialog() == true)
            {
                string email = loginDlg.tbEmail.Text;
                string password = loginDlg.tbPassword.Text;
                try
                {
                    YZYDbContext ctx = new YZYDbContext();
                    User loginUser = ctx.Users.ToList().Where(u => (u.Email == email && u.Password == password)).FirstOrDefault();
                    if (loginUser != null)
                    {
                        if (loginUser.UserRole == UserRoleEnum.Student)
                        {
                            GlobalSettings.userRole = UserRoleEnum.Student;
                            GlobalSettings.userID = loginUser.UserID;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                catch (SystemException ex)
                {
                    Log.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
            }
        }

        private void ButtonPayment_Click(object sender, RoutedEventArgs e)
        {
            PaymentManagementDialog dlg = new PaymentManagementDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
        }
    }

    public class GlobalSettings
    {
        public static UserRoleEnum userRole;
        public static int userID;
    }
}
