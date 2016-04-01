namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HomeContent : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Index", "ButtonLink", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Index", "ButtonLink", c => c.String(maxLength: 100));
        }
    }
}
