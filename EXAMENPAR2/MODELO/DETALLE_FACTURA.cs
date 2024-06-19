using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System;
using MySql.Data.MySqlClient;

namespace ConsoleApp1.ExamenPar2.Modelo
{
    public class DetalleFactura
    {
        public int Id { get; set; }
        public int IdFactura { get; set; }
        public int IdProducto { get; set; }
        public string CantidadProducto { get; set; }
        public int Subtotal { get; set; }

        public void CrearDetalleFactura(MySqlConnection connection)
        {
            try
            {
                string query = "INSERT INTO Detalle_Factura (id_Factura, id_Producto, Cantidad_producto, Subtotal) " +
                               "VALUES (@IdFactura, @IdProducto, @CantidadProducto, @Subtotal)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdFactura", IdFactura);
                    command.Parameters.AddWithValue("@IdProducto", IdProducto);
                    command.Parameters.AddWithValue("@CantidadProducto", CantidadProducto);
                    command.Parameters.AddWithValue("@Subtotal", Subtotal);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Detalle de factura creado exitosamente.");
                    else
                        Console.WriteLine("Error al crear el detalle de factura.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al crear el detalle de factura: " + ex.Message);
            }
        }

        public void LeerDetalleFactura(MySqlConnection connection, int id)
        {
            try
            {
                string query = "SELECT * FROM Detalle_Factura WHERE id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Id = reader.GetInt32("id");
                            IdFactura = reader.GetInt32("id_Factura");
                            IdProducto = reader.GetInt32("id_Producto");
                            CantidadProducto = reader.GetString("Cantidad_producto");
                            Subtotal = reader.GetInt32("Subtotal");

                            Console.WriteLine($"Detalle de factura encontrado: IdFactura {IdFactura}, IdProducto {IdProducto}");
                        }
                        else
                        {
                            Console.WriteLine("Detalle de factura no encontrado.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al leer el detalle de factura: " + ex.Message);
            }
        }

        public void ActualizarDetalleFactura(MySqlConnection connection)
        {
            try
            {
                string query = "UPDATE Detalle_Factura SET id_Factura = @IdFactura, id_Producto = @IdProducto, Cantidad_producto = @CantidadProducto, " +
                               "Subtotal = @Subtotal WHERE id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdFactura", IdFactura);
                    command.Parameters.AddWithValue("@IdProducto", IdProducto);
                    command.Parameters.AddWithValue("@CantidadProducto", CantidadProducto);
                    command.Parameters.AddWithValue("@Subtotal", Subtotal);
                    command.Parameters.AddWithValue("@Id", Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Detalle de factura actualizado exitosamente.");
                    else
                        Console.WriteLine("Error al actualizar el detalle de factura.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al actualizar el detalle de factura: " + ex.Message);
            }
        }

        public void EliminarDetalleFactura(MySqlConnection connection)
        {
            try
            {
                string query = "DELETE FROM Detalle_Factura WHERE id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        Console.WriteLine("Detalle de factura eliminado exitosamente.");
                    else
                        Console.WriteLine("Error al eliminar el detalle de factura.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el detalle de factura: " + ex.Message);
            }
        }

        public static void ListarDetallesFactura(MySqlConnection connection)
        {
            string query = "SELECT * FROM Detalle_Factura";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"ID: {reader["id"]}, IdFactura: {reader["id_Factura"]}, IdProducto: {reader["id_Producto"]}, CantidadProducto: {reader["Cantidad_producto"]}, Subtotal: {reader["Subtotal"]}");
                }
            }
        }

        public static void BuscarDetalleFacturaPorId(MySqlConnection connection, int idDetalleFactura)
        {
            string query = "SELECT * FROM Detalle_Factura WHERE id = @Id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", idDetalleFactura);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["id"]}, IdFactura: {reader["id_Factura"]}, IdProducto: {reader["id_Producto"]}, CantidadProducto: {reader["Cantidad_producto"]}, Subtotal: {reader["Subtotal"]}");
                    }
                    else
                    {
                        Console.WriteLine("No se encontró ningún detalle de factura con ese ID.");
                    }
                }
            }
        }
    }
}
