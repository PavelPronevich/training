namespace CreateDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReportDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ReportDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ReportDate");
        }
    }
}
