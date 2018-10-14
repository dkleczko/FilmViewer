using System.Collections.Generic;
using System.Linq;

namespace FilmViewer.AppHelpers
{
    public static class ManagerHelper
    {
        public static List<int> CategoryCommaCollectionRefactor(string collection)
        {
            List<int> results = new List<int>();

            if(!string.IsNullOrEmpty(collection))
            {
                results = collection.Split(',').Select(int.Parse).ToList();

            }
            return results;
        }

    }
}