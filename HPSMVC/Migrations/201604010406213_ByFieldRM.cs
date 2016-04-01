namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ByFieldRM : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Event", "By");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Event", "By", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
