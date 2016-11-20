using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NTEC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Logging()
        {
            return Redirect("~/Reports/Views/LogginHistoryView.aspx");
        }
        public ActionResult FullAudit()
        {
            return Redirect("~/Reports/Views/FullAuditLogs.aspx");
        }
        public ActionResult ReferenceUsrDprts()
        {
            return Redirect("~/Reports/Views/ReferenceUsrDprts.aspx");
        }
        public ActionResult ReferenceForSpecific()
        {
            return Redirect("~/Reports/Views/ReferenceForSpecific.aspx");
        }
        public ActionResult NumberOfGeneratedRefrence()
        {
            return Redirect("~/Reports/Views/NumberOFReference.aspx");
        }
        public ActionResult CountOfGeneratedRefrence()
        {
            return Redirect("~/Reports/Views/RefCounter.aspx");
        }
    }
}