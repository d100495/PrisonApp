using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class identity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IdentityRoles", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.IdentityUserRoles", new[] {"AppUser_Id"});
            AddColumn("dbo.IdentityUserRoles", "AppUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.IdentityUserRoles", "AppUser_Id");
            AddForeignKey("dbo.IdentityRoles", "AppUser_Id", "dbo.AppUsers", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.IdentityRoles", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.IdentityUserRoles", new[] {"AppUser_Id"});
            DropColumn("dbo.IdentityUserRoles", "AppUser_Id");
        }
    }
}