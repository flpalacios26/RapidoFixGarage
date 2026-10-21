namespace RapidoFixGarage.Models
{
    /// <summary>
    /// Representa un vehículo asociado a un cliente.
    /// </summary>
    public class Vehiculo
    {
        public int VehiculoID { get; set; }
        public int ClienteID { get; set; }
        public string Placa { get; set; }
        public string VIN { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int? Anio { get; set; }
        public string Color { get; set; }
        public string ClienteNombre { get; set; }
    }
}