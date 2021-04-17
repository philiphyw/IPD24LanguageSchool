using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using YZYAdminGUI;
using YZYLibrary;

namespace YZYUnitTest
{
    [TestClass]
    public class AdminLoginDialogTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void checkCourseID_Failed()
        {
            ValidationRules.checkCourseID(-1);
        }
    }
}
