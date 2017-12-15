using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class pesel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AppUsers", "Pesel", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.AppUsers", "Pesel", c => c.Int(false));
        }
    }
}