using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.DataAccess
{
    public class ClienteDAL
    {
        public int Registrar(Cliente c)
        {
            const string sql = @"INSERT INTO Clientes (Nombre, Apellido, Telefono, Email, Direccion)
                                 VALUES (@Nombre, @Apellido, @Telefono, @Email, @Direccion);
                                 SELECT SCOPE_IDENTITY();";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Nombre", c.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", c.Apellido);
                cmd.Parameters.AddWithValue("@Telefono", (object)c.Telefono ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)c.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Direccion", (object)c.Direccion ?? DBNull.Value);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Cliente> Buscar(string filtro)
        {
            var lista = new List<Cliente>();
            const string sql = @"SELECT ClienteID, Nombre, Apellido, Telefono, Email, Direccion, FechaRegistro
                                 FROM Clientes
                                 WHERE Nombre LIKE @F OR Apellido LIKE @F OR Telefono LIKE @F
                                 ORDER BY Nombre";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@F", "%" + (filtro ?? "") + "%");
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        lista.Add(new Cliente
                        {
                            ClienteID = (int)rd["ClienteID"],
                            Nombre = rd["Nombre"].ToString(),
                            Apellido = rd["Apellido"].ToString(),
                            Telefono = rd["Telefono"]?.ToString(),
                            Email = rd["Email"]?.ToString(),
                            Direccion = rd["Direccion"]?.ToString(),
                            FechaRegistro = (DateTime)rd["FechaRegistro"]
                        });
                    }
                }
            }
            return lista;
        }

        public List<Cliente> ListarTodos() => Buscar("");
    }
}