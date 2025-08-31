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
            dgvBitacoraEstudiantes.DataSource = null;
            dgvBitacoraEstudiantes.DataSource = Proyecto.CargarEstudianteProyecto();
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
    }
}
