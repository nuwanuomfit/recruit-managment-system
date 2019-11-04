using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string TepNo { get; set; }
        public bool Admin { get; set; }
        public bool Interviewer { get; set; }
        public bool Receiptionist { get; set; }
    }
}