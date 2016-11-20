using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class RefUnAssignedPool
    {
        [Key]
        public int RefUnassignedPoolId { get; set; }
        public long StartNumber { get; set; }
        public int Year { get; set; }
        public int CorrespondentTypeId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
    }
}