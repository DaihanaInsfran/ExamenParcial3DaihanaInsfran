using System;
using MySql.Data.MySqlClient;
using ConsoleApp1.ExamenPar2.Modelo;
using ConsoleApp1.ExamenPar2.Repository;
using ConsoleApp1.EXAMENPAR2.MODELO;
using ConsoleApp1.EXAMENPAR2.REPOSITORY.DATA.CONFIGURACIONESDB;
using ConsoleApp1.SERVICIO;
using ConsoleApp1.EXAMENPAR2.REPOSITORY;
using MongoDB.Driver.Core.Configuration;

string connectionString = "server=localhost;port=3306;database=Basede_datos;user=root;password=1234;";
ConexionDB conexionDB = new ConexionDB(connectionString);
MySqlConnection connection = conexionDB.EstablecerConexion();

ClienteRepository clienteRepository = new ClienteRepository(connection);
DetalleFacturaRepository detalleFacturaRepository = new DetalleFacturaRepository(connection);
FacturaRepository facturaRepository = new FacturaRepository(connectionString);
SucursalRepository sucursalRepository = new SucursalRepository(connectionString);
ProductoRepository productoRepository = new ProductoRepository(connection);

bool continuar = true;
while (continuar)
{
    Console.WriteLine("Menú:");
    Console.WriteLine("1. Listar clientes");
    Console.WriteLine("2. Buscar cliente por ID");
    Console.WriteLine("3. Ingresar nuevo cliente");
    Console.WriteLine("4. Actualizar cliente");
    Console.WriteLine("5. Eliminar cliente");
    Console.WriteLine("6. Listar facturas");
    Console.WriteLine("7. Buscar facturas por ID de cliente");
    Console.WriteLine("8. Ingresar nueva factura");
    Console.WriteLine("9. Actualizar factura");
    Console.WriteLine("10. Eliminar factura");
    Console.WriteLine("11. Listar productos");
    Console.WriteLine("12. Buscar producto por ID");
    Console.WriteLine("13. Ingresar nuevo producto");
    Console.WriteLine("14. Actualizar producto");
    Console.WriteLine("15. Eliminar producto");
    Console.WriteLine("16. Listar detalles de factura");
    Console.WriteLine("17. Buscar detalle de factura por ID");
    Console.WriteLine("18. Ingresar nuevo detalle de factura");
    Console.WriteLine("19. Actualizar detalle de factura");
    Console.WriteLine("20. Eliminar detalle de factura");
    Console.WriteLine("0. Salir");
    Console.Write("Ingrese la opción deseada: ");

    string opcion = Console.ReadLine();

    try
    {
        switch (opcion)
        {
            case "1":
                // Listar clientes
                foreach (var cliente in clienteRepository.GetAllClientes())
                {
                    Console.WriteLine($"ID: {cliente.Id}, Nombre: {cliente.Nombre}, Apellido: {cliente.Apellido}");
                }
                break;
            case "2":
                // Buscar cliente por ID
                Console.Write("Ingrese el ID del cliente a buscar: ");
                int idClienteBuscar = Convert.ToInt32(Console.ReadLine());
                var clienteEncontrado = clienteRepository.GetClienteById(idClienteBuscar);
                if (clienteEncontrado != null)
                {
                    Console.WriteLine($"Cliente encontrado - ID: {clienteEncontrado.Id}, Nombre: {clienteEncontrado.Nombre}, Apellido: {clienteEncontrado.Apellido}");
                }
                else
                {
                    Console.WriteLine("Cliente no encontrado.");
                }
                break;
            case "3":
                // Ingresar nuevo cliente
                Console.Write("Ingrese el nombre del cliente: ");
                string nombre = Console.ReadLine();
                Console.Write("Ingrese el apellido del cliente: ");
                string apellido = Console.ReadLine();
                Console.Write("Ingrese el ID del banco: ");
                int idBanco = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese el documento del cliente: ");
                string documento = Console.ReadLine();
                Console.Write("Ingrese la dirección del cliente: ");
                string direccion = Console.ReadLine();
                Console.Write("Ingrese el correo electrónico del cliente: ");
                string correo = Console.ReadLine();
                Console.Write("Ingrese el número de celular del cliente: ");
                string celular = Console.ReadLine();
                Console.Write("Ingrese el estado del cliente ('Activo' o 'Inactivo'): ");
                string estadoCliente = Console.ReadLine();
                Cliente nuevoCliente = new Cliente
                {
                    Id_banco = idBanco,
                    Nombre = nombre,
                    Apellido = apellido,
                    Documento = documento,
                    Direccion = direccion,
                    Correo = correo,
                    Celular = celular,
                    Estado = estadoCliente
                };
                clienteRepository.AddCliente(nuevoCliente);
                Console.WriteLine("Cliente ingresado correctamente.");
                break;
            case "4":
                // Actualizar cliente
                Console.Write("Ingrese el ID del cliente a actualizar: ");
                int idClienteActualizar = Convert.ToInt32(Console.ReadLine());
                var clienteActualizar = clienteRepository.GetClienteById(idClienteActualizar);
                if (clienteActualizar != null)
                {
                    Console.Write("Ingrese el nuevo nombre del cliente: ");
                    string nuevoNombre = Console.ReadLine();
                    Console.Write("Ingrese el nuevo apellido del cliente: ");
                    string nuevoApellido = Console.ReadLine();
                    // Continuar con la actualización de otros campos si es necesario
                    clienteActualizar.Nombre = nuevoNombre;
                    clienteActualizar.Apellido = nuevoApellido;
                    clienteRepository.UpdateCliente(clienteActualizar);
                    Console.WriteLine("Cliente actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("Cliente no encontrado.");
                }
                break;
            case "5":
                // Eliminar cliente
                Console.Write("Ingrese el ID del cliente a eliminar: ");
                int idClienteEliminar = Convert.ToInt32(Console.ReadLine());
                clienteRepository.DeleteCliente(idClienteEliminar);
                Console.WriteLine("Cliente eliminado correctamente.");
                break;
            case "6":
                foreach (var factura in facturaRepository.GetAllFacturas())
                {
                    Console.WriteLine($"ID: {factura.Id}, ID Cliente: {factura.IdCliente}, Número de Factura: {factura.NroFactura}, Total: {factura.Total}");
                }
                break;
            case "7":
                Console.Write("Ingrese el ID del cliente para buscar sus facturas: ");
                int idClienteFacturas = Convert.ToInt32(Console.ReadLine());
                var facturasCliente = facturaRepository.GetFacturasByClienteId(idClienteFacturas);
                foreach (var factura in facturasCliente)
                {
                    Console.WriteLine($"ID: {factura.Id}, ID Cliente: {factura.IdCliente}, Número de Factura: {factura.NroFactura}, Total: {factura.Total}");
                }
                break;
            case "8":
                // Ingresar nueva factura
                Console.Write("Ingrese el ID del cliente para la nueva factura: ");
                int idClienteFactura = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese el número de factura: ");
                string nroFactura = Console.ReadLine();
                Console.Write("Ingrese el total de la factura: ");
                decimal totalFactura = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Ingrese el total de IVA al 5%: ");
                decimal totalIva5 = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Ingrese el total de IVA al 10%: ");
                decimal totalIva10 = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Ingrese el total en letras: ");
                string totalLetras = Console.ReadLine();
                Console.Write("Ingrese la sucursal: ");
                string sucursal = Console.ReadLine();

                Factura nuevaFactura = new Factura
                {
                    IdCliente = idClienteFactura,
                    NroFactura = nroFactura,
                    FechaHora = DateTime.Now,
                    Total = totalFactura,
                    TotalIva5 = totalIva5,
                    TotalIva10 = totalIva10,
                    TotalLetras = totalLetras,
                    Sucursal = sucursal
                };
                facturaRepository.AddFactura(nuevaFactura);
                Console.WriteLine("Factura ingresada correctamente.");
                break;
            case "9":
                // Actualizar factura
                Console.Write("Ingrese el ID de la factura a actualizar: ");
                int idFacturaActualizar;
                if (!int.TryParse(Console.ReadLine(), out idFacturaActualizar))
                {
                    Console.WriteLine("Entrada no válida para el ID de la factura.");
                    break;
                }
                var facturaActualizar = facturaRepository.GetFacturaById(idFacturaActualizar);
                if (facturaActualizar != null)
                {
                    Console.Write("Ingrese el nuevo número de factura: ");
                    string nuevoNroFactura = Console.ReadLine();
                    facturaActualizar.NroFactura = nuevoNroFactura;
                    facturaRepository.UpdateFactura(facturaActualizar);
                    Console.WriteLine("Factura actualizada correctamente.");
                }
                else
                {
                    Console.WriteLine("Factura no encontrada.");
                }
                break;
            case "10":
                // Eliminar factura
                Console.Write("Ingrese el ID de la factura a eliminar: ");
                int idFacturaEliminar;
                if (!int.TryParse(Console.ReadLine(), out idFacturaEliminar))
                {
                    Console.WriteLine("Entrada no válida para el ID de la factura.");
                    break;
                }
                facturaRepository.DeleteFactura(idFacturaEliminar);
                Console.WriteLine("Factura eliminada correctamente.");
                break;
            case "11":
                // Listar productos
                foreach (var producto in productoRepository.GetAllProductos())
                {
                    Console.WriteLine($"ID: {producto.Id}, Descripción: {producto.Descripcion}, " +
                                      $"Stock: {producto.CantidadStock}, " +
                                      $"Categoría: {producto.Categoria}, " +
                                      $"Marca: {producto.Marca}, " +
                                      $"Estado: {producto.Estado}");
                }
                break;

            case "12":
                // Buscar producto por ID
                Console.Write("Ingrese el ID del producto a buscar: ");
                int idProductoBuscar = Convert.ToInt32(Console.ReadLine());
                var productoEncontrado = productoRepository.GetProductoById(idProductoBuscar);
                if (productoEncontrado != null)
                {
                    Console.WriteLine($"Producto encontrado - ID: {productoEncontrado.Id}, Descripción: {productoEncontrado.Descripcion}, Stock: {productoEncontrado.CantidadStock}");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
                break;

            case "13":
                // Ingresar nuevo producto
                Console.Write("Ingrese la descripción del producto: ");
                string descripcion = Console.ReadLine();
                Console.Write("Ingrese la cantidad mínima del producto: ");
                int cantidadMinima = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese la cantidad en stock del producto: ");
                int cantidadStock = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese el precio de compra del producto: ");
                decimal precioCompra = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Ingrese el precio de venta del producto: ");
                decimal precioVenta = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Ingrese la categoría del producto: ");
                string categoria = Console.ReadLine();
                Console.Write("Ingrese la marca del producto: ");
                string marca = Console.ReadLine();
                Console.Write("Ingrese el estado del producto ('Activo' o 'Inactivo'): ");
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
                productoRepository.AddProducto(nuevoProducto);
                Console.WriteLine("Producto ingresado correctamente.");
                break;

            case "14":
                // Actualizar producto
                Console.Write("Ingrese el ID del producto a actualizar: ");
                int idProductoActualizar = Convert.ToInt32(Console.ReadLine());
                var productoActualizar = productoRepository.GetProductoById(idProductoActualizar);
                if (productoActualizar != null)
                {
                    Console.Write("Ingrese la nueva descripción del producto: ");
                    string nuevaDescripcion = Console.ReadLine();
                    Console.Write("Ingrese la nueva cantidad mínima del producto: ");
                    int nuevaCantidadMinima = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ingrese la nueva cantidad en stock del producto: ");
                    int nuevaCantidadStock = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ingrese el nuevo precio de compra del producto: ");
                    decimal nuevoPrecioCompra = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Ingrese el nuevo precio de venta del producto: ");
                    decimal nuevoPrecioVenta = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Ingrese la nueva categoría del producto: ");
                    string nuevaCategoria = Console.ReadLine();
                    Console.Write("Ingrese la nueva marca del producto: ");
                    string nuevaMarca = Console.ReadLine();
                    Console.Write("Ingrese el nuevo estado del producto ('Activo' o 'Inactivo'): ");
                    string nuevoEstado = Console.ReadLine();

                    productoActualizar.Descripcion = nuevaDescripcion;
                    productoActualizar.CantidadMinima = nuevaCantidadMinima;
                    productoActualizar.CantidadStock = nuevaCantidadStock;
                    productoActualizar.PrecioCompra = nuevoPrecioCompra;
                    productoActualizar.PrecioVenta = nuevoPrecioVenta;
                    productoActualizar.Categoria = nuevaCategoria;
                    productoActualizar.Marca = nuevaMarca;
                    productoActualizar.Estado = nuevoEstado;

                    productoRepository.UpdateProducto(productoActualizar);
                    Console.WriteLine("Producto actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("Producto no encontrado.");
                }
                break;

            case "15":
                // Eliminar producto
                Console.Write("Ingrese el ID del producto a eliminar: ");
                int idProductoEliminar = Convert.ToInt32(Console.ReadLine());
                productoRepository.DeleteProducto(idProductoEliminar);
                Console.WriteLine("Producto eliminado correctamente.");
                break;

            case "16":
                // Listar detalles de factura
                Console.WriteLine("Detalles de factura disponibles:");
                DetalleFactura.ListarDetallesFactura(connection);
                break;

            case "17":
                // Buscar detalle de factura por ID
                Console.Write("Ingrese el ID del detalle de factura a buscar: ");
                int idDetalleFacturaBuscar = Convert.ToInt32(Console.ReadLine());
                DetalleFactura.BuscarDetalleFacturaPorId(connection, idDetalleFacturaBuscar);
                break;

            case "18":
                // Ingresar nuevo detalle de factura
                DetalleFactura nuevoDetalleFactura = new DetalleFactura();
                Console.Write("Ingrese el ID de la factura: ");
                nuevoDetalleFactura.IdFactura = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese el ID del producto: ");
                nuevoDetalleFactura.IdProducto = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese la cantidad del producto: ");
                nuevoDetalleFactura.CantidadProducto = Console.ReadLine(); 
                Console.Write("Ingrese el subtotal: ");
                nuevoDetalleFactura.Subtotal = Convert.ToInt32(Console.ReadLine());

                
                detalleFacturaRepository.AddDetalleFactura(nuevoDetalleFactura);
                Console.WriteLine("Detalle de factura ingresado correctamente.");
                break;

            case "19":
                // Actualizar detalle de factura
                Console.Write("Ingrese el ID del detalle de factura a actualizar: ");
                int idDetalleFacturaActualizar = Convert.ToInt32(Console.ReadLine());
                DetalleFactura detalleFacturaActualizar = new DetalleFactura();
                detalleFacturaActualizar.Id = idDetalleFacturaActualizar;

                Console.Write("Ingrese el nuevo ID de la factura: ");
                detalleFacturaActualizar.IdFactura = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese el nuevo ID del producto: ");
                detalleFacturaActualizar.IdProducto = Convert.ToInt32(Console.ReadLine());
                Console.Write("Ingrese la nueva cantidad del producto: ");
                detalleFacturaActualizar.CantidadProducto = Console.ReadLine(); 
                Console.Write("Ingrese el nuevo subtotal: ");
                detalleFacturaActualizar.Subtotal = Convert.ToInt32(Console.ReadLine());

                DetalleFacturaRepository detalleFacturaRepoActualizar = new DetalleFacturaRepository(connection);
                detalleFacturaRepoActualizar.UpdateDetalleFactura(detalleFacturaActualizar);
                Console.WriteLine("Detalle de factura actualizado correctamente.");
                break;

            case "20":
    // Eliminar detalle de factura
    Console.Write("Ingrese el ID del detalle de factura a eliminar: ");
    int idDetalleFacturaEliminar = Convert.ToInt32(Console.ReadLine());
    DetalleFacturaRepository detalleFacturaRepoEliminar = new DetalleFacturaRepository(connection);
    detalleFacturaRepoEliminar.DeleteDetalleFactura(idDetalleFacturaEliminar);
    Console.WriteLine("Detalle de factura eliminado correctamente.");
    break;

            case "0":
                // Salir
                continuar = false;
                break;
            default:
                Console.WriteLine("Opción no válida. Intente nuevamente.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ocurrió un error: " + ex.Message);
    }
}
