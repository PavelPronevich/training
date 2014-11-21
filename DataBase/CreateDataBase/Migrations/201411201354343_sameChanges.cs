namespace CreateDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sameChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "CustomerName", c => c.String());
            AlterColumn("dbo.Goods", "GoodsName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Goods", "GoodsName", c => c.Int(nullable: false));
            AlterColumn("dbo.Customers", "CustomerName", c => c.Int(nullable: false));
        }
    }
}
