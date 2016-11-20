using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class LangTransalation
    {
        [Key]
        public int TranslationId { get; set; }
        public int? ModuleId { get; set; }
        public int? LanguageId { get; set; }
        public int? EntityId { get; set; }
        public int? EntityRecordId { get; set; }
        public string transText { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool isDeleted { get; set; }
    }
}