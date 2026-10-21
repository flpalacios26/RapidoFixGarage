<%@ Page Title="Vehículos" Language="C#" MasterPageFile="~/Forms/Site.Master" AutoEventWireup="true" CodeBehind="Vehiculos.aspx.cs" Inherits="RapidoFixGarage.Forms.Vehiculos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="section-title">🚗 Gestión de Vehículos</h2>

    <div class="card">
        <h2>Registrar nuevo vehículo</h2>
        <div class="form-row">
            <div>
                <label>Propietario (Cliente)</label>
                <asp:DropDownList ID="ddlClientes" runat="server" />
            </div>
            <div><label>Placa</label><asp:TextBox ID="txtPlaca" runat="server" /></div>
            <div><label>VIN</label><asp:TextBox ID="txtVIN" runat="server" /></div>
            <div><label>Marca</label><asp:TextBox ID="txtMarca" runat="server" /></div>
            <div><label>Modelo</label><asp:TextBox ID="txtModelo" runat="server" /></div>
            <div><label>Año</label><asp:TextBox ID="txtAnio" runat="server" TextMode="Number" /></div>
            <div><label>Color</label><asp:TextBox ID="txtColor" runat="server" /></div>
        </div>
        <asp:Button ID="btnGuardar" runat="server" CssClass="btn" Text="Guardar vehículo" OnClick="btnGuardar_Click" />
    </div>

    <div class="card">
        <h2>Buscar vehículo (por Placa o VIN)</h2>
        <div class="form-row">
            <div>
                <label>Placa o VIN</label>
                <asp:TextBox ID="txtBuscar" runat="server" />
            </div>
            <div style="display:flex;align-items:flex-end;">
                <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-secondary" Text="🔍 Buscar" OnClick="btnBuscar_Click" />
            </div>
        </div>
        <asp:GridView ID="gvVehiculos" runat="server" CssClass="data" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Placa" HeaderText="Placa" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
                <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                <asp:BoundField DataField="Anio" HeaderText="Año" />
                <asp:BoundField DataField="ClienteNombre" HeaderText="Propietario" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
