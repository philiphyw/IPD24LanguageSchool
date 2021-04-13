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
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //CoursEditDialog ced = new CoursEditDialog();
            //ced.Owner = this;
            //ced.ShowDialog();
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
