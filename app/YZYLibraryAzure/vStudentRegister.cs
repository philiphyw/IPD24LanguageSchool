//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YZYLibraryAzure
{
    using System;
    using System.Collections.Generic;
    
    public partial class vStudentRegister
    {
        public int UserID { get; set; }
        public string CateDesc { get; set; }
        public short Difficulty { get; set; }
        public string CourseDesc { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public decimal Tuition { get; set; }
        public string Teacher { get; set; }
        public int RegisterStatus { get; set; }
        public int CourseID { get; set; }
        public int RegisterID { get; set; }




        public string StartDateStr { get { return $"{StartDate:d}"; } }
        public string EndDateStr { get { return $"{EndDate:d}"; } }
        public string TuitionStr { get { return Tuition == 0 ? "0" : string.Format("{0:.##}", Tuition); } }
    }
}
