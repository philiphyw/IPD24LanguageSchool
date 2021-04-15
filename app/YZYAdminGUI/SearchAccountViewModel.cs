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
    class SearchAccountViewModel : IDataErrorInfo
    {
        private YZYDbContext ctx;
        public ObservableCollection<User> Users { get; set; }

        public YZYCommand DeleteCommand { get; set; }
        public YZYCommand UpdateCommand { get; set; }
        public YZYCommand AddCommand { get; set; }

        public SearchAccountViewModel()
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
            Users = new ObservableCollection<User>();
            LoadCourse();
            DeleteCommand = new YZYCommand(this.OnDelete, this.CanExecute);
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanExecute);
            AddCommand = new YZYCommand(this.OnAdd, this.CanExecute);
        }

        private void LoadCourse()
        {
            try
            {
                var UserList = ctx.Users.ToList();
                Users.Clear();
                foreach (var item in UserList)
                {
                    Users.Add(item);
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get
            {
                return _selectedUser;
            }
            set
            {
                _selectedUser = value;
            }
        }

        public void OnDelete()
        {
            //FIXME: if not selected
            //FIXME: failed if continously delete 2nd time
            try
            {
                ctx.Users.Remove(SelectedUser);
                ctx.SaveChanges();
            }
            catch(DbUpdateException ex)
            {
                Log.WriteLine(SelectedUser.UserID+"[DELETE]: "+ex.Message);
            }

            LoadCourse();
        }
        public bool CanExecute()
        {
            if (SelectedUser != null)
            {
                return true;
            }
            return false;
        }
        public void OnUpdate()
        {
            try
            {
                var item = (from r in ctx.Users where r.UserID == SelectedUser.UserID select r).FirstOrDefault<User>();
                if (item != null)
                {
                    //FIXME: dialog needed?
                    //item = SelectedUser;
                }
                ctx.SaveChanges();
                LoadCourse();
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
                    case "CourseID":
                        if (!String.IsNullOrEmpty(_errorMessage[0]))
                            return _errorMessage[0];
                        break;
                }
                return string.Empty;
            }
        }
        /*
        private Course _newCourse;
        public Course NewCourse
        {
            get
            {
                return _newCourse;
            }
        }
        public int CourseID
        {
            get
            {
                return _newCourse.CourseID;
            }
            set
            {
                if (_newCourse == null)
                {
                    _newCourse = new Course();
                }
                try
                {
                    ValidationRules.checkCourseID(value);
                    _newCourse.CourseID = value;
                    _errorMessage[0] = string.Empty;
                }
                catch (InvalidParameterException ex)
                {
                    _errorMessage[0] = ex.Message;
                }
            }
        }
        */
        public void OnAdd()
        {
            try
            {
                //FIXME: check related userid in database
                // dialog needed?
                ctx.Users.Add(SelectedUser);
                ctx.SaveChanges();
                LoadCourse();
                //CourseID = 0;
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }

    }
}
