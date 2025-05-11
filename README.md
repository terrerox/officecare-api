# Equipment API

This API manages equipment, equipment types, and maintenance tasks.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (local or remote)

## Database Setup

1. **Create the Database**

   Create a database named `equipment` in your SQL Server instance.

2. **Run the following SQL script to create tables and seed data:**

   ```sql
   -- 1. Create EquipmentType table
   CREATE TABLE dbo.EquipmentTypes (
       Id           INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
       Description  NVARCHAR(100)     NOT NULL
   );

   -- 2. Create Equipment table
   CREATE TABLE dbo.Equipments (
       Id              INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
       Brand           NVARCHAR(100)     NOT NULL,
       Model           NVARCHAR(100)     NOT NULL,
       EquipmentTypeId INT               NOT NULL,
       PurchaseDate    DATE              NOT NULL,
       SerialNumber    NVARCHAR(100)     NULL,
       CONSTRAINT FK_Equipment_EquipmentType
           FOREIGN KEY (EquipmentTypeId)
           REFERENCES dbo.EquipmentTypes(Id)
           ON UPDATE CASCADE
           ON DELETE NO ACTION
   );

   -- 3. Create MaintenanceTask table
   CREATE TABLE dbo.MaintenanceTasks (
       Id          INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
       Description NVARCHAR(200)     NOT NULL
   );

   -- 4. Create EquipmentMaintenance join table (many-to-many)
   CREATE TABLE dbo.EquipmentMaintenances (
       EquipmentId       INT NOT NULL,
       MaintenanceTaskId INT NOT NULL,
       CONSTRAINT PK_EquipmentMaintenance PRIMARY KEY (EquipmentId, MaintenanceTaskId),
       CONSTRAINT FK_EM_Equipment
           FOREIGN KEY (EquipmentId)
           REFERENCES dbo.Equipments(Id)
           ON UPDATE CASCADE
           ON DELETE CASCADE,
       CONSTRAINT FK_EM_MaintenanceTask
           FOREIGN KEY (MaintenanceTaskId)
           REFERENCES dbo.MaintenanceTasks(Id)
           ON UPDATE CASCADE
           ON DELETE CASCADE
   );

   -- Seed EquipmentTypes
   INSERT INTO dbo.EquipmentTypes (Description) VALUES
       ('Laptop'),
       ('Desktop'),
       ('Printer'),
       ('Monitor');
   ```

## Configuration

1. **Set the connection string**

   The API uses the connection string in `Api/appsettings.json` under `DefaultConnection`. Example:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost,1433;Database=equipment;User Id=sa;Password=YourPassword;Encrypt=True;TrustServerCertificate=True;"
   }
   ```

   - Update `Server`, `User Id`, and `Password` as needed for your SQL Server instance.

## Running the API

1. Open a terminal and navigate to the `Api` directory:

   ```sh
   cd Api
   ```

2. Run the API:

   ```sh
   dotnet run
   ```

3. The API will start (by default on `http://localhost:5208` or as shown in the console output).

4. Open your browser and navigate to `http://localhost:5208/swagger` to access the Swagger UI and test the endpoints.

## Project Structure

- `Api/` - ASP.NET Core Web API project
- `Repository/` - Data access and models
- `Services/` - Business logic

## Notes

- Ensure your SQL Server is running and accessible before starting the API.
- If you change the database name or credentials, update the connection string accordingly. 