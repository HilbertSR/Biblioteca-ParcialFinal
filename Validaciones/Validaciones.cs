using System;

static class Validaciones
{
    public static int ValidarID(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("El id debe ser mayor a 0");
        }

        return id;
    }

    public static string ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre no puede estar vacio.");
        }

        return nombre.Trim();
    }

    public static string ValidarAutor(string autor)
    {
        if (string.IsNullOrWhiteSpace(autor))
        {
            throw new ArgumentException("El autor no puede estar vacio.");
        }

        return autor.Trim();
    }

    public static EstadoLectura ValidarEstado(EstadoLectura estado)
    {
        if (!Enum.IsDefined(typeof(EstadoLectura), estado))
        {
            throw new ArgumentException("El estado es incorrecto.");
        }

        return estado;
    }
}