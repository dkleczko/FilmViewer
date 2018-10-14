using FilmViewer.Business.Dto.Extended.Director;
using PagedList;

namespace FilmViewer.Models.User
{
    public class VotedDirectorsViewModel
    {
        public IPagedList<VotedDirectorByUserDto> VotedDirectors { get; set; }
    }
}