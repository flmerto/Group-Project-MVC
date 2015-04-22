namespace MVC_Group_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BiddingItems",
                c => new
                    {
                        BiddingItemID = c.Int(nullable: false, identity: true),
                        ItemName = c.String(nullable: false),
                        ItemDescription = c.String(nullable: false),
                        ItemImageURL = c.String(),
                        SubCategoryID = c.Int(nullable: false),
                        BidStartTime = c.DateTime(nullable: false),
                        BidEndTime = c.DateTime(nullable: false),
                        BidStartPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        HighestBidPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BiddingItemID)
                .ForeignKey("dbo.SubCategories", t => t.SubCategoryID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.SubCategoryID)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryID = c.Int(nullable: false, identity: true),
                        SubCategoryName = c.String(),
                        ImagePath = c.String(),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SubCategoryID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        ImagePath = c.String(),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Address = c.String(),
                        LastName = c.String(),
                        FirstName = c.String(),
                        RoleName = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardID = c.Int(nullable: false, identity: true),
                        CardType = c.String(),
                        CardHolderName = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        CSC = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CreditCardID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OnGoingBids",
                c => new
                    {
                        OnGoingBidsID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        BiddingItemID = c.Int(nullable: false),
                        BidPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BidTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OnGoingBidsID)
                .ForeignKey("dbo.BiddingItems", t => t.BiddingItemID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.BiddingItemID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionsID = c.Int(nullable: false, identity: true),
                        PurchaseDateTime = c.DateTime(nullable: false),
                        Amount = c.Double(nullable: false),
                        BiddingItemID = c.Int(nullable: false),
                        CreditCardID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionsID)
                .ForeignKey("dbo.BiddingItems", t => t.BiddingItemID, cascadeDelete: true)
                .ForeignKey("dbo.CreditCards", t => t.CreditCardID, cascadeDelete: true)
                .Index(t => t.BiddingItemID)
                .Index(t => t.CreditCardID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "CreditCardID", "dbo.CreditCards");
            DropForeignKey("dbo.Transactions", "BiddingItemID", "dbo.BiddingItems");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OnGoingBids", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.OnGoingBids", "BiddingItemID", "dbo.BiddingItems");
            DropForeignKey("dbo.CreditCards", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BiddingItems", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BiddingItems", "SubCategoryID", "dbo.SubCategories");
            DropForeignKey("dbo.SubCategories", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Transactions", new[] { "CreditCardID" });
            DropIndex("dbo.Transactions", new[] { "BiddingItemID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OnGoingBids", new[] { "BiddingItemID" });
            DropIndex("dbo.OnGoingBids", new[] { "UserId" });
            DropIndex("dbo.CreditCards", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SubCategories", new[] { "CategoryID" });
            DropIndex("dbo.BiddingItems", new[] { "UserId" });
            DropIndex("dbo.BiddingItems", new[] { "SubCategoryID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OnGoingBids");
            DropTable("dbo.CreditCards");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Categories");
            DropTable("dbo.SubCategories");
            DropTable("dbo.BiddingItems");
        }
    }
}
