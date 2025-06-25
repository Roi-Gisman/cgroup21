using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using WebApplication1.BL;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using System.Text.Json;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the appsettings.json 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a user into the users table 
    //--------------------------------------------------------------------------------------------------
    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Name", user.Name);
        paramDic.Add("@Email", user.Email);
        paramDic.Add("@Password", user.Password);
        
        cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertUser_2025", con, paramDic);         // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------
    // This method delete a user into the users table 
    //--------------------------------------------------------------------------------------------------
    public int DeleteUser(int id)
    {

        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Id", id);
        cmd = CreateCommandWithStoredProcedureGeneral("SP_DeleteUser_2025", con, paramDic);         // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------
    // This method update a user into the users table 
    //--------------------------------------------------------------------------------------------------
    public int UpdateUser(int id,User user)
    {

        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@userId", id);
        paramDic.Add("@Name", user.Name);
        paramDic.Add("@email", user.Email);
        paramDic.Add("@password", user.Password);
        paramDic.Add("@Active", user.Active);

        cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateUser_2025", con, paramDic);         // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------
    // This method get user by email and password from the users table 
    //--------------------------------------------------------------------------------------------------
    public User GetUserByEmailPassword(string email,string password)
    {
        SqlConnection con;
        User user = new User();

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetByEmailAndPassword_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Email", email);
        cmd.Parameters.AddWithValue("@Password", password);

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Id = (int)reader["Id"];
                user.Name = reader["Name"].ToString();
                user.Email = reader["email"].ToString();
                user.Password = reader["password"].ToString();
                user.Active = Convert.ToBoolean(reader["Active"]);
            }
            return user;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    ////---------------------------------------------------------------------------------
    //// get movies by date
    ////---------------------------------------------------------------------------------
    public List<User> GetUsers()
    {
        List<User> users = new List<User>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetAllUsers_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;


        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                User u = new User();
                u.Id = (int)reader["Id"];
                u.Name = reader["Name"].ToString();
                u.Email = reader["Email"].ToString();
                u.Password = reader["Password"].ToString();
                u.Active = Convert.ToBoolean(reader["Active"]);
                users.Add(u);
            }
            return users;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a user into the users table 
    //--------------------------------------------------------------------------------------------------
    private static Random random = new Random();
    public int InsertMovie(Movie movie)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
            if (movie.PriceToRent == 0)
            { 
                movie.PriceToRent = random.Next(10, 31);
            }
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Url", movie.Url);
        paramDic.Add("@PrimaryTitle", movie.PrimaryTitle);
        paramDic.Add("@Description", movie.Description);
        paramDic.Add("@PrimaryImage", movie.PrimaryImage);
        paramDic.Add("@Year", movie.Year);
        paramDic.Add("@ReleaseDate", movie.ReleaseDate.ToDateTime(TimeOnly.MinValue));
        paramDic.Add("@Language", movie.Language);
        paramDic.Add("@Budget", movie.Budget);
        paramDic.Add("@GrossWorldwide", movie.GrossWorldwide);
        paramDic.Add("@Genres", movie.Genres);
        paramDic.Add("@IsAdult", movie.IsAdult);
        paramDic.Add("@RuntimeMinutes", movie.RuntimeMinutes);
        paramDic.Add("@AverageRating", movie.AverageRating);
        paramDic.Add("@NumVotes", movie.NumVotes);
        paramDic.Add("@PriceToRent", movie.PriceToRent);

        cmd = CreateCommandWithStoredProcedureGeneral("SP_InsertMovie_2025", con, paramDic);         // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method delete a movie from the movies table 
    //--------------------------------------------------------------------------------------------------
    public int DeleteMovie(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@movieId",id);
        cmd = CreateCommandWithStoredProcedureGeneral("SP_DeleteMovie_2025", con, paramDic);         // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    //--------------------------------------------------------------------------------------------------
    // This method update a movie into the movies table 
    //--------------------------------------------------------------------------------------------------
    public int UpdateMovie(Movie movie)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@Url", movie.Url);
        paramDic.Add("@PrimaryTitle", movie.PrimaryTitle);
        paramDic.Add("@Description", movie.Description);
        paramDic.Add("@PrimaryImage", movie.PrimaryImage);
        paramDic.Add("@Year", movie.Year);
        paramDic.Add("@ReleaseDate", movie.ReleaseDate.ToDateTime(TimeOnly.MinValue));
        paramDic.Add("@Language", movie.Language);
        paramDic.Add("@Budget", movie.Budget);
        paramDic.Add("@GrossWorldwide", movie.GrossWorldwide);
        paramDic.Add("@Genres", movie.Genres);
        paramDic.Add("@IsAdult", movie.IsAdult);
        paramDic.Add("@RuntimeMinutes", movie.RuntimeMinutes);
        paramDic.Add("@AverageRating", movie.AverageRating);
        paramDic.Add("@NumVotes", movie.NumVotes);
        paramDic.Add("@PriceToRent", movie.PriceToRent);



        cmd = CreateCommandWithStoredProcedureGeneral("SP_UpdateMovie_2025", con, paramDic);         // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //---------------------------------------------------------------------------------
    // get rented movie by movie id
    //---------------------------------------------------------------------------------
    public Movie GetMovie(int movieId)
    {
        Movie m = new Movie();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetMovieById_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@MovieId", movieId);

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                m.Id = (int)reader["Id"];
                m.Url = reader["url"].ToString();
                m.PrimaryTitle = reader["primaryTitle"].ToString();
                m.Description = reader["description"].ToString();
                m.PrimaryImage = reader["primaryImage"].ToString();
                m.Year = (int)reader["year"];
                m.ReleaseDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["releaseDate"]));
                m.Language = reader["language"].ToString();
                m.Budget = Convert.ToDouble(reader["budget"]);
                m.GrossWorldwide = Convert.ToDouble(reader["grossWorldwide"]);
                m.Genres = reader["genres"].ToString();
                m.IsAdult = Convert.ToBoolean(reader["isAdult"]);
                m.RuntimeMinutes = Convert.ToInt32(reader["runtimeMinutes"]);
                m.AverageRating = Convert.ToDecimal(reader["averageRating"]); m.NumVotes = Convert.ToInt32(reader["numVotes"]);
                m.PriceToRent= Convert.ToInt32(reader["priceToRent"]);
            }
            return m;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //---------------------------------------------------------------------------------
    // get all movies 
    //---------------------------------------------------------------------------------
    public List<Movie> GetAllMoviesFromDb()
    {
        List<Movie> movies = new List<Movie>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetAllMovies_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie();
                m.Id = (int)reader["Id"];
                m.Url = reader["url"].ToString();
                m.PrimaryTitle = reader["primaryTitle"].ToString();
                m.Description = reader["description"].ToString();
                m.PrimaryImage = reader["primaryImage"].ToString();
                m.Year = (int)reader["year"];
                m.ReleaseDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["releaseDate"]));
                m.Language = reader["language"].ToString();
                m.Budget = Convert.ToDouble(reader["budget"]);
                m.GrossWorldwide = Convert.ToDouble(reader["grossWorldwide"]);
                m.Genres = reader["genres"].ToString();
                m.IsAdult = Convert.ToBoolean(reader["isAdult"]);
                m.RuntimeMinutes = Convert.ToInt32(reader["runtimeMinutes"]);
                m.AverageRating = Convert.ToDecimal(reader["averageRating"]);
                m.NumVotes = Convert.ToInt32(reader["numVotes"]);
                m.PriceToRent = Convert.ToInt32(reader["priceToRent"]);

                movies.Add(m);
            }
            return movies;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //---------------------------------------------------------------------------------
    // get rented movie by user id
    //---------------------------------------------------------------------------------
    public List<Movie> GetCurrentlyRentedMoviesByUser(int userId)
    {
        List<Movie> movies = new List<Movie>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetCurrentRentedMoviesByUser_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@userId", userId);

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie();
                m.Id = (int)reader["Id"];
                m.Url = reader["url"].ToString();
                m.PrimaryTitle = reader["primaryTitle"].ToString();
                m.Description = reader["description"].ToString();
                m.PrimaryImage = reader["primaryImage"].ToString();
                m.Year = (int)reader["year"];
                m.ReleaseDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["releaseDate"]));
                m.Language = reader["language"].ToString();
                m.Budget = Convert.ToDouble(reader["budget"]);
                m.GrossWorldwide = Convert.ToDouble(reader["grossWorldwide"]);
                m.Genres = reader["genres"].ToString();
                m.IsAdult = Convert.ToBoolean(reader["isAdult"]);
                m.RuntimeMinutes = Convert.ToInt32(reader["runtimeMinutes"]);
                m.AverageRating = Convert.ToDecimal(reader["averageRating"]);
                m.NumVotes = Convert.ToInt32(reader["numVotes"]);
                m.PriceToRent = Convert.ToInt32(reader["priceToRent"]);


                movies.Add(m);
            }
            return movies;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    //--------------------------------------------------------------------------------------------------
    // This method delete a movie from the cart 
    //--------------------------------------------------------------------------------------------------
    public int DeleteFromCart(int userId,int movieId)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserId", userId);
        paramDic.Add("@MovieId", movieId);
        cmd = CreateCommandWithStoredProcedureGeneral("SP_RemoveFromCart", con, paramDic);         // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    //--------------------------------------------------------------------------------------------------
    // This method insert movie to the cart  
    //--------------------------------------------------------------------------------------------------
    public int InsertMovieToCart(int userId, int movieId,DateOnly rentEnd , double totalPrice)
    {

        SqlConnection con;
        SqlCommand cmd;
        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@UserId", userId);
        paramDic.Add("@MovieID", movieId);
        paramDic.Add("@RentEnd", rentEnd.ToDateTime(TimeOnly.MinValue));
        paramDic.Add("@TotalPrice", totalPrice);
        cmd = CreateCommandWithStoredProcedureGeneral("SP_AddToCart", con, paramDic);         // create the command
        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    ////---------------------------------------------------------------------------------
    //// get user's cart
    ////---------------------------------------------------------------------------------
    public List<Movie> GetCart(int userId)
    {
        List<Movie> movies = new List<Movie>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetCurrentRentedMoviesByUser_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", userId);

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie();
                m.Id = (int)reader["Id"];
                m.Url = reader["url"].ToString();
                m.PrimaryTitle = reader["primaryTitle"].ToString();
                m.Description = reader["description"].ToString();
                m.PrimaryImage = reader["primaryImage"].ToString();
                m.Year = (int)reader["year"];
                m.ReleaseDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["releaseDate"]));
                m.Language = reader["language"].ToString();
                m.Budget = Convert.ToDouble(reader["budget"]);
                m.GrossWorldwide = Convert.ToDouble(reader["grossWorldwide"]);
                m.Genres = reader["genres"].ToString();
                m.IsAdult = Convert.ToBoolean(reader["isAdult"]);
                m.RuntimeMinutes = Convert.ToInt32(reader["runtimeMinutes"]);
                m.AverageRating = Convert.ToDecimal(reader["averageRating"]);
                m.NumVotes = Convert.ToInt32(reader["numVotes"]);
                m.PriceToRent = Convert.ToInt32(reader["priceToRent"]);

                bool isDeleted = false; 
                if (!reader.IsDBNull(reader.GetOrdinal("deletedAt")))
                {
                    isDeleted = true;
                }

                var rentalCount = reader.IsDBNull(reader.GetOrdinal("rentalCount")) ? 0 : reader.GetInt32(reader.GetOrdinal("rentalCount"));
                if (!isDeleted) 
                {
                    movies.Add(m);
                }

            }
            return movies;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    //--------------------------------------------------------------------------------------------------
    // This method rent movie 
    //--------------------------------------------------------------------------------------------------
    public int RentMovie(int userId,int movieId,DateOnly dateEnd)
    {
        DateOnly dateStart= DateOnly.FromDateTime(DateTime.Now);
        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@userId", userId);
        paramDic.Add("@movieId", movieId);
        paramDic.Add("@rentStart", dateStart);
        paramDic.Add("@rentEnd", dateEnd);
        cmd = CreateCommandWithStoredProcedureGeneral("SP_AddRentedMovie_2025", con, paramDic);         // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }
    ////---------------------------------------------------------------------------------
    //// get 10 movies
    ////---------------------------------------------------------------------------------
    public List<Movie> GetMoviesForPage(int page, int pageSize)
    {
        List<Movie> movies = new List<Movie>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetMoviesPaginated_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Offset", (page - 1) * pageSize);
        cmd.Parameters.AddWithValue("@PageSize", pageSize);

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie();
                m.Id = (int)reader["Id"];
                m.Url = reader["url"].ToString();
                m.PrimaryTitle = reader["primaryTitle"].ToString();
                m.Description = reader["description"].ToString();
                m.PrimaryImage = reader["primaryImage"].ToString();
                m.Year = (int)reader["year"];
                m.ReleaseDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["releaseDate"]));
                m.Language = reader["language"].ToString();
                m.Budget = Convert.ToDouble(reader["budget"]);
                m.GrossWorldwide = Convert.ToDouble(reader["grossWorldwide"]);
                m.Genres = reader["genres"].ToString();
                m.IsAdult = Convert.ToBoolean(reader["isAdult"]);
                m.RuntimeMinutes = Convert.ToInt32(reader["runtimeMinutes"]);
                m.AverageRating = Convert.ToDecimal(reader["averageRating"]);
                m.NumVotes = Convert.ToInt32(reader["numVotes"]);
                m.PriceToRent = Convert.ToInt32(reader["priceToRent"]);


                movies.Add(m);
            }
            return movies;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    ////---------------------------------------------------------------------------------
    //// get movies by title
    ////---------------------------------------------------------------------------------
    public List<Movie> GetMoviesByTitle(string title)
    {
        List<Movie> movies = new List<Movie>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetMovieByTitle_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PrimaryTitle", title);

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie();
                m.Id = (int)reader["Id"];
                m.Url = reader["url"].ToString();
                m.PrimaryTitle = reader["primaryTitle"].ToString();
                m.Description = reader["description"].ToString();
                m.PrimaryImage = reader["primaryImage"].ToString();
                m.Year = (int)reader["year"];
                m.ReleaseDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["releaseDate"]));
                m.Language = reader["language"].ToString();
                m.Budget = Convert.ToDouble(reader["budget"]);
                m.GrossWorldwide = Convert.ToDouble(reader["grossWorldwide"]);
                m.Genres = reader["genres"].ToString();
                m.IsAdult = Convert.ToBoolean(reader["isAdult"]);
                m.RuntimeMinutes = Convert.ToInt32(reader["runtimeMinutes"]);
                m.AverageRating = Convert.ToDecimal(reader["averageRating"]);
                m.NumVotes = Convert.ToInt32(reader["numVotes"]);
                m.PriceToRent = Convert.ToInt32(reader["priceToRent"]);


                movies.Add(m);
            }
            return movies;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    ////---------------------------------------------------------------------------------
    //// get user by id
    ////---------------------------------------------------------------------------------
    public User GetUserId(int id)
    {
        User user = new User();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetUserById_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Id", id);
        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                user.Id = (int)reader["Id"];
                user.Name = reader["Name"].ToString();
                user.Email = reader["email"].ToString();
                user.Password = reader["password"].ToString();
                user.Active = Convert.ToBoolean(reader["Active"]);
            }
            return user;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    ////---------------------------------------------------------------------------------
    //// get movies by date
    ////---------------------------------------------------------------------------------
    public List<Movie> GetMovieByDate(DateOnly startDate,DateOnly endDate)
    {
        List<Movie> movies = new List<Movie>();
        SqlConnection con;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }
        SqlCommand cmd = new SqlCommand("SP_GetMoviesByDateRange_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StartDate", startDate);
        cmd.Parameters.AddWithValue("@EndDate", endDate);


        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Movie m = new Movie();
                m.Id = (int)reader["Id"];
                m.Url = reader["url"].ToString();
                m.PrimaryTitle = reader["primaryTitle"].ToString();
                m.Description = reader["description"].ToString();
                m.PrimaryImage = reader["primaryImage"].ToString();
                m.Year = (int)reader["year"];
                m.ReleaseDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["releaseDate"]));
                m.Language = reader["language"].ToString();
                m.Budget = Convert.ToDouble(reader["budget"]);
                m.GrossWorldwide = Convert.ToDouble(reader["grossWorldwide"]);
                m.Genres = reader["genres"].ToString();
                m.IsAdult = Convert.ToBoolean(reader["isAdult"]);
                m.RuntimeMinutes = Convert.ToInt32(reader["runtimeMinutes"]);
                m.AverageRating = Convert.ToDecimal(reader["averageRating"]);
                m.NumVotes = Convert.ToInt32(reader["numVotes"]);
                m.PriceToRent = Convert.ToInt32(reader["priceToRent"]);


                movies.Add(m);
            }
            return movies;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }
    ////---------------------------------------------------------------------------------
    //// get movies by date
    ////---------------------------------------------------------------------------------
    public JsonElement getCartDetail(int userId, int movieId)
    {
        JsonElement data;
        SqlConnection con;

        try
        {
            con = connect("myProjDB");
        }
        catch (Exception ex)
        {
            throw (ex);
        }

        SqlCommand cmd = new SqlCommand("SP_GetCart_2025", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@UserId", userId);
        cmd.Parameters.AddWithValue("@MovieId", movieId);

        try
        {
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var obj = new
                {
                    UserId = (int)reader["UserId"],
                    MovieId = (int)reader["MovieId"],
                    RentEnd = DateOnly.FromDateTime(Convert.ToDateTime(reader["RentEnd"])).ToString("yyyy-MM-dd"),
                    TotalPrice = Convert.ToDouble(reader["TotalPrice"])
                };

                string jsonString = JsonSerializer.Serialize(obj);
                data = JsonSerializer.Deserialize<JsonElement>(jsonString);
                return data;
            }

            return default;
        }
        catch (Exception ex)
        {
            throw (ex);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
            }
        }
    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if (paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            }


        return cmd;
    }


}
