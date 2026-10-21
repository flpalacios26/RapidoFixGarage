using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.DataAccess
{
    public class OrdenDAL
    {
        public int Crear(OrdenReparacion o)
        {
            const string sql = @"INSERT INTO OrdenesReparacion 
                                    (NumeroOrden, VehiculoID, MotivoIngreso, Diagnostico, Estado)
                                 VALUES 
                                    (@Numero, @VehiculoID, @Motivo, @Diagnostico, 'Abierta');
                                 SELECT SCOPE_IDENTITY();";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Numero", GenerarNumero());
                cmd.Parameters.AddWithValue("@VehiculoID", o.VehiculoID);
                cmd.Parameters.AddWithValue("@Motivo", o.MotivoIngreso);
                cmd.Parameters.AddWithValue("@Diagnostico", (object)o.Diagnostico ?? DBNull.Value);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<OrdenReparacion> Abiertas() => PorEstado("Abierta");
        public List<OrdenReparacion> Cerradas() => PorEstado("Cerrada");

        public void Cerrar(int ordenID, string observaciones)
        {
            const string sql = @"UPDATE OrdenesReparacion
                                 SET Estado='Cerrada', FechaCierre=GETDATE(), Observaciones=@Obs
                                 WHERE OrdenID=@ID";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Obs", (object)observaciones ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", ordenID);
                cmd.ExecuteNonQuery();
            }
        }

        public List<OrdenReparacion> PorVehiculo(int vehiculoID)
        {
            var lista = new List<OrdenReparacion>();
            const string sql = @"SELECT o.*, v.Placa, c.Nombre + ' ' + c.Apellido AS ClienteNombre
                                 FROM OrdenesReparacion o
                                 INNER JOIN Vehiculos v ON o.VehiculoID = v.VehiculoID
                                 INNER JOIN Clientes c ON v.ClienteID = c.ClienteID
                                 WHERE o.VehiculoID = @ID
                                 ORDER BY o.FechaIngreso DESC";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ID", vehiculoID);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) lista.Add(Mapear(rd));
                }
            }
            return lista;
        }

        public List<OrdenReparacion> Todas()
        {
            var lista = new List<OrdenReparacion>();
            const string sql = @"SELECT o.*, v.Placa, c.Nombre + ' ' + c.Apellido AS ClienteNombre
                                 FROM OrdenesReparacion o
                                 INNER JOIN Vehiculos v ON o.VehiculoID = v.VehiculoID
                                 INNER JOIN Clientes c ON v.ClienteID = c.ClienteID
                                 ORDER BY o.FechaIngreso DESC";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) lista.Add(Mapear(rd));
            }
            return lista;
        }

        private List<OrdenReparacion> PorEstado(string estado)
        {
            var lista = new List<OrdenReparacion>();
            const string sql = @"SELECT o.*, v.Placa, c.Nombre + ' ' + c.Apellido AS ClienteNombre
                                 FROM OrdenesReparacion o
                                 INNER JOIN Vehiculos v ON o.VehiculoID = v.VehiculoID
                                 INNER JOIN Clientes c ON v.ClienteID = c.ClienteID
                                 WHERE o.Estado = @Estado
                                 ORDER BY o.FechaIngreso DESC";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Estado", estado);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read()) lista.Add(Mapear(rd));
                }
            }
            return lista;
        }

        private OrdenReparacion Mapear(SqlDataReader rd)
        {
            return new OrdenReparacion
            {
                OrdenID = (int)rd["OrdenID"],
                NumeroOrden = rd["NumeroOrden"].ToString(),
                VehiculoID = (int)rd["VehiculoID"],
                FechaIngreso = (DateTime)rd["FechaIngreso"],
                MotivoIngreso = rd["MotivoIngreso"].ToString(),
                Diagnostico = rd["Diagnostico"]?.ToString(),
                Estado = rd["Estado"].ToString(),
                FechaCierre = rd["FechaCierre"] == DBNull.Value ? (DateTime?)null : (DateTime)rd["FechaCierre"],
                Observaciones = rd["Observaciones"]?.ToString(),
                Placa = rd["Placa"]?.ToString(),
                ClienteNombre = rd["ClienteNombre"]?.ToString()
            };
        }

        private string GenerarNumero() => $"ORD-{DateTime.Now:yyyyMMddHHmmss}";
    }
}