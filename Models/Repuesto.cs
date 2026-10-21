namespace RapidoFixGarage.Models
{
    /// <summary>
    /// Repuesto del inventario.
    /// </summary>
    public class Repuesto
    {
        public int RepuestoID { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Existencia { get; set; }
        public int StockMinimo { get; set; }

        public bool Disponible => Existencia > 0;
    }
}