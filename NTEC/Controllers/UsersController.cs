using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using NTEC.Models;
using NTEC.Classes.Security;
using NTEC.Models.Entities;
using NTEC.Models.Security;
using NTEC.Models.UnitOfWork;
using PagedList;
using Resources;


namespace NTEC.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        IUnitOfWork _uow;

        public UsersController()
        {
            _uow = new UnitOfWork();
        }
        //
        // GET: /Users/
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(int? page)
        {

            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Opened Users Index"
                ,
                EventDetailsAr = "قام بفتح صفحة المستخدمين"
            });
            _uow.Save();
            return View();
        }
        public ActionResult List(int? page)
        {
            var quary = from s in _uow.Useres.List()
                        where s.IsDeleted != true
                        orderby s.CreatedAt descending
                        select s;
            int pageSize = int.Parse(WebConfigurationManager.AppSettings["PagingSize"]);
            int pageNumber = (page ?? 1);
            var onePageOfUser = quary.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfGeneratedReference = onePageOfUser;
            return PartialView("_UserList");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser()
        {
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Open Create User",
                EventDetailsAr = "قام بفتح صفحة انشاء مستخدم جديد"

            });
            _uow.Save();
            return PartialView("_CreateUser");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser(UsersModel model)
        {
            if (ModelState.IsValid)
            {
                var uniqueEmail = _uow.Useres.List().Where(x => x.Email == model.Email);
                if (uniqueEmail.Any())
                {
                    ModelState.AddModelError("Email", "The Email exists,Please choose another one");

                    return PartialView("_CreateUser", model);
                }
                var userEntity = new Users();
                var encPass = StringCipher.Encrypt(model.ConfirmPassword,
                WebConfigurationManager.AppSettings["EncDecKey"]);
                userEntity.RoleID = model.RoleID;
                userEntity.FirstName = model.FirstName;
                userEntity.LastName = model.LastName;
                userEntity.Email = model.Email;
                userEntity.Password = encPass;
                userEntity.Telephone = model.Telephone;
                userEntity.DOB = model.DOB;
                userEntity.CreatedAt = DateTime.Now;
                userEntity.IsActive = model.IsActive;
                userEntity.CreatedBy = SessionManager.CurrentUser.UserId;
             
                foreach (var departmentsId in model.Departments)
                {
                    userEntity.UserDepartment.Add(new UserDepartment() { DepartmentId = departmentsId });
                }
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 1,
                    EventTime = DateTime.Now,
                    EventDetails = "Created New User  " + model.FirstName + " " + model.LastName,
                    EventDetailsAr = "قام بانشاء هذا المستخدم " + model.FirstName + " " + model.LastName
                });

                _uow.Useres.Add(userEntity);
                _uow.Save();
                string url = Url.Action("List", "Users");
                return Json(new { success = true, url = url });
            }

            return PartialView("_CreateUser", model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public ActionResult UpdateProfile()
        {
            var userEntity = _uow.Useres.Find(SessionManager.CurrentUser.UserId);
            if (userEntity == null)
            {
                return HttpNotFound();
            }

            var model = new UpdateUserModel
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,
                DOB = userEntity.DOB,
                IsActive = userEntity.IsActive != null && userEntity.IsActive.Value,
                RoleID = userEntity.RoleID,
                Telephone = userEntity.Telephone,
                ModifiedAt = DateTime.Now,
                SelectedDepartments = new List<SelectedDepart>(),
                Departments = new List<int>()
            };
            if (userEntity.UserDepartment != null)
            {

                foreach (var variable in userEntity.UserDepartment)
                {
                    model.SelectedDepartments.Add(new SelectedDepart()
                    {
                        DepartmentId = variable.DepartmentId,
                        NameEn = variable.Department.NameEn,
                        NameAr = variable.Department.NameAr

                    });
                    model.Departments.Add(variable.DepartmentId);
                }
            }
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Opened Update Profile User  " + model.FirstName + " " + model.LastName
            });
            _uow.Save();

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult UpdateProfile(UpdateUserModel updateModel)
        {
            if (ModelState.IsValid)
            {
                Users userEntityToEdit = _uow.Useres.Find(updateModel.UserId);
                userEntityToEdit.FirstName = updateModel.FirstName;
                userEntityToEdit.LastName = updateModel.LastName;
                userEntityToEdit.Email = updateModel.Email;
                userEntityToEdit.IsActive = updateModel.IsActive;
                userEntityToEdit.RoleID = updateModel.RoleID;
                userEntityToEdit.Telephone = updateModel.Telephone;
                userEntityToEdit.ModifiedBy = SessionManager.CurrentUser.UserId;
                userEntityToEdit.ModifiedAt = DateTime.Now;
               
                if (updateModel.Departments != null)
                {
                    foreach (var variable in userEntityToEdit.UserDepartment.ToList())
                    {
                        _uow.UserDepartments.Delete(variable.Id);
                    }

                    foreach (var variable in updateModel.Departments)
                    {
                        userEntityToEdit.UserDepartment.Add(new UserDepartment()
                        {
                            DepartmentId = variable,
                            UserId = userEntityToEdit.UserId
                        });
                    }
                }


                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 2,
                    EventTime = DateTime.Now,
                    EventDetails = "Updated this User  " + updateModel.FirstName + " " + updateModel.LastName,
                    EventDetailsAr = "قام بتعديل بيانات هذا المستخدم " + updateModel.FirstName + " " + updateModel.LastName
                });
                _uow.Useres.Edit(userEntityToEdit.UserId, userEntityToEdit);
                _uow.Save();
               
                TempData["successMessage"] = "Your Profile updated Succefuly";
                var secUser = new SecuredUser { User = new UserLogin() { DisplayName = updateModel.FirstName, Email = updateModel.Email, Isauthenticated = true, RoleName = updateModel.RoleID == 1 ? "Admin" : "User", UserId = updateModel.UserId, IsSystemAdmin = updateModel.RoleID == 1 } };
                string dataS =
                    secUser.UserId + "," + //user Obj  (0-2)
                    secUser.Username + "," + //user Obj
                    secUser.DisplayName + "," +
                    secUser.IsSysAdmin + "," +//user Obj
                    secUser.RoleName + ",";   //ticket Obj
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, updateModel.FirstName, DateTime.Now, DateTime.Now.AddDays(364), true, dataS, FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash)
                {
                    Expires = ticket.IsPersistent ? ticket.Expiration : DateTime.Today.AddDays(1)
                };
                HttpContext.Response.Cookies.Add(cookie);

                HttpCookie authCookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                FormsAuthenticationTicket ticketD = FormsAuthentication.Decrypt(authCookie.Value);
                string[] values = ticketD.UserData.Split(',').Select(sValue => sValue.Trim()).ToArray();


                var cuser = new UserLogin
                {
                    UserId = Convert.ToInt32(values[0]),
                    Username = values[1],
                    DisplayName = values[2],
                    IsSystemAdmin = Convert.ToBoolean(values[3]),
                    RoleName = values[4]

                };
                var abcUser = new SecuredUser {User = cuser};




                SessionManager.CurrentUser = secUser;
                if (User.IsInRole("Admin") && userEntityToEdit.RoleID == 2)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Login");
                }
                return RedirectToAction("UpdateProfile", updateModel);
            }

            TempData["failedMessage"] = "something wrong happened";
            return View(updateModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdateUser(int ID)
        {
            var userEntity = _uow.Useres.Find(ID);
            if (userEntity == null)
            {
                return HttpNotFound();
            }

            var model = new UpdateUserModel
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Email = userEntity.Email,

                DOB = userEntity.DOB,
                IsActive = userEntity.IsActive != null && userEntity.IsActive.Value,
                RoleID = userEntity.RoleID,
                Telephone = userEntity.Telephone,
                ModifiedAt = DateTime.Now,
                SelectedDepartments = new List<SelectedDepart>(),
                Departments = new List<int>()
            };
            if (userEntity.UserDepartment != null)
            {

                foreach (var variable in userEntity.UserDepartment)
                {
                    model.SelectedDepartments.Add(new SelectedDepart()
                    {
                        DepartmentId = variable.DepartmentId,
                        NameEn = variable.Department.NameEn,
                        NameAr = variable.Department.NameAr
                    });
                    model.Departments.Add(variable.DepartmentId);
                }
            }
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Opened Edit User  " + model.FirstName + " " + model.LastName,
                EventDetailsAr = "فتح صفحة تعديل هذا المستخدم" + model.FirstName + " " + model.LastName
            });
            _uow.Save();
            return PartialView("_UpdateUser", model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateUser(UpdateUserModel usermodel, int ID, bool? update)
        {

            if (!ModelState.IsValid)
            {
                return PartialView("_UpdateUser", usermodel);
            }
            Users userEntityToEdit = _uow.Useres.Find(ID);
            userEntityToEdit.FirstName = usermodel.FirstName;
            userEntityToEdit.LastName = usermodel.LastName;
            userEntityToEdit.Email = usermodel.Email;
            userEntityToEdit.IsActive = usermodel.IsActive;
            var islogout = userEntityToEdit.RoleID != usermodel.RoleID;
            userEntityToEdit.RoleID = usermodel.RoleID;
            userEntityToEdit.Telephone = usermodel.Telephone;
            userEntityToEdit.ModifiedBy = SessionManager.CurrentUser.UserId;
            userEntityToEdit.ModifiedAt = DateTime.Now;
            
            if (usermodel.Departments != null)
            {
                foreach (var VARIABLE in userEntityToEdit.UserDepartment.ToList())
                {
                    _uow.UserDepartments.Delete(VARIABLE.Id);
                }

                foreach (var VARIABLE in usermodel.Departments)
                {
                    userEntityToEdit.UserDepartment.Add(new UserDepartment()
                    {
                        DepartmentId = VARIABLE,
                        UserId = userEntityToEdit.UserId
                    });
                }
            }


            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 2,
                EventTime = DateTime.Now,
                EventDetails = "Updated this User  " + usermodel.FirstName + " " + usermodel.LastName,
                EventDetailsAr = "قام بتعديل بيانات هذا المستخدم " + usermodel.FirstName + " " + usermodel.LastName
            });
            _uow.Useres.Edit(userEntityToEdit.UserId, userEntityToEdit);
            _uow.Save();
            if (islogout && userEntityToEdit.UserId == SessionManager.CurrentUser.UserId)
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("Login", "Login");
            }
            var url = (User.IsInRole("Admin")) ? Url.Action("List", "Users") : Url.Action("CreateGenerateRefrence", "GeneratedReference");
            return Json(new { success = true, url = url });
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var existing = _uow.Useres.Find(id.Value);
            if (existing == null)
            {
                return HttpNotFound();
            }
            var model = new UsersModel() { UserId = existing.UserId };
            return PartialView("_DeleteUser", model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int ID)
        {
            Users existing = _uow.Useres.Find(ID);
            existing.IsDeleted = true;

            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 3,
                EventTime = DateTime.Now,
                EventDetails = "Deleted this User  " + existing.FirstName + " " + existing.LastName,
                EventDetailsAr = "قام بحذف هذا المستخدم " + existing.FirstName + " " + existing.LastName
            });
            _uow.Useres.Edit(existing.UserId, existing);
            _uow.Save();
            string url = Url.Action("List", "Users");
            return Json(new { success = true, url = url });
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin,User")]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var existing = _uow.Useres.Find(SessionManager.CurrentUser.UserId);
            if (existing == null)
            {
                return HttpNotFound();
            }


            if (ModelState.IsValid)
            {
                string oldPassword = StringCipher.Decrypt(existing.Password, WebConfigurationManager.AppSettings["EncDecKey"]);
                if (oldPassword != model.OldPassword)
                {
                    ModelState.AddModelError("OldPassword", "it's not your old password");
                    return View();
                }
                existing.Password = StringCipher.Encrypt(model.ConfirmPassword,
                    WebConfigurationManager.AppSettings["EncDecKey"]);
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 14,
                    EventTime = DateTime.Now,
                    EventDetails = "Changed his Passowrd",
                    EventDetailsAr = "قام بتغيير كلمة مروره السرية"
                });
                _uow.Useres.Edit(existing.UserId, existing);
                _uow.Save();
               
                return RedirectToAction("CreateGenerateRefrence", "GeneratedReference");
            }
            return View(model);
        }

    }
}