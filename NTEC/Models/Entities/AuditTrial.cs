using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class AuditTrial
    {
        [Key]
        public long AuditTrialId { get; set; }
        public int? ReferenceId { get; set; }
        public int? EventTypeId { get; set; }
        public DateTime? EventTime { get; set; }
        public string EventDetails { get; set; }
        public string EventDetailsAr { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("EventTypeId")]
        public virtual EventType EventType { get; set; }
        [ForeignKey("ReferenceId")]
        public virtual GeneratedReference GeneratedReference { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }
    }
}