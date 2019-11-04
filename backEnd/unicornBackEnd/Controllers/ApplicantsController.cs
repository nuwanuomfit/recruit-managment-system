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
    public class ApplicantsController : ApiController
    {
        
        UserLogin userLogin = new UserLogin();
        EncoAndDeco enco = new EncoAndDeco();
        unicorn_databaseEntities entities = new unicorn_databaseEntities();

        //Get Applicants
        public IEnumerable<ApplicantModel> Get()
        {
            ModelFactory _modelFactory = new ModelFactory();
            return entities.Applicants.ToList().Select(a => _modelFactory.SetApplicant(a));
        }

        //Get Applicant State
        public int Get(int id)
        {
            int appState = (int)entities.spGetAppState(id).FirstOrDefault();
            return appState;
        }

        //Add Applicant
        public HttpResponseMessage Post(Applicant applicant)
        {
            try
            {
              
                entities.Applicants.Add(applicant);
                entities.SaveChanges();

                    userLogin.Name = applicant.FirstName;
                    userLogin.UserIdLogin = applicant.ApplicantId;
                    userLogin.isAdmin = false;
                    userLogin.isInterviewer = false;
                    userLogin.isReceiptionist = false;
                    userLogin.isApplicant = true;
                    userLogin.LoginUsername = applicant.Email;
                    userLogin.LoginPassword = enco.encryptpass(applicant.NIC);

                   entities.UserLogins.Add(userLogin);
                   entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, applicant);
                    message.Headers.Location = new Uri(Request.RequestUri + applicant.ApplicantId.ToString());
                    return message;


            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Edit Applicant
        public HttpResponseMessage Put([FromBody] EditApplicant applicant)
        {
            try
            {
                using (entities)
                {
                   

                    entities.spEditApplicants(applicant.ApplicantId,
                                               applicant.FirstName,
                                               applicant.LastName,
                                               applicant.Gender,
                                               applicant.NIC,
                                               applicant.TepNo,
                                               applicant.Email,
                                               applicant.State
                                             );
                     entities.SaveChanges();

                    var ee = entities.UserLogins.FirstOrDefault(a => (a.UserIdLogin == applicant.ApplicantId && a.isApplicant==true));
                    ee.Name = applicant.FirstName;
                    ee.UserIdLogin = (int)applicant.ApplicantId;
                    ee.isAdmin = false;
                    ee.isInterviewer = false;
                    ee.isReceiptionist = false;
                    ee.isApplicant = true;
                    ee.LoginUsername = applicant.Email;
                    ee.LoginPassword = enco.encryptpass(applicant.NIC);
                    entities.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        //Delete Applicant
        public HttpResponseMessage Delete(int id)
        {

            using (entities)
            {
                try
                {
                    var entity = entities.Applicants.FirstOrDefault(e => e.ApplicantId == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Applicant id = " + id.ToString() + "not found");

                    }
                    else
                    {
                        entities.Applicants.Remove(entity);
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

        public IEnumerable<spGetApplicantsResults_Result> Options(int id)
        {
            return entities.spGetApplicantsResults(id);

        }

    }

}
