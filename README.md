# Tennis

This is a .NET web application used to simulate a tennis match. 

## Description

The user can create new tennis matches or query the current status using the REST API. The actual simulation of the tennis match is done using a queue that is passed through by the event of the ball played.  The application then simulates the next game for each such event. MessTransit was used to implement the queue and PostgreSQL was used to store all the states. 

## Getting Started

### Dependencies

* .NET 6
* ASP.NET Core
* Docker
* MessTransit
* PostgreSQL

### Executing program

You need to have Docker installed for the application to run properly. 
```
docker compose up
```
Docker image: https://hub.docker.com/r/stepanpicek/tennis
