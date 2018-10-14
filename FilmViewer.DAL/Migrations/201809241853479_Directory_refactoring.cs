namespace FilmViewer.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Directory_refactoring : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Actors", newName: "MoviePersons");
            RenameTable(name: "dbo.MovieActors", newName: "MoviePersonMovies");
            RenameTable(name: "dbo.VoteActors", newName: "VoteMoviePersons");
            RenameColumn(table: "dbo.PhotoPaths", name: "Actor_Id", newName: "MoviePerson_Id");
            RenameColumn(table: "dbo.MoviePersonMovies", name: "Actor_Id", newName: "MoviePerson_Id");
            RenameColumn(table: "dbo.VoteMoviePersons", name: "Actor_Id", newName: "MoviePerson_Id");
            RenameIndex(table: "dbo.PhotoPaths", name: "IX_Actor_Id", newName: "IX_MoviePerson_Id");
            RenameIndex(table: "dbo.VoteMoviePersons", name: "IX_Actor_Id", newName: "IX_MoviePerson_Id");
            RenameIndex(table: "dbo.MoviePersonMovies", name: "IX_Actor_Id", newName: "IX_MoviePerson_Id");
            DropPrimaryKey("dbo.MoviePersonMovies");
            AddPrimaryKey("dbo.MoviePersonMovies", new[] { "MoviePerson_Id", "Movie_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.MoviePersonMovies");
            AddPrimaryKey("dbo.MoviePersonMovies", new[] { "Movie_Id", "Actor_Id" });
            RenameIndex(table: "dbo.MoviePersonMovies", name: "IX_MoviePerson_Id", newName: "IX_Actor_Id");
            RenameIndex(table: "dbo.VoteMoviePersons", name: "IX_MoviePerson_Id", newName: "IX_Actor_Id");
            RenameIndex(table: "dbo.PhotoPaths", name: "IX_MoviePerson_Id", newName: "IX_Actor_Id");
            RenameColumn(table: "dbo.VoteMoviePersons", name: "MoviePerson_Id", newName: "Actor_Id");
            RenameColumn(table: "dbo.MoviePersonMovies", name: "MoviePerson_Id", newName: "Actor_Id");
            RenameColumn(table: "dbo.PhotoPaths", name: "MoviePerson_Id", newName: "Actor_Id");
            RenameTable(name: "dbo.VoteMoviePersons", newName: "VoteActors");
            RenameTable(name: "dbo.MoviePersonMovies", newName: "MovieActors");
            RenameTable(name: "dbo.MoviePersons", newName: "Actors");
        }
    }
}
