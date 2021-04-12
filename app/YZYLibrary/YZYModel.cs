using System;
using System.Data.Entity;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

//all database first code shall be in this file
namespace YZYLibrary
{
    public class YZYDBContext : DbContext
    {
        public YZYDBContext() : base("name=YZYDBContext") { }

        public virtual DbSet<Course> Course { get; set; }
    }

    public class Course : INotifyPropertyChanged
    {
        //INotifyPropertyChanged: implemenatation start
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        //INotifyPropertyChanged: implemenatation end

        public Course() { }

        //auto-generated code of table Course
        [Key]
        public int ID
        {
            get;
            set;
        }
    }
}