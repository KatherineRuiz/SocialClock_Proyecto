using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelos.Entidades
{
   public class BitacoraSocial
    {
        private int idBitacora;
        private int registroHoras;
        private string descripcion;
        private DateTime fechaBitacora;
        private int idEstudiante;

        public int IdBitacora { get => idBitacora; set => idBitacora = value; }
        public int RegistroHoras { get => registroHoras; set => registroHoras = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime FechaBitacora { get => fechaBitacora; set => fechaBitacora = value; }
        public int IdEstudiante { get => idEstudiante; set => idEstudiante = value; }


        public bool InsertarBitacoraSocial()
        {
            // Usar using asegura que la conexión se cierre aunque ocurra un error
            using (SqlConnection con = Conexion.Conectar())
            {
                string comando = "INSERT INTO BitacoraSocial (registroHoras, descripcion, fechaBitacora, idEstudiante) " +
                    "VALUES (@registroHoras, @descripcion, @fechaBitacora, @idEstudiante)";

                using (SqlCommand cmd = new SqlCommand(comando, con))
                {
                    // Pasar directamente los valores tipados
                    cmd.Parameters.AddWithValue("@registroHoras", registroHoras);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@fechaBitacora", fechaBitacora);
                    cmd.Parameters.AddWithValue("@idEstudiante", idEstudiante);

                    //Enviamos el comando a SqlServer
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Ingreso correcto");
                        return true;
                    }
                    else
                    {

                        MessageBox.Show("Error");
                        return false;
                    }
                }
            }
        }

        public bool EliminarBitacora(int id)
        {
            try
            {
                using (SqlConnection con = Conexion.Conectar())
                {
                    string consultaDelete = "DELETE FROM BitacoraSocial WHERE idBitacora = @idBitacora";
                    SqlCommand cmd = new SqlCommand(consultaDelete, con);
                    cmd.Parameters.AddWithValue("@idBitacora", id);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DataTable CargarBitacoraSocial(int idEstudiante)
        {
            using (SqlConnection con = Conexion.Conectar())
            {
                string query = "SELECT idBitacora AS [N°], registroHoras AS Horas, descripcion AS Actividad, fechaBitacora AS Fecha, idEstudiante AS Estudiante " +
                    $"FROM BitacoraSocial where idEstudiante = '{idEstudiante}'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ActualizarBitacora()
        {
            try
            {
                SqlConnection conexion = Conexion.Conectar(); ;
                string consultaUpdate = "Update BitacoraSocial set registroHoras = @horas, descripcion = @descripcion, fechaBitacora = @fecha where idBitacora = @id";
                SqlCommand actualizar = new SqlCommand(consultaUpdate, conexion);
                actualizar.Parameters.AddWithValue("@horas", registroHoras);
                actualizar.Parameters.AddWithValue("@descripcion", descripcion);
                actualizar.Parameters.AddWithValue("@fecha", fechaBitacora);
                actualizar.Parameters.AddWithValue("@id", idBitacora);

                actualizar.ExecuteNonQuery();
                MessageBox.Show("Datos actualizados", "Actualizar");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos" + ex);
                return false;
            }
        }

        public static DataTable ObtenerBitacoraPorEstudiante(int idEstudiante)
        {
            using (SqlConnection con = Conexion.Conectar())
                {
                string query = "SELECT idBitacora AS [N°], registroHoras AS Horas, descripcion AS Actividad, fechaBitacora AS Fecha, idEstudiante AS Estudiante " +
                    $"FROM BitacoraSocial WHERE idEstudiante = @idEstudiante";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                da.SelectCommand.Parameters.AddWithValue("@idEstudiante", idEstudiante);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

    }
}
