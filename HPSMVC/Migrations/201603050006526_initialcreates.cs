namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Event", "LinkText", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Event", "LinkText");
        }
    }
}
