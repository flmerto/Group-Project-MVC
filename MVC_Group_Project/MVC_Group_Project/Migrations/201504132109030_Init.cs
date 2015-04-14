namespace MVC_Group_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BiddingItems", "SubCategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.BiddingItems", "SubCategoryID");
            AddForeignKey("dbo.BiddingItems", "SubCategoryID", "dbo.SubCategories", "SubCategoryID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BiddingItems", "SubCategoryID", "dbo.SubCategories");
            DropIndex("dbo.BiddingItems", new[] { "SubCategoryID" });
            DropColumn("dbo.BiddingItems", "SubCategoryID");
        }
    }
}
