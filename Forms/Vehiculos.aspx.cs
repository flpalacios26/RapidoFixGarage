using System;
using RapidoFixGarage.DataAccess;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.Forms
{
    public partial class Vehiculos : System.Web.UI.Page
    {
        private readonly VehiculoDAL vehDal = new VehiculoDAL();
        private readonly ClienteDAL cliDal = new ClienteDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarClientes();
                Cargar("");
            }
        }

        private void CargarClientes()
        {
            ddlClientes.DataSource = cliDal.ListarTodos();
            ddlClientes.DataTextField = "NombreCompleto";
            ddlClientes.DataValueField = "ClienteID";
            ddlClientes.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                vehDal.Registrar(new Vehiculo
                {
                    ClienteID = int.Parse(ddlClientes.SelectedValue),
                    Placa = txtPlaca.Text.Trim(),
                    VIN = txtVIN.Text.Trim(),
                    Marca = txtMarca.Text.Trim(),
                    Modelo = txtModelo.Text.Trim(),
                    Anio = string.IsNullOrEmpty(txtAnio.Text) ? (int?)null : int.Parse(txtAnio.Text),
                    Color = txtColor.Text.Trim()
                });
                txtPlaca.Text = txtVIN.Text = txtMarca.Text = txtModelo.Text = txtAnio.Text = txtColor.Text = "";
                Cargar("");
            }
            catch (Exception ex) { ClientScript.RegisterStartupScript(GetType(), "err", $"alert('{ex.Message}');", true); }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => Cargar(txtBuscar.Text.Trim());

        private void Cargar(string filtro)
        {
            gvVehiculos.DataSource = vehDal.Buscar(filtro);
            gvVehiculos.DataBind();
        }
    }
}