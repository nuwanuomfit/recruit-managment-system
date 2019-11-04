using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Class
{
    public class AppQuestion
    {
        public Nullable<int> index { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public string QuestionType { get; set; }
        public string Question { get; set; }
        public string Answer1 { get; set; }
        public bool selected_1 { get; set; }
        public string Answer2 { get; set; }
        public bool selected_2 { get; set; }
        public string Answer3 { get; set; }
        public bool selected_3 { get; set; }
        public string Answer4 { get; set; }
        public bool selected_4 { get; set; }
        public Nullable<int> selected { get; set; }
        public bool result { get; set; }
    }
}