namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class picturesplz : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.File", "fileContent", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.File", "fileContent", c => c.Binary(nullable: false));
        }
    }
}
