using System;
using System.IO;

namespace FilmViewer.AppHelpers
{
    public static class FileHelpers
    {
        public static string GenerateFilename(string movieTitle, string fileName)
        {
            return string.Format("{0}-{1}-{2}", movieTitle, Guid.NewGuid(), fileName);
        }

        public static string GetPhotoPath(string folder, string photoPath, string defaultPath = "")
        {
            if (!string.IsNullOrEmpty(folder) && !string.IsNullOrEmpty(photoPath))
            {
                return Path.Combine(folder, Path.GetFileName(photoPath));
            }
            return defaultPath;
        }

        public static string GetPhotoName(string photoPath)
        {
            return Path.GetFileName(photoPath);
        }
    }
}