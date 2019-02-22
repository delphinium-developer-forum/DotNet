namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addvotes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ansId = c.Int(nullable: false),
                        votes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.ansId })
                .ForeignKey("dbo.Answers", t => t.ansId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ansId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Id", "dbo.User");
            DropForeignKey("dbo.Votes", "ansId", "dbo.Answers");
            DropIndex("dbo.Votes", new[] { "ansId" });
            DropIndex("dbo.Votes", new[] { "Id" });
            DropTable("dbo.Votes");
        }
    }
}
