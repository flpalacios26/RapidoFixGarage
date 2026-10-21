using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.DataAccess
{
    public class RepuestoDAL
    {
        public int Registrar(Repuesto r)
        {
            const string sql = @"INSERT INTO Repuestos 
                                    (Codigo, Nombre, Descripcion, PrecioUnitario, Existencia, StockMinimo)
                                 VALUES 
                                    (@Codigo, @Nombre, @Descripcion, @Precio, @Existencia, @StockMin);
                                 SELECT SCOPE_IDENTITY();";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Codigo", r.Codigo);
                cmd.Parameters.AddWithValue("@Nombre", r.Nombre);
                cmd.Parameters.AddWithValue("@Descripcion", (object)r.Descripcion ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Precio", r.PrecioUnitario);
                cmd.Parameters.AddWithValue("@Existencia", r.Existencia);
                cmd.Parameters.AddWithValue("@StockMin", r.StockMinimo);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public List<Repuesto> Buscar(string filtro)
        {
            var lista = new List<Repuesto>();
            const string sql = @"SELECT * FROM Repuestos
                                 WHERE Codigo LIKE @F OR Nombre LIKE @F
                                 ORDER BY Nombre";
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

        public List<Repuesto> Disponibles()
        {
            var lista = new List<Repuesto>();
            const string sql = "SELECT * FROM Repuestos WHERE Existencia > 0 ORDER BY Nombre";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read()) lista.Add(Mapear(rd));
            }
            return lista;
        }

        public void ActualizarExistencia(int repuestoID, int delta)
        {
            const string sql = @"UPDATE Repuestos 
                                 SET Existencia = Existencia + @Delta
                                 WHERE RepuestoID = @ID AND (Existencia + @Delta) >= 0";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@Delta", delta);
                cmd.Parameters.AddWithValue("@ID", repuestoID);
                if (cmd.ExecuteNonQuery() == 0)
                    throw new InvalidOperationException("Stock insuficiente.");
            }
        }

        public void AsociarAReparacion(int ordenID, int repuestoID, int cantidad)
        {
            using (var cn = Conexion.ObtenerConexion())
            using (var tx = cn.BeginTransaction())
            {
                try
                {
                    int existencia;
                    decimal precio;
                    using (var cmd = new SqlCommand(
                        "SELECT Existencia, PrecioUnitario FROM Repuestos WHERE RepuestoID=@ID", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@ID", repuestoID);
                        using (var rd = cmd.ExecuteReader())
                        {
                            if (!rd.Read()) throw new InvalidOperationException("Repuesto no existe.");
                            existencia = (int)rd["Existencia"];
                            precio = (decimal)rd["PrecioUnitario"];
                        }
                    }
                    if (existencia < cantidad)
                        throw new InvalidOperationException("Existencia insuficiente.");

                    using (var cmd = new SqlCommand(
                        @"INSERT INTO RepuestosUtilizados (OrdenID, RepuestoID, Cantidad, PrecioUnitario)
                          VALUES (@OrdenID, @RepuestoID, @Cantidad, @Precio)", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@OrdenID", ordenID);
                        cmd.Parameters.AddWithValue("@RepuestoID", repuestoID);
                        cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                        cmd.Parameters.AddWithValue("@Precio", precio);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand(
                        "UPDATE Repuestos SET Existencia = Existencia - @C WHERE RepuestoID=@ID", cn, tx))
                    {
                        cmd.Parameters.AddWithValue("@C", cantidad);
                        cmd.Parameters.AddWithValue("@ID", repuestoID);
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public List<RepuestoUtilizado> PorOrden(int ordenID)
        {
            var lista = new List<RepuestoUtilizado>();
            const string sql = @"SELECT ru.*, r.Nombre AS RepuestoNombre
                                 FROM RepuestosUtilizados ru
                                 INNER JOIN Repuestos r ON ru.RepuestoID = r.RepuestoID
                                 WHERE ru.OrdenID = @ID";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ID", ordenID);
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        lista.Add(new RepuestoUtilizado
                        {
                            DetalleID = (int)rd["DetalleID"],
                            OrdenID = (int)rd["OrdenID"],
                            RepuestoID = (int)rd["RepuestoID"],
                            Cantidad = (int)rd["Cantidad"],
                            PrecioUnitario = (decimal)rd["PrecioUnitario"],
                            RepuestoNombre = rd["RepuestoNombre"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public List<RepuestoUtilizado> TodosUtilizados()
        {
            var lista = new List<RepuestoUtilizado>();
            const string sql = @"SELECT ru.*, r.Nombre AS RepuestoNombre
                                 FROM RepuestosUtilizados ru
                                 INNER JOIN Repuestos r ON ru.RepuestoID = r.RepuestoID
                                 ORDER BY ru.OrdenID";
            using (var cn = Conexion.ObtenerConexion())
            using (var cmd = new SqlCommand(sql, cn))
            using (var rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    lista.Add(new RepuestoUtilizado
                    {
                        DetalleID = (int)rd["DetalleID"],
                        OrdenID = (int)rd["OrdenID"],
                        RepuestoID = (int)rd["RepuestoID"],
                        Cantidad = (int)rd["Cantidad"],
                        PrecioUnitario = (decimal)rd["PrecioUnitario"],
                        RepuestoNombre = rd["RepuestoNombre"].ToString()
                    });
                }
            }
            return lista;
        }

        private Repuesto Mapear(SqlDataReader rd)
        {
            return new Repuesto
            {
                RepuestoID = (int)rd["RepuestoID"],
                Codigo = rd["Codigo"].ToString(),
                Nombre = rd["Nombre"].ToString(),
                Descripcion = rd["Descripcion"]?.ToString(),
                PrecioUnitario = (decimal)rd["PrecioUnitario"],
                Existencia = (int)rd["Existencia"],
                StockMinimo = (int)rd["StockMinimo"]
            };
        }
    }
}