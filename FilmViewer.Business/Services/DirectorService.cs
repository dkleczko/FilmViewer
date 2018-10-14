using System.Collections.Generic;
using System.IO;
using System.Linq;
using FilmViewer.Business.Abstract.Factory;
using FilmViewer.Business.Abstract.Services;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Director;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.Services
{
    public class DirectorService : IDirectorService
    {
        private readonly IUnitOfWork _uow;
        private readonly IDirectoryFactory _directoryFactory;
        public DirectorService(IUnitOfWork uow, IDirectoryFactory directoryFactory)
        {
            _uow = uow;
            _directoryFactory = directoryFactory;
        }

        public int AddDirector(DirectorDetailsDto directorDto, string serverPath, string virtualPath)
        {
            var director = BusinessMapper.Mapper.Map<Director>(directorDto);
            director.Folder = _directoryFactory.CreateDirectoryForMovie(serverPath, director.Name, virtualPath);
            _uow.DirectorRepository.Add(director);
            _uow.Complete();

            return director.Id;
        }

        public void AddPhotosToDirector(DirectorDto directorDto, List<PhotoPathDto> photosList)
        {
            var directorEntity = _uow.DirectorRepository.GetDirectorByIdWithPhotoPaths(directorDto.Id);
            if (directorEntity != null)
            {
                directorEntity.PhotoUrl = directorDto.PhotoUrl;
                if (directorEntity.PhotoUrls == null)
                {
                    directorEntity.PhotoUrls = new List<PhotoPath>();
                }
                var photoPathList = BusinessMapper.Mapper.Map<List<PhotoPath>>(photosList);
                photoPathList.ForEach(p => directorEntity.PhotoUrls.Add(p));

                _uow.Complete();
            }
        }

        public void DeleteDirector(int directorId)
        {
            var directorEntity = _uow.DirectorRepository.Get(directorId);
            var directorVotes = _uow.VoteMoviePersonRepository.GetAllVotesOfMoviePerson(directorId);

            _uow.DirectorRepository.Remove(directorEntity);
            _uow.VoteMoviePersonRepository.RemoveRange(directorVotes);

            _uow.Complete();

        }

        public void DeletePhotoFromDirector(string fileName, int directorId)
        {
            var directorEntity = _uow.DirectorRepository.GetDirectorByIdWithPhotoPaths(directorId);

            if (directorEntity != null)
            {
                var photoToRemove = directorEntity.PhotoUrls.Where(p => Path.GetFileName(p.Path) == fileName);

                _uow.PhotoPathRepository.RemoveRange(photoToRemove);

                _uow.Complete();
            }

        }

        public void EditDirector(DirectorDetailsDto directorDto)
        {
            var directorEntity = _uow.DirectorRepository.Get(directorDto.Id);

            directorEntity.Biography = directorDto.Biography;
            directorEntity.Name = directorDto.Name;
            directorEntity.BirthPlace = directorDto.BirthPlace;
            directorEntity.Birthdate = directorDto.Birthdate;
            directorEntity.MaritalStatus = directorDto.MaritalStatus;

            _uow.Complete();
        }

        public bool VoteDirector(VoteDirectorDto voteDirectorDto)
        {
            var director = _uow.DirectorRepository.Get(voteDirectorDto.DirectorId);
            var user = _uow.UserRepository.GetApplicationUserWithVoteMoviePerson(voteDirectorDto.UserId);
            var earlierUserVotes = user.VoteMoviePerson.Where(p => p.MoviePerson.Id == director.Id).ToList();

            foreach (var vote in earlierUserVotes)
            {
                director.VoteScores -= vote.VoteScore;
                director.VoteCount--;
                user.VoteMoviePerson.Remove(vote);
            }
            director.VoteScores += voteDirectorDto.Score;
            director.VoteCount++;
            user.VoteMoviePerson.Add(new VoteMoviePerson()
            {
                ApplicationUser = user,
                MoviePerson = director,
                VoteScore = voteDirectorDto.Score
            });

            var result = _uow.Complete();
            return result > 0;
        }
    }
}
