using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class Exception
    {
        [Key]
        public long ExId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public string Stack { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}