# Overview

Build a microservice that lets users manage a personal library of music albums. Album data comes from a
third-party music catalogue, either Spotify or Deezer.
We're not after a production-complete system. What we want to see is how you structure one.

## Deployment

PREREQUISITES: .NET 10 SDK, SQL Server, Docker
- In this solution, the API runs locally, but it depends on SQL Server database, which runs on Docker. I have added a docker-compose file in the repository which   contains the setup of the database service.
To start the database service container, navigate at the root directory of the project where the docker-compose file is located and use the following command in the command line:

  ```bash
   docker-compose up 
  ```

- After database container is up and running, you can build and run the project (MusicAlbumsLibrary.Api) from the IDE of your choice or you can use the following commands in the command line:  
  ```bash
   dotnet run
  ```
  or

  ```bash
   dotnet watch run
  ```

- Also, you can run the test suite direcly from the IDE, or use the following commands:

  Run all tests:
  ```bash
   dotnet test
  ```

  Run a specific test project:
  ```bash
   dotnet test tests/MusicAlbumsLibrary.UnitTests
   dotnet test tests/MusicAlbumsLibrary.IntegrationTests
  ```

- The API runs on [*https://localhost:7282/*](https://localhost:7282/). I have configured Swagger UI for the API endpoints [*https://localhost:7282/swagger/index.html*](https://localhost:7282/swagger/index.html)


## Architecture

The solution follows Clean Architecture design. 
The Core/Domain layer does not have dependencies on other layers. Application layer depends only on Core/Domain layer. Infrastructure layer depends on Application layer. The API project wires everything together.

` MusicAlbumsLibrary.Core `   contains entities and repository interfaces

` MusicAlbumsLibrary.Application `    contains services for feature implementation, custom exceptions, abstractions/contracts

` MusicAlbumsLibrary.Infrastructure `    handles external concerns: database configuration, third-party api http clients

` MusicAlbumsLibrary.Api `    the entry point of the application. It contains controllers, api models, custom middlewares


## Notes

What I would do next:
- Add more unit and integration tests, to include most of the services and api endpoints
- Add api models request validations
- Add retry and timeout configurations for the third-party client providers
