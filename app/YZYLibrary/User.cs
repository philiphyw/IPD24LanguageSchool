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
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Courses = new HashSet<Course>();
            this.Payments = new HashSet<Payment>();
            this.Registers = new HashSet<Register>();
        }

        private string _fullname = string.Empty;
        [NotMapped]
        public string FullName
        {
            get
            {
                if (_fullname == string.Empty)
                {
                    _fullname = FName + " " + LName;
                }
                return _fullname;
            }
            set
            {
                string[] names = value.Split(' ');
                if (names != null && names.Length > 1)
                {
                    _fullname = value;
                    switch (names.Length)
                    {
                        case 2:
                            FName = names[0];
                            LName = names[1];
                            break;
                        case 3:
                            FName = names[0];
                            MName = names[1];
                            LName = names[2];
                            break;
                    }
                }

            }
        }
        [NotMapped]
        public string Address
        {
            get
            {
                return StreetNo + " " + StreetName + ", " + City + ", " + Province + ", " + PostalCode;
            }
        }

        public int UserID { get; set; }
        public UserRoleEnum UserRole { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public string UserSIN { get; set; }
        public int Gender { get; set; }
        public string StreetNo { get; set; }
        public string StreetName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Cell { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Course> Courses { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Register> Registers { get; set; }
    }
}
