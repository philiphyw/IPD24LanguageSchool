using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YZYLibrary;

namespace YZYAdminGUI
{
    public class ValidationRules
    {
        public static void checkCourseID(int value)
        {
            if (value <= 0)
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
    }
}