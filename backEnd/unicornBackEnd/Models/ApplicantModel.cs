using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Models
{
    public class ApplicantModel
    {
        public int ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NIC { get; set; }
        public string TepNo { get; set; }
        public string Email { get; set; }
        public int State { get; set; }
    }
}