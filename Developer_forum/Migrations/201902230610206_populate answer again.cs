namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateansweragain : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Answers(Id,quesId,answer) values('22b468cf-532f-4045-8c91-797091df25d5',11,'AngularJS provides capability to create Single Page Application in a very clean and maintainable way.')");

        }

        public override void Down()
        {
        }
    }
}
