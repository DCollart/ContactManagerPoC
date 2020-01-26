# ContactManagerPoC
This project is a proof of concept to show how I would structured and develop a web API.

## Git
This repository will use the [conventional commit notation](https://www.conventionalcommits.org/en/v1.0.0/).

## Folders structure
The root folders follow the [Microsoft guidelines](https://docs.microsoft.com/en-us/dotnet/core/porting/project-structure) about the source and the tests code.

## Projects organization
The source code will be structured as a classical onion architecture.

* Domain: Contains all the domain/business logic.
* Infrastructure: Contains all the purely technical code (database/file/network/... related).
* Application: Contains all the use cases. Kind of orchestration layer.
* WebApi : The name itself should be enough :-)