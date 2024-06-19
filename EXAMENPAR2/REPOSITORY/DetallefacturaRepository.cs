using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.EXAMENPAR2.MODELO;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using ConsoleApp1.SERVICIO;
using ConsoleApp1.ExamenPar2.Modelo;
using System;
using System.Collections.Generic;
using ConsoleApp1.Servicio;

namespace ConsoleApp1.ExamenPar2.Repository
{
    public class DetalleFacturaRepository
    {
        private readonly MySqlConnection _connection;

        public DetalleFacturaRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public List<DetalleFactura> GetAllDetallesFactura()
        {
            string query = "SELECT * FROM Detalle_Factura";
            return _connection.Query<DetalleFactura>(query).AsList();
        }

        public DetalleFactura GetDetalleFacturaById(int id)
        {
            string query = "SELECT * FROM Detalle_Factura WHERE id = @Id";
            return _connection.QueryFirstOrDefault<DetalleFactura>(query, new { Id = id });
        }

        public void AddDetalleFactura(DetalleFactura detalleFactura)
        {
            string query = "INSERT INTO Detalle_Factura (id_Factura, id_Producto, Cantidad_producto, Subtotal) " +
                           "VALUES (@IdFactura, @IdProducto, @CantidadProducto, @Subtotal)";

            _connection.Execute(query, detalleFactura);
            Console.WriteLine("Detalle de factura ingresado exitosamente.");
        }

        public void UpdateDetalleFactura(DetalleFactura detalleFactura)
        {
            string query = "UPDATE Detalle_Factura SET id_Factura = @IdFactura, id_Producto = @IdProducto, Cantidad_producto = @CantidadProducto, " +
                           "Subtotal = @Subtotal WHERE id = @Id";

            _connection.Execute(query, detalleFactura);
            Console.WriteLine("Detalle de factura actualizado exitosamente.");
        }

        public void DeleteDetalleFactura(int id)
        {
            string query = "DELETE FROM Detalle_Factura WHERE id = @Id";
            _connection.Execute(query, new { Id = id });
            Console.WriteLine("Detalle de factura eliminado exitosamente.");
        }
    }
}
