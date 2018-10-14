using System.Collections.Generic;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Director;

namespace FilmViewer.Business.Abstract.Services
{
    public interface IDirectorService
    {
        int AddDirector(DirectorDetailsDto directorDto, string serverPath, string virtualPath);
        void AddPhotosToDirector(DirectorDto directorDto, List<PhotoPathDto> photosList);
        void DeleteDirector(int directorId);
        void DeletePhotoFromDirector(string fileName, int directorId);
        void EditDirector(DirectorDetailsDto directorDto);
        bool VoteDirector(VoteDirectorDto voteDirectorDto);
    }
}
