namespace HPSMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fitbits : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FitBit",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        User = c.String(nullable: false),
                        Progress = c.String(nullable: false, maxLength: 7),
                        Goal = c.String(nullable: false, maxLength: 7),
                        dateStart = c.DateTime(),
                        dateEnd = c.DateTime(),
                        percentageEarned = c.String(nullable: false, maxLength: 4),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FitBit");
        }
    }
}
