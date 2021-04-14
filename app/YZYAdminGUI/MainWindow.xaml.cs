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

namespace YZYAdminGUI
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

        private void Button_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        private void LanguageToggle_Click(object sender, RoutedEventArgs e)
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
    }
}
