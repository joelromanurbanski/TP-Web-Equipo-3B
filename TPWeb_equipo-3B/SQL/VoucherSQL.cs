using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dominio;

namespace SQL
{
    public class VoucherSQL
    {
        private string connectionString;

        public VoucherSQL()
        {
            // Lee la cadena de conexión desde Web.config
            connectionString = ConfigurationManager.ConnectionStrings["PromoDB"].ConnectionString;
        }


        // Verifica si el código existe en la tabla de Vouchers
        public bool EsCodigoValido(string codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Vouchers WHERE Codigo = @codigo", con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Verifica si el código ya fue usado
        public bool EstaUsado(string codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Usado FROM Vouchers WHERE Codigo = @codigo", con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    return Convert.ToBoolean(result);

                return false;
            }
        }

        // Marca un voucher como usado
        public void MarcarComoUsado(string codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Vouchers SET Usado = 1 WHERE Codigo = @codigo", con);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.ExecuteNonQuery();
            }
        }

        // Inserta un voucher nuevo
        public void AgregarVoucher(string codigo, int idCliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO Vouchers (Codigo, IdCliente, Usado) VALUES (@codigo, @idCliente, 0)", con);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);

                cmd.ExecuteNonQuery();
            }
        }

        // Listar todos los vouchers
        public List<Voucher> ListarVouchers()
        {
            List<Voucher> lista = new List<Voucher>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Codigo, IdCliente, Usado FROM Vouchers", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Voucher v = new Voucher();
                    v.Id = (int)reader["Id"];
                    v.Codigo = (string)reader["Codigo"];
                    v.IdCliente = (int)reader["IdCliente"];
                    v.Usado = (bool)reader["Usado"];
                    lista.Add(v);
                }
            }
            return lista;
        }
    }
}
