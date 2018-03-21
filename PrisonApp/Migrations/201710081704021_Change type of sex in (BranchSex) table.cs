namespace PrisonApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangetypeofsexinBranchSextable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BranchesSex", "Sex", c => c.String(nullable: false, maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BranchesSex", "Sex", c => c.Int(nullable: false));
        }
    }
}
