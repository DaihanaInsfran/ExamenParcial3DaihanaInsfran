using ConsoleApp1.EXAMENPAR2.MODELO;
using ConsoleApp1.SERVICIO;
using Dapper;
using MySql.Data.MySqlClient;
using ConsoleApp1.ExamenPar2.Modelo;
using System;
using System.Collections.Generic;
using ConsoleApp1.Servicio;

namespace ConsoleApp1.ExamenPar2.Repository
{
    public class ProductoRepository
    {
        private readonly MySqlConnection _connection;

        public ProductoRepository(MySqlConnection connection)
        {
            _connection = connection;
        }

        public List<Producto> GetAllProductos()
        {
            string query = "SELECT * FROM Producto";
            return _connection.Query<Producto>(query).AsList();
        }

        public Producto GetProductoById(int id)
        {
            string query = "SELECT * FROM Producto WHERE id = @Id";
            return _connection.QueryFirstOrDefault<Producto>(query, new { Id = id });
        }

        public void AddProducto(Producto producto)
        {
            if (ClaseServicioProducto.ValidarProducto(producto, out string mensajeError))
            {
                string query = "INSERT INTO Producto (Descripcion, Cantidad_minima, Cantidad_stock, Precio_compra, Precio_venta, Categoria, Marca, Estado) " +
                               "VALUES (@Descripcion, @CantidadMinima, @CantidadStock, @PrecioCompra, @PrecioVenta, @Categoria, @Marca, @Estado)";

                _connection.Execute(query, producto);
                Console.WriteLine("Producto ingresado exitosamente.");
            }
            else
            {
                Console.WriteLine($"Error: {mensajeError}");
            }
        }

        public void UpdateProducto(Producto producto)
        {
            if (ClaseServicioProducto.ValidarProducto(producto, out string mensajeError))
            {
                string query = "UPDATE Producto SET Descripcion = @Descripcion, Cantidad_minima = @CantidadMinima, Cantidad_stock = @CantidadStock, " +
                               "Precio_compra = @PrecioCompra, Precio_venta = @PrecioVenta, Categoria = @Categoria, Marca = @Marca, Estado = @Estado " +
                               "WHERE id = @Id";

                _connection.Execute(query, producto);
                Console.WriteLine("Producto actualizado exitosamente.");
            }
            else
            {
                Console.WriteLine($"Error: {mensajeError}");
            }
        }

        public void DeleteProducto(int id)
        {
            string query = "DELETE FROM Producto WHERE id = @Id";
            _connection.Execute(query, new { Id = id });
            Console.WriteLine("Producto eliminado exitosamente.");
        }
    }
}
