using Developer_forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using System.Data.Entity;
using System.Security.Claims;

namespace Developer_forum.Controllers.api
{
   
    public class AllOpsController : ApiController
    {
        private ApplicationDbContext dbContext;
       
        //Contructor to initialize ApplicationDContext() object
        public AllOpsController()
        {
            dbContext = new ApplicationDbContext();
        }


        //not useful atm
        //Handle Get Request for questions with particular quesId
        //[HttpGet]
        //[ResponseType(typeof(Question))]
        //[Route("api/Questions/GetQuestions/{id}")]
        //public IHttpActionResult GetQuestions(int id)
        //{
        //    var question = dbContext.Questions.AsEnumerable().Where(q=>q.quesId==id)
        //                          .Join(dbContext.Users.AsEnumerable(),
        //                           ques => ques.Id, u => u.Id, (ques, u) => new UserQuestion()
        //                              {
        //                                   quesId = id,
        //                                   question = ques.question,
        //                                   activityDate = ques.activityDate,
        //                                   userId = u.Id,
        //                                   userName = u.name
        //                              });
        //    if (question == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(question);
        //}

        //Handle Get Request for all questions
        [HttpGet]
        [ResponseType(typeof(Question))]
        [Route("api/Questions/GetQuestions")]
        public IEnumerable<UserQuestion> GetQuestions()
        {
            var data = dbContext.Questions.AsEnumerable()
                        .Join(dbContext.Users.AsEnumerable(),
                        ques => ques.Id, u => u.Id, (ques, u) => new UserQuestion()
                        {
                            quesId = ques.quesId,
                            question = ques.question,
                            activityDate = ques.activityDate,
                            userId = u.Id,
                            userName = u.name
                        }).OrderByDescending(c=>c.activityDate);
            return data;
        }

      //  Handle Get Request for all answers
        [HttpGet]
        [Route("api/Answers/GetAnswers/{id}")]
        public Object GetAnswers(int id)
        {
          
        
            var an = dbContext.Answers.AsEnumerable()
                        .Where(ans => ans.quesId == id)
                        .Join(dbContext.Users.AsEnumerable(),
                       ans => ans.Id, u => u.Id, (ans, u) => new allAnswers()
                       {
                           ansId = ans.ansId,
                           answer = ans.answer,
                           userName = u.name
                       });


            ////lis of answer having same quesid
            // var myInClause = dbContext.Answers.Where(c=>c.quesId==id).Select(c=>c.ansId);
            // //list contains votes of those answers
            // var results = from x in dbContext.Votes
            //               where myInClause.Contains(x.ansId)
            //               select x;

            //shortcut for above code
            //selecting votes of answers for particular question 
            var re = dbContext.Answers.AsEnumerable().Where(c => c.quesId == id)
                        .Join(dbContext.Votes.AsEnumerable(),
                        c => c.ansId, v => v.ansId, (c, v) => new
                        {
                            v.votes,
                            v.ansId

                        });
            //adding votes of same answer by different users
            var result = re.GroupBy(x => x.ansId, (key, values) => new
                           {
                             ansid = key,
                          totalVotes = values.Sum(x => x.votes)
                            });
            //ADD VOTES TO 'an' OBJECT

            var final= an.AsEnumerable()
                        .Join(result.AsEnumerable(),
                       ans => ans.ansId, v => v.ansid, (ans, v) => new allAnswers()
                       {
                           ansId = ans.ansId,
                           answer = ans.answer,
                           userName = ans.userName,
                           votes=v.totalVotes

                       });

            return final;
        }









        [HttpGet]
        [Route("api/Answers/fun/{id}")]
        public Object fun(int id)
        {
           ////lis of answer having same quesid
           // var myInClause = dbContext.Answers.Where(c=>c.quesId==id).Select(c=>c.ansId);
           // //list contains votes of those answers
           // var results = from x in dbContext.Votes
           //               where myInClause.Contains(x.ansId)
           //               select x;

            //shortcut for above code
            //selecting votes of answers for particular question 
            var re = dbContext.Answers.AsEnumerable().Where(c => c.quesId == id)
                        .Join(dbContext.Votes.AsEnumerable(),
                        c => c.ansId, v => v.ansId, (c, v) => new
                        { v.votes,
                        v.ansId
            
                        });
            //adding votes of same answer by different users
            var result = re.GroupBy(x => x.ansId, (key, values) => new
            {ansid=key,
            totalVotes=values.Sum(x=>x.votes)
            });
           
            
            return result;
        }



    }
}
