namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plzwork : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Program", "fileContent", c => c.Binary());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Program", "fileContent", c => c.Binary(nullable: false));
        }
    }
}
