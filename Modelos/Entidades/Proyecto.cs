using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Modelos.Entidades
{
    public class Proyecto
    {
        private int idProyecto;
        private string nombreProyecto;
        private bool estadoProyecto;

        public int IdProyecto { get => idProyecto; set => idProyecto = value; }
        public string NombreProyecto { get => nombreProyecto; set => nombreProyecto = value; }
        public bool EstadoProyecto { get => estadoProyecto; set => estadoProyecto = value; }

        public static DataTable CargarProyecto()
        {
            SqlConnection conexion = Conexion.Conectar();
            string consultaQuery = "select idProyecto, nombreProyecto, estadoProyecto from Proyecto;";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable virtualTable = new DataTable();
            add.Fill(virtualTable);
            return virtualTable;
        }

        public static DataTable CargarTodosProyectos()
        {
            SqlConnection conexion = Conexion.Conectar();
            string consultaQuery = "select idProyecto As [Num.], nombreProyecto As Proyecto, CASE estadoProyecto\r\nwhen 0 then 'ACTIVO'\r\nwhen 1 then 'INACTIVO'\r\nEND As [Estado]\r\nfrom Proyecto ";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable virtualTable = new DataTable();
            add.Fill(virtualTable);
            return virtualTable;
        }

        public bool InsertarProyecto()
        {

            SqlConnection con = Conexion.Conectar();
            string comando = "Insert into Proyecto(nombreProyecto, estadoProyecto)" + "values (@nombreProyecto, @estadoProyecto);";
            SqlCommand cmd = new SqlCommand(comando, con);
            cmd.Parameters.AddWithValue("@nombreProyecto", nombreProyecto);
            cmd.Parameters.AddWithValue("@estadoProyecto", EstadoProyecto);

            if (cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ActualizarProyectos()

        {
            SqlConnection conexion = Conexion.Conectar();
            string comando = "UPDATE Proyecto set nombreProyecto=@nombreProyecto," + "estadoProyecto=@estadoProyecto where idProyecto=@id";
            SqlCommand cmd = new SqlCommand(comando , conexion);
            cmd.Parameters.AddWithValue("@nombreProyecto", nombreProyecto);
            cmd.Parameters.AddWithValue("@estadoProyecto", estadoProyecto);
            cmd.Parameters.AddWithValue("@id", idProyecto);
            if(cmd.ExecuteNonQuery()>0)
            {
                return true;
            }
            else
            {
                return false;

            }

        }
        public bool EliminarProyectos(int idProyecto)
        {
            SqlConnection conexion;
            try
            {
                conexion = Conexion.Conectar();
                string consultaDelete = "DELETE FROM Proyecto WHERE idProyecto = @idProyecto";
                SqlCommand delete = new SqlCommand(consultaDelete, conexion);
                delete.Parameters.AddWithValue("@idProyecto", idProyecto);

                int filasAfectadas = delete.ExecuteNonQuery();

                return filasAfectadas > 0; 
            }
            catch (Exception)
            {
                return false; 
            }
        }

        public static DataTable BuscarProyecto(string nombre)
        {
            try
            {
                SqlConnection connection = Conexion.Conectar();
                string Comando = "SELECT idProyecto AS [N°],nombreProyecto AS Proyecto," +
                    " CASE estadoProyecto WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado] " +
                    $"FROM Proyecto WHERE nombreProyecto LIKE '%{nombre}%'";

                SqlDataAdapter ad = new SqlDataAdapter(Comando, connection);

                DataTable dt = new DataTable();
                ad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo realizar la busqueda.\nDetalle: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return null;
            }
        }


        public static DataTable ObtenerEstudiantesPorProyecto(int idProyecto)
        {
            DataTable dt = new DataTable();

            try
            {
                string connectionString = Conexion.Conectar().ConnectionString;

                string query = @"
        SELECT 
            Estudiante.idEstudiante AS [N°],
            Estudiante.nombreEstudiante AS Nombre,
            Estudiante.carnet AS Carnet,
            Especialidad.nombreEspecialidad AS Especialidad,
            NivelAcademico.nombreNivel AS [Nivel académico],
            Seccion.nombreSeccion AS Seccion,
            Estudiante.nie AS NIE,
            CASE Estudiante.estadoEstudiante
                WHEN 0 THEN 'ACTIVO'
                WHEN 1 THEN 'INACTIVO'
            END AS Estado,
            Proyecto.nombreProyecto AS Proyecto,
            ISNULL(SUM(BitacoraSocial.registroHoras), 0) AS [Horas]
        FROM 
            Estudiante
        INNER JOIN Esp_Niv_Sec 
            ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec
        INNER JOIN Especialidad 
            ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad
        INNER JOIN NivelAcademico 
            ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico
        INNER JOIN Seccion 
            ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion
        INNER JOIN Proyecto 
            ON Estudiante.id_Proyecto = Proyecto.idProyecto
        LEFT JOIN BitacoraSocial 
            ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante
        WHERE 
            Proyecto.idProyecto = @idProyecto
        GROUP BY 
            Estudiante.idEstudiante,
            Estudiante.nombreEstudiante,
            Estudiante.carnet,
            Especialidad.nombreEspecialidad,
            NivelAcademico.nombreNivel,
            Seccion.nombreSeccion,
            Estudiante.nie,
            Estudiante.estadoEstudiante,
            Proyecto.nombreProyecto;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idProyecto", idProyecto);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    conn.Open();
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener estudiantes por proyecto: " + ex.Message, "Error Catastrofico");
            }

            return dt;
        }




    }
}
