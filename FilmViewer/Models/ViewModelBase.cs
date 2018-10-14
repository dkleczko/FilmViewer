using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FilmViewer.DAL.Model;

namespace FilmViewer.Models
{
    public class ViewModelBase
    {
        public List<Category> categories { get; set; }
    }
}