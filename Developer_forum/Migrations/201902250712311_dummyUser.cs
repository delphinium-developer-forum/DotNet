namespace Developer_forum.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dummyUser : DbMigration
    {
        public override void Up()
        {
        Sql("    INSERT INTO[dbo].[User]([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [name], [imageUrl]) VALUES(N'dummy', N'dummy@gmail.com', 0, N'ANzpQad5/gwtSRV1/xvEfGCzM0aMK30ra7AJZTtt18zAa45TFPx2tNg7U4OlyJ4+yg==', N'a7713f5b-2cb7-4508-94ff-ee33db7c6892', NULL, 0, 0, NULL, 0, 0, N'dummy', N'dummy', N'zzzzz')");

        }

    public override void Down()
        {
        }
    }
}
