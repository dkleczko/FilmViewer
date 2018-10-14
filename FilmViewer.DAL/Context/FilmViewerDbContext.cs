using System.Data.Entity;
using FilmViewer.DAL.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FilmViewer.DAL.Context
{

    public class FilmViewerDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<MainMovie> MainMovie { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviePerson> MoviePersons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Metadata> Metadatas { get; set; }
        public DbSet<VoteMoviePerson> VoteMoviePersons { get; set; }
        public DbSet<VoteMovie> VoteMovies { get; set; }
        public DbSet<UserActivities> UserActivities { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Actor> Actors { get; set; }


        public FilmViewerDbContext()
            : base("FilmViewerDbConnection", throwIfV1Schema: false)
        {
        }

        public static FilmViewerDbContext Create()
        {
            return new FilmViewerDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasMany(e => e.Comments).WithOptional(s => s.Movie).WillCascadeOnDelete(true);
            modelBuilder.Entity<Movie>().HasMany(e => e.PhotoUrls).WithOptional().WillCascadeOnDelete(true);
            modelBuilder.Entity<MoviePerson>().HasMany(e => e.PhotoUrls).WithOptional().WillCascadeOnDelete(true);

            base.OnModelCreating(modelBuilder);
        }

    }
}
