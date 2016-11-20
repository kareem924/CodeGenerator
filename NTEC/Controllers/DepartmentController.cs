using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NTEC.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Data.Entity;
using PagedList;

namespace NTEC.Controllers
{
    public class DepartmentController : Controller
    {
        NTECEntities2 NT = new NTECEntities2();
        //
        // GET: /Departments2/        
        public ActionResult Index(int? page)
        {

            var Quary = from s in NT.Department
                        orderby s.CreatedAt
                        select s;
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    generatedReference = (IOrderedQueryable<GeneratedReference>)generatedReference.Where(s => s.CompanyName.Contains(searchString)
            //                                                                           || s.GeneratedCodeEn.Contains(searchString)
            //                                                                           || s.GeneratedCodeAr.Contains(searchString));
            //}

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var onePageOfUser = Quary.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfGeneratedReference = onePageOfUser;
            return View();
        }

        public JsonResult GetData([DataSourceRequest] DataSourceRequest request)
        {
            List<DepartmentsModel> departlst = new List<DepartmentsModel>();
            var lst = NT.Department;
            foreach (var depts in lst)
            {
                var departments = new DepartmentsModel
                {
                    DepartmentId = depts.DepartmentId,
                    NameAr = depts.NameAr,
                    NameEn = depts.NameEn,
                    ShortcutAr = depts.ShortcutAr,
                    ShortcutEn = depts.ShortcutEn
                };
                //Departments.CreatedAt = Depts.CreatedAt;
                //Departments.CreatedBy = Depts.CreatedBy;
                //Departments.ModifiedAt = Depts.ModifiedAt;
                //Departments.ModifiedBy = Depts.ModifiedBy;

                departlst.Add(departments);
                NT.SaveChanges();
            }

            //Fill Distribution Method Object

            return Json(departlst.ToDataSourceResult(request));


        }


        [HttpGet]
        public ViewResult CreateDepartment()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartment(DepartmentsModel model)
        {
            if (ModelState.IsValid)
            {

                var departmententity = new Department();

                departmententity.NameEn = model.NameEn;
                departmententity.ShortcutEn = model.ShortcutEn;
                departmententity.NameAr = model.NameAr;
                departmententity.ShortcutAr = model.ShortcutAr;

                NT.Department.Add(departmententity);
                NT.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public ActionResult UpdateDepartment(int ID)
        {

            var objstudentdetail = (from data in NT.Department
                                    where data.DepartmentId == ID
                                    select data).FirstOrDefault();

            DepartmentsModel model = new DepartmentsModel();

            model.DepartmentId = objstudentdetail.DepartmentId;
            model.NameEn = objstudentdetail.NameEn;
            model.ShortcutEn = objstudentdetail.ShortcutEn;
            model.NameAr = objstudentdetail.NameAr;
            model.ShortcutAr = objstudentdetail.ShortcutAr;
           

            return View(model);
        }

        [HttpPost]
        public ActionResult UpdateDepartment(DepartmentsModel Deptmodel, int ID)
        {
            Department objdstudent = NT.Department.First(m => m.DepartmentId == ID);
            objdstudent.NameEn = Deptmodel.NameEn;
            objdstudent.ShortcutEn = Deptmodel.ShortcutEn;
            objdstudent.NameAr = Deptmodel.NameAr;
            objdstudent.ShortcutAr = Deptmodel.ShortcutAr;
           
            /*Save data to database*/
       
                NT.SaveChanges();

           
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DeleteDepartment(int ID)
        {
         
            Department objstudentdetail = new Department();
            objstudentdetail = (from data in NT.Department
                                where data.DepartmentId == ID
                                select data).FirstOrDefault();
            DepartmentsModel model = new DepartmentsModel();

            model.DepartmentId = objstudentdetail.DepartmentId;
            model.NameEn = objstudentdetail.NameEn;
            model.ShortcutEn = objstudentdetail.ShortcutEn;
            model.NameAr = objstudentdetail.NameAr;
            model.ShortcutAr = objstudentdetail.ShortcutAr;

           
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteDepartment(DepartmentsModel Deptmodel, int ID)
        {
           
            Department dept = new Department();
            dept.DepartmentId = ID;
            NT.Department.Attach(dept);
            NT.Department.Remove(dept);
            /*Save data to database*/
            NT.SaveChanges();
            return RedirectToAction("/Index/");
            // return RedirectToAction("Index");
          
        }  
	}
}