using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public  class EventType
    {
        public EventType()
        {
            this.AuditTrial = new HashSet<AuditTrial>();
        }
        [Key]
        public int EventTypeId { get; set; }
        public string EventName { get; set; }
        public string EventNameAr { get; set; }
        public virtual ICollection<AuditTrial> AuditTrial { get; set; }
    }
}