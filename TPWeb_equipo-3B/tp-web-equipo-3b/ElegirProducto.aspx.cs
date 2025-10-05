using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQL;
using Dominio;


namespace tp_web_equipo_3b
{
    public partial class ElegirProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarPremios();
        }

        private void CargarPremios()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Id, Nombre, Descripcion FROM ARTICULOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo
                    {
                        Id = (int)datos.Lector["Id"],
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"].ToString(),
                        ImagenUrl = "https://via.placeholder.com/200" // ⚡ default si no hay imagen
                    };
                    lista.Add(art);
                }
            }
            finally
            {
                datos.cerrarConexion();
            }

            repPremios.DataSource = lista;
            repPremios.DataBind();
        }

        protected void repPremios_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Elegir")
            {
                if (Session["voucher"] == null)
                {
                    lblMensaje.Text = "El voucher no está definido. Volvé al inicio.";
                    return;
                }

                string codigo = Session["voucher"].ToString();
                int idArticulo = int.Parse(e.CommandArgument.ToString());

                // ⚡ por ahora un cliente de prueba
                int idCliente = 1;

                VoucherSQL voucherSQL = new VoucherSQL();
                try
                {
                    voucherSQL.CanjearVoucher(codigo, idCliente, idArticulo);
                    lblMensaje.CssClass = "text-success";
                    lblMensaje.Text = "¡Canje realizado con éxito!";
                }
                catch (Exception ex)
                {
                    lblMensaje.CssClass = "text-danger";
                    lblMensaje.Text = "Error al canjear: " + ex.Message;
                }
            }
        }
    }
}
