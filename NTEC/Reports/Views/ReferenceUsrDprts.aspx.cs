using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ReferenceUsrDprts : BaseForm
    {
        IUnitOfWork _uow;

        public ReferenceUsrDprts()
        {
            _uow = new UnitOfWork();
        }
        List<ReferenceByUserOrDeprt> _generatedReferenceRpt = null;
        CultureInfo enUS = new CultureInfo("en-US");
        protected void Page_Load(object sender, EventArgs e)
        {
            FromDate.Attributes["readonly"] = "readonly";
            ToDate.Attributes["readonly"] = "readonly";
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
                    UsersDrpLst.DataSource = _uow.Useres.List().ToArray().Select(item => new
                    {
                        UserId = item.UserId,
                        FullName = item.FirstName + ' ' + item.LastName
                    });
                    UsersDrpLst.DataTextField = "FullName";
                    UsersDrpLst.DataValueField = "UserId";
                    UsersDrpLst.DataBind();
                    UsersDrpLst.Items.Insert(0, "الجميع");
                    DepartDplst.DataSource = _uow.Departments.List().Where(x => x.IsDeleted != true).ToArray();
                    DepartDplst.DataTextField = "NameAr";
                    DepartDplst.DataValueField = "DepartmentId";
                    DepartDplst.DataBind();
                    DepartDplst.Items.Insert(0, "الجميع");
                    TypeDrpLst.DataSource = _uow.CorrespondentTypeNames.List().ToArray();
                    TypeDrpLst.DataTextField = "TypeNameAr";
                    TypeDrpLst.DataValueField = "CorrespondentId";
                    TypeDrpLst.DataBind();
                    TypeDrpLst.Items.Insert(0, "الجميع");
                    _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderByDescending(a => a.CreatedAt).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReferenceByUserOrDeprtAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _generatedReferenceRpt);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    UsersDrpLst.DataSource = _uow.Useres.List().ToArray().Select(item => new
                    {
                        UserId = item.UserId,
                        FullName = item.FirstName + ' ' + item.LastName
                    });
                    UsersDrpLst.DataTextField = "FullName";
                    UsersDrpLst.DataValueField = "UserId";
                    UsersDrpLst.DataBind();
                    UsersDrpLst.Items.Insert(0, "All");
                    DepartDplst.DataSource = _uow.Departments.List().Where(x => x.IsDeleted != true).ToArray();
                    DepartDplst.DataTextField = "NameEn";
                    DepartDplst.DataValueField = "DepartmentId";
                    DepartDplst.DataBind();
                    DepartDplst.Items.Insert(0, "All");
                    TypeDrpLst.DataSource = _uow.CorrespondentTypeNames.List().ToArray();
                    TypeDrpLst.DataTextField = "TypeName";
                    TypeDrpLst.DataValueField = "CorrespondentId";
                    TypeDrpLst.DataBind();
                    TypeDrpLst.Items.Insert(0, "All");
                    _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderByDescending(a => a.CreatedAt).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReferenceByUserOrDeprt.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _generatedReferenceRpt);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            
                if (SessionManager.LanguageId == "1")
                {
                    _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderByDescending(a => a.CreatedAt).ToList();
                    if (UsersDrpLst.SelectedIndex > 0)
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedBy == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                    }
                    if (DepartDplst.SelectedIndex > 0)
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.DepartmentId == int.Parse(DepartDplst.SelectedValue)).ToList();
                    }
                    if (TypeDrpLst.SelectedIndex > 0)
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CorrespondentId == int.Parse(TypeDrpLst.SelectedValue)).ToList();
                    }
                    if (!string.IsNullOrEmpty(FromDate.Text))
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt.Value.Date >= Convert.ToDateTime(FromDate.Text, enUS)).ToList();
                    }
                    if (!string.IsNullOrEmpty(ToDate.Text))
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt.Value.Date <= Convert.ToDateTime(ToDate.Text, enUS)).ToList();
                    }
                    if (!string.IsNullOrEmpty(CompanyTxt.Text))
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CompanyName.Contains(CompanyTxt.Text)).ToList();
                    }
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReferenceByUserOrDeprtAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _generatedReferenceRpt);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderByDescending(a => a.CreatedAt).ToList();
                    if (UsersDrpLst.SelectedIndex > 0)
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedBy == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                    }
                    if (DepartDplst.SelectedIndex > 0)
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.DepartmentId == int.Parse(DepartDplst.SelectedValue)).ToList();
                    }
                    if (TypeDrpLst.SelectedIndex > 0)
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CorrespondentId == int.Parse(TypeDrpLst.SelectedValue)).ToList();
                    }
                    if (!string.IsNullOrEmpty(FromDate.Text))
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt.Value.Date >= Convert.ToDateTime(FromDate.Text, enUS).Date).ToList();
                    }
                    if (!string.IsNullOrEmpty(ToDate.Text))
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt.Value.Date <= Convert.ToDateTime(ToDate.Text, enUS).Date).ToList();
                    }
                    if (!string.IsNullOrEmpty(CompanyTxt.Text))
                    {
                        _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CompanyName.Contains(CompanyTxt.Text)).ToList();
                    }
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/ReferenceByUserOrDeprt.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _generatedReferenceRpt);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
         

        }
        
    }
}