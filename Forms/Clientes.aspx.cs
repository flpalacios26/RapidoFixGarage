using System;
using RapidoFixGarage.DataAccess;
using RapidoFixGarage.Models;

namespace RapidoFixGarage.Forms
{
    public partial class Clientes : System.Web.UI.Page
    {
        private readonly ClienteDAL dal = new ClienteDAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) Cargar("");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                dal.Registrar(new Cliente
                {
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim()
                });
                // Limpiar formulario
                txtNombre.Text = txtApellido.Text = txtTelefono.Text = txtEmail.Text = txtDireccion.Text = "";
                Cargar("");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "err", $"alert('{ex.Message}');", true);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => Cargar(txtBuscar.Text.Trim());

        private void Cargar(string filtro)
        {
            gvClientes.DataSource = dal.Buscar(filtro);
            gvClientes.DataBind();
        }
    }
}