# JobBoardApp
Project to manage small Job Board CRUD, powered by Kevin Omar Alvarez.

## Before run the project you would need

* SQLEXPRESS
* NET Core 3.1 (LTS)
* Visual Studio 2019

> Note: If you want to change database connection go to `appsettings.Development.json`

## Database config

For the database I prefer to use migrations, so you only need to run the following command into the project folder to set it up `C:\...\JobBoardApp`

```console
> dotnet ef database update
```

> Note: If you use console method you need dotnet tool globally

The other way is to use de PM Console from VS2019

```package manager console
PM> cd .\MovieRental
PM > Update-Database
```