namespace FilmViewer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Birthdate = c.DateTime(),
                        BirthPlace = c.String(maxLength: 200),
                        Folder = c.String(),
                        Biography = c.String(),
                        PhotoUrl = c.String(),
                        MaritalStatus = c.String(),
                        VoteScores = c.Int(nullable: false),
                        VoteCount = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitlePl = c.String(nullable: false, maxLength: 400),
                        Folder = c.String(),
                        TitleEng = c.String(maxLength: 400),
                        Content = c.String(),
                        PremiereDate = c.DateTime(),
                        ProductionCountries = c.String(),
                        Duration = c.Int(nullable: false),
                        PhotoPath = c.String(),
                        HeraldUrl = c.String(),
                        VoteScores = c.Int(nullable: false),
                        voteCount = c.Int(nullable: false),
                        Metadata = c.String(),
                        Director_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.Director_Id)
                .Index(t => t.Director_Id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Content = c.String(),
                        User_Id = c.String(maxLength: 128),
                        Movie_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
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
                "dbo.VoteActors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteScore = c.Int(nullable: false),
                        Actor_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.Actor_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Actor_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.VoteMovies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoteScore = c.Int(nullable: false),
                        Movie_Id = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.Movie_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.PhotoPaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Movie_Id = c.Int(),
                        Actor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
            CreateTable(
                "dbo.MainMovies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieImagePath = c.String(nullable: false),
                        SliderType = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.Metadatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.UserActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActivityDate = c.DateTime(nullable: false),
                        UserId = c.String(maxLength: 128),
                        MovieId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.MovieId);
            
            CreateTable(
                "dbo.MovieActors",
                c => new
                    {
                        Movie_Id = c.Int(nullable: false),
                        Actor_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Movie_Id, t.Actor_Id })
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .Index(t => t.Movie_Id)
                .Index(t => t.Actor_Id);
            
            CreateTable(
                "dbo.CategoryMovies",
                c => new
                    {
                        Category_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Category_Id, t.Movie_Id })
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.Category_Id)
                .Index(t => t.Movie_Id);
            
            CreateTable(
                "dbo.ApplicationUserMovies",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Movie_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.Movie_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Movie_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserActivities", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserActivities", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MainMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.PhotoPaths", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.PhotoPaths", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Movies", "Director_Id", "dbo.Actors");
            DropForeignKey("dbo.Comments", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoteMovies", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoteMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.VoteActors", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoteActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.ApplicationUserMovies", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CategoryMovies", "Movie_Id", "dbo.Movies");
            DropForeignKey("dbo.CategoryMovies", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.MovieActors", "Actor_Id", "dbo.Actors");
            DropForeignKey("dbo.MovieActors", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.ApplicationUserMovies", new[] { "Movie_Id" });
            DropIndex("dbo.ApplicationUserMovies", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CategoryMovies", new[] { "Movie_Id" });
            DropIndex("dbo.CategoryMovies", new[] { "Category_Id" });
            DropIndex("dbo.MovieActors", new[] { "Actor_Id" });
            DropIndex("dbo.MovieActors", new[] { "Movie_Id" });
            DropIndex("dbo.UserActivities", new[] { "MovieId" });
            DropIndex("dbo.UserActivities", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MainMovies", new[] { "Movie_Id" });
            DropIndex("dbo.PhotoPaths", new[] { "Actor_Id" });
            DropIndex("dbo.PhotoPaths", new[] { "Movie_Id" });
            DropIndex("dbo.VoteMovies", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.VoteMovies", new[] { "Movie_Id" });
            DropIndex("dbo.VoteActors", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.VoteActors", new[] { "Actor_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "Movie_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Movies", new[] { "Director_Id" });
            DropTable("dbo.ApplicationUserMovies");
            DropTable("dbo.CategoryMovies");
            DropTable("dbo.MovieActors");
            DropTable("dbo.UserActivities");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Metadatas");
            DropTable("dbo.MainMovies");
            DropTable("dbo.PhotoPaths");
            DropTable("dbo.VoteMovies");
            DropTable("dbo.VoteActors");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.Movies");
            DropTable("dbo.Actors");
        }
    }
}
