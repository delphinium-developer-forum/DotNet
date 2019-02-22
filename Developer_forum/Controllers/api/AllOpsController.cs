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
        public AllOpsController()
        {
            dbContext = new ApplicationDbContext();
        }

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


        [HttpGet]
        [ResponseType(typeof(Question))]
        [Route("api/Question/GetQuestions")]
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


        //[HttpGet]
        //[Route("api/Answers/GetAnswers")]
        //public IEnumerable<Answer> GetAnswers()
        //{
        //   //return dbContext.Answers.Include(a => a.question)
        //           var answer=dbContext.

        //}





    }
}
