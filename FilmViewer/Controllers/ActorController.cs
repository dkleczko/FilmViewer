using System.Web.Mvc;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Extended.Actor;
using FilmViewer.Models.Actors;
using PagedList;
namespace FilmViewer.Controllers
{
    public class ActorController : BaseController
    {
        
        private readonly IActorDataProvider _actorDataProvider;
        private readonly IActorService _actorService;
        public ActorController(IActorDataProvider actorDataProvider, IActorService actorService)
        {
            _actorDataProvider = actorDataProvider;
            _actorService = actorService;
        }

        [HttpGet]
        public ActionResult Index(int? page, string searchString = null)
        {
            ViewBag.ActorFilter = searchString;
            var actors = _actorDataProvider.GetActorBySearchString(searchString, ApplicationUser);
            var pageSize = 5;
            var pageNumber = (page ?? 1);
            var actorsListViewModel = new ActorsListViewModel()
            {
                Actors = actors.ToPagedList(pageNumber, pageSize)

            };
            return View(actorsListViewModel);
        }

        [HttpGet]
        public ViewResult Details(int id)
        {
            var actor = _actorDataProvider.GetActorDetailsPhotosAndMovies(id, ApplicationUser);
            var detailsViewModel = new DetailsViewModel()
            {
                Actor = actor
            };
            return View(detailsViewModel);
        }
        [HttpPost]
        [Authorize]
        public JsonResult AddVote(AddVoteRequest request)
        {
            var result = _actorService.VoteActor(new VoteActorDto()
            {
                ActorId = request.ActorId,
                Score = request.Stars,
                UserId = ApplicationUser.Id
            });
            var obj = _actorDataProvider.GetCurrentActorVote(request.ActorId);
            return Json(obj);
        }
        [HttpGet]
        public ViewResult RatingList()
        {
            var viewModel = new RatingListViewModel()
            {
                TopActorsList = _actorDataProvider.GetTop100VotedActors()
            }; 
            return View(viewModel);
        }
    }
}