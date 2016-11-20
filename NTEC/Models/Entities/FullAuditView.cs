using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class FullAuditView
    {
        [Key]
        public long AuditTrialId { get; set; }
        public int? ReferenceId { get; set; }
        public int? EventTypeId { get; set; }
        public DateTime? EventTime { get; set; }
        public string EventDetails { get; set; }
        public int? UserId { get; set; }
        public string EventName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
      
        public string EventNameAr { get; set; }
        public string EventDetailsAr { get; set; }
    }
}