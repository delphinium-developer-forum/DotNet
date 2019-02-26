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
        public Object GetQuestions(int pageNumber)
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

            SuccessStatus s = new SuccessStatus();
            s.status = "success";
            s.data = pageData;
            s.records = new records() {
                pageNo = pageNumber,
                returned = pageData.Count(),
                total = data.Count()
            };
            
            s.sort = new sort() {field="ActivityDate",
                order="Desc"
                };
            

            return s;
        }

      //  Handle Get Request for all answers
        [HttpGet]
        [Route("api/Answers/GetAnswers/{id}/{pageNumber}")]
        public Object GetAnswers(int id,int pageNumber)
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

                       }).OrderByDescending(c=>c.votes);



            var pageSize = 10;

            var pageData = final.Skip(pageSize * (pageNumber - 1)).Take(pageSize);



            SuccessStatus s = new SuccessStatus();
            s.status = "success";
            s.data = pageData;
            s.records = new records()
            {
                pageNo = pageNumber,
                returned = pageData.Count(),
                total = final.Count()
            };

            s.sort = new sort()
            {
                field = "votes",
                order = "Desc"
            };






            return s;
        }

        //get for update particular answer
        [HttpGet]
        [Route("api/Answers/UpdateAnswers/{qid}/{aid}")]
        public Object UpdateAnswer(int qid,int aid)
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

            


            SuccessStatus s = new SuccessStatus();
            s.status = "success";
            s.data = final;

            return s;
            
        }

        //upload answer and update answer
        [HttpPost]
        [Route("api/Answers/UploadAnswers")]
        public Object UploadAnswers( [FromBody]Answer answer) {

            try
            {
                var verify = dbContext.Answers.Where(c => c.quesId == answer.quesId).Where(c => c.Id == answer.Id).SingleOrDefault();

                SuccessStatus s = new SuccessStatus();
                s.status = "success";

                if (verify == null)
                {  //if answer does not exists
                    dbContext.Answers.Add(answer);
                    dbContext.SaveChanges();
                    var var2 = dbContext.Answers.Where(c => c.quesId == answer.quesId).Where(c => c.Id == answer.Id).SingleOrDefault();
                    Vote v = new Vote() { ansId = var2.ansId, votes = 0, Id = "dummy" };
                    dbContext.Votes.Add(v);
                    dbContext.SaveChanges();
                    s.data = var2;
                    return s;
                    
                }
                else
                { //if answer exists
                    verify.answer = answer.answer;
                    dbContext.SaveChanges();
                    var var2 = dbContext.Answers.Where(c => c.quesId == answer.quesId).Where(c => c.Id == answer.Id).SingleOrDefault();
                    s.data = var2;
                    return s;
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
        public Object UploadVotes(Vote vote)
        {
            try
            {
                var votesUser = dbContext.Votes.Where(v => v.ansId == vote.ansId).SingleOrDefault();

                SuccessStatus s = new SuccessStatus();
                s.status = "success";
                    if (votesUser.Id == "dummy")
                    {
                        votesUser.Id = vote.Id;
                        votesUser.votes = vote.votes;
                        dbContext.SaveChanges();

                    s.data = votesUser;
                    }
                    else 
                    {
                        var existUser = dbContext.Votes.Where(v => v.ansId == vote.ansId).Where(u=>u.Id==vote.Id).SingleOrDefault();
                        if (existUser == null)
                        {
                        dbContext.Votes.Add(vote);
                        s.data = vote;
                        }
                        else
                         {

                            if (existUser.votes != vote.votes)
                            {

                            existUser.votes = 0;
                            
                            }
                        
                         }
                    dbContext.SaveChanges();
                    s.data = existUser;
                    }
                return s;
            }

            catch (NullReferenceException e) {

                return BadRequest(e.Message);

            }
        }


        [HttpPost]
        [Route("api/Questions/UploadQuestions")]
        public Object Post([FromBody] Question question)
        {
            try
            {
                using (var entities = new ApplicationDbContext())// to define scope of dbContext object
                {
                    entities.Questions.Add(question);
                    entities.SaveChanges();

                    //// sending message to particular location
                    //var message = Request.CreateResponse(HttpStatusCode.Created, question);
                    //message.Headers.Location = new Uri(Request.RequestUri + question.Id.ToString());
                    //return message;
                    SuccessStatus s = new SuccessStatus();
                    s.status = "Success";
                    /*becoz question id will be automatically attached to this question.quesId
                      after the record is inserted to db using SaveChanges*/
                    s.data = question;

                    return s;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        

    }
}
