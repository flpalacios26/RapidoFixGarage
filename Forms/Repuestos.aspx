<%@ Page Title="Repuestos" Language="C#" MasterPageFile="~/Forms/Site.Master" AutoEventWireup="true" CodeBehind="Repuestos.aspx.cs" Inherits="RapidoFixGarage.Forms.Repuestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="section-title">📦 Inventario de Repuestos</h2>

    <div class="card">
        <h2>Registrar repuesto</h2>
        <div class="form-row">
            <div><label>Código</label><asp:TextBox ID="txtCodigo" runat="server" /></div>
            <div><label>Nombre</label><asp:TextBox ID="txtNombre" runat="server" /></div>
            <div><label>Precio unitario</label><asp:TextBox ID="txtPrecio" runat="server" TextMode="Number" /></div>
            <div><label>Existencia</label><asp:TextBox ID="txtExistencia" runat="server" TextMode="Number" /></div>
            <div><label>Stock mínimo</label><asp:TextBox ID="txtStockMin" runat="server" TextMode="Number" /></div>
        </div>
        <asp:Button ID="btnGuardar" runat="server" CssClass="btn" Text="Guardar repuesto" OnClick="btnGuardar_Click" />
    </div>

    <div class="card">
        <h2>Lista de repuestos</h2>
        <div class="form-row">
            <div><label>Código o Nombre</label><asp:TextBox ID="txtBuscar" runat="server" /></div>
            <div style="display:flex;align-items:flex-end;">
                <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-secondary" Text="🔍 Buscar" OnClick="btnBuscar_Click" />
            </div>
        </div>
        <asp:GridView ID="gvRepuestos" runat="server" CssClass="data" AutoGenerateColumns="false"
            DataKeyNames="RepuestoID" OnRowCommand="gvRepuestos_RowCommand">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                <asp:BoundField DataField="Nombre" HeaderText="Repuesto" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Existencia" HeaderText="Stock" />
                <asp:TemplateField HeaderText="Ajustar">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Sumar" CommandArgument='<%# Eval("RepuestoID") %>' Text="➕ +1" />
                        &nbsp;|&nbsp;
                        <asp:LinkButton runat="server" CommandName="Restar" CommandArgument='<%# Eval("RepuestoID") %>' Text="➖ -1" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>