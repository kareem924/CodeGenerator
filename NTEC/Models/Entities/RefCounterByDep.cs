using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public class RefCounterByDep
    {
        [Key]
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public int Count { get; set; }
    }
}