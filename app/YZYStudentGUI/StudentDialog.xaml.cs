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

namespace DotnetProject_YZY
{
    /// <summary>
    /// Interaction logic for StudentDialog.xaml
    /// </summary>
    public partial class StudentDialog : Window
    {
        private YZYUserViewModel uservmInstance;
        public StudentDialog()
        {
            InitializeComponent();
            uservmInstance = new YZYUserViewModel();
            this.DataContext = uservmInstance;
        }
    }
}
