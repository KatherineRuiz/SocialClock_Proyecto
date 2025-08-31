using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Entidades
{
    public class Seccion
    {
        private int idSeccion;
        private string nombreSeccion;

        public int IdSeccion { get => idSeccion; set => idSeccion = value; }
        public string NombreSeccion { get => nombreSeccion; set => nombreSeccion = value; }

        public static DataTable CargarSeccion()
        {
            SqlConnection conexion = Conexion.Conectar();
            string consultaQuery = "select idSeccion, nombreSeccion from Seccion;";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable virtualTable = new DataTable();
            add.Fill(virtualTable);
            return virtualTable;
        }
    }
}
