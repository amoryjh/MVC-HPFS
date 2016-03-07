namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProgramFiles : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Program", "FileID", "dbo.File");
            DropIndex("dbo.Program", new[] { "FileID" });
            AddColumn("dbo.Program", "fileName", c => c.String(maxLength: 250));
            AddColumn("dbo.Program", "fileType", c => c.String());
            AddColumn("dbo.Program", "fileContent", c => c.Binary(nullable: false));
            AlterColumn("dbo.Program", "Title", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Program", "Content", c => c.String(nullable: false, maxLength: 800));
            DropColumn("dbo.Program", "FileID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Program", "FileID", c => c.Int(nullable: false));
            AlterColumn("dbo.Program", "Content", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Program", "Title", c => c.String(nullable: false, maxLength: 40));
            DropColumn("dbo.Program", "fileContent");
            DropColumn("dbo.Program", "fileType");
            DropColumn("dbo.Program", "fileName");
            CreateIndex("dbo.Program", "FileID");
            AddForeignKey("dbo.Program", "FileID", "dbo.File", "ID");
        }
    }
}
