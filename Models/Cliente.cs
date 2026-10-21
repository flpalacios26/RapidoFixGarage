using System;

namespace RapidoFixGarage.Models
{
    /// <summary>
    /// Registro de clientes
    /// </summary>
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}