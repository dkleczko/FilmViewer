using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmViewer.DAL.Model;
using FilmViewer.Models;
namespace FilmViewer.MetadataHelper
{
    public class MetadataModel
    {
        public MetadataModel()
        {
            Categories = new List<string>();
            Actors = new List<string>();
            YearProduction = new List<string>();
            Directors = new List<string>();
            Metadata = new List<string>();
        }
        public List<string> Categories { get; set; }
        public List<string> Actors {get;set;}
        public List<string> YearProduction {get;set;}
        public List<string> Directors {get;set;}
        public List<string> Metadata {get;set;}

        public void CreateModel(List<string> metaModel, Movie movie)
        {
            Categories = movie.Categories.Select(p => p.Name).ToList();
            Actors = movie.Actors.Select(p => p.Name).ToList();
            if(movie.PremiereDate != null)
            {
                YearProduction.Add(movie.PremiereDate.Value.Year.ToString());
            }
            if(movie.Director != null)
            {
                Directors.Add(movie.Director.Name);

            }
            Metadata = metaModel;
        }
    }
}