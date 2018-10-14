using System.Web.Mvc;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Models.User;
using PagedList;

namespace FilmViewer.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserDataProvider _userDataProvider;
        public UserController(IUserDataProvider userDataProvider)
        {
            _userDataProvider = userDataProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var indexUserModel = new UserViewModel()
            {
                LikedMovies = _userDataProvider.GetUserFavouritesCount(ApplicationUser.Id),
                VotedMovies = _userDataProvider.GetUserMovieVotesCount(ApplicationUser.Id),
                VotedActors = _userDataProvider.GetUserActorVotesCount(ApplicationUser.Id)
            };
            return View(indexUserModel);
        }
        [HttpGet]
        public ViewResult VotedMovies(int? page)
        {
            var pageSize = 10;
            var pageNumber = (page ?? 1);
            var votedMovies = _userDataProvider.GetVotedMoviesByUser(ApplicationUser.Id);
            var viewModel = new VotedMoviesViewModel()
            {
                VotedMovies = votedMovies.ToPagedList(pageNumber, pageSize)
            };
            return View(viewModel);
        }
        [HttpGet]
        public ViewResult VotedActors(int? page)
        {
            var votedActors = _userDataProvider.GetVotesActorsByUser(ApplicationUser.Id);
            var pageSize = 10;
            var pageNumber = (page ?? 1);
            var viewModel  = new VotedActorsViewModel()
            {
                VotedActors = votedActors.ToPagedList(pageNumber, pageSize)
            };


            return View(viewModel);
        }

        [HttpGet]
        public ViewResult VotedDirectors(int? page)
        {
            var votedDirectors = _userDataProvider.GetVotedDirectorsByUser(ApplicationUser.Id);
            var pageSize = 10;
            var pageNumber = (page ?? 1);
            var viewModel = new VotedDirectorsViewModel()
            {
                VotedDirectors = votedDirectors.ToPagedList(pageNumber, pageSize)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult Favourites(int? page)
        {
            var favouriteMovies = _userDataProvider.GetFavouritesMoviesByUser(ApplicationUser.Id);

            var pageSize = 10;
            var pageNumber = (page ?? 1);
            var viewModel = new FavouritesViewModel()
            {
                FavouritesMovies = favouriteMovies.ToPagedList(pageNumber, pageSize)
            };

            return View(viewModel);
        }
        [HttpGet]
        public ViewResult Recommendations()
        {
            var recommenderMovies = _userDataProvider.GetRecommendedMoviesWithSimilarity(ApplicationUser);

            var viewModel = new RecommendationsViewModel()
            {
                Movies = recommenderMovies
            };
            return View(viewModel);
        }
    }
}