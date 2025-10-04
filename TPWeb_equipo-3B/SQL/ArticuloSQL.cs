using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace SQL
{
    public class ArticuloSQL
    {
        private string connectionString = "Data Source=.;Initial Catalog=PromoDB;Integrated Security=True";

        public List<Articulo> Listar()
        {
            List<Articulo> lista = new List<Articulo>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Nombre, Descripcion, Precio, ImagenUrl FROM Articulos", con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Articulo art = new Articulo();
                    art.Id = (int)reader["Id"];
                    art.Nombre = (string)reader["Nombre"];
                    art.Descripcion = (string)reader["Descripcion"];
                    art.Precio = (decimal)reader["Precio"];
                    art.ImagenUrl = reader["ImagenUrl"] != DBNull.Value ? (string)reader["ImagenUrl"] : null;

                    lista.Add(art);
                }
            }

            return lista;
        }
    }
}
