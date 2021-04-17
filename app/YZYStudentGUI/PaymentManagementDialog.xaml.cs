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
using YZYLibraryAzure;

namespace YZYStudentGUI
{
    /// <summary>
    /// Interaction logic for PaymentManagementDialog.xaml
    /// </summary>
    public partial class PaymentManagementDialog : Window
    {
        YZYDbContextAzure ctx;
        private User curUser;
        public PaymentManagementDialog()
        {

            try
            {
                using (ctx = new YZYDbContextAzure())
                {
                    //TODO set curUser = login user in the login dialog  
                    curUser = ctx.Users.Where(r => r.UserID == 2).FirstOrDefault();
                }

            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message); ;
            }

            InitializeComponent();

        }

        private void LoadData()
        {
            try
            {
                using (ctx = new YZYDbContextAzure())
                {
                    decimal totalTuition = ctx.Registers.Include("Course").Where(r => r.UserID == curUser.UserID).Sum(r => r.Cours.Tuition);
                    decimal paidTuition = ctx.Payments.Where(r => r.UserID == curUser.UserID).Sum(r => r.Amount);


                    lbTuitionTotal.Content = String.Format("{0:.##}", totalTuition);
                    lbPaidTuition.Content = String.Format("{0:.##}", paidTuition);
                    lbBalance.Content = String.Format("{0:.##}", (totalTuition - paidTuition));

                    lvRegisters.ItemsSource = ctx.vStudentRegisters.Where(r => r.UserID == curUser.UserID).ToList();
                    
                }
            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message);
                
            }
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            if (curUser == null)
            {
                return;
            }

            if (tbPayAccount.Text.Length < 0)
            {
                return;
            }


            if (tbAmount.Text.Length < 0)
            {
                return;
            }

            try
            {
                if (!decimal.TryParse(tbAmount.Text, out decimal amount))
                {
                    return;
                }

                using (YZYDbContextAzure ctx = new YZYDbContextAzure())
                {
                    Payment payment = new Payment { UserID = curUser.UserID, PayAccount = tbPayAccount.Text, Amount = amount, PayDate = DateTime.Today };
                    ctx.Payments.Add(payment);
                    ctx.SaveChanges();
                    this.InitializeComponent();
                    LoadData();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //can't put loaddata in the custructor, otherwise will cause 
            LoadData();
        }
    }
}
