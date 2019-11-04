using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using unicornBackEnd.Models;
using unicornBackEnd.Class;
using DataAccess;

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ExamApplicantsController : ApiController
    {
  
        SendEmail sendEmail = new SendEmail();
        unicorn_databaseEntities entities = new unicorn_databaseEntities();


        public IEnumerable<spGetExamApplicant_Result> Get()
        {
            return entities.spGetExamApplicant();
        }
        public HttpResponseMessage Put(int[] idArray)
        {
            try
            {
                using (entities)
                {
                    for (int i = 1; i < idArray.Length; i++)
                    {
                        entities.spAddExamApplicant(idArray[0], idArray[i]);
                        entities.spUpdateAppState(idArray[i]);
                        entities.SaveChanges();
                    }
                    for (int i = 1; i < idArray.Length; i++)
                    {
                        spGetApplicantEmailForExam_Result spGetAppEx = entities.spGetApplicantEmailForExam(idArray[i], idArray[0]).FirstOrDefault();
                        sendEmail.SendExamMail(spGetAppEx.Email, spGetAppEx.Date);
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
