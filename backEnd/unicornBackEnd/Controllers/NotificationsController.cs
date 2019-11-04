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
    public class NotificationsController : ApiController
    {
        DateTime notifiTomorrow;
        unicorn_databaseEntities entity = new unicorn_databaseEntities();

        public IEnumerable<spGetNotifications_Result> Get(int id)
        {
            return entity.spGetNotifications(id);
        }

        public HttpResponseMessage Post(HoldDate tomorrow)
        {

            notifiTomorrow = tomorrow.InterviewDate;
            DateTime lastNotifiDate = (DateTime)entity.spGetLastNotifiDate().FirstOrDefault();

            if (notifiTomorrow.ToShortDateString() != lastNotifiDate.ToShortDateString())
            {
                int notifyExamCount = (int)entity.spGetNotifyExam(notifiTomorrow).FirstOrDefault();
                spGetNotifyInterviews_Result[] notifyInterviews = entity.spGetNotifyInterviews(notifiTomorrow).ToArray();

                if (notifyExamCount != 0)
                {
                    int?[] notifiersIdArray = entity.spGetExamNotifiers().ToArray();
                    for (int i = 0; i < notifiersIdArray.Length; i++)
                    {
                        Notification notification = new Notification
                        {
                            DateTime = notifiTomorrow,
                            Notification1 = "There will be " + notifyExamCount + " exams in " + notifiTomorrow.ToShortDateString(),
                            UserId = (int)notifiersIdArray[i]
                        };
                        entity.Notifications.Add(notification);
                        entity.SaveChanges();
                    }

                    //entity.spUpdateNotificationDate(notifiTomorrow);
                    //entity.SaveChanges();
                }
                if (notifyInterviews.Length != 0)
                {
                    int?[] notifiersIdArray = entity.spGetExamNotifiers().ToArray();
                    for (int i = 0; i < notifiersIdArray.Length; i++)
                    {
                        Notification notification = new Notification
                        {
                            DateTime = notifiTomorrow,
                            Notification1 = "There will be " + notifyInterviews.Length + " interviews in " + notifiTomorrow.ToShortDateString(),
                            UserId = (int)notifiersIdArray[i]
                        };
                        entity.Notifications.Add(notification);
                        entity.SaveChanges();
                    }

                    //entity.spUpdateNotificationDate(notifiTomorrow);
                    //entity.SaveChanges();
                }
                if (notifyInterviews.Length != 0)
                {
                    for (int i = 0; i < notifyInterviews.Length; i++)
                    {
                        int?[] notifiersIdArray = entity.spGetNotifyInterviewers(notifyInterviews[i].InterviewId).ToArray();
                        for (int j = 0; j < notifiersIdArray.Length; j++)
                        {
                            Notification notification = new Notification
                            {
                                DateTime = notifiTomorrow,
                                Notification1 = "There will be a interview in " + notifiTomorrow.ToShortDateString() + " at " + notifyInterviews[i].Time,
                                UserId = (int)notifiersIdArray[i]
                            };
                            entity.Notifications.Add(notification);
                            entity.SaveChanges();
                        }

                        //entity.spUpdateNotificationDate(notifiTomorrow);
                        //entity.SaveChanges();
                    }
                }
                entity.spUpdateNotificationDate(notifiTomorrow);
                entity.SaveChanges();
            }
            //return Request.CreateResponse(HttpStatusCode.OK);
            var message = Request.CreateResponse(HttpStatusCode.Created, tomorrow);
            message.Headers.Location = new Uri(Request.RequestUri + tomorrow.InterviewDate.ToString());
            return message;
        }

        public HttpResponseMessage Put(Notification[] notifys)
        {
            for (int i = 0; i < notifys.Length; i++)
            {
                entity.spSetNotifyChecked(notifys[i].NotificationId);
            }
            var message = Request.CreateResponse(HttpStatusCode.Created, notifys);
            message.Headers.Location = new Uri(Request.RequestUri + notifys.Length.ToString());
            return message;
        }
    }
}
