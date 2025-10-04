using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    internal class ClienteSQL
    {
        public bool dniExiste(string documento)
        public void insertarCliente(string Documento, string Nombre, string Apellido, string Email, string Direccion, string Ciudad, int CP)
        public int ObtenerIdCliente(string Documento)
        public Cliente PrellenarDatos(string dni)
        public Cliente PrellenarDatosPorId(int idCliente)

    }
}
