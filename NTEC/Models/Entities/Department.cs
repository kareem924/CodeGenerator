using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{

    public  class Department
    {
        public Department()
        {
            this.GeneratedReference = new HashSet<GeneratedReference>();
            this.UserDepartment = new HashSet<UserDepartment>();
        }
        [Key]
        public int DepartmentId { get; set; }
        public string NameEn { get; set; }
        public string ShortcutEn { get; set; }
        public string NameAr { get; set; }
        public string ShortcutAr { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        public virtual ICollection<GeneratedReference> GeneratedReference { get; set; }
        public virtual ICollection<UserDepartment> UserDepartment { get; set; }
    }
}