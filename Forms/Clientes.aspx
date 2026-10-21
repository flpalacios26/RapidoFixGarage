<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Forms/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="RapidoFixGarage.Forms.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="section-title">👤 Gestión de Clientes</h2>

    <!-- FORMULARIO DE REGISTRO -->
    <div class="card">
        <h2>Registrar nuevo cliente</h2>
        <div class="form-row">
            <div><label>Nombre</label><asp:TextBox ID="txtNombre" runat="server" /></div>
            <div><label>Apellido</label><asp:TextBox ID="txtApellido" runat="server" /></div>
            <div><label>Teléfono</label><asp:TextBox ID="txtTelefono" runat="server" /></div>
            <div><label>Email</label><asp:TextBox ID="txtEmail" runat="server" /></div>
            <div><label>Dirección</label><asp:TextBox ID="txtDireccion" runat="server" /></div>
        </div>
        <asp:Button ID="btnGuardar" runat="server" CssClass="btn" Text="Guardar cliente" OnClick="btnGuardar_Click" />
    </div>

    <!-- BÚSQUEDA Y LISTA -->
    <div class="card">
        <h2>Buscar cliente</h2>
        <div class="form-row">
            <div>
                <label>Nombre / Apellido / Teléfono</label>
                <asp:TextBox ID="txtBuscar" runat="server" />
            </div>
            <div style="display:flex;align-items:flex-end;">
                <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-secondary" Text="🔍 Buscar" OnClick="btnBuscar_Click" />
            </div>
        </div>
        <asp:GridView ID="gvClientes" runat="server" CssClass="data" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ClienteID" HeaderText="ID" />
                <asp:BoundField DataField="NombreCompleto" HeaderText="Cliente" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
