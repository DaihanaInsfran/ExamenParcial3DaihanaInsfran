using System;
using ConsoleApp1.ExamenPar2.Modelo;
using ConsoleApp1.EXAMENPAR2.MODELO;

using ConsoleApp1.ExamenPar2.Modelo;

namespace ConsoleApp1.Servicio
{
    public class ClaseServicioProducto
    {
        public static bool ValidarProducto(Producto producto, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (string.IsNullOrWhiteSpace(producto.Descripcion) ||
                producto.CantidadMinima <= 0 ||
                producto.CantidadStock < 0 ||
                producto.PrecioCompra <= 0 ||
                producto.PrecioVenta <= 0 ||
                string.IsNullOrWhiteSpace(producto.Categoria) ||
                string.IsNullOrWhiteSpace(producto.Marca) ||
                string.IsNullOrWhiteSpace(producto.Estado))
            {
                mensajeError = "Todos los campos son obligatorios y deben tener valores válidos.";
                return false;
            }

            if (producto.CantidadMinima <= 1)
            {
                mensajeError = "La cantidad mínima debe ser un número mayor a 1.";
                return false;
            }

            return true;
        }
    }
}
