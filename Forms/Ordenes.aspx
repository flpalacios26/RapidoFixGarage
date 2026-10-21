<%@ Page Title="Órdenes" Language="C#" MasterPageFile="~/Forms/Site.Master" AutoEventWireup="true" CodeBehind="Ordenes.aspx.cs" Inherits="RapidoFixGarage.Forms.Ordenes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="section-title">🧾 Órdenes de Reparación</h2>

    <!-- 1. CREAR ORDEN -->
    <div class="card">
        <h2>Nueva orden</h2>
        <div class="form-row">
            <div>
                <label>Vehículo (Placa)</label>
                <asp:DropDownList ID="ddlVehiculos" runat="server" />
            </div>
            <div><label>Motivo de ingreso</label><asp:TextBox ID="txtMotivo" runat="server" TextMode="MultiLine" Rows="2" /></div>
            <div><label>Diagnóstico inicial</label><asp:TextBox ID="txtDiagnostico" runat="server" TextMode="MultiLine" Rows="2" /></div>
        </div>
        <asp:Button ID="btnCrear" runat="server" CssClass="btn" Text="Crear orden" OnClick="btnCrear_Click" />
    </div>

    <!-- 2. ÓRDENES ABIERTAS -->
    <div class="card">
        <h2>Órdenes abiertas</h2>
        <asp:GridView ID="gvAbiertas" runat="server" CssClass="data" AutoGenerateColumns="false"
            DataKeyNames="OrdenID" OnRowCommand="gvAbiertas_RowCommand">
            <Columns>
                <asp:BoundField DataField="NumeroOrden" HeaderText="N° Orden" />
                <asp:BoundField DataField="Placa" HeaderText="Placa" />
                <asp:BoundField DataField="ClienteNombre" HeaderText="Cliente" />
                <asp:BoundField DataField="FechaIngreso" HeaderText="Ingreso" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:TemplateField HeaderText="Acción">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Cerrar" CommandArgument='<%# Eval("OrdenID") %>' Text="✔ Cerrar orden" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <!-- 3. ASOCIAR REPUESTOS (El módulo de Pavel) -->
    <div class="card">
        <h2>Asociar repuesto a una orden</h2>
        <div class="form-row">
            <div>
                <label>Orden (N° Orden)</label>
                <asp:DropDownList ID="ddlOrdenesAsociar" runat="server" />
            </div>
            <div>
                <label>Repuesto</label>
                <asp:DropDownList ID="ddlRepuestosAsociar" runat="server" />
            </div>
            <div>
                <label>Cantidad</label>
                <asp:TextBox ID="txtCantidad" runat="server" TextMode="Number" Text="1" />
            </div>
        </div>
        <asp:Button ID="btnAsociar" runat="server" CssClass="btn btn-secondary" Text="Asociar repuesto" OnClick="btnAsociar_Click" />
    </div>

    <!-- 4. HISTORIAL -->
    <div class="card">
        <h2>Historial (Órdenes cerradas)</h2>
        <asp:GridView ID="gvCerradas" runat="server" CssClass="data" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="NumeroOrden" HeaderText="N° Orden" />
                <asp:BoundField DataField="Placa" HeaderText="Placa" />
                <asp:BoundField DataField="FechaIngreso" HeaderText="Ingreso" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="FechaCierre" HeaderText="Cierre" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate><span class="badge badge-closed">Cerrada</span></ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>