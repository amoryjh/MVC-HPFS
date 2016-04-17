namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class viewerReqOnFile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.File", "Viewer", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.File", "Viewer", c => c.String(maxLength: 100));
        }
    }
}
