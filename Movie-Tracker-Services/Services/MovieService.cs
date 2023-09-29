using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_Tracker.models.Data;
using Movie_Tracker.models.Models;
using Movie_Tracker.models.ViewModels;
using Movie_Tracker_Common.GenericResponses;
using Movie_Tracker_Services.Service_Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Movie_Tracker_Services.Services
{
    public class MovieService : IMovieService
    {
        MovieTrackerContext movieContext;
        public MovieService(MovieTrackerContext _movieContext)
        {
            movieContext = _movieContext;
        }

        public JsonResult GetMovies(string? searchTerm)
        {
            List<MovieVM> movieData;
            try
            {
                var result = from movie in movieContext.Movies
                             join genre in movieContext.Genres on movie.GenreId equals genre.GenreId
                             where movie.DeletedAt == null
                             select new MovieVM()
                             {
                                 Budget = movie.Budget,
                                 Description = movie.Description,
                                 Director = movie.Director,
                                 Genre = genre.GenreName,
                                 GrossIndia = movie.GrossIndia,
                                 GrossWorld = movie.GrossWorld,
                                 Id = movie.MovieId,
                                 LeadActor = movie.LeadActor,
                                 PosterUrl = movie.Image,
                                 Title = movie.Title,
                                 Total = movie.Total,
                                 Writer = movie.Writer
                             };
                movieData = result.ToList();
                if (searchTerm != null)
                {
                    string searchTermLower = searchTerm.ToLower();
                    movieData = movieData.Where(movie => movie.Title.ToLower().Contains(searchTermLower) || movie.Genre.ToLower().Contains(searchTermLower) || movie.Description.ToLower().Contains(searchTermLower)).ToList();
                }
                return new JsonResult(new ApiResponse<List<MovieVM>>()
                {
                    Data = movieData,
                    Message = ResponseMessage.Success,
                    StatusCode = ResponseCode.Success,
                    Result = true

                });

            }
            catch
            {
                return ReturnError(ResponseMessage.Error, ResponseCode.InternalServerError);
            }

        }

        public JsonResult GetMoviesDetails(int id)
        {
            try
            {
                var result = from movie in movieContext.Movies
                             join genre in movieContext.Genres on movie.GenreId equals genre.GenreId
                             where movie.MovieId == id && movie.DeletedAt == null
                             select new MovieVM()
                             {
                                 Budget = movie.Budget,
                                 Description = movie.Description,
                                 Director = movie.Director,
                                 Genre = genre.GenreName,
                                 GrossIndia = movie.GrossIndia,
                                 GrossWorld = movie.GrossWorld,
                                 Id = movie.MovieId,
                                 LeadActor = movie.LeadActor,
                                 PosterUrl = movie.Image,
                                 Title = movie.Title,
                                 Total = movie.Total,
                                 Writer = movie.Writer,
                                 GenreId = genre.GenreId
                             };
                MovieVM movieData = result.FirstOrDefault();
                if (movieData is null)
                {
                    return ReturnError(ResponseMessage.MovieNotFound, ResponseCode.NotFound);
                }

                return new JsonResult(new ApiResponse<MovieVM>()
                {
                    Data = movieData,
                    Message = ResponseMessage.Success,
                    StatusCode = ResponseCode.Success,
                    Result = true
                });
            }

            catch
            {
                return ReturnError(ResponseMessage.Error, ResponseCode.InternalServerError);
            }

        }

        public JsonResult GetGenres()
        {
            try
            {
                List<Genre> genres = movieContext.Genres.ToList();

                return new JsonResult(new ApiResponse<List<Genre>>()
                {
                    Data = genres,
                    Message = ResponseMessage.Success,
                    StatusCode = ResponseCode.Success,
                    Result = true

                });
            }
            catch
            {
                return ReturnError(ResponseMessage.Error, ResponseCode.InternalServerError);
            }
        }

        public JsonResult AddMovie(MovieVM movie)
        {
            try
            {
                Movie movieToAdd = new Movie() { 
                Budget = movie.Budget,
                Description= movie.Description,
                Director= movie.Director,
                GenreId = (int)movie.GenreId,
                GrossIndia=movie.GrossIndia,
                GrossWorld=movie.GrossWorld,
                Image=movie.PosterUrl,
                LeadActor=movie.LeadActor,
                Title = movie.Title,
                Total=movie.Total,  
                Writer=movie.Writer,
                };
                movieContext.Movies.Add(movieToAdd);
                movieContext.SaveChanges();
                return new JsonResult(new ApiResponse<MovieVM>()
                {
                    Data = movie,
                    Message = ResponseMessage.Success,
                    StatusCode = ResponseCode.Success,
                    Result = true

                });
            }

            catch
            {
                return ReturnError(ResponseMessage.Error, ResponseCode.InternalServerError);
            }
        }

        public JsonResult UpdateMovie(MovieVM movie)
        {
            try
            {

                Movie movieToUpdate = movieContext.Movies.Where(x => x.MovieId == movie.Id && x.DeletedAt==null).FirstOrDefault();
                    if(movieToUpdate is null)
                    {
                        return ReturnError(ResponseMessage.MovieNotFound, ResponseCode.NotFound);
                    }
                movieToUpdate.UpdatedAt = DateTime.Now;
                movieToUpdate.Director = movie.Director;
                movieToUpdate.Title = movie.Title;
                movieToUpdate.Total = movie.Total;  
                movieToUpdate.Writer = movie.Writer;
               movieToUpdate.LeadActor = movie.LeadActor;
                movieToUpdate.Budget = movie.Budget;
                movieToUpdate.Description = movie.Description;
                movieToUpdate.GenreId = (int)movie.GenreId;
                movieToUpdate.GrossIndia = movie.GrossIndia;
                movieToUpdate.GrossWorld = movie.GrossWorld;
                movieToUpdate.Image = movie.PosterUrl;
                movieContext.Update(movieToUpdate);
                movieContext.SaveChanges();

                return new JsonResult(new ApiResponse<MovieVM>()
                {
                    Data = movie,
                    Message = ResponseMessage.Success,
                    StatusCode = ResponseCode.Success,
                    Result = true
                });
            }

            catch
            {
                return ReturnError(ResponseMessage.Error, ResponseCode.InternalServerError);
            }
        }

        public JsonResult DeleteMovie(int id)
        {
            try
            {
                Movie movie = movieContext.Movies.Where(x => x.MovieId == id && x.DeletedAt == null).FirstOrDefault();
                if(movie is null)
                {
                    return ReturnError(ResponseMessage.MovieNotFound, ResponseCode.NotFound);
                }
                movie.DeletedAt=DateTime.Now;
                movieContext.Update(movie);
                movieContext.SaveChanges();
                return new JsonResult(new ApiResponse<int>()
                {
                    Data = id,
                    Message = ResponseMessage.Success,
                    StatusCode = ResponseCode.Success,
                    Result = true
                });
            }

            catch
            {
                return ReturnError(ResponseMessage.Error, ResponseCode.InternalServerError);
            }
        }

        public JsonResult ReturnError(string message, int code)
        {
            return new JsonResult(new ApiResponse<string>()
            {
                Data = null,
                Message = message,
                StatusCode = code,
                Result = false

            });
        }

    }
}
