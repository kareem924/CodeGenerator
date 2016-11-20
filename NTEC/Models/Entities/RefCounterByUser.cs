using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class RefCounterByUser
    {
        [Key]
        
        public int Count { get; set; }
        public string Name { get; set; }
    }
}