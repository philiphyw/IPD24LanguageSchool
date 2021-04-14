using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibrary;

namespace YZYAdminGUI
{
    class SearchCourseViewModel
    {
        private YZYDbContext ctx;
        public ObservableCollection<Course> Courses { get; set; }
        public YZYCommand DeleteCommand { get; set; }
        public YZYCommand UpdateCommand { get; set; }
        public YZYCommand AddCommand { get; set; }

        public SearchCourseViewModel()
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
                    item.UserID = SelectedCourse.UserID;
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
        public void OnAdd()
        {
            try
            {
                //WORKROUND: wdit a selected item to Add
                //TOFIX: add item cannot reuse controller bound with list view
                ctx.Courses.Add(SelectedCourse);
                ctx.SaveChanges();
                LoadCourse();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }

    }
}
