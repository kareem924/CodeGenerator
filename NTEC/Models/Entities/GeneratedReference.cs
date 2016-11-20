using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class GeneratedReference
    {
        public GeneratedReference()
        {
            this.AuditTrial = new HashSet<AuditTrial>();
        }
        [Key]
        public int GeneratedReferenceId { get; set; }
        public int DepartmentId { get; set; }
        public int CorrespondentId { get; set; }
        public string GeneratedCodeEn { get; set; }
        public string GeneratedCodeAr { get; set; }
        public string CompanyNameAr { get; set; }
        public string CompanyName { get; set; }
        public bool? IsAssigned { get; set; }
        public int? ReferenceNumber { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool? IsDeleted { get; set; }
        [ForeignKey("CategoryId")]
        public virtual ICollection<AuditTrial> AuditTrial { get; set; }
        [ForeignKey("CorrespondentId")]
        public virtual CorrespondentTypeName CorrespondentTypeName { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        [ForeignKey("CreatedBy")]
        public virtual Users Users { get; set; }
    }
}