namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class New : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.File", "EventID", "dbo.Event");
            DropIndex("dbo.File", new[] { "EventID" });
            CreateTable(
                "dbo.FileEvent",
                c => new
                    {
                        File_ID = c.Int(nullable: false),
                        Event_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.File_ID, t.Event_ID })
                .ForeignKey("dbo.File", t => t.File_ID, cascadeDelete: true)
                .ForeignKey("dbo.Event", t => t.Event_ID, cascadeDelete: true)
                .Index(t => t.File_ID)
                .Index(t => t.Event_ID);
            
            AddColumn("dbo.Index", "FileID", c => c.Int(nullable: false));
            AddColumn("dbo.Program", "FileID", c => c.Int(nullable: false));
            CreateIndex("dbo.Index", "FileID");
            CreateIndex("dbo.Program", "FileID");
            AddForeignKey("dbo.Index", "FileID", "dbo.File", "ID");
            AddForeignKey("dbo.Program", "FileID", "dbo.File", "ID");
            DropColumn("dbo.File", "EventID");
            DropColumn("dbo.Index", "Image");
            DropColumn("dbo.Program", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Program", "Image", c => c.Binary(nullable: false));
            AddColumn("dbo.Index", "Image", c => c.Binary(nullable: false));
            AddColumn("dbo.File", "EventID", c => c.Int());
            DropForeignKey("dbo.Program", "FileID", "dbo.File");
            DropForeignKey("dbo.Index", "FileID", "dbo.File");
            DropForeignKey("dbo.FileEvent", "Event_ID", "dbo.Event");
            DropForeignKey("dbo.FileEvent", "File_ID", "dbo.File");
            DropIndex("dbo.FileEvent", new[] { "Event_ID" });
            DropIndex("dbo.FileEvent", new[] { "File_ID" });
            DropIndex("dbo.Program", new[] { "FileID" });
            DropIndex("dbo.Index", new[] { "FileID" });
            DropColumn("dbo.Program", "FileID");
            DropColumn("dbo.Index", "FileID");
            DropTable("dbo.FileEvent");
            CreateIndex("dbo.File", "EventID");
            AddForeignKey("dbo.File", "EventID", "dbo.Event", "ID");
        }
    }
}
