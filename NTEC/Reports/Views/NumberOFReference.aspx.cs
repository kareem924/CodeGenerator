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
using NTEC.Models.Entities;
using NTEC.Models.UnitOfWork;

namespace NTEC.Reports.Views
{
    public partial class NumberOFReference : BaseForm
    {
        IUnitOfWork _uow;
        List<ReferenceByUserOrDeprt> _generatedReferenceRpt = null;
        CultureInfo enUS = new CultureInfo("en-US");
        ReportParameter[] parameters = new ReportParameter[7];
        public NumberOFReference()
        {
            _uow = new UnitOfWork();
        }
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
                if (SessionManager.LanguageId=="1")
                {
                   
                    UsersDrpLst.DataSource = _uow.Useres.List().ToArray();
                    UsersDrpLst.DataTextField = "FirstName";
                    UsersDrpLst.DataValueField = "UserId";
                    UsersDrpLst.DataBind();
                    UsersDrpLst.Items.Insert(0, "الجميع");
                    DepartDplst.DataSource = _uow.Departments.List().ToArray();
                    DepartDplst.DataTextField = "NameAr";
                    DepartDplst.DataValueField = "DepartmentId";
                    DepartDplst.DataBind();
                    DepartDplst.Items.Insert(0, "الجميع");
                    TypeDrpLst.DataSource = _uow.CorrespondentTypeNames.List().ToArray();
                    TypeDrpLst.DataTextField = "TypeNameAr";
                    TypeDrpLst.DataValueField = "CorrespondentId";
                    TypeDrpLst.DataBind();
                    TypeDrpLst.Items.Insert(0, "الجميع");
                    _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderBy(a => a.CreatedAt).ToList();
                  
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[1] = new ReportParameter("UserName", "الجميع");
                    parameters[2] = new ReportParameter("DepartmentName", "الجميع");
                    parameters[3] = new ReportParameter("From", " ");
                    parameters[4] = new ReportParameter("To", " ");
                    parameters[5] = new ReportParameter("Company", "الجميع");
                    parameters[6] = new ReportParameter("Type", "الجميع");
                    this.ReportViewer1.LocalReport.SetParameters(parameters);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/NumberOfGeneratedReferencesAr.rdlc");
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    UsersDrpLst.DataSource = _uow.Useres.List().ToArray();
                    UsersDrpLst.DataTextField = "FirstName";
                    UsersDrpLst.DataValueField = "UserId";
                    UsersDrpLst.DataBind();
                    UsersDrpLst.Items.Insert(0, "All");
                    DepartDplst.DataSource = _uow.Departments.List().ToArray();
                    DepartDplst.DataTextField = "NameEn";
                    DepartDplst.DataValueField = "DepartmentId";
                    DepartDplst.DataBind();
                    DepartDplst.Items.Insert(0, "All");
                    TypeDrpLst.DataSource = _uow.CorrespondentTypeNames.List().ToArray();
                    TypeDrpLst.DataTextField = "TypeName";
                    TypeDrpLst.DataValueField = "CorrespondentId";
                    TypeDrpLst.DataBind();
                    TypeDrpLst.Items.Insert(0, "All");
                    _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderBy(a => a.CreatedAt).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[1] = new ReportParameter("UserName", "All");
                    parameters[2] = new ReportParameter("DepartmentName", "All");
                    parameters[3] = new ReportParameter("From", " ");
                    parameters[4] = new ReportParameter("To", " ");
                    parameters[5] = new ReportParameter("Company", "All");
                    parameters[6] = new ReportParameter("Type", "All");
                    this.ReportViewer1.LocalReport.SetParameters(parameters);
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/NumberOfGeneratedReferences.rdlc");
                    ReportViewer1.LocalReport.Refresh();
                }
              
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var grb = _uow.ReferenceByUserOrDeprts.List().GroupBy(x => x.CreatedBy);
            if (SessionManager.LanguageId=="1")
            {
                _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderBy(a => a.CreatedAt).ToList();
                parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                parameters[1] = new ReportParameter("UserName", "الجميع");
                parameters[2] = new ReportParameter("DepartmentName", "الجميع");
                parameters[3] = new ReportParameter("From", " ");
                parameters[4] = new ReportParameter("To", " ");
                parameters[5] = new ReportParameter("Company", "الجميع");
                parameters[6] = new ReportParameter("Type", "الجميع");
                if (UsersDrpLst.SelectedIndex > 0)
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedBy == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                   
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    var referenceByUserOrDeprt = _generatedReferenceRpt.FirstOrDefault();
                    if (referenceByUserOrDeprt != null)
                    { parameters[1] = new ReportParameter("UserName", referenceByUserOrDeprt.Name); }
                    else
                    {
                        parameters[1] = new ReportParameter("UserName", UsersDrpLst.SelectedItem.Text);
                    }
                }
                if (DepartDplst.SelectedIndex > 0)
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.DepartmentId == int.Parse(DepartDplst.SelectedValue)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    var referenceByUserOrDeprt = _generatedReferenceRpt.FirstOrDefault();
                    if (referenceByUserOrDeprt != null)
                    { parameters[2] = new ReportParameter("DepartmentName", referenceByUserOrDeprt.NameAr); }
                    else
                    {
                        parameters[2] = new ReportParameter("DepartmentName", DepartDplst.SelectedItem.Text);
                    }
                }
                if (TypeDrpLst.SelectedIndex > 0)
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CorrespondentId == int.Parse(TypeDrpLst.SelectedValue)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    var referenceByUserOrDeprt = _generatedReferenceRpt.FirstOrDefault();
                    if (referenceByUserOrDeprt != null)
                    { parameters[6] = new ReportParameter("Type", referenceByUserOrDeprt.TypeNameAr); }
                    else
                    {
                        parameters[6] = new ReportParameter("Type", TypeDrpLst.SelectedItem.Text);
                    }
                }
                if (!string.IsNullOrEmpty(FromDate.Text))
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt.Value.Date >= Convert.ToDateTime(FromDate.Text, enUS).Date).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[3] = new ReportParameter("From", FromDate.Text);
                }
                if (!string.IsNullOrEmpty(ToDate.Text))
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt.Value.Date <= Convert.ToDateTime(ToDate.Text, enUS).Date).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[4] = new ReportParameter("To", ToDate.Text);
                }
                if (!string.IsNullOrEmpty(CompanyTxt.Text))
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CompanyName.Contains(CompanyTxt.Text)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[5] = new ReportParameter("Company", CompanyTxt.Text);
                }
                this.ReportViewer1.LocalReport.SetParameters(parameters);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/NumberOfGeneratedReferencesAr.rdlc");
                ReportViewer1.LocalReport.Refresh();

            }
            else
            {
                _generatedReferenceRpt = _uow.ReferenceByUserOrDeprts.List().OrderBy(a => a.CreatedAt).ToList();
                parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                parameters[1] = new ReportParameter("UserName", "All");
                parameters[2] = new ReportParameter("DepartmentName", "All");
                parameters[3] = new ReportParameter("From", " ");
                parameters[4] = new ReportParameter("To", " ");
                parameters[5] = new ReportParameter("Company", "All");
                parameters[6] = new ReportParameter("Type", "All");
                if (UsersDrpLst.SelectedIndex > 0)
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedBy == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    var referenceByUserOrDeprt = _generatedReferenceRpt.FirstOrDefault();
                    if (referenceByUserOrDeprt != null)
                    { parameters[1] = new ReportParameter("UserName", referenceByUserOrDeprt.Name); }
                    else
                    {
                        parameters[1] = new ReportParameter("UserName", UsersDrpLst.SelectedItem.Text);
                    }
                }
                if (DepartDplst.SelectedIndex > 0)
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.DepartmentId == int.Parse(DepartDplst.SelectedValue)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    var referenceByUserOrDeprt = _generatedReferenceRpt.FirstOrDefault();
                    if (referenceByUserOrDeprt != null)
                    { parameters[2] = new ReportParameter("DepartmentName", referenceByUserOrDeprt.NameEn); }
                    else
                    {
                        parameters[2] = new ReportParameter("DepartmentName", DepartDplst.SelectedItem.Text);
                    }
                }
                if (TypeDrpLst.SelectedIndex > 0)
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CorrespondentId == int.Parse(TypeDrpLst.SelectedValue)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    var referenceByUserOrDeprt = _generatedReferenceRpt.FirstOrDefault();
                    if (referenceByUserOrDeprt != null)
                    { parameters[6] = new ReportParameter("Type", referenceByUserOrDeprt.TypeName); }
                    else
                    {
                        parameters[6] = new ReportParameter("Type", TypeDrpLst.SelectedItem.Text);
                    }
                }
                if (!string.IsNullOrEmpty(FromDate.Text))
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt >= Convert.ToDateTime(FromDate.Text, enUS)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[3] = new ReportParameter("From", FromDate.Text);
                }
                if (!string.IsNullOrEmpty(ToDate.Text))
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CreatedAt <= Convert.ToDateTime(ToDate.Text, enUS)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[4] = new ReportParameter("To", ToDate.Text);
                }
                if (!string.IsNullOrEmpty(CompanyTxt.Text))
                {
                    _generatedReferenceRpt = _generatedReferenceRpt.Where(x => x.CompanyName.Contains(CompanyTxt.Text)).ToList();
                    parameters[0] = new ReportParameter("NumberOfReference", _generatedReferenceRpt.Count().ToString());
                    parameters[5] = new ReportParameter("Company", CompanyTxt.Text);
                }
                this.ReportViewer1.LocalReport.SetParameters(parameters);
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/NumberOfGeneratedReferences.rdlc");
                ReportViewer1.LocalReport.Refresh();

            }
        

        }
    }
}