namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picsd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.File", "fileName", c => c.String(maxLength: 250));
            AlterColumn("dbo.File", "fileType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.File", "fileType", c => c.String(nullable: false));
            AlterColumn("dbo.File", "fileName", c => c.String(nullable: false, maxLength: 250));
        }
    }
}
