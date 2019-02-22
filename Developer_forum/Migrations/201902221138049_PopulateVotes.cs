namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateVotes : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Votes(Id,ansId,votes)values('22b468cf-532f-4045-8c91-797091df25d5',1,-1)");
            Sql("insert into Votes(Id,ansId,votes)values('22b468cf-532f-4045-8c91-797091df25d5',7,1)");
            Sql("insert into Votes(Id,ansId,votes)values('ace12cbe-905d-4dab-8f2d-79d54c2148f3',5,-1)");
        }
        
        public override void Down()
        {
            
        }
    }
}
