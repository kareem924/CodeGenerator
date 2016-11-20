using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.GenerateReference
{
    public class UnassignModel
    {
        public string GeneratedCodeEn { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "your reason is required")]
        public string UnassignReason { get; set; }

    }
}