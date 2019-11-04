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

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    //[Authorize]
    public class LoginController : ApiController
    {
        public IEnumerable<spCheckLogin_Result> Get(string username,string password)
        {
            
            unicorn_databaseEntities ctx = new unicorn_databaseEntities();
            EncoAndDeco enco = new EncoAndDeco(); 
            string password1 = enco.encryptpass(password);
            return ctx.spCheckLogin(username, password1);
        }

    }
} 