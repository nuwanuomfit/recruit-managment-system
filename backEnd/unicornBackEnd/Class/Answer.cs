using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Class
{
    public class Answer
    {
        public Nullable<int> ApplicantId { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<int> SelectAnswer { get; set; }
    }
}