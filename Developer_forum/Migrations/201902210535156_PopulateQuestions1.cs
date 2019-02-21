namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateQuestions1 : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Questions(question,activityDate,Id) values('What is data binding in AngularJS?','2019-02-01','22b468cf-532f-4045-8c91-797091df25d5')");
            Sql("insert into Questions(question,activityDate,Id) values('What is scope in AngularJS?','2019-02-02','22b468cf-532f-4045-8c91-797091df25d5')");
            Sql("insert into Questions(question,activityDate,Id) values('What are the controllers in AngularJS?','2019-02-03','22b468cf-532f-4045-8c91-797091df25d5')");
            Sql("insert into Questions(question,activityDate,Id) values('What are the services in AngularJS?','2019-02-04','ace12cbe-905d-4dab-8f2d-79d54c2148f3')");
            Sql("insert into Questions(question,activityDate,Id) values('What are the filters in AngularJS?','2019-02-05','22b468cf-532f-4045-8c91-797091df25d5')");
            Sql("insert into Questions(question,activityDate,Id) values('Explain directives in AngularJS','2019-02-06','ace12cbe-905d-4dab-8f2d-79d54c2148f3')");
            Sql("insert into Questions(question,activityDate,Id) values('Explain templates in AngularJS.','2019-02-07','22b468cf-532f-4045-8c91-797091df25d5')");
            Sql("insert into Questions(question,activityDate,Id) values('What is routing in AngularJS?','2019-02-08','22b468cf-532f-4045-8c91-797091df25d5')");
            Sql("insert into Questions(question,activityDate,Id) values('What is deep linking in AngularJS?','2019-02-09','ace12cbe-905d-4dab-8f2d-79d54c2148f3')");
            Sql("insert into Questions(question,activityDate,Id) values('What are the advantages of AngularJS?','2019-02-10','ace12cbe-905d-4dab-8f2d-79d54c2148f3')");
            Sql("insert into Questions(question,activityDate,Id) values('Which are the core directives of AngularJS?','2019-02-11','22b468cf-532f-4045-8c91-797091df25d5')");
            Sql("insert into Questions(question,activityDate,Id) values('Explain AngularJS boot process.','2019-02-12','ace12cbe-905d-4dab-8f2d-79d54c2148f3')");
            Sql("insert into Questions(question,activityDate,Id) values('How AngularJS integrates with HTML?','2019-02-13','ace12cbe-905d-4dab-8f2d-79d54c2148f3')");
            Sql("insert into Questions(question,activityDate,Id) values('Explain ng-app directive.','2019-02-14','ace12cbe-905d-4dab-8f2d-79d54c2148f3')");
        }

        public override void Down()
        {
            Sql("delete from Questions where quesId=2");
            Sql("delete from Questions where quesId=3");
            Sql("delete from Questions where quesId=4");
            Sql("delete from Questions where quesId=5");
            Sql("delete from Questions where quesId=6");
            Sql("delete from Questions where quesId=7");
            Sql("delete from Questions where quesId=8");
            Sql("delete from Questions where quesId=9");
            Sql("delete from Questions where quesId=10");
            Sql("delete from Questions where quesId=11");
            Sql("delete from Questions where quesId=12");
            Sql("delete from Questions where quesId=13");
            Sql("delete from Questions where quesId=14");
            Sql("delete from Questions where quesId=15");
        }
    }
}
