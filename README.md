# Overview

Build a microservice that lets users manage a personal library of music albums. Album data comes from a
third-party music catalogue, either Spotify or Deezer.
We're not after a production-complete system. What we want to see is how you structure one.

## Deployment

PREREQUISITES: .NET 10 SDK, SQL Server, Docker
- In this solution, the API runs locally, but it depends on SQL Server database, which runs on Docker. I have added a docker-compose file in the repository which   contains the setup of the database service.
To start the database service container, navigate at the root directory of the project where the docker-compose file is located and use the following command in the command line:

  ` docker-compose up `

- After database container is up and running, you can build and run the project (MusicAlbumsLibrary.Api) from the IDE of your choice or you can use the following commands in the command line:  ` dotnet run `  or  ` dotnet watch run `

- The API runs on [*https://localhost:7282/*](https://localhost:7282/). I have configured Swagger UI for the API endpoints [*https://localhost:7282/swagger/index.html*](https://localhost:7282/swagger/index.html)