using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class historiaZmianWyrokow : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HistoriaZmianWyrokow");
            AddPrimaryKey("dbo.HistoriaZmianWyrokow", new[] {"FK_idWyroku", "FK_HistoriaZmian_idWpisu"});
        }

        public override void Down()
        {
            DropPrimaryKey("dbo.HistoriaZmianWyrokow");
            AddPrimaryKey("dbo.HistoriaZmianWyrokow",
                new[] {"idWieznia", "FK_idWyroku", "idKategoriiPrzestepstwa", "FK_HistoriaZmian_idWpisu"});
        }
    }
}