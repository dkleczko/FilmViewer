using System.Collections.Generic;
using System.Linq;
using FilmViewer.Business.Abstract.DataProviders;
using FilmViewer.Business.Dto.Domain;
using FilmViewer.Business.Dto.Extended.Director;
using FilmViewer.Business.Enums;
using FilmViewer.Business.Mappings;
using FilmViewer.DAL.Abstract.Uow;
using FilmViewer.DAL.Model;

namespace FilmViewer.Business.DataProviders
{
    public class DirectorDataProvider : IDirectorDataProvider
    {
        private readonly IUnitOfWork _uow;
        public DirectorDataProvider(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public List<DirectorDto> GetDirectorsBySearchString(string searchString)
        {
            var directors = _uow.DirectorRepository.GetDirectorsBySearchString(searchString);
            return BusinessMapper.Mapper.Map<List<DirectorDto>>(directors);
        }

        public DirectorDto GetDirectorDtoById(int id)
        {
            var directorEntity = _uow.DirectorRepository.Get(id);
            return BusinessMapper.Mapper.Map<DirectorDto>(directorEntity);
        }

        public List<DirectorDetailsDto> GetDirectorBySearchString(string searchString, SortParam sortParam, SortOrder sortOrder)
        {
            var directorsList = _uow.DirectorRepository.GetDirectorsBySearchString(searchString);
            switch (sortParam)
            {
                case SortParam.Name:
                    directorsList = sortOrder == SortOrder.Asc ? directorsList.OrderBy(p => p.Name) : directorsList.OrderByDescending(p => p.Name);
                    break;
                case SortParam.Date:
                    directorsList = sortOrder == SortOrder.Asc
                        ? directorsList.OrderBy(p => p.Birthdate)
                        : directorsList.OrderByDescending(p => p.Birthdate);
                    break;
            }

            return BusinessMapper.Mapper.Map<List<DirectorDetailsDto>>(directorsList);
        }

        public DirectorDetailsPhotosDto GetDirectorWithPhotosById(int directorId)
        {
            var directorEntity = _uow.DirectorRepository.GetDirectorByIdWithPhotoPaths(directorId);
            var directorDto = BusinessMapper.Mapper.Map<DirectorDetailsPhotosDto>(directorEntity);
            return directorDto;

        }

        public List<DirectorCurrentUserVoteDto> GetDirectorBySearchString(string searchString, ApplicationUser applicationUser = null)
        {
            var result = new List<DirectorCurrentUserVoteDto>();
            var directorsEntities = _uow.DirectorRepository.GetDirectorsBySearchString(searchString);
            foreach (var directorEntity in directorsEntities)
            {
                var directorDto = BusinessMapper.Mapper.Map<DirectorCurrentUserVoteDto>(directorEntity);
                if (applicationUser != null)
                {
                    var userVote = directorEntity.Votes.FirstOrDefault(p => p.ApplicationUser != null && p.ApplicationUser.Id == applicationUser.Id);
                    if (userVote != null)
                    {
                        directorDto.CurrentUserVote = userVote.VoteScore;
                        directorDto.HasUserVoteDirector = true;
                    }
                }
                result.Add(directorDto);

            }
            return result;
        }

        public DirectorPhotosMoviesDto GetDirectorDetailsPhotosAndMovies(int id, ApplicationUser applicationUser = null)
        {
            var director = _uow.DirectorRepository.GetDirectorByIdWithPhotosMoviesVoteActor(id);

            var directorDto = BusinessMapper.Mapper.Map<Director, DirectorPhotosMoviesDto>(director);

            if (applicationUser != null)
            {
                var userVote = director.Votes.FirstOrDefault(p => p.ApplicationUser != null && 
                p.ApplicationUser.Id == applicationUser.Id);
                if (userVote != null)
                {
                    directorDto.CurrentUserVote = userVote.VoteScore;
                    directorDto.HasUserVoteDirector = true;
                }
            }
            return directorDto;
        }

        public CurrentDirectorVoteDto GetCurrentDirectorVote(int directorId)
        {
            var directorEntity = _uow.DirectorRepository.Get(directorId);

            return BusinessMapper.Mapper.Map<CurrentDirectorVoteDto>(directorEntity);

        }

        public List<DirectorDto> GetTop100VotedDirectors()
        {
            var directorEntities = _uow.DirectorRepository.GetTop100VotedDirectors();

            return BusinessMapper.Mapper.Map<List<DirectorDto>>(directorEntities);
        }
    }
}
