using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace SQL
{
    public class ClienteSQL
    {
        private string connectionString;

        public ClienteSQL()
        {
            connectionString = ConfigurationManager.ConnectionStrings["PromoDB"].ConnectionString;
        }

        
        public bool DocumentoExiste(string documento)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE Documento = @doc", con);
                cmd.Parameters.AddWithValue("@doc", documento);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        
        public void AgregarCliente(string documento, string nombre, string apellido, string email, string direccion, string ciudad, int cp)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Clientes (Documento, Nombre, Apellido, Email, Direccion, Ciudad, CP) " +
                    "VALUES (@doc, @nombre, @apellido, @correo, @direccion, @ciudad, @cp)", con);

                cmd.Parameters.AddWithValue("@doc", documento);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@apellido", apellido);
                cmd.Parameters.AddWithValue("@correo", email);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@ciudad", ciudad);
                cmd.Parameters.AddWithValue("@cp", cp);

                cmd.ExecuteNonQuery();
            }
        }

        // Devuelve un cliente a partir del Documento
        public Cliente PrellenarDatos(string documento)
        {
            Cliente cliente = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Clientes WHERE Documento = @doc", con);
                cmd.Parameters.AddWithValue("@doc", documento);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        Id = (int)reader["Id"],
                        Documento = reader["Documento"].ToString(),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Email = reader["Email"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        Ciudad = reader["Ciudad"].ToString(),
                        CP = (int)reader["CP"]
                    };
                }
            }
            return cliente;
        }

        // Devuelve el IdCliente por Documento
        public int ObtenerIdCliente(string documento)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id FROM Clientes WHERE Documento = @doc", con);
                cmd.Parameters.AddWithValue("@doc", documento);

                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
    }
}

