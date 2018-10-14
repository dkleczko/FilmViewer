using System;
using System.Linq;

namespace FilmViewer.AppHelpers
{
    public static class Helper
    {
        
        public static string GetRandomCharacters()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 15)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if(!String.IsNullOrEmpty(source))
                return source.IndexOf(toCheck, comp) >= 0;
            return false;
        }
 
    }
}