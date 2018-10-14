using System;
using System.Diagnostics;
using AutoMapper;
using FilmViewer.Business.Mappings.Domain;
using FilmViewer.Business.Mappings.Extended.Actor;
using FilmViewer.Business.Mappings.Extended.Director;
using FilmViewer.Business.Mappings.Extended.Movie;

namespace FilmViewer.Business.Mappings
{
    public static class BusinessMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg => {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                MapExtendedActor(cfg);
                MapExtendedDirector(cfg);
                MapExtendedMovie(cfg);
                MapDomainDto(cfg);
            });
            AssertConfigurationInDebug(config);
            var mapper = config.CreateMapper();
            return mapper;
        });

        internal static IMapper Mapper => Lazy.Value;

        [Conditional("DEBUG")]
        private static void AssertConfigurationInDebug(IConfigurationProvider config)
        {
            config.AssertConfigurationIsValid();
        }

        private static void MapExtendedActor(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<ActorPhotosMoviesDtoProfile>();
            cfg.AddProfile<ActorDetailsCurrentUserVoteDtoProfile>();
            cfg.AddProfile<CurrentActorVoteDtoProfile>();
            cfg.AddProfile<VotedActorsByUserDtoProfile>();
        }

        private static void MapExtendedMovie(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<Top6RatesMoviesDtoProfile>();
            cfg.AddProfile<AllMainMoviesDtoProfile>();
            cfg.AddProfile<RecommendedMovieForUserDtoProfile>();
            cfg.AddProfile<SearchMoviesDtoProfile>();
            cfg.AddProfile<MovieDetailsExtendedDtoProfile>();
            cfg.AddProfile<VotedMoviesByUserDtoProfile>();
            cfg.AddProfile<MovieWithSimilarityDtoProfile>();

        }
        private static void MapExtendedDirector(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<DirectorDetailsPhotosDtoProfile>();
            cfg.AddProfile<DirectorCurrentUserVoteDtoProfile>();
            cfg.AddProfile<DirectorPhotosMoviesDtoProfile>();
            cfg.AddProfile<CurrentDirectorVoteDtoProfile>();
            cfg.AddProfile<VotedDirectorByUserDtoProfile>();
        }
        private static void MapDomainDto(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<ActorDtoProfile>();
            cfg.AddProfile<ActorDetailsDtoProfile>();
            cfg.AddProfile<PhotoPathDtoProfile>();
            cfg.AddProfile<MovieDtoProfile>();
            cfg.AddProfile<MovieDetailsDtoProfile>();
            cfg.AddProfile<MetadataDtoProfile>();
            cfg.AddProfile<DirectorDtoProfile>();
            cfg.AddProfile<DirectorDetailsDtoProfile>();
            cfg.AddProfile<MoviePersonDetailsDtoProfile>();
            cfg.AddProfile<MoviePersonDtoProfile>();
            cfg.AddProfile<CategoryDtoProfile>();
            cfg.AddProfile<CommentDtoProfile>();
            cfg.AddProfile<ApplicationUserDtoProfile>();
            cfg.AddProfile<ApplicationUserDetailsDtoProfile>();
            cfg.AddProfile<RoleDtoProfile>();
            cfg.AddProfile<SliderTypeDtoProfile>();

        }

    }
}
