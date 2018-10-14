using System;
using System.Globalization;
using FilmViewer.Business.Dto.Domain;

namespace FilmViewer.Business.Dto.Extended.Movie
{
    public class Top6RatesMoviesDto : MovieDto
    {
        public int Duration { get; set; }
        public int CommentsCount { get; set; }

        public string AverageScore
        {
            get
            {
                if (VoteCount > 0)
                {
                    return Math.Round(VoteScores / (decimal) VoteCount, 1)
                        .ToString(CultureInfo.InvariantCulture);
                }

                return "0.0";
            }
        }
    }
}
