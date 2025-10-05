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
            // Podés cargar el formulario con datos si el DNI ya estaba en Session
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

                // Verifica si ya existe el cliente
                if (!clienteSQL.DniExiste(dni))
                {
                    // Si no existe, lo agrega
                    clienteSQL.AgregarCliente(dni, nombre, apellido, email, direccion, ciudad, cp);
                }

                // Obtengo el Id del cliente
                int idCliente = clienteSQL.ObtenerIdCliente(dni);

                // Recupero voucher y artículo desde Session
                string codigoVoucher = Session["codigoVoucher"] != null ? Session["codigoVoucher"].ToString() : null;
                int idArticulo = Session["idArticulo"] != null ? Convert.ToInt32(Session["idArticulo"]) : 0;

                if (!string.IsNullOrEmpty(codigoVoucher) && idArticulo > 0)
                {
                    VoucherSQL voucherSQL = new VoucherSQL();
                    voucherSQL.UpgradeVoucher(codigoVoucher, idCliente, idArticulo);
                }
                else
                {
                    lblMensaje.Text = "Error: faltan datos de voucher o premio.";
                    return;
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
