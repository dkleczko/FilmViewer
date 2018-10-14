using FilmViewer.Models.Home;
using System.Web.Mvc;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Extended.Email;

namespace FilmViewer.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IMovieDataProvider _movieDataProvider;
        private readonly IMainMovieDataProvider _mainMovieDataProvider;
        private readonly IEmailService _emailService;
        public HomeController(IMovieDataProvider movieDataProvider, IMainMovieDataProvider mainMovieDataProvider, IEmailService emailService)
        {
            _movieDataProvider = movieDataProvider;
            _mainMovieDataProvider = mainMovieDataProvider;
            _emailService = emailService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            return View(homeViewModel);
        }
        [HttpGet]
        public ActionResult MainMoviesList()
        {
            var mainMovieList = new MainMoviesListViewModel()
            {
                MainMovieList = _mainMovieDataProvider.GetAllMainMovies()
            };

            return PartialView(mainMovieList);
        }
        [HttpGet]
        public ActionResult Top6RatesMovies()
        {
            var top6RatesMoviesViewModel = new Top6RatesMoviesViewModel()
            {
                Top6Movies = _movieDataProvider.GeTop6RatesMovies()
            };

            return PartialView(top6RatesMoviesViewModel);
        }

        [HttpGet]
        public ActionResult SelectedMoviesForCurrentUser()
        {
            var selectedMoviesForCurrentUser = new SelectedMoviesForCurrentUserViewModel()
            {
                PrepardMoviesForUser = _movieDataProvider.GetRecommendedMoviesForUser(ApplicationUser)
            };

            return PartialView(selectedMoviesForCurrentUser);
        }

        [HttpGet]
        public ViewResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(string name, string email, string message)
        {
            _emailService.SendEmailMessage(new EmailMessageDto()
            {
                ClientName = name,
                EmailAddress = email,
                Message = message
            });
            return RedirectToAction("Contact");
        }

        public PartialViewResult Menu()
        {
            return PartialView("~/Views/Shared/_Layout.cshtml");
        }
    }
}