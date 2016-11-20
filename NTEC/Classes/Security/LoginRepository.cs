using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTEC.Models.Security;
using System.Web.Security;
using System.Web.SessionState;
using NTEC.Models;
using NTEC.ViewModels;
using Exception = System.Exception;
using System.Web.Configuration;
using NTEC.Models.UnitOfWork;

namespace NTEC.Classes.Security
{
    public class LoginRepository
    {
        public String LoginUser(UserLoginVM usrLogin)
        {
            Object result = null;

            try
            {

                var login = AuthenticationHandler.IsAuthenticated(usrLogin.UserName, usrLogin.Password);
                if (login != null)
                {


                    if (login.Isauthenticated == true)
                    {

                        var secUser = new SecuredUser { User = login };
                        string dataS =
                            secUser.UserId + "," + //user Obj  (0-2)
                            secUser.Username + "," + //user Obj
                            secUser.DisplayName + "," + 
                            secUser.IsSysAdmin + "," +//user Obj
                            secUser.RoleName + ",";   //ticket Obj
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, usrLogin.UserName, DateTime.Now, DateTime.Now.AddDays(364), usrLogin.RememberMe, dataS, FormsAuthentication.FormsCookiePath);
                        string hash = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                        if (ticket.IsPersistent)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        else
                        {
                            cookie.Expires = DateTime.Today.AddDays(1);
                        }
                        HttpContext.Current.Response.Cookies.Add(cookie);

                        HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                        FormsAuthenticationTicket ticketD = FormsAuthentication.Decrypt(authCookie.Value);
                        string[] values = ticketD.UserData.Split(',').Select(sValue => sValue.Trim()).ToArray();


                        var cuser = new UserLogin
                        {
                            UserId = Convert.ToInt32(values[0]),
                            Username = values[1],
                            DisplayName = values[2],
                            IsSystemAdmin = Convert.ToBoolean(values[3]),
                            RoleName = values[4]

                        };
                        var abcUser = new SecuredUser();

                        abcUser.User = cuser;



                        SessionManager.CurrentUser = secUser;
                        var redirectUrl = "GeneratedReference/CreateGenerateRefrence";
                        result = new
                        {
                            urlData = redirectUrl,
                            success = true,
                            message = "Login succeed"
                        };


                    }
                    else
                    {
                        result = new
                        {
                            LoginResponse = "",
                            success = false,
                            message = "Login Failed"
                        };
                    }

                }
                else
                {
                    result = new
                    {
                        LoginResponse = "",
                        success = false,
                        message = "Login Failed"
                    };
                }
            }
            catch (Exception ex)
            {
                result = new
                {
                    success = false,
                    message = ex.Message
                };
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(result);
        }
    }
}

public static class AuthenticationHandler
{
    public static UserLogin IsAuthenticated(string username, string password)
    {
        var _uow = new UnitOfWork(); 
        var usrLogin = new UserLogin();
        var login = _uow.Useres.List().FirstOrDefault(x => x.Email == username && x.IsActive == true && x.IsDeleted != true);
       
        if (login != null)
        {
            string proDecPassString = NTEC.Classes.Security.StringCipher.Decrypt(login.Password, WebConfigurationManager.AppSettings["EncDecKey"]);
            if (proDecPassString == password)
            {
                usrLogin.Isauthenticated = true;
                usrLogin.UserId = login.UserId;
                usrLogin.DisplayName = login.FirstName;
                usrLogin.Email = login.Email;
                usrLogin.Username = login.FirstName;
                usrLogin.IsSystemAdmin = login.RoleID == 1;
                usrLogin.RoleName = login.Roles.RoleName;
                return usrLogin;
            }
        }
        return null;
    }
}