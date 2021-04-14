//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YZYLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Register
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Register()
        {
            this.Evaluations = new HashSet<Evaluation>();
        }
    
        public int RegisterID { get; set; }
        public int UserID { get; set; }
        public int CourseID { get; set; }
        public string RegisterStatus { get; set; }
        public Nullable<int> PaymentID { get; set; }
    
        public virtual Course Cours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evaluation> Evaluations { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual User User { get; set; }
    }
}