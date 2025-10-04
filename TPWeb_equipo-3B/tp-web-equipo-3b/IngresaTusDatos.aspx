<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IngresaTusDatos.aspx.cs" Inherits="tp_web_equipo_3b.IngresaTusDatos" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ingresar tus Datos</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Ingresá tus datos</h2>

            <asp:Label ID="lblDocumento" runat="server" Text="Documento:"></asp:Label>
            <asp:TextBox ID="txtDocumento" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="lblApellido" runat="server" Text="Apellido:"></asp:Label>
            <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="lblDireccion" runat="server" Text="Dirección:"></asp:Label>
            <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="lblCiudad" runat="server" Text="Ciudad:"></asp:Label>
            <asp:TextBox ID="txtCiudad" runat="server"></asp:TextBox><br /><br />

            <asp:Label ID="lblCP" runat="server" Text="Código Postal: "></asp:Label>
            <asp:TextBox ID="txtCP" runat="server"></asp:TextBox><br /><br />

            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
            <br /><br />
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Green"></asp:Label>
        </div>
    </form>
</body>
</html>
