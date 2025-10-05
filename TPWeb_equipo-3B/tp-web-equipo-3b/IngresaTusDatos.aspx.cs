using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using SQL;

namespace tp_web_equipo_3b
{
    public partial class IngresaTusDatos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // nada en load por ahora
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string dni = txtDocumento.Text;
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                string email = txtEmail.Text;
                string direccion = txtDireccion.Text;
                string ciudad = txtCiudad.Text;
                int cp = int.Parse(txtCP.Text);

                ClienteSQL clienteSQL = new ClienteSQL();

                // Verifica si ya existe el DNI
                if (!clienteSQL.DniExiste(dni))
                {
                    // Si no existe, agrega cliente nuevo
                    clienteSQL.AgregarCliente(dni, nombre, apellido, email, direccion, ciudad, cp);
                }

                // Guardamos en Session el nombre del cliente para mostrarlo en UsuarioRegistrado
                Session["nombreusuario"] = nombre;

                // Redirige a la pantalla de confirmación
                Response.Redirect("UsuarioRegistrado.aspx");
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error: " + ex.Message;
            }
        }

    }
}
