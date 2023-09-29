using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Tracker.models.ViewModels;
using Movie_Tracker_Common.GenericResponses;
using Movie_Tracker_Services.Service_Interfaces;

namespace Movie_Tracker_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieService movieService;
        public MovieController(IMovieService _movieService)
        {
            movieService = _movieService;
        }

        [HttpGet("GetMovies")]
        public JsonResult GetMovies(string? searchTerm)
        {
            return movieService.GetMovies(searchTerm);
        }

        [HttpGet("GetMovieDetails")]
        public JsonResult GetMoviesDetails(int id)
        {
            return movieService.GetMoviesDetails(id);
        }

        [HttpGet("GetGenres")]
        public JsonResult GetGenres()
        {
            return movieService.GetGenres();
        }

        [Authorize]
        [HttpPost("AddMovie")]
        public JsonResult AddMovie(MovieVM movie)
        {
            return movieService.AddMovie(movie);
        }

        [Authorize]
        [HttpDelete("DeleteMovie")]
        public JsonResult DeleteMovie(int id)
        {
            return movieService.DeleteMovie(id);
        }

        [Authorize]
        [HttpPut("UpdateMovie")]
        public JsonResult UpdateMovie(MovieVM movie)
        {
            return movieService.UpdateMovie(movie);
        }
    }
}
