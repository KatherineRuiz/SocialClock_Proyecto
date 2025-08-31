using Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vistas.Formularios
{
    public partial class frmInicio_Colaborador : Form
    {
        public frmInicio_Colaborador()
        {
            InitializeComponent();
            RedondearPanel(pnlBienvenida, 40);
            ocultarSubTabla(false);
            mostrarNivelAcademico();
            mostrarProyecto();
            mostrarSeccion();
            mostrarEspecialidad();
            MostrarEstudiantePrimerAño();
            MostrarEstudianteSegundoAño();
            MostrarEstudianteTercerAño();
        }

        private void frmInicio_Colaborador_Load(object sender, EventArgs e)
        {

        }
        //Metodo para redondear las esquinas de los paneles
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

        //Método para ocultar subtablas
        private void ocultarSubTabla(bool estado)
        {
            pnlPrimerAño.Visible = estado;
            pnlEspacio1.Visible = estado;
            pnlSegundoAño.Visible = estado;
            pnlEspacio2.Visible = estado;
            pnlTercerAño.Visible = estado;
            pnlEspacio3.Visible = estado;
        }

        #region Gestión de botones del listado de estudiantes
        private void btnPrimerAño_Click(object sender, EventArgs e)
        {
            pnlSegundoAño.Visible = false;
            pnlEspacio2.Visible = false;
            pnlTercerAño.Visible = false;
            pnlEspacio3.Visible = false;
            if (pnlPrimerAño.Visible == false)
            {
                pnlPrimerAño.Visible = true;
                pnlEspacio1.Visible = true;
            }
            else
            {
                ocultarSubTabla(false);
            }

            MostrarEstudiantePrimerAño();
        }

        private void btnSeundoAño_Click_1(object sender, EventArgs e)
        {
            pnlPrimerAño.Visible = false;
            pnlEspacio1.Visible = false;
            pnlTercerAño.Visible = false;
            pnlEspacio3.Visible = false;
            if (pnlSegundoAño.Visible == false)
            {
                pnlSegundoAño.Visible = true;
                pnlEspacio2.Visible = true;
            }
            else
            {
                ocultarSubTabla(false);
            }
        }


        private void btnTercerAño_Click(object sender, EventArgs e)
        {
            pnlPrimerAño.Visible = false;
            pnlEspacio1.Visible = false;
            pnlSegundoAño.Visible = false;
            pnlEspacio2.Visible = false;
            if (pnlTercerAño.Visible == false)
            {
                pnlTercerAño.Visible = true;
                pnlEspacio3.Visible = true;
            }
            else
            {
                ocultarSubTabla(false);
            }
        }

        #endregion



        #region Metodos para mostrar las opciones disponibles de los combo box
        private void mostrarNivelAcademico()
        {
            cbNivelAcademico.DataSource = null;
            cbNivelAcademico.DataSource = NivelAcademico.CargarNivelAcademico();
            cbNivelAcademico.DisplayMember = "nombreNivel";
            cbNivelAcademico.ValueMember = "idNivelAcademico";
            cbNivelAcademico.SelectedIndex = -1;
        }

        private void mostrarEspecialidad()
        {
            cbEspecialidad.DataSource = null;
            cbEspecialidad.DataSource = Especialidad.CargarEspecialidad();
            cbEspecialidad.DisplayMember = "nombreEspecialidad";
            cbEspecialidad.ValueMember = "idEspecialidad";
            cbEspecialidad.SelectedIndex = -1;
        }

        private void mostrarSeccion()
        {
            cbSeccion.DataSource = null;
            cbSeccion.DataSource = Seccion.CargarSeccion();
            cbSeccion.DisplayMember = "nombreSeccion";
            cbSeccion.ValueMember = "idSeccion";
            cbSeccion.SelectedIndex = -1;
        }

        private void mostrarProyecto()
        {
            cbProyecto.DataSource = null;
            cbProyecto.DataSource = Proyecto.CargarProyecto();
            cbProyecto.DisplayMember = "nombreProyecto";
            cbProyecto.ValueMember = "idProyecto";
            cbProyecto.SelectedIndex = -1;
        }
        #endregion

        private void MostrarEstudiantePrimerAño()
        {
            dgvPrimerAño.DataSource = null;
            dgvPrimerAño.DataSource = Estudiante.CargarEstudiantesPrimerAño();
        }

        private void MostrarEstudianteSegundoAño()
        {
            dgvSegundoAño.DataSource = null;
            dgvSegundoAño.DataSource = Estudiante.CargarEstudiantesSegundoAño();
        }

        private void MostrarEstudianteTercerAño()
        {
            dgvTercerAño.DataSource = null;
            dgvTercerAño.DataSource = Estudiante.CargarEstudiantesTercerAño();
        }


        private void btnInscribir_Click(object sender, EventArgs e)
        {
            //Creamos un objeto Estudiante
            Estudiante es = new Estudiante();
            es.Carnet = txtCarnet.Text;
            es.NombreEstudiante = txtNombre.Text;
            es.Nie = txtNie.Text;
            es.Proyecto = Convert.ToInt32(cbProyecto.SelectedValue);
            es.Especialidad = Convert.ToInt32(cbEspecialidad.SelectedValue);
            es.NivelAcademico = Convert.ToInt32(cbNivelAcademico.SelectedValue);
            es.Seccion = Convert.ToInt32(cbSeccion.SelectedValue);

            es.Estado = false;


            es.InsertarEstudiantes();
            MostrarEstudiantePrimerAño();
            MostrarEstudianteSegundoAño();
            MostrarEstudianteTercerAño();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtCarnet.Text = "";
            txtNie.Text = "";
        }

        
    }
}
