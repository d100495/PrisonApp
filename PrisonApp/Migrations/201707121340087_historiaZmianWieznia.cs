using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class historiaZmianWieznia : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.HistoriaZmianWieznia");
            AlterColumn("dbo.HistoriaZmianWieznia", "Pesel", c => c.String(maxLength: 11, unicode: false));
            AlterColumn("dbo.HistoriaZmianWieznia", "Plec", c => c.String(maxLength: 1, unicode: false));
            AddPrimaryKey("dbo.HistoriaZmianWieznia", new[] {"FK_idWieznia", "FK_HistoriaZmian_idWpisuWieznia"});
        }

        public override void Down()
        {
            DropPrimaryKey("dbo.HistoriaZmianWieznia");
            AlterColumn("dbo.HistoriaZmianWieznia", "Plec", c => c.String(false, 1, unicode: false));
            AlterColumn("dbo.HistoriaZmianWieznia", "Pesel", c => c.String(false, 11, unicode: false));
            AddPrimaryKey("dbo.HistoriaZmianWieznia",
                new[] {"FK_idWieznia", "Pesel", "Plec", "FK_HistoriaZmian_idWpisuWieznia"});
        }
    }
}