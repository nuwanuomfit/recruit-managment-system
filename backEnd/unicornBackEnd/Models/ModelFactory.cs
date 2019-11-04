using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace unicornBackEnd.Models
{
    public class ModelFactory
    {
        public UserModel Create(User user)
        {
            return new UserModel()
            {
                UserId = user.UserId,
                EmpId = user.EmpId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Email = user.Email,
                TepNo = user.TepNo,
                Admin = user.Admin,
                Interviewer = user.Interviewer,
                Receiptionist = user.Receiptionist,

            };
        }

        public ApplicantModel SetApplicant(Applicant applicant)
        {
            return new ApplicantModel()
            {
                ApplicantId = applicant.ApplicantId,
                FirstName = applicant.FirstName,
                LastName = applicant.LastName,
                Gender = applicant.Gender,
                NIC = applicant.NIC,
                TepNo = applicant.TepNo,
                Email = applicant.Email,
                State = applicant.State
            };
        }

        public ExamModel CreateExam(Exam exam)
        {
            return new ExamModel()
            {
                ExamId = exam.ExamId,
                Date = exam.Date,
                Duration = exam.Duration,
                NoOfQuestions = exam.NoOfQuestions,
                Programming = exam.Programming,
                Mathematics = exam.Mathematics,
                IQ = exam.IQ
            };
        }

        public QuestionModel SetQus(Question question)
        {
            return new QuestionModel()
            {
                QuestionId = question.QuestionId,
                QuestionType = question.QuestionType,
                Question1 = question.Question1,
                Date = question.Date,
                UserId = question.UserId,
                Answer1 = question.Answer1,
                Answer1State = question.Answer1State,
                Answer2 = question.Answer2,
                Answer2State = question.Answer2State,
                Answer3 = question.Answer3,
                Answer3State = question.Answer3State,
                Answer4 = question.Answer4,
                Answer4State = question.Answer4State
            };
        }

    }
}