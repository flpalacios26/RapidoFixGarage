using System;
using RapidoFixGarage.DataAccess;

namespace RapidoFixGarage.Forms
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarKPIs();
            }
        }

        private void CargarKPIs()
        {
            var clientes = new ClienteDAL().ListarTodos();
            var vehiculos = new VehiculoDAL().ListarTodos();
            var ordenes = new OrdenDAL().Abiertas();
            var repuestos = new RepuestoDAL().Buscar("");

            lblClientes.Text = clientes.Count.ToString();
            lblVehiculos.Text = vehiculos.Count.ToString();
            lblOrdenesAbiertas.Text = ordenes.Count.ToString();

            int stockBajo = 0;
            foreach (var r in repuestos)
                if (r.Existencia <= r.StockMinimo) stockBajo++;
            lblStockBajo.Text = stockBajo.ToString();
        }
    }
}