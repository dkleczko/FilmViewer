using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PagedList;
using FilmViewer.AppHelpers;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Enums;
using FilmViewer.DAL.Context;
using FilmViewer.DAL.Model;
using FilmViewer.Localization;
using FilmViewer.Mappings;
using FilmViewer.Models;
using FilmViewer.Models.Moderator;
using FilmViewer.Roles;
using FilmViewer.Statics;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IndexViewModel = FilmViewer.Models.Moderator.IndexViewModel;

namespace FilmViewer.Controllers
{
    [Authorize(Roles = CustomRoles.Moderator)]
    public class ModeratorController : BaseController
    {
        private readonly IMovieDataProvider _movieDataProvider;
        private readonly IActorDataProvider _actorDataProvider;
        private readonly ICategoryDataProvider _categoryDataProvider;
        private readonly IDirectorDataProvider _directorDataProvider;
        private readonly IMetadataDataProvider _metadataDataProvider;
        private readonly IMainMovieDataProvider _mainMovieDataProvider;

        private readonly IMovieService _movieService;
        private readonly ICategoryService _categoryService;
        private readonly IActorService _actorService;
        private readonly IMetadataService _metadataService;
        private readonly IMainMovieService _mainMovieService;
        private readonly IDirectorService _directorService;
        public ModeratorController(IMovieDataProvider movieDataProvider,
            IActorDataProvider actorDataProvider, ICategoryDataProvider categoryDataProvider, IMovieService movieService,
            IDirectorDataProvider directorDataProvider, IMetadataDataProvider metadataDataProvider, ICategoryService categoryService, IActorService actorService,
            IMetadataService metadataService, IMainMovieService mainMovieService, IMainMovieDataProvider mainMovieDataProvider, IDirectorService directorService)
            : base()
        {
            _movieDataProvider = movieDataProvider;
            _actorDataProvider = actorDataProvider;
            _categoryDataProvider = categoryDataProvider;
            _directorDataProvider = directorDataProvider;
            _metadataDataProvider = metadataDataProvider;
            _mainMovieDataProvider = mainMovieDataProvider;

            _movieService = movieService;
            _categoryService = categoryService;
            _actorService = actorService;
            _metadataService = metadataService;
            _mainMovieService = mainMovieService;
            _directorService = directorService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> CreateModerator()
        {
            var user = new ApplicationUser() { UserName = "Moderator", FirstName = "Damian", LastName = "Kleczko" };
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new FilmViewerDbContext()));
            var roleresult = roleManager.Create(new IdentityRole("Moderator"));
            var result = await UserManager.CreateAsync(user, "Moderator");
            if (result.Succeeded)
            {
                var usr = UserManager.FindByName("Moderator");
                var r = await UserManager.AddToRoleAsync(usr.Id, "Moderator");
                return true;
            }
            return false;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<bool> CreateRoleUser()
        {
            var roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new FilmViewerDbContext()));
            var roleresult = roleManager.Create(new IdentityRole("User"));
            return roleresult.Succeeded;

        }

        [HttpGet]
        public ActionResult Index()
        {
            var actorsCount = _actorDataProvider.GetAllActorsCount();
            var moviesCount = _movieDataProvider.GetCountOfAllMovies();
            var categoriesCount = _categoryDataProvider.GetAllCatagoriesCount();
            var indexViewModel = new IndexViewModel()
            {
                ActorsCount = actorsCount,
                MoviesCount = moviesCount,
                CategoriesCount = categoriesCount
            };
            return View(indexViewModel);
        }
        [HttpGet]
        public ViewResult CreateFilm()
        {
            return View(new CreateFilmViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilm(CreateFilmViewModel viewModel, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int? directorId = null;
                if (int.TryParse(collection[CreateFilmViewModel.DirectorCollectionKey], out var dt))
                {
                    directorId = dt;
                }
                var movieId = _movieService.AddMovie(FilmViewerMapper.Mapper.Map<MovieDetailsDto>(viewModel),
                    ManagerHelper.CategoryCommaCollectionRefactor(collection[CreateFilmViewModel.ActorCollectionKey]),
                    directorId,
                    ManagerHelper.CategoryCommaCollectionRefactor(collection[CreateFilmViewModel.MetadataCollectionKey]),
                    ManagerHelper.CategoryCommaCollectionRefactor(collection[CreateFilmViewModel.CategoryCollectionKey]),
                    Server.MapPath(FolderPaths.UploadPath),
                    FolderPaths.UploadPath
                );
                return RedirectToAction("AddPhotoToFilm", new { id = movieId });
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult AddPhotoToFilm(int id)
        {
            var movie = _movieDataProvider.GetMovieById(id, null);
            var addPhotoToMovieViewModel = new AddPhotoToMovieViewModel()
            {
                Movie = movie
            };
            return View(addPhotoToMovieViewModel);

        }

        [HttpPost]
        [ActionName("AddPhotoToFilm")]
        public ActionResult AddPhotoToFilmPost(int id)
        {
            var movie = _movieDataProvider.GetMovieById(id, null);
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = FileHelpers.GenerateFilename(movie.Title, file.FileName);
                    var photoUrl = Path.Combine(
                        Server.MapPath(movie.Folder), fileName);
                    file.SaveAs(photoUrl);
                    movie.PhotoUrl = photoUrl;
                }
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var f = Request.Files[i];
                    if (f != null && f.ContentLength > 0)
                    {
                        var fileName = FileHelpers.GenerateFilename(movie.Title, f.FileName);
                        var photoUrl = Path.Combine(
                            Server.MapPath(movie.Folder), fileName);
                        f.SaveAs(photoUrl);
                        movie.Photos.Add(new PhotoPathDto
                        {
                            Path = photoUrl
                        });
                    }
                }
                _movieService.AddPhotosToMovie(movie);
                return RedirectToAction("Index");

            }
            return RedirectToAction("AddPhotoToFilm", new { id = movie.MovieId });
        }

        [HttpPost]
        public bool AddMorePhotoToFilm(int movieId)
        {
            var movie = _movieDataProvider.GetMovieById(movieId);
            if (Request.Files.Count > 0 && movie != null)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var f = Request.Files[i];
                    if (f != null && f.ContentLength > 0)
                    {
                        var fileName = Helper.GetRandomCharacters() + movie.Title + Path.GetFileName(f.FileName);
                        var path = Path.Combine(
                            Server.MapPath(movie.Folder), fileName);
                        movie.Photos.Add(new PhotoPathDto
                        {
                            Path = path
                        });

                        f.SaveAs(path);

                    }
                }
                _movieService.AddPhotosToMovie(movie);
                return true;
            }
            return false;
        }

        [HttpPost]
        public bool RemoveMoviePhoto(string fileName, int movieId)
        {
            _movieService.DeletePhotoFromMovie(fileName, movieId);

            return true;
        }

        [HttpGet]
        [OutputCache(Duration = 200)]
        public JsonResult SearchCategoryWithName(string searchTerm)
        {
            var result = _categoryDataProvider.GetCategoriesBySearchString(searchTerm);
            var jsonResult = result.Select(results => new { id = results.CategoryId, name = results.CategoryName });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 200)]
        public JsonResult SearchActorWithName(string searchTerm)
        {
            var result = _actorDataProvider.GetActorBySearchString(searchTerm, null);
            var jsonResult = result.Select(results => new { id = results.Id, name = results.Name });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 200)]
        public JsonResult SearchDirectorWithName(string searchTerm)
        {
            var result = _directorDataProvider.GetDirectorsBySearchString(searchTerm);
            var jsonResult = result.Select(results => new { id = results.Id, name = results.Name });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OutputCache(Duration = 300)]
        public JsonResult SearchMetadataWithName(string searchTerm)
        {
            var result = _metadataDataProvider.GetMetadataBySearchString(searchTerm);
            var jsonResult = result.Select(results => new { id = results.MetadataId, name = results.MetadataName });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [OutputCache(Duration = 300)]
        public JsonResult SearchMovieWithName(string searchTerm)
        {
            var result = _movieDataProvider.SearchMovies(searchTerm, null, SearchMovieBy.Title, null);
            var jsonResult = result.Select(results => new { id = results.MovieId, name = results.Title });
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ViewResult CreateCategory()
        {
            return View(new CreateCategoryViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory(CreateCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var result = _categoryService.AddCategory(new CategoryDto()
                {
                    CategoryName = category.CategoryName
                });
                switch (result)
                {
                    case AddResult.DuplicateElement:
                        ModelState.AddModelError("Duplicate", Resources.CategoryAlredyAdded);
                        break;
                    case AddResult.UnknownError:
                        ModelState.AddModelError("UnknownError", Resources.UnknownError);
                        break;
                    case AddResult.Added:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-success",
                            Message = Resources.CategoryAdded
                        };
                        break;
                }
                return View("CreateCategory");
            }
            return View(category);

        }

        [HttpGet]
        public ViewResult CreateActor()
        {
            return View(new CreateActorViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateActor(CreateActorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var actorDetailsDto = FilmViewerMapper.Mapper.Map<ActorDetailsDto>(viewModel);
                var actorId = _actorService.AddActor(actorDetailsDto, Server.MapPath(FolderPaths.UploadPath),
                    FolderPaths.UploadPath);
                return RedirectToAction("AddPhotoToActor", new { id = actorId });
            }

            return View(viewModel);
        }

        [HttpGet]
        public ViewResult CreateDirector()
        {
            return View(new CreateDirectorViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDirector(CreateDirectorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var directorDetailsDto = FilmViewerMapper.Mapper.Map<DirectorDetailsDto>(viewModel);
                var directorId = _directorService.AddDirector(directorDetailsDto, Server.MapPath(FolderPaths.UploadPath),
                    FolderPaths.UploadPath);
                return RedirectToAction("AddPhotoToDirector", new { id = directorId });
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult AddPhotoToDirector(int id)
        {
            var directorDto = _directorDataProvider.GetDirectorDtoById(id);
            var viewModel = new AddPhotoToDirectorViewModel()
            {
                Director = directorDto
            };
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("AddPhotoToDirector")]
        public ActionResult AddPhotoToDirectorPost(int id)
        {
            var director = _directorDataProvider.GetDirectorDtoById(id);
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Helper.GetRandomCharacters() + director.Name + Path.GetFileName(file.FileName);
                    director.PhotoUrl = Path.Combine(
                        Server.MapPath(director.Folder), fileName);
                    file.SaveAs(director.PhotoUrl);
                }
                var phototsList = new List<PhotoPathDto>();
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var f = Request.Files[i];
                    if (f.ContentLength > 0)
                    {
                        var fileName = Helper.GetRandomCharacters() + director.Name + Path.GetFileName(f.FileName);
                        phototsList.Add(new PhotoPathDto()
                        {
                            Path = Path.Combine(
                                Server.MapPath(director.Folder), fileName)
                        });
                        f.SaveAs(Path.Combine(
                            Server.MapPath(director.Folder), fileName));
                    }
                }
                _directorService.AddPhotosToDirector(director, phototsList);

                return RedirectToAction("Index");
            }
            else
            {
                var addPhotoToDirectorViewModel = new AddPhotoToDirectorViewModel()
                {
                    Director = director
                };
                return View(addPhotoToDirectorViewModel);
            }
        }
        [HttpGet]
        public ActionResult AddPhotoToActor(int id)
        {
            var actor = _actorDataProvider.GetActorDetailsPhotosAndMovies(id);
            var viewModel = new AddPhotoToActorViewModel()
            {
                Actor = actor
            };
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("AddPhotoToActor")]
        public ActionResult AddPhotoToActorPost(int id)
        {
            var actor = _actorDataProvider.GetActorDetailsPhotosAndMovies(id);
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Helper.GetRandomCharacters() + actor.Name + Path.GetFileName(file.FileName);
                    actor.PhotoUrl = Path.Combine(
                        Server.MapPath(actor.Folder), fileName);
                    file.SaveAs(actor.PhotoUrl);
                }
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var f = Request.Files[i];
                    if (f.ContentLength > 0)
                    {
                        var fileName = Helper.GetRandomCharacters() + actor.Name + Path.GetFileName(f.FileName);
                        actor.Photos.Add(new PhotoPathDto()
                        {
                            Path = Path.Combine(
                                Server.MapPath(actor.Folder), fileName)
                        });
                        f.SaveAs(Path.Combine(
                            Server.MapPath(actor.Folder), fileName));
                    }
                }
                _actorService.AddPhotosToActor(actor);

                return RedirectToAction("Index");
            }
            else
            {
                var addPhotoToActorViewModel = new AddPhotoToActorViewModel()
                {
                    Actor = actor
                };
                return View(addPhotoToActorViewModel);
            }
        }

        [HttpPost]
        public bool AddMorePhotoToActor(int actorId)
        {
            var actor = _actorDataProvider.GetActorDetailsPhotosAndMovies(actorId);
            if (Request.Files.Count > 0)
            {
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var f = Request.Files[i];
                    if (f.ContentLength > 0)
                    {
                        var fileName = Helper.GetRandomCharacters() + actor.Name + Path.GetFileName(f.FileName);
                        actor.Photos.Add(new PhotoPathDto()
                        {
                            Path = Path.Combine(
                                Server.MapPath(actor.Folder), fileName)
                        });
                        f.SaveAs(Path.Combine(
                            Server.MapPath(actor.Folder), fileName));
                    }
                }
                _actorService.AddPhotosToActor(actor);

                return true;
            }
            return false;
        }

        [HttpPost]
        public bool RemoveActorPhoto(string fileName, int actorId)
        {
            _actorService.DeletePhotoFromActor(fileName, actorId);

            return true;
        }

        [HttpGet]
        public ViewResult CreateMetadata()
        {
            return View(new CreateMetadataViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMetadata(CreateMetadataViewModel metadata)
        {
            if (ModelState.IsValid)
            {
                var isMetadataAlreadyExist = _metadataDataProvider.IsMetadataWithThisNameAdded(metadata.Name);
                if (isMetadataAlreadyExist)
                {
                    ModelState.AddModelError("MetadataAlreadyExist", Resources.MetadataAlreadyExist);
                    return View();
                }
                _metadataService.AddMetadata(FilmViewerMapper.Mapper.Map<MetadataDto>(metadata));
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-success",
                    Message = Resources.MetadataCreatedMessage
                };
                return RedirectToAction("CreateMetadata");
            }
            return View(metadata);
        }

        [HttpGet]
        public ViewResult ViewActors(string searchString, string currentFilter, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ActorsFilter = searchString;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var sortOrderEnum = SortOrder.Asc;
            var sortParamEnum = SortParam.Name;

            if (sortOrder == "name_desc")
            {
                sortOrderEnum = SortOrder.Desc;
            }
            else if (sortOrder == "date_desc")
            {
                sortOrderEnum = SortOrder.Desc;
                sortParamEnum = SortParam.Date;
            }
            else if (sortOrder == "Date")
            {
                sortParamEnum = SortParam.Date;
            }

            var actors = _actorDataProvider.GetActorBySearchString(searchString, sortParamEnum, sortOrderEnum);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.ActorsFilter = searchString;

            var pageSize = 10;
            var pageNumber = (page ?? 1);

            var viewModel = new ViewActorsViewModel()
            {
                Actors = actors.ToPagedList(pageNumber, pageSize)
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult ActorDelete(int id)
        {
            _actorService.DeleteActor(id);
            TempData[UserMessages.UserMessage] = new MessageViewModel()
            {
                CssClass = "alert-success",
                Message = Resources.ActorRemovedSuccesfully
            };
            return RedirectToAction("ViewActors");
        }

        [HttpGet]
        public ViewResult ActorEdit(int id)
        {
            var actor = _actorDataProvider.GetActorDetailsPhotosAndMovies(id);

            var viewModel = FilmViewerMapper.Mapper.Map<ActorEditViewModel>(actor);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ActorEdit(ActorEditViewModel actor)
        {
            if (ModelState.IsValid)
            {
                _actorService.EditActor(FilmViewerMapper.Mapper.Map<ActorDetailsDto>(actor));
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-success",
                    Message = Resources.ActorEditedSuccesfully
                };
            }
            return RedirectToAction("ViewActors");
        }

        [HttpGet]
        public ViewResult ViewDirectors(string searchString, string currentFilter, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ActorsFilter = searchString;
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var sortOrderEnum = SortOrder.Asc;
            var sortParamEnum = SortParam.Name;

            if (sortOrder == "name_desc")
            {
                sortOrderEnum = SortOrder.Desc;
            }
            else if (sortOrder == "date_desc")
            {
                sortOrderEnum = SortOrder.Desc;
                sortParamEnum = SortParam.Date;
            }
            else if (sortOrder == "Date")
            {
                sortParamEnum = SortParam.Date;
            }

            var directors = _directorDataProvider.GetDirectorBySearchString(searchString, sortParamEnum, sortOrderEnum);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.ActorsFilter = searchString;

            var pageSize = 10;
            var pageNumber = (page ?? 1);

            var viewModel = new ViewDirectorsViewModel()
            {
                Directors = directors.ToPagedList(pageNumber, pageSize)
            };
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult CategoryView(string searchString, int? page)
        {

            ViewBag.SearchCategory = searchString;

            var categories = _categoryDataProvider.GetCategoriesBySearchString(searchString);

            var pageSize = 10;
            var pageNumber = (page ?? 1);

            var viewModel = new CategoryViewViewModel()
            {
                CategoryList = categories.ToPagedList(pageNumber, pageSize)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategory(int id)
        {
            _categoryService.DeleteCategory(id);

            TempData[UserMessages.UserMessage] = new MessageViewModel()
            {
                CssClass = "alert-success",
                Message = Resources.CategoryRemovedSuccesfully
            };

            return RedirectToAction("CategoryView");
        }

        [HttpGet]
        public ViewResult EditCategory(int id)
        {
            var category = _categoryDataProvider.GetCategoryById(id);

            var viewModel = FilmViewerMapper.Mapper.Map<EditCategoryViewModel>(category);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory(EditCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var result =
                    _categoryService.EditCategory(
                        FilmViewerMapper.Mapper.Map<CategoryDto>(category));

                switch (result)
                {
                    case AddResult.DuplicateElement:
                        ModelState.AddModelError("Duplicate", Resources.CategoryAlredyAdded);
                        return View(category);
                    case AddResult.UnknownError:
                        ModelState.AddModelError("UnknownError", Resources.UnknownError);
                        return View(category);
                    case AddResult.Added:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-success",
                            Message = Resources.CategoryEdited
                        };
                        break;
                }
                return RedirectToAction("CategoryView");

            }
            return View(category);

        }

        [HttpGet]
        public ViewResult MetadataView(string searchString, string currentFilter, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.MetadataFilter = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.MetadataFilter = searchString;

            var sortOrderEnum = SortOrder.Asc;

            if (sortOrder == "name_desc")
            {
                sortOrderEnum = SortOrder.Desc;
            }

            var metadataDtosList = _metadataDataProvider.GetMetadataBySearchString(searchString, sortOrderEnum);

            var pageSize = 10;
            var pageNumber = (page ?? 1);

            var viewModel = new MetadataViewViewModel()
            {
                MetadatasList = metadataDtosList.ToPagedList(pageNumber, pageSize)
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMetadata(int id)
        {
            _metadataService.DeleteMetadata(id);
            TempData[UserMessages.UserMessage] = new MessageViewModel()
            {
                CssClass = "alert-success",
                Message = Resources.MetadataDeleted
            };
            return RedirectToAction("MetadataView");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMovie(int id)
        {
            _movieService.DeleteMovie(id);
            TempData[UserMessages.UserMessage] = new MessageViewModel()
            {
                CssClass = "alert-success",
                Message = Resources.MovieRemovedSuccesfully
            };

            return RedirectToAction("MovieView");
        }

        [HttpGet]
        public ViewResult EditMetadata(int id)
        {
            var metadataDto = _metadataDataProvider.GetMetadataById(id);

            var viewModel = FilmViewerMapper.Mapper.Map<EditMetadataViewModel>(metadataDto);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMetadata(EditMetadataViewModel metadata)
        {
            if (ModelState.IsValid)
            {
                var result = _metadataService.EditMetadata(FilmViewerMapper.Mapper.Map<MetadataDto>(metadata));
                switch (result)
                {
                    case AddResult.DuplicateElement:
                        ModelState.AddModelError("Duplicate", Resources.MetadataAlreadyExist);
                        return View(metadata);
                    case AddResult.UnknownError:
                        ModelState.AddModelError("UnknownError", Resources.UnknownError);
                        return View(metadata);
                    case AddResult.Added:
                        TempData[UserMessages.UserMessage] = new MessageViewModel()
                        {
                            CssClass = "alert-success",
                            Message = Resources.MetadataEditedSuccesfully
                        };
                        break;
                }
                return RedirectToAction("MetadataView");

            }
            return View(metadata);
        }

        [HttpGet]
        public ViewResult MovieView(string searchString, string currentFilter, string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitlePLParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PremiereDateParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.DurationParm = sortOrder == "Duration" ? "duration_desc" : "Duration";

            ViewBag.MovieFilter = searchString;

            var sortMovieByEnum = SortMovieBy.Name;
            var sortOrderEnum = SortOrder.Asc;

            switch (sortOrder)
            {
                case "name_desc":
                    sortOrderEnum = SortOrder.Desc;
                    break;
                case "date_desc":
                    sortMovieByEnum = SortMovieBy.PremiereDate;
                    sortOrderEnum = SortOrder.Desc;
                    break;
                case "Date":
                    sortMovieByEnum = SortMovieBy.PremiereDate;
                    break;
                case "duration_desc":
                    sortMovieByEnum = SortMovieBy.Duration;
                    sortOrderEnum = SortOrder.Desc;
                    break;
                case "Duration":
                    sortMovieByEnum = SortMovieBy.Duration;
                    break;

            }
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.MovieFilter = searchString;
            var moviesDto = _movieDataProvider.SearchMovie(searchString, sortMovieByEnum, sortOrderEnum);

            var pageSize = 10;
            var pageNumber = (page ?? 1);

            var viewModel = new MovieViewViewModel()
            {
                MovieList = moviesDto.ToPagedList(pageNumber, pageSize)
            };

            return View(viewModel);
        }

        [HttpGet]
        public ViewResult MovieEdit(int id)
        {
            var movieDto = _movieDataProvider.GetMovieById(id);

            var viewModel = FilmViewerMapper.Mapper.Map<MovieEditViewModel>(movieDto);

            return View(viewModel);

        }
        [HttpPost]
        public ActionResult MovieEdit(MovieEditViewModel viewModel, FormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int? directorId = null;
                if (int.TryParse(collection[CreateFilmViewModel.DirectorCollectionKey], out var dt))
                {
                    directorId = dt;
                }
                _movieService.EditMovie(FilmViewerMapper.Mapper.Map<MovieDetailsDto>(viewModel),
                   ManagerHelper.CategoryCommaCollectionRefactor(collection[CreateFilmViewModel.ActorCollectionKey]),
                   directorId,
                   ManagerHelper.CategoryCommaCollectionRefactor(collection[CreateFilmViewModel.MetadataCollectionKey]),
                   ManagerHelper.CategoryCommaCollectionRefactor(collection[CreateFilmViewModel.CategoryCollectionKey])
               );
                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-success",
                    Message = Resources.MovieEditedSuccesfully
                };

                return RedirectToAction("MovieView");

            }
            return RedirectToAction("MovieEdit", new { id = viewModel.MovieId });
        }

        [HttpGet]
        public ViewResult AddBestChoiceMovie()
        {
            return View(new AddBestChoiceMovieViewModel());
        }


        [HttpPost]
        public ActionResult AddBestChoiceMovie(AddBestChoiceMovieViewModel viewModel, FormCollection collection)
        {
            int? movieId = null;
            if (int.TryParse(collection[AddBestChoiceMovieViewModel.MovieCollectionKey], out var dt))
            {
                movieId = dt;
            }
            if (ModelState.IsValid && Request.Files.Count > 0 && movieId.HasValue)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Helper.GetRandomCharacters() + Path.GetFileName(file.FileName);

                    var path = Path.Combine(
                        Server.MapPath(FolderPaths.MainMoviesUploadPath), fileName);
                    _mainMovieService.AddMainMovie(viewModel.SliderType, viewModel.SliderTitle, viewModel.SliderContent,
                        path, movieId.Value);
                    file.SaveAs(Path.Combine(
                        Server.MapPath(FolderPaths.MainMoviesUploadPath), fileName));
                    TempData[UserMessages.UserMessage] = new MessageViewModel()
                    {
                        CssClass = "alert-success",
                        Message = Resources.MainMovieAddedSuccesfully
                    };

                    return RedirectToAction("ViewBestChoiceMovies");
                }

            }
            return View(viewModel);
        }

        [HttpGet]
        public ViewResult ViewBestChoiceMovies(int? page)
        {
            var hotMovies = _mainMovieDataProvider.GetAllMainMovies();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(new ViewBestChoiceMoviesViewModel()
            {
                MainMovies = hotMovies.ToPagedList(pageNumber, pageSize)
            }
            );
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBestChoiceMovie(int id)
        {
            _mainMovieService.DeleteMainMovie(id);

            TempData[UserMessages.UserMessage] = new MessageViewModel()
            {
                CssClass = "alert-success",
                Message = Resources.MainMovieRemovedSuccesfully
            };
            return RedirectToAction("ViewBestChoiceMovies");
        }

        [HttpPost]
        public ActionResult DeleteComment(int movieId, int commentId)
        {
            _movieService.RemoveComment(commentId);
            return RedirectToAction("Details", "Movie", new { id = movieId });
        }

        [HttpGet]
        public ActionResult DirectorEdit(int id)
        {
            var directorDto = _directorDataProvider.GetDirectorWithPhotosById(id);

            var viewModel = FilmViewerMapper.Mapper.Map<DirectorEditViewModel>(directorDto);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirectorEdit(DirectorEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var directorDto = FilmViewerMapper.Mapper.Map<DirectorDetailsDto>(viewModel);

                _directorService.EditDirector(directorDto);

                TempData[UserMessages.UserMessage] = new MessageViewModel()
                {
                    CssClass = "alert-success",
                    Message = Resources.DirectorEditedSuccesfully
                };

            }
            return RedirectToAction("ViewDirectors", "Moderator");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DirectorDelete(int id)
        {
            _directorService.DeleteDirector(id);
            TempData[UserMessages.UserMessage] = new MessageViewModel()
            {
                CssClass = "alert-success",
                Message = Resources.DirectorRemovedSuccesfully
            };
            return RedirectToAction("ViewDirectors", "Moderator");
        }

        [HttpPost]
        public bool AddMorePhotoToDirector(int directorId)
        {
            var directorDto = _directorDataProvider.GetDirectorDtoById(directorId);
            if (Request.Files.Count > 0)
            {
                var photoPaths = new List<PhotoPathDto>();
                for (var i = 0; i < Request.Files.Count; i++)
                {
                    var f = Request.Files[i];

                    if (f.ContentLength > 0)
                    {
                        var fileName = Helper.GetRandomCharacters() + directorDto.Name + Path.GetFileName(f.FileName);
                        photoPaths.Add(new PhotoPathDto()
                        {
                            Path = Path.Combine(
                                Server.MapPath(directorDto.Folder), fileName)
                        });
                        f.SaveAs(Path.Combine(
                            Server.MapPath(directorDto.Folder), fileName));
                    }
                }
                _directorService.AddPhotosToDirector(directorDto, photoPaths);

                return true;
            }
            return false;
        }

        [HttpPost]
        public bool RemoveRemoveDirectorPhotoPhoto(string fileName, int directorId)
        {
            _directorService.DeletePhotoFromDirector(fileName, directorId);

            return true;
        }
    }
}