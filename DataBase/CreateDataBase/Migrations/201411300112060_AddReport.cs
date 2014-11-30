namespace CreateDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportID = c.Int(nullable: false, identity: true),
                        fileReport = c.String(),
                        ManagerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportID)
                .ForeignKey("dbo.Managers", t => t.ManagerID, cascadeDelete: true)
                .Index(t => t.ManagerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "ManagerID", "dbo.Managers");
            DropIndex("dbo.Reports", new[] { "ManagerID" });
            DropTable("dbo.Reports");
        }
    }
}
