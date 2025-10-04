using Dominio;
using SQL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;   
using WebGrease.Activities;

namespace tp_web_equipo_3b
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Nada por ahora
        }

        protected void btnParticipa_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text.Trim();
            VoucherSQL voucherSQL = new VoucherSQL();

            if (string.IsNullOrEmpty(codigo))
            {
                lblError.Text = "Por favor ingresá un código.";
                return;
            }

            if (!voucherSQL.EsCodigoValido(codigo))
            {
                lblError.Text = "El código ingresado no existe.";
                return;
            }

            if (voucherSQL.EstaUsado(codigo))
            {
                lblError.Text = "Este código ya fue utilizado.";
                return;
            }

            // ✅ Guardamos el código en sesión y redirigimos a la selección de premio
            Session["codigoVoucher"] = codigo;
            Response.Redirect("ElegirProducto.aspx");
        }
    }
}
