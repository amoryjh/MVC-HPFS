namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alertMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Index", "AlertMessage", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Index", "AlertMessage");
        }
    }
}
