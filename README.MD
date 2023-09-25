
# BookImporter - Summary of Submission

This is my submission for the BookImporter coding assessment. 
 - Simple .NET 6 WebAPI to upload .txt files, from which it extracts data from.
 - Output is a database write to a DB (scaffolded using EF Core code-first approach)
 - Created with production ready practices in mind (database logging, with import batch metadata)
 - Uses Strategy Design Pattern for parsing book formats.
 - Unit tests included

## Technologies and Tools Used:
- .NET 6 WebAPI
-  EF Core 7
- VS 2022, SSMS 18

## Local Development Setup

### Prerequisites

- .NET 6 Runtime: https://dotnet.microsoft.com/en-us/download/dotnet/6.0
  - Download the version for your OS.
- Database : Sql Server: https://www.microsoft.com/en-us/sql-server/sql-server-downloads
  - Download express for quick-setup local development / testing.

### Automated Local Setup
- If you are running windows, you can run an automated batch file located in the root folder of the application. 
```
local-machine-setup.bat
```
- Take note that installing .NET 6 runtime is a prerequisite, and installation of .NET 6 is not part of the automated process (mainly due to needing a system restart).
- Alternatively, you can follow the below steps to setup local development manually.

### Migrations
- Requires dotnet ef CLI Tools : https://docs.microsoft.com/en-us/ef/core/cli/dotnet
- Install using terminal (requires .NET runtime)
```
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef
```
- In the root folder (containing the .sln file), run the following in the terminal: 
```
dotnet ef database update -p "./BookImporter.Repositories" -s "./BookImporter.Web"
```
### How to test
- In the root folder (containing the .sln file), run the following in the terminal: 
```
dotnet test 
```
### How to run
- In the root folder (containing the .sln file), run the following in the terminal:
- API:
```
dotnet run --project "./BookImporter.Web"
```
### View Swagger Doc
- While running, open a browser and navigate to https://localhost:7092/swagger


