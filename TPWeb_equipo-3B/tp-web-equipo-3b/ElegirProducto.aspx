<%@ Page Title="Elegir Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ElegirProducto.aspx.cs" Inherits="tp_web_equipo_3b.ElegirProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Elegí tu Artículo</h2>

    <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="ElegirArticulo_Command">
        <ItemTemplate>
            <div class="card" style="width: 18rem; display:inline-block; margin:10px;">
                <img class="card-img-top" src='<%# Eval("ImagenUrl") %>' alt="Imagen Artículo" style="height:200px; object-fit:cover;" />
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    <p class="card-text"><%# Eval("Descripcion") %></p>
                    <p class="card-text"><strong>Precio:</strong> $<%# Eval("Precio") %></p>
                    <asp:Button runat="server" CommandName="Elegir" CommandArgument='<%# Eval("Id") %>' Text="Elegir" CssClass="btn btn-primary" />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
