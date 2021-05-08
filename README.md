# CarnaWebAPIChallenge

Create and configure appsettings.json in API folder.

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "CarnaPostgreSqlConnection": "Host=<hostname>;Database=<db_name>;Username=<username>;Password=<password>"
  }
}
 
 
 Run, `dotnet restore` on root folder for restoring dependencies.
 Then run, `dotnet watch run` on API folder.


