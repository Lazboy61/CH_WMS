# CargoHubTeam2

- Khadija Arkhouch
- Sara Mokadem
- Hasan cakir

### eerste stap

verplaats het folder Tests buiten het CH_WMS folder anders werkt het niet!.

## Vereisten om het project te runnen

### 1. Installeer .NET SDK 8.0

[Download hier](https://dotnet.microsoft.com/download/dotnet/8.0)

### 2. Databasevereisten

- **SQL Server** [Download hier](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **PostgreSQL** [Download hier](https://www.postgresql.org/download/)

### 3. Tools

- **Entity Framework Core CLI:**
  ```bash
  dotnet tool install --global dotnet-ef
  ```

Herstel de dependencies (NuGet-pakketten):
dotnet restore

### Alternatieve installatie van dependencies

Als `dotnet restore` niet werkt of als je handmatig pakketten wilt toevoegen, gebruik de volgende commando's:

```bash
dotnet add package Castle.Core --version 5.1.1
dotnet add package FluentAssertions --version 8.0.0
dotnet add package Microsoft.AspNetCore.OpenApi --version 8.0.7
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 9.0.1
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.1
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.1
dotnet add package Swashbuckle.AspNetCore --version 7.2.0
dotnet add package xunit --version 2.4.1
dotnet add package xunit.runner.visualstudio --version 3.0.0
dotnet add package System.ComponentModel.Annotations --version 5.0.0
```

Zorg ervoor dat deze commando's worden uitgevoerd in de map waar het .csproj-bestand zich bevindt, anders krijg je een foutmelding.

## Prerequiste

Download Docker set up https://www.docker.com/products/docker-desktop/

### 3 dotnet ef database update

will use the latest created migrations and will push its database structure in the DB that can be seen at http://localhost:8081/login?next=/

### In terminal

docker compose up
doe migration
doe dotnet ef update
Open je browser

### In PgAdmin

open PgAdmin via localhost:8081
username "admin@ad.min" en wachtwoord "admin" om in te loggen
nadat je bent ingelogd doe je rechts klikken op eerste optie "Servers"
klik op register
klik op Servers...
en dan zie een tabel gelijk voor je zie je name
Name = cargohub
en daarna ga je naar het tab "Connections"
bij tab Connections vul je het volgende in
hostname: postgres
port: 5432
Username: admin@ad.min
Password: admin
klik Save

### Terug naar terminal

dotnet run --load-json
daarna krijg je een optie en je schrijft daarna "yes"
wachten totdat alles is geload
en dan kun je naar localhost5000:swagger om alles te checken (als het niet werk check je localhost url dotnet run en dan zie je daar welke localhost je gebruikt)

### Testen

voor dat je begint met testen zorg er voor dat je in een andere terminal het program.cs heb gestart en controleer of het docker database wel is gestart anders gaan de testen fout.
