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
using System.Windows.Shapes;
using YZYLibrary;

namespace YZYStudentGUI
{
    /// <summary>
    /// Interaction logic for RegisterDialog.xaml
    /// </summary>
    public partial class RegisterDialog : Window
    {
        private StudentRegisterViewModel regvmInstance;
        public RegisterDialog()
        {
            InitializeComponent();
            regvmInstance = new StudentRegisterViewModel();
            this.DataContext = regvmInstance;
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
