namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIdentityModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "name", c => c.String());
            AddColumn("dbo.User", "imageUrl", c => c.String());
            DropColumn("dbo.User", "FirstName");
            DropColumn("dbo.User", "LastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "LastName", c => c.String());
            AddColumn("dbo.User", "FirstName", c => c.String());
            DropColumn("dbo.User", "imageUrl");
            DropColumn("dbo.User", "name");
        }
    }
}
