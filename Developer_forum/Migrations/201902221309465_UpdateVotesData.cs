namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVotesData : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Votes(Id,ansId,votes)values('f108ad40-a36d-4cab-bc02-53fe897bdd30',1,-1)");
            Sql("insert into Votes(Id,ansId,votes)values('ace12cbe-905d-4dab-8f2d-79d54c2148f3',1,-1)");

        }

        public override void Down()
        {
        }
    }
}
