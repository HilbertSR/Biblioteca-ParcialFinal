using System;
using System.Collections.Generic;

public class LibroServicio
{
    private readonly LibroADO ado;
    public LibroServicio()
    {
        ado = new LibroADO();
        ado.Inicializar();
    }

    public void Agregar(string nombre, string autor, EstadoLectura estado)
    {
        Libro libro = new Libro(nombre, autor, estado);

        ado.Insertar(libro);
    }

    public List<Libro> Listar()
    {
        return ado.Leer();
    }

    public void Modificar(int id, string nombre, string autor, EstadoLectura estado)
    {
        Validaciones.ValidarID(id);

        Libro? libro = ado.Leer().Find(l => l.ID == id);

        if (libro == null)
        {
            throw new ArgumentException("No existe un libro con ese ID.");
        }

        libro.Actualizar(nombre, autor, estado);

        ado.Actualizar(libro);
    }

    public void Eliminar(int id)
    {
        Validaciones.ValidarID(id);

        Libro? libro = ado.Leer().Find(l => l.ID == id);

        if (libro == null)
        {
            throw new ArgumentException("No existe un libro con ese ID.");
        }

        ado.Eliminar(id);
    }
}