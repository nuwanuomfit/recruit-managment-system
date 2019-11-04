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
    public class HoldInterviewController : ApiController
    {
        unicorn_databaseEntities entity = new unicorn_databaseEntities();
        public AppQuestion[] Get(int id)
        {

            spGetAppResultSheet_Result[] result = entity.spGetAppResultSheet(id).ToArray();
            AppQuestion[] appQuestions = new AppQuestion[result.Length];
             
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i].QuestionId == null)
                {
                     
                    AppQuestion temp = new AppQuestion();

                    temp.index = i;
                    temp.QuestionId = 0;
                    temp.Question = "Deleted";
                    temp.QuestionType = "Deleted";
                    temp.Answer1 = "Deleted";
                    temp.Answer2 = "Deleted";
                    temp.Answer3 = "Deleted";
                    temp.Answer4 = "Deleted";
                    temp.selected = result[i].Answer;
                    temp.result = result[i].State;

                    appQuestions[i] = temp;

                }
                else
                {

                    spGetAppQuestion_Result tempQuestion = entity.spGetAppQuestion(result[i].QuestionId).FirstOrDefault();
                    AppQuestion temp = new AppQuestion();

                    temp.index = i;
                    temp.QuestionId = tempQuestion.QuestionId;
                    temp.Question = tempQuestion.Question;
                    temp.QuestionType = tempQuestion.QuestionType;
                    temp.Answer1 = tempQuestion.Answer1;
                    temp.Answer2 = tempQuestion.Answer2;
                    temp.Answer3 = tempQuestion.Answer3;
                    temp.Answer4 = tempQuestion.Answer4;
                    temp.selected = result[i].Answer;
                    temp.result = result[i].State;

                    switch (result[i].Answer)
                    {
                        case 1:
                            temp.selected_1 = true;
                            temp.selected_2 = false;
                            temp.selected_3 = false;
                            temp.selected_4 = false;
                            break;
                        case 2:
                            temp.selected_1 = false;
                            temp.selected_2 = true;
                            temp.selected_3 = false;
                            temp.selected_4 = false;
                            break;
                        case 3:
                            temp.selected_1 = false;
                            temp.selected_2 = false;
                            temp.selected_3 = true;
                            temp.selected_4 = false;
                            break;
                        case 4:
                            temp.selected_1 = false;
                            temp.selected_2 = false;
                            temp.selected_3 = false;
                            temp.selected_4 = true;
                            break;
                        case -1:
                            temp.selected_1 = false;
                            temp.selected_2 = false;
                            temp.selected_3 = false;
                            temp.selected_4 = false;
                            break;

                    }
                    appQuestions[i] = temp;
                }

            }
            return appQuestions;
        }
        public IEnumerable<spGetInterviewsByDate_Result> Get()
        {
            DateTime date = DateTime.Today;
            return entity.spGetInterviewsByDate(date);               
        }

        public HttpResponseMessage Post(Comment comment)
        {
            try
            {
                using (entity)
                {
                    entity.Comments.Add(comment);
                    entity.spUpdateAppState(comment.ApplicantId);
                    entity.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, comment);
                    message.Headers.Location = new Uri(Request.RequestUri + comment.CommentId.ToString());
                    return message;
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
