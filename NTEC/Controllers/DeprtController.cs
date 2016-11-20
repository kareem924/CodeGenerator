using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTEC.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity;
using System.Net;
using System.Web.Configuration;
using NTEC.Classes.Security;
using NTEC.Models.Entities;
using NTEC.Models.UnitOfWork;
using PagedList;

namespace NTEC.Controllers
{
    [Authorize]
    public class DeprtController : BaseController
    {
        IUnitOfWork _uow;
        // GET: /Departments/
        public DeprtController()
        {
            _uow = new UnitOfWork();
        }
        public ActionResult Index()
        {
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Open Department Index",
                EventDetailsAr = "زار صفحة الرئيسية للاقسام"
            });
            _uow.Save();
            return View();
        }

        public ActionResult List(int? page)
        {

            var Quary = from s in _uow.Departments.List()
                        orderby s.CreatedAt descending
                        where s.IsDeleted != true
                        select s;


            int pageSize = int.Parse(WebConfigurationManager.AppSettings["PagingSize"]);
            int pageNumber = (page ?? 1);
            var onePageOfUser = Quary.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfGeneratedReference = onePageOfUser;

            return PartialView("_DeprtList");
        }



        [HttpGet]
        public PartialViewResult CreateDepartment()
        {
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Open Create New Department",
                EventDetailsAr = "زار صفحة انشاء قسم جديد"
            });
            _uow.Save();
            return PartialView("_CreateDepartment");
        }
        [HttpPost]
        public ActionResult CreateDepartment(DepartmentsModel model)
        {

            if (ModelState.IsValid)
            {
                var uniquedepartment = _uow.Departments.List().Where(x => x.NameEn == model.NameEn && x.IsDeleted != true);
                if (uniquedepartment.Any())
                {
                    ModelState.AddModelError("NameEn", "This Department exists,Please choose another one");
                    return PartialView("_CreateDepartment");
                }
                var departmententity = new Department();
                departmententity.NameEn = model.NameEn;
                departmententity.ShortcutEn = model.ShortcutEn;
                departmententity.NameAr = model.NameAr;
                departmententity.ShortcutAr = model.ShortcutAr;
                departmententity.CreatedAt = DateTime.Now;
                departmententity.CreatedBy = SessionManager.CurrentUser.UserId;
                _uow.Departments.Add(departmententity);
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 5,
                    EventTime = DateTime.Now,
                    EventDetails = "Created this Department  " + " " + model.NameEn,
                    EventDetailsAr = " انشيء هذا القسم " + " " + model.NameAr
                });
                _uow.Save();
                string url = Url.Action("List", "Deprt");
                return Json(new { success = true, url = url });
            }
            return PartialView("_CreateDepartment");
        }



        [HttpGet]
        public ActionResult UpdateDepartment(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var departEntity = _uow.Departments.Find(id);
            if (departEntity == null)
            {
                return HttpNotFound();
            }
            var model = new DepartmentsModel
            {
                DepartmentId = departEntity.DepartmentId,
                NameEn = departEntity.NameEn,
                ShortcutEn = departEntity.ShortcutEn,
                NameAr = departEntity.NameAr,
                ShortcutAr = departEntity.ShortcutAr
            };
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Open Edit  Department " + " " + model.NameEn,
                EventDetailsAr = "قام بزيارة تعديل هذا القسم " + " " + model.NameAr
            });
            _uow.Save();
            return PartialView("_UpdateDepartment", model);
        }
        [HttpPost]
        public ActionResult UpdateDepartment(DepartmentsModel deptmodel)
        {
            if (ModelState.IsValid)
            {
                if (deptmodel.DepartmentId != null)
                {
                    Department depEntity = _uow.Departments.Find(deptmodel.DepartmentId.Value);
                    depEntity.NameEn = deptmodel.NameEn;
                    depEntity.ShortcutEn = deptmodel.ShortcutEn;
                    depEntity.NameAr = deptmodel.NameAr;
                    depEntity.ShortcutAr = deptmodel.ShortcutAr;
                    depEntity.ModifiedAt = DateTime.Now;
                    depEntity.ModifiedBy = SessionManager.CurrentUser.UserId;
                    _uow.Departments.Edit(depEntity.DepartmentId, depEntity);
                }
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTypeId = 6,
                    EventTime = DateTime.Now,
                    EventDetails = "Updated this Department" + " " + deptmodel.NameEn,
                    EventDetailsAr = "قام بتعديل هذا القسم " + " " + deptmodel.NameAr
                });

                _uow.Save();

                string url = Url.Action("List", "Deprt");
                return Json(new { success = true, url = url });
            }

            return PartialView("_UpdateDepartment", deptmodel);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department existing = _uow.Departments.Find(id.Value);
            if (existing == null)
            {
                return HttpNotFound();
            }
            var model = new DepartmentsModel() { DepartmentId = existing.DepartmentId };
            return PartialView("_Delete", model);
        }

        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int departmentId)
        {
            Department existing = _uow.Departments.Find(departmentId);
            existing.IsDeleted = true;
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 7,
                EventTime = DateTime.Now,
                EventDetails = "Deleted this Department" + " " + existing.NameEn,
                EventDetailsAr = "قام بحذف هذا القسم " + " " + existing.NameAr
            });
            _uow.Departments.Edit(existing.DepartmentId, existing);
            _uow.Save();

            string url = Url.Action("List", "Deprt");
            return Json(new { success = true, url = url });

        }

    }
}
