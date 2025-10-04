using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using Dominio;
using SQL;

namespace tp_web_equipo_3b
{
    public partial class IngresaTusDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                ClienteSQL clienteSQL = new ClienteSQL();
                string documento = txtDocumento.Text.Trim();

                // Validar si ya existe
                if (clienteSQL.DniExiste(documento))
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "⚠ El documento ingresado ya existe en el sistema.";
                    return;
                }

                // Crear objeto Cliente
                Cliente cliente = new Cliente
                {
                    Documento = documento,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Ciudad = txtCiudad.Text.Trim(),
                };

                // Manejar el CP (validación de número)
                int cp;
                if (!int.TryParse(txtCP.Text.Trim(), out cp))
                {
                    lblMensaje.ForeColor = System.Drawing.Color.Red;
                    lblMensaje.Text = "⚠ El Código Postal debe ser un número.";
                    return;
                }
                cliente.CP = cp;

                // Insertar en la BD
                clienteSQL.AgregarCliente(
                    cliente.Documento,
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.Email,
                    cliente.Direccion,
                    cliente.Ciudad,
                    cliente.CP
                );

                lblMensaje.ForeColor = System.Drawing.Color.Green;
                lblMensaje.Text = "✅ Datos guardados correctamente.";
            }
            catch (Exception ex)
            {
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "❌ Error: " + ex.Message;
            }
        }
    }
}
