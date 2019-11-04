using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using unicornBackEnd.Models;
using DataAccess;
using System.Web.Http.Cors;
using unicornBackEnd.Class;
using System.Text;

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class WebAppLoginController : ApiController
    {
        unicorn_databaseEntities ctx = new unicorn_databaseEntities();
        EncoAndDeco enco = new EncoAndDeco();
        
        public IEnumerable<spCheckLoginInWebApp_Result> Get(string username, string password)
        {
            
            string pass1 = enco.encryptpass(password);
            return ctx.spCheckLoginInWebApp(username, pass1);
        }

        public string Get(string email)
        {
            bool isUser = false;
            spForgotPassword_Result forgot = new spForgotPassword_Result();
                forgot = ctx.spForgotPassword(email).FirstOrDefault();
                ctx.SaveChanges();
            EncoAndDeco deco = new EncoAndDeco();

            string password = deco.Decrypt(forgot.Password);

            try
            {
                SendEmail send = new SendEmail();
                if (forgot != null)
                {
                    isUser=true;
                    send.SendForGotMail(email, forgot.UserName, password,isUser);
                    return "sent";
                }
                else
                {
                    send.SendForGotMail(email, forgot.UserName, forgot.Password, isUser);
                    return "nouser";
                }
            }
            catch(Exception ex){
                return "exception";
            }
        }

    }

    
}
