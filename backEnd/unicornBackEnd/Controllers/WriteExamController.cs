using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DataAccess;
using System.Web.Http.Cors;
using unicornBackEnd.Class;

namespace unicornBackEnd.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class WriteExamController : ApiController
    {
        spGetExamByApplicant_Result test;
        unicorn_databaseEntities entity = new unicorn_databaseEntities();

        public int[] Get(int id)
        {
      
            this.test = entity.spGetExamByApplicant(id).FirstOrDefault();
            int[] detailArray = new int[2];
            detailArray[0] = this.test.NoOfQuestions;
            detailArray[1] = this.test.Duration;

            return detailArray;
        }

        public bool[] Put(Answer[] answerArray)
        {
            bool[] resultArray = new bool[answerArray.Length];
            int total = 0;
            try
            {
                using (entity)
                {
                    spGetAnswer_Result questionAnswer;

                    for (int i = 0; i < answerArray.Length; i++)
                    {
                        questionAnswer = entity.spGetAnswer(answerArray[i].QuestionId).FirstOrDefault();
                        switch (answerArray[i].SelectAnswer)
                        {
                            case 1:
                                resultArray[i] = questionAnswer.Answer1State;
                                break;
                            case 2:
                                resultArray[i] = questionAnswer.Answer2State;
                                break;
                            case 3:
                                resultArray[i] = questionAnswer.Answer3State;
                                break;
                            case 4:
                                resultArray[i] = questionAnswer.Answer4State;
                                break;
                            case -1:
                                resultArray[i] = false;
                                break;
                        }
                        entity.spInsertAppAnswers(answerArray[i].ApplicantId, answerArray[i].QuestionId, answerArray[i].SelectAnswer, resultArray[i]);
                        entity.SaveChanges();
                    }
                    for (int i = 0; i < resultArray.Length; i++)
                    {
                        if (resultArray[i])
                            total++;
                    }
                    double doubTotal = total;
                    int totalMarks = (int)((doubTotal / answerArray.Length) * 100);
                    entity.spAddTotalMarks(answerArray[0].ApplicantId, totalMarks);
                    entity.spUpdateAppState(answerArray[0].ApplicantId);
                    entity.SaveChanges();
                    return resultArray;
                }
            }
            catch (Exception ex)
            {
                return resultArray;
            }
        }
        public IEnumerable<spGetExamQus_Result> Options(int id)
        {
            //unicorn_solutionEntities ctx = new unicorn_solutionEntities();
            this.test = entity.spGetExamByApplicant(id).FirstOrDefault(); //Select(u => _modelfactory.Create(u));
            int prog = this.test.Programming;
            int math = this.test.Mathematics;
            int iq = this.test.IQ;

            return entity.spGetExamQus(prog, math, iq);

        }
    }
}