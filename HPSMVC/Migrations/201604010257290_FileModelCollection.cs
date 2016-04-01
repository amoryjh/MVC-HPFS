namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileModelCollection : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Event", "File_ID", "dbo.File");
            DropIndex("dbo.Event", new[] { "File_ID" });
            DropColumn("dbo.Event", "File_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "File_ID", c => c.Int());
            CreateIndex("dbo.Event", "File_ID");
            AddForeignKey("dbo.Event", "File_ID", "dbo.File", "ID");
        }
    }
}
