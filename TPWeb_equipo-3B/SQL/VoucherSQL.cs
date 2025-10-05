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

        // Verifica si el voucher existe y no fue usado
        public bool CodigoValido(string codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Vouchers WHERE Codigo = @codigo AND Usado = 0", con);
                cmd.Parameters.AddWithValue("@codigo", codigo);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // Marca un voucher como usado
        public void MarcarUsado(string codigo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Vouchers SET Usado = 1 WHERE Codigo = @codigo", con);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.ExecuteNonQuery();
            }
        }

        // Marca el voucher como usado y le asigna un cliente
        public void AsignarCliente(string codigo, int idCliente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Vouchers SET IdCliente = @idCliente, Usado = 1 WHERE Codigo = @codigo", con);

                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.ExecuteNonQuery();
            }
        }

        // Lista todos los vouchers
        public List<Voucher> ListarVouchers()
        {
            List<Voucher> lista = new List<Voucher>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Codigo, IdCliente, IdArticulo, Usado FROM Vouchers", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Voucher v = new Voucher();
                    v.Id = (int)reader["Id"];
                    v.Codigo = (string)reader["Codigo"];
                    v.IdCliente = reader["IdCliente"] != DBNull.Value ? (int)reader["IdCliente"] : 0;
                    v.IdArticulo = reader["IdArticulo"] != DBNull.Value ? (int)reader["IdArticulo"] : 0;
                    v.Usado = (bool)reader["Usado"];

                    lista.Add(v);
                }
            }
            return lista;
        }

        // Asigna cliente y premio al voucher
        public void UpgradeVoucher(string codigoVoucher, int idCliente, int idArticulo)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Vouchers SET IdCliente = @idCliente, IdArticulo = @idArticulo, Usado = 1 WHERE Codigo = @codigo", con);

                cmd.Parameters.AddWithValue("@codigo", codigoVoucher);
                cmd.Parameters.AddWithValue("@idCliente", idCliente);
                cmd.Parameters.AddWithValue("@idArticulo", idArticulo);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
