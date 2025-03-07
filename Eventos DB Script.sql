CREATE DATABASE EventosDB;
GO

USE EventosDB;
GO

-- Creación de la tabla Salones
CREATE TABLE Salones (
    IdSalon INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Capacidad INT NOT NULL,
    Ubicacion NVARCHAR(200) NOT NULL
);

-- Creación de la tabla Eventos
CREATE TABLE Eventos (
    IdEvento INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Fecha DATE NOT NULL,
    HoraInicio TIME NOT NULL,
    HoraFin TIME NOT NULL,
    IdSalon INT NOT NULL,
    FOREIGN KEY (IdSalon) REFERENCES Salones(IdSalon)
);

-- Creación de la tabla Reservas
CREATE TABLE Reservas (
    IdReserva INT IDENTITY(1,1) PRIMARY KEY,
    IdEvento INT NOT NULL,
    NombreCliente NVARCHAR(100) NOT NULL,
    Contacto NVARCHAR(50) NOT NULL,
    FOREIGN KEY (IdEvento) REFERENCES Eventos(IdEvento)
);

-- Inserción de datos de prueba
INSERT INTO Salones (Nombre, Capacidad, Ubicacion) VALUES
('Salon A', 100, 'Edificio Central'),
('Salon B', 150, 'Edificio Anexo'),
('Salon C', 200, 'Planta Alta');

INSERT INTO Eventos (Nombre, Fecha, HoraInicio, HoraFin, IdSalon) VALUES
('Conferencia de Tecnología', '2024-07-15', '09:00', '12:00', 1),
('Taller de Programación', '2024-07-16', '14:00', '18:00', 2),
('Charla Motivacional', '2024-07-17', '10:00', '13:00', 3);

INSERT INTO Reservas (IdEvento, NombreCliente, Contacto) VALUES
(1, 'Carlos Pérez', 'carlos@example.com'),
(2, 'María López', 'maria@example.com'),
(3, 'Juan Rodríguez', 'juan@example.com');
