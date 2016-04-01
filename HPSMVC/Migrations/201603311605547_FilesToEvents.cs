namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilesToEvents : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FileEvent", "File_ID", "dbo.File");
            DropForeignKey("dbo.FileEvent", "Event_ID", "dbo.Event");
            DropIndex("dbo.FileEvent", new[] { "File_ID" });
            DropIndex("dbo.FileEvent", new[] { "Event_ID" });
            AddColumn("dbo.Event", "fileName", c => c.String(maxLength: 250));
            AddColumn("dbo.Event", "fileType", c => c.String());
            AddColumn("dbo.Event", "fileContent", c => c.Binary());
            AddColumn("dbo.Event", "File_ID", c => c.Int());
            CreateIndex("dbo.Event", "File_ID");
            AddForeignKey("dbo.Event", "File_ID", "dbo.File", "ID");
            DropTable("dbo.FileEvent");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FileEvent",
                c => new
                    {
                        File_ID = c.Int(nullable: false),
                        Event_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.File_ID, t.Event_ID });
            
            DropForeignKey("dbo.Event", "File_ID", "dbo.File");
            DropIndex("dbo.Event", new[] { "File_ID" });
            DropColumn("dbo.Event", "File_ID");
            DropColumn("dbo.Event", "fileContent");
            DropColumn("dbo.Event", "fileType");
            DropColumn("dbo.Event", "fileName");
            CreateIndex("dbo.FileEvent", "Event_ID");
            CreateIndex("dbo.FileEvent", "File_ID");
            AddForeignKey("dbo.FileEvent", "Event_ID", "dbo.Event", "ID", cascadeDelete: true);
            AddForeignKey("dbo.FileEvent", "File_ID", "dbo.File", "ID", cascadeDelete: true);
        }
    }
}
