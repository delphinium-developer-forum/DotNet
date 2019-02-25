namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateVotesData1 : DbMigration
    {
        public override void Up()
        {
            Sql("insert into Votes(Id,ansId,votes)values('f108ad40-a36d-4cab-bc02-53fe897bdd30',16,-1)");


        }

        public override void Down()
        {
        }
    }
}
