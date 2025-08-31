using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Entidades
{
    public class Especialidad
    {
        private int idEspecialidad;
        private string nombreEspecialidad;
       
        public int IdEspecialidad { get => idEspecialidad; set => idEspecialidad = value; }
        public string NombreEspecialidad { get => nombreEspecialidad; set => nombreEspecialidad = value; }

        public static DataTable CargarEspecialidad()
        {
            SqlConnection conexion = Conexion.Conectar();
            string consultaQuery = "select idEspecialidad, nombreEspecialidad from Especialidad;";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable virtualTable = new DataTable();
            add.Fill(virtualTable);
            return virtualTable;
        }
    }
}
