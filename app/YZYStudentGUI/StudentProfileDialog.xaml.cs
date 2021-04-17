using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for StudentProfileDialog.xaml
    /// </summary>
    public partial class StudentProfileDialog : Window
    {
        YZYDbContextAzure ctx;
        private User curUser;
        private byte[] curPictureArr;
        private bool isPictureModified = false;
        private bool isPasswordModified = false;
        public StudentProfileDialog()
        {
            InitializeComponent();
        }


        private void LoadData()
        {
            try
            {
                using (ctx = new YZYDbContextAzure())
                {
                    //TODO set curUser = login user in the login dialog  
                    curUser = ctx.Users.Where(r => r.UserID == 2).FirstOrDefault();

                    tbNewCell.Text = curUser.Cell;
                    tbNewCity.Text = curUser.City;
                    tbNewEmail.Text = curUser.Email;
                    tbNewPassword.Text = curUser.Password;
                    tbNewGender.Text = Enum.GetName( typeof(GenderEnum),curUser.Gender);
                    tbNewPhone.Text = curUser.Phone;
                    tbNewPostalCode.Text = curUser.PostalCode;
                    tbNewProvince.Text = curUser.Province;
                    tbNewSIN.Text = curUser.UserSIN;
                    tbNewStreetName.Text = curUser.StreetName;
                    tbNewStreetNo.Text = curUser.StreetNo;
                    tbFirstName.Text = curUser.FName;
                    tbMiddleName.Text = curUser.MName;
                    tbLastName.Text = curUser.LName;
                    tbUserRole.Text =Enum.GetName(typeof(UserRoleEnum), curUser.UserRole);

                    if (curUser.Photo.Length>0)
                    {
                        using (MemoryStream stream = new MemoryStream(curUser.Photo))
                        {
                            imageViewer.Source = BitmapFrame.Create(stream,
                                                             BitmapCreateOptions.None,
                                                             BitmapCacheOption.OnLoad);

                        }
                    }

                }
            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message);

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btPickPicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    curPictureArr = File.ReadAllBytes(dlg.FileName);
                    tbImage.Visibility = Visibility.Hidden; // hide the text box
                                                            //System.Drawing.Image bitmap  = byteArrayToImage(currOwnerImage); // ex: SystemException

                    Image image = new Image();
                    using (MemoryStream stream = new MemoryStream(curPictureArr))
                    {
                        //To show the image on the component, instead of assign an image object to the source, need a use BitmapFrame.Create to create a new BitmapFrame
                        imageViewer.Source = BitmapFrame.Create(stream,
                                                         BitmapCreateOptions.None,
                                                         BitmapCacheOption.OnLoad);

                    }

                    isPictureModified = true;

                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    MessageBox.Show(ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }



        private void btSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (isPasswordModified == true)
            {
                if (!tbNewPassword.Text.Equals(tbNewConfirmPassword.Text))
                {
                    MessageBox.Show("Password didn't matched the confirmed password. please check.");
                    return;
                }
            }

            try
            {

                using (ctx = new YZYDbContextAzure())
                {
                    User udUser = ctx.Users.Where(r => r.UserID == curUser.UserID).FirstOrDefault();


                  
                        udUser.Cell = tbNewCell.Text;
                        udUser.City = tbNewCity.Text;
                        udUser.Email = tbNewEmail.Text;
                        udUser.Password = tbNewPassword.Text;
                        GenderEnum gender = (GenderEnum)Enum.Parse(typeof(GenderEnum), tbNewGender.Text, true);
                        udUser.Gender = gender;
                        udUser.Phone = tbNewPhone.Text;
                        udUser.PostalCode = tbNewPostalCode.Text;
                        udUser.Province = tbNewProvince.Text;
                        udUser.UserSIN = tbNewSIN.Text;
                        udUser.StreetName = tbNewStreetName.Text;
                        udUser.StreetNo = tbNewStreetNo.Text;
                    


                    if (isPictureModified == true)
                    {
                        udUser.Photo = curPictureArr;
                    }

                    ctx.SaveChanges();
                    MessageBox.Show("Profile updated");
                    this.InitializeComponent();
                    LoadData();
                }
               
            }
            catch (SystemException ex)
            {

                MessageBox.Show("Failed to connect to database: " + ex.Message);
            }

            


        }

        private void tbNewPassword_KeyDown(object sender, KeyEventArgs e)
        {
            isPasswordModified = true;
        }
    }
}
