using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using NTEC.Classes.Helpers;
using NTEC.Classes.Security;
using NTEC.Models;
using NTEC.Models.Entities;
using NTEC.Models.UnitOfWork;

namespace NTEC.Reports.Views
{
    public partial class FullAuditLogs : BaseForm
    {
        IUnitOfWork _uow;


        public FullAuditLogs()
        {
            _uow = new UnitOfWork();
        }
        List<FullAuditView> _fullAudit = null;
        CultureInfo enUS = new CultureInfo("en-US");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(User.Identity.IsAuthenticated && User.IsInRole("Admin")))
            {
                Response.Redirect("~/login/login");
            }
            if (SessionManager.LanguageId=="1")
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


           
            FromDate.Attributes["readonly"] = "readonly";
            ToDate.Attributes["readonly"] = "readonly";
           
        
            if (!IsPostBack)
            {
                if (SessionManager.LanguageId == "1")
                {
                   
                    UsersDrpLst.DataSource = _uow.Useres.List().ToArray().Select(item=>new
                    {
                        UserId=item.UserId,
                        FullName=item.FirstName+' '+item.LastName
                    });
                    UsersDrpLst.DataTextField = "FullName";
                    UsersDrpLst.DataValueField = "UserId";
                    UsersDrpLst.DataBind();
                    UsersDrpLst.Items.Insert(0, "جميع");

                    TypeDrpLst.DataSource = _uow.EventTypes.List().ToArray();
                    TypeDrpLst.DataTextField = "EventNameAr";
                    TypeDrpLst.DataValueField = "EventTypeId";
                    TypeDrpLst.DataBind();
                    TypeDrpLst.Items.Insert(0, "جميع");
                    _fullAudit = _uow.FullAuditViews.List().OrderByDescending(a => a.EventTime).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FullAdutitAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
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

                    TypeDrpLst.DataSource = _uow.EventTypes.List().ToArray();
                    TypeDrpLst.DataTextField = "EventName";
                    TypeDrpLst.DataValueField = "EventTypeId";
                    TypeDrpLst.DataBind();
                    TypeDrpLst.Items.Insert(0, "All");
                    _fullAudit = _uow.FullAuditViews.List().OrderByDescending(a => a.EventTime).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FullAdutit.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
               
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
                if (SessionManager.LanguageId == "1")
                {
                    _fullAudit = _uow.FullAuditViews.List().OrderByDescending(a => a.EventTime).ToList();
                    if (UsersDrpLst.SelectedIndex > 0)
                    {
                        _fullAudit = _fullAudit.Where(x => x.UserId == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                    }
                    if (TypeDrpLst.SelectedIndex > 0)
                    {
                        _fullAudit =
                            _fullAudit.Where(x => x.EventTypeId == int.Parse(TypeDrpLst.SelectedValue)).ToList();
                    }
                    if (!string.IsNullOrEmpty(FromDate.Text))
                    {
                        _fullAudit =
                            _fullAudit.Where(x => x.EventTime.Value.Date >= Convert.ToDateTime(FromDate.Text, enUS).Date)
                                .ToList();
                    }
                    if (!string.IsNullOrEmpty(ToDate.Text))
                    {
                        _fullAudit =
                            _fullAudit.Where(x => x.EventTime.Value.Date <= Convert.ToDateTime(ToDate.Text, enUS).Date)
                                .ToList();
                    }

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FullAdutitAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    _fullAudit = _uow.FullAuditViews.List().OrderByDescending(a => a.EventTime).ToList();
                    if (UsersDrpLst.SelectedIndex > 0)
                    {
                        _fullAudit = _fullAudit.Where(x => x.UserId == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                    }
                    if (TypeDrpLst.SelectedIndex > 0)
                    {
                        _fullAudit =
                            _fullAudit.Where(x => x.EventTypeId == int.Parse(TypeDrpLst.SelectedValue)).ToList();
                    }
                    if (!string.IsNullOrEmpty(FromDate.Text))
                    {
                        _fullAudit =
                            _fullAudit.Where(x => x.EventTime.Value.Date >= Convert.ToDateTime(FromDate.Text, enUS))
                                .ToList();
                    }
                    if (!string.IsNullOrEmpty(ToDate.Text))
                    {
                        _fullAudit =
                            _fullAudit.Where(x => x.EventTime.Value.Date <= Convert.ToDateTime(ToDate.Text, enUS))
                                .ToList();
                    }

                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/FullAdutit.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", _fullAudit);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
            

        }

       
    }
}