using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class DropTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Table");
        }

        public override void Down()
        {
            CreateTable(
                    "dbo.Table",
                    c => new
                    {
                        idCeli = c.Int(false),
                        idOdzial = c.Int(false),
                        idWieznia = c.Int(false),
                        Wolna = c.Boolean(),
                        Ilosc = c.Int(),
                        HistoriaZmian_idWpisu = c.Int(false)
                    })
                .PrimaryKey(t => t.idCeli);
        }
    }
}