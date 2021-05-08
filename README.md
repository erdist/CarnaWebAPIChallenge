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
 
 Change http connection address on API/Properties/launchSettings.json to either localhost:5000 or your own local machine IP such as 192.168.x.x:5000.
 Run, `dotnet restore` on root folder for restoring dependencies.
 Then run, `dotnet watch run` on API folder.


