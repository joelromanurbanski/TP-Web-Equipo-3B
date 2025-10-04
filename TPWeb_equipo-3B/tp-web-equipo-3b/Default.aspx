<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_web_equipo_3b._Default" %>

<!-- 🔹 Bloque de estilos dentro del placeholder "head" -->
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .fondo-verde {
            background-color: rgba(0, 255, 0, 0.3);
            padding: 10px;
            border-radius: 5px;
            margin-top: 20px;
        }

        .centrar {
            text-align: center;
        }

        .input-codigo {
            padding: 10px;
            font-size: 1.2rem;
            width: 300px;
        }

        .btn-participa {
            background-color: yellow;
            color: black;
            font-weight: bold;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            margin-left: 10px;
        }
    </style>
</asp:Content>

<!-- 🔹 Contenido principal dentro de MainContent -->
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="centrar">
        <h2>Ingresá tu código de participación</h2>
        <asp:TextBox ID="txtCodigo" runat="server" CssClass="input-codigo" placeholder="Ej: PROMO123" />
        <asp:Button ID="btnParticipa" runat="server" Text="Participar" CssClass="btn-participa" OnClick="btnParticipa_Click" />
        
        <div class="fondo-verde centrar">
            <asp:Label ID="lblError" runat="server" ForeColor="Red" />
        </div>
    </div>
</asp:Content>
