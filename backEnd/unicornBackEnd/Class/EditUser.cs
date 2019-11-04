using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Class
{
    public class EditUser
    {
        public Nullable<int> UserId { get; set; }
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string TepNo { get; set; }
        public Nullable<bool> Admin { get; set; }
        public Nullable<bool> Interviewer { get; set; }
        public Nullable<bool> Receiptionist { get; set; }
    }
}