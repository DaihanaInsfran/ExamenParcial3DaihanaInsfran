using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.SERVICIO;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Dapper;
using ConsoleApp1.Servicio;



namespace ConsoleApp1.ExamenPar2.Modelo
{
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int CantidadMinima { get; set; }
        public int CantidadStock { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string Estado { get; set; }

        public void CrearProducto(MySqlConnection connection)
        {
            try
            {
                string query = "INSERT INTO Producto (Descripcion, Cantidad_minima, Cantidad_stock, Precio_compra, Precio_venta, Categoria, Marca, Estado) " +
                               "VALUES (@Descripcion, @CantidadMinima, @CantidadStock, @PrecioCompra, @PrecioVenta, @Categoria, @Marca, @Estado)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", Descripcion);
                    command.Parameters.AddWithValue("@CantidadMinima", CantidadMinima);
                    command.Parameters.AddWithValue("@CantidadStock", CantidadStock);
                    command.Parameters.AddWithValue("@PrecioCompra", PrecioCompra);
                    command.Parameters.AddWithValue("@PrecioVenta", PrecioVenta);
                    command.Parameters.AddWithValue("@Categoria", Categoria);
                    command.Parameters.AddWithValue("@Marca", Marca);
                    command.Parameters.AddWithValue("@Estado", Estado);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Producto creado exitosamente.");
                    else
                        Console.WriteLine("Error al crear el producto.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el producto: " + ex.Message);
            }
        }

        public void LeerProducto(MySqlConnection connection, int id)
        {
            try
            {
                string query = "SELECT * FROM Producto WHERE id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Id = reader.GetInt32("id");
                            Descripcion = reader.GetString("Descripcion");
                            CantidadMinima = reader.GetInt32("Cantidad_minima");
                            CantidadStock = reader.GetInt32("Cantidad_stock");
                            PrecioCompra = reader.GetDecimal("Precio_compra");
                            PrecioVenta = reader.GetDecimal("Precio_venta");
                            Categoria = reader.GetString("Categoria");
                            Marca = reader.GetString("Marca");
                            Estado = reader.GetString("Estado");

                            Console.WriteLine($"Producto encontrado: {Descripcion}");
                        }
                        else
                        {
                            Console.WriteLine("Producto no encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el producto: " + ex.Message);
            }
        }

        public void ActualizarProducto(MySqlConnection connection)
        {
            try
            {
                string query = "UPDATE Producto SET Descripcion = @Descripcion, Cantidad_minima = @CantidadMinima, Cantidad_stock = @CantidadStock, " +
                               "Precio_compra = @PrecioCompra, Precio_venta = @PrecioVenta, Categoria = @Categoria, Marca = @Marca, Estado = @Estado " +
                               "WHERE id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", Descripcion);
                    command.Parameters.AddWithValue("@CantidadMinima", CantidadMinima);
                    command.Parameters.AddWithValue("@CantidadStock", CantidadStock);
                    command.Parameters.AddWithValue("@PrecioCompra", PrecioCompra);
                    command.Parameters.AddWithValue("@PrecioVenta", PrecioVenta);
                    command.Parameters.AddWithValue("@Categoria", Categoria);
                    command.Parameters.AddWithValue("@Marca", Marca);
                    command.Parameters.AddWithValue("@Estado", Estado);
                    command.Parameters.AddWithValue("@Id", Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Producto actualizado exitosamente.");
                    else
                        Console.WriteLine("Error al actualizar el producto.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el producto: " + ex.Message);
            }
        }

        public void EliminarProducto(MySqlConnection connection)
        {
            try
            {
                string query = "DELETE FROM Producto WHERE id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Producto eliminado exitosamente.");
                    else
                        Console.WriteLine("Error al eliminar el producto.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el producto: " + ex.Message);
            }
        }

        public static void ListarProductos(MySqlConnection connection)
        {
            string query = "SELECT * FROM Producto";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, Descripción: {reader["Descripcion"]}, Stock: {reader["Cantidad_stock"]}");
                }
            }
        }

        public static void BuscarProductoPorId(MySqlConnection connection, int idProducto)
        {
            string query = "SELECT * FROM Producto WHERE id = @Id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", idProducto);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, Descripción: {reader["Descripcion"]}, Stock: {reader["Cantidad_stock"]}");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún producto con ese ID.");
                    }
                }
            }
        }

        public static void IngresarNuevoProducto(MySqlConnection connection)
        {
            Console.Write("Ingrese la descripción del nuevo producto: ");
            string descripcion = Console.ReadLine();
            Console.Write("Ingrese la cantidad mínima del nuevo producto: ");
            int cantidadMinima = int.Parse(Console.ReadLine());
            Console.Write("Ingrese la cantidad en stock del nuevo producto: ");
            int cantidadStock = int.Parse(Console.ReadLine());
            Console.Write("Ingrese el precio de compra del nuevo producto: ");
            decimal precioCompra = decimal.Parse(Console.ReadLine());
            Console.Write("Ingrese el precio de venta del nuevo producto: ");
            decimal precioVenta = decimal.Parse(Console.ReadLine());
            Console.Write("Ingrese la categoría del nuevo producto: ");
            string categoria = Console.ReadLine();
            Console.Write("Ingrese la marca del nuevo producto: ");
            string marca = Console.ReadLine();
            Console.Write("Ingrese el estado del nuevo producto: ");
            string estado = Console.ReadLine();

            Producto nuevoProducto = new Producto
            {
                Descripcion = descripcion,
                CantidadMinima = cantidadMinima,
                CantidadStock = cantidadStock,
                PrecioCompra = precioCompra,
                PrecioVenta = precioVenta,
                Categoria = categoria,
                Marca = marca,
                Estado = estado
            };

            if (ClaseServicioProducto.ValidarProducto(nuevoProducto, out string mensajeError))
            {
                string query = "INSERT INTO Producto (Descripcion, Cantidad_minima, Cantidad_stock, Precio_compra, Precio_venta, Categoria, Marca, Estado) " +
                               "VALUES (@Descripcion, @CantidadMinima, @CantidadStock, @PrecioCompra, @PrecioVenta, @Categoria, @Marca, @Estado)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Descripcion", descripcion);
                    command.Parameters.AddWithValue("@CantidadMinima", cantidadMinima);
                    command.Parameters.AddWithValue("@CantidadStock", cantidadStock);
                    command.Parameters.AddWithValue("@PrecioCompra", precioCompra);
                    command.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                    command.Parameters.AddWithValue("@Categoria", categoria);
                    command.Parameters.AddWithValue("@Marca", marca);
                    command.Parameters.AddWithValue("@Estado", estado);

                    command.ExecuteNonQuery();
                    Console.WriteLine("Producto ingresado exitosamente.");
                }
            }
            else
            {
                Console.WriteLine($"Error: {mensajeError}");
            }
        }
    }
}
