# 🎬 Movie World 

## Project Overview

This is a full-stack web application designed to manage and interact with a movie catalog, possibly serving as a basic **Movie Rental or Management System**. The system provides user authentication (login/register), allows users to view available movies, manage their rented list, and includes a dedicated administration area for catalog control.

This project was developed as a hands-on exercise to practice core web development skills: creating a responsive front-end interface, implementing client-server communication using AJAX, and building a RESTful API with database integration.

## 🚀 Features

* **User Authentication:** Secure user registration and login pages (`registerPage.html`, `loginPage.html`).
* **Movie Catalog:** Browse and view all available movies on the main page (`index.html`).
* **User Profiles:** Functionality to edit user profile details (`editProfilePage.html`).
* **Movie Rental/Tracking:** Pages dedicated to renting movies and viewing a personal list of rented items (`RentMovie.html`, `myMovies.html`).
* **Admin Tools:** Dedicated administrative page to add new movies to the catalog (`adminPage.html`, `addMovie.html`).
* **API Endpoints:** A comprehensive set of endpoints managed by **MoviesController** and **UsersController** for data handling.
* **Client-Server Interaction:** Seamless data fetching and manipulation using AJAX calls defined in `scripts/ajaxCalls.js`.

## ⚙️ Technologies Used

This project is built using a classic client-server architecture.

| Component | Technology | Description |
| :--- | :--- | :--- |
| **Frontend** | HTML5, CSS | Structure and styling of the user interface. |
| **Frontend Logic** | JavaScript, AJAX | Handles user interaction, form validation, and asynchronous communication with the API. |
| **Backend API** | C# / ASP.NET Core Web API (.NET 6.0+) | Provides the RESTful endpoints for users and movies. |
| **Business Logic (BL)** | C# Classes (`User.cs`, `Movie.cs`) | Defines the core data models and business rules. |
| **Data Access Layer (DAL)** | C# (`DBservices.cs`) | Manages connections and CRUD operations with the database. |
| **Database** | SQL Server (or similar) | Used to store user credentials and the movie catalog. |

## 🛠️ Getting Started

### Prerequisites

You need the following software installed on your machine:

* [.NET SDK](https://dotnet.microsoft.com/download) (Version 6.0 or higher)
* A code editor (e.g., Visual Studio or VS Code).
* A local or accessible **SQL database** instance configured with the necessary tables.

### Installation & Setup

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/Roi-Gisman/cgroup21
    cd cgroup21
    ```
2.  **Database Setup:**
    * Create your database structure (tables for Users, Movies, Rentals, etc.).
    * Update the connection string in the configuration file (`server/appsettings.json`) to point to your local database.
3.  **Run the Backend (Server):**
    * Navigate to the server directory: `cd server`
    * Run the API:
        ```bash
        dotnet run
        ```
    * The API will typically start on a port like `http://localhost:5000` or `http://localhost:5001`.
4.  **Run the Frontend (Client):**
    * Open the main file (`client/index.html`) in your web browser.
    * *(Note: You might need to use a local web server extension (like Live Server for VS Code) to run the HTML/JS pages and handle AJAX requests correctly, depending on your browser's security settings.)*

## ✍️ Author

* **Roi Gisman** - *Initial Work & Development*
* [LinkedIn](https://www.linkedin.com/in/roi-gisman)

* ## 📜 License

This project is open source and available under the [MIT License](https://opensource.org/licenses/MIT).
