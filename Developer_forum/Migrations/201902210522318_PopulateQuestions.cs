namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateQuestions : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Questions(question,activityDate,Id) values('What is AngularJS?','2019-02-20','22b468cf-532f-4045-8c91-797091df25d5')");
        }
        
        public override void Down()
        {
            Sql("delete from Questions where quesId=1");
        }
    }
}
