using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTEC.Models.Entities;

namespace NTEC.Models.Security
{
    public class SecuredUser
    {
        public UserLogin User { get; set; }


        public String Username
        {
            get
            {
                return User.Username;

            }
        }
        public int UserId
        {
            get
            {

                return User.UserId;
            }
        }
        public String DisplayName
        {
            get
            {
                return User.DisplayName;

            }
        }
        public String RoleName
        {
            get
            {
                return User.RoleName;

            }
        }
        public bool IsSysAdmin
        {
            get
            {

                return User.IsSystemAdmin;

            }
        }

    }
    public class UserLogin
    {
        public List<int> RoleId { get; set; }
        public bool Isauthenticated { get; set; }
        public int AuthTypeId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystemAdmin { get; set; }
        public Language LanguageId { get; set; }
        public string RoleName { get; set; }
        public bool LogEvents { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}