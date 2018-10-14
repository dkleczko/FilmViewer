using System;
using System.Diagnostics;
using AutoMapper;
using FilmViewer.Mappings.ViewModels;

namespace FilmViewer.Mappings
{
    public static class FilmViewerMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg => {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                MapViewModels(cfg);
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

        private static void MapViewModels(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<CreateFilmViewModelProfile>();
            cfg.AddProfile<CreateActorViewModelProfile>();
            cfg.AddProfile<CreateMetadataViewModelProfile>();
            cfg.AddProfile<ActorEditViewModelProfile>();
            cfg.AddProfile<EditCategoryViewModelProfile>();
            cfg.AddProfile<MovieEditViewModelProfile>();
            cfg.AddProfile<CreateDirectorViewModelProfile>();
            cfg.AddProfile<DirectorEditViewModelProfile>();
        } 
    }
}