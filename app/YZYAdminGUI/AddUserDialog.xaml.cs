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

namespace YZYAdminGUI
{
    /// <summary>
    /// Interaction logic for AddUserDialog.xaml
    /// </summary>
    public partial class AddUserDialog : Window
    {
        public AddUserDialog(bool isAddNew = true)
        {
            InitializeComponent();
            if(isAddNew == true)
            {
                tbDialogTitle.Text = Properties.Resources.content_adduser_dialog;
            }
            else
            {
                tbDialogTitle.Text = Properties.Resources.content_edituser_dialog;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
