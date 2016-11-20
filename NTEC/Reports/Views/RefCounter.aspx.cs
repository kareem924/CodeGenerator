using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using NTEC.Classes.Security;
using NTEC.Models.Entities;
using NTEC.Models.UnitOfWork;

namespace NTEC.Reports.Views
{
    public partial class TestingReport : BaseForm
    {
        IUnitOfWork _uow;


        public TestingReport()
        {
            _uow = new UnitOfWork();
        }
        List<RefCounterByUser> _fullAudit = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.Identity.IsAuthenticated && User.IsInRole("Admin")))
            {
                Response.Redirect("~/login/login");
            }
            if (SessionManager.LanguageId == "1")
            {
                HtmlGenericControl link = new HtmlGenericControl("LINK");
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("href", ResolveClientUrl("~/Content/CssAr/components-rtl.css"));

                HtmlGenericControl link2 = new HtmlGenericControl("LINK");
                link2.Attributes.Add("rel", "stylesheet");
                link2.Attributes.Add("type", "text/css");
                link2.Attributes.Add("href", ResolveClientUrl("~/Content/CssAr/bootstrap-rtl.min.css"));
                Page.Header.Controls.Add(link);
                Page.Header.Controls.Add(link2);
            }
            else
            {
                HtmlGenericControl link = new HtmlGenericControl("LINK");
                link.Attributes.Add("rel", "stylesheet");
                link.Attributes.Add("type", "text/css");
                link.Attributes.Add("href", ResolveClientUrl("~/Content/components.css"));

                HtmlGenericControl link2 = new HtmlGenericControl("LINK");
                link2.Attributes.Add("rel", "stylesheet");
                link2.Attributes.Add("type", "text/css");
                link2.Attributes.Add("href", ResolveClientUrl("~/Content/bootstrap.min.css"));
                Page.Header.Controls.Add(link);
                Page.Header.Controls.Add(link2);
            }
            if (!IsPostBack)
            {
              
                if (SessionManager.LanguageId == "1")
                {
                    _fullAudit = _uow.RefCounterByUsers.List().ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RefCounterByUserAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    _fullAudit = _uow.RefCounterByUsers.List().ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RefCounterByUser.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
            }
          
        }

        protected void ReferenceDrpLst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ReferenceDrpLst.SelectedValue=="1")
            {
                if (SessionManager.LanguageId == "1")
                {
                    _fullAudit = _uow.RefCounterByUsers.List().ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RefCounterByUserAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    _fullAudit = _uow.RefCounterByUsers.List().ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RefCounterByUser.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
             
            }
            else if (ReferenceDrpLst.SelectedValue == "2")
            {
                if (SessionManager.LanguageId=="1")
                {
                    var refCountByDep = _uow.RefCounterByDeps.List().ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RefCounterByDepAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", refCountByDep);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    var refCountByDep = _uow.RefCounterByDeps.List().ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RefCounterByDep.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", refCountByDep);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
               
            }
        }
    }
}