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
    public partial class WebForm1 : BaseForm
    {
        IUnitOfWork _uow;

        public WebForm1()
        {
            _uow =new UnitOfWork();
        }
        List<LogginHistoryView> logging = null;
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
                if (SessionManager.LanguageId=="1")
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
                    TypesUsrsDplst.DataSource = _uow.EventTypes.List().Where(x => x.EventTypeId == 8 || x.EventTypeId == 9).ToList();
                    TypesUsrsDplst.DataTextField = "EventNameAr";
                    TypesUsrsDplst.DataValueField = "EventTypeId";
                    TypesUsrsDplst.SelectedIndex = -1;
                    TypesUsrsDplst.DataBind();
                    TypesUsrsDplst.Items.Insert(0, "الجميع");
                    logging = _uow.LogginHistoryViews.List().Where(x => x.EventTypeId == 8 || x.EventTypeId == 9).OrderByDescending(a => a.EventTime).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/LogginHistoryViewAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", logging);
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
                    TypesUsrsDplst.DataSource = _uow.EventTypes.List().Where(x => x.EventTypeId == 8 || x.EventTypeId == 9).ToList();
                    TypesUsrsDplst.DataTextField = "EventName";
                    TypesUsrsDplst.DataValueField = "EventTypeId";
                    TypesUsrsDplst.SelectedIndex = -1;
                    TypesUsrsDplst.DataBind();
                    TypesUsrsDplst.Items.Insert(0, "All");
                    logging = _uow.LogginHistoryViews.List().Where(x => x.EventTypeId == 8 || x.EventTypeId == 9).OrderByDescending(a => a.EventTime).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/LogginHistoryView.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", logging);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
              

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
                if (SessionManager.LanguageId == "1")
                {
                    logging =
                        _uow.LogginHistoryViews.List()
                            .Where(x => x.EventTypeId == 8 || x.EventTypeId == 9)
                            .OrderByDescending(a => a.EventTime)
                            .ToList();
                    if (UsersDrpLst.SelectedIndex > 0)
                    {
                        logging = logging.Where(x => x.UserId == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                    }
                    if (TypesUsrsDplst.SelectedIndex > 0)
                    {
                        logging = logging.Where(x => x.EventTypeId == int.Parse(TypesUsrsDplst.SelectedValue)).ToList();
                    }
                    if (!string.IsNullOrEmpty(FromDate.Text))
                    {
                        logging =
                            logging.Where(x => x.EventTime.Value.Date >= Convert.ToDateTime(FromDate.Text, enUS).Date)
                                .ToList();
                    }
                    if (!string.IsNullOrEmpty(ToDate.Text))
                    {
                        logging =
                            logging.Where(x => x.EventTime.Value.Date <= Convert.ToDateTime(ToDate.Text, enUS).Date)
                                .ToList();
                    }
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/LogginHistoryViewAr.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", logging);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
                else
                {
                    logging =
                        _uow.LogginHistoryViews.List()
                            .Where(x => x.EventTypeId == 8 || x.EventTypeId == 9)
                            .OrderByDescending(a => a.EventTime)
                            .ToList();
                    if (UsersDrpLst.SelectedIndex > 0)
                    {
                        logging = logging.Where(x => x.UserId == int.Parse(UsersDrpLst.SelectedValue)).ToList();
                    }
                    if (TypesUsrsDplst.SelectedIndex > 0)
                    {
                        logging = logging.Where(x => x.EventTypeId == int.Parse(TypesUsrsDplst.SelectedValue)).ToList();
                    }
                    if (!string.IsNullOrEmpty(FromDate.Text))
                    {
                        logging = logging.Where(x => x.EventTime.Value.Date >= Convert.ToDateTime(FromDate.Text, enUS).Date).ToList();
                    }
                    if (!string.IsNullOrEmpty(ToDate.Text))
                    {
                        logging = logging.Where(x => x.EventTime.Value.Date <= Convert.ToDateTime(ToDate.Text, enUS).Date).ToList();
                    }
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/LogginHistoryView.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", logging);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();
                }
           
           
        }
       
    }
}
 