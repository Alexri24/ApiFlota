CREATE DATABASE ApiFlotaDB;

SELECT name, database_id, create_date 
FROM sys.databases 
WHERE name = 'ApiFlotaDB';

USE ApiFlotaDB;

CREATE TABLE Conductores (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(255) NOT NULL,
    Licencia NVARCHAR(50) NOT NULL,
    Antiguedad INT NOT NULL,
    SalarioBase DECIMAL(10, 2) NOT NULL,
    EsInternacional BIT NOT NULL,
    FechaNacimiento DATETIME2 NOT NULL
);


CREATE TABLE Camiones (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Matricula NVARCHAR(50) NOT NULL,
    Marca NVARCHAR(100) NOT NULL,
    Kilometraje INT NOT NULL,
    CapacidadCarga DECIMAL(10, 2) NOT NULL,
    EsOperativo BIT NOT NULL DEFAULT 1,
    FechaCompra DATETIME2 NOT NULL
);


CREATE TABLE Mantenimientos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Tipo NVARCHAR(100) NOT NULL,
    Coste DECIMAL(10, 2) NOT NULL,
    HorasTrabajo INT NOT NULL,
    EsPreventivo BIT NOT NULL,
    FechaProgramada DATETIME2 NOT NULL,
    Descripcion NVARCHAR(MAX) NULL,
    CamionId INT NOT NULL,
    CONSTRAINT FK_Mantenimiento_Camion FOREIGN KEY (CamionId) REFERENCES Camiones(Id)
);


CREATE TABLE Rutas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Origen NVARCHAR(255) NOT NULL,
    Destino NVARCHAR(255) NOT NULL,
    DistanciaKm DECIMAL(10, 2) NOT NULL,
    Prioridad INT NOT NULL,
    DuracionEstimada INT NOT NULL,
    FechaInicio DATETIME2 NOT NULL,
    CamionId INT NOT NULL,
    CONSTRAINT FK_Ruta_Camion FOREIGN KEY (CamionId) REFERENCES Camiones(Id)
);


CREATE TABLE Asignaciones (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaAsignacion DATETIME2 NOT NULL,
    FechaFin DATETIME2 NULL,
    EstaActiva BIT NOT NULL,
    Motivo NVARCHAR(255) NULL,
    PrimaAsignacion DECIMAL(10, 2) NOT NULL,
    CamionId INT NOT NULL,
    ConductorId INT NOT NULL,
    CONSTRAINT FK_Asignacion_Camion FOREIGN KEY (CamionId) REFERENCES Camiones(Id),
    CONSTRAINT FK_Asignacion_Conductor FOREIGN KEY (ConductorId) REFERENCES Conductores(Id)
);

INSERT INTO Conductores (Nombre, Licencia, Antiguedad, SalarioBase, EsInternacional, FechaNacimiento)
VALUES
('Alejandro', 'C1', '3 años', 1900, 1, 10/10/1985),
('Jorge', 'C', '4 años', 1500, 1, 12/05/2000);

INSERT INTO Camiones (Matricula, Marca, Kilometraje, CapacidadCarga, EsOperativo, FechaCompra)
VALUES
('1234ABC', 'Mercedes', 123456, 1200, 0, 10/10/2010),
('5678DEF', 'Volvo', 1200, 1500, 1, 22/09/2012);

INSERT INTO Mantenimientos (Tipo, Coste, HorasTrabajo, EsPreventivo, FechaProgramada, Descripcion)
VALUES
('Cambio Aceite', 100, 2, 1, 23/11/2025, 'Cambio aceite'),
('Cambio Rueda', 1050, 25, 0, 24/01/2026, 'Cambio rueda por pinchazo');

INSERT INTO Rutas (Origen, Destino, DistanciaKm, Prioridad, DuracionEstimada, FechaInicio)
VALUES
('Zaragoza','Madrid', 300, 2, 3, 08/12/2025),
('Madrid', 'Barcelona', 550, 5, 5.5, 12/12/2025);

INSERT INTO Asignaciones (FechaAsignacion, FechaFin, EstaActiva, Motivo, PrimaAsignacion)
VALUES
(12/10/2025, 13/11/2025, 0, 'Ruta Larga', 1000),
(12/11/2025, 15/11/2025, 0, 'Ruta corta', 150);