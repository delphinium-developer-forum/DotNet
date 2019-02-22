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

        //Handle Get Request for questions with particular quesId
        [HttpGet]
        [ResponseType(typeof(Question))]
        [Route("api/Questions/GetQuestions/{id}")]
        public IHttpActionResult GetQuestions(int id)
        {
            var question = dbContext.Questions.AsEnumerable().Where(q=>q.quesId==id)
                                  .Join(dbContext.Users.AsEnumerable(),
                                   ques => ques.Id, u => u.Id, (ques, u) => new UserQuestion()
                                      {
                                           quesId = id,
                                           question = ques.question,
                                           activityDate = ques.activityDate,
                                           userId = u.Id,
                                           userName = u.name
                                      });
            if (question == null)
            {
                return NotFound();
            }
            return Ok(question);
        }

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
                        });
            return data;
        }

        //Handle Get Request for all answers
        //[HttpGet]
        //[Route("api/Answers/GetAnswers/{id}")]
        //public IEnumerable<allAnswers> GetAnswers(int id)
        //{
        //    //return dbContext.Answers.Include(a => a.question)
        //    var voteUp = dbContext.Votes.Where(v => v.votes == 1).GroupBy(v => v.ansId).Count();
        //    var voteDown = dbContext.Votes.Where(v => v.votes == -1).GroupBy(v => v.ansId).Count();

        //    //var total = voteUp - voteDown;
        //    //var answer = dbContext.Answers.AsEnumerable()
        //    //            .Where(ans=>ans.quesId==id)
        //    //            .Join(dbContext.Users.AsEnumerable(),
        //    //           ans => ans.Id, u => u.Id, (ans, u) => new allAnswers()
        //    //           {
        //    //               ansId = ans.ansId,
        //    //               answer = ans.answer,

        //    //               userName = u.name
        //    //});
        //    //return data;
        //    return data;
        //}









        //[HttpGet]
        //[Route("api/Answers/fun/{id}")]
        //public Object fun(int id)
        //{

        //    var voteUp = from Vote in dbContext.Votes group Vote by Vote.ansId into grouping select new { grouping.Key, numvotes=grouping.Count(p=>p.ansId)};


        //    //return dbContext.Answers.Include(a => a.question)
            //var voteUp = dbContext.Votes.Where(v => v.votes == 1).GroupBy(v => v.ansId);
            //var voteDown = dbContext.Votes.Where(v => v.votes == -1).GroupBy(v => v.ansId).Count();

                           //var total = voteUp - voteDown;
                           //var answer = dbContext.Answers.AsEnumerable()
                           //            .Where(ans=>ans.quesId==id)
                           //            .Join(dbContext.Users.AsEnumerable(),
                           //           ans => ans.Id, u => u.Id, (ans, u) => new allAnswers()
                           //           {
                           //               ansId = ans.ansId,
                           //               answer = ans.answer,

                           //               userName = u.name
                           //});
        //                   //return data;
        //    return voteDown;
        //}



    }
}
