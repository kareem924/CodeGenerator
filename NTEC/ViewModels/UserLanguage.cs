using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NTEC.Classes.Security;

namespace NTEC.Models.User
{
    public class UserLanguage
    {
        public string AppLanguage(int langId, string redirectUrl)
        {


            Object result = null;
          
            try
            {

                SessionManager.LanguageId = langId.ToString();

                result = new
                {
                    success = true,
                    LangUrl = redirectUrl,
                    LangId = langId,
                    message = ""
                };

            }
            catch (SystemException ex)
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