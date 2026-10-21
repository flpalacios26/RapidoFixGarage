<%@ Page Title="Reportes" Language="C#" MasterPageFile="~/Forms/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="RapidoFixGarage.Forms.Reportes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="section-title">📊 Reportes e Integración</h2>

    <div class="card">
        <h2>Historial general de Órdenes</h2>
        <asp:GridView ID="gvOrdenes" runat="server" CssClass="data" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="NumeroOrden" HeaderText="N° Orden" />
                <asp:BoundField DataField="Placa" HeaderText="Placa" />
                <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" />
                <asp:BoundField DataField="FechaCierre" HeaderText="Fecha Cierre" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# Eval("Estado").ToString() == "Abierta" ? "<span class='badge badge-open'>Abierta</span>" : "<span class='badge badge-closed'>Cerrada</span>" %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <div class="card">
        <h2>Repuestos utilizados en el taller</h2>
        <asp:GridView ID="gvRepuestos" runat="server" CssClass="data" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="OrdenID" HeaderText="Orden ID" />
                <asp:BoundField DataField="RepuestoNombre" HeaderText="Repuesto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>