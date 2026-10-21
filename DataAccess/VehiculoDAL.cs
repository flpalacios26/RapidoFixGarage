using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.DataAccess
{
    public class VehiculoDAL
    {
        public int Registrar(Vehiculo v)
        {
            const string sql = @"INSERT INTO Vehiculos 
                                    (ClienteID, Placa, VIN, Marca, Modelo, Anio, Color)
                                 VALUES 
                                    (@ClienteID, @Placa, @VIN, @Marca, @Modelo, @Anio, @Color);
                                 SELECT SCOPE_IDENTITY();";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ClienteID", v.ClienteID);
                cmd.Parameters.AddWithValue("@Placa", v.Placa);
                cmd.Parameters.AddWithValue("@VIN", (object)v.VIN ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Marca", v.Marca);
                cmd.Parameters.AddWithValue("@Modelo", v.Modelo);
                cmd.Parameters.AddWithValue("@Anio", (object)v.Anio ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Color", (object)v.Color ?? DBNull.Value);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Vehiculo> Buscar(string filtro)
        {
            var lista = new List<Vehiculo>();
            const string sql = @"SELECT v.*, c.Nombre + ' ' + c.Apellido AS ClienteNombre
                                 FROM Vehiculos v
                                 INNER JOIN Clientes c ON v.ClienteID = c.ClienteID
                                 WHERE v.Placa LIKE @F OR v.VIN LIKE @F
                                 ORDER BY v.Placa";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@F", "%" + (filtro ?? "") + "%");
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) lista.Add(Mapear(rd));
                }
            }
            return lista;
        }

        public List<Vehiculo> PorCliente(int clienteID)
        {
            var lista = new List<Vehiculo>();
            const string sql = @"SELECT v.*, c.Nombre + ' ' + c.Apellido AS ClienteNombre
                                 FROM Vehiculos v
                                 INNER JOIN Clientes c ON v.ClienteID = c.ClienteID
                                 WHERE v.ClienteID = @ID ORDER BY v.Placa";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ID", clienteID);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) lista.Add(Mapear(rd));
                }
            }
            return lista;
        }

        public List<Vehiculo> ListarTodos() => Buscar("");

        private Vehiculo Mapear(SqlDataReader rd)
        {
            return new Vehiculo
            {
                VehiculoID = (int)rd["VehiculoID"],
                ClienteID = (int)rd["ClienteID"],
                Placa = rd["Placa"].ToString(),
                VIN = rd["VIN"]?.ToString(),
                Marca = rd["Marca"].ToString(),
                Modelo = rd["Modelo"].ToString(),
                Anio = rd["Anio"] == DBNull.Value ? (int?)null : (int)rd["Anio"],
                Color = rd["Color"]?.ToString(),
                ClienteNombre = rd["ClienteNombre"]?.ToString()
            };
        }
    }
}