using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.Helpers;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Helpers
{
    public class MetadataHelper :IMetadataHelper
    {
        public IEnumerable<string> GenerateMetadataList(IEnumerable<Metadata> metadatas, Movie movie)
        {
            var listOfStringMetadatas = new List<string>();
            if (metadatas != null)
            {
                listOfStringMetadatas.AddRange(metadatas.Select(p => p.Name));
            }
            if (movie != null)
            {
                if (movie.Categories != null)
                {
                    listOfStringMetadatas.AddRange(movie.Categories.Select(p => p.Name));
                }

                if (movie.Actors != null)
                {
                    listOfStringMetadatas.AddRange(movie.Actors.Select(p => p.Name));
                }

                if (movie.Director != null)
                {
                    listOfStringMetadatas.Add(movie.Director.Name);
                }
                if (movie.PremiereDate.HasValue)
                {
                    listOfStringMetadatas.Add(movie.PremiereDate.Value.Year.ToString());
                }
            }

            return listOfStringMetadatas.Distinct();
        }
    }
}
