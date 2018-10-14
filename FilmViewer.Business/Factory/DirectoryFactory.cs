using System;
using System.IO;
using FilmViewer.Business.Abstract.Factory;

namespace FilmViewer.Business.Factory
{
    public class DirectoryFactory : IDirectoryFactory
    {
        public string CreateDirectoryForMovie(string path, string movieTitle, string virtualPath)
        {
            var guid = Guid.NewGuid();
            var fullPath = Path.Combine(path, string.Format("{0}-{1}", movieTitle, guid));
            var dirInfo =
                Directory.CreateDirectory(fullPath);
            return Path.Combine(virtualPath, string.Format("{0}-{1}", movieTitle, guid));
        }
    }
}
