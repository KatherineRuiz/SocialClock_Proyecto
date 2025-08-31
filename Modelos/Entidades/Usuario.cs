using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string ConsultarClave(string user)
        {
            string resultado = "";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consultaQuery = "SELECT clave FROM Usuario WHERE nombreUsuario = @usr and id_Rol = 1";

                using (SqlCommand cmd = new SqlCommand(consultaQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@usr", user);

                    object valor = cmd.ExecuteScalar();

                    if (valor != null)
                    {
                        resultado = valor.ToString();
                    }
                }
            }

            return resultado;
        }

        public string ConsultarClaveColaborador(string user)
        {
            string resultado = "";

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consultaQuery = "SELECT clave FROM Usuario WHERE nombreUsuario = @usr and id_Rol = 2";

                using (SqlCommand cmd = new SqlCommand(consultaQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@usr", user);
                    object valor = cmd.ExecuteScalar();

                    if (valor != null)
                    {
                        resultado = valor.ToString();
                    }
                }
            }

            return resultado;
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
    }
}
