using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.BL
{
    public class Movie
    {
        private int id;
        private string url;
        private string primaryTitle;
        private string description;
        private string primaryImage;
        private int year;
        private DateOnly releaseDate;
        private string language;
        private double budget;
        private double grossWorldwide;
        private string genres;
        private bool isAdult;
        private int runtimeMinutes;
        private decimal averageRating;
        private int numVotes;
        private int priceToRent;

        public Movie(int id, string url, string primaryTitle, string description, string primaryImage, int year, DateOnly releaseDate, string language, double budget, double grossWorldwide, string genres, bool isAdult, int runtimeMinutes, decimal averageRating, int numVotes, int priceToRent)
        {
            this.id = id;
            this.url = url;
            this.primaryTitle = primaryTitle;
            this.description = description;
            this.primaryImage = primaryImage;
            this.year = year;
            this.releaseDate = releaseDate;
            this.language = language;
            this.budget = budget;
            this.grossWorldwide = grossWorldwide;
            this.genres = genres;
            this.isAdult = isAdult;
            this.runtimeMinutes = runtimeMinutes;
            this.averageRating = averageRating;
            this.numVotes = numVotes;
            this.priceToRent = priceToRent;
        }
        public Movie()
        {
        }
        public int Id { get => id; set => id = value; }
        public string Url { get => url; set => url = value; }
        public string PrimaryTitle { get => primaryTitle; set => primaryTitle = value; }
        public string Description { get => description; set => description = value; }
        public string PrimaryImage { get => primaryImage; set => primaryImage = value; }
        public int Year { get => year; set => year = value; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ReleaseDate { get => releaseDate; set => releaseDate = value; }
        public string Language { get => language; set => language = value; }
        public double Budget { get => budget; set => budget = value; }
        public double GrossWorldwide { get => grossWorldwide; set => grossWorldwide = value; }
        public string Genres { get => genres; set => genres = value; }
        public bool IsAdult { get => isAdult; set => isAdult = value; }
        public int RuntimeMinutes { get => runtimeMinutes; set => runtimeMinutes = value; }
        public decimal AverageRating { get => averageRating; set => averageRating = value; }
        public int NumVotes { get => numVotes; set => numVotes = value; }
        public int PriceToRent { get => priceToRent; set => priceToRent=value; }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            return dbs.InsertMovie(this);
        }
        public int Delete(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteMovie(id);
        }
        public int RentedMovie(int userId, int movieId, DateOnly dateEnd)
        {
            DBservices dbs = new DBservices();
            return dbs.RentMovie(userId, movieId, dateEnd);
        }

        public Movie GetMovieById(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetMovie(id);
        }
        public List<Movie> GetMoviesForPages(int page, int pageSize)
        {
            DBservices dbs = new DBservices();
            return dbs.GetMoviesForPage(page, pageSize);
        }
        public List<Movie> GetRentedMovies(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetCurrentlyRentedMoviesByUser(id);
        }
        public List<Movie> GetMovies()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllMoviesFromDb();
        }
        public List<Movie> GetCart(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.GetCart(id);
        }
        public int InsertMovieToCart(int userId, int movieId, DateOnly rentEnd, double totalPrice)
        {
            DBservices dbs = new DBservices();
            return dbs.InsertMovieToCart(userId, movieId, rentEnd, totalPrice);
        }
        public int DeleteFromCart(int userId, int movieId)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteFromCart(userId, movieId);
        }

        public List<Movie> GetByTitle(string title)
        {
            DBservices dbs = new DBservices();
            return dbs.GetMoviesByTitle(title);
        }

        public List<Movie> GetByReleaseDate(DateOnly startDate, DateOnly endDate)
        {
            DBservices dbs = new DBservices();
            return dbs.GetMovieByDate(startDate, endDate);
        }
        public Cart getDetailRentMovie(int userId, int movieId)
        {
            DBservices dbs = new DBservices();
            return dbs.getCartDetail(userId, movieId);
        }
    }
}
