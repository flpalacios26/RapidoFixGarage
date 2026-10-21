using System;

namespace RapidoFixGarage.Models
{
    /// <summary>
    /// Orden de reparación asociada a un vehículo.
    /// </summary>
    public class OrdenReparacion
    {
        public int OrdenID { get; set; }
        public string NumeroOrden { get; set; }
        public int VehiculoID { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string MotivoIngreso { get; set; }
        public string Diagnostico { get; set; }
        public string Estado { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string Observaciones { get; set; }

        public string Placa { get; set; }
        public string ClienteNombre { get; set; }
    }
}