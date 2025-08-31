using System;
using Modelos.Entidades;
using Modelos;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;

namespace Vistas.Formularios
{
    public partial class frmProyectos_Colaborador : Form
    {
        public frmProyectos_Colaborador()
        {
            InitializeComponent();
            mostrarProyecto();
            proyectoConexion();
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

        private void mostrarProyecto()
        {
            dgvContenido.DataSource = null;
            dgvContenido.DataSource = Proyecto.CargarTodosProyectos();
        }

        private void proyectoConexion()
        {
            dgvBitacoraEstudiantesColaborador.DataSource = null;
            dgvBitacoraEstudiantesColaborador.DataSource = Proyecto.ObtenerEstudiantesPorProyecto(idProyecto);
        }


        private void dgvContenido_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Asegura que no sea encabezado
            {
            // Leer los datos del registro seleccionado
                DataGridViewRow fila = dgvContenido.Rows[e.RowIndex];

                string id = fila.Cells["Num."].Value.ToString();
                string Proyecto = fila.Cells["Proyecto"].Value.ToString();
                // cbProyecto.Text = Proyecto ;

                tabControl1.SelectedTab = tpEstudiantesProyecto;
            }
        }

        private void frmProyectos_Colaborador_Load(object sender, EventArgs e)
        {
            mostrarProyecto();
          
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            new frmProyectos_Colaborador().Show();
            this.Hide();
        }

        private void dgvBitacoraEstudiantes_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvBitacoraEstudiantes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvContenido.Rows[e.RowIndex];

                txtNombreEstudiante.Text = fila.Cells["Nombre"].Value?.ToString() ?? "";
                txtHoras.Text = fila.Cells["Horas"].Value?.ToString() ?? "";
                txtActvidad.Text = fila.Cells["ActividadRealizada"].Value?.ToString() ?? "";
            }
        }

        private void dgvBitacoraEstudiantesColaborador_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow fila = dgvBitacoraEstudiantesColaborador.Rows[e.RowIndex];

                    // Asegúrate de que estas columnas existen exactamente con ese nombre en tu DataGridView
                    txtHoras.Text = fila.Cells["Horas"].Value?.ToString();
                    txtActvidad.Text = fila.Cells["Proyecto"].Value?.ToString();

                    // Si tienes una columna "Fecha", puedes activarla aquí:
                    // dtpFechaBitacora.Value = Convert.ToDateTime(fila.Cells["Fecha"].Value);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo mostrar: " + ex.Message, "Error catastrófico");
            }
        }


        private void btnVerBitacora_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvContenido.SelectedRows.Count > 0)
                {
                    int idEstudiante = Convert.ToInt32(dgvContenido.SelectedRows[0].Cells["Num."].Value);

                    // Asegúrate de tener el using correcto o usar el nombre completo de la clase aquí
                    DataTable bitacora = BitacoraSocial.ObtenerBitacoraPorEstudiante(idEstudiante);

                    dgvBitacoraEstudiantesColaborador.DataSource = bitacora;

                    // Cambiar a la pestaña donde está el dgvBitacoraEstudiantes
                    tabControl1.SelectedTab = tpEstudiantesProyecto;
                }
                else
                {
                    MessageBox.Show("Selecciona un estudiante para ver su bitácora.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("No se pudo realizar la accion"+ex.Message,"Error Catastrofico");
            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
