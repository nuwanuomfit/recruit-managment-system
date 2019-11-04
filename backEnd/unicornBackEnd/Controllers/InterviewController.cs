using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using System.Web.Http.Cors;
using unicornBackEnd.Class;

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class InterviewController : ApiController
    {
       spGetInterviews_Result[] interview_Result;
        spGetInterviewInterviewers_Result[] interviewInterviewers_Result;
       
        public IEnumerable<ShowInterviews> Get()
        {
              unicorn_databaseEntities ctx = new unicorn_databaseEntities();
          
            interview_Result = ctx.spGetInterviews().ToArray();

            ShowInterviews[] interviews = new ShowInterviews[interview_Result.Length];

            for (int i = 0; i < interview_Result.Length; i++)
            {
                ShowInterviews obe = new ShowInterviews();
                interviews[i] = obe;
                interviews[i].InterviewId = interview_Result[i].InterviewId;
                interviews[i].Date = interview_Result[i].Date;
                interviews[i].Time = interview_Result[i].Time;               
 
                interviewInterviewers_Result = ctx.spGetInterviewInterviewers(interview_Result[i].InterviewId).ToArray();
                interviews[i].InterviewerFirstName = new string[interviewInterviewers_Result.Length];
                interviews[i].InterviewerLastName = new string[interviewInterviewers_Result.Length];

               for(int a=0; a<interviewInterviewers_Result.Length; a++)
                {
                    interviews[i].InterviewerFirstName[a] = interviewInterviewers_Result[a].InterviewerFirstName;
                    interviews[i].InterviewerLastName[a] = interviewInterviewers_Result[a].InterviewerLastName;

                }
            
            }

            return interviews;
        }



        public IEnumerable<spGetApplicantByInterviewId_Result> Get(int id)
        {
            unicorn_databaseEntities ctx = new unicorn_databaseEntities();
            return ctx.spGetApplicantByInterviewId(id);
        }


        public HttpResponseMessage Post([FromBody] Interview addInterview) 
        {
            try
            {
                using (unicorn_databaseEntities entities = new unicorn_databaseEntities())
                {
                    entities.Interviews.Add(addInterview);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);

                }
            }
            catch (Exception ex) 
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        public HttpResponseMessage Delete(int id)
        {

            
            
                try
                {
                    using (unicorn_databaseEntities entities = new unicorn_databaseEntities())
                    {
                    entities.spDeleteInterview(id);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            
        }

        public HttpResponseMessage Put([FromBody]Interview interview)
        {
            try
            {
                using (unicorn_databaseEntities entities = new unicorn_databaseEntities())
                {
                    var entity = entities.Interviews.FirstOrDefault(e => e.InterviewId == interview.InterviewId);
                    entity.InterviewId = interview.InterviewId;
                    entity.Date = interview.Date;
                    entity.Time = interview.Time;
                    entity.UserId = interview.UserId;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}