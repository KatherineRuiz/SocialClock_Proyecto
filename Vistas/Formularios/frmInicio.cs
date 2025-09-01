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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Vistas.Formularios
{
    public partial class frmInicio : Form
    {
        public frmInicio()
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
            MostrarEstudianteRetirado();

            lblEstado.Visible = false;
            rbActivo.Visible = false;
            rbInactivo.Visible = false;
            
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
            pnlEstudiantesRetirados.Visible = estado;
            pnlEspacio4.Visible = estado;
        }

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

        private void MostrarEstudianteRetirado()
        {
            dgvEstudiantesRetirados.DataSource = null;
            dgvEstudiantesRetirados.DataSource = Estudiante.CargarEstudiantesRetirados();
        }

        #region "Metodo para pintar formularios"
        //Creamos un atributo
        private Form activarForm = null;

        private void abrirForm(Form formularioPintar)
        {
            if (activarForm != null)
            //Si existe un formulario abierto, se cerrará
            {
                activarForm.Close();
            }
            //Le damos todos los permisos que tiene la clase form
            activarForm = formularioPintar;
            //Convertimos a un hijo de tipo de form
            formularioPintar.TopLevel = false;
            //Quitamos los bordes
            formularioPintar.FormBorderStyle = FormBorderStyle.None;
            formularioPintar.Dock = DockStyle.Fill;

            pnlBusqueda.Controls.Add(formularioPintar);
            formularioPintar.BringToFront();
            formularioPintar.Show();
        }
        #endregion

        private void btnInscribir_Click(object sender, EventArgs e)
        {
            if (cbEspecialidad.SelectedIndex != -1 && cbNivelAcademico.SelectedIndex != -1 && cbSeccion.SelectedIndex != -1 && cbProyecto.SelectedIndex != -1 &&
                !string.IsNullOrWhiteSpace(txtCarnet.Text) && !string.IsNullOrWhiteSpace(txtNombre.Text))
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

                if (rbActivo.Checked == true)
                {
                    es.Estado = false;
                }
                else if (rbInactivo.Checked == true)
                {
                    es.Estado = true;
                }
                try
                {
                    es.InsertarEstudiantes();

                    MostrarEstudiantePrimerAño();
                    MostrarEstudianteSegundoAño();
                    MostrarEstudianteTercerAño();
                    MostrarEstudianteRetirado();
                    MessageBox.Show("Exelente. Datos registrados", "Inscripción exitosa");
                }
                catch (Exception)
                {

                    MessageBox.Show("Error al insgresar datos", "Advertencia");
                }
            }
            else
            {
                MessageBox.Show("Todos los campos, excepto <Nie>, necesitan ingresarse obligatoriamente ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               
            }
            
        }

        #region Gestión de botones del listado de estudiantes
        private void btnPrimerAño_Click(object sender, EventArgs e)
        {
            pnlSegundoAño.Visible = false;
            pnlEspacio2.Visible = false;
            pnlTercerAño.Visible = false;
            pnlEspacio3.Visible = false;
            pnlEstudiantesRetirados.Visible = false;
            pnlEspacio4.Visible = false;
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

        private void btnSeundoAño_Click(object sender, EventArgs e)
        {
            pnlPrimerAño.Visible = false;
            pnlEspacio1.Visible = false;
            pnlTercerAño.Visible = false;
            pnlEspacio3.Visible = false;
            pnlEstudiantesRetirados.Visible = false;
            pnlEspacio4.Visible = false;
            if (pnlSegundoAño.Visible == false)
            {
                pnlSegundoAño.Visible = true;
                pnlEspacio2.Visible = true;
            }
            else
            {
                ocultarSubTabla(false);
            }
            MostrarEstudianteSegundoAño();
        }

        private void btnTercerAño_Click(object sender, EventArgs e)
        {
            pnlPrimerAño.Visible = false;
            pnlEspacio1.Visible = false;
            pnlSegundoAño.Visible = false;
            pnlEspacio2.Visible = false;
            pnlEstudiantesRetirados.Visible = false;
            pnlEspacio4.Visible = false;
            if (pnlTercerAño.Visible == false)
            {
                pnlTercerAño.Visible = true;
                pnlEspacio3.Visible = true;
            }
            else
            {
                ocultarSubTabla(false);
            }

            MostrarEstudianteTercerAño();
        }

        private void btnEstudiantesRetirados_Click(object sender, EventArgs e)
        {
            pnlPrimerAño.Visible = false;
            pnlEspacio1.Visible = false;
            pnlSegundoAño.Visible = false;
            pnlEspacio2.Visible = false;
            pnlTercerAño.Visible = false;
            pnlEspacio3.Visible = false;
            if (pnlEstudiantesRetirados.Visible == false)
            {
                pnlEstudiantesRetirados.Visible = true;
                pnlEspacio4.Visible = true;
            }
            else
            {
                ocultarSubTabla(false);
            }

            MostrarEstudianteRetirado();
        }
        #endregion

        private void frmInicio_Load(object sender, EventArgs e)
        {
            RedondearPanel(pnlBienvenida, 40);
            ocultarSubTabla(false);

        }

        #region Metodos para cargar datos que se actualizaran
        private void dgvPrimerAño_DoubleClick(object sender, EventArgs e)
        {
            txtCarnet.Text = dgvPrimerAño.CurrentRow.Cells[1].Value.ToString();
            txtNombre.Text = dgvPrimerAño.CurrentRow.Cells[2].Value.ToString();
            cbEspecialidad.Text =dgvPrimerAño.CurrentRow.Cells[3].Value.ToString();
            cbNivelAcademico.Text = dgvPrimerAño.CurrentRow.Cells[4].Value.ToString();
            cbSeccion.Text = dgvPrimerAño.CurrentRow.Cells[5].Value.ToString();
            txtNie.Text = dgvPrimerAño.CurrentRow.Cells[6].Value.ToString();
            cbProyecto.Text = dgvPrimerAño.CurrentRow.Cells[8].Value.ToString();


            lblEstado.Visible = true;
            rbActivo.Visible = true;
            rbInactivo.Visible = true;

        }

        private void dgvSegundoAño_DoubleClick(object sender, EventArgs e)
        {
            txtCarnet.Text = dgvSegundoAño.CurrentRow.Cells[1].Value.ToString();
            txtNombre.Text = dgvSegundoAño.CurrentRow.Cells[2].Value.ToString();
            txtNie.Text = dgvSegundoAño.CurrentRow.Cells[6].Value.ToString();
            cbEspecialidad.Text = dgvSegundoAño.CurrentRow.Cells[3].Value.ToString();
            cbNivelAcademico.Text = dgvSegundoAño.CurrentRow.Cells[4].Value.ToString();
            cbSeccion.Text = dgvSegundoAño.CurrentRow.Cells[5].Value.ToString();
            cbProyecto.Text = dgvSegundoAño.CurrentRow.Cells[8].Value.ToString();

            lblEstado.Visible = true;
            rbActivo.Visible = true;
            rbInactivo.Visible = true;
        }

        private void dgvTercerAño_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtCarnet.Text = dgvTercerAño.CurrentRow.Cells[1].Value.ToString();
            txtNombre.Text = dgvTercerAño.CurrentRow.Cells[2].Value.ToString();
            txtNie.Text = dgvTercerAño.CurrentRow.Cells[6].Value.ToString();
            cbEspecialidad.Text = dgvTercerAño.CurrentRow.Cells[3].Value.ToString();
            cbNivelAcademico.Text = dgvTercerAño.CurrentRow.Cells[4].Value.ToString();
            cbSeccion.Text = dgvTercerAño.CurrentRow.Cells[5].Value.ToString();
            cbProyecto.Text = dgvTercerAño  .CurrentRow.Cells[8].Value.ToString();

            lblEstado.Visible = true;
            rbActivo.Visible = true;
            rbInactivo.Visible = true;
        }

        private void dgvEstudiantesRetirados_DoubleClick(object sender, EventArgs e)
        {
            txtCarnet.Text = dgvEstudiantesRetirados.CurrentRow.Cells[1].Value.ToString();
            txtNombre.Text = dgvEstudiantesRetirados.CurrentRow.Cells[2].Value.ToString();
            txtNie.Text = dgvEstudiantesRetirados.CurrentRow.Cells[6].Value.ToString();
            cbEspecialidad.Text = dgvEstudiantesRetirados.CurrentRow.Cells[3].Value.ToString();
            cbNivelAcademico.Text = dgvEstudiantesRetirados.CurrentRow.Cells[4].Value.ToString();
            cbSeccion.Text = dgvEstudiantesRetirados.CurrentRow.Cells[5].Value.ToString();
            cbProyecto.Text = dgvEstudiantesRetirados.CurrentRow.Cells[8].Value.ToString();

            lblEstado.Visible = true;
            rbActivo.Visible = true;
            rbInactivo.Visible = true;
        }
        #endregion

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (cbEspecialidad.SelectedIndex != -1 && cbNivelAcademico.SelectedIndex != -1 && cbSeccion.SelectedIndex != -1 && cbProyecto.SelectedIndex != -1 &&
                !string.IsNullOrWhiteSpace(txtCarnet.Text) && !string.IsNullOrWhiteSpace(txtNombre.Text))
            {

                Estudiante es = new Estudiante();
                es.Carnet = txtCarnet.Text;
                es.NombreEstudiante = txtNombre.Text;
                es.Nie = txtNie.Text;
                es.Especialidad = Convert.ToInt32(cbEspecialidad.SelectedValue);
                es.NivelAcademico = Convert.ToInt32(cbNivelAcademico.SelectedValue);
                es.Seccion = Convert.ToInt32(cbSeccion.SelectedValue);
                es.Proyecto = Convert.ToInt32(cbProyecto.SelectedValue);

                if (rbActivo.Checked == true)
                {
                    es.Estado = false;
                }
                else if (rbInactivo.Checked == true)
                {
                    es.Estado = true;
                }

                string registroEditar = "";

                if (dgvPrimerAño.Visible && dgvPrimerAño.CurrentRow != null)
                {
                    es.Id = int.Parse(dgvPrimerAño.CurrentRow.Cells[0].Value.ToString());
                    registroEditar = dgvPrimerAño.CurrentRow.Cells[2].Value?.ToString();
                }

                else if (dgvSegundoAño.Visible && dgvSegundoAño.CurrentRow != null)
                {
                    es.Id = int.Parse(dgvSegundoAño.CurrentRow.Cells[0].Value.ToString());
                    registroEditar = dgvSegundoAño.CurrentRow.Cells[2].Value?.ToString();
                }
                else if (dgvTercerAño.Visible && dgvTercerAño.CurrentRow != null)
                {
                    es.Id = int.Parse(dgvTercerAño.CurrentRow.Cells[0].Value.ToString());
                    registroEditar = dgvTercerAño.CurrentRow.Cells[2].Value?.ToString();
                }
                else if (dgvEstudiantesRetirados.Visible && dgvEstudiantesRetirados.CurrentRow != null)
                {
                    es.Id = int.Parse(dgvEstudiantesRetirados.CurrentRow.Cells[0].Value.ToString());
                    registroEditar = dgvEstudiantesRetirados.CurrentRow.Cells[2].Value?.ToString();
                }
                else
                {
                    MessageBox.Show("Asegúrese de seleccionar un registro", "Advertencia");
                    return;
                }
                DialogResult respuesta = MessageBox.Show("¿Quieres editar este registro?\n" + registroEditar,
                                                         "Editar registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (respuesta == DialogResult.Yes)
                {
                    if (es.ActualizarEstudiantes() == true)
                    {
                        MostrarEstudiantePrimerAño();
                        MostrarEstudianteSegundoAño();
                        MostrarEstudianteTercerAño();
                        MostrarEstudianteRetirado();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo editar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("Todos los campos, excepto <Nie>, necesitan ingresarse obligatoriamente ", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnBusqueda_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                string carnet = txtBuscar.Text;

                Estudiante es = new Estudiante();

                if (es.CarnetEstudiante(carnet) == true)
                {
                    tlpInicio.Visible = false;
                    this.AutoScrollMinSize = new Size(0, 0);
                    abrirForm(new frmResultado(carnet));

                }
                else
                {
                    MessageBox.Show("Error en la búsqueda", "Advertencia");

                }
            }
            else
            {
                MessageBox.Show("Asegurese de ingresar un valor en el campo de búsqueda", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
        }

        #region Validaciones
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo permite números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back) && (e.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo permite letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void txtCarnet_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo permite números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        private void txtNie_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo permite números", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
                return;
            }
        }

        #endregion

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Text = "";
            txtCarnet.Text = "";
            txtNie.Text = "";
            rbActivo.Checked = true;

            lblEstado.Visible = false;
            rbActivo.Visible = false;
            rbInactivo.Visible = false;
        }
    }
}
