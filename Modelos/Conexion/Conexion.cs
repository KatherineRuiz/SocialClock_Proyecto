using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Windows;

namespace Modelos
{
    public class Conexion
    {
        private static string servidor = "DESKTOP-V2L6QH5\\SQLEXPRESS";
        private static string baseDeDatos = "Social_Clock";

        public static SqlConnection Conectar()
        {
            try
            {
                //creamos una cadena de conexion
                string cadena =
                    $"Data Source = {servidor},54321;Initial Catalog = {baseDeDatos};Integrated Security = true;";

                //Creamos un objeto de tipo SqlConnection
                SqlConnection con = new SqlConnection(cadena);

                //Abrimos la conexion entre SQL Server y nuestra aplicacion
                con.Open();
                return con;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en la conexion con la base de datos" + ex);
                return null;
            }

        }
    }
}
