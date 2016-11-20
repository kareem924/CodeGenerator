using System;
using System.ComponentModel.DataAnnotations;

namespace NTEC.Models.Entities
{

    public class LogginHistoryView
    {

        public string EventName { get; set; }
       
        [Key]
        public long AuditTrialId { get; set; }
        public Nullable<int> ReferenceId { get; set; }
        public Nullable<int> EventTypeId { get; set; }
        public Nullable<System.DateTime> EventTime { get; set; }
        public string EventDetails { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Email { get; set; }
        public string EventNameAr { get; set; }
        public string EventDetailsAr { get; set; }
        public string Name { get; set; }
    }
}