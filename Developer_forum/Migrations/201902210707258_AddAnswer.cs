namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnswer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        quesId = c.Int(nullable: false),
                        ansId = c.Int(nullable: false, identity: true),
                        answer = c.String(nullable: false),
                    })
                .PrimaryKey(t => new { t.Id, t.quesId })
                .ForeignKey("dbo.User", t => t.Id, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.quesId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.quesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "quesId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "Id", "dbo.User");
            DropIndex("dbo.Answers", new[] { "quesId" });
            DropIndex("dbo.Answers", new[] { "Id" });
            DropTable("dbo.Answers");
        }
    }
}
