using System.Data.Entity.Migrations;

namespace PrisonApp.Migrations
{
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Cele",
                    c => new
                    {
                        idCeli = c.Int(false, true),
                        FK_idOdzial = c.Int(false),
                        Wolna = c.Boolean(false),
                        IloscMiejsc = c.Int(false),
                        AktywnaNieaktywna = c.Boolean(name: "Aktywna/Nieaktywna", nullable: false)
                    })
                .PrimaryKey(t => t.idCeli)
                .ForeignKey("dbo.Odzial", t => t.FK_idOdzial)
                .Index(t => t.FK_idOdzial);

            CreateTable(
                    "dbo.Odzial",
                    c => new
                    {
                        idOdzial = c.Int(false, true),
                        IloscCel = c.Int(false)
                    })
                .PrimaryKey(t => t.idOdzial);

            CreateTable(
                    "dbo.KategoriaPrzestepstwaOdzial",
                    c => new
                    {
                        FK_idOdzial = c.Int(false),
                        FK_idKategoriiPrzestepstwa = c.Int(false),
                        PlecOdzial = c.String(false, 1, unicode: false)
                    })
                .PrimaryKey(t => new {t.FK_idOdzial, t.FK_idKategoriiPrzestepstwa, t.PlecOdzial})
                .ForeignKey("dbo.KategoriePrzestepstwa", t => t.FK_idKategoriiPrzestepstwa)
                .ForeignKey("dbo.Odzial", t => t.FK_idOdzial)
                .Index(t => t.FK_idOdzial)
                .Index(t => t.FK_idKategoriiPrzestepstwa);

            CreateTable(
                    "dbo.KategoriePrzestepstwa",
                    c => new
                    {
                        idKategoriiPrzestepstwa = c.Int(false, true),
                        NazwaKategorii = c.String(false, 45, unicode: false)
                    })
                .PrimaryKey(t => t.idKategoriiPrzestepstwa);

            CreateTable(
                    "dbo.Wyroki",
                    c => new
                    {
                        idWyroku = c.Int(false, true),
                        FK_idWieznia = c.Int(false),
                        FK_idKategoriiPrzestepstwa = c.Int(false),
                        Czas = c.Int(false),
                        DataRozpoczecia = c.DateTime(false, storeType: "date")
                    })
                .PrimaryKey(t => t.idWyroku)
                .ForeignKey("dbo.Wiezniowie", t => t.FK_idWieznia)
                .ForeignKey("dbo.KategoriePrzestepstwa", t => t.FK_idKategoriiPrzestepstwa)
                .Index(t => t.FK_idWieznia)
                .Index(t => t.FK_idKategoriiPrzestepstwa);

            CreateTable(
                    "dbo.HistoriaZmianWyrokow",
                    c => new
                    {
                        idWieznia = c.Int(false),
                        FK_idWyroku = c.Int(false),
                        idKategoriiPrzestepstwa = c.Int(false),
                        FK_HistoriaZmian_idWpisu = c.Int(false),
                        Czas = c.Int(),
                        DataRozpoczecia = c.DateTime(storeType: "date")
                    })
                .PrimaryKey(
                    t => new {t.idWieznia, t.FK_idWyroku, t.idKategoriiPrzestepstwa, t.FK_HistoriaZmian_idWpisu})
                .ForeignKey("dbo.HistoriaZmian", t => t.FK_HistoriaZmian_idWpisu)
                .ForeignKey("dbo.Wyroki", t => t.FK_idWyroku)
                .Index(t => t.FK_idWyroku)
                .Index(t => t.FK_HistoriaZmian_idWpisu);

            CreateTable(
                    "dbo.HistoriaZmian",
                    c => new
                    {
                        idWpisu = c.Int(false, true),
                        idPracownika = c.Int(false),
                        Data = c.DateTime(false, storeType: "date")
                    })
                .PrimaryKey(t => t.idWpisu);

            CreateTable(
                    "dbo.HistoriaZmianPrzydzialow",
                    c => new
                    {
                        FK_idWiezniaPrzydzialu = c.Int(false),
                        FK_HistoriaZmian_idWpisuPrzydzialu = c.Int(false),
                        idCeli = c.Int()
                    })
                .PrimaryKey(t => new {t.FK_idWiezniaPrzydzialu, t.FK_HistoriaZmian_idWpisuPrzydzialu})
                .ForeignKey("dbo.Przydzialy", t => t.FK_idWiezniaPrzydzialu)
                .ForeignKey("dbo.HistoriaZmian", t => t.FK_HistoriaZmian_idWpisuPrzydzialu)
                .Index(t => t.FK_idWiezniaPrzydzialu)
                .Index(t => t.FK_HistoriaZmian_idWpisuPrzydzialu);

            CreateTable(
                    "dbo.Przydzialy",
                    c => new
                    {
                        FK_idWieznia = c.Int(false),
                        FK_idCeli = c.Int(false)
                    })
                .PrimaryKey(t => t.FK_idWieznia)
                .ForeignKey("dbo.Wiezniowie", t => t.FK_idWieznia)
                .ForeignKey("dbo.Cele", t => t.FK_idCeli)
                .Index(t => t.FK_idWieznia)
                .Index(t => t.FK_idCeli);

            CreateTable(
                    "dbo.Wiezniowie",
                    c => new
                    {
                        idWieznia = c.Int(false, true),
                        ImieWieznia = c.String(false, 45, unicode: false),
                        NazwiskoWieznia = c.String(false, 45, unicode: false),
                        Pesel = c.String(false, 11, unicode: false),
                        Plec = c.String(false, 11, unicode: false)
                    })
                .PrimaryKey(t => t.idWieznia);

            CreateTable(
                    "dbo.HistoriaZmianWieznia",
                    c => new
                    {
                        FK_idWieznia = c.Int(false),
                        Pesel = c.String(false, 11, unicode: false),
                        Plec = c.String(false, 1, unicode: false),
                        FK_HistoriaZmian_idWpisuWieznia = c.Int(false),
                        ImieWieznia = c.String(maxLength: 45, unicode: false),
                        NazwiskoWieznia = c.String(maxLength: 45, unicode: false)
                    })
                .PrimaryKey(t => new {t.FK_idWieznia, t.Pesel, t.Plec, t.FK_HistoriaZmian_idWpisuWieznia})
                .ForeignKey("dbo.Wiezniowie", t => t.FK_idWieznia)
                .ForeignKey("dbo.HistoriaZmian", t => t.FK_HistoriaZmian_idWpisuWieznia)
                .Index(t => t.FK_idWieznia)
                .Index(t => t.FK_HistoriaZmian_idWpisuWieznia);

            CreateTable(
                    "dbo.IdentityRoles",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(),
                        Discriminator = c.String(false, 128),
                        AppUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);

            CreateTable(
                    "dbo.IdentityUserRoles",
                    c => new
                    {
                        RoleId = c.String(false, 128),
                        UserId = c.String(false, 128),
                        IdentityRole_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => new {t.RoleId, t.UserId})
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.IdentityRole_Id);

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

            CreateTable(
                    "dbo.AppUsers",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        Pesel = c.Int(false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(false),
                        TwoFactorEnabled = c.Boolean(false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(false),
                        AccessFailedCount = c.Int(false),
                        UserName = c.String()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.IdentityUserClaims",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AppUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);

            CreateTable(
                    "dbo.IdentityUserLogins",
                    c => new
                    {
                        UserId = c.String(false, 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AppUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.IdentityRoles", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.IdentityUserLogins", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.IdentityUserClaims", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.Przydzialy", "FK_idCeli", "dbo.Cele");
            DropForeignKey("dbo.KategoriaPrzestepstwaOdzial", "FK_idOdzial", "dbo.Odzial");
            DropForeignKey("dbo.Wyroki", "FK_idKategoriiPrzestepstwa", "dbo.KategoriePrzestepstwa");
            DropForeignKey("dbo.HistoriaZmianWyrokow", "FK_idWyroku", "dbo.Wyroki");
            DropForeignKey("dbo.HistoriaZmianWyrokow", "FK_HistoriaZmian_idWpisu", "dbo.HistoriaZmian");
            DropForeignKey("dbo.HistoriaZmianWieznia", "FK_HistoriaZmian_idWpisuWieznia", "dbo.HistoriaZmian");
            DropForeignKey("dbo.HistoriaZmianPrzydzialow", "FK_HistoriaZmian_idWpisuPrzydzialu", "dbo.HistoriaZmian");
            DropForeignKey("dbo.Wyroki", "FK_idWieznia", "dbo.Wiezniowie");
            DropForeignKey("dbo.Przydzialy", "FK_idWieznia", "dbo.Wiezniowie");
            DropForeignKey("dbo.HistoriaZmianWieznia", "FK_idWieznia", "dbo.Wiezniowie");
            DropForeignKey("dbo.HistoriaZmianPrzydzialow", "FK_idWiezniaPrzydzialu", "dbo.Przydzialy");
            DropForeignKey("dbo.KategoriaPrzestepstwaOdzial", "FK_idKategoriiPrzestepstwa",
                "dbo.KategoriePrzestepstwa");
            DropForeignKey("dbo.Cele", "FK_idOdzial", "dbo.Odzial");
            DropIndex("dbo.IdentityUserLogins", new[] {"AppUser_Id"});
            DropIndex("dbo.IdentityUserClaims", new[] {"AppUser_Id"});
            DropIndex("dbo.IdentityUserRoles", new[] {"IdentityRole_Id"});
            DropIndex("dbo.IdentityRoles", new[] {"AppUser_Id"});
            DropIndex("dbo.HistoriaZmianWieznia", new[] {"FK_HistoriaZmian_idWpisuWieznia"});
            DropIndex("dbo.HistoriaZmianWieznia", new[] {"FK_idWieznia"});
            DropIndex("dbo.Przydzialy", new[] {"FK_idCeli"});
            DropIndex("dbo.Przydzialy", new[] {"FK_idWieznia"});
            DropIndex("dbo.HistoriaZmianPrzydzialow", new[] {"FK_HistoriaZmian_idWpisuPrzydzialu"});
            DropIndex("dbo.HistoriaZmianPrzydzialow", new[] {"FK_idWiezniaPrzydzialu"});
            DropIndex("dbo.HistoriaZmianWyrokow", new[] {"FK_HistoriaZmian_idWpisu"});
            DropIndex("dbo.HistoriaZmianWyrokow", new[] {"FK_idWyroku"});
            DropIndex("dbo.Wyroki", new[] {"FK_idKategoriiPrzestepstwa"});
            DropIndex("dbo.Wyroki", new[] {"FK_idWieznia"});
            DropIndex("dbo.KategoriaPrzestepstwaOdzial", new[] {"FK_idKategoriiPrzestepstwa"});
            DropIndex("dbo.KategoriaPrzestepstwaOdzial", new[] {"FK_idOdzial"});
            DropIndex("dbo.Cele", new[] {"FK_idOdzial"});
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.IdentityUserClaims");
            DropTable("dbo.AppUsers");
            DropTable("dbo.Table");
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.HistoriaZmianWieznia");
            DropTable("dbo.Wiezniowie");
            DropTable("dbo.Przydzialy");
            DropTable("dbo.HistoriaZmianPrzydzialow");
            DropTable("dbo.HistoriaZmian");
            DropTable("dbo.HistoriaZmianWyrokow");
            DropTable("dbo.Wyroki");
            DropTable("dbo.KategoriePrzestepstwa");
            DropTable("dbo.KategoriaPrzestepstwaOdzial");
            DropTable("dbo.Odzial");
            DropTable("dbo.Cele");
        }
    }
}