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
            connectionString = ConfigurationManager.ConnectionStrings["PromoDB"].ConnectionString;
        }

        // Verifica si el código existe
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

        // Verifica si ya está usado (IdCliente distinto de NULL)
        public bool EstaUsado(string codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT IdCliente FROM Vouchers WHERE Codigo = @codigo", con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value;
            }
        }

        // Marca el voucher como usado asignando un cliente
        public void AsignarCliente(string codigo, int idCliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Vouchers SET IdCliente = @idCliente WHERE Codigo = @codigo", con);
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
                SqlCommand cmd = new SqlCommand("SELECT Id, Codigo, IdCliente FROM Vouchers", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Voucher v = new Voucher();
                    v.Id = (int)reader["Id"];
                    v.Codigo = (string)reader["Codigo"];
                    v.IdCliente = reader["IdCliente"] != DBNull.Value ? (int)reader["IdCliente"] : 0;
                    lista.Add(v);
                }
            }
            return lista;
        }

        // Marca el voucher con cliente y artículo
        public void UpgradeVoucher(string codigoVoucher, int idCliente, int idArticulo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Vouchers SET IdCliente = @idCliente, IdArticulo = @idArticulo WHERE Codigo = @codigo", con);

                cmd.Parameters.AddWithValue("@codigo", codigoVoucher);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@idArticulo", idArticulo);

                cmd.ExecuteNonQuery();
            }
        }

    }
}
