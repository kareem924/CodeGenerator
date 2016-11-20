using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Resources;

namespace NTEC.Models
{
    public class UsersModel
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required]
        public int RoleID { get; set; }

        public int Department { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailValid", ErrorMessage = null)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailValid", ErrorMessage = null)]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "LastNameRequired")]
        public string LastName { get; set; }

        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordLength")]
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordRequired")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [StringLength(8, MinimumLength = 4, ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordLength")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "PasswordConfirm")]
        public string ConfirmPassword { get; set; }
        public string Telephone { get; set; }

        public List<SelectedDepart> SelectedDepartments { get; set; }
        public DateTime? DOB { get; set; }
          [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "DepartmentUsersValidation")]
        public List<int> Departments { get; set; }
      
        public List<DepartmentsModel> DepartmentId { get; set; }
        public bool IsActive { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedAt { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedAt { get; set; }


    }

    public class UpdateUserModel
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required]
        public int RoleID { get; set; }

        public int Department { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailValid", ErrorMessage = null)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "EmailValid", ErrorMessage = null)]
        public string Email { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "FirstNameRequired")]
        public string FirstName { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "LastNameRequired")]
        public string LastName { get; set; }

       
        public string Telephone { get; set; }

        public List<SelectedDepart> SelectedDepartments { get; set; }
        public DateTime? DOB { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource), ErrorMessageResourceName = "DepartmentUsersValidation")]
        public List<int> Departments { get; set; }

        public List<DepartmentsModel> DepartmentId { get; set; }
        public bool IsActive { get; set; }
        [ScaffoldColumn(false)]
        public int? CreatedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedAt { get; set; }
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? ModifiedAt { get; set; }
    }
    public class SelectedDepart
    {
        public int DepartmentId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}