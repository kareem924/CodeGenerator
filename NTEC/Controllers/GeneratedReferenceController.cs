using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using System.Web.Mvc;
using NTEC.Classes.Helpers;
using NTEC.Models;
using NTEC.Models.GenerateReference;
using PagedList;
using NTEC.Classes.Security;
using NTEC.Models.Entities;
using NTEC.Models.UnitOfWork;

namespace NTEC.Controllers
{
    [Authorize]
    public class GeneratedReferenceController : BaseController
    {
        IUnitOfWork _uow;

        public GeneratedReferenceController()
        {
            _uow = new UnitOfWork();
        }
        #region Controllers
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            var generatedReference = from s in _uow.GeneratedReferences.List()
                                     orderby s.CreatedAt
                                     where (s.IsAssigned == false)
                                     select s;
            int pageSize = int.Parse(WebConfigurationManager.AppSettings["PagingSize"]);
            int pageNumber = (page ?? 1);
            var onePageOfGeneratedReference = generatedReference.ToPagedList(pageNumber, pageSize);
            ViewBag.OnePageOfGeneratedReference = onePageOfGeneratedReference;
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Visited UnAssigned Pool page"
            });
            _uow.Save();
            return View();
        }
        public ActionResult IncomingIndex(int? page)
        {

            if (SessionManager.CurrentUser.IsSysAdmin)
            {
                var generatedReference = from s in _uow.GeneratedReferences.List()
                                         orderby s.CreatedAt descending
                                         where (s.IsDeleted != true && s.IsAssigned != false && s.CorrespondentId == 1)
                                         select s;

                int pageSize = int.Parse(WebConfigurationManager.AppSettings["PagingSize"]);
                int pageNumber = (page ?? 1);
                var onePageOfGeneratedReference = generatedReference.ToPagedList(pageNumber, pageSize);
                ViewBag.OnePageOfGeneratedReference = onePageOfGeneratedReference;
                return PartialView("_IncomingIndex");
            }
            else
            {
                var generatedReference = from s in _uow.GeneratedReferences.List()
                                         orderby s.CreatedAt descending
                                         where (s.IsDeleted != true && s.IsAssigned == true && s.CorrespondentId == 1 && s.CreatedBy == SessionManager.CurrentUser.UserId)
                                         select s;

                int pageSize = int.Parse(WebConfigurationManager.AppSettings["PagingSize"]);
                int pageNumber = (page ?? 1);
                var onePageOfGeneratedReference = generatedReference.ToPagedList(pageNumber, pageSize);
                ViewBag.OnePageOfGeneratedReference = onePageOfGeneratedReference;
                return PartialView("_IncomingIndex");
            }
        }
        public ActionResult OutgoingIndex(int? page)
        {
            if (SessionManager.CurrentUser.IsSysAdmin)
            {
                var generatedReference = from s in _uow.GeneratedReferences.List()
                                         orderby s.CreatedAt descending
                                         where (s.IsDeleted != true && s.IsAssigned != false && s.CorrespondentId == 2)
                                         select s;
                int pageSize = int.Parse(WebConfigurationManager.AppSettings["PagingSize"]);
                int pageNumber = (page ?? 1);
                var onePageOfGeneratedReference = generatedReference.ToPagedList(pageNumber, pageSize);
                ViewBag.OnePageOfGeneratedReferenceOutgoing = onePageOfGeneratedReference;
                return PartialView("_OutgoingIndex");
            }
            else
            {
                var generatedReference = from s in _uow.GeneratedReferences.List()
                                         orderby s.CreatedAt descending
                                         where (s.IsDeleted != true && s.IsAssigned == true && s.CorrespondentId == 2 && s.CreatedBy == SessionManager.CurrentUser.UserId)
                                         select s;
                int pageSize = int.Parse(WebConfigurationManager.AppSettings["PagingSize"]);
                int pageNumber = (page ?? 1);
                var onePageOfGeneratedReference = generatedReference.ToPagedList(pageNumber, pageSize);
                ViewBag.OnePageOfGeneratedReferenceOutgoing = onePageOfGeneratedReference;
                return PartialView("_OutgoingIndex");
            }

        }
        [HttpGet]
        public ViewResult CreateGenerateRefrence()
        {
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Visited Create Generate Reference",
                EventDetailsAr = "قام بزيارة صفحة انشاء مرجع جديد"
            });
            _uow.Save();
            return View();
        }
        [HttpPost]
        public ActionResult CreateGenerateRefrence(GenerateRefrenceModel model)
        {
            if (!ModelState.IsValid)
            {
                var sd = ModelState.Values;
                return Json(new {success = false});
            }
            var generatedCode = GenerateNewRefrence(model.CompanyName, model.CompanyNameAr, model.DepartmentId, model.Incoming);

            model.GeneratedCodeEn = generatedCode.GeneratedCodeEn;
            model.GeneratedCodeAr = generatedCode.GeneratedCodeAr;
            if (generatedCode.IsUnAssignedRef)
            {
                var toDeletedReference = _uow.GeneratedReferences.Find(generatedCode.UnAssignedId);

                var updGenerated = new GeneratedReference
                {
                    CompanyName = model.CompanyName,
                    CompanyNameAr = model.CompanyNameAr,
                    DepartmentId = model.DepartmentId,
                    GeneratedCodeAr = model.GeneratedCodeAr,
                    GeneratedCodeEn = model.GeneratedCodeEn,
                    IsAssigned = true,
                    CorrespondentId = model.Incoming == "Incoming" ? 1 : 2,
                    CreatedBy = SessionManager.CurrentUser.UserId,
                    CreatedAt = DateTime.Now
                };
                updGenerated.AuditTrial.Add(new AuditTrial()
                {
                    EventTypeId = 12,
                    UserId = SessionManager.CurrentUser.UserId,
                    EventTime = DateTime.Now,
                    EventDetails = "This User Created new reference based on this Unassigned Reference " + toDeletedReference.GeneratedCodeEn,
                    EventDetailsAr = "هذا المستخدم قام بنشاء مرجع جديد بنائاً علي هذا المرجع الملغي تعيينه " + toDeletedReference.GeneratedCodeAr

                });
                _uow.GeneratedReferences.Add(updGenerated);
                toDeletedReference.IsDeleted = true;
                _uow.AutitTrails.Add(new AuditTrial()
                {
                    UserId = SessionManager.CurrentUser.UserId,
                    ReferenceId = toDeletedReference.GeneratedReferenceId,
                    EventTypeId = 13,
                    EventTime = DateTime.Now,
                    EventDetails = "This reference has been deleted after you reused this number in new generated reference",
                    EventDetailsAr = "هذا المرجع تم حذفه بعد ما استخدم ترقيمه في مرجع جديد"
                });
                _uow.Save();
                ModelState.Clear();

                return Json(new { success = true, GeneratedCodeAr = model.GeneratedCodeAr, GeneratedCodeEn = model.GeneratedCodeEn }, JsonRequestBehavior.AllowGet);
            }
            var newGenerated = new GeneratedReference
            {
                CompanyName = model.CompanyName,
                CompanyNameAr = model.CompanyNameAr,
                DepartmentId = model.DepartmentId,
                GeneratedCodeAr = model.GeneratedCodeAr,
                GeneratedCodeEn = model.GeneratedCodeEn,
                IsAssigned = true,
                CorrespondentId = model.Incoming == "Incoming" ? 1 : 2,
                CreatedBy = SessionManager.CurrentUser.UserId,
                CreatedAt = DateTime.Now
            };
            newGenerated.AuditTrial.Add(new AuditTrial()
            {
                EventTypeId = 10,
                UserId = SessionManager.CurrentUser.UserId,
                EventTime = DateTime.Now,
                EventDetails = "This User Created this new reference",
                EventDetailsAr = "هذا المستخدم قام بانشاء مرجع جديد"
            });
            _uow.GeneratedReferences.Add(newGenerated);
            var lastnumber = 0;
            string lastYear = null;
            if (model.Incoming == "Outgoing")
            {


                var generatedCodeSplitting = model.GeneratedCodeEn.Split('/');
                lastnumber = int.Parse(generatedCodeSplitting[1]);
                var refNoCountToUpdate = _uow.RefNoCounts.List().FirstOrDefault(x => x.IsCurrentYearxx == true && x.CorrespondentTypeID == 2);
                if (refNoCountToUpdate != null)
                {
                    refNoCountToUpdate.StartNumber = lastnumber;
                }
            }
            if (model.Incoming == "Incoming")
            {
                var generatedCodeSplitting = model.GeneratedCodeEn.Split(new Char[] { '/', '-' },
                    StringSplitOptions.RemoveEmptyEntries);
                lastnumber = int.Parse(generatedCodeSplitting[2]);
                var refNoCountToUpdate = _uow.RefNoCounts.List().FirstOrDefault(x => x.IsCurrentYearxx == true && x.CorrespondentTypeID == 1);
                if (refNoCountToUpdate != null)
                {
                    refNoCountToUpdate.StartNumber = lastnumber;
                }
            }
            _uow.Save();
            ModelState.Clear();

            return Json(new { success = true, GeneratedCodeAr = model.GeneratedCodeAr, GeneratedCodeEn = model.GeneratedCodeEn });
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var generatedRefrence = _uow.GeneratedReferences.Find(id.Value);
            var model = new GenerateRefrenceModel()
            {
                EditDepartmentId = generatedRefrence.DepartmentId,
                GeneratedCodeAr = generatedRefrence.GeneratedCodeAr,
                GeneratedCodeEn = generatedRefrence.GeneratedCodeEn,
                GeneratedReferenceId = generatedRefrence.GeneratedReferenceId
            };
            _uow.AutitTrails.Add(new AuditTrial()
            {
                UserId = SessionManager.CurrentUser.UserId,
                EventTypeId = 4,
                EventTime = DateTime.Now,
                EventDetails = "Visited Reassign Generate Reference Modal",
                EventDetailsAr = "قام بزيارة صفحة الغاء تعيين مرجع"
            });
            _uow.Save();
            return PartialView("_Edit", model);
        }
        [HttpPost]
        public ActionResult Edit(GenerateRefrenceModel model)
        {
            if (ModelState.IsValid)
            {
                var generatedRefrence = _uow.GeneratedReferences.Find(model.GeneratedReferenceId);
                var departmentShortCode = _uow.Departments.Find(model.EditDepartmentId);
                var ci = new CultureInfo("ar-EG");
                if (generatedRefrence.CorrespondentId == 1)
                {
                    var oldReferenceEn = generatedRefrence.GeneratedCodeEn.Split(new char[] { '/', '-' },
                            StringSplitOptions.RemoveEmptyEntries);
                    var generatedCodedEn = new StringBuilder();
                    generatedCodedEn.Append(oldReferenceEn[0]).Append("/").Append("R-").Append(oldReferenceEn[2]).Append("/").Append(departmentShortCode.ShortcutEn).Append("/").Append(oldReferenceEn[4]);
                    var oldReferenceAr = generatedRefrence.GeneratedCodeAr.Split(new Char[] { '/', '-' },
                            StringSplitOptions.RemoveEmptyEntries);
                    var generatedCodedAr = new StringBuilder();
                    generatedCodedAr.Append(oldReferenceAr[0]).Append("/").Append("و-").Append(CultureHelper.ConvertToEasternArabicNumerals(int.Parse(oldReferenceEn[2]).ToString("0000", ci))).Append("/").Append(departmentShortCode.ShortcutAr).Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(int.Parse(oldReferenceEn[4]).ToString("00", ci)));
                    generatedRefrence.GeneratedCodeEn = generatedCodedEn.ToString();
                    generatedRefrence.GeneratedCodeAr = generatedCodedAr.ToString();
                    generatedRefrence.DepartmentId = model.EditDepartmentId;
                    generatedRefrence.IsAssigned = true;
                    _uow.AutitTrails.Add(new AuditTrial()
                    {
                        UserId = SessionManager.CurrentUser.UserId,
                        EventTypeId = 12,
                        EventTime = DateTime.Now,
                        ReferenceId = model.GeneratedReferenceId,
                        EventDetails = "this reference " + '"' + model.GeneratedCodeEn + '"' + "has been reassigned with new Department,it's now this one" + '"' + generatedCodedEn.ToString() + '"'
                    });
                    _uow.GeneratedReferences.Edit(generatedRefrence.GeneratedReferenceId, generatedRefrence);
                    _uow.Save();
                    return Json(new { success = true, type = 1, Encode = generatedCodedEn.ToString(), ArCode = generatedCodedAr.ToString(), depid = model.EditDepartmentId });

                }
                else
                {
                    var oldReferenceEn = generatedRefrence.GeneratedCodeEn.Split('/');
                    var generatedCodedEn = new StringBuilder();
                    generatedCodedEn.Append(oldReferenceEn[0]).Append("/").Append(oldReferenceEn[1]).Append("/").Append(departmentShortCode.ShortcutEn).Append("/").Append(oldReferenceEn[3]);
                    var oldReferenceAr = generatedRefrence.GeneratedCodeAr.Split('/');
                    var generatedCodedAr = new StringBuilder();
                    generatedCodedAr.Append(oldReferenceAr[0]).Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(int.Parse(oldReferenceEn[1]).ToString("0000", ci))).Append("/").Append(departmentShortCode.ShortcutAr).Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(int.Parse(oldReferenceEn[3]).ToString("00", ci)));
                    generatedRefrence.GeneratedCodeEn = generatedCodedEn.ToString();
                    generatedRefrence.GeneratedCodeAr = generatedCodedAr.ToString();
                    generatedRefrence.DepartmentId = model.EditDepartmentId;
                    generatedRefrence.IsAssigned = true;
                    _uow.AutitTrails.Add(new AuditTrial()
                    {
                        UserId = SessionManager.CurrentUser.UserId,
                        EventTypeId = 12,
                        EventTime = DateTime.Now,
                        ReferenceId = model.GeneratedReferenceId,
                        EventDetails = "this reference " + '"' + model.GeneratedCodeEn + '"' + "has been reassigned with new Department,it's now this one" + '"' + generatedCodedEn.ToString() + '"'
                    });
                    _uow.GeneratedReferences.Edit(generatedRefrence.GeneratedReferenceId, generatedRefrence);
                    _uow.Save();
                    return Json(new { success = true, type = 2, Encode = generatedCodedEn.ToString(), ArCode = generatedCodedAr.ToString(), depid = model.EditDepartmentId });
                }
            }
            return Json(new { success = false, message = "There is a wrong in your " });
        }
        public ActionResult GetGenerated(int id)
        {
            var generatedReference = _uow.GeneratedReferences.Find(id);
            var model = new UnassignModel()
            {
                GeneratedCodeEn = generatedReference.GeneratedCodeEn,
                Id = generatedReference.GeneratedReferenceId,
            };
            return PartialView("_RenderNotificationsPreview", model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Unassign(UnassignModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var genereatedToDelete = _uow.GeneratedReferences.Find(model.Id);
                    if (genereatedToDelete.CorrespondentId == 1)
                    {
                        var oldReferenceEn = genereatedToDelete.GeneratedCodeEn.Split(new char[] { '/', '-' },
                                StringSplitOptions.RemoveEmptyEntries);
                        genereatedToDelete.ReferenceNumber = int.Parse(oldReferenceEn[2]);
                    }
                    else
                    {
                        var oldReferenceEn = genereatedToDelete.GeneratedCodeEn.Split('/');
                        genereatedToDelete.ReferenceNumber = int.Parse(oldReferenceEn[1]);

                    }
                    genereatedToDelete.IsAssigned = false;
                    _uow.AutitTrails.Add(new AuditTrial()
                    {
                        EventDetails = "Ref . No " + genereatedToDelete.GeneratedCodeEn + " has been unAssigned from Department " + genereatedToDelete.Department.NameEn + " and Company " + genereatedToDelete.CompanyName + " For " + model.UnassignReason,
                        EventDetailsAr = "مرجع رقم " + genereatedToDelete.GeneratedCodeAr + " تم الغاء تعيينه من قسم " + genereatedToDelete.Department.NameAr + " ومن شركة  " + genereatedToDelete.CompanyName + " بسبب " + model.UnassignReason,
                        ReferenceId = model.Id,
                        UserId = SessionManager.CurrentUser.UserId,
                        EventTime = DateTime.Now,
                        EventTypeId = 11
                    });
                    _uow.GeneratedReferences.Edit(genereatedToDelete.GeneratedReferenceId, genereatedToDelete);
                    _uow.Save();
                  

                    return RedirectToAction("CreateGenerateRefrence");
                }
            }
            catch (System.Exception)
            {
               

                return RedirectToAction("CreateGenerateRefrence");

            }
           

            return RedirectToAction("CreateGenerateRefrence");
        }
        #endregion
        #region functions
        public JsonResult GetDepartmentsForGenerating(string text)
        {
            var deps = _uow.Departments.List().Where(x => x.IsDeleted != true && x.UserDepartment.Any(u=>u.UserId==SessionManager.CurrentUser.UserId) ).ToList().Select(item => new DepartmentsModel
            {

                NameEn = item.NameEn,
                DepartmentId = item.DepartmentId,
                NameAr = item.NameAr
            }).ToList();

            if (!string.IsNullOrEmpty(text))
            {
                deps = deps.Where(p => p.NameEn.Contains(text)).Select(item => new DepartmentsModel
                {
                    NameEn = item.NameEn,
                    DepartmentId = item.DepartmentId,
                    NameAr = item.NameAr
                }).ToList();
            }
            return Json(deps, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDepartments(string text)
        {
            var deps = _uow.Departments.List().Where(x => x.IsDeleted != true ).ToList().Select(item => new DepartmentsModel
            {

                NameEn = item.NameEn,
                DepartmentId = item.DepartmentId,
                NameAr = item.NameAr
            }).ToList();

            if (!string.IsNullOrEmpty(text))
            {
                deps = deps.Where(p => p.NameEn.Contains(text)).Select(item => new DepartmentsModel
                {
                    NameEn = item.NameEn,
                    DepartmentId = item.DepartmentId,
                    NameAr = item.NameAr
                }).ToList();
            }
            return Json(deps, JsonRequestBehavior.AllowGet);
        }

        public GeneratedCode GenerateNewRefrence(string companyName, string companyNameAr, int depId, string correspondentType)
        {
            CultureInfo ci;
            int date;
            GeneratedCode generatedCodeObj;
            DepartmentsModel depShortCode;
            C_RefNoCount cRefNoCount;
            UpdatePublicYEar();
            if (correspondentType == "Outgoing")
            {
                ci = new CultureInfo("ar-EG");
                date = DateTime.Now.Year - (DateTime.Now.Year / 100 * 100);
                generatedCodeObj = new GeneratedCode();
                depShortCode = _uow.Departments.List().Where(p => p.DepartmentId == depId).Select(item => new DepartmentsModel
                {
                    ShortcutEn = item.ShortcutEn,
                    ShortcutAr = item.ShortcutAr,
                    DepartmentId = item.DepartmentId,
                }).FirstOrDefault();
                var unAssignedReference =
                    _uow.GeneratedReferences.List().ToList().Where(x => x.IsAssigned == false && x.IsDeleted != true && x.CorrespondentId == 2 && x.CreatedAt.Value.Year==DateTime.Now.Year).OrderBy(x => x.ReferenceNumber).FirstOrDefault();
                if (unAssignedReference != null && depShortCode != null)
                {
                    var unAssignednumber = unAssignedReference.GeneratedCodeEn.Split('/');

                    var number = int.Parse(unAssignednumber[1]);
                    var generatedCodedEn = new StringBuilder();
                    generatedCodedEn.Append("NTEC").Append("/").Append(number.ToString("0000")).Append("/").Append(depShortCode.ShortcutEn).Append("/").Append(date);
                    generatedCodeObj.GeneratedCodeEn = generatedCodedEn.ToString();
                    var generatedCodedAr = new StringBuilder();
                    generatedCodedAr.Append("ش و ت").Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(number.ToString("0000", ci))).Append("/").Append(depShortCode.ShortcutAr).Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(date.ToString("00", ci)));
                    generatedCodeObj.GeneratedCodeAr = generatedCodedAr.ToString();
                    generatedCodeObj.IsUnAssignedRef = true;
                    generatedCodeObj.UnAssignedId = unAssignedReference.GeneratedReferenceId;
                    return generatedCodeObj;
                }
                cRefNoCount = _uow.RefNoCounts.List().ToList().LastOrDefault(x => x.CorrespondentTypeID == 2 && x.IsCurrentYearxx == true);
                if (cRefNoCount != null && depShortCode != null)
                {

                    var number = cRefNoCount.StartNumber + 1;
                    var generatedCodedEn = new StringBuilder();
                    generatedCodedEn.Append("NTEC").Append("/").Append(number.ToString("0000")).Append("/").Append(depShortCode.ShortcutEn).Append("/").Append(date);
                    generatedCodeObj.GeneratedCodeEn = generatedCodedEn.ToString();
                    var generatedCodedAr = new StringBuilder();
                    generatedCodedAr.Append("ش و ت").Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(number.ToString("0000", ci))).Append("/").Append(depShortCode.ShortcutAr).Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(date.ToString("00", ci)));
                    generatedCodeObj.GeneratedCodeAr = generatedCodedAr.ToString();
                }
                return generatedCodeObj;
            }
            ci = new CultureInfo("ar-EG");
            date = DateTime.Now.Year - (DateTime.Now.Year / 100 * 100);
            generatedCodeObj = new GeneratedCode();
            depShortCode = _uow.Departments.List().Where(p => p.DepartmentId == depId).Select(item => new DepartmentsModel
            {
                ShortcutEn = item.ShortcutEn,
                ShortcutAr = item.ShortcutAr,
                DepartmentId = item.DepartmentId,
            }).FirstOrDefault();
            var unAssignedOutgoingReference =
                   _uow.GeneratedReferences.List().ToList().Where(x => x.IsAssigned == false && x.IsDeleted != true && x.CorrespondentId == 1 && x.CreatedAt.Value.Year == DateTime.Now.Year).OrderBy(x => x.ReferenceNumber).FirstOrDefault();
            if (unAssignedOutgoingReference != null && depShortCode != null)
            {
                var unAssignednumber = unAssignedOutgoingReference.GeneratedCodeEn.Split(new Char[] { '/', '-' },
                            StringSplitOptions.RemoveEmptyEntries);
                var number = int.Parse(unAssignednumber[2]);
                var generatedCodedEn = new StringBuilder();
                generatedCodedEn.Append("NTEC").Append("/").Append("R-").Append(number.ToString("0000")).Append("/").Append(depShortCode.ShortcutEn).Append("/").Append(date);
                generatedCodeObj.GeneratedCodeEn = generatedCodedEn.ToString();
                var generatedCodedAr = new StringBuilder();
                generatedCodedAr.Append("ش و ت").Append("/").Append("و-").Append(CultureHelper.ConvertToEasternArabicNumerals(number.ToString("0000", ci))).Append("/").Append(depShortCode.ShortcutAr).Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(date.ToString("00", ci)));
                generatedCodeObj.GeneratedCodeAr = generatedCodedAr.ToString();
                generatedCodeObj.IsUnAssignedRef = true;
                generatedCodeObj.UnAssignedId = unAssignedOutgoingReference.GeneratedReferenceId;
                generatedCodeObj.Isupdated = true;
                return generatedCodeObj;
            }
            cRefNoCount = _uow.RefNoCounts.List().ToList().LastOrDefault(x => x.CorrespondentTypeID == 1 && x.IsCurrentYearxx == true);
            if (cRefNoCount != null && depShortCode != null)
            {
                var number = cRefNoCount.StartNumber + 1;
                var generatedCodedEn = new StringBuilder();
                generatedCodedEn.Append("NTEC").Append("/").Append("R-").Append(number.ToString("0000")).Append("/").Append(depShortCode.ShortcutEn).Append("/").Append(date);
                generatedCodeObj.GeneratedCodeEn = generatedCodedEn.ToString();
                var generatedCodedAr = new StringBuilder();
                generatedCodedAr.Append("ش و ت").Append("/").Append("و-").Append(CultureHelper.ConvertToEasternArabicNumerals(number.ToString("0000", ci))).Append("/").Append(depShortCode.ShortcutAr).Append("/").Append(CultureHelper.ConvertToEasternArabicNumerals(date.ToString("00", ci)));

                generatedCodeObj.GeneratedCodeAr = generatedCodedAr.ToString();
            }
            return generatedCodeObj;
        }

        public void UpdatePublicYEar()
        {
            var currentDate = DateTime.Now.Year - (DateTime.Now.Year / 100 * 100);
            var updateYearEntity = _uow.RefNoCounts.List().ToList().Where(x => int.Parse(x.Year) == int.Parse(currentDate.ToString())).ToList();
            var firstOrDefault = updateYearEntity.FirstOrDefault();
            if (firstOrDefault != null && (updateYearEntity.Any() && firstOrDefault.IsCurrentYearxx == false))
            {
                foreach (var variable in updateYearEntity)
                {
                    variable.IsCurrentYearxx = true;
                }
                foreach (var variable in _uow.RefNoCounts.List().ToList().Where(x => int.Parse(x.Year) != int.Parse(currentDate.ToString())).ToList())
                {
                    variable.IsCurrentYearxx = false;
                }
                _uow.Save();
            }
            else if (!updateYearEntity.Any())
            {
                foreach (var variable in _uow.RefNoCounts.List())
                {
                    variable.IsCurrentYearxx = false;
                }
                _uow.RefNoCounts.Add(new C_RefNoCount() { IsCurrentYearxx = true, Year = currentDate.ToString(), StartNumber = 0, CorrespondentTypeID = 1 });
                _uow.RefNoCounts.Add(new C_RefNoCount() { IsCurrentYearxx = true, Year = currentDate.ToString(), StartNumber = 0, CorrespondentTypeID = 2 });
                _uow.Save();
            }
        }
        #endregion
    }
}