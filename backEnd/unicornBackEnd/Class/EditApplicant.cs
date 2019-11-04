using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Class
{
    public class EditApplicant
    {
        public Nullable<int> ApplicantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string NIC { get; set; }
        public string TepNo { get; set; }
        public string Email { get; set; }
        public Nullable<int> State { get; set; }
    }
}