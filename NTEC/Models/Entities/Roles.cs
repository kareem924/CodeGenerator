using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NTEC.Models.Entities
{
    public partial class Roles
    {
        public Roles()
        {
            this.Users = new HashSet<Users>();
        }
        [Key]
        public int RoldeId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}