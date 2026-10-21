using System;
using RapidoFixGarage.DataAccess;

namespace RapidoFixGarage.Forms
{
    public partial class Reportes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarReportes();
            }
        }

        private void CargarReportes()
        {
            // Cargar todas las órdenes
            gvOrdenes.DataSource = new OrdenDAL().Todas();
            gvOrdenes.DataBind();

            // Cargar todos los repuestos usados
            gvRepuestos.DataSource = new RepuestoDAL().TodosUtilizados();
            gvRepuestos.DataBind();
        }
    }
}