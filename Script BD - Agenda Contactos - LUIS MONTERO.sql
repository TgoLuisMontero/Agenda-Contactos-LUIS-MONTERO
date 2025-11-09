CREATE DATABASE Agenda; -- Creamos la base de datos Agenda

USE Agenda; -- Usamos la base de datos

--Creamos la Tabla Contactos
CREATE TABLE Contactos 
(
	idContacto INT PRIMARY KEY IDENTITY(1,1),   -- Identificador Autoincrementable
	Nombre     VARCHAR(10) NOT NULL,            -- Nombre del Contacto
	Telefono   VARCHAR(20),                     -- Numero de Telefono
	Email      VARCHAR(150) UNIQUE              -- Correo electronico, dato unico...
);

--Consulta basica de prueba...
SELECT * FROM Contactos;

--Insertar datos de prueba desde la BD
INSERT INTO Contactos(Nombre, Telefono, Email)
Values('Luis', '809-963-0626', 'tgo.luismontero@outlook.com'),
	  ('Wilda', '809-000-4445', 'wsuero@gmail.com');

--Store Procedure: Insertar Contacto
CREATE PROC sp_insertar_contacto
@Nombre VARCHAR(10),
@Telefono VARCHAR(20),
@Email VARCHAR(150)
as
Begin
	INSERT INTO Contactos(Nombre, Telefono, Email)
	               VALUES(@Nombre, @Telefono, @Email)
End;

--Store Procedure: Buscar Contacto
CREATE PROC sp_buscar_contacto
@telefono VARCHAR (20)
AS
BEGIN
	SELECT * FROM Contactos Where Telefono LIKE '%' + @telefono + '%';
END;

--Store Procedure: Mostrar Contactos
CREATE PROC sp_mostrar_contactos
AS
Begin
	SET NOCOUNT ON;

	SELECT *
	FROM Contactos 
	ORDER BY idContacto ASC;
End;

--Store Procedure: Actualizar Contacto
CREATE PROC sp_modificar_contacto
@idContacto INT,
@Nombre VARCHAR(10),
@Telefono VARCHAR(20),
@Email VARCHAR(150)
AS
Begin
	UPDATE Contactos 
	SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email
	WHERE idContacto = @idContacto; -- Actualizamos por el id, todos los campos
End;

--Store Procedure: Eliminar Contacto
CREATE PROC sp_eliminar_contacto
@idContacto INT
As
Begin
	DELETE FROM Contactos WHERE idContacto = @idContacto; -- Eliminamos por el id
End;