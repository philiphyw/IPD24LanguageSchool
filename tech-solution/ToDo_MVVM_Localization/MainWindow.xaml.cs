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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoMVVM.Library;

namespace ToDo_MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToDoViewModel todovmInstance;
        public MainWindow()
        {
            CultureInfo culture = new CultureInfo(ConfigurationManager.AppSettings["DefaultCulture"]);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            InitializeComponent();

            switch (ConfigurationManager.AppSettings["DefaultCulture"])
            {
                case "en":
                    rbEnglish.IsChecked = true;
                    break;
                case "zh-Hans":
                    rbChinese.IsChecked = true;
                    break;
            }
            
            //comboStatus.ItemsSource = Enum.GetValues(typeof(STATUS)).Cast<STATUS>();
            comboStatus.SelectedIndex = 0;

            todovmInstance = new ToDoViewModel();
            this.DataContext = todovmInstance;
        }

        private void rbEnglish_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("DefaultCulture");
            config.AppSettings.Settings.Add("DefaultCulture", "en");

            config.Save(ConfigurationSaveMode.Modified);
        }

        private void rbChinese_Checked(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("DefaultCulture");
            config.AppSettings.Settings.Add("DefaultCulture", "zh-Hans");

            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
