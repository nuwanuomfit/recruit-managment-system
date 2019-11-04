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
    //[Authorize]
    public class UsersController : ApiController
    {

        unicorn_databaseEntities entities = new unicorn_databaseEntities();
        EncoAndDeco enco = new EncoAndDeco();

        
        // GET api/values
        public IEnumerable<spGetAllUsers_Result> Get()
        {
            return entities.spGetAllUsers();               //Select(u => _modelfactory.Create(u));
        }

        // GET api/values/5
        public spGetUsers_Result Get(int id)
        {

            spGetUsers_Result res  =  entities.spGetUsers(id).FirstOrDefault(u => u.UserId == id);

            string a = enco.Decrypt(res.Password);
            res.Password = a;
            return res;
        }

        // POST api/values
        public HttpResponseMessage Post(User user)
        {
            
            try
            {
                using (entities)
                {
                    
                    string email = user.Password;
                    string strpass = enco.encryptpass(user.Password);
                    user.Password = strpass;
          
                    entities.Users.Add(user);
                    entities.SaveChanges();

                    SendEmail sendEmail = new SendEmail();
                    sendEmail.SendUserMail(user.Email, user.UserName, email);

                    var message = Request.CreateResponse(HttpStatusCode.Created, user);
                    message.Headers.Location = new Uri(Request.RequestUri + user.UserId.ToString());
                    return message;
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        public HttpResponseMessage Patch([FromBody] User profileedit)
        {
            try
            {
                using (entities)
                {
                    var entity = entities.Users.FirstOrDefault(e => e.UserId == profileedit.UserId);


                    entity.EmpId = profileedit.EmpId;
                    entity.FirstName = profileedit.FirstName;
                    entity.LastName = profileedit.LastName;
                    entity.Gender = profileedit.Gender;
                    entity.Admin = profileedit.Admin;
                    entity.Interviewer = profileedit.Interviewer;
                    entity.Receiptionist = profileedit.Receiptionist;
                    entity.Email = profileedit.Email;
                    entity.TepNo = profileedit.TepNo;
                    entity.UserName = profileedit.UserName;
                    entity.Password = enco.encryptpass(profileedit.Password);
                    entity.UserId = profileedit.UserId;

                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

       public HttpResponseMessage Put([FromBody] EditUser user)
        {
            try
            {
                using (entities)
                {

                    entities.spEditUsers(user.UserId,
                                          user.EmpId,
                                          user.FirstName,
                                          user.LastName,
                                          user.Gender,
                                          user.Email,
                                          user.TepNo,
                                          user.Admin,
                                          user.Interviewer,
                                          user.Receiptionist
                                        );
                    entities.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.Created);

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE api/values/5
        public HttpResponseMessage Delete(int id)
        {

            using (entities)
            {
                try
                {
                    var entity = entities.Users.FirstOrDefault(e => e.UserId == id);
                    if (entity == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "User id = " + id.ToString() + "not found");

                    }
                    else
                    {
                        entities.Users.Remove(entity);
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
