using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
namespace NTEC.Models.Entities
{

    public class C_RefNoCount
    {
        [Key]
        public int RefNoCountId { get; set; }
        public string Year { get; set; }
        public long StartNumber { get; set; }
        public int CorrespondentTypeID { get; set; }
        public Nullable<bool> IsCurrentYearxx { get; set; }
    }
}