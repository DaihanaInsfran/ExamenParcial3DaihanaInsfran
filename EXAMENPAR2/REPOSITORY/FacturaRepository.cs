using ConsoleApp1.EXAMENPAR2.MODELO;
using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace ConsoleApp1.EXAMENPAR2.REPOSITORY.DATA.CONFIGURACIONESDB
{
    public class FacturaRepository
    {
        private readonly string _connectionString;

        public FacturaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public FacturaRepository(MySqlConnection connection)
        {
        }

        private IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public IEnumerable<Factura> GetAllFacturas()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return connection.Query<Factura>("SELECT * FROM Factura");
            }
        }

        public IEnumerable<Factura> GetFacturasByClienteId(int clienteId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return connection.Query<Factura>("SELECT * FROM Factura WHERE IdCliente = @IdCliente", new { IdCliente = clienteId });
            }
        }

        public Factura GetFacturaById(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<Factura>("SELECT * FROM Factura WHERE Id = @Id", new { Id = id });
            }
        }

        public void AddFactura(Factura factura)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"INSERT INTO Factura (IdCliente, NroFactura, FechaHora, Total, TotalIva5, TotalIva10, TotalLetras, Sucursal) 
                                 VALUES (@IdCliente, @NroFactura, @FechaHora, @Total, @TotalIva5, @TotalIva10, @TotalLetras, @Sucursal)";
                connection.Execute(query, factura);
            }
        }

        public void UpdateFactura(Factura factura)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = @"UPDATE Factura SET 
                                 IdCliente = @IdCliente, 
                                 NroFactura = @NroFactura, 
                                 FechaHora = @FechaHora, 
                                 Total = @Total, 
                                 TotalIva5 = @TotalIva5, 
                                 TotalIva10 = @TotalIva10, 
                                 TotalLetras = @TotalLetras, 
                                 Sucursal = @Sucursal 
                                 WHERE Id = @Id";
                connection.Execute(query, factura);
            }
        }

        public void DeleteFactura(int id)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Factura WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }
    }
}
