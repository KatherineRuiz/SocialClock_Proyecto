using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Entidades
{
    public class NivelAcademico
    {
        private int idNivelAcademico;
        private string nombreNivel;

        public int IdNivelAcademico { get => idNivelAcademico; set => idNivelAcademico = value; }
        public string NombreNivel { get => nombreNivel; set => nombreNivel = value; }

        public static DataTable CargarNivelAcademico()
        {
            SqlConnection conexion = Conexion.Conectar();
            string consultaQuery = "select idNivelAcademico, nombreNivel from NivelAcademico;";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable virtualTable = new DataTable();
            add.Fill(virtualTable);
            return virtualTable;
        }
    }
}
