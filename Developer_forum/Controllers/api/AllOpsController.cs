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


        //[HttpGet]
        //[Route("api/Answers/GetAnswers")]
        //public IEnumerable<Answer> GetAnswers()
        //{
        //   //return dbContext.Answers.Include(a => a.question)
        //           var answer=dbContext.

        //}


       












        //public override void Up()
        //{
        //    Sql("insert into Answers(Id,quesId,answer) values('c2eacdde-c70d-46d8-986b-2cbc3b39427f',11,'AngularJS provides capability to create Single Page Application in a very clean and maintainable way.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('c2eacdde-c70d-46d8-986b-2cbc3b39427f',1,'AngularJS is a framework to build large scale and high performance web application while keeping them as easy-to-maintain.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('c2eacdde-c70d-46d8-986b-2cbc3b39427f',2,'Data binding is the automatic synchronization of data between model and view components. ng-model directive is used in data binding.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('c2eacdde-c70d-46d8-986b-2cbc3b39427f',5,'AngularJS come with several built-in services. For example $https: service is used to make XMLHttpRequests (Ajax calls). Services are singleton objects which are instantiated only once in app.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('c2eacdde-c70d-46d8-986b-2cbc3b39427f',6,'Filters select a subset of items from an array and return a new array. Filters are used to show filtered items from a list of items based on defined criteria.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('455b2375-2ba2-4505-ac72-6206631c017a',4,'Controllers are JavaScript functions that are bound to a particular scope. They are the prime actors in AngularJS framework and carry functions to operate on data and decide which view is to be updated to show the updated model based data.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('c2eacdde-c70d-46d8-986b-2cbc3b39427f',7,'Directives are markers on DOM elements (such as elements, attributes, css, and more). These can be used to create custom HTML tags that serve as new, custom widgets. AngularJS has built-in directives (ng-bind, ng-model, etc) to perform most of the task that developers have to do.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('c2eacdde-c70d-46d8-986b-2cbc3b39427f',8,'Templates are the rendered view with information from the controller and model. These can be a single file (like index.html) or multiple views in one page using partials')");
        //    Sql("insert into Answers(Id,quesId,answer) values('03b77b1e-27e3-4044-959f-95ef90cb8066',11,'AngularJS uses dependency injection and make use of separation of concerns.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('03b77b1e-27e3-4044-959f-95ef90cb8066',1,'AngularJS is open source, completely free, and used by thousands of developers around the world. It is licensed under the Apache License version 2.0.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('03b77b1e-27e3-4044-959f-95ef90cb8066',3,'Scopes are objects that refer to the model. They act as glue between controller and view.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('455b2375-2ba2-4505-ac72-6206631c017a',11,'In AngularJS, views are pure html pages, and controllers written in JavaScript do the business processing.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('03b77b1e-27e3-4044-959f-95ef90cb8066',9,'It is concept of switching views. AngularJS based controller decides which view to render based on the business logic.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('455b2375-2ba2-4505-ac72-6206631c017a',10,'Deep linking allows you to encode the state of application in the URL so that it can be bookmarked. The application can then be restored from the URL to the same state.')");
        //    Sql("insert into Answers(Id,quesId,answer) values('03b77b1e-27e3-4044-959f-95ef90cb8066',15,'ng-app directive defines and links an AngularJS application to HTML. It also indicate the start of the application.')");
        //}

        //public override void Down()
        //{
        //    Sql("delete from Answers where ansId=1");
        //    Sql("delete from Answers where ansId=3");
        //    Sql("delete from Answers where ansId=4");
        //    Sql("delete from Answers where ansId=5");
        //    Sql("delete from Answers where ansId=6");
        //    Sql("delete from Answers where ansId=7");
        //    Sql("delete from Answers where ansId=8");
        //    Sql("delete from Answers where ansId=9");
        //    Sql("delete from Answers where ansId=10");
        //    Sql("delete from Answers where ansId=11");
        //    Sql("delete from Answers where ansId=12");
        //    Sql("delete from Answers where ansId=13");
        //    Sql("delete from Answers where ansId=14");
        //    Sql("delete from Answers where ansId=15");
        //    Sql("delete from Answers where ansId=2");
        //}

    }
}
