using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using unicornBackEnd.Models;
using DataAccess;
using System.Web.Http.Cors;

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class QusandAnsController : ApiController
    {
        ModelFactory _modelfactory;
        unicorn_databaseEntities entities = new unicorn_databaseEntities();

        public QusandAnsController()
        {
            _modelfactory = new ModelFactory();
        }
        
        //Get Questions
        public IEnumerable<QuestionModel> Get()
        {
            return entities.Questions.ToList().Select(a => _modelfactory.SetQus(a));
        }

        public IEnumerable<spGetQuestions_Result> Get(String type)
        {
            return entities.spGetQuestions(type);
        }

         //Add Questions
         public HttpResponseMessage Post([FromBody] Question question)
        {
            try
            {
                using (entities)
                {

                    entities.Questions.Add(question);
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);

                };
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Edit Questions
        public HttpResponseMessage Put([FromBody] Question question)
        {
            try
            {
                using (entities)
                {
                    var entity = entities.Questions.FirstOrDefault(q => q.QuestionId == question.QuestionId);
                    
                    entity.QuestionId=question.QuestionId;
                    entity.QuestionType=question.QuestionType;
                    entity.Question1 = question.Question1;
                    entity.Date = question.Date;
                    entity.UserId = question.UserId;
                    entity.Answer1 = question.Answer1;
                    entity.Answer1State = question.Answer1State;
                    entity.Answer2 = question.Answer2;
                    entity.Answer2State = question.Answer2State;
                    entity.Answer3 = question.Answer3;
                    entity.Answer3State = question.Answer3State;
                    entity.Answer4 = question.Answer4;
                    entity.Answer4State = question.Answer4State;
                        
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Delete Questions
        public HttpResponseMessage Delete(int id)
        {

            using (entities)
            {
                try
                {
                    var entity = entities.Questions.FirstOrDefault(q => q.QuestionId == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Question id = " + id.ToString() + "not found");

                    }
                    else
                    {
                        entities.Questions.Remove(entity);
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