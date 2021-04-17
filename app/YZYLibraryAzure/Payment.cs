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
    
    public partial class Payment
    {
        public int PaymentID { get; set; }
        public int UserID { get; set; }
        public string PayAccount { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime PayDate { get; set; }
    
        public virtual User User { get; set; }


        public string AmountStr { get { return string.Format("{0:.##}", Amount); } }
        public string PayDateStr { get { return $"{PayDate:d}"; } }
    }
}
