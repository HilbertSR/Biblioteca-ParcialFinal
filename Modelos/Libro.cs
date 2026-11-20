using System;

public enum EstadoLectura
{
    Pendiente,
    Leyendo,
    Leido
}
public class Libro
{
    public int ID { get; private set; }
    public string Nombre { get; private set; }
    public string Autor { get; private set; }
    public EstadoLectura Estado { get; private set; }

    public Libro(string n, string a, EstadoLectura e)
    {
        string nombre = Validaciones.ValidarNombre(n);
        string autor = Validaciones.ValidarAutor(a);
        EstadoLectura estado = Validaciones.ValidarEstado(e);

        Nombre = nombre;
        Autor = autor;
        Estado = estado;
    }

    public Libro(int id, string n, string a, EstadoLectura e) : this(n, a, e)
    {
        int identificacion = Validaciones.ValidarID(id);
        ID = identificacion;
    }

    public void Actualizar(string n, string a, EstadoLectura e)
    {
        string nombre = Validaciones.ValidarNombre(n);
        string autor = Validaciones.ValidarAutor(a);
        EstadoLectura estado = Validaciones.ValidarEstado(e); 

        Nombre = nombre;
        Autor = autor;
        Estado = estado;
    }

    public override string ToString()
    {
        return $"{ID} | {Nombre} | {Autor} | {Estado}";
    }
}