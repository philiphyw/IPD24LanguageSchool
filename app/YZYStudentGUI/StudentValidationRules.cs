using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using YZYLibrary;

namespace YZYStudentGUI
{
    public class StudentValidationRules
    {
        public static void checkFirstName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }       
        public static void checkMiddleName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }     
        public static void checkLastName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }       
        public static void checkSIN(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]{9}$"))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
        public static void checkStreetNo(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]+$"))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
        public static void checkStreetName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,50}$") || String.IsNullOrWhiteSpace(value))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
        public static void checkCityName(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Za-z ]{2,30}$") || String.IsNullOrWhiteSpace(value))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
        public static void checkPostalCode(string value)
        {
            if (!Regex.IsMatch(value, @"^[A-Z][0-9][A-Z][0-9][A-Z][0-9]$"))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
        public static void checkPhone(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]{10}$"))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }       
        public static void checkCell(string value)
        {
            if (!Regex.IsMatch(value, @"^[0-9]{10}$"))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
        public static void checkEmail(string value)
        {
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new InvalidParameterException(Properties.Resources.error_course_id + $": {value}");
            }
        }
    }
}
