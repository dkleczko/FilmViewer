using FilmViewer.Business.Dto.Extended.Director;
using PagedList;

namespace FilmViewer.Models.Director
{
    public class DirectorListViewModel
    {
        public IPagedList<DirectorCurrentUserVoteDto> DirectorsList { get; set; }
    }
}