using System;

namespace FilmViewer.Business.Dto.Domain
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int MovieId { get; set; }
    }
}
