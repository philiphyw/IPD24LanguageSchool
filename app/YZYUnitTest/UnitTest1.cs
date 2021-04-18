using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using YZYLibraryAzure;
using YZYStudentGUI;

namespace YZYUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            YZYDbContextAzure ctx;
            ctx = new YZYDbContextAzure();
            //GlobalSettings.userID = 2;
            User testUser = ctx.Users.Where(r => r.UserID == 2).FirstOrDefault();
            int expResult = 2;
            Assert.AreEqual(expResult, testUser.UserID);
        }

        [TestMethod]
        public void TestMethod2()
        {

        }

    }




}
