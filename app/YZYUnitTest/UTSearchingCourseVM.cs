using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using YZYLibraryAzure;
using YZYAdminGUI;


namespace YZYUnitTest
{
    [TestClass]
    public class UTSearchingCourseVM
    {
        private SearchCourseViewModel _vmSearchCourse = new SearchCourseViewModel();
        private int _courseID = -1;

        [TestMethod]
        public void AddCourse_Success()
        {
            
            /*
            Random rnd = new Random();
            int orderNumber = rnd.Next(1, 100000);
            
            string emailOrg = _vmSearchAccount.SelectedUser.Email;
            string emailNew = $"{_vmSearchAccount.SelectedUser.FName}.{_vmSearchAccount.SelectedUser.LName}.{orderNumber}@hotmail.com";
            _vmSearchAccount.SelectedUser.Email = emailNew;
            _vmSearchAccount.OnUpdate();
            string emailReload = _vmSearchAccount.Users.ToList().Where(u => u.UserID == userIDSelected).FirstOrDefault().Email;
            */
            int userIDSelected = _vmSearchAccount.SelectedUser.UserID;
            _vmSearchAccount.OnDelete();
            var item = _vmSearchAccount.Users.ToList().Where(u => u.UserID == userIDSelected).FirstOrDefault();
            Assert.AreEqual(item, null);
        }

        [TestMethod]
        public void UpdarteCourse_Success()
        {
            _vmSearchCourse.SelectedCourse = _vmSearchCourse.Courses.ElementAt(_vmSearchCourse.Courses.Count / 2);
            /*
            Random rnd = new Random();
            int orderNumber = rnd.Next(1, 100000);
            
            string emailOrg = _vmSearchAccount.SelectedUser.Email;
            string emailNew = $"{_vmSearchAccount.SelectedUser.FName}.{_vmSearchAccount.SelectedUser.LName}.{orderNumber}@hotmail.com";
            _vmSearchAccount.SelectedUser.Email = emailNew;
            _vmSearchAccount.OnUpdate();
            string emailReload = _vmSearchAccount.Users.ToList().Where(u => u.UserID == userIDSelected).FirstOrDefault().Email;
            */
            int userIDSelected = _vmSearchAccount.SelectedUser.UserID;
            _vmSearchAccount.OnDelete();
            var item = _vmSearchAccount.Users.ToList().Where(u => u.UserID == userIDSelected).FirstOrDefault();
            Assert.AreEqual(item, null);
        }

        [TestMethod]
        public void DeleteCourse_Success()
        {
            _vmSearchCourse.SelectedCourse = _vmSearchCourse.Courses.ElementAt(_vmSearchCourse.Courses.Count / 2);
            /*
            Random rnd = new Random();
            int orderNumber = rnd.Next(1, 100000);
            
            string emailOrg = _vmSearchAccount.SelectedUser.Email;
            string emailNew = $"{_vmSearchAccount.SelectedUser.FName}.{_vmSearchAccount.SelectedUser.LName}.{orderNumber}@hotmail.com";
            _vmSearchAccount.SelectedUser.Email = emailNew;
            _vmSearchAccount.OnUpdate();
            string emailReload = _vmSearchAccount.Users.ToList().Where(u => u.UserID == userIDSelected).FirstOrDefault().Email;
            */
            int userIDSelected = _vmSearchAccount.SelectedUser.UserID;
            _vmSearchAccount.OnDelete();
            var item = _vmSearchAccount.Users.ToList().Where(u => u.UserID == userIDSelected).FirstOrDefault();
            Assert.AreEqual(item, null);
        }
    }
}
