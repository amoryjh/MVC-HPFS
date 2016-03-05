namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picturest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.File", "fileName", c => c.String(nullable: false, maxLength: 250));
            AddColumn("dbo.File", "fileType", c => c.String(nullable: false));
            AddColumn("dbo.File", "fileContent", c => c.Binary(nullable: false));
            DropColumn("dbo.File", "Name");
            DropColumn("dbo.File", "Type");
            DropColumn("dbo.File", "Data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.File", "Data", c => c.Binary(nullable: false));
            AddColumn("dbo.File", "Type", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.File", "Name", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.File", "fileContent");
            DropColumn("dbo.File", "fileType");
            DropColumn("dbo.File", "fileName");
        }
    }
}
