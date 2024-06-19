using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.ExamenPar2.Modelo;


namespace ConsoleApp1.SERVICIO
{
    public class ClaseServicioDetallefactura
    {
        public static bool ValidarDetalleFactura(DetalleFactura detalleFactura, out string mensajeError)
        {
            mensajeError = string.Empty;

            if (detalleFactura.IdFactura <= 0 ||
                detalleFactura.IdProducto <= 0 ||
                string.IsNullOrWhiteSpace(detalleFactura.CantidadProducto) ||
                detalleFactura.Subtotal <= 0)
            {
                mensajeError = "Todos los campos son obligatorios y deben tener valores válidos.";
                return false;
            }

            if (!int.TryParse(detalleFactura.CantidadProducto, out int cantidad) || cantidad <= 0)
            {
                mensajeError = "La cantidad del producto debe ser un número válido y mayor a 0.";
                return false;
            }

            return true;
        }
    }
}
