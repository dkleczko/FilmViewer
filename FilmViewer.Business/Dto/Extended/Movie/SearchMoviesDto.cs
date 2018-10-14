using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Movie
{
    public class SearchMoviesDto : MovieDetailsDto
    {
        public int CommentsCount { get; set; }
        public int VideosCount { get; set; }
        public int PhotosCount { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public string DirectorName { get; set; }
        public int DirectorId { get; set; }
        public bool IsUserFavourite { get; set; }
        public int CurrentUserVote { get; set; }
        public bool HasUserVoteActor { get; set; }
    }
}
