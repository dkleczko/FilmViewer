namespace FilmViewer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class voteCountName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MoviePersonMovies", "MoviePerson_Id", "dbo.MoviePersons");
            DropForeignKey("dbo.MoviePersonMovies", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.MoviePersonMovies", new[] { "MoviePerson_Id" });
            DropIndex("dbo.MoviePersonMovies", new[] { "Movie_Id" });
            AddColumn("dbo.MoviePersons", "Movie_Id", c => c.Int());
            AddColumn("dbo.Movies", "MoviePerson_Id", c => c.Int());
            CreateIndex("dbo.MoviePersons", "Movie_Id");
            CreateIndex("dbo.Movies", "MoviePerson_Id");
            AddForeignKey("dbo.MoviePersons", "Movie_Id", "dbo.Movies", "Id");
            AddForeignKey("dbo.Movies", "MoviePerson_Id", "dbo.MoviePersons", "Id");
            DropTable("dbo.MoviePersonMovies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MoviePersonMovies",
                c => new
                    {
                        MoviePerson_Id = c.Int(nullable: false),
                        Movie_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MoviePerson_Id, t.Movie_Id });
            
            DropForeignKey("dbo.Movies", "MoviePerson_Id", "dbo.MoviePersons");
            DropForeignKey("dbo.MoviePersons", "Movie_Id", "dbo.Movies");
            DropIndex("dbo.Movies", new[] { "MoviePerson_Id" });
            DropIndex("dbo.MoviePersons", new[] { "Movie_Id" });
            DropColumn("dbo.Movies", "MoviePerson_Id");
            DropColumn("dbo.MoviePersons", "Movie_Id");
            CreateIndex("dbo.MoviePersonMovies", "Movie_Id");
            CreateIndex("dbo.MoviePersonMovies", "MoviePerson_Id");
            AddForeignKey("dbo.MoviePersonMovies", "Movie_Id", "dbo.Movies", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MoviePersonMovies", "MoviePerson_Id", "dbo.MoviePersons", "Id", cascadeDelete: true);
        }
    }
}
