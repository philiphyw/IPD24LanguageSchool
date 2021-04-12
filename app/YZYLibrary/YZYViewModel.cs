using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace YZYLibrary
{
    public class YZYCourseViewModel
    {
        private YZYDBContext database;
        public ObservableCollection<Course> Courses { get; set; }
        public YZYCommand DeleteCommand { get; set; }
        public YZYCommand UpdateCommand { get; set; }
        public YZYCommand AddCommand { get; set; }

        public YZYCourseViewModel()
        {
            Log.setLogOnFile();
            try
            {
                database = new YZYDBContext();
            }
            catch (SystemException ex)
            {
                Log.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            Courses = new ObservableCollection<Course>();
            LoadCourses();
            DeleteCommand = new YZYCommand(this.OnDelete, this.CanExecute);
            UpdateCommand = new YZYCommand(this.OnUpdate, this.CanExecute);
            AddCommand = new YZYCommand(this.OnAdd, this.CanExecute);
        }

        private void LoadCourses()
        {
            try
            {

                var courseList = database.Course.ToList();
                Courses.Clear();
                foreach (var item in courseList)
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
            database.Course.Remove(SelectedCourse);
            database.SaveChanges();
            LoadCourses();
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
                var item = (from td in database.Course where td.ID == SelectedCourse.ID select td).FirstOrDefault<Course>();
                if (item != null)
                {
                    //TODO: item update
                }
                database.SaveChanges();
                LoadCourses();
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
                database.Course.Add(SelectedCourse);
                database.SaveChanges();
                LoadCourses();
            }
            catch (Exception ex)
                when ((ex is InvalidParameterException) || (ex is SystemException))
            {
                Log.WriteLine(ex.Message);
            }
        }
    }
}