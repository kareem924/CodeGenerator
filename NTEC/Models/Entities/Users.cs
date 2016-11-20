using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class Users
    {
        public Users()
        {
            this.AuditTrial = new HashSet<AuditTrial>();
            this.GeneratedReference = new HashSet<GeneratedReference>();
            this.UserDepartment = new HashSet<UserDepartment>();
        }
        [Key]
        public int UserId { get; set; }
        public int RoleID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Telephone { get; set; }
        public DateTime? DOB { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<AuditTrial> AuditTrial { get; set; }
        public virtual ICollection<GeneratedReference> GeneratedReference { get; set; }
         [ForeignKey("RoleID")]
        public virtual Roles Roles { get; set; }
        public virtual ICollection<UserDepartment> UserDepartment { get; set; }
    }
}