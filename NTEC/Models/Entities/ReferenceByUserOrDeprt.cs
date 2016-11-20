using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class ReferenceByUserOrDeprt
    {
        [Key]
        public int GeneratedReferenceId { get; set; }
        public int DepartmentId { get; set; }
        public int CorrespondentId { get; set; }
        public string GeneratedCodeEn { get; set; }
        public string GeneratedCodeAr { get; set; }
        public string CompanyNameAr { get; set; }
        public string CompanyName { get; set; }
        public Nullable<bool> IsAssigned { get; set; }
        public Nullable<int> ReferenceNumber { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedAt { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedAt { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string Email { get; set; }
       
        public string NameEn { get; set; }
        
        public string TypeName { get; set; }
        public string TypeNameAr { get; set; }
        public string NameAr { get; set; }
        public string Name { get; set; }
    }
}