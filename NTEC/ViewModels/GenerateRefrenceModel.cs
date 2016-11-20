using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.GenerateReference
{
    public class GenerateRefrenceModel
    {
        public int GeneratedReferenceId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public int EditDepartmentId { get; set; }
        public string GeneratedCodeEn { get; set; }
    
        public string GeneratedCodeAr { get; set; }
        [Required]
        public string CompanyName { get; set; }
      
        public string CompanyNameAr { get; set; }
        public string Incoming { get; set; }
        public bool IsAssigned { get; set; }
    }

    public class GeneratedCode
    {
        public string GeneratedCodeEn { get; set; }
        public string GeneratedCodeAr { get; set; }
        public bool IsUnAssignedRef { get; set; }
        public int UnAssignedId { get; set; }
        public bool Isupdated { get; set; }

    }
}