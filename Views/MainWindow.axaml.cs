using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace parcialFinal;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        CargarLibros();
        ActualizarBotones();
    }

    LibroServicio servicio = new LibroServicio();

    private Libro? libroSeleccionado;

    private EstadoLectura ObtenerEstado()
    {
        return (EstadoLectura)CmbEstado.SelectedIndex;
    }

    private void CargarLibros()
    {
        LstLibros.ItemsSource = servicio.Listar();
    }

    private void LimpiarCampos()
    {
        TxtNombre.Text = "";
        TxtAutor.Text = "";
        CmbEstado.SelectedIndex = 0;

        libroSeleccionado = null;
        LstLibros.SelectedItem = null;

        ActualizarBotones();
    }

    private void ActualizarBotones()
    {
        bool haySeleccion = libroSeleccionado != null;

        BtnAgregar.IsEnabled = !haySeleccion;
        BtnActualizar.IsEnabled = haySeleccion;
        BtnBorrar.IsEnabled = haySeleccion;
    }

    private void Boton_Agregar(object? sender, RoutedEventArgs e)
    {
        try
        {
            string nombre = TxtNombre.Text ?? "";
            string autor = TxtAutor.Text ?? "";
            EstadoLectura estado = ObtenerEstado();

            servicio.Agregar(nombre, autor, estado);

            CargarLibros();

            TxtMensaje.Text = "Libro agregado correctamente.";

            LimpiarCampos();
        }
        catch (Exception ex)
        {
            TxtMensaje.Text = ex.Message;
        }
    }

    private void Boton_Actualizar(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (libroSeleccionado == null)
            {
                throw new ArgumentException("Selecciona un libro primero.");
            }

            string nombre = TxtNombre.Text ?? "";
            string autor = TxtAutor.Text ?? "";
            EstadoLectura estado = ObtenerEstado();

            servicio.Modificar(
                libroSeleccionado.ID,
                nombre,
                autor,
                estado
            );

            CargarLibros();

            TxtMensaje.Text = "Libro actualizado correctamente.";

            LimpiarCampos();
        }
        catch (Exception ex)
        {
            TxtMensaje.Text = ex.Message;
        }
    }

    private void Boton_Borrar(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (libroSeleccionado == null)
            {
                throw new ArgumentException("Selecciona un libro primero.");
            }

            servicio.Eliminar(libroSeleccionado.ID);

            CargarLibros();

            TxtMensaje.Text = "Libro eliminado correctamente.";

            LimpiarCampos();
        }
        catch (Exception ex)
        {
            TxtMensaje.Text = ex.Message;
        }
    }

    private void Boton_Limpiar(object? sender, RoutedEventArgs e)
    {
        LimpiarCampos();
        TxtMensaje.Text = "Interfaz limpiada.";
    }

    private void LstLibros_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LstLibros.SelectedItem is Libro libro)
        {
            libroSeleccionado = libro;

            TxtNombre.Text = libro.Nombre;
            TxtAutor.Text = libro.Autor;

            CmbEstado.SelectedIndex = (int)libro.Estado;

            TxtMensaje.Text = "Libro seleccionado.";
        }
        else
        {
            libroSeleccionado = null;
        }

        ActualizarBotones();
    }
}