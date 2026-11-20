using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

public class LibroADO
{
    private readonly string biblioteca = "Data Source=biblioteca.db";

    public void Inicializar()
    {
        using var connection = new SqliteConnection(biblioteca);
        connection.Open();
        var crearTabla = connection.CreateCommand();
        crearTabla.CommandText = @"CREATE TABLE IF NOT EXISTS Libro (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Nombre TEXT NOT NULL,
            Autor TEXT NOT NULL,
            Estado INTEGER DEFAULT 0 NOT NULL)";
        crearTabla.ExecuteNonQuery();
    }

    public void Insertar(Libro libro)
    {
        using var connection = new SqliteConnection(biblioteca);
        connection.Open();
        var insertar = connection.CreateCommand();
        insertar.CommandText = "INSERT INTO Libro (Nombre, Autor, Estado) VALUES ($nombre, $autor, $estado)";
        insertar.Parameters.AddWithValue("$nombre", libro.Nombre);
        insertar.Parameters.AddWithValue("$autor", libro.Autor);
        insertar.Parameters.AddWithValue("$estado", (int)libro.Estado);
        insertar.ExecuteNonQuery();
    }

    public List<Libro> Leer()
    {
        List<Libro> libros = new List<Libro>();

        using var connection = new SqliteConnection(biblioteca);
        connection.Open();

        using var consultar = connection.CreateCommand();
        consultar.CommandText = "SELECT ID, Nombre, Autor, Estado FROM Libro";

        using var reader = consultar.ExecuteReader();

        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string nombre = reader.GetString(1);
            string autor = reader.GetString(2);
            EstadoLectura estado = (EstadoLectura)reader.GetInt32(3);

            Libro libro = new Libro(id, nombre, autor, estado);

            libros.Add(libro);
        }
        return libros;
    }

    public void Actualizar(Libro libro)
    {
        using var connection = new SqliteConnection(biblioteca);
        connection.Open();

        using var actualizar = connection.CreateCommand();

        actualizar.CommandText = "UPDATE Libro SET Nombre = $nombre, Autor = $autor, Estado = $estado WHERE ID = $id";
        actualizar.Parameters.AddWithValue("$nombre", libro.Nombre);
        actualizar.Parameters.AddWithValue("$autor", libro.Autor);
        actualizar.Parameters.AddWithValue("$estado", (int)libro.Estado);
        actualizar.Parameters.AddWithValue("$id", libro.ID);
        actualizar.ExecuteNonQuery();
    }

    public void Eliminar(int id)
    {
        using var connection = new SqliteConnection(biblioteca);
        connection.Open();

        using var eliminar = connection.CreateCommand();

        eliminar.CommandText = "DELETE FROM Libro WHERE ID = $id";
        eliminar.Parameters.AddWithValue("$id", id);
        eliminar.ExecuteNonQuery();
    }
}