using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class UserDepartment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}