namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IndexChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Index", "FileID", "dbo.File");
            DropIndex("dbo.Index", new[] { "FileID" });
            AddColumn("dbo.Index", "fileName", c => c.String(maxLength: 250));
            AddColumn("dbo.Index", "fileType", c => c.String());
            AddColumn("dbo.Index", "fileContent", c => c.Binary());
            DropColumn("dbo.Index", "FileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Index", "FileID", c => c.Int(nullable: false));
            DropColumn("dbo.Index", "fileContent");
            DropColumn("dbo.Index", "fileType");
            DropColumn("dbo.Index", "fileName");
            CreateIndex("dbo.Index", "FileID");
            AddForeignKey("dbo.Index", "FileID", "dbo.File", "ID");
        }
    }
}
