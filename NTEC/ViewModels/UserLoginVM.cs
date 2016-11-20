using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTEC.ViewModels
{
    public class UserLoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}