namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQuestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        quesId = c.Int(nullable: false, identity: true),
                        question = c.String(nullable: false),
                        activityDate = c.DateTime(nullable: false),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.quesId)
                .ForeignKey("dbo.User", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Id", "dbo.User");
            DropIndex("dbo.Questions", new[] { "Id" });
            DropTable("dbo.Questions");
        }
    }
}
