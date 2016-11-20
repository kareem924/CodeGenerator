using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using NTEC.Models.Security;

namespace NTEC.Classes.Security
{
    public static class SessionManager
    {
        #region Private Data

        private static String USER_TICKET = "USER_OBJECT";
        private static String LangId_TICKET = "LANGID_TICKET";
        private static String OUId_LIST = "OUId_LIST";
        private static String URId_LIST = "URId_LIST";


        #endregion



        public static SecuredUser CurrentUser
        {
            get
            {
                var abcUser = new SecuredUser();

                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                {
                    FormsAuthenticationTicket ticketD = FormsAuthentication.Decrypt(authCookie.Value);
                    if (ticketD != null)
                    {
                        string[] values = ticketD.UserData.Split(',').Select(sValue => sValue.Trim()).ToArray();
                        abcUser.User = new UserLogin
                        {
                            UserId = int.Parse(values[0]),
                            Username = values[1],
                            DisplayName = values[2],
                            IsSystemAdmin = Convert.ToBoolean(values[3])
                        };
                    }


                    return abcUser;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                //System.Web.HttpContext.Current.Session[USER_TICKET] = value;
            }
        }

   


       

        public static String LanguageId
        {
            get
            {
                return GetCookie(LangId_TICKET);
            }
            set
            {
                SetCookie(LangId_TICKET, value, TimeSpan.FromDays(30));
            }
        }



        public static Int32 SessionTimeout
        {
            get
            {
                return System.Web.HttpContext.Current.Session.Timeout;
            }
        }


        //public static String GetUserFullName()
        //{
        //    if (SessionManager.CurrentUser != null)
        //        return SessionManager.CurrentUser.Username;
        //    else
        //        return null;
        //}

     

        /// <summary>
        /// Returns if the current user is active
        /// </summary>
        public static Boolean IsUserLoggedIn
        {
            get
            {
                //if (SessionManager.CurrentUser != null)
                //    return true;
                //else
                //    return false;
                return true;
            }

        }

       
        //public static long GetGridSetting
        //{
        //    get
        //    {
        //        long valueCookie = GetUserSettingCookie("gridUserSetting");
        //        return valueCookie;
        //    }
            
        //}
        public static int GetGridSetting { get; set; }
        

        

        public static void SetCookie(string key, string value, TimeSpan expires)
        {
            HttpCookie encodedCookie = new HttpCookie(key, value);

            if (HttpContext.Current.Request.Cookies[key] != null)
            {
                var cookieOld = HttpContext.Current.Request.Cookies[key];
                cookieOld.Expires = DateTime.Now.Add(expires);
                cookieOld.Value = encodedCookie.Value;
                HttpContext.Current.Response.Cookies.Add(cookieOld);
            }
            else
            {
                encodedCookie.Expires = DateTime.Now.Add(expires);
                HttpContext.Current.Response.Cookies.Add(encodedCookie);
            }
        }

        public static string GetCookie(string key)
        {
            string value = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];

            if (cookie != null)
            {
                value = cookie.Value;

            }
            return value;

        }


        public static void LogoutUser()
        {
            //if (SessionManager.CurrentUser !=null)
            //{
            //    iLeadCommon.Core.AuthenticationHandler.KillSession(SessionManager.CurrentUser.UserTicket.TicketNo, -1, SessionManager.CurrentUser.UserId);
            //}
            
            FormsAuthentication.SignOut();
            
            AbandonSession();

        }


        #region Methods
        public static void AbandonSession()
        {

            for (int i = 0; i < System.Web.HttpContext.Current.Session.Count; i++)
            {
                System.Web.HttpContext.Current.Session[i] = null;
            }
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.Session.Clear();
        }

        #endregion



    }


}