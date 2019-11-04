using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace unicornBackEnd.Models
{
    public class ExamModel
    {
        public int ExamId { get; set; }
        public DateTime Date { get; set; }
        //public TimeSpan Time { get; set; }
        public int Duration { get; set; }
        public int NoOfQuestions { get; set; }
        public int Programming { get; set; }
        public int Mathematics { get; set; }
        public int IQ { get; set; }
    }
}