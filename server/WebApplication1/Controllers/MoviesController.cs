using Microsoft.AspNetCore.Mvc;
using WebApplication1.BL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //GET: api/<MoviesController>
        [HttpGet("allMovies")]
        public IEnumerable<Movie> Get()
        {
            Movie movies = new Movie();
            return movies.GetMovies();
        }
        [HttpGet("getMovieById")]
        public Movie GetMovieId([FromQuery] int id)
        {
            Movie movie = new Movie();
            return movie.GetMovieById(id);
        }
        [HttpGet("getByTitle")]
        public IEnumerable<Movie> GetMovieByTitle([FromQuery] string title)
        {
            Movie movies = new Movie();
            return movies.GetByTitle(title);
        }

        [HttpGet("searchByDate/from/{startDate}/until/{endDate}")]
        public IEnumerable<Movie> GetByDate([FromRoute] DateOnly startDate, [FromRoute] DateOnly endDate)
        {
            Movie movies = new Movie();
            return movies.GetByReleaseDate(startDate, endDate);
        }
        
        [HttpGet("GetRentedMovies")]
        public IEnumerable<Movie> GetRentedMovies([FromQuery] int id)
        {
            Movie movies = new Movie();
            return movies.GetRentedMovies(id);
        }
        [HttpGet("getCart")]
        public IEnumerable<Movie> GetCart([FromQuery] int userId)
        {
            Movie movies = new Movie();
            return movies.GetCart(userId);
        }
        [HttpGet("moviesForPage/page/{page}")]
        public IEnumerable<Movie> GetMoviesForPages([FromQuery] int page, int pageSize = 10)
        {
            Movie movies = new Movie();
            return movies.GetMoviesForPages(page, pageSize);
        }
        [HttpGet("getDetailRent/userId/{userId:int}/movieId/{movieId:int}")]
        public Cart getDetailRentMovie([FromRoute] int userId, [FromRoute] int movieId)
        {
            Movie movies = new Movie();
            return movies.getDetailRentMovie(userId, movieId);
        }
        // POST api/<MoviessController>
        [HttpPost]
        public int Post([FromBody] Movie movie)
        {
            int numEffected = movie.Insert();
            return numEffected;
        }

        [HttpPost("insertMovieToCart")] 
        public int InsertMovieToCart([FromBody] Cart request)
        {
            Movie movie = new Movie();
            return movie.InsertMovieToCart(request.UserId, request.MovieId, request.RentEnd, request.TotalPrice);
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            Movie movie = new Movie();
            return movie.Delete(id);
        }

        // DELETE api/<MoviesController>/5
        [HttpDelete("deleteFromCart/userId/{userId:int}/movieId/{movieId:int}")]
        public int DeleteFromCart([FromRoute] int userId, [FromRoute] int movieId)
        {
            Movie movie = new Movie();
            return movie.DeleteFromCart(userId, movieId);
        }
    }
}