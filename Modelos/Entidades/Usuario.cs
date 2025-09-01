using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Modelos.Entidades
{
    public class Usuario
    {

        private int idUsuario;
        private string nombreUsuario;
        private string clave;
        private bool estadoUsuario;
        private int id_Rol;

        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Clave { get => clave; set => clave = value; }
        public bool EstadoUsuario { get => estadoUsuario; set => estadoUsuario = value; }
        public int Id_Rol { get => id_Rol; set => id_Rol = value; }

        public Usuario() { }

        public bool VerificarLoginAdmin(string nombreusuario, string clave)
        {
            string hashEnBaseDeDatos = "";
            SqlConnection con = Conexion.Conectar();
            string query = "Select clave from Usuario Where nombreUsuario = @Usuario and id_Rol = 1";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Usuario", nombreusuario);

            if (cmd.ExecuteScalar() == null)
            {
                return false;
            }
            else
            {
                hashEnBaseDeDatos = cmd.ExecuteScalar().ToString();

                return BCrypt.Net.BCrypt.Verify(clave, hashEnBaseDeDatos);
            }
        }

        public bool VerificarLoginColaborador(string nombreusuario, string clave)
        {
            string hashEnBaseDeDatos = "";
            SqlConnection con = Conexion.Conectar();
            string query = "Select clave from Usuario Where nombreUsuario = @Usuario and id_Rol = 2";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Usuario", nombreusuario);

            if (cmd.ExecuteScalar() == null)
            {
                return false;
            }
            else
            {
                hashEnBaseDeDatos = cmd.ExecuteScalar().ToString();

                return BCrypt.Net.BCrypt.Verify(clave, hashEnBaseDeDatos);
            }
        }

        public static DataTable CargarUsuario()
        {
            SqlConnection conexion = Conexion.Conectar();
            string consultaQuery = "select Usuario.idUsuario, Usuario.nombreUsuario As [Usuario], Rol.nombreRol As [Rol], clave As [Contraseña]," +
                " CASE estadoUsuario\r\nwhen 0 then 'ACTIVO'\r\nwhen 1 then 'INACTIVO'\r\nEND As [Estado]\r\nfrom Usuario" +
                "\r\ninner join\r\nRol On Usuario.id_Rol = Rol.idRol";
            SqlDataAdapter ad = new SqlDataAdapter(consultaQuery, conexion);
            DataTable dt = new DataTable();
            ad.Fill(dt);

            return dt;
        }
        public bool InsertarUsuario()
        {
            SqlConnection con = Conexion.Conectar();
            string comando = "insert into Usuario(nombreUsuario, clave, estadoUsuario, id_Rol)" + "values (@nombreUsuario, @clave, @estadoUsuario, @id_Rol);";
            SqlCommand cmd = new SqlCommand(comando, con);
            cmd.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
            cmd.Parameters.AddWithValue("@clave", clave);
            cmd.Parameters.AddWithValue("@estadoUsuario", estadoUsuario);
            cmd.Parameters.AddWithValue("@id_Rol", id_Rol);


            if (cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EliminarUsuario(int id)
        {
            SqlConnection conexion = Conexion.Conectar();
            string colsultaDelete = "Delete from Usuario where idUsuario = @id";
            SqlCommand delete = new SqlCommand(colsultaDelete, conexion);
            delete.Parameters.AddWithValue("@id", id);
            if (delete.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ActualizarUsuario()
        {
            try
            {
                SqlConnection conexion = Conexion.Conectar();
                string consultaUpdate = "Update Usuario set nombreUsuario = @nombre, clave = @clave, estadoUsuario = @estadoUsuario, id_Rol = @id_Rol where idUsuario = @idUsuario";
                SqlCommand actualizar = new SqlCommand(consultaUpdate, conexion);
                actualizar.Parameters.AddWithValue("@nombre", nombreUsuario);
                actualizar.Parameters.AddWithValue("@clave", clave);
                actualizar.Parameters.AddWithValue("@estadoUsuario", estadoUsuario);
                actualizar.Parameters.AddWithValue("@id_Rol", id_Rol);
                actualizar.Parameters.AddWithValue("@idUsuario", IdUsuario);
                actualizar.ExecuteNonQuery();
                MessageBox.Show("Datos Actualizados", "Actualizar");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar los datos" + ex);
                return false;
            }
        }

        public static DataTable BuscarUsuario(string termino)
        {
            SqlConnection conn = Conexion.Conectar();
            string comando = $"select Usuario.idUsuario, Usuario.nombreUsuario As [Nombre], Rol.nombreRol As [Rol]," +
                $" Usuario.clave As [Clave],CASE estadoUsuario\r\nwhen 0 then 'ACTIVO'\r\nwhen 1 then 'INACTIVO'\r\nEND As [Estado]" +
                $"from Usuario inner join Rol on Usuario.id_Rol = Rol.idRol " +
                $"where Usuario.nombreUsuario LIKE '%{termino}%';";
            SqlDataAdapter ad = new SqlDataAdapter(comando, conn);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            return dt;
        }
    }
}
