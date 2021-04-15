using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibrary;

namespace YZYAdminGUI
{
    class SearchAccountViewModel : IDataErrorInfo
    {
        private YZYDbContext ctx;
        public ObservableCollection<Course> Courses { get; set; }
        public List<Category> Categories { get; set; }
        public List<string> CategoryStrings { get; set; }
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
            Courses = new ObservableCollection<Course>();
            Categories = new List<Category>();
            CategoryStrings = new List<string>();
            LoadCourse();
            DeleteCommand = new YZYCommand(this.OnDelete, this.CanExecute);
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanExecute);
            AddCommand = new YZYCommand(this.OnAdd, this.CanExecute);
        }

        private void LoadCourse()
        {
            try
            {
                var CourseList = ctx.Courses.ToList();
                Courses.Clear();
                foreach (var item in CourseList)
                {
                    Courses.Add(item);
                }
                Categories = ctx.Categories.ToList();
                CategoryStrings.Clear();
                CategoryStrings.Add("Please select");
                foreach (var item in Categories)
                {
                    CategoryStrings.Add(item.CateDesc);
                }
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
            }
        }

        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get
            {
                return _selectedCourse;
            }
            set
            {
                _selectedCourse = value;
            }
        }

        public void OnDelete()
        {
            //FIXME: if not selected
            //FIXME: failed if continously delete 2nd time
            ctx.Courses.Remove(SelectedCourse);
            ctx.SaveChanges();
            LoadCourse();
        }
        public bool CanExecute()
        {
            if (SelectedCourse != null)
            {
                return true;
            }
            return false;
        }
        public void OnUpdate()
        {
            try
            {
                var item = (from r in ctx.Courses where r.CourseID == SelectedCourse.CourseID select r).FirstOrDefault<Course>();
                if (item != null)
                {
                    item.CourseID = SelectedCourse.CourseID;
                    item.StartDate = SelectedCourse.StartDate;
                    item.EndDate = SelectedCourse.EndDate;
                    item.CourseDesc = SelectedCourse.CourseDesc;
                    item.Tuition = SelectedCourse.Tuition;
                    item.UserID = SelectedCourse.UserID;//FIXME: if name is changed, we have to check related userid in database
                    item.CategoryID = SelectedCourse.CategoryID;
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
                ctx.Courses.Add(SelectedCourse);
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
