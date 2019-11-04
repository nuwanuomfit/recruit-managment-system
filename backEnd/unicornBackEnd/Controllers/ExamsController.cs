using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using unicornBackEnd.Class;
using unicornBackEnd.Models;
using DataAccess;

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ExamsController : ApiController
    {
        ModelFactory _modelFactory = new ModelFactory();
        unicorn_databaseEntities entities = new unicorn_databaseEntities();

        public IEnumerable<ExamModel> Get()
        {
            
            return entities.Exams.ToList().Select(e => _modelFactory.CreateExam(e));               //Select(u => _modelfactory.Create(u));
        }

        public IEnumerable<spGetApplicantsByExamId_Result> Get(int id)
        {
            return entities.spGetApplicantsByExamId(id);
        }

        public HttpResponseMessage Post(Exam exam)
        {
            try
            {
                using (entities)
                {
                    entities.Exams.Add(exam);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, exam);
                    message.Headers.Location = new Uri(Request.RequestUri + exam.ExamId.ToString());
                    return message;
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Put([FromBody]Exam exam)
        {
            try
            {
                using (entities)
                {
                    var entity = entities.Exams.FirstOrDefault(e => e.ExamId == exam.ExamId);
                    entity.Date = exam.Date;
                    entity.Duration = exam.Duration;
                    entity.NoOfQuestions = exam.NoOfQuestions;
                    entity.Programming = exam.Programming;
                    entity.Mathematics = exam.Mathematics;
                    entity.IQ = exam.IQ;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        

        public HttpResponseMessage Delete(int id)
        {

            using (entities)
            {
                try
                {
                    var entity = entities.Exams.FirstOrDefault(e => e.ExamId == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Exam id = " + id.ToString() + "not found");

                    }
                    else
                    {
                        entities.spDeleteExam(id);
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
}
 