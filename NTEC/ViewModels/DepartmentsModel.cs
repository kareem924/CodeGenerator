using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Resources;


namespace NTEC.Models
{
    public class DepartmentsModel
    {
      
        public int? DepartmentId { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "NameEnRequired")]
        
        public string NameEn { get; set; }
        [Required(ErrorMessageResourceType = typeof (Resource), ErrorMessageResourceName="ShortcutEnRequired")]
      
        public string ShortcutEn { get; set; }
         [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "NameArRequired")]
        
        public string NameAr { get; set; }
       [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "ShortcutArRequired")]
       
        public string ShortcutAr { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedAt { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedAt { get; set; }
   
    }
}