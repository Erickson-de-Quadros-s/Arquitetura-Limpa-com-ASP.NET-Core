# DevFreela.API
This project was started because of the knowledge journey in ASP.NET Core where was implemented clean architecture .

![GitHub repo size](https://github.com/Erickson-de-Quadros-s/Arquitetura-Limpa-com-ASP.NET-Core)

## Description

This application using EntityFrame Work with SQL for storage and Swagger for API documentation.

### Objective
The primary objective of this project is using EntityFrameWork and your application


### Technologies implemented:

[![.NET](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/download)
[![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/get-started)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-007878?style=for-the-badge&logo=entity-framework&logoColor=white)](https://learn.microsoft.com/en-us/ef/)
[![Swagger](https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=white)](https://swagger.io/docs/)



## Installation
```bash
$ docker pull mcr.microsoft.com/mssql/server:2022-latest
```
## Configuration
```
$ docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=YourPassword!' -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server:2022-latest
```

### Clone the repository:
```bash
$ git clone <https://github.com/Erickson-de-Quadros-s/Arquitetura-Limpa-com-ASP.NET-Core>
$ cd Arquitetura-Limpa-com-ASP.NET-Core
```

## Running the app
### development:

```bash
$ dotnet build
$ dotnet run
```

## Running the app with docker

### Prerequisites
Validate that Docker and Docker Compose are installed on your system. You can download and install Docker Desktop from [here](https://www.docker.com/products/docker-desktop).

```bash
$ docker-compose up --build
$ docker pull mcr.microsoft.com/mssql/server:2022-latest
$ docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=SuaSenha!' -p 1433:1433 --name sql_server_container -d mcr.microsoft.com/mssql/server:2022-latest
```

## Acess the application:
- Swagger UI: http://localhost:5025/swagger



