# Interactive Podcast Project

This project is a full-stack application for an interactive podcast platform, built with an Angular frontend and a .NET backend.

## Project Structure

- **api/**: Contains the .NET Core backend with user registration, login functionality, and database migrations.
- **interactive-podcast-frontend/**: Angular-based frontend that handles user interaction.

## Prerequisites

1. **Node.js**: Install Node.js for running the Angular frontend.
2. **.NET SDK**: Install .NET Core SDK for running the backend.
3. **SQL Server**: Ensure that you have Microsoft SQL Server installed.

   - _By default, this project uses Microsoft SQL Server, but it can be configured to use other databases (e.g., PostgreSQL, MySQL, or SQLite) by updating the connection string and configuring Entity Framework accordingly._

## Development Setup

### 1. Backend (API)

1. Navigate to the `api` directory:

   ```bash
   cd api
   ```

2. Restore .NET dependencies:

   ```bash
   dotnet restore
   ```

3. **Set up the Database**:
   You will need to configure a database connection string using the **user secrets**. Here’s how to do that:

   1. Add user secrets for your database connection string:
      ```bash
      dotnet user-secrets init
      dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=your_server;Database=your_db;User Id=your_user;Password=your_password;"
      ```

   - The connection string should look something like this:
     ```plaintext
     Server=localhost;Database=interactive_podcast;User Id=sa;Password=YourPassword;
     ```

4. Apply database migrations to set up the necessary tables:

   ```bash
   dotnet ef database update
   ```

5. Run the backend:
   ```bash
   dotnet run
   ```

### 2. Frontend (Angular)

1. Navigate to the `interactive-podcast-frontend` directory:

   ```bash
   cd interactive-podcast-frontend
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Run the development server:

   ```bash
   npm start
   ```

   The app will automatically reload if you make changes to the source files. You can access the app at `http://localhost:4200/`.

## Features

- **User Registration**: Allows new users to register with their details.
- **User Login**: Users can log in to access platform features.
- **Database Migrations**: The project includes initial database migrations to set up the necessary tables.

## Running Unit Tests

- **Backend**: You can run unit tests for the backend by navigating to the `api` directory and running:

  ```bash
  dotnet test
  ```
