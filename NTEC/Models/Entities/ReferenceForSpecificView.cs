using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class ReferenceForSpecificView
    {
         [Key]
        public long AuditTrialId { get; set; }
        public Nullable<int> ReferenceId { get; set; }
        public Nullable<int> EventTypeId { get; set; }
        public Nullable<System.DateTime> EventTime { get; set; }
        public string EventDetails { get; set; }
        public Nullable<int> UserId { get; set; }
        public string GeneratedCodeEn { get; set; }
        public string GeneratedCodeAr { get; set; }
        public string CompanyName { get; set; }
        public Nullable<bool> IsAssigned { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public string EventName { get; set; }
        public string NameEn { get; set; }
        public string FirstName { get; set; }
        public string TypeName { get; set; }
    }
}