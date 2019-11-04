using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using unicornBackEnd.Class;
using DataAccess;

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class InterviewApplicantsController:ApiController
    {

        spGetApplicantEmail_Result spGetApplicantEmail_Result;
        SendEmail sendEmail = new SendEmail();

        public IEnumerable<spSelectApplicantsForInterview_Result> Get()
        {
            unicorn_databaseEntities ctx = new unicorn_databaseEntities();
            return ctx.spSelectApplicantsForInterview();
        }

        public IEnumerable<spGetApplicantByInterviewId_Result> Get(int id)
        {
            unicorn_databaseEntities ctx = new unicorn_databaseEntities();
            return ctx.spGetApplicantByInterviewId(id);
        }

            public HttpResponseMessage Put(int[] idArray)
            {   
                try
                {
                    using (unicorn_databaseEntities entities = new unicorn_databaseEntities())
                    {
                        for (int i = 1; i < idArray.Length; i++)
                        {
                            entities.spAddApplicantInterviews(idArray[0], idArray[i]);
                            entities.SaveChanges();
                        }

                        for(int i=1; i<idArray.Length; i++)
                        {
                             this.spGetApplicantEmail_Result = entities.spGetApplicantEmail(idArray[i], idArray[0]).FirstOrDefault();
                            sendEmail.SendMail(this.spGetApplicantEmail_Result.Email,this.spGetApplicantEmail_Result.Date,this.spGetApplicantEmail_Result.Time);  
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