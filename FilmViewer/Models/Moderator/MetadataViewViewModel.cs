using FilmViewer.Business.Dto.Domain;
using PagedList;

namespace FilmViewer.Models.Moderator
{
    public class MetadataViewViewModel
    {
        public IPagedList<MetadataDto> MetadatasList { get; set; }
    }
}