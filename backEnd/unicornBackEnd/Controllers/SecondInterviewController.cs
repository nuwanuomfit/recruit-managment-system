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
    public class SecondInterviewController:ApiController
    {
        unicorn_databaseEntities entities = new unicorn_databaseEntities();
        public IEnumerable<Nullable<int>> Get()
        {
            
             return entities.spGetLastInterviewId();
        }

        public HttpResponseMessage Post(int[] idArray)
        {
            try
            {
                using (entities)
                {
                   
                        for (int i = 1; i < idArray.Length; i++)
                        {
                            entities.spAddInterviewres(idArray[0], idArray[i]);
                            entities.SaveChanges();
                        }
                    
                    return Request.CreateResponse(HttpStatusCode.Created);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

       
    }
}