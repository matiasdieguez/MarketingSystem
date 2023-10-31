# Crear Directorio del sistema
- Ej.
mkdir MarketingSystem

# Crear proyecto Backend
- cd MarketingSystem
- dotnet new webapi -o "MarketingSystem.Backend"
- Cambiar la configuracion del .csproj a:
    <Nullable>annotations</Nullable>

# Crear proyecto Dto
- cd MarketingSystem
- dotnet new classlib -o "MarketingSystem.Dto"
- Cambiar la configuracion del .csproj a:
    <Nullable>annotations</Nullable>

# Crear proyecto Frontend
- cd ..
- dotnet new blazorwasm -o "MarketingSystem.Frontend"
- Cambiar la configuracion del .csproj a:
    <Nullable>annotations</Nullable>

# Borramos el ejemplo de WheatherForecast

# Creamos la carpeta Mod33els en Backend
- cd MarketingSystem.Backend
- mkdir Models

# Copiamos los archivos Contact.cs y Products.cs a Models

# Agregamos los siguientes paquetes para manejar la base de datos
- dotnet add package Microsoft.EntityFrameworkCore
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Tools

# Configurar en Program.cs de Backend Entity Framework

- En linea 10 agregar:

builder.Services.AddDbContext<MarketingSystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketingSystemDbConnection"));
});

# Crear la clase de DbContext
- Dentro de Models

    public class MarketingSystemDbContext : DbContext
    {
        public MarketingSystemDbContext(DbContextOptions<MarketingSystemDbContext> context) : base(context)
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Product> Products { get; set; }
 
    }

# Agregar en Program.cs los siguientes usings:
using MarketingSystem.Backend.Models;
using Microsoft.EntityFrameworkCore;

# Instalacion de servidor de base de datos
- SQL Server Developer
- https://go.microsoft.com/fwlink/p/?linkid=2215158&clcid=0x40A&culture=es-es&country=es


# Ingresar el connection string en appsettings.json
- Agregar en linea 8
  "ConnectionStrings": {
    "MarketingSystemDbConnection":"Server=(localdb)\\MSSQLLocalDB;Database=MarketingSystem;Integrated Security=true"
  },

# Ejecutar comandos de EF
- dotnet ef migrations add Initial
- dotnet ef datbase update

# Para ver la base de datos, instalar extension vscode MS SQL
- Ejecutar el comando (Ctrl+P) MS SQL Add Connection
- Pegar el connection string
  
# Clonar repositorio
- Ejecutar
- git clone https://github.com/matiasdieguez/MarketingSystem.git

