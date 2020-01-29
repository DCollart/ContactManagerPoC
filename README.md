[![Build Status](https://dev.azure.com/Mithril-Belgium/PoC/_apis/build/status/DCollart.ContactManagerPoC?branchName=master)](https://dev.azure.com/Mithril-Belgium/PoC/_build/latest?definitionId=4&branchName=master)
[![Build Status](https://dev.azure.com/Mithril-Belgium/PoC/_apis/build/status/DCollart.ContactManagerPoC?branchName=master)](https://dev.azure.com/Mithril-Belgium/PoC/_build/latest?definitionId=4&branchName=master)
# ContactManagerPoC
This project is a proof of concept to show how I would structured and develop a web API.
The patterns/archichecture are obviously overkilled for that kind of CRUD application. 

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