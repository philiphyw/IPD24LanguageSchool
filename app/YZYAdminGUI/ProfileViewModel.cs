using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibrary;

namespace YZYAdminGUI
{
    class ProfileViewModel : IDataErrorInfo
    {
        private YZYDbContext ctx;
        public User LoginUser { get; set; }

        public YZYCommand UpdateCommand { get; set; }

        public ProfileViewModel()
        {
            Log.setLogOnFile();
            try
            {
                ctx = new YZYDbContext();
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            LoadUser();
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanUpdate);
        }

        private void LoadUser()
        {
            try
            {
                User user = ctx.Users.ToList().Where(u=>u.UserID==GlobalSettings.userID).FirstOrDefault();
                if(user != null)
                {
                    LoginUser = user;
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }

        public bool CanUpdate()
        {
            return true;
        }
        public void OnUpdate()
        {
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }

        private string[] _errorMessage = new string[7] { string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty };
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "UserID":
                        if (!String.IsNullOrEmpty(_errorMessage[0]))
                            return _errorMessage[0];
                        break;
                }
                return string.Empty;
            }
        }
    }
}
