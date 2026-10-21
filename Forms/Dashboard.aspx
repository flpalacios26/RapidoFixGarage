<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Forms/Site.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="RapidoFixGarage.Forms.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="hero">
        <h1>Todo el control del taller en una sola plataforma</h1>
        <p>Gestiona clientes, vehículos, órdenes de reparación y repuestos desde un panel único. Ahorra tiempo y aumenta la rentabilidad de RápidoFix Garage.</p>
    </div>

    <div class="kpi-grid">
        <div class="kpi">
            <div class="kpi-label">Clientes registrados</div>
            <div class="kpi-value"><asp:Label ID="lblClientes" runat="server" Text="0" /></div>
        </div>
        <div class="kpi green">
            <div class="kpi-label">Vehículos en taller</div>
            <div class="kpi-value"><asp:Label ID="lblVehiculos" runat="server" Text="0" /></div>
        </div>
        <div class="kpi cyan">
            <div class="kpi-label">Órdenes abiertas</div>
            <div class="kpi-value"><asp:Label ID="lblOrdenesAbiertas" runat="server" Text="0" /></div>
        </div>
        <div class="kpi amber">
            <div class="kpi-label">Repuestos en stock bajo</div>
            <div class="kpi-value"><asp:Label ID="lblStockBajo" runat="server" Text="0" /></div>
        </div>
    </div>

    <h2 class="section-title">🛠️ Funciones del sistema</h2>
    <div class="features">
        <div class="feature-card">
            <div class="icon">👤</div>
            <h3>Clientes y vehículos</h3>
            <p>Registra y busca clientes, asocia vehículos por placa o VIN y consulta los vehículos vinculados a cada propietario.</p>
            <a href="Clientes.aspx">Gestionar clientes →</a>
        </div>
        <div class="feature-card">
            <div class="icon">🧾</div>
            <h3>Órdenes de reparación</h3>
            <p>Crea órdenes, registra motivo de ingreso y diagnóstico, consulta órdenes abiertas y cierra con observaciones.</p>
            <a href="Ordenes.aspx">Ver órdenes →</a>
        </div>
        <div class="feature-card">
            <div class="icon">📦</div>
            <h3>Inventario de repuestos</h3>
            <p>Controla existencias, consulta disponibilidad, asocia repuestos a órdenes y descuenta stock automáticamente.</p>
            <a href="Repuestos.aspx">Ver inventario →</a>
        </div>
        <div class="feature-card">
            <div class="icon">📊</div>
            <h3>Reportes e integración</h3>
            <p>Reporte de órdenes abiertas/cerradas, reparaciones por vehículo, repuestos utilizados y existencias del inventario.</p>
            <a href="Reportes.aspx">Ver reportes →</a>
        </div>
    </div>
</asp:Content>