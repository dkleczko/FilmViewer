using System.Web.Mvc;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Enums;
using FilmViewer.Extension.Exceptions;
using FilmViewer.Models.Movies;
using PagedList;

namespace FilmViewer.Controllers
{
    public class MovieController : BaseController
    {
        private readonly IMovieDataProvider _movieDataProvider;
        private readonly ICommentDataProvider _commentDataProvider;
        private readonly IMovieService _movieService;
        public MovieController(IMovieDataProvider movieDataProvider, ICommentDataProvider commentDataProvider
            , IMovieService movieService) : base()
        {
            _movieDataProvider = movieDataProvider;
            _commentDataProvider = commentDataProvider;
            _movieService = movieService;

        }

        [HttpGet]
        public ActionResult Index(string searchString, int? searchBy, int? page, int? category)
        {
            var searchByEnum = searchBy.HasValue ? (SearchMovieBy) searchBy : SearchMovieBy.NoSearch;
            ViewBag.CurrentFilter = searchString;
            ViewBag.SearchBy = searchBy;
            var movies = _movieDataProvider.SearchMovies(searchString, category, searchByEnum, ApplicationUser);
            var pageSize = 3;
            var pageNumber = (page ?? 1);
            var movieListModel = new MovieListViewModel()
            {
                Movies = movies.ToPagedList(pageNumber, pageSize),
            };
            return View(movieListModel);
        }

        [HttpGet]
        public ActionResult MostCommentedMovies()
        {
            var viewModel = new MostCommentedMoviesViewModel()
            {
                Movies = _movieDataProvider.GetTop10MostCommentedMovies()
            };
            return PartialView(viewModel);
        }

        [HttpGet]
        public ViewResult Details(int id, int? commentCount)
        {
            commentCount = commentCount ?? 10;
            var movie = _movieDataProvider.GetMovieById(id, ApplicationUser);
            var comments = _commentDataProvider.GetCommentsForMovie(id, commentCount.Value);
            var detailsViewModel = new DetailsViewModel()
            {
                Movie = movie,
                Comments = comments
            };

            return View(detailsViewModel);
        }

        [HttpGet]
        public ActionResult SimilarMovies(int movieId)
        {
            var movies = _movieDataProvider.GetSimilarMoviesToMovieId(movieId);

            var viewModel = new SimilarMoviesViewModel()
            {
                SimilarMovies = movies
            };

            return PartialView(viewModel);
        }


        [Authorize]
        [HttpPost]
        public int AddOrRemoveMovieFromFavourites(int id)
        {
            var result = _movieService.AddOrRemoveMovieFromFavourites(id, ApplicationUser.Id);
            return (int)result;
           
        }



        [Authorize]
        [HttpPost]
        public JsonResult AddVote(int movieId, int stars)
        {
            if (stars >= 0 && stars <= 5)
            {
                var result = _movieService.VoteMovie(stars, movieId, ApplicationUser.Id);
                return Json(result);
            }
            throw new NoValidStarsScoreException(string.Format("No valid stars value ({0})", stars));
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(string content, int movieId)
        {
            if(content.Length > 0)
            {
                _movieService.AddComment(content, movieId, ApplicationUser.Id);
            }
            return RedirectToAction("Details", new { id = movieId });
        }


        [HttpGet]
        public ViewResult RatingList()
        {

            var movies = _movieDataProvider.GetTop100BestRatedMovies();
            var viewModel = new RatingListViewModel()
            {
                RatingMoviesList = movies
            };
            return View(viewModel);
        }
    }
}