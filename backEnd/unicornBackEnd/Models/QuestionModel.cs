using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Models
{
    public class QuestionModel
    {
        public int QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string Question1 { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<int> UserId { get; set; }
        public string Answer1 { get; set; }
        public bool Answer1State { get; set; }
        public string Answer2 { get; set; }
        public bool Answer2State { get; set; }
        public string Answer3 { get; set; }
        public bool Answer3State { get; set; }
        public string Answer4 { get; set; }
        public bool Answer4State { get; set; }
    }
}