using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class stringIdWpisuHistoriaZmian : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HistoriaZmian", "idPracownika", c => c.String());
        }

        public override void Down()
        {
            AlterColumn("dbo.HistoriaZmian", "idPracownika", c => c.Int(false));
        }
    }
}