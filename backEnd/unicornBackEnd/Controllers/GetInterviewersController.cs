
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
    public class GetInterviewersController:ApiController
    {
        spGetInterviewers_Result[] spGetInterviewers_Result;

        public IEnumerable<Interviewer> Get()
        {
            unicorn_databaseEntities ctx = new unicorn_databaseEntities();
            this.spGetInterviewers_Result=ctx.spGetInterviewers().ToArray();

            Interviewer[] interviewer = new Interviewer[this.spGetInterviewers_Result.Length];
            
            for(int i = 0; i < spGetInterviewers_Result.Length; i++)
            {
                Interviewer obj = new Interviewer();
                interviewer[i] = obj;
                interviewer[i].UserId = this.spGetInterviewers_Result[i].UserId;
                interviewer[i].EmpId = this.spGetInterviewers_Result[i].EmpId;
                interviewer[i].InterviewerName = this.spGetInterviewers_Result[i].FirstName + " " + this.spGetInterviewers_Result[i].LastName;
                interviewer[i].TepNo = this.spGetInterviewers_Result[i].TepNo;
            }

            return interviewer;
        }
    }
}