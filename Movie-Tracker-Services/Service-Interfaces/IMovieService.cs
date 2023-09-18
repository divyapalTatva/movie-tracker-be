using Microsoft.AspNetCore.Mvc;
using Movie_Tracker_Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Movie_Tracker_Services.Service_Interfaces
{
    public interface IMovieService
    {
        public JsonResult GetMovies(string? searchTerm);

        public JsonResult GetMoviesDetails(int id);

        public JsonResult AddMovie(MovieVM movie);

        public JsonResult UpdateMovie(MovieVM movie);

        public JsonResult DeleteMovie(int id);
    }
}
