namespace RapidoFixGarage.Models
{
    /// <summary>
    /// Detalle de repuestos usados en una orden.
    /// </summary>
    public class RepuestoUtilizado
    {
        public int DetalleID { get; set; }
        public int OrdenID { get; set; }
        public int RepuestoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public string RepuestoNombre { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}