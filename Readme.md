# Gateways Test

## Software Requirements
Programming languages: C#
Framework: ASP.NET / ASP.NET Core
Database: MSSQL or in-memory
Automated build: Solution of choice
UI: Angular
## Description
This sample project is managing gateways - master devices that control multiple peripheral devices.
Your task is to create a REST service (JSON/HTTP) for storing information about these gateways and
their associated devices. This information must be stored in the database.
When storing a gateway, any field marked as “to be validated” must be validated and an error returned if it
is invalid. Also, no more that 10 peripheral devices are allowed for a gateway.
The service must also offer an operation for displaying information about all stored gateways (and their
devices) and an operation for displaying details for a single gateway. Finally, it must be possible to add and
remove a device from a gateway.

### Each gateway has:
* a unique serial number (string),
* human-readable name (string),
* IPv4 address (to be validated),
* multiple associated peripheral devices.
### Each peripheral device has:
* a UID (number),
* vendor (string),
* date created,
* status - online/offline.

# Installation
## Database Configuration
Set the database configuration in the `appsettings.json` file at the `DomainConnectionString` key. The Database provider used is MSSQL Server.
## Generate Database
To initialize the database use the migrations in the `GatewaysTest.Infrastructure` project and start the update using the `GatewaysTest.Api` project.
```
-dotnet ef database update -s ./GatewaysTest.Api
```
## Aws Configuration

In case of use Aws as automatic build as part of CI/CD process configure in the BuildProject to use the source code yml file and set the current variables 
- `ACCOUNT`: The aws account
- `REPOSITORY`: The elastic image repository
- `IMAGE_NAME`: The docker image to store the build
- `FUNCTION`: The lambda function name
