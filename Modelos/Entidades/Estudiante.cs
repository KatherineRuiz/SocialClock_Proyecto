using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Modelos.Entidades
{

    public class Estudiante
    {
        //Declaramos los atributos de la entidad
        private int id;
        private string nombreEstudiante;
        private string carnet;
        private string nie;
        private bool estado;
        private int proyecto;
        private int espNivSec;

        private int especialidad;
        private int nivelAcademico;
        private int seccion;


        public int Id { get => id; set => id = value; }
        public string NombreEstudiante { get => nombreEstudiante; set => nombreEstudiante = value; }
        public string Carnet { get => carnet; set => carnet = value; }
        public string Nie { get => nie; set => nie = value; }
        public bool Estado { get => estado; set => estado = value; }
        public int Proyecto { get => proyecto; set => proyecto = value; }
        public int EspNivSec { get => espNivSec; set => espNivSec = value; }


        public int Especialidad { get => especialidad; set => especialidad = value; }
        public int NivelAcademico { get => nivelAcademico; set => nivelAcademico = value; }
        public int Seccion { get => seccion; set => seccion = value; }


        //Creeamos un metodo estatico de tipo DataTable para cargar los registros que están en la base de datos en una tabla 
        //Metodo Leer
        public static DataTable CargarEstudiantesPrimerAño()
        {
            //Creamso una variable de tipo SqlConnection y llamamos al metodo de la clase Conexion
            SqlConnection conexion = Conexion.Conectar();

            string consultaQuery = "SELECT Estudiante.idEstudiante AS [N°], carnet AS [Carnet], nombreEstudiante AS [Nombre], " +
                "Especialidad.nombreEspecialidad AS [Especialidad], NivelAcademico.nombreNivel AS [Nivel académico], " +
                "Seccion.nombreSeccion AS [Seccion], nie AS [NIE], " +
                "CASE estadoEstudiante WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado], " +
                "Proyecto.nombreProyecto AS [Proyecto], SUM(BitacoraSocial.registroHoras) AS [No. Horas] " +
                "FROM Estudiante " +
                "LEFT JOIN BitacoraSocial ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante " +
                "INNER JOIN Proyecto ON Estudiante.id_Proyecto = Proyecto.idProyecto " +
                "INNER JOIN Esp_Niv_Sec ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec " +
                "INNER JOIN Especialidad ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad " +
                "INNER JOIN NivelAcademico ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico " +
                "INNER JOIN Seccion ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion " +
                "WHERE estadoEstudiante = 0 AND idNivelAcademico = 1 " +
                "GROUP BY Estudiante.idEstudiante, carnet, nombreEstudiante, Especialidad.nombreEspecialidad, " +
                "NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;";

            //Creamos un objeto de tipo SqlDataAdapter para obtener el resultado completo
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            //Creamos un objeto DataTable, una tabla donde se guardara la informacion
            DataTable dataVirtual = new DataTable();
            //Pasamos la informacion de adaptador a la tabla
            add.Fill(dataVirtual);

            return dataVirtual;
        }

        public static DataTable CargarEstudiantesSegundoAño()
        {
            //Creamos una variable de tipo SqlConnection y llamamos al metodo de la clase Conexion
            SqlConnection conexion = Conexion.Conectar();

            string consultaQuery = "SELECT Estudiante.idEstudiante AS [N°], carnet AS [Carnet], nombreEstudiante AS [Nombre], " +
                "Especialidad.nombreEspecialidad AS [Especialidad], NivelAcademico.nombreNivel AS [Nivel académico], " +
                "Seccion.nombreSeccion AS [Seccion], nie AS [NIE], " +
                "CASE estadoEstudiante WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado], " +
                "Proyecto.nombreProyecto AS [Proyecto], SUM(BitacoraSocial.registroHoras) AS [No. Horas] " +
                "FROM Estudiante " +
                "LEFT JOIN BitacoraSocial ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante " +
                "INNER JOIN Proyecto ON Estudiante.id_Proyecto = Proyecto.idProyecto " +
                "INNER JOIN Esp_Niv_Sec ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec " +
                "INNER JOIN Especialidad ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad " +
                "INNER JOIN NivelAcademico ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico " +
                "INNER JOIN Seccion ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion " +
                "WHERE estadoEstudiante = 0 AND idNivelAcademico = 2 " +
                "GROUP BY Estudiante.idEstudiante, carnet, nombreEstudiante, Especialidad.nombreEspecialidad, " +
                "NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;";

            //Creamos un objeto de tipo SqlDataAdapter para obtener el resultado completo
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            //Creamos un objeto DataTable, una tabla donde se guardara la informacion
            DataTable dataVirtual = new DataTable();
            //Pasamos la informacion de adaptador a la tabla
            add.Fill(dataVirtual);

            return dataVirtual;
        }

        public static DataTable CargarEstudiantesTercerAño()
        {
            //Creamos una variable de tipo SqlConnection y llamamos al metodo de la clase Conexion
            SqlConnection conexion = Conexion.Conectar();

            string consultaQuery = "SELECT Estudiante.idEstudiante AS [N°], carnet AS [Carnet], nombreEstudiante AS [Nombre], " +
                "Especialidad.nombreEspecialidad AS [Especialidad], NivelAcademico.nombreNivel AS [Nivel académico], " +
                "Seccion.nombreSeccion AS [Seccion], nie AS [NIE], " +
                "CASE estadoEstudiante WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado], " +
                "Proyecto.nombreProyecto AS [Proyecto], SUM(BitacoraSocial.registroHoras) AS [No. Horas] " +
                "FROM Estudiante " +
                "LEFT JOIN BitacoraSocial ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante " +
                "INNER JOIN Proyecto ON Estudiante.id_Proyecto = Proyecto.idProyecto " +
                "INNER JOIN Esp_Niv_Sec ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec " +
                "INNER JOIN Especialidad ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad " +
                "INNER JOIN NivelAcademico ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico " +
                "INNER JOIN Seccion ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion " +
                "WHERE estadoEstudiante = 0 AND idNivelAcademico = 3 " +
                "GROUP BY Estudiante.idEstudiante, carnet, nombreEstudiante, Especialidad.nombreEspecialidad, " +
                "NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;";


            //Creamos un objeto de tipo SqlDataAdapter para obtener el resultado completo
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            //Creamos un objeto DataTable, una tabla donde se guardara la informacion
            DataTable dataVirtual = new DataTable();
            //Pasamos la informacion de adaptador a la tabla
            add.Fill(dataVirtual);

            return dataVirtual;
        }

        public static DataTable CargarEstudiantesRetirados()
        {
            //Creamso una variable de tipo SqlConnection y llamamos al metodo de la clase Conexion
            SqlConnection conexion = Conexion.Conectar();

            string consultaQuery = "SELECT Estudiante.idEstudiante AS [N°], carnet AS [Carnet], nombreEstudiante AS [Nombre], " +
                 "Especialidad.nombreEspecialidad AS [Especialidad], NivelAcademico.nombreNivel AS [Nivel académico], " +
                "Seccion.nombreSeccion AS [Seccion], nie AS [NIE], " +
                "CASE estadoEstudiante WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado], " +
                "Proyecto.nombreProyecto AS [Proyecto], SUM(BitacoraSocial.registroHoras) AS [No. Horas] " +
                "FROM Estudiante " +
                "LEFT JOIN BitacoraSocial ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante " +
                "INNER JOIN Proyecto ON Estudiante.id_Proyecto = Proyecto.idProyecto " +
                "INNER JOIN Esp_Niv_Sec ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec " +
                "INNER JOIN Especialidad ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad " +
                "INNER JOIN NivelAcademico ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico " +
                "INNER JOIN Seccion ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion " +
                "WHERE estadoEstudiante = 1 " +
                "GROUP BY Estudiante.idEstudiante, carnet, nombreEstudiante, Especialidad.nombreEspecialidad, " +
                "NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;";


            //Creamos un objeto de tipo SqlDataAdapter para obtener el resultado completo
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            //Creamos un objeto DataTable, una tabla donde se guardara la informacion
            DataTable dataVirtual = new DataTable();
            //Pasamos la informacion de adaptador a la tabla
            add.Fill(dataVirtual);

            return dataVirtual;
        }

        //Metodo Insertar
        public bool InsertarEstudiantes()
        {
            EspNivSec = BuscarEspNivSec(especialidad, nivelAcademico, seccion);

            SqlConnection conexion = Conexion.Conectar();

            //Comando para insertar datos a Sql Server
            string consultaQuery = "insert into Estudiante( nombreEstudiante, carnet, nie, estadoEstudiante, id_Proyecto, id_EspNivSec)" +
                "values ( @nombreEstudiante, @carnet, @nie, @estadoEstudiante, @id_Proyecto, @id_EspNivSec);";

            //Creamos un objeto de tipo SqlCommand
            SqlCommand cmd = new SqlCommand(consultaQuery, conexion);

            //Sustituimos los valores temporales por los astributos
            cmd.Parameters.AddWithValue("@nombreEstudiante", nombreEstudiante);
            cmd.Parameters.AddWithValue("@carnet", carnet);
            cmd.Parameters.AddWithValue("@nie", nie);
            cmd.Parameters.AddWithValue("@estadoEstudiante", estado);
            cmd.Parameters.AddWithValue("@id_Proyecto", proyecto);
            cmd.Parameters.AddWithValue("@id_EspNivSec", EspNivSec);

            //Enviamos el comando a SqlServer
                if (cmd.ExecuteNonQuery() > 0)
                {
                    return true;
                }
                else
                {

                    MessageBox.Show("Error");
                    return false;
                }
            
        }

        public int BuscarEspNivSec(int especialidad, int nivelAcademico, int seccion)
        {
            int resultado = 0;

            using (SqlConnection conexion = Conexion.Conectar())
            {
                string consultaQuery = "SELECT idEsp_Niv_Sec FROM Esp_Niv_Sec WHERE id_Especialidad = @esp AND id_NivelAcademico = @niv AND id_Seccion = @sec;";

                using (SqlCommand cmd = new SqlCommand(consultaQuery, conexion))
                {
                    cmd.Parameters.AddWithValue("@esp", especialidad);
                    cmd.Parameters.AddWithValue("@niv", nivelAcademico);
                    cmd.Parameters.AddWithValue("@sec", seccion);

                    object valor = cmd.ExecuteScalar();

                    if (valor != null && int.TryParse(valor.ToString(), out int id))
                    {
                        resultado = id;
                    }
                }
            }

            return resultado;
        }

        public bool ActualizarEstudiantes()
        {
            espNivSec = BuscarEspNivSec(especialidad, nivelAcademico, seccion);
            try
            {
                SqlConnection conexion = Conexion.Conectar(); ;
                string consultaUpdate = "Update Estudiante set carnet = @carnet, nombreEstudiante = @nombreEstudiante, nie = @nie, estadoEstudiante = @estadoEstudiante, id_Proyecto=@proyecto, id_EspNivSec = @espNivSec where idEstudiante = @id";
                SqlCommand actualizar = new SqlCommand(consultaUpdate, conexion);
                actualizar.Parameters.AddWithValue("@carnet", carnet);
                actualizar.Parameters.AddWithValue("@nombreEstudiante", nombreEstudiante);
                actualizar.Parameters.AddWithValue("@nie", nie);
                actualizar.Parameters.AddWithValue("@estadoEstudiante", estado);
                actualizar.Parameters.AddWithValue("@proyecto", proyecto);
                actualizar.Parameters.AddWithValue("@espNivSec", espNivSec);
                actualizar.Parameters.AddWithValue("@id", id);

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

        public bool CarnetEstudiante(string carnet)
        {
            SqlConnection conexion = Conexion.Conectar();

            string consultaQuery = "select Estudiante.idEstudiante As [N°], carnet As [Carnet], nombreEstudiante As [Nombre],Especialidad.nombreEspecialidad As [Especialidad]," +
                "\r\nNivelAcademico.nombreNivel As [Nivel académico], Seccion.nombreSeccion As [Seccion], nie As [NIE], CASE estadoEstudiante" +
                "\r\nwhen 0 then 'ACTIVO'" +
                "\r\nwhen 1 then 'INACTIVO'" +
                "\r\nEND As [Estado],\r\nProyecto.nombreProyecto As [Proyecto], BitacoraSocial.registroHoras As [No. Horas]" +
                "\r\nfrom Estudiante " +
                "\r\nLEFT JOIN \r\nBitacoraSocial on BitacoraSocial.idEstudiante = Estudiante.idEstudiante" +
                "\r\nINNER JOIN\r\nProyecto on Estudiante.id_Proyecto = Proyecto.idProyecto" +
                "\r\nINNER JOIN\r\nEsp_Niv_Sec on Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec" +
                "\r\nINNER JOIN\r\nEspecialidad on Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad" +
                "\r\nINNER JOIN \r\nNivelAcademico on Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico" +
                $"\r\nINNER JOIN \r\nSeccion on Esp_Niv_Sec.id_Seccion = Seccion.idSeccion where carnet like '%{carnet}%'";
            using (SqlCommand cmd = new SqlCommand(consultaQuery, conexion))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
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

        public static DataTable CargarBusqueda(string carnet)
        {
            //Creamos una variable de tipo SqlConnection y llamamos al metodo de la clase Conexion
            SqlConnection conexion = Conexion.Conectar();

            string consultaQuery = "SELECT Estudiante.idEstudiante AS [N°], carnet AS [Carnet], nombreEstudiante AS [Nombre], " +
            "Especialidad.nombreEspecialidad AS [Especialidad], NivelAcademico.nombreNivel AS [Nivel académico], " +
            "Seccion.nombreSeccion AS [Seccion], nie AS [NIE], " +
             "CASE estadoEstudiante WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado], " +
            "Proyecto.nombreProyecto AS [Proyecto], SUM(BitacoraSocial.registroHoras) AS [No. Horas] " +
            "FROM Estudiante " +
            "LEFT JOIN BitacoraSocial ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante " +
            "INNER JOIN Proyecto ON Estudiante.id_Proyecto = Proyecto.idProyecto " +
            "INNER JOIN Esp_Niv_Sec ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec " +
            "INNER JOIN Especialidad ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad " +
            "INNER JOIN NivelAcademico ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico " +
            "INNER JOIN Seccion ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion " +
            $"WHERE carnet LIKE '%{carnet}%' " +
            "GROUP BY Estudiante.idEstudiante, carnet, nombreEstudiante, Especialidad.nombreEspecialidad, " +
            "NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;";


            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            //Creamos un objeto DataTable, una tabla donde se guardara la informacion
            DataTable dataVirtual = new DataTable();
            //Pasamos la informacion de adaptador a la tabla
            try
            {
                add.Fill(dataVirtual);
                if (dataVirtual.Rows.Count > 0)
                {
                    MessageBox.Show("Búsqueda exitosa", "Éxito");
                }
                else
                {
                    MessageBox.Show("No se encontraron resultados para el carnet ingresado", "Sin resultados");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo realizar la busqueda" + ex);
            }

            return dataVirtual;
        }

        public bool EliminarEstudiante(int id)
        {
            SqlConnection con = Conexion.Conectar();
            //Creamos el comando necesario para eliminar datos
            string comando = "Delete from Estudiante where idEstudiante = @id;";
            //Creamos un objeto de tipo SqlCommand
            SqlCommand cmd = new SqlCommand(comando, con);
            cmd.Parameters.AddWithValue("@id", id);
            
            if (cmd.ExecuteNonQuery() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static DataTable CargarEstudianteProyecto(int idProyecto)
        {

            SqlConnection conexion = Conexion.Conectar();
            string consultaQuery = "SELECT Estudiante.idEstudiante AS [N°], carnet AS [Carnet], nombreEstudiante AS [Nombre], " +
                "Especialidad.nombreEspecialidad AS [Especialidad], NivelAcademico.nombreNivel AS [Nivel académico], " +
                "Seccion.nombreSeccion AS [Seccion], nie AS [NIE], " +
                "CASE estadoEstudiante WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado], " +
                "Proyecto.nombreProyecto AS [Proyecto], SUM(BitacoraSocial.registroHoras) AS [No. Horas] " +
                "FROM Estudiante " +
                "LEFT JOIN BitacoraSocial ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante " +
                "INNER JOIN Proyecto ON Estudiante.id_Proyecto = Proyecto.idProyecto " +
                "INNER JOIN Esp_Niv_Sec ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec " +
                "INNER JOIN Especialidad ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad " +
                "INNER JOIN NivelAcademico ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico " +
                "INNER JOIN Seccion ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion " +
                $"WHERE estadoEstudiante = 0 AND idProyecto = {idProyecto} " +
                "GROUP BY Estudiante.idEstudiante, carnet, nombreEstudiante, Especialidad.nombreEspecialidad, " +
                "NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;";
            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            DataTable virtualTable = new DataTable();
            add.Fill(virtualTable);
            return virtualTable;
        }

        public static DataTable BuscarEstudianteProyecto(string carnet, string proyecto)
        {
            //Creamos una variable de tipo SqlConnection y llamamos al metodo de la clase Conexion
            SqlConnection conexion = Conexion.Conectar();

            string consultaQuery = "SELECT Estudiante.idEstudiante AS [N°], carnet AS [Carnet], nombreEstudiante AS [Nombre], " +
            "Especialidad.nombreEspecialidad AS [Especialidad], NivelAcademico.nombreNivel AS [Nivel académico], " +
            "Seccion.nombreSeccion AS [Seccion], nie AS [NIE], " +
             "CASE estadoEstudiante WHEN 0 THEN 'ACTIVO' WHEN 1 THEN 'INACTIVO' END AS [Estado], " +
            "Proyecto.nombreProyecto AS [Proyecto], SUM(BitacoraSocial.registroHoras) AS [No. Horas] " +
            "FROM Estudiante " +
            "LEFT JOIN BitacoraSocial ON BitacoraSocial.idEstudiante = Estudiante.idEstudiante " +
            "INNER JOIN Proyecto ON Estudiante.id_Proyecto = Proyecto.idProyecto " +
            "INNER JOIN Esp_Niv_Sec ON Estudiante.id_EspNivSec = Esp_Niv_Sec.idEsp_Niv_Sec " +
            "INNER JOIN Especialidad ON Esp_Niv_Sec.id_Especialidad = Especialidad.idEspecialidad " +
            "INNER JOIN NivelAcademico ON Esp_Niv_Sec.id_NivelAcademico = NivelAcademico.idNivelAcademico " +
            "INNER JOIN Seccion ON Esp_Niv_Sec.id_Seccion = Seccion.idSeccion " +
            $"WHERE carnet LIKE '%{carnet}%' and nombreProyecto = '{proyecto}'" +
            "GROUP BY Estudiante.idEstudiante, carnet, nombreEstudiante, Especialidad.nombreEspecialidad, " +
            "NivelAcademico.nombreNivel, Seccion.nombreSeccion, nie, estadoEstudiante, Proyecto.nombreProyecto;";


            SqlDataAdapter add = new SqlDataAdapter(consultaQuery, conexion);
            //Creamos un objeto DataTable, una tabla donde se guardara la informacion
            DataTable dataVirtual = new DataTable();
            //Pasamos la informacion de adaptador a la tabla
            try
            {
                add.Fill(dataVirtual);
                if (dataVirtual.Rows.Count > 0)
                {
                    MessageBox.Show("Búsqueda exitosa", "Éxito");
                }
                else
                {
                    MessageBox.Show("No se encontraron resultados para el carnet ingresado", "Sin resultados");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo realizar la busqueda" + ex);
            }

            return dataVirtual;
        }

    }


}

