using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Class
{
    public class AddInterview
    {
        public Nullable<int> InterviewId { get; set; } 
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> Time { get; set; }
        public Nullable<int> AdminId { get; set; }
    }
}