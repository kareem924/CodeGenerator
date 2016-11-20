using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using NTEC.Classes.Security;
using NTEC.Models;
using NTEC.Models.Entities;
using NTEC.Models.UnitOfWork;

namespace NTEC.Reports.Views
{
    public partial class ReferenceForSpecific : BaseForm
    {
        IUnitOfWork _uow;

        public ReferenceForSpecific()
        {
            _uow =new UnitOfWork();
        }
        List<ReferenceForSpecificView> _fullAudit = null;
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
                ReferenceDrpLst.DataSource = _uow.GeneratedReferences.List().ToArray();
                ReferenceDrpLst.DataTextField = "GeneratedCodeEn";
                ReferenceDrpLst.DataValueField = "GeneratedReferenceId";
                ReferenceDrpLst.DataBind();
                ReferenceDrpLst.Items.Insert(0, "All");

                _fullAudit = _uow.ReferenceForSpecificViews.List().OrderByDescending(a => a.EventTime).ToList();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReferenceForSpecific.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            _fullAudit = _uow.ReferenceForSpecificViews.List().OrderBy(a => a.EventTime).ToList();
            if (ReferenceDrpLst.SelectedIndex > 0)
            {
                _fullAudit = _fullAudit.Where(x => x.ReferenceId == int.Parse(ReferenceDrpLst.SelectedValue)).ToList();
            }
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReferenceForSpecific.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
            ReportViewer1.LocalReport.DataSources.Add(rdc);
            ReportViewer1.LocalReport.Refresh();
        }
    }
}