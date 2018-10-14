using System.Web.Mvc;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Extended.Director;
using FilmViewer.Models.Director;
using PagedList;

namespace FilmViewer.Controllers
{
    public class DirectorController : BaseController
    {
        private IDirectorDataProvider _directorDataProvider;

        private IDirectorService _directorService;

        public DirectorController(IDirectorDataProvider directorDataProvider, IDirectorService directorService)
        {
            _directorDataProvider = directorDataProvider;
            _directorService = directorService;
        }

        [HttpGet]
        public ActionResult Index(int? page, string searchString = null)
        {
            ViewBag.DirectorFilter = searchString;
            var directors = _directorDataProvider.GetDirectorBySearchString(searchString, ApplicationUser);
            var pageSize = 5;
            var pageNumber = (page ?? 1);
            var actorsListViewModel = new DirectorListViewModel()
            {
                DirectorsList = directors.ToPagedList(pageNumber, pageSize)
            };
            return View(actorsListViewModel);

        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            var director = _directorDataProvider.GetDirectorDetailsPhotosAndMovies(id, ApplicationUser);
            var detailsViewModel = new DetailsViewModel()
            {
                Director = director
            };
            return View(detailsViewModel);
        }

        [HttpPost]
        [Authorize]
        public JsonResult AddVote(AddVoteRequest request)
        {
            var result = _directorService.VoteDirector(new VoteDirectorDto()
            {
                DirectorId = request.DirectorId,
                Score = request.Stars,
                UserId = ApplicationUser.Id
            });
            var obj = _directorDataProvider.GetCurrentDirectorVote(request.DirectorId);
            return Json(obj);
        }

        [HttpGet]
        public ViewResult RatingList()
        {
            var viewModel = new RatingListViewModel()
            {
                Top100Directors = _directorDataProvider.GetTop100VotedDirectors()
            };
            return View(viewModel);
        }
    }
}