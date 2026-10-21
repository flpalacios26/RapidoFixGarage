using System;
using System.Web.UI.WebControls;
using RapidoFixGarage.DataAccess;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.Forms
{
    public partial class Ordenes : System.Web.UI.Page
    {
        private readonly OrdenDAL dal = new OrdenDAL();
        private readonly VehiculoDAL vehDal = new VehiculoDAL();
        private readonly RepuestoDAL repDal = new RepuestoDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar combo de vehículos para crear orden
                ddlVehiculos.DataSource = vehDal.ListarTodos();
                ddlVehiculos.DataTextField = "Placa";
                ddlVehiculos.DataValueField = "VehiculoID";
                ddlVehiculos.DataBind();

                CargarGrids();
                CargarCombos();
            }
        }

        // --- MÉTODOS PARA CREAR Y CERRAR ÓRDENES ---

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                dal.Crear(new OrdenReparacion
                {
                    VehiculoID = int.Parse(ddlVehiculos.SelectedValue),
                    MotivoIngreso = txtMotivo.Text.Trim(),
                    Diagnostico = txtDiagnostico.Text.Trim()
                });
                txtMotivo.Text = txtDiagnostico.Text = "";
                CargarGrids();
                CargarCombos();
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        protected void gvAbiertas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Cerrar")
            {
                int id = int.Parse(e.CommandArgument.ToString());
                dal.Cerrar(id, "Cerrada desde el sistema.");
                CargarGrids();
                CargarCombos();
            }
        }

        private void CargarGrids()
        {
            gvAbiertas.DataSource = dal.Abiertas();
            gvAbiertas.DataBind();
            gvCerradas.DataSource = dal.Cerradas();
            gvCerradas.DataBind();
        }

        // --- MÉTODOS PARA ASOCIAR REPUESTOS ---

        private void CargarCombos()
        {
            // Combo de órdenes abiertas
            ddlOrdenesAsociar.DataSource = dal.Abiertas();
            ddlOrdenesAsociar.DataTextField = "NumeroOrden";
            ddlOrdenesAsociar.DataValueField = "OrdenID";
            ddlOrdenesAsociar.DataBind();

            // Combo de repuestos disponibles
            ddlRepuestosAsociar.DataSource = repDal.Disponibles();
            ddlRepuestosAsociar.DataTextField = "Nombre";
            ddlRepuestosAsociar.DataValueField = "RepuestoID";
            ddlRepuestosAsociar.DataBind();
        }

        protected void btnAsociar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlOrdenesAsociar.Items.Count == 0 || ddlRepuestosAsociar.Items.Count == 0)
                    throw new Exception("Debe haber órdenes abiertas y repuestos disponibles.");

                int ordenId = int.Parse(ddlOrdenesAsociar.SelectedValue);
                int repuestoId = int.Parse(ddlRepuestosAsociar.SelectedValue);
                int cantidad = int.Parse(txtCantidad.Text);

                repDal.AsociarAReparacion(ordenId, repuestoId, cantidad);

                ClientScript.RegisterStartupScript(GetType(), "ok", "alert('Repuesto asociado y stock descontado con éxito.');", true);
                CargarCombos();
                CargarGrids();
            }
            catch (Exception ex) { MostrarError(ex.Message); }
        }

        private void MostrarError(string msg)
        {
            ClientScript.RegisterStartupScript(GetType(), "err", $"alert('{msg.Replace("'", "\\'")}');", true);
        }
    }
}