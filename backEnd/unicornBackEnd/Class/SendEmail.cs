using System;
using System.Net.Mail;

namespace unicornBackEnd.Class
{
    public class SendEmail
    {
        public void SendMail(string email,DateTime date,DateTime time)
        {
             string date1 = date.ToShortDateString();
             string time1 = time.ToShortTimeString();

            string to = email; //To address    
            string from = "unicornrms@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Hi Applicant, <br> We are happy to inform you that you have successfully passed the examination and you have been selected for the interview at Unicorn Solution.<br>Your information is given below.Date - " + date1 +"<br>Time - " +time1+ "<br>You are requested to appear for the interview.<br>Thank You,<br> UnicornSolutions";

            message.Subject = "Selection to Unicorn Solutions Interview";
            message.Body = mailbody;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("unicornrms@gmail.com", "$YP@!T#RmS");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
               
            }

            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        public void SendExamMail(string email, DateTime date)
        {
            string date1 = date.ToShortDateString();
           
            string to = email;//"cj070mora@gmail.com"; //To address    
            string from = "unicornrms@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Hi Applicant,<br>You have been selected for the recruitment selection exam at Unicorn Solution. Your examination Date will be " +date1+"<br>Thank You,<br>UnicornSolutions ";
            message.Subject = "Selection to Unicorn Solutions Exam";
            message.Body = mailbody;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("unicornrms@gmail.com", "$YP@!T#RmS");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
                
            }

            catch (Exception ex)
            {
                throw ex;
                
            }
        }

        public void SendUserMail(string email, string username, string password)
        {
                   
            string to = email;//"cj070mora@gmail.com"; //To address    
            string from = "unicornrms@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Hi User,<br>Welcome to Unicorn Solution Recruitment Management System.Please use given details for your first login<br>Username - " + username + "<br>Password -" + password + "<br>Thank You,<br>UnicornSolutions";

            message.Subject = "Unicorn Solutions User Login details";
            message.Body = mailbody;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("unicornrms@gmail.com", "$YP@!T#RmS");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
                
            }

            catch (Exception ex)
            {
                throw ex;
               
            }
        }

        public void SendForGotMail(string email,string username,string password,bool noUser)
        {
          

            string to = email; //To address    
            string from = "unicornrms@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to);

            string mailbody = "Your USRMS login details have been recovered.<br>Username : "+username+ "<br>Password : " +password+"<br>Thank you<br>UnicornSolutions";

            message.Subject = "Unicorn Solutions Username Password recovery";
            message.Body = mailbody;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("unicornrms@gmail.com", "$YP@!T#RmS");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
             
            }

            catch (Exception ex)
            {
                throw ex;
               
            }
        }
    }
}