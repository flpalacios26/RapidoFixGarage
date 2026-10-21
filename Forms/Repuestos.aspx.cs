using System;
using System.Web.UI.WebControls;
using RapidoFixGarage.DataAccess;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.Forms
{
    public partial class Repuestos : System.Web.UI.Page
    {
        private readonly RepuestoDAL dal = new RepuestoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) Cargar("");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                dal.Registrar(new Repuesto
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    PrecioUnitario = decimal.Parse(txtPrecio.Text),
                    Existencia = int.Parse(txtExistencia.Text),
                    StockMinimo = int.Parse(txtStockMin.Text)
                });
                txtCodigo.Text = txtNombre.Text = txtPrecio.Text = txtExistencia.Text = txtStockMin.Text = "";
                Cargar("");
            }
            catch (Exception ex) { ClientScript.RegisterStartupScript(GetType(), "err", $"alert('{ex.Message}');", true); }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => Cargar(txtBuscar.Text.Trim());

        protected void gvRepuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());
            try
            {
                if (e.CommandName == "Sumar") dal.ActualizarExistencia(id, 1);
                else if (e.CommandName == "Restar") dal.ActualizarExistencia(id, -1);
                Cargar(txtBuscar.Text.Trim());
            }
            catch (Exception ex) { ClientScript.RegisterStartupScript(GetType(), "err", $"alert('{ex.Message}');", true); }
        }

        private void Cargar(string filtro)
        {
            gvRepuestos.DataSource = dal.Buscar(filtro);
            gvRepuestos.DataBind();
        }
    }
}