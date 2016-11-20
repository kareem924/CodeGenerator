using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public  class CorrespondentTypeName
    {
        public CorrespondentTypeName()
        {
            this.GeneratedReference = new HashSet<GeneratedReference>();
        }
        [Key]
        public int CorrespondentId { get; set; }
        public string TypeName { get; set; }
        public string TypeNameAr { get; set; }

        public virtual ICollection<GeneratedReference> GeneratedReference { get; set; }
    }
}