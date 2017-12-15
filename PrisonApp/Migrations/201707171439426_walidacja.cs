using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class walidacja : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wiezniowie", "Plec", c => c.String(false, 1, unicode: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Wiezniowie", "Plec", c => c.String(false, 11, unicode: false));
        }
    }
}