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

        //Handle Get Request for all questions
        [HttpGet]
        [ResponseType(typeof(Question))]
        [Route("api/Questions/GetQuestions/{pageNumber}")]
        public IEnumerable<UserQuestion> GetQuestions(int pageNumber)
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
            var pageSize = 10;

            var pageData = data.Skip(pageSize * (pageNumber-1)).Take(pageSize);

            return pageData;
        }

      //  Handle Get Request for all answers
        [HttpGet]
        [Route("api/Answers/GetAnswers/{id}/{pageNumber}")]
        public IEnumerable<allAnswers> GetAnswers(int id,int pageNumber)
        {

            //selecting answers for particular question in answer table and get details from user table also
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



            var pageSize = 10;

            var pageData = final.Skip(pageSize * (pageNumber - 1)).Take(pageSize);

            return pageData;
        }

        //get for update particular answer
        [HttpGet]
        [Route("api/Answers/UpdateAnswers/{qid}/{aid}")]
        public IEnumerable<allAnswers> UpdateAnswer(int qid,int aid)
        {

            //selecting answers for particular question in answer table
            var an = dbContext.Answers.AsEnumerable()
                        .Where(ans => ans.quesId == qid)
                        .Join(dbContext.Users.AsEnumerable(),
                       ans => ans.Id, u => u.Id, (ans, u) => new allAnswers()
                       {
                           ansId = ans.ansId,
                           answer = ans.answer,
                           userName = u.name
                       });

            
            //adding votes of a particular answer(having ansId=aid) by different users
            var result = dbContext.Votes.Where(ans=>ans.ansId==aid).GroupBy(x => x.ansId, (key, values) => new
            {
                ansid = key,
                totalVotes = values.Sum(x => x.votes)
            });
            //ADD VOTES TO 'an' OBJECT :'an' has all answer details

            var final = an.AsEnumerable().Where(ans=>ans.ansId==aid)
                        .Join(result.AsEnumerable(),
                       ans => ans.ansId, v => v.ansid, (ans, v) => new allAnswers()
                       {
                           ansId = ans.ansId,
                           answer = ans.answer,
                           userName = ans.userName,
                           votes = v.totalVotes

                       });

            return final;
        }

        //upload answer and update answer
        [HttpPost]
        [Route("api/Answers/UploadAnswers")]
        public IHttpActionResult UploadAnswers( [FromBody]Answer answer) {

            try
            {
                var verify = dbContext.Answers.Where(c => c.quesId == answer.quesId).Where(c => c.Id == answer.Id).SingleOrDefault();
                if (verify == null)
                {  //if answer does not exists
                    dbContext.Answers.Add(answer);
                    dbContext.SaveChanges();
                    var var2 = dbContext.Answers.Where(c => c.quesId == answer.quesId).Where(c => c.Id == answer.Id).SingleOrDefault();
                    Vote v = new Vote() { ansId = var2.ansId, votes = 0, Id = "dummy" };
                    dbContext.Votes.Add(v);
                    dbContext.SaveChanges();
                    return Ok("answer uploaded");
                }
                else
                { //if answer exists
                    verify.answer = answer.answer;

                    dbContext.SaveChanges();
                    return Ok("your answer is updated");
                }
            }
            catch (System.Reflection.TargetException e)
            {
                return BadRequest(e.Message + e.Data);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException e)
            {
                return BadRequest(e.Message);
            }
            // if user voilates fk contraint
            catch (System.Data.Entity.Validation.DbEntityValidationException e) {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [Route("api/Votes/UploadVotes")]
        public IHttpActionResult UploadVotes(Vote vote)
        {
            try
            {
                var votesUser = dbContext.Votes.Where(v => v.ansId == vote.ansId).SingleOrDefault();
           
            
                    if (votesUser.Id == "dummy")
                    {
                        votesUser.Id = vote.Id;
                        votesUser.votes = vote.votes;
                        dbContext.SaveChanges();
                        return Ok("first vote is uploaded");
                    }
                    else 
                    {
                        var existUser = dbContext.Votes.Where(v => v.ansId == vote.ansId).Where(u=>u.Id==vote.Id).SingleOrDefault();
                        if (existUser == null)
                        {
                        dbContext.Votes.Add(vote);
                       
                        }
                        else
                         {

                            if (existUser.votes != vote.votes)
                            {

                            votesUser.votes = 0;
                            
                            }
                        
                         }
                    dbContext.SaveChanges();
                    return Ok("your vote is uploaded");
                    }
                }
           
            catch (NullReferenceException e) {

                return BadRequest(e.Message);

            }
        }


        [HttpPost]
        [Route("api/Questions/UploadQuestions")]
        public HttpResponseMessage Post([FromBody] Question question)
        {
            try
            {
                using (var entities = new ApplicationDbContext())
                {
                    entities.Questions.Add(question);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, question);
                    message.Headers.Location = new Uri(Request.RequestUri + question.Id.ToString());
                    return message;

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        [HttpPut]
        [Route("api/Votes/ChangeVotes")]
        public IHttpActionResult ChangeVotes(Vote vote)
        {
            try
            {
                var votesUser = dbContext.Votes.Where(v => v.ansId == vote.ansId).Where(v=>v.Id==vote.Id).SingleOrDefault();

                if (votesUser.votes != vote.votes) {

                    votesUser.votes = 0;
                    dbContext.SaveChanges();
                }
                return Ok("votes changed");
            }

            catch (NullReferenceException e)
            {

                return BadRequest(e.Message);

            }
        }






    }
}
