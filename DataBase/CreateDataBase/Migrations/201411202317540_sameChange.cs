namespace CreateDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sameChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                    })
                .PrimaryKey(t => t.ProductID);
            
            AddColumn("dbo.Orders", "ProductID", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CustomerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "ManagerID");
            CreateIndex("dbo.Orders", "ProductID");
            CreateIndex("dbo.Orders", "CustomerID");
            AddForeignKey("dbo.Orders", "CustomerID", "dbo.Customers", "CustomerID", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ManagerID", "dbo.Managers", "ManagerID", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ProductID", "dbo.Products", "ProductID", cascadeDelete: true);
            DropColumn("dbo.Orders", "GoddsID");
            DropTable("dbo.Goods");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Goods",
                c => new
                    {
                        GoodsID = c.Int(nullable: false, identity: true),
                        GoodsName = c.String(),
                    })
                .PrimaryKey(t => t.GoodsID);
            
            AddColumn("dbo.Orders", "GoddsID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Orders", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Orders", "ManagerID", "dbo.Managers");
            DropForeignKey("dbo.Orders", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerID" });
            DropIndex("dbo.Orders", new[] { "ProductID" });
            DropIndex("dbo.Orders", new[] { "ManagerID" });
            DropColumn("dbo.Orders", "CustomerID");
            DropColumn("dbo.Orders", "ProductID");
            DropTable("dbo.Products");
        }
    }
}
