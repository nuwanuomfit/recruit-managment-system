//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class AppAnswer
    {
        public int AppAnswerId { get; set; }
        public int ApplicantId { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public int Answer { get; set; }
        public bool State { get; set; }
    
        public virtual Applicant Applicant { get; set; }
        public virtual Question Question { get; set; }
    }
}
