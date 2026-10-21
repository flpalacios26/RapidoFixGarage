using System.Configuration;
using System.Data.SqlClient;

namespace RapidoFixGarage.DataAccess
{
    public static class Conexion
    {
        // Lee la cadena de conexión llamada "RapidoFixGarage" del Web.config
        private static readonly string cadena =
            ConfigurationManager.ConnectionStrings["RapidoFixGarage"].ConnectionString;

        public static SqlConnection ObtenerConexion()
        {
            var cn = new SqlConnection(cadena);
            cn.Open();
            return cn;
        }
    }
}