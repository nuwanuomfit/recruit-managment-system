using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Class
{
    public class ShowInterviews
    {
        public int InterviewId { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime Time { get; set; }         
        public string[] InterviewerFirstName { get; set; }
        public string[] InterviewerLastName { get; set; }
    }

}
