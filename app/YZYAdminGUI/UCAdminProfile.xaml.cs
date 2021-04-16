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

namespace YZYAdminGUI
{

    /// <summary>
    /// Interaction logic for UCAdminProfile.xaml
    /// </summary>
    public partial class UCAdminProfile : UserControl
    {
        private ProfileViewModel _vmProfile;
        public UCAdminProfile()
        {
            InitializeComponent();
            _vmProfile = new ProfileViewModel();
            this.DataContext = _vmProfile;
        }
    }
}
