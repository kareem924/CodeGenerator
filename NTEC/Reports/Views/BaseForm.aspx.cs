using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NTEC.Classes.Helpers;

namespace NTEC.Reports.Views
{
    public partial class BaseForm : System.Web.UI.Page
    {
        protected override void InitializeCulture()
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ? Request.UserLanguages[0] : null; // obtain it from HTTP header AcceptLanguages
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
            // Modify current thread's cultures            
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            base.InitializeCulture();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}