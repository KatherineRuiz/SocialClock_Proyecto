using Modelos;
using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Vistas.Formularios
{
    public partial class frmListaProyectos : Form
    {
        public frmListaProyectos()
        {
            InitializeComponent();
            mostrarProyecto();
            proyectoConexion();
            CargarDataGrid(1);//Para mientras
            
        }
        private void RedondearPanel(Panel panel, int radio)
        {
            //Creamos un objeto de tipo GraphicsPath
            GraphicsPath path = new GraphicsPath();
            //Dibujamos la figura
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radio, radio), 180, 90);
            path.AddArc(new Rectangle(panel.Width - radio, 0, radio, radio), 270, 90);
            path.AddArc(new Rectangle(panel.Width - radio, panel.Height - radio, radio, radio), 0, 90);
            path.AddArc(new Rectangle(0, panel.Height - radio, radio, radio), 90, 90);
            path.CloseFigure();
            panel.Region = new Region(path);
        }
       

        //private void btnProyectos_Click(object sender, EventArgs e)
        //{
        //    dgvContenido.DataSource = null;
        //    dgvContenido.DataSource = Proyecto.cargarTodosProyectos();
        //}
        private void mostrarProyecto()
        {
            dgvContenido.DataSource = null;
            dgvContenido.DataSource = Proyecto.CargarTodosProyectos();
        }


        private void proyectoConexion()
        {
            dgvBitacoraEstudiantes.DataSource = null;
            dgvBitacoraEstudiantes.DataSource = Proyecto.CargarEstudianteProyecto();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Proyecto p = new Proyecto();
            p.NombreProyecto = txtNombreProyecto.Text;
            if (rbnActivo.Checked == true)
            {
                p.EstadoProyecto = false;
            }
            else
            {
                p.EstadoProyecto = true;

            }

            p.InsertarProyecto();
            MessageBox.Show("Exito", "Datos ingresados correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mostrarProyecto();

        }

        // Método que rellena los datos de la fila seleccionada
        private void LlenarDatosDesdeFila(DataGridViewRow fila)
        {
            try
            {
                if (fila != null)
                {
                    // Leer los valores directamente
                    string estado = fila.Cells["Estado"].Value?.ToString();
                    string proyecto = fila.Cells["Proyecto"].Value?.ToString();

                    // Asignar al TextBox
                    txtNombreProyecto.Text = proyecto ?? "";

                    // Asignar RadioButtons según estado
                    if (estado == "ACTIVO")
                    {
                        rbnActivo.Checked = true;
                        rbnInactivo.Checked = false;
                    }
                    else if (estado == "INACTIVO")
                    {
                        rbnActivo.Checked = false;
                        rbnInactivo.Checked = true;
                    }
                    else
                    {
                        // Estado desconocido: limpiar ambos
                        rbnActivo.Checked = false;
                        rbnInactivo.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar los datos:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Un clic para rellenar los datos
        private void dgvContenido_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContenido.Rows[e.RowIndex];
                LlenarDatosDesdeFila(fila);
            }
        }

        // Doble clic para ir a la otra pestaña
        private void dgvContenido_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Opcional: puedes volver a llamar el método por seguridad
                DataGridViewRow fila = dgvContenido.Rows[e.RowIndex];
                LlenarDatosDesdeFila(fila);

                // Cambiar de pestaña
                // // Cambia esto por el nombre de tu TabPage
            }
        }


        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            Proyecto p = new Proyecto();
            p.NombreProyecto = txtNombreProyecto.Text;
            if (rbnActivo.Checked == true)
            {
                p.EstadoProyecto = false;
            }
            else
            {
                p.EstadoProyecto = true;

            }

            p.InsertarProyecto();
            MessageBox.Show("Exito", "Datos ingresados correctamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            mostrarProyecto();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Proyecto p = new Proyecto();
            p.NombreProyecto = txtNombreProyecto.Text;
            if (rbnActivo.Checked == true)
            {
                p.EstadoProyecto = false;
            }
            else
            {
                p.EstadoProyecto = true;

            }
            p.IdProyecto = int.Parse(dgvContenido.CurrentRow.Cells[0].Value.ToString());
            if(p.ActualizarProyectos()==true)
            {
                MessageBox.Show("Datos Actualizados correctamente","Exito");
                mostrarProyecto();
            }
            else
            {
                MessageBox.Show("Hubo un error , intenta de nuevo.","Error");
            }
        }

        private void dgvContenido_DoubleClick(object sender, EventArgs e)
        {
            txtNombreProyecto.Text = dgvContenido.CurrentRow.Cells[1].Value.ToString();
            if (dgvContenido.CurrentRow.Cells[2].Value.ToString()=="ACTIVO") {
            rbnActivo.Checked = true;
            }
            else { 
            rbnInactivo.Checked = true;
            
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombreProyecto.Text = "";

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

            
        

        private void frmListaProyectos_Load(object sender, EventArgs e)
        {
            this.dgvBitacoraEstudiantes.CellClick += new DataGridViewCellEventHandler(dgvBitacoraEstudiantes_CellClick);

        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                dgvContenido.DataSource = null;
                dgvContenido.DataSource = Proyecto.Buscar(txtNombreProyecto.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Intenta de nuevo", "Error" + ex);

            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar ID del estudiante
                if (!int.TryParse(txtEstudianteBitacora.Text, out int idEstudiante))
                {
                    MessageBox.Show("Ingrese un ID de estudiante válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEstudianteBitacora.Focus();
                    return;
                }

                // Validar registro de horas
                if (!int.TryParse(txtHoras.Text, out int horas))
                {
                    MessageBox.Show("Ingrese un número válido de horas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtHoras.Focus();
                    return;
                }

                // Crear objeto bitacora
                BitacoraSocial bitacora = new BitacoraSocial
                {
                    IdEstudiante = idEstudiante,
                    RegistroHoras = horas,
                    Descripcion = txtActvidad.Text,
                    FechaBitacora = dtpFechaBitacora.Value.Date
                };

                if (bitacora.InsertarBitacoraSocial())
                {
                    MessageBox.Show("Registro guardado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Limpiar campos
                    txtEstudianteBitacora.Clear();
                    txtHoras.Clear();
                    txtActvidad.Clear();

                    // Actualizar DataGridView
                    CargarDataGrid(Convert.ToInt32(dgvBitacoraEstudiantes.CurrentRow.Cells[0].Value.ToString()));//Para mientras
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el registro", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar.\nDetalle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnLimpiarBitacora_Click(object sender, EventArgs e)
        {
            txtEstudianteBitacora.Text = "";
            txtHoras.Text = "";
            txtActvidad.Text = "";
        }

        private void dgvBitacoraEstudiantes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvBitacoraEstudiantes.Rows[e.RowIndex];

                    txtEstudianteBitacora.Text = fila.Cells["Nombre"].Value.ToString();
                    txtHoras.Text = fila.Cells["Horas"].Value.ToString();
                    txtActvidad.Text = fila.Cells["Proyecto"].Value.ToString();
                   // dtpFechaBitacora.Value = Convert.ToDateTime(fila.Cells["Fecha"].Value);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo mostrar" +ex.Message,"Error Catastrofico");
            }
        }

        private void CargarDataGrid(int idEstudiante)
        {
            BitacoraSocial bitacora = new BitacoraSocial();
            dgvBitacoraEstudiantes.DataSource = BitacoraSocial.CargarBitacoraSocial(idEstudiante);
        }

       
       

        private void btnEliminarProyecto_Click(object sender, EventArgs e)
        {
            //// Verificar que haya una fila seleccionada
            //try
            //{
            //    if (dgvContenido.CurrentRow == null) return;

            //    if (!int.TryParse(dgvContenido.CurrentRow.Cells[0].Value?.ToString(), out int id))
            //    {
            //        MessageBox.Show("ID no válido");
            //        return;
            //    }

            //    string registroAEliminar = dgvContenido.CurrentRow.Cells[1].Value?.ToString();
            //    DialogResult respuesta = MessageBox.Show("¿Quieres eliminar este registro?\n" + registroAEliminar,
            //                                             "Eliminar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //    if (respuesta == DialogResult.Yes)
            //    {
            //        // Crear instancia y asignar id
            //        BitacoraSocial bitacora = new BitacoraSocial();
            //        bitacora.IdBitacora = id;

            //        if (bitacora.eliminarBitacora(id))
            //        {
            //            MessageBox.Show("Registro eliminado\n" + registroAEliminar, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            CargarDataGrid();
            //        }
            //        else
            //        {
            //            MessageBox.Show("No se pudo eliminar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message);
            //}
        }

        private void btnEliminarBitacora_Click(object sender, EventArgs e)
        {

        }

        //private void btnBusqueda_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string termino = txtBusqueda.Text.Trim();

        //        DataTable resultado = BitacoraSocial.Buscar(termino);

        //        if (resultado.Rows.Count > 0)
        //        {
        //            dgvContenido.DataSource = resultado;
        //        }
        //        else
        //        {
        //            MessageBox.Show("No se encontraron estudiantes con ese criterio.", "Resultado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            dgvContenido.DataSource = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Ocurrió un error al realizar la búsqueda:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        private void btnEliminarProyecto_Click_1(object sender, EventArgs e)
        {
            try
            {
                BitacoraSocial bitacora = new BitacoraSocial();
                int id = int.Parse(dgvContenido.CurrentRow.Cells[0].Value.ToString());
                ///Esto me sirve para indicar que registro quiero eliminar y que aparezca en el messagebox
                string registroAEliminar = dgvContenido.CurrentRow.Cells[1].Value.ToString();
                DialogResult respuesta = MessageBox.Show("Quieres eliminar este registro ? \n" + registroAEliminar, "Eliminando un registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    if (bitacora.EliminarBitacora(id) == true)
                    {
                        MessageBox.Show("Registro eliminado\n" + registroAEliminar, "Eliminando", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDataGrid(id);
                    }

                }
                else
                {
                    MessageBox.Show("Registro no eliminado", "No seleccionado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo eliminar , intenta de nuevo", "Error catastrofico" + ex);
            }
        }

      

        private void dgvContenido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvContenido_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContenido.Rows[e.RowIndex];
                LlenarDatosDesdeFila(fila);
            }
            if (e.RowIndex >= 0)
            {
                try
                {
                    // Obtener la fila seleccionada
                    DataGridViewRow fila = dgvContenido.Rows[e.RowIndex];

                    // Obtener el idProyecto desde la celda (asegúrate de que se llama así en el DataGridView)
                    int idProyecto = Convert.ToInt32(fila.Cells["Num."].Value);

                    // Llamar al método del modelo que devuelve los estudiantes del proyecto
                    DataTable estudiantes = Proyecto.ObtenerEstudiantesPorProyecto(idProyecto);

                    // Asignar el resultado al DataGridView que está en el otro tab
                    dgvBitacoraEstudiantes.DataSource = estudiantes;

                    // Cambiar a la pestaña que contiene el dgvBitacoraEstudiantes
                    tabControl1.SelectedTab = tpEstudiantesProyecto; // Asegúrate de que este es el nombre correcto del TabPage
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al seleccionar el proyecto: " + ex.Message);
                }
            }
        }

        private void dgvContenido_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            tabControl1.SelectedTab = tpEstudiantesProyecto;
           
        }

        private void btnRegresarProyectos_Click(object sender, EventArgs e)
        {
        
        }

        private void btnRegresarEstudiantes_Click(object sender, EventArgs e)
        {
            new frmListaProyectos().Show();
            this.Hide();
        }

        private void dgvBitacoraEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvBitacoraEstudiantes.Rows[e.RowIndex];

                txtEstudianteBitacora.Text = fila.Cells["Nombre"].Value?.ToString();
                
                txtHoras.Text = fila.Cells["Horas"].Value?.ToString(); // Asegúrate que esta columna esté en tu consulta SQL como 'Horas'
                txtActvidad.Text = fila.Cells["Proyecto"].Value?.ToString();
                
            }
        }

        private void tpEstudiantesProyecto_Click(object sender, EventArgs e)
        {

        }

        private void btnVerBitacora_Click(object sender, EventArgs e)
        {
            BitacoraSocial bitacora = new BitacoraSocial();
            bitacora.IdEstudiante = int.Parse(dgvBitacoraEstudiantes.CurrentRow.Cells[0].Value.ToString());
           dgvBitacoraEstudiantes.DataSource = BitacoraSocial.ObtenerBitacoraPorEstudiante(bitacora.IdEstudiante);
        }
    }
}
