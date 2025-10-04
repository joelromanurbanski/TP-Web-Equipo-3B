using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SQL;

namespace tp_web_equipo_3b
{
    public partial class ElegirProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Cargar artículos desde la base
                ArticuloSQL articuloSQL = new ArticuloSQL();
                rptArticulos.DataSource = articuloSQL.Listar();
                rptArticulos.DataBind();

                // Guardar voucher de la página anterior
                if (Session["codigoVoucher"] == null && Request.QueryString["CodigoVoucher"] != null)
                {
                    Session["codigoVoucher"] = Request.QueryString["CodigoVoucher"];
                }
            }
        }

        protected void ElegirArticulo_Command(object sender, CommandEventArgs e)
        {
            int idArticulo = Convert.ToInt32(e.CommandArgument);

            // Guardar selección en sesión
            Session["articuloSeleccionado"] = idArticulo;

            // Redirigir a la siguiente página (registro cliente)
            Response.Redirect("RegistroCliente.aspx");
        }
    }
}
