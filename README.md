# Notes Project: Backend and Frontend

The Notes project is a web application for managing user notes. It consists of two parts: the backend developed using .NET and SQLite database, and the frontend built with Angular.

### Demo video
https://youtu.be/Fkxt-ohpSYc

## Technologies

### Backend

The Notes project backend is developed using the following technologies:

- .NET: a software development platform used for creating backend applications.
- C#: a programming language used for developing applications on the .NET platform.
- ASP.NET Core: a framework for building web applications on the .NET platform.
- SQLite: a database used for storing data in the project.

### Frontend

The Notes project frontend is developed using the following technologies:

- Angular: a platform for building web applications using TypeScript.
- HTML and CSS: used for markup and styling the user interface.

### Docker

The project includes a Dockerfile for creating container images for the backend and frontend. Docker facilitates the deployment and management process by packaging all the necessary dependencies and configurations into containers.

## Functionality

The application provides the following functionality:

- Registration and login for users and administrators.
- Creating, viewing, editing, and deleting user notes.
- Displaying a list of users for administrators and the ability to delete users.

### Backend

The backend of the application provides an API for managing users and notes. It includes the following controllers:

- `AuthController`: Responsible for handling requests related to user and administrator authentication. It provides endpoints for user and administrator registration and login.
- `AdminController`: Responsible for handling requests related to user management. It provides endpoints for retrieving a list of users and deleting users.
- `NotesController`: Responsible for handling requests related to user notes. It provides endpoints for retrieving notes, adding new notes, editing, and deleting existing notes.

The SQLite database is used for storing users and notes.

To offload the controllers and enable flexible data handling in the Notes project, the following approaches are used:

- Data Transfer Objects (DTO): DTOs are used for transferring data between the client and server. DTOs are data objects that differ from database entities and contain only the necessary information for performing operations.
- Service Layer: The Notes project utilizes a service layer for business logic and data operation handling. Services provide methods for performing operations such as user registration, retrieving a list of users, adding and deleting notes. Controllers interact with the service layer to handle requests and return corresponding HTTP responses to the client.

### Frontend

The frontend of the application is a web interface for interacting with the backend. It includes the following components:

- `AppComponent`: The root component of the application, displaying a header and a button for opening/closing authentication.
- `AuthComponent`: A component for user and administrator authentication. It contains forms for user and administrator registration and login.
- `AdminComponent`: A component for managing users by administrators. It displays a list of users and allows administrators to delete selected users.
- `UserComponent`: A component for managing user notes. It displays a list of user notes, allows adding new notes, editing, and deleting existing notes.



## Installation and Running

To install and run the application, follow these steps:

- Clone the project repository to your local machine.
- Make sure you have Docker and Docker Compose installed.
- Open a command prompt and navigate to the project's root folder.
- Run the command `docker-compose up` to start the server and client containers.
- After a successful startup, you can access the application in your browser at http://localhost:4200.

## Contribution

If you would like to contribute to the Notes project, you can do the following:

- Fork the project repository and make necessary changes.
- Submit a pull request with your changes.
