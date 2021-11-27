# Fotoquest
Fotoquest repo is a solution for image processing, storage and provisioning in the Docker environment. Photos uploaded by mobile apps like FotoQuest Go should be enhanced, persisted and made available in different resolutions through an .Net Core API.

# Prerequisite
- Docker - https://docs.microsoft.com/en-us/virtualization/windowscontainers/quick-start/set-up-environment?tabs=Windows-Server

# Architecture & Components
- This project is developed using Clean architecture where all the layers are decoupled.
- Swagger Implementation
- Error Handling Middleware
- Health Checks all the external APIs used
- Docker Impementation
- Unit Tests

# Endpoints
- Swagger: http://localhost:50445/swagger/index.html
- Fotoquest API: http://localhost:50445/v1/image/
- Fotoquest API: https://localhost:50446/v1/image/
- Health: http://localhost:50445/health

# Documentation
- https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/visual-studio-tools-for-docker?view=aspnetcore-5.0

# Frameworks:
- Dotnetcore 3.1
- Docker 20.10.6


# How to run the solution:
- Clone the repo into a folder of your choice: git clone https://github.com/venkateshsoftnet/FotoQuest.WebApi
- Open powershell from the root folder eg: FotoQuest
- Type "docker-compose up"

The endpoint will be up and running. 
curl --location --request GET 'http://localhost:50445/v1/image' \
--header 'Content-Type: application/json'

# Running Unit Tests 
- Navigate to the unit test folder: cd FotoQuest.Application.UnitTests
- Type dotnet test, to run the unit tests included.

# What else can be done to improve code quality
- Authentication and authorisation can be implemented
- Logging can be enabled for all the layers
- Files can be stored in the cloud storage (AWS S3, Azure blob)
- Add UI validations
- More negative unit tests and improve code coverage 
- Add integration tests using SpecFlow
- Docker installation could be automated
- Proper branching strategy should be followed, eg: create a Release Candidate branch, create a new feature branch from it and raise pull requests on to the RC branch
