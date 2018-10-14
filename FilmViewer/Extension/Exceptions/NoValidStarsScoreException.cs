using System;

namespace FilmViewer.Extension.Exceptions
{
    public class NoValidStarsScoreException :Exception
    {
        public NoValidStarsScoreException(string msg) : base(msg)
        {
            
        }
    }
}