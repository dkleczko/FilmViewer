namespace FilmViewer.Business.Abstract.Factory
{
    public interface IDirectoryFactory
    {
        string CreateDirectoryForMovie(string path, string movieTitle, string virtualPath);

    }
}
